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
                "[3] - Return to Main Menu"
            };
            switch (new Menu(menuItems, true).UseMenu())
            {
                case "[1] - Default Colour Scheme.":
                    UIProperties.UIColours.ChangeColourScheme(0);
                    RenderGame.SetConsoleSettings();
                    MainMenu.Run();
                    break;
                case "[2] - Placeholder Scheme 1":
                    UIProperties.UIColours.ChangeColourScheme(1);
                    RenderGame.SetConsoleSettings();
                    MainMenu.Run();
                    break;
                case "[3] - Return to Main Menu": MainMenu.Run(); break;
            }
        }
    }
}
