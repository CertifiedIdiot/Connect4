using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4_ConsoleUI.UIProperties
{
    public static class UIColours
    {
        // TODO: Feel free to try out other colours to create a nice looking colour scheme
        // TODO: Colourscheme to make it easier to switch all colours at once?
        internal static Color PlayerOneColour { get; set; } = Color.DodgerBlue;     // Original colour - Color.DodgerBlue
        internal static Color PlayerTwoColour { get; set; } = Color.OrangeRed;     // Original colour - Color.OrangeRed
        internal static Color GameboardColour { get; set; } = Color.CadetBlue;     // Original colour - Color.CadetBlue
        internal static Color TableColour { get; set; } = Color.SaddleBrown;      // Original colour - Color.SaddleBrown
        internal static Color BackgroundColour { get; set; } = Color.Bisque;     // Original colour - Color.Bisque
        internal static Color TextColour { get; set; } = Color.DarkCyan;

    }
}
