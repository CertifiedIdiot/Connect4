using Connect4.Network;
using Connect4_ConsoleUI;
using Connect4_ConsoleUI.GameUI;
using Connect4_ConsoleUI.Menus;

string IP = NetworkHelpers.GetIPV4();

var menuItems = new List<string>()
{
    "Please enter an IP you want to host on.",
    "[1] - " + IP,
    "[2] - Enter IP manually.",
    "[3] - Exit relay."
};

switch (new CreateMenu(menuItems, true).UseMenu())
{
    case "[2] - Enter IP manually.":
        Console.WriteLine("IP: "); new RelayServer(MenuHelpers.AskForIP()).Start();
        break;

    case "[3] - Exit relay.":
        RenderGame.ExitScreen(); Environment.Exit(1337);
        break;

    default:
        new RelayServer(IP).Start();
        break;
}
