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
            var menuItems = new List<string>() {
                "Main Menu",
                "[1] - Hotseat Game.",
                "[2] - Multiplayer Setup.",
                "[3] - Change Display Settings.",
                "[4] - Quit Game."
            };
            switch (new Menu(menuItems, true).UseMenu())
            {
                case "[1] - Hotseat Game.": StartHotSeat(); break;
                case "[2] - Multiplayer Setup.": StartNetWorkSetUp(); break;
                case "[3] - Change Display Settings.": StartOptionsMenu(); break;
                case "[4] - Quit Game.": Environment.Exit(0); break; // TODO - "exiting game"-screen before close
            }
        }

        private static void StartHotSeat() => new QuickTest(null!, true).Run();

        private static void StartNetWorkSetUp() => new NetworkSetup().Run();

        private static void StartOptionsMenu() => OptionsMenu.Run();
    }
}
