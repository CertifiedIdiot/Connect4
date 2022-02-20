using Connect4_ConsoleUI.GameUI;

namespace Connect4_ConsoleUI.Menus
{
    public static class PlayAgainMenu
    {
        internal static void Rematch(QuickTest qt)
        {
            RenderGame.MenuHeader();
            var menuItems = new List<string>() {
                "Play Again",
                "[1] - Play the opponent again.",
                "[2] - Return to Main Menu.",
                "[3] - Exit Game."
                };
            switch (new CreateMenu(menuItems, true).UseMenu())
            {
                case "[1] - Play the opponent again.": qt.Run(); break;
                case "[2] - Return to Main Menu.": MainMenu.Run(); break;
                case "[3] - Exit Game.": ExitTheGame(); break;
            }
        }

        private static void ExitTheGame()
        {
            RenderGame.ExitScreen();
            Environment.Exit(0);
        }
    }
}
