using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Network;
using Connect4;
using Microsoft.VisualStudio.Threading;

// TODO: Remove me!
namespace Connect4_ConsoleUI
{
    public static class RelayDebug
    {
        static RelayServer relay = new("127.0.0.1");
        static Client client = new("127.0.0.1");
        static public List<string> Result { get; set; } = default!;

        static void UpdateResult()
        {
            var dataIn = client.Receive();
            Result = JsonHandler.Deserialize<List<string>>(dataIn);
        }

        public static async void StartRelay()
        {
            relay.Start();
        }

        public static void StartClient()
        {
            client.Start();
            string username = string.Empty;

            // Send over username
            Console.WriteLine("Sending username...");
            client.Send("Ankan1337");
            while (client.Receive() == "rejected")
            {
                Console.WriteLine("Username: ");
                username = Console.ReadLine();
                client.Send(username!);
                
            }

            client.Send("SendActiveUsers");
            UpdateResult();
            foreach (var user in Result)
            {
                Console.WriteLine(user);
            }
        }        
    }
}
