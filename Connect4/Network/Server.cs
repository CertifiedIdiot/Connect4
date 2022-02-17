using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Connect4.Network
{
    public class Server:INetwork
    {
        public string IP { get; set; } = string.Empty;
        private int dataByteCount { get; set; }
        private byte[] dataBuffer { get; set; } = new byte[1024];
        private string dataIn { get; set; } = string.Empty;

        private Socket remoteClient;
        private IPEndPoint remoteClientIPep;
        private Socket newSocket;

        public Server(string IP = "")
        {
            this.IP = IP;
        }

        public string Start()
        {
            IPEndPoint IPep = new IPEndPoint(IPAddress.Parse(IP), 9050);
            newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newSocket.Bind(IPep);
            newSocket.Listen(1);
            // Will not continue from this point until a client connects
            remoteClient = newSocket.Accept();
            remoteClientIPep = (IPEndPoint)remoteClient.RemoteEndPoint;

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

        public void Send(string message)
        {
            remoteClient.Send(Encoding.UTF8.GetBytes(message));
        }

        public string Receive()
        {
            dataByteCount = remoteClient.Receive(dataBuffer);
            dataIn = Encoding.UTF8.GetString(dataBuffer, 0, dataByteCount);
            dataBuffer = new byte[1024]; // Resetting databuffer

            return dataIn;
        }

        public string GetIPV4()
        {
            string output = string.Empty;
            foreach (NetworkInterface net in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (net.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                    net.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    net.OperationalStatus == OperationalStatus.Up)
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