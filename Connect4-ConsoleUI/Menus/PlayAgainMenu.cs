using Connect4_ConsoleUI.GameUI;

namespace Connect4_ConsoleUI.Menus
{
    /// <summary>
    /// Menu to be run when the player has won or lost a game. Gives the user choices relating to playing a new round.
    /// </summary>
    public static class PlayAgainMenu
    {
        internal static void Rematch(ConsoleConnect4 qt)
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
                case "[3] - Exit Game.": ExitTheGame(qt); break;
            }
        }

        private static void ExitTheGame(ConsoleConnect4 qt)
        {
            qt.Stop();
            RenderGame.ExitScreen();
            Environment.Exit(0);
        }
    }
}
