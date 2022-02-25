using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using Connect4.Interfaces;

namespace Connect4.Network
{
    /// <summary>
    /// Enables communication with a <see cref="Client"/> on the local network or remotly.
    /// </summary>
    /// <remarks>Do NOT attempt to use this class without first connecting to a <see cref="Client"/>.</remarks>
    /// <seealso cref="Connect4.Interfaces.INetwork" />
    public class Server : INetwork
    {
        public string IP { get; set; }
        private int DataByteCount { get; set; }
        private byte[] DataBuffer { get; set; } = new byte[1024];
        private string DataIn { get; set; } = string.Empty;

        private Socket ClientSocket = default!;
        private IPEndPoint ClientIPep = default!;
        private Socket newSocket = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class and
        /// specifies the <see cref="Server"/> host IP.
        /// </summary>
        /// <remarks>
        /// Do NOT attempt to host with your public IP.
        /// Use your local IPV4 address and portforward or use UPnP.
        /// Only use your public IP when connecting to a <see cref="Server"/>.
        /// </remarks>
        /// <param name="IP">The IP.</param>
        public Server(string IP = "") => this.IP = IP;

        /// <summary>
        /// Starts this instance and waits for a <see cref="Client"/> connection attempt.
        /// </summary>
        /// <remarks>Will NOT continue until a <see cref="Client"/> connects.</remarks>
        /// <returns>string connection message with IP and port from the <see cref="Client"/> </returns>
        public string Start()
        {
            IPEndPoint IPep = new IPEndPoint(IPAddress.Parse(IP), 9050);
            newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newSocket.Bind(IPep);
            newSocket.Listen(1);
            // Will not continue from this point until a client connects
            ClientSocket = newSocket.Accept();
            ClientIPep = (IPEndPoint)ClientSocket.RemoteEndPoint!;

            return string.Format("Now connected to {0} on port {1}.", ClientIPep.Address, ClientIPep.Port);
        }

        // TODO: Implement UPnP
        public void StartUPnP()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Closes all connections to the <see cref="Client"/>.
        /// </summary>
        /// <returns>string disconnection message with IP and port from connected <see cref="Client"/>.</returns>
        public string Stop()
        {
            ClientSocket.Close();
            newSocket.Close();

            return string.Format("Disconnected from {0} on port {1}." + ClientIPep.Address, ClientIPep.Port);
        }

        /// <summary>
        /// Sends the specified message to the connected <see cref="Client"/> with UTF8 encoding.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message) => ClientSocket.Send(Encoding.UTF8.GetBytes(message));

        /// <summary>
        /// Listens for sent UTF8 encoded messages from the connected <see cref="Client"/>.
        /// </summary>
        /// <remarks>Will NOT continue until a message is received from the <see cref="Client"/>.</remarks>
        /// <returns>string with decoded UTF8 message</returns>
        public string Receive()
        {
            DataByteCount = ClientSocket.Receive(DataBuffer);
            DataIn = Encoding.UTF8.GetString(DataBuffer, 0, DataByteCount);
            DataBuffer = new byte[1024]; // Resetting databuffer

            return DataIn;
        }
    }
}