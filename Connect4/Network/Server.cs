using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Connect4.Network
{
    public class Server:INetwork
    {
        public string IP { get; set; } = string.Empty;
        private int DataByteCount { get; set; }
        private byte[] DataBuffer { get; set; } = new byte[1024];
        private string DataIn { get; set; } = string.Empty;

        private Socket remoteClient = default!;
        private IPEndPoint remoteClientIPep = default!;
        private Socket newSocket = default!;

        public Server(string IP = "") => this.IP = IP;

        public string Start()
        {
            IPEndPoint IPep = new IPEndPoint(IPAddress.Parse(IP), 9050);
            newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newSocket.Bind(IPep);
            newSocket.Listen(1);
            // Will not continue from this point until a client connects
            remoteClient = newSocket.Accept();
            remoteClientIPep = (IPEndPoint)remoteClient.RemoteEndPoint!;

            return string.Format("Now connected to {0} on port {1}.", remoteClientIPep.Address, remoteClientIPep.Port);
        }

        // TODO: Implement UPnP
        public void StartUPnP()
        {
            throw new NotImplementedException();
        }

        public string Stop()
        {
            remoteClient.Close();
            newSocket.Close();

            return string.Format("Disconnected from {0} on port {1}." + remoteClientIPep.Address, remoteClientIPep.Port);
        }

        public void Send(string message) => remoteClient.Send(Encoding.UTF8.GetBytes(message));

        public string Receive()
        {
            DataByteCount = remoteClient.Receive(DataBuffer);
            DataIn = Encoding.UTF8.GetString(DataBuffer, 0, DataByteCount);
            DataBuffer = new byte[1024]; // Resetting databuffer

            return DataIn;
        }

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
                    foreach (UnicastIPAddressInformation IP in net.GetIPProperties().UnicastAddresses)
                    {
                        if (IP.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = IP.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
    }
}