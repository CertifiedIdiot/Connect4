using Connect4.Enums;
using Connect4.Interfaces;
using Connect4.Models;
using Connect4.Network;
using Connect4.Structs;

namespace Connect4.Game
{
    public class Game
    {
        private Owner gameWonBy;
        private readonly INetwork network;
        public event EventHandler<string> BoardChangedEvent;
        public event EventHandler<string> GameWonEvent;
        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
        public Slot[,] Board { get; set; } = new Slot[7, 6];
        public IPlayer ActivePlayer { get; set; }
        public Game(INetwork network, bool goFirst)
        {
            this.network = network;
            PlayerOne = Connect4Factory.GetPlayer("player1", Owner.PlayerOne);
            PlayerTwo = Connect4Factory.GetPlayer("player2", Owner.PlayerTwo);
            ActivePlayer = PlayerOne;
            if (!goFirst) RecieveGameState();
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
                    if (network != null) SendGameState();
                    return true;
                }
            }
            return false;
        }
        private void SendGameState()
        {
            var json = JsonHandler.Serialize(new GameState() { PlayerOnesTurn = ActivePlayer == PlayerOne, Board = Board, GameWonBy = gameWonBy });
            network.Send(json);
            if (gameWonBy == Owner.None) RecieveGameState();
        }

        private void RecieveGameState()
        {
            Console.WriteLine("Waiting for opponent...");
            var json = network.Receive();
            var gameState = JsonHandler.Deserialize<GameState>(json);
            ActivePlayer = gameState.PlayerOnesTurn ? PlayerOne : PlayerTwo;
            Board = gameState.Board;
            gameWonBy = gameState.GameWonBy;
            BoardChangedEvent?.Invoke(this, "recived gameState");
            if (gameState.GameWonBy != Owner.None) GameWonEvent?.Invoke(this, gameState.GameWonBy == PlayerOne.PlayerNumber ? PlayerOne.Name : PlayerTwo.Name);
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
                        gameWonBy = ActivePlayer.PlayerNumber;
                        if (network != null) SendGameState();
                        GameWonEvent?.Invoke(this, $"{ActivePlayer.Name}");
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