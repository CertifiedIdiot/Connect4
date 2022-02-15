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
            PrintBoard();
        }

        private void Game_GameWonEvent(object? sender, string e)
        {
            gameWon = true;
            Console.Clear();
            Console.WriteLine(e);
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
                Console.Write($"(Move: {counter}){game.ActivePlayer.Name}, enter a column: ");
                _ = int.TryParse(Console.ReadLine(), out int num);
                bool validMove = game.MakeMove(num - 1);
                if (validMove) counter++;
            } while (counter < 43 && !gameWon);
        }
        private void Game_BoardChangedEvent(object? sender, string e) => PrintBoard();

        private void PrintBoard()
        {
            RenderGameElement.GameBoard(UIPositions.GameBoardXPos, UIPositions.GameBoardYPos, UIColours.GameboardColour);
            RenderGameElement.PlayerPositions(game.Board, UIColours.PlayerOneColour, UIColours.PlayerTwoColour);
            //for (int row = 0; row <= game.Board.GetUpperBound(1); row++)
            //{
            //    for (int column = 0; column <= game.Board.GetUpperBound(0); column++)
            //    {
            //        if (game.Board[column, row].State == Owner.None) Console.Write(".");
            //        else if (game.Board[column, row].State == Owner.PlayerOne) Console.Write("X");
            //        else Console.Write("O");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("1234567");
        }
    }
}
