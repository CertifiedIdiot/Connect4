namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Game;
    using Connect4_ConsoleUI.GameUI;
    using Connect4_ConsoleUI.UIProperties;
    using System;

    internal class QuickTest
    {
        readonly Game game;
        bool gameWon = false;
        public QuickTest()
        {
            game = Connect4Factory.GetGame();
            game.BoardChangedEvent += Game_BoardChangedEvent;
            game.GameWonEvent += Game_GameWonEvent;
            RenderGame.RenderBasicGameElements();
            UpdatePlayerPositions();
        }

        private void Game_GameWonEvent(object? sender, string e)
        {
            gameWon = true;
            Console.Clear();
            //Console.WriteLine(e); // Add victory splashscreen + active player name in ascii font - JE will add it
            RenderGameElement.WinnerSplashscreen($"{game.ActivePlayer.Name} won!");
           
            Console.SetCursorPosition(0, Console.WindowHeight - 1);  //Moves console "exit messages" further down, for testing purposes.
        }

        ~QuickTest()
        {
            game.BoardChangedEvent -= Game_BoardChangedEvent;
            game.GameWonEvent -= Game_GameWonEvent;
        }

        public void Run()
        {
            var counter = 1;
            do
            {
                Console.Write($"(Move: {counter}){game.ActivePlayer.Name}, enter a column: "); // Move into a "make a move" messagebox at certain position. - JE will add it
                _ = int.TryParse(Console.ReadLine(), out int num);
                bool validMove = game.MakeMove(num - 1);
                if (validMove) counter++;
            } while (counter < 43 && !gameWon);
        }
        private void Game_BoardChangedEvent(object? sender, string e) => UpdatePlayerPositions();

        private void UpdatePlayerPositions() => RenderGameElement.PlayerPositions(game.Board);
    }
}
