using Connect4_ConsoleUI.Resources;
using Connect4_ConsoleUI.UIHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4_ConsoleUI.GameUI
{
    public class RenderGameElement
    {
        // TODO: Add XML comments

        public static void BackgroundTable(int posX, int posY, Color tableColour)
        {
            Print.AtPosition(ASCIIGraphics.tableArray, posX, posY, tableColour);
        }

        public static void GameBoard(int posX, int posY, Color gameboardColour)
        {
            Print.AtPosition(ASCIIGraphics.gameboardArray, posX, posY, gameboardColour);
        }

        public static void PlayerDropPositions(bool[] playerDropPositions, Color playerColour)
        {
            var playerIconPositionXIncrease = 0;
            for (int i = 0; i < playerDropPositions.Length; i++)
            {

                if (playerDropPositions[i])
                    Print.AtPosition(ASCIIGraphics.playerIconArray, 33 + playerIconPositionXIncrease, 5, playerColour);
                else
                    Print.AtPosition("", 33 + playerIconPositionXIncrease, 5);
                playerIconPositionXIncrease += 7;
            }
        }

        public static void PlayerPositions(char[,] boardPositions, int gameboardXPos, int gameboardYPos, Color playerOneColour, Color playerTwoColour)
        {
            const int rows = 6;
            const int columns = 7;
            var xOffsetPosition = gameboardXPos + 3; // Print relative to the position of the gameboard
            var yOffsetPosition = gameboardYPos + 1;
            var xIncrease = 0; // Spaces between each icon in X
            var yIncrease = 0; // Spaces between each icon in Y

            for (int i = 0; i < rows; i++)
            {
                for (int ii = 0; ii < columns; ii++)
                {
                    if (boardPositions[i, ii] == 'x' || boardPositions[i, ii] == 'X')
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerTwoColour);
                    if (boardPositions[i, ii] == 'o' || boardPositions[i, ii] == 'O')
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerOneColour);
                    if (boardPositions[i, ii] == '*')
                        Print.AtPosition("", 33 + xIncrease, 11 + yIncrease, playerOneColour);
                    xIncrease += columns;
                }
                yIncrease += 4; // Move 4 spaces down in y
                xIncrease = 0; // Reset x position to 0
            }
        }
    }
}
