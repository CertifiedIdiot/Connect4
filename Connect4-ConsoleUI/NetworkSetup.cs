﻿namespace Connect4_ConsoleUI
{
using Connect4_ConsoleUI.GameUI;
    using Connect4_ConsoleUI.Menus;
    using System;
    using System.Text.RegularExpressions;

    internal static class NetworkSetup
    {
        public static void Run()
        {
            RenderGame.MenuHeader();
            var menuItems = new List<string>() { "Network setup", "Start as server.", "Start as client", "Return to Main Menu" };
            switch (new CreateMenu(menuItems, true).UseMenu())
            {
                case "Start as server.": StartNetwork(true); break;
                case "Start as client": StartNetwork(false); break;
                case "Return to Main Menu": MainMenu.Run(); break;
            }
        }

        private static void StartNetwork(bool startAsServer)
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

        private static string AskForIP()
        {
            var input = Console.ReadLine();
            while (!ValidIP(input!))
            {
                Console.Write("Please enter a valid IP: ");
                input = Console.ReadLine()!.Trim();
            }
            return input!;
        }

        private static bool ValidIP(string ip)
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
