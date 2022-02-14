using System.Drawing;
using Console = Colorful.Console;


namespace Connect4_ConsoleUI.UIHelpers
{
    public static class Print
    {
        #region PrintGraphics.AtPosition + overloads
        /// <summary>
        /// Prints a string at a chosen console position.
        /// </summary>
        /// <param name="graphicsObject">String to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        public static void AtPosition(string graphicsObject, int posX, int PosY)
        {

            for (var i = 0; i < graphicsObject.Length; i++)
            {
                Console.SetCursorPosition(posX, PosY++);
                Console.WriteLine(graphicsObject[i]);
            }
        }


        /// <summary>
        /// Prints a string array at a chosen console position.
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
        /// Prints a string array at a chosen console position. Can print in a specific Color-type colour.
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
        /// Prints a string at a chosen console position. Can print in a specific Color-type colour.
        /// </summary>
        /// <param name="graphicsObject">String to be printed.</param>
        /// <param name="posX">Horizontal Console position.</param>
        /// <param name="PosY">Vertical Console position.</param>
        /// <param name="colour">The color of the string, from the Color type.</param>
        public static void AtPosition(string graphicsObject, int posX, int PosY, Color colour)
        {

            for (var i = 0; i < graphicsObject.Length; i++)
            {
                Console.SetCursorPosition(posX, PosY++);
                Console.WriteLine(graphicsObject[i], colour);
            }
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

            for (var i = 0; i < graphicsObject.Length; i++)
            {
                Console.BackgroundColor = backgroundColour;
                Console.SetCursorPosition(posX, PosY++);
                Console.WriteLine(graphicsObject[i], color);
            }
        }

        /// <summary>
        /// Prints a string array at a chosen console position. Can print in a specific Color-type colour.
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
        #endregion
    }
}
