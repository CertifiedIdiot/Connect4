using System.Net;
using System.Net.Sockets;
using System.Text;
using Connect4.Interfaces;

namespace Connect4.Network
{
    /// <summary>
    /// Enables communication with a <see cref="Connect4.Network.Server"/> on the local network or remotly.
    /// </summary>
    /// <remarks>Do NOT attempt to this class without first connecting to a server.</remarks>
    /// <seealso cref="Connect4.Interfaces.INetwork" />
    public class Client : INetwork
    {
        public string IP { get; set; }
        private int DataByteCount { get; set; }
        private byte[] DataBuffer { get; set; } = new byte[1024];
        private string DataIn { get; set; } = string.Empty;

        private Socket ServerSocket = default!;
        private IPEndPoint ServerIPpep = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class and
        /// specifies the <see cref="Server"/> connection IP for <see cref="Client"/>.
        /// </summary>
        /// <param name="IP">The IP.</param>
        public Client(string IP = "") => this.IP = IP;

        /// <summary>
        /// Starts this instance and attempts to contact and connect to a <see cref="Server"/> on 
        /// <see cref="IP"/> with port 9050.
        /// </summary>
        /// <remarks>Will return an error message upon unsuccessful connection attempt.</remarks>
        /// <returns>string connection message with IP and port or error message.</returns>
        public string Start()
        {
            IPEndPoint IPep = new(IPAddress.Parse(IP), 9050);
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Attempt connection with server
            try
            {
                ServerSocket.Connect(IPep);
                ServerIPpep = (IPEndPoint)ServerSocket.RemoteEndPoint!;

                return string.Format("Now connected to {0} on port {1}.", ServerIPpep.Address, ServerIPpep.Port);
            }
            catch (Exception ex)
            {
                return string.Format("Error: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sends the specified message to the connected server with UTF8 encoding.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message)
        {
            ServerSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Listens for sent UTF8 encoded messages from the connected server.
        /// </summary>
        /// <remarks>Will NOT continue until message is received from server.</remarks>
        /// <returns>string with decoded message</returns>
        public string Receive()
        {
            DataByteCount = ServerSocket.Receive(DataBuffer);
            DataIn = Encoding.UTF8.GetString(DataBuffer, 0, DataByteCount);
            DataBuffer = new byte[1024]; // Resetting dataBuffer

            return DataIn;
        }

        /// <summary>
        /// Closes all connections to server.
        /// </summary>
        /// <returns>string disconnection message with IP and port from connected server.</returns>
        public string Stop()
        {
            ServerSocket.Close();
            return string.Format("Disconnected from {0} on port {1}.", ServerIPpep.Address, ServerIPpep.Port);
        }
    }
}