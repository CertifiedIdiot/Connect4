using Connect4.Enums;
using Connect4.Interfaces;
using Connect4.Structs;
using Connect4_ConsoleUI.Resources;
using Connect4_ConsoleUI.UIHelpers;
using Connect4_ConsoleUI.UIProperties;
using Figgle;
using System.Drawing;

namespace Connect4_ConsoleUI.GameUI
{
    internal static class RenderGameElement
    {
        /// <summary>
        /// Prints the background table at the chosen position.
        /// </summary>
        /// <param name="posX">Horizontal position.</param>
        /// <param name="posY">Vertical position.</param>
        /// <param name="tableColour">Colour of the table.</param>
        internal static void BackgroundTable(int posX, int posY, Color tableColour) => Print.AtPosition(ASCIIGraphics.tableArray, posX, posY, tableColour);

        /// <summary>
        /// Prints out the gameboard at the chosen position with the chosen colour.
        /// </summary>
        /// <param name="posX">Horizontal position.</param>
        /// <param name="posY">Vertical position.</param>
        /// <param name="gameboardColour">Colour of the gameboard</param>
        internal static void GameBoard(int posX, int posY, Color gameboardColour) => Print.AtPosition(ASCIIGraphics.gameboardArray, posX, posY, gameboardColour);

        #region Player positions

        /// <summary>
        /// Prints out the coloured player positions inside of the gameboard. The positions are relative to the gameboard.
        /// </summary>
        /// <param name="boardPositions">The current state of the board slots</param>
        internal static void PlayerPositions(Slot[,] boardPositions)
        {
            const int rows = 6;
            const int columns = 7;
            var xOffsetPosition = UIPositions.GameBoardXPos + 3;
            var yOffsetPosition = UIPositions.GameBoardYPos + 1;
            var xIncrease = 0; // Spaces between each icon in X
            var yIncrease = 0; // Spaces between each icon in Y

            for (int i = 0; i < rows; i++)
            {
                for (int ii = 0; ii < columns; ii++)
                {
                    if (boardPositions[ii, i].State == Token.PlayerOne)
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, UIColours.PlayerOneColour);
                    if (boardPositions[ii, i].State == Token.PlayerTwo)
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, UIColours.PlayerTwoColour);
                    if (boardPositions[ii, i].State == Token.None)
                        Print.AtPosition("", xOffsetPosition + xIncrease, yOffsetPosition + yIncrease);
                    xIncrease += columns;
                }
                yIncrease += 4; // Move 4 spaces down in y
                xIncrease = 0; // Reset x position to 0
            }
        }
        #endregion

        #region Elements to the left        
        /// <summary>
        /// Displays P1/P2 respectively next to the gameboard, for the currently active player.
        /// </summary>
        /// <param name="player">The active player.</param>
        internal static void DisplayPlayerTurn(IPlayer player)
        {
            int yOffset = UIPositions.GameBoardYPos - 1;
            var playerOneAscii = FiggleFonts.Standard.Render("           P1    ");
            var playerTwoAscii = FiggleFonts.Standard.Render("           P2");
            if (player.PlayerNumber == Token.PlayerOne)
                Print.StringAtPosition(playerOneAscii, yOffset, UIColours.PlayerOneColour);
            if (player.PlayerNumber == Token.PlayerTwo)
                Print.StringAtPosition(playerTwoAscii, yOffset, UIColours.PlayerTwoColour);
        }

        /// <summary>
        /// Displays the turn counter to the left of the gameboard
        /// </summary>
        /// <param name="gameTurn">The current game turn.</param>
        internal static void DisplayTurnCounter(int gameTurn)
        {
            int yOffset = UIPositions.GameBoardYPos + 5;
            var stringGameTurn = gameTurn.ToString();
            var gameTurnAscii = FiggleFonts.Standard.Render($"           {stringGameTurn}     ");
            Print.StringAtPosition(gameTurnAscii, yOffset, UIColours.GameboardColour);
        }
        #endregion

        #region Top messagebox        
        /// <summary>
        /// Displays messages on top of the gameboard.
        /// </summary>
        /// <param name="text">The text.</param>
        internal static void DisplayTopMessage(string text)
        {
            int yOffset = UIPositions.GameBoardYPos - 8;
            Print.StringAtPositionCentered(text, yOffset, UIColours.TextColour);
        }

        /// <summary>
        /// Clears whatever is on the DisplayTopMessage row, for example leftover "Console input"-numbers.
        /// </summary>
        /// <param name="text">The text.</param>
        internal static void ClearNumber(string text)
        {
            string eraser = "";
            int xOffset = UIPositions.GameBoardXPos + 1;
            int yOffset = UIPositions.GameBoardYPos - 8;
            for (int i = 0; i < text.Length; i++)
            {
                eraser += " ";
            }
            Print.StringAtPosition(eraser, xOffset + 38, yOffset);
        }
        #endregion

        #region Column numbers        
        /// <summary>
        /// Displays the column numbers above the gameboard.
        /// </summary>
        internal static void DisplayColumnNumbers()
        {
            int xOffset = UIPositions.GameBoardXPos + 3;
            int yOffset = UIPositions.GameBoardYPos - 6;
            Console.SetCursorPosition(xOffset, yOffset);
            Print.StringAtPosition(ASCIIGraphics.columnNumbers, yOffset, UIColours.GameboardColour);
        }

        #endregion

        #region SplashScreens        
        /// <summary>
        /// Prints the name of the winner in ASCII-font in the top-middle position of the console.
        /// </summary>
        /// <param name="winnerName">Name of the winner.</param>
        internal static void WinSplashscreenDisplayWinnerName(string winnerName)
        {
            Print.StringAtPosition("                     ", 12); // Need to set console in correct position before FiggleFonts prints its first row.
            Print.GradientAtPosition(FiggleFonts.Standard.Render("                     " + winnerName), 12, UIColours.GameboardColour);
        }
        /// <summary>
        /// Prints a semi animated ASCII art.
        /// </summary>
        internal static void WinSplashscreenBackground()
        {
            for (int i = 0; i < 5; i++)
            {
                Print.GradientAtPosition(ASCIIGraphics.splashscreenFireworksAlt, 0, UIColours.PlayerOneColour);
                System.Threading.Thread.Sleep(50);
                Print.GradientAtPosition(ASCIIGraphics.splashscreenFireworksAlt, 0, UIColours.PlayerTwoColour);
                System.Threading.Thread.Sleep(50);
            }
        }
        /// <summary>
        /// Splashscreens the start screen.
        /// </summary>
        internal static void SplashscreenStartScreen()
        {
            while (!Console.KeyAvailable)
            {
                Console.CursorVisible = false;
                Print.GradientAtPosition(ASCIIGraphics.connect4string, 10, UIColours.PlayerOneColour);
                System.Threading.Thread.Sleep(100);
                Print.GradientAtPosition(ASCIIGraphics.connect4string, 10, UIColours.PlayerTwoColour);
            }
            Console.ReadKey(true);
        }
        internal static void SplashscreenPreMatch()
        {
            Console.CursorVisible = false;
            for (int i = 0; i < 3; i++)
            {
                Print.GradientAtPosition(ASCIIGraphics.connect4string, 10, UIColours.PlayerOneColour);
                System.Threading.Thread.Sleep(100);
                Print.GradientAtPosition(ASCIIGraphics.connect4string, 10, UIColours.PlayerTwoColour);
            }
        }
        #endregion        
        /// <summary>
        /// Prints the game ASCII header, to be used in all Menus.
        /// </summary>
        internal static void MenuHeader()
        {
            var posX = 42;
            var posY = 10;
            var xIncrease = 0;
            Print.StringAtPosition(ASCIIGraphics.connect4stringHeader, 1);
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    Print.AtPosition(ASCIIGraphics.playerIconArray, posX + xIncrease, posY, UIColours.PlayerTwoColour);
                }
                else
                    Print.AtPosition(ASCIIGraphics.playerIconArray, posX + xIncrease, posY, UIColours.PlayerOneColour);
                xIncrease += 10;
            }
        }

        internal static void ExitMessage()
        {
            Print.StringAtPositionCentered("Thank you for playing. Press any key to exit.", 15);
            Console.ReadKey(true);
        }
    }
}
