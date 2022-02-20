using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Network;
using Connect4;

// TODO: Remove me!
namespace Connect4_ConsoleUI
{
    public static class RelayDebug
    {
        static RelayServer relay = new("127.0.0.1");
        static Client client = new("127.0.0.1");
        static public List<string> result { get; set; }

        static void UpdateResult()
        {
            var dataIn = client.Receive();
            result = JsonHandler.Deserialize<List<string>>(dataIn);
        }

        static public void Start()
        {
            relay.Start();
            client.Start();

            relay.Receive();
            new Task(() => UpdateResult());
            client.Send("SendActiveUsers");

            foreach(var user in result)
            {
                Console.WriteLine(user);
            }
            //switch (Console.Readkey())

            //    case 1:
            //    RelayServer relay = new("127.0.0.1");
            //    break;
            //case 2:
            //    new Menu(, true).UseMenu()

            //break;
        }
        
    }
}
