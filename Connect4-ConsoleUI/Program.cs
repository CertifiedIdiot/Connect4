// TODO: Remove debug
using Connect4.Network;
Console.WriteLine("1) Host\n2) Connect");
switch(Console.ReadLine())
{
    case "1":       
        Server server = new();
        Console.WriteLine("Is this IP correct? y/n " + server.Ip);
        if(Console.ReadLine().ToLower() == "n")
        {
            Console.WriteLine("Please enter desired IP: ");
            server.Ip = Console.ReadLine();
        }

        Console.WriteLine("Start with UPnP? y/n");
        if (Console.ReadLine().ToLower() == "y")
        {
            Console.WriteLine("Starting with UPnP....");
            Thread.Sleep(2000);
            server.StartWithUPnP();
        }
        else
        {
            server.Start();
        }
        
        while (true)
        {
            string input = Console.ReadLine();
            if (input.ToLower() == "exit")
            {
                server.Stop();
                Environment.Exit(1337);
            }            
            server.SendClean(input);
            Console.WriteLine("Client: " + server.Receive());
        }

    case "2":
        Console.WriteLine("Please enter appropiate IP for the server you wish to connect to, 127.0.0.1 for localhost.");
        Client client = new(Console.ReadLine());
        client.Start();
        while (true)
        {
            Console.WriteLine("Server: " + client.Receive());
            client.SendClean(Console.ReadLine());
        }

    default:
        Console.WriteLine("Invalid Choice");
        Environment.Exit(1337);
        break;
}