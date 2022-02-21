using Connect4.Enums;
using Connect4.Interfaces;
using Connect4.Models;
using Connect4.Structs;

namespace Connect4.Game
{
    public class Game
    {
        /// <summary>
        /// Indicates the winner of the game, defaults to <see cref="Token.None"/> before a game is won.
        /// </summary>
        private Token gameWonBy;

        /// <summary>
        /// Network implementation, null if hotseat game.
        /// </summary>
        private readonly INetwork network;
        /// <summary>
        /// Occurs when the board is changed and UI should update.
        /// </summary>
        public event EventHandler<EventArgs>? BoardChangedEvent;
        /// <summary>
        /// Occurs when the game is over, either by a player winning or a draw by filling all the slots of the board.
        /// </summary>
        public event EventHandler<GameOverEventArgs>? GameOverEvent;
        /// <summary>
        /// Gets or sets the counter for the move about to be made.
        /// </summary>
        /// <value>
        /// The current move number.
        /// </value>
        public int MoveCounter { get; set; } = 1;
        public Token InstanceId { get; }
        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
        public Slot[,] Board { get; set; } = new Slot[7, 6];
        public IPlayer ActivePlayer { get; set; }
        public Game(INetwork network, bool goFirst)
        {
            this.network = network;
            PlayerOne = Connect4Factory.GetPlayer("Player 1", Token.PlayerOne);
            PlayerTwo = Connect4Factory.GetPlayer("Player 2", Token.PlayerTwo);
            ActivePlayer = PlayerOne;
            InstanceId = goFirst ? Token.PlayerOne : Token.PlayerTwo;
        }

        public void SetupNewGame()
        {
            MoveCounter = 1;
            Board = new Slot[7, 6];
            if (gameWonBy != Token.None)
            {
                ActivePlayer = gameWonBy == Token.PlayerOne ? PlayerTwo : PlayerOne;
                gameWonBy = Token.None;
            }
            BoardChangedEvent?.Invoke(this, EventArgs.Empty);
        }
        public void Start()
        {
            if (network != null! && ActivePlayer.PlayerNumber != InstanceId) ReceiveGameState();
        }

        public bool MakeMove(int column)
        {
            if (ValidMove(column, out int row))
            {
                PlaceToken(column, row);
                _ = CheckForFour();
                UpdateGameState();
                return true;
            }

            return false;
        }

        private bool ValidMove(int column, out int row)
        {
            row = -1;
            if (column >= 0 && column <= Board.GetUpperBound(0))
            {
                row = CheckColumn(column);
                return row >= 0;
            }
            return false;
        }

        private void UpdateGameState()
        {
            MoveCounter++;
            ActivePlayer = ActivePlayer == PlayerOne ? PlayerTwo : PlayerOne;
            if (MoveCounter == 43 && gameWonBy == Token.None) GameOverEvent?.Invoke(this, new GameOverEventArgs("Draw."));
            if (network != null!) SendGameState();
        }

        private void PlaceToken(int column, int row)
        {
            Board[column, row].State = ActivePlayer.PlayerNumber;
            BoardChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void SendGameState()
        {
            var json = JsonHandler.Serialize(new GameState()
            {
                PlayerOnesTurn = ActivePlayer == PlayerOne,
                Board = Board,
                GameWonBy = gameWonBy,
                MoveCounter = MoveCounter
            });
            network.Send(json);
            if (gameWonBy == Token.None && MoveCounter != 43) ReceiveGameState();
        }

        private void ReceiveGameState()
        {
            var json = network.Receive();
            var gameState = JsonHandler.Deserialize<GameState>(json);
            ActivePlayer = gameState.PlayerOnesTurn ? PlayerOne : PlayerTwo;
            Board = gameState.Board;
            gameWonBy = gameState.GameWonBy;
            MoveCounter = gameState.MoveCounter;
            BoardChangedEvent?.Invoke(this, EventArgs.Empty);
            if (gameState.GameWonBy != Token.None) GameOverEvent?.Invoke(this, gameState.GameWonBy == PlayerOne.PlayerNumber ? new GameOverEventArgs(PlayerOne.Name) : new GameOverEventArgs(PlayerTwo.Name));
            else if (MoveCounter == 43) GameOverEvent?.Invoke(this, new GameOverEventArgs("Draw."));
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
                        GameOverEvent?.Invoke(this, new GameOverEventArgs(ActivePlayer.Name));
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
                if (Board[column, i].State == Token.None) return i;
            }
            return -1;
        }
    }
}