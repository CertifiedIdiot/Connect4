using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Linq;
using System.Text.RegularExpressions;

namespace Connect4.Network
{
    public static class NetworkHelpers
    {
        /// <summary>
        /// Gets the most likely IPV4 address in use for internet connection.
        /// </summary>
        /// <remarks>Can and will fail you.</remarks>
        /// <returns>string IPV4 address.</returns>
        public static string GetIPV4()
        {
            string output = string.Empty;
            foreach (NetworkInterface NetIF in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((NetIF.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    NetIF.OperationalStatus == OperationalStatus.Up)
                    ||
                    (NetIF.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    NetIF.OperationalStatus == OperationalStatus.Up))
                {
                    foreach (var IP in NetIF.GetIPProperties().UnicastAddresses.Where(IP => IP.Address.AddressFamily == AddressFamily.InterNetwork))
                    {
                        output = IP.Address.ToString();
                    }
                }
            }
            return output;
        }

        public static bool ValidIP(string ip)
        {
            var regex = new Regex(@"^\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}$");
            if (!regex.IsMatch(ip)) return false;
            var numbers = ip.Split('.');
            foreach (var number in numbers)
            {
                _ = int.TryParse(number, out int num);
                if (num < 0 || num > 255) return false;
            }
            return true;
        }
    }
}
