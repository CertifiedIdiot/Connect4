﻿namespace Connect4_ConsoleUI
{
using Connect4_ConsoleUI.GameUI;
    using Connect4_ConsoleUI.Menus;
    using System;
    using Connect4.Network;
    using System.Text.RegularExpressions;

    internal static class NetworkSetup
    {
        public static void Run()
        {
            RenderGame.MenuHeader();
            var menuItems = new List<string>() { "Network setup", "Start as server.", "Start as client.", "Use a relay server.", "Return to Main Menu." };
            switch (new CreateMenu(menuItems, true).UseMenu())
            {
                case "Start as server.": MenuHelpers.StartNetwork(true); break;
                case "Start as client.": MenuHelpers.StartNetwork(false); break;
                case "Use a relay server.": RelayMenu.Run(); break;
                case "Return to Main Menu.": MainMenu.Run(); break;
            }
        }
    }
}
