using Connect4_ConsoleUI.GameUI;
using Connect4.Network;

namespace Connect4_ConsoleUI.Menus
{
    internal static class RelayMenu
    {
        public static void Run()
        {
            RenderGame.MenuHeader();
            var menuItems = new List<string>() {
                "Connect to lobby/Host lobby on a relay server.",
                "[1] - Open lobby on relay server.",
                "[2] - Connect to lobby on relay server.",
                "[3] - Return to Main Menu."
            };
            switch (new CreateMenu(menuItems, true).UseMenu())
            {
                case "[1] - Open lobby on relay server.": MenuHelpers.StartLobby(); break;
                case "[2] - Connect to lobby on relay server.": MenuHelpers.ConnectToLobby(); break;
                case "[3] - Return to Main Menu.": MainMenu.Run(); break;
            }
        }
    }
}
