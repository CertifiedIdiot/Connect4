using Connect4_ConsoleUI.GameUI;

namespace Connect4_ConsoleUI.Menus
{
    internal static class MainMenu
    {
        internal static void Start()
        {
            RenderGame.StartScreen();
            Run();
        }

        internal static void Run()
        {
            RenderGame.MenuHeader();
            var menuItems = new List<string>() {
                "Main Menu",
                "[1] - Hotseat Game.",
                "[2] - Single Player Game.",
                "[3] - Multiplayer Setup.",
                "[4] - Change Colour Scheme.",
                "[5] - Quit Game."
            };
            switch (new CreateMenu(menuItems, true).UseMenu())
            {
                case "[1] - Hotseat Game.": StartHotSeat(); break;
                case "[2] - Single Player Game.": StartSinglePlayer(); break;
                case "[3] - Multiplayer Setup.": StartNetWorkSetUp(); break;
                case "[4] - Change Colour Scheme.": StartOptionsMenu(); break;
                case "[5] - Quit Game.": ExitTheGame(); break;
            }
        }

        private static void StartSinglePlayer() => new ConsoleConnect4(null!, true, true).Run();

        private static void ExitTheGame()
        {
            RenderGame.ExitScreen();
            Environment.Exit(0);
        }

        private static void StartHotSeat() => new ConsoleConnect4(null!, true).Run();

        private static void StartNetWorkSetUp() => new NetworkSetup().Run();

        private static void StartOptionsMenu() => OptionsMenu.Run();
    }
}
