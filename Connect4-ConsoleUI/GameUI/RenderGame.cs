using Connect4_ConsoleUI.UIProperties;
using Console = Colorful.Console;

namespace Connect4_ConsoleUI.GameUI
{
    internal static class RenderGame
    {
        // TODO graphical elements as separate classes instead?

        // Temp solution to be able to see the possible "drop positions" of the player icons above the board. True = Displays the icon above the board.
        internal static bool[] playerDropPos = { true, false, false, false, false, false, false };

        // The gameboard display is controlled by this array, which is supposed to be the updated/actual gameboard. Can be manually changed for now.
        internal static char[,] GetCharBoardArrayTest() => new char[,]
        {
                { '*','*','*','*','*','*','*'},
                { '*','o','*','*','*','*','*'},
                { '*','x','*','*','*','*','*'},
                { '*','x','o','*','*','*','*'},
                { 'o','x','x','o','*','*','*'},
                { 'x','o','o','x','*','*','*'},
        };

        internal static void Start()
        {
            SetConsoleSettings();
            RenderGameElement.BackgroundTable(UIPositions.BackgroundTableXpos, UIPositions.BackgroundTableYpos, UIColours.TableColour);
            RenderGameElement.GameBoard(UIPositions.GameBoardXPos, UIPositions.GameBoardYPos, UIColours.GameboardColour);
            RenderGameElement.PlayerDropPositions(playerDropPos, UIColours.PlayerOneColour);

            UIUpdatePlayerPositions();

            //Move console exit messages further down, for testing purposes.
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }

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
        internal static void UIUpdatePlayerPositions() => RenderGameElement.PlayerPositions(GetCharBoardArrayTest(), UIColours.PlayerOneColour, UIColours.PlayerTwoColour);
    }
}
