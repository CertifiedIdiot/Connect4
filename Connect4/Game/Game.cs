using Connect4.Enums;
using Connect4.Interfaces;
using Connect4.Structs;

namespace Connect4.Game
{
    public class Game
    {
        public event EventHandler<string> BoardChangedEvent;
        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
        public Slot[,] Board { get; set; } = new Slot[7, 6];
        public IPlayer ActivePlayer { get; set; }
        public Game(IPlayer player1, IPlayer player2)
        {
            PlayerOne = player1;
            PlayerOne.Name = "Player One";
            PlayerOne.Color = Owner.PlayerOne;
            PlayerTwo = player2;
            PlayerTwo.Name = "Player Two";
            PlayerTwo.Color = Owner.PlayerTwo;
            ActivePlayer = PlayerOne;
        }

        public bool MakeMove(int column)
        {
            if (column >= 0 && column <= Board.GetUpperBound(0))
            {
                var row = CheckColumn(column);
                if (row >= 0)
                {
                    Board[column, row].State = ActivePlayer.Color;
                    BoardChangedEvent?.Invoke(this, "Token placed");
                    ActivePlayer = ActivePlayer == PlayerOne ? PlayerTwo : PlayerOne;
                    return true;
                }
            }
            return false;
        }

        private int CheckColumn(int column)
        {
            for (int i = Board.GetUpperBound(1); i >= 0; i--)
            {
                if (Board[column, i].State == Owner.None) return i;
            }
            return -1;
        }
    }
}