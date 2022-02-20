// -----------------------------------------------------------------------------------------------
//  Menu.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Connect4_ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using UIProperties;
    using Console = Colorful.Console;

    internal class CreateMenu
    {
        #region Private Fields

        private string bottomLine = "";
        private int headerLines = 0;
        private readonly string HelpText = " Arrow keys to navigate, Enter to select.";
        private int infoLines = 0;
        private int menuWidth = 0;
        private string midLine = "";
        private string topLine = "";
        private int menuPosX = 0;
        private int menuPosY = 0;

        #endregion Private Fields

        #region internal Constructors

        internal CreateMenu(IList<string> menuItems, bool center = false, int headerLines = 1, int infoLines = 0)
        {
            if (menuItems == null || menuItems.Count == 0)
            {
                menuItems = new List<string>() { "Autoadded item because of null or empty menu. Check your code!" };
                headerLines = 0;
                infoLines = 0;
            }
            MenuItems = menuItems;
            SetupMenu(center, headerLines, infoLines);
        }

        #endregion internal Constructors

        #region internal Properties

        private IList<string> MenuItems { get; }

        #endregion internal Properties

        #region Private Properties

        private int HeaderLines { get => headerLines; set => headerLines = value >= MenuItems.Count ? 0 : value; }
        private int InfoLines { get => infoLines; set => infoLines = value + HeaderLines >= MenuItems.Count ? 0 : value; }
        private int StartSelected => HeaderLines + InfoLines;

        #endregion Private Properties

        #region internal Methods

        /// <summary>
        /// Draws the menu. Send in true to print menu in the center of the screen.
        /// </summary>
        internal string UseMenu() => DoMenu();

        #endregion internal Methods

        #region Private Methods

        private string DoMenu()
        {
            var highlightItem = StartSelected;
            var userChoice = "";

            SetColors();
            do
            {
                UpdateMenu(highlightItem);
                var input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow or ConsoleKey.UpArrow:
                        if (highlightItem == StartSelected) highlightItem = MenuItems.Count - 1;
                        else highlightItem--;
                        break;

                    case ConsoleKey.RightArrow or ConsoleKey.DownArrow:
                        if (highlightItem == MenuItems.Count - 1) highlightItem = StartSelected;
                        else highlightItem++;
                        break;

                    case ConsoleKey.Enter:
                        userChoice = MenuItems[highlightItem];
                        break;

                    default:
                        if (char.IsDigit(input.KeyChar))
                            highlightItem = StartSelected + int.Parse(input.KeyChar.ToString()) - 1;
                        break;
                }
            } while (userChoice?.Length == 0);
            return userChoice!;
        }

        private static void InvertColors()
        {
            Console.ForegroundColor = UIColours.BackgroundColour;
            Console.BackgroundColor = UIColours.TextColour;
        }

        private static void SetColors()
        {
            Console.ForegroundColor = UIColours.TextColour;
            Console.BackgroundColor = UIColours.BackgroundColour;
        }

        private void SetupMenu(bool center, int headerLines, int infoLines)
        {
            HeaderLines = headerLines;
            InfoLines = infoLines;
            SetupPrintables();
            SetupPosition(center);
        }

        private void SetupPosition(bool center)
        {
            if (center)
            {
                menuPosX = (Console.WindowWidth / 2) - (bottomLine.Length / 2);
                menuPosY = (Console.WindowHeight / 2) - MenuItems.Count;
            }
        }

        private void SetupPrintables()
        {
            menuWidth = HelpText.Length;
            foreach (var item in MenuItems.Where(item => item.Length > menuWidth))
            {
                menuWidth = item.Length;
            }

            topLine = "╔" + new string('═', menuWidth) + "╗";
            midLine = "╟" + new string('─', menuWidth) + "╢";
            bottomLine = "╚" + new string('═', menuWidth) + "╝";
        }

        private void UpdateMenu(int highlightItem)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(menuPosX, menuPosY);

            Console.WriteLine(topLine);

            for (var row = 0; row < MenuItems.Count; row++)
            {
                Console.CursorLeft = menuPosX;
                if (row == highlightItem)
                {
                    Console.Write("║");
                    InvertColors();
                    Console.Write(MenuItems[row].PadRight(menuWidth));
                    SetColors();
                    Console.WriteLine("║");
                }
                else
                {
                    Console.WriteLine("║" + MenuItems[row].PadRight(menuWidth) + "║");
                    if (row == HeaderLines - 1 || row == HeaderLines + InfoLines - 1)
                    {
                        Console.CursorLeft = menuPosX;
                        Console.WriteLine(midLine);
                    }
                }
            }
            Console.CursorLeft = menuPosX;
            Console.WriteLine(bottomLine);
            Console.CursorLeft = menuPosX;
            Console.WriteLine(HelpText);
        }
        #endregion Private Methods
    }
}