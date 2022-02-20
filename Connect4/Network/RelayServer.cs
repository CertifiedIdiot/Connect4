using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Connect4.Network
{
    public class RelayServer
    {
        public string IP { get; set; }
        private bool PoolConnections = true;
        private readonly List<RelayUser> ConnectionPool = new();

        public RelayServer(string IP = "") => this.IP = IP;

        public void Start()
        {
            StartConnectionPool();
        }

        void SendActConnPoolUsers(RelayUser user)
        {

            List<string> users = new();

            foreach (RelayUser u in ConnectionPool)
            {
                if(user.LobbyIsOpen == true)
                {
                    users.Add(u.Username);
                }
            }

            string jsonUsers = JsonHandler.Serialize(users);
            Send(user, jsonUsers);
        }

        private void AddUserToPool(RelayUser user)
        {
            bool accepted = false;

            while (!accepted)
            {
                var username = Receive(user);
                if (IsNameDupe(username))
                {
                    Send(user, "rejected");
                }
                else
                {
                    accepted = true;
                }
            }

            ConnectionPool.Add(user);
            Send(user, "accepted");
        }
        private bool IsNameDupe(string username)
        {
            if (ConnectionPool.Exists(u => u.Username.Equals(username)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string Receive(RelayUser user)
        {
            int DataByteCount;
            byte[] DataBuffer = new byte[1024];
            string output;

            DataByteCount = user.ClientSocket.Receive(DataBuffer);
            output = Encoding.UTF8.GetString(DataBuffer, 0, DataByteCount);

            if(output == "sendActiveUsers")
            {
                SendActConnPoolUsers(user);
                return "";
            }
            if(output == "ping")
            {
                return "pong";
            }

            return output;
        }

        public string Receive(string username)
        {
            try
            {
                return Receive(ConnectionPool.Find(u => u.Username.Equals(username)));
            }
            catch (Exception ex)
            {
                return string.Format("Error: " + ex.ToString());
            }
        }

        public static void Send(RelayUser user, string message)
        {
            user.ClientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        public void StartConnectionPool()
        {
            while (PoolConnections)
            {
                IPEndPoint IPep = new(IPAddress.Parse(IP), 9050);
                RelayUser user = new();
                user.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                user.ClientSocket.Bind(IPep);
                user.ClientSocket.Listen(10);
                // Will not continue from this point until a client connects
                user.ClientSocket = user.ClientSocket.Accept();

                new Task(() => AddUserToPool(user)).Start();
            }
        }
        
        public void Stop()
        {
            PoolConnections = false;
            foreach (RelayUser user in ConnectionPool)
            {
                user.ClientSocket.Close();
            }
        }

        /// <summary>
        /// Gets the most likely IPV4 address in use for internet connection.
        /// </summary>
        /// <remarks>Can and will fail you.</remarks>
        /// <returns>string IPV4 address.</returns>
        public static string GetIPV4()
        {
            string output = string.Empty;
            foreach (NetworkInterface net in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((net.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    net.OperationalStatus == OperationalStatus.Up)
                    ||
                    (net.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    net.OperationalStatus == OperationalStatus.Up))
                {
                    foreach (UnicastIPAddressInformation _IP in net.GetIPProperties().UnicastAddresses)
                    {
                        if (_IP.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = _IP.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
    }
}
