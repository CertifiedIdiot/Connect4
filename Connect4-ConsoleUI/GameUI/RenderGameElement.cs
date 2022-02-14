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
        public static void BackgroundTable(int posX, int posY, Color tableColour)
        {
            Print.AtPosition(ASCIIGraphics.tableArray, posX, posY, tableColour);
        }

        public static void GameBoard(int posX, int posY, Color gameboardColour)
        {
            Print.AtPosition(ASCIIGraphics.gameboardArray, posX, posY, gameboardColour);
        }
    }
}
