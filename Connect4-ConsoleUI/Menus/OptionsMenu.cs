using Connect4_ConsoleUI.GameUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4_ConsoleUI.Menus
{
    internal static class OptionsMenu
    {
        internal static void Run()
        {
            RenderGame.MenuHeader();
            var menuItems = new List<string>() {
                "Change the Game Colours",
                "[1] - Default Colour Scheme.",
                "[2] - Placeholder Scheme 1",
                "[3] - Retro à la Pong",
                "[4] - Monochrome n' Chill",
                "[5] - Return to Main Menu"
            };
            switch (new CreateMenu(menuItems, true).UseMenu())
            {
                case "[1] - Default Colour Scheme.": ChangeDisplaySettings(0);
                    break;
                case "[2] - Placeholder Scheme 1": ChangeDisplaySettings(1);
                    break;
                case "[3] - Retro à la Pong": ChangeDisplaySettings(2);
                    break;
                case "[4] - Monochrome n' Chill": ChangeDisplaySettings(3);
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
