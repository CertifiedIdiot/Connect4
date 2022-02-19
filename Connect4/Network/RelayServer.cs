using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using Connect4.Interfaces;

namespace Connect4.Network
{
    public class RelayServer
    {
        public string IP { get; set; }
        private bool PoolConnections = true;

        private readonly List<RelayUser> ConnectionPool = new();
        readonly CancellationTokenSource StopToken = new();
        public RelayServer(string IP = "")
        {
            this.IP = IP;
        }

        public void Start()
        {
            StartConnectionPool();
        }
        public string[] GetConnectionPoolUsers()
        {
            string[] users = new string[ConnectionPool.Count];

            for(int i = 0; i < ConnectionPool.Count; i++)
            {
                users[i] = ConnectionPool[i].Username;
            }

            return users;
        }
        public static string Receive(RelayUser user)
        {
            int DataByteCount;
            byte[] DataBuffer = new byte[1024];

            DataByteCount = user.ClientSocket.Receive(DataBuffer);

            return Encoding.UTF8.GetString(DataBuffer, 0, DataByteCount);
        }

        public static void Send(RelayUser user, string message) =>
            user.ClientSocket.Send(Encoding.UTF8.GetBytes(message));

        // TODO: Prevent duplicate usernames
        private void AddUserToPool(RelayUser user)
        {
            user.Username = Receive(user);
            ConnectionPool.Add(user);
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
            foreach(var user in ConnectionPool)
            {
                user.ClientSocket.Close();
            }
        }
    }
}
