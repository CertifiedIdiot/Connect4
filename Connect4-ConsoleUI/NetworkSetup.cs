namespace Connect4_ConsoleUI
{
    using System;
    using System.Text.RegularExpressions;

    internal class NetworkSetup
    {
        public void Run()
        {
            var menuItems = new List<string>() { "Network setup", "Start as server.", "Start as client", "Start hot seat game.", "Exit."};
            var input = new Menu(menuItems,true).UseMenu(); // if true = menu is centered

            switch (input)
            {
                case "Start as server.": StartNetwork(true); break;
                case "Start as client": StartNetwork(false); break;
                case "Start hot seat game.": StartHotSeat(); break;
                case "Exit.": Environment.Exit(0); break;
                default:
                    break;
            }
        }

        private void StartHotSeat() => new QuickTest(null!, true).Run();
        private void StartNetwork(bool startAsServer)
        {
            var network = startAsServer ? Connect4.Connect4Factory.GetServer() : Connect4.Connect4Factory.GetClient();
            Console.Write(startAsServer ? "Enter IP you want to host on: " : "Enter IP you want to connect on: ");
            network.IP = AskForIP();
            Console.WriteLine("Waiting for connection...");
            Console.WriteLine(network.Start());
            Console.ReadLine();
            if (startAsServer) new QuickTest(network, true).Run();
            else new QuickTest(network, false).Run();
        }

        private string AskForIP()
        {
            var input = Console.ReadLine();
            while (!ValidIP(input!))
            {
                Console.Write("Please enter a valid IP: ");
                input = Console.ReadLine();
            }
            return input!;
        }

        private bool ValidIP(string ip)
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
