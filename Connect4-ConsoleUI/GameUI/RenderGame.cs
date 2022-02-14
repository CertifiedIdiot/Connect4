using Connect4_ConsoleUI.UIProperties;
using Console = Colorful.Console;

namespace Connect4_ConsoleUI.GameUI
{
    internal static class RenderGame
    {

        // Start => Display stuff


        internal static char[,] GetCharBoardArrayTest() => new char[,]
    {
                { '*','*','*','*','*','*','*'},
                { '*','o','*','*','*','*','*'},
                { '*','x','*','*','*','*','*'},
                { '*','x','o','*','*','*','*'},
                { 'o','x','x','o','*','*','*'},
                { 'x','o','o','x','*','*','*'},
     };

        /// <summary>
        /// Sets the console settings. Adjusts the console height to fit the UI, and changes the console background colour.
        /// </summary>
        internal static void SetConsoleSettings()
        {
            Console.WindowHeight += 10;
            Console.BackgroundColor = UIColours.BackgroundColour;
            Console.Clear();
        }
    }
}
