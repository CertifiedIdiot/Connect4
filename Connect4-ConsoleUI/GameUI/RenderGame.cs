﻿using Connect4_ConsoleUI.UIProperties;
using Connect4.Interfaces;
using Console = Colorful.Console;
using Connect4.Structs;

namespace Connect4_ConsoleUI.GameUI
{
    public static class RenderGame
    {
        /// <summary>
        /// Renders the basic game elements. The gameboard, the table, and adjusts the console.
        /// </summary>
        internal static void RenderBasicGameElements()
        {
            SetConsoleSettings();
            RenderGameElement.BackgroundTable(UIPositions.BackgroundTableXpos, UIPositions.BackgroundTableYpos, UIColours.TableColour);
            RenderGameElement.GameBoard(UIPositions.GameBoardXPos, UIPositions.GameBoardYPos, UIColours.GameboardColour);
        }

        /// <summary>
        /// Sets the console settings. Adjusts the console height to fit the UI, and changes the console background colour.
        /// </summary>
        internal static void SetConsoleSettings()
        {
            Console.WindowHeight = 40;
            Console.BackgroundColor = UIColours.BackgroundColour;
            Console.ForegroundColor = UIColours.TextColour;
            Console.CursorVisible = false;
            Console.Clear();
        }
        /// <summary>
        /// Displays the win-condition splashscreen, with a background and the winners name.
        /// </summary>
        /// <param name="winner">The winner.</param>
        internal static void WinSplashscreen(string winner)
        {
            Console.Clear();
            Console.CursorVisible = false;
            RenderGameElement.WinSplashscreenBackground();
            RenderGameElement.WinSplashscreenDisplayWinnerName(winner);
            Console.ReadKey(true);
        }

        /// <summary>
        /// Displays the startscreen of the game and sets the console settings.
        /// </summary>
        internal static void StartScreen()
        {
            SetConsoleSettings();
            RenderGameElement.SplashscreenStartScreen();
        }
        /// <summary>
        /// Displays the shorter version startscreen before each round.
        /// </summary>
        internal static void StartRound()
        {
            SetConsoleSettings();
            RenderGameElement.SplashscreenPreMatch();
        }

        /// <summary>
        /// Renders the game information.
        /// </summary>
        /// <param name="messagebox">The message on top of the board.</param>
        /// <param name="counter">The turn counter.</param>
        /// <param name="player">The active player.</param>
        /// <param name="gameboard">The positions on the gameboard.</param>
        internal static void RenderGameInfo(string messagebox, int counter, IPlayer player, Slot[,] gameboard)
        {
            Console.CursorVisible = false;
            RenderGameElement.DisplayPlayerTurn(player);
            RenderGameElement.DisplayTurnCounter(counter);
            RenderGameElement.DisplayTopMessage(messagebox);
            RenderGameElement.DisplayColumnNumbers();
            RenderGameElement.PlayerPositions(gameboard);
        }

        /// <summary>
        /// Clears the screen and displays the top menu header ASCII.
        /// </summary>
        internal static void MenuHeader()
        {
            Console.Clear();
            RenderGameElement.MenuHeader();
        }
        /// <summary>
        /// Clears the screen, displays header, displays exit message.
        /// </summary>
        public static void ExitScreen()
        {
            MenuHeader();
            RenderGameElement.ExitMessage();
        }
    }
}
