namespace Connect4_ConsoleUI.Menus
{
    public static class PlayAgainMenu
    {
        internal static void Hotseat()
        {
            Console.Clear();
            var menuItems = new List<string>() {
                "Play Again",
                "[1] - Play the opponent again.",
                "[2] - Return to Main Menu.",
                "[3] - Exit Game."
                };
            switch (new Menu(menuItems, true).UseMenu())
            {
                case "[1] - Play the opponent again.": StartHotSeat(); break;
                case "[2] - Return to Main Menu.": MainMenu.Run(); break;
                case "[3] - Exit Game.": Environment.Exit(0); break;
            }
        }
        internal static void Network()
        {
            Console.Clear();
            var menuItems = new List<string>() {
                "Play Again",
                "[1] - Play the opponent again.",
                "[2] - Return to Main Menu.",
                "[3] - Exit Game."
                };
            switch (new Menu(menuItems, true).UseMenu())
            {
                case "[1] - Play the opponent again.":
                    // TODO: Insert network "play opponent again" option here, depending on how the network stuff works. 
                    break;
                case "[2] - Return to Main Menu.": MainMenu.Run(); break;
                case "[3] - Exit Game.": Environment.Exit(0); break;
            }
        }
        private static void StartHotSeat() => new QuickTest(null!, true).Run();
    }
}
