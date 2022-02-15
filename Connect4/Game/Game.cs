using Connect4.Enums;
using Connect4.Interfaces;
using Connect4.Structs;

namespace Connect4.Game
{
    public class Game
    {
        public event EventHandler<string> BoardChangedEvent;
        public event EventHandler<string> GameWonEvent;
        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
        public Slot[,] Board { get; set; } = new Slot[7, 6];
        public IPlayer ActivePlayer { get; set; }
        public Game(IPlayer player1, IPlayer player2)
        {
            PlayerOne = player1;
            PlayerOne.Name = "Player One";
            PlayerOne.PlayerNumber = Owner.PlayerOne;
            PlayerTwo = player2;
            PlayerTwo.Name = "Player Two";
            PlayerTwo.PlayerNumber = Owner.PlayerTwo;
            ActivePlayer = PlayerOne;
        }

        public bool MakeMove(int column)
        {
            if (column >= 0 && column <= Board.GetUpperBound(0))
            {
                var row = CheckColumn(column);
                if (row >= 0)
                {
                    Board[column, row].State = ActivePlayer.PlayerNumber;
                    BoardChangedEvent?.Invoke(this, $"{ActivePlayer.Name} placed a token.");
                    var gameWon = CheckForFour();
                    ActivePlayer = ActivePlayer == PlayerOne ? PlayerTwo : PlayerOne;
                    return true;
                }
            }
            return false;
        }

        private bool CheckForFour() => CheckDirection(1, 0)
            || CheckDirection(1, 1)
            || CheckDirection(0, 1)
            || CheckDirection(-1, 1);

        private bool CheckDirection(int deltaColumn, int deltaRow)
        {
            for (int row = 0; row <= Board.GetUpperBound(1); row++)
            {
                for (int column = 0; column <= Board.GetUpperBound(0); column++)
                {
                    if (FindConnecting(column, row, deltaColumn, deltaRow) == 4)
                    {
                        GameWonEvent?.Invoke(this, $"{ActivePlayer.Name} won.");
                        return true;
                    }
                }
            }
            return false;
        }

        private int FindConnecting(int column, int row, int deltaColumn, int deltaRow)
        {
            int columnToCheck = column, rowToCheck = row, counter = 0;
            while (ValidCell(columnToCheck, rowToCheck) && SlotOwnedByActivePlayer(columnToCheck, rowToCheck) && counter < 4)
            {
                counter++;
                columnToCheck += deltaColumn;
                rowToCheck += deltaRow;
            }
            return counter;
        }
        private bool SlotOwnedByActivePlayer(int column, int row) => Board[column, row].State == ActivePlayer.PlayerNumber;
        private bool ValidCell(int x, int y) => x >= 0 && x <= Board.GetUpperBound(0) && y >= 0 && y <= Board.GetUpperBound(1);

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