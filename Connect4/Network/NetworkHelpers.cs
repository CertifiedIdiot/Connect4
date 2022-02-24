using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Linq;

namespace Connect4.Network
{
    public class NetworkHelpers
    {
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
                    foreach (var IP in net.GetIPProperties().UnicastAddresses.Where(IP => IP.Address.AddressFamily == AddressFamily.InterNetwork))
                    {
                        output = IP.Address.ToString();
                    }
                }
            }
            return output;
        }
    }
}
