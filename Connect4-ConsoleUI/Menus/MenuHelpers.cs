using Connect4;
using Connect4.Interfaces;
using Connect4.Network;
using Connect4_ConsoleUI.GameUI;

namespace Connect4_ConsoleUI.Menus
{
    public static class MenuHelpers
    {
        public static string AskForIP()
        {
            string input = Console.ReadLine()!;
            while (!NetworkHelpers.ValidIP(input!))
            {
                Console.Write("Please enter a valid IP: ");
                input = Console.ReadLine()!.Trim();
            }
            return input!;
        }

        public static INetwork ClientSetup()
        {
            bool accepted = false;

            INetwork network = Connect4.Connect4Factory.GetClient();
            Console.WriteLine("Enter relay server IP: ");
            Console.CursorVisible = true;
            network.IP = AskForIP();
            network.Start();

            Console.WriteLine("Username must not be taken, you will be asked to enter another if username taken.");
            while (!accepted)
            {
                Console.WriteLine("Please enter a username: ");
                string? username = Console.ReadLine();
                network.Send(username!);
                if (network.Receive() == "accepted")
                {
                    accepted = true;
                }
            }
            Console.CursorVisible = false;
            
            return network;
        }

        public static void StartLobby()
        {
            INetwork network = ClientSetup();

            network.Send("openLobby");
            new ConsoleConnect4(network, false).Run();
        }

        public static void ConnectToLobby()
        {
            INetwork network = ClientSetup();

            while (true)
            {
                network.Send("sendActiveUsers");
                var menuItems = JsonHandler.Deserialize<List<string>>(network.Receive());
                menuItems.Insert(0, "Open Lobbys:");
                RenderGame.MenuHeader();
                network.Send("connect: " + new CreateMenu(menuItems, true).UseMenu());
                if (network.Receive() == "accepted")
                {
                    new ConsoleConnect4(network, true).Run();
                    return;
                }
            }
        }

        public static void StartNetwork(bool startAsServer)
        {
            INetwork? network = startAsServer ? Connect4.Connect4Factory.GetServer() : Connect4.Connect4Factory.GetClient();
            Console.Write(startAsServer ? "Enter IP you want to host on: " : "Enter IP you want to connect on: ");
            network.IP = AskForIP();
            Console.WriteLine("Waiting for connection...");
            Console.WriteLine(network.Start());
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            if (startAsServer)
            {
                new ConsoleConnect4(network, true).Run();
            }
            else
            {
                new ConsoleConnect4(network, false).Run();
            }
        }
    }
}