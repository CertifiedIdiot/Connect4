using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.VisualStudio.Threading;

namespace Connect4.Network
{

    //TODO: cleanup relay class and make use of a helper class
    public class RelayServer
    {
        public string IP { get; set; }
        private bool ServerIsRunning = true;
        private readonly List<RelayUser> ConnectionPool = new();

        public RelayServer(string IP = "") => this.IP = IP;

        /// <summary>
        /// Will freeze calling thread.
        /// </summary>
        public void Start()
        {
            StartConnectionPool();
        }

        /// <summary>
        /// Sends all RelayUsers in the connection pool that has LobbyIsOpen set to true.
        /// </summary>
        /// <param name="user">The user.</param>
        private void SendActConnPoolUsers(RelayUser user)
        {
            List<string> users = new();

            foreach (RelayUser u in ConnectionPool)
            {
                if (user.LobbyIsOpen)
                {
                    users.Add(u.Username);
                }
            }

            string jsonUsers = JsonHandler.Serialize(users);
            Send(user, jsonUsers);
        }

        /// <summary>
        /// Gets the index of the connection pool user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private int GetConnPoolUserIndex(RelayUser user)
        {
            return ConnectionPool.FindIndex(u => u.Username == user.Username);
        }

        /// <summary>
        /// Adds the user to connection pool.
        /// </summary>
        /// <param name="user">The user.</param>
        private void AddUserToPool(RelayUser user)
        {
            bool accepted = false;

            while (!accepted)
            {
                string username = Receive(user);
                if (CheckValidUsername(username))
                {
                    Send(user, "rejected");
                }
                else
                {
                    accepted = true;
                    Send(user, "accepted");
                    user.Username = username;
                }
            }

            ConnectionPool.Add(user);
            Task.Run(() => AddListener(user)).Forget();
        }

        /// <summary>
        /// Adds a listener to the specified RelayUser.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private void AddListener(RelayUser user)
        {
            string dataIn = string.Empty;
            while (ServerIsRunning)
            {
                dataIn = Receive(user);

                if (dataIn == "invalid")
                {
                    var index = GetConnPoolUserIndex(user);
                    ConnectionPool.RemoveAt(index);
                    return;
                }
                if (dataIn == "sendActiveUsers")
                {
                    SendActConnPoolUsers(user);
                }
                if(dataIn == "openLobby")
                {
                    OpenUserLobby(user);
                    return;
                }
                if (dataIn.StartsWith("connect: "))
                {
                    Task.Run(() => ConnectClient(user, dataIn.Remove(0, 8))).Forget();
                    return;
                }
            }
        }

        //TODO: Cleanup method, split into multiple
        private void ConnectClient(RelayUser user, string username)
        {           
            while (ServerIsRunning)
            {
                string userData;

                userData = Receive(user);
                if (userData == "invalid")
                {
                    var index = GetConnPoolUserIndex(user);
                    ConnectionPool.RemoveAt(index);

                    index = GetConnPoolUserIndex(GetConnPoolUser(username));
                    ConnectionPool.RemoveAt(index);

                    return;
                }
                Send(username, userData);
                userData = Receive(username);
                if (userData == "invalid")
                {
                    var index = GetConnPoolUserIndex(user);
                    ConnectionPool.RemoveAt(index);

                    index = GetConnPoolUserIndex(GetConnPoolUser(username));
                    ConnectionPool.RemoveAt(index);

                    return;
                }
                
                Send(user, Receive(username));
            }
        }

        /// <summary>
        /// Receives a incoming message from the specified RelayUser.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The message.</returns>
        public string Receive(RelayUser user)
        {
            int DataByteCount = default!;
            byte[] DataBuffer = new byte[1024];
            string output;
            // HACK: Prevents force closed connections crashing the relay
            try
            {
                DataByteCount = user.ClientSocket.Receive(DataBuffer);
                output = Encoding.UTF8.GetString(DataBuffer, 0, DataByteCount);
            }
            catch
            {
                output = "invalid";
            }
            

            return output;
        }

        /// <summary>
        /// Receives a incoming message from the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Message from user, "invalid" if message empty or "rejected" if user doesnt exist.</returns>
        public string Receive(string username)
        {
            if (CheckValidUsername(username))
            {
                var dataIn = Receive(GetConnPoolUser(username));
                return string.IsNullOrEmpty(dataIn) ? "invalid" : dataIn;
            }
            else
            {
                return "rejected";
            }
        }

        /// <summary>
        /// Gets the connection pool user with given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>RelayUser</returns>
        private RelayUser GetConnPoolUser(string username)
        {
            return ConnectionPool.Find(u => u.Username.Equals(username))!;
        }

        /// <summary>
        /// Checks the if the username exists in the ConnectionPool.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>True is user exists else False.</returns>
        private bool CheckValidUsername(string username)
        {
            return ConnectionPool.Exists(u => u.Username.Equals(username));
        }

        /// <summary>
        /// Sends a message to the specifed user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public void Send(RelayUser user, string message)
        {
            user.ClientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        private void Send(string username, string message)
        {
            if (CheckValidUsername(username))
            {
                GetConnPoolUser(username).ClientSocket.Send(Encoding.UTF8.GetBytes(message));
            }
        }

        /// <summary>
        /// Starts the connection pool.
        /// </summary>
        /// <returns></returns>
        private void StartConnectionPool()
        {           
            Socket newClientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newClientSocket.Bind(new IPEndPoint(IPAddress.Parse(IP), 9050));

            while (ServerIsRunning)
            {
                RelayUser user = new();
                newClientSocket.Listen(10);
                // Will not continue from this point until a client connects
                user.ClientSocket = newClientSocket.Accept();

                Task.Run(() => AddUserToPool(user)).Forget();
            }
        }

        private void OpenUserLobby(RelayUser user)
        {
            var index = GetConnPoolUserIndex(user);
            ConnectionPool[index].LobbyIsOpen = true;
        }

        /// <summary>
        /// Closes all active sockets and shutsdown the <see cref="RelayServer"/>.
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            ServerIsRunning = false;
            foreach (RelayUser user in ConnectionPool)
            {
                user.ClientSocket.Close();
            }
        }
    }
}
