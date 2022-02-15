using Connect4_ConsoleUI.UIProperties;
using Console = Colorful.Console;

namespace Connect4_ConsoleUI.GameUI
{
    internal static class RenderGame
    {

        
        internal static void Start()
        {
            // TODO graphical elements as separate classes instead?
            SetConsoleSettings();
            RenderGameElement.BackgroundTable(UIPositions.BackgroundTableXpos, UIPositions.BackgroundTableYpos, UIColours.TableColour);
            RenderGameElement.GameBoard(UIPositions.GameBoardXPos, UIPositions.GameBoardYPos, UIColours.GameboardColour);
            UIUpdatePlayerPositions();


            //Move console exit messages further down, for testing purposes.
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }


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

        /// <summary>
        /// UI method to update the player positions on the gameboard. Temp solution.
        /// </summary>
        internal static void UIUpdatePlayerPositions()
        {
            RenderGameElement.PlayerPositions(GetCharBoardArrayTest(), UIPositions.GameBoardXPos, UIPositions.GameBoardYPos, UIColours.PlayerOneColour, UIColours.PlayerTwoColour);
        }
    }
}
