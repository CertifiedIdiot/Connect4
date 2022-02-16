using Connect4_ConsoleUI.UIProperties;
using Connect4.Interfaces;
using Console = Colorful.Console;

namespace Connect4_ConsoleUI.GameUI
{
    internal static class RenderGame
    {
        // TODO graphical elements as separate classes instead?

        // Temp solution to be able to see the possible "drop positions" of the player icons above the board. True = Displays the icon above the board.
        //internal static bool[] playerDropPos = { true, false, false, false, false, false, false };

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
            Console.Clear();
        }

        internal static void WinSplashscreen(string winner)
        {
            Console.CursorVisible = false;
            RenderGameElement.WinSplashscreenBackground();
            RenderGameElement.WinSplashscreenDisplayWinnerName(winner);
            Console.ReadKey();
            //new QuickTest().Run(null, true); // TODO: Comment out this to prevent restart after splashscreen, only used for testing
        }

        internal static void StartScreen()
        {
            SetConsoleSettings();
            RenderGameElement.SplashscreenStartScreen();
        }

        internal static void RenderLeftInfoBox(int counter, IPlayer player)
        {
            RenderGameElement.DisplayPlayerTurn(player);
            RenderGameElement.DisplayTurnCounter(counter);
        }
    }
}
