namespace Connect4_ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class NetworkSetup
    {
        public void Run()
        {
            Console.WriteLine("1) Start as server.");
            Console.WriteLine("2) Start as client.");
            Console.WriteLine("3) Start a hot seat game.");
            Console.WriteLine("Pick a number or ESC to exit.");
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.D1 or ConsoleKey.NumPad1: StartServer(); break;
                case ConsoleKey.D2 or ConsoleKey.NumPad2: StartClient();break;
                case ConsoleKey.D3 or ConsoleKey.NumPad3: StartHotSeat(); break;
                case ConsoleKey.Escape: Environment.Exit(0); break;
                default:
                    break;
            }
        }

        private void StartHotSeat() => new QuickTest(null,true).Run();
        private void StartServer()
        {
            var server = Connect4.Connect4Factory.GetServer();
            Console.Write("Enter IP you want to host on: ");
            server.IP = Console.ReadLine();
            Console.WriteLine("Waiting for connection...");
            Console.WriteLine(server.Start());
            Console.ReadLine();
            new QuickTest(server,true).Run();
        }
        private void StartClient()
        {
            var client = Connect4.Connect4Factory.GetClient();
            Console.Write("Enter IP you want to connect on: ");
            client.IP = Console.ReadLine();
            Console.WriteLine("Waiting for connection...");
            Console.WriteLine(client.Start());
            Console.ReadLine();
            new QuickTest(client,false).Run();
        }
    }
}
