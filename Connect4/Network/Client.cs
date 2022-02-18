using System.Net;
using System.Net.Sockets;
using System.Text;
using Connect4.Interfaces;

namespace Connect4.Network
{
    public class Client : INetwork
    {
        public string IP { get; set; } = string.Empty;
        private int DataByteCount { get; set; }
        private byte[] DataBuffer { get; set; } = new byte[1024];
        private string DataIn { get; set; } = string.Empty;

        private Socket remoteServer = default!;
        private IPEndPoint remoteServerPpep = default!;

        public Client(string IP = "") => this.IP = IP;

        public string Start()
        {
            IPEndPoint IPep = new(IPAddress.Parse(IP), 9050);
            remoteServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Attempt connection with server
            try
            {
                remoteServer.Connect(IPep);
                remoteServerPpep = (IPEndPoint)remoteServer.RemoteEndPoint!;

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
            DataByteCount = remoteServer.Receive(DataBuffer);
            DataIn = Encoding.UTF8.GetString(DataBuffer, 0, DataByteCount);
            DataBuffer = new byte[1024]; // Resetting dataBuffer

            return DataIn;
        }

        public string Stop()
        {
            remoteServer.Close();
            return string.Format("Disconnected from {0} on port {1}.", remoteServerPpep.Address, remoteServerPpep.Port);
        }
    }
}