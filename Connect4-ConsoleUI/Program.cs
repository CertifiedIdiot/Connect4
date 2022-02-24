using Connect4_ConsoleUI;
using Connect4_ConsoleUI.Menus;

//MainMenu.Start();
Console.WriteLine("1) Relay\n" +
    "2) Client");
switch (Console.ReadLine())
{
    case "1":
        RelayDebug.StartRelay();
        break;
    case "2":
        RelayDebug.StartClient();
        break;
    case "3":
        MainMenu.Start();
        break;
}
