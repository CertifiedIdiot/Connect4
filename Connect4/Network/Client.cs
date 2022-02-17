using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Connect4.Network
{
    public class Client : INetwork
    {
        public string IP { get; set; } = string.Empty;
        private int dataByteCount { get; set; }
        private byte[] dataBuffer { get; set; } = new byte[1024];
        private string dataIn { get; set; } = string.Empty;

        private Socket remoteServer;
        private IPEndPoint remoteServerPpep;

        public Client(string IP="")
        {
            this.IP = IP;
        }

        public string Start()
        {
            IPEndPoint IPep = new IPEndPoint(IPAddress.Parse(IP), 9050);
            remoteServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Attempt connection with server
            try
            {
                remoteServer.Connect(IPep);
                remoteServerPpep = (IPEndPoint)remoteServer.RemoteEndPoint;

                return string.Format("Now connected to {0} on port {1}.", remoteServerPpep.Address, remoteServerPpep.Port);
            }
            catch (Exception ex)
            {
                return string.Format("Error: " + ex.ToString());
            }
        }

        public void Send(string message)
        {
            remoteServer.Send(Encoding.UTF8.GetBytes(message));
        }

        public string Receive()
        {
            dataByteCount = remoteServer.Receive(dataBuffer);
            dataIn = Encoding.UTF8.GetString(dataBuffer, 0, dataByteCount);
            dataBuffer = new byte[1024]; // Resetting dataBuffer

            return dataIn;
        }

        public string Stop()
        {
            remoteServer.Close();
            return string.Format("Disconnected from {0} on port {1}.", remoteServerPpep.Address, remoteServerPpep.Port);
        }
    }
}