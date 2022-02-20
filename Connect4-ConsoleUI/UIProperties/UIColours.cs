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

        /// <summary>
        /// <para> List containing different colour schemes for the game. Each colour scheme should have 6 colours. </para>
        ///
        /// <para> To manually change scheme used, edit the first number in the property, eg: </para>
        ///
        /// <para> Color.FromName(ColourSchemes[0][0]) => Color.FromName(ColourSchemes[1][0]) => the PlayerOneColour has changed to the second colour scheme in the ColourSchemes list. </para>
        /// </summary>
        private static readonly List<List<string>> ColourSchemes = new List<List<string>>()
        {
        // Colour order - PlayerOneColour, PlayerTwoColour, GameboardColour, TableColour, BackgroundColour, Textcolour
            new List<string> { "DodgerBlue", "OrangeRed", "CadetBlue", "SaddleBrown", "Bisque", "DarkCyan"},
            new List<string> { "Black", "White", "DodgerBlue", "DarkCyan", "OrangeRed", "Bisque"},
            //new List<string> {"", "", "", "", "", "" },
        };

        /// <summary> Gets or sets the player one colour See also <seealso cref="ColourSchemes"/> for more info. </summary>
        internal static Color PlayerOneColour { get; set; } = Color.FromName(ColourSchemes[0][0]);   // Original colour - Color.DodgerBlue

        /// <summary> Gets or sets the player two colour colour. See also <seealso cref="ColourSchemes"/> for more info. </summary>
        internal static Color PlayerTwoColour { get; set; } = Color.FromName(ColourSchemes[0][1]);     // Original colour - Color.OrangeRed

        /// <summary> Gets or sets the gameboard colour. See also <seealso cref="ColourSchemes"/> for more info. </summary>
        internal static Color GameboardColour { get; set; } = Color.FromName(ColourSchemes[0][2]);      // Original colour - Color.CadetBlue

        /// <summary> Gets or sets the table element colour. See also <seealso cref="ColourSchemes"/> for more info. </summary>
        internal static Color TableColour { get; set; } = Color.FromName(ColourSchemes[0][3]);      // Original colour - Color.SaddleBrown

        /// <summary> Gets or sets the background colour. See also <seealso cref="ColourSchemes"/> for more info. </summary>
        internal static Color BackgroundColour { get; set; } = Color.FromName(ColourSchemes[0][4]);      // Original colour - Color.Bisque                

        /// <summary> Gets or sets the text colour. See also <seealso cref="ColourSchemes"/> for more info. </summary>
        internal static Color TextColour { get; set; } = Color.FromName(ColourSchemes[0][5]);           // Original colour - Color.DarkCyan

        internal static void ChangeColourScheme(int selectedScheme)
        {
            UIColours.PlayerOneColour = Color.FromName(ColourSchemes[selectedScheme][0]);
            UIColours.PlayerTwoColour = Color.FromName(ColourSchemes[selectedScheme][1]);
            UIColours.GameboardColour = Color.FromName(ColourSchemes[selectedScheme][2]);
            UIColours.TableColour = Color.FromName(ColourSchemes[selectedScheme][3]);
            UIColours.BackgroundColour = Color.FromName(ColourSchemes[selectedScheme][4]);
            UIColours.TextColour = Color.FromName(ColourSchemes[selectedScheme][5]);
        }
    }


}
