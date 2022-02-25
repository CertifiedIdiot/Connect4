using Connect4_ConsoleUI.Menus;
using Connect4_ConsoleUI;
using Connect4.Network;
//TODO: fix quick and dirty debug
Console.WriteLine("1) Game.\n" +
    "2) Act as relay.");
switch (Console.ReadLine())
{
    case "1":
        MainMenu.Start();
        break;

    case "2":
        new RelayServer(MenuHelpers.AskForIP()).Start();
        break;

    default:
        MainMenu.Start();
        break;
}
