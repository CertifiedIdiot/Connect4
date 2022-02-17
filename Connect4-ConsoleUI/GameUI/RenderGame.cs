using Connect4_ConsoleUI.UIProperties;
using Connect4.Interfaces;
using Console = Colorful.Console;

namespace Connect4_ConsoleUI.GameUI
{
    internal static class RenderGame
    {
        // TODO graphical elements as separate classes instead?

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
            Console.CursorVisible = false;
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

        internal static void RenderGameInfo(string messagebox, int counter, IPlayer player)
        {
            Console.CursorVisible = false;
            RenderGameElement.DisplayPlayerTurn(player);
            RenderGameElement.DisplayTurnCounter(counter);
            RenderGameElement.DisplayTopMessage(messagebox);
            RenderGameElement.DisplayColumnNumbers();
        }
    }
}
