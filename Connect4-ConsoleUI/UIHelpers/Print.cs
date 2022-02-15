using System.Drawing;
using Console = Colorful.Console;

namespace Connect4_ConsoleUI.UIHelpers
{
    public static class Print
    {
        /// <summary>
        /// Prints a string at the chosen console row position, in colour.
        /// </summary>
        /// <param name="graphicsObject">The string to be printed.</param>
        /// <param name="posY">The y position.</param>
        /// <param name="colour">The colour.</param>
        public static void StringAtPosition(string graphicsObject, int posY, Color colour)
        {
            Console.CursorTop = posY;
            Console.WriteLine(graphicsObject, colour);
        }
        /// <summary>
        /// Prints a string at the chosen console column and row position, in colour.
        /// </summary>
        /// <param name="graphicsObject">The string to be printed.</param>
        /// <param name="posX">The x position.</param>
        /// <param name="posY">The y position.</param>
        /// <param name="colour">The colour.</param>
        public static void StringAtPosition(string graphicsObject, int posX, int posY, Color colour)
        {
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(graphicsObject, colour);
        }

        public static void StringAtPosition(string graphicsObject, int posY)
        {
            Console.CursorTop = posY;
            Console.WriteLine(graphicsObject);
        }

        public static void StringAtPosition(string graphicsObject, int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(graphicsObject);
        }

        /// <summary>
        /// Prints a colour gradiented string at chosen y position.
        /// </summary>
        /// <param name="graphicsObject">The graphics object.</param>
        /// <param name="posY">The position y.</param>
        /// <param name="colorA">The color a.</param>
        /// <param name="colorB">The color b.</param>
        public static void GradientAtPosition(string graphicsObject, int posY, Color colorA, Color colorB)
        {
            var charArrayed = graphicsObject.ToCharArray();
            Console.CursorTop = posY;
            Console.WriteWithGradient(charArrayed, colorA, colorB, 5);
        }
        #region Print at position + overloads
        /// <summary>
        /// Prints a string array at a chosen console position. Prints each row in the array on a separate console-row.
        /// </summary>
        /// <param name="graphicsObject">String to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        public static void AtPosition(string[] graphicsObject, int posX, int PosY)
        {
            for (var i = 0; i < graphicsObject.Length; i++)
            {
                Console.SetCursorPosition(posX, PosY++);
                Console.WriteLine(graphicsObject[i]);
            }
        }

        /// <summary>
        /// Prints a string array at a chosen console position.  Prints each row in the array on a separate console-row. Can print in a specific Color-type colour.
        /// </summary>
        /// <param name="graphicsObject">String to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        /// <param name="colour">The color of the string, from the Color type.</param>
        public static void AtPosition(string[] graphicsObject, int posX, int PosY, Color colour)
        {
            for (var i = 0; i < graphicsObject.Length; i++)
            {
                Console.SetCursorPosition(posX, PosY++);
                Console.WriteLine(graphicsObject[i], colour);
            }
        }

        /// <summary>
        /// Prints a string array at a chosen console position.  Prints each row in the array on a separate console-row. Can print in a specific Color-type colour for both foreground and background.
        /// </summary>
        /// <param name="graphicsObject">String array to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        /// <param name="color">Text colour.</param>
        /// <param name="backgroundColour">Background colour of the text.</param>
        public static void AtPosition(string[] graphicsObject, int posX, int PosY, Color color, Color backgroundColour)
        {
            for (var i = 0; i < graphicsObject.Length; i++)
            {
                Console.BackgroundColor = backgroundColour;
                Console.SetCursorPosition(posX, PosY++);
                Console.WriteLine(graphicsObject[i], color);
            }
        }

        /// <summary>
        /// Prints a string at a chosen console position.
        /// </summary>
        /// <param name="graphicsObject">String to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        public static void AtPosition(string graphicsObject, int posX, int PosY)
        {
            Console.SetCursorPosition(posX, PosY);
            Console.WriteLine(graphicsObject);
        }

        /// <summary>
        /// Prints a string at a chosen console position. Can print in a specific Color-type colour.
        /// </summary>
        /// <param name="graphicsObject">String to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        /// <param name="colour">The color of the string, from the Color type.</param>
        public static void AtPosition(string graphicsObject, int posX, int PosY, Color colour)
        {
            Console.SetCursorPosition(posX, PosY);
            Console.WriteLine(graphicsObject, colour);
        }

        /// <summary>
        /// Prints a string  at a chosen console position. Can print in a specific Color-type colour.
        /// </summary>
        /// <param name="graphicsObject">String to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        /// <param name="color">Text colour.</param>
        /// <param name="backgroundColour">Background colour of the text.</param>
        public static void AtPosition(string graphicsObject, int posX, int PosY, Color color, Color backgroundColour)
        {
            Console.BackgroundColor = backgroundColour;
            Console.SetCursorPosition(posX, PosY);
            Console.WriteLine(graphicsObject, color);
        }
        #endregion
    }
}
