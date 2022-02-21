using Connect4_ConsoleUI.GameUI;

namespace Connect4_ConsoleUI.Menus
{
    internal static class OptionsMenu
    {
        internal static void Run()
        {
            RenderGame.MenuHeader();
            var menuItems = new List<string>() {
                "[1] - Default Colour Scheme.",
                "[2] - Retro à la Pong",
                "[3] - Monochrome n' Chill",
                "[4] - Aurora Fourealis",
                "[5] - Return to Main Menu"
            };
            switch (new CreateMenu(menuItems, true, 0).UseMenu())
            {
                case "[1] - Default Colour Scheme.":
                    ChangeDisplaySettings(0);
                    break;
                case "[2] - Retro à la Pong":
                    ChangeDisplaySettings(1);
                    break;
                case "[3] - Monochrome n' Chill":
                    ChangeDisplaySettings(2);
                    break;
                case "[4] - Aurora Fourealis":
                    ChangeDisplaySettings(3);
                    break;
                case "[5] - Return to Main Menu": MainMenu.Run(); break;
            }

        }
        private static void ChangeDisplaySettings(int schemeNumber)
        {
            UIProperties.UIColours.ChangeColourScheme(schemeNumber);
            RenderGame.SetConsoleSettings();
            MainMenu.Run();
        }
    }
}
