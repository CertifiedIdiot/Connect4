namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Enums;
    using Connect4.Game;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class QuickTest:IDisposable
    {
        readonly Game game;
        public QuickTest()
        {
            game = Connect4Factory.GetGame();
            game.BoardChangedEvent += Game_BoardChangedEvent;
            PrintBoard();
        }

        public void Dispose() => game.BoardChangedEvent -= Game_BoardChangedEvent;

        public void Run()
        {
            var counter = 1;
            do
            {
                Console.Write($"(Move: {counter}){game.ActivePlayer.Name}, enter a column: ");
                _=int.TryParse(Console.ReadLine(), out int num);
                bool validMove = game.MakeMove(num - 1);
                if (validMove) counter++;
            } while (counter<43);
        }
        private void Game_BoardChangedEvent(object? sender, string e) => PrintBoard();

        private void PrintBoard()
        {
            for (int row = 0; row <= game.Board.GetUpperBound(1); row++)
            {
                for (int column = 0; column <= game.Board.GetUpperBound(0); column++)
                {
                    if (game.Board[column, row].State == Color.None) Console.Write(".");
                    else if (game.Board[column, row].State == Color.Red) Console.Write("X");
                    else Console.Write("O");
                }
                Console.WriteLine();
            }
            Console.WriteLine("1234567");
        }
    }
}
