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
                "[4] - Doom 1989",
                "[5] - Aurora Fourealis",
                "[6] - Return to Main Menu"
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
                case "[4] - Doom 1989":
                    ChangeDisplaySettings(3);
                    break;
                case "[5] - Aurora Fourealis":
                    ChangeDisplaySettings(4);
                    break;
                case "[6] - Return to Main Menu": MainMenu.Run(); break;
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
