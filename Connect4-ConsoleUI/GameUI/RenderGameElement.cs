﻿using Connect4.Enums;
using Connect4.Structs;
using Connect4_ConsoleUI.Resources;
using Connect4_ConsoleUI.UIHelpers;
using Connect4_ConsoleUI.UIProperties;
using Figgle;
using System.Drawing;

namespace Connect4_ConsoleUI.GameUI
{
    public static class RenderGameElement
    {
        /// <summary>
        /// Prints the background table at the chosen position.
        /// </summary>
        /// <param name="posX">Horizontal position.</param>
        /// <param name="posY">Vertical position.</param>
        /// <param name="tableColour">Colour of the table.</param>
        public static void BackgroundTable(int posX, int posY, Color tableColour) => Print.AtPosition(ASCIIGraphics.tableArray, posX, posY, tableColour);

        /// <summary>
        /// Prints out the gameboard at the chosen position with the chosen colour.
        /// </summary>
        /// <param name="posX">Horizontal position.</param>
        /// <param name="posY">Vertical position.</param>
        /// <param name="gameboardColour">Colour of the gameboard</param>
        public static void GameBoard(int posX, int posY, Color gameboardColour) => Print.AtPosition(ASCIIGraphics.gameboardArray, posX, posY, gameboardColour);

        /// <summary>
        /// Prints out the possible "drop positions" at the chosen position. The drop positions are relative to the position of the gameboard.
        /// </summary>
        /// <param name="playerDropPositions"></param>
        /// <param name="playerColour"></param>
        public static void PlayerDropPositions(bool[] playerDropPositions, Color playerColour)
        {
            var xOffsetPosition = UIPositions.GameBoardXPos + 3;
            var yOffsetPosition = UIPositions.GameBoardYPos - 5;
            var playerIconPositionXIncrease = 0;
            for (int i = 0; i < playerDropPositions.Length; i++)
            {
                if (playerDropPositions[i])
                    Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + playerIconPositionXIncrease, yOffsetPosition, playerColour);
                else
                    Print.AtPosition("", 33 + playerIconPositionXIncrease, 5);
                playerIconPositionXIncrease += 7;
            }
        }

        /// <summary>
        /// Prints out the coloured player positions inside of the gameboard. The positions are relative to the gameboard.
        /// </summary>
        /// <param name="boardPositions"></param>
        public static void PlayerPositions(Slot[,] boardPositions)
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
                    if (boardPositions[ii, i].State == Owner.PlayerOne)
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, UIColours.PlayerOneColour);
                    if (boardPositions[ii, i].State == Owner.PlayerTwo)
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, UIColours.PlayerTwoColour);
                    if (boardPositions[ii, i].State == Owner.None)
                        Print.AtPosition("", xOffsetPosition + xIncrease, yOffsetPosition + yIncrease);
                    xIncrease += columns;
                }
                yIncrease += 4; // Move 4 spaces down in y
                xIncrease = 0; // Reset x position to 0
            }
        }

        internal static void WinnerSplashscreen(string winnerName)
        {
            Console.CursorVisible = false;
            //Print.GradientAtPosition(FiggleFonts.Colossal.Render(winnerName), 15, Color.Crimson, Color.Fuchsia);
            for (int i = 0; i < 5; i++)
            {
                Print.GradientAtPosition(ASCIIGraphics.splashscreenFireworks, 0, UIColours.PlayerOneColour, UIColours.PlayerTwoColour);
                System.Threading.Thread.Sleep(500);
                Print.GradientAtPosition(ASCIIGraphics.splashscreenFireworks, 0, UIColours.PlayerTwoColour, UIColours.PlayerOneColour);
                //Print.GradientAtPosition(FiggleFonts.Colossal.Render(winnerName), 15, Color.Crimson, Color.Fuchsia);
                System.Threading.Thread.Sleep(500);
            }
            Console.CursorVisible = true;
        }
    }
}
