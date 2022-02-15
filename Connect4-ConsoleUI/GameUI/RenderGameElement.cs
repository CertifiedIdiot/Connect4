using Connect4.Game;
using Connect4_ConsoleUI.Resources;
using Connect4_ConsoleUI.UIHelpers;
using Connect4_ConsoleUI.UIProperties;
using Connect4.Game;
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
        /// <param name="playerOneColour"></param>
        /// <param name="playerTwoColour"></param>
        public static void PlayerPositions(char[,] boardPositions, Color playerOneColour, Color playerTwoColour)
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
                    if (boardPositions[i, ii] == 'x' || boardPositions[i, ii] == 'X')
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerTwoColour);
                    if (boardPositions[i, ii] == 'o' || boardPositions[i, ii] == 'O')
                        Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerOneColour);
                    else 
                        Print.AtPosition("", xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerOneColour);
                    xIncrease += columns;
                }
                yIncrease += 4; // Move 4 spaces down in y
                xIncrease = 0; // Reset x position to 0
            }
        }

        //public static void TestPlayerPositions(char[,] boardPositions, Color playerOneColour, Color playerTwoColour)
        //{
        //    const int rows = 6;
        //    const int columns = 7;
        //    var xOffsetPosition = UIPositions.GameBoardXPos + 3;
        //    var yOffsetPosition = UIPositions.GameBoardYPos + 1;
        //    var xIncrease = 0; // Spaces between each icon in X
        //    var yIncrease = 0; // Spaces between each icon in Y

        //    for (int row = 0; row <= game.Board.GetUpperBound(1); row++)
        //    {
        //        for (int ii = 0; ii < columns; ii++)
        //        {
        //            if (boardPositions[i, ii] == 'x' || boardPositions[i, ii] == 'X')
        //                Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerTwoColour);
        //            if (boardPositions[i, ii] == 'o' || boardPositions[i, ii] == 'O')
        //                Print.AtPosition(ASCIIGraphics.playerIconArray, xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerOneColour);
        //            else
        //                Print.AtPosition("", xOffsetPosition + xIncrease, yOffsetPosition + yIncrease, playerOneColour);
        //            xIncrease += columns;
        //        }
        //        yIncrease += 4; // Move 4 spaces down in y
        //        xIncrease = 0; // Reset x position to 0
        //    }
        //}
    }
}
