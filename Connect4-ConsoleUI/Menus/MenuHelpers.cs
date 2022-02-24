using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Network;

namespace Connect4_ConsoleUI.Menus
{
    internal static class MenuHelpers
    {
        public static string AskForIP()
        {
            var input = Console.ReadLine();
            while (!NetworkHelpers.ValidIP(input!))
            {
                Console.Write("Please enter a valid IP: ");
                input = Console.ReadLine()!.Trim();
            }
            return input!;
        }

        public static void StartLobby()
        {
            var network = Connect4.Connect4Factory.GetClient();
            network.IP = AskForIP();
            network.Start();
            
        }

        public static void ConnectToLobby()
        {

        }

        public static void StartNetwork(bool startAsServer)
        {
            var network = startAsServer ? Connect4.Connect4Factory.GetServer() : Connect4.Connect4Factory.GetClient();
            Console.Write(startAsServer ? "Enter IP you want to host on: " : "Enter IP you want to connect on: ");
            network.IP = AskForIP();
            Console.WriteLine("Waiting for connection...");
            Console.WriteLine(network.Start());
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            if (startAsServer) new ConsoleConnect4(network, true).Run();
            else new ConsoleConnect4(network, false).Run();
        }
    }
}
