using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4_ConsoleUI.UIProperties
{
    public static class UIPositions
    {
        internal static int GameBoardXPos { get; set; } = 33;
        internal static int GameBoardYPos { get; set; } = 10;
        internal static int BackgroundTableXpos { get; set; } = 12;
        internal static int BackgroundTableYpos { get; set; } = Console.WindowHeight - 12;
    }
}
