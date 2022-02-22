namespace Connect4.Models
{
    using Connect4.Enums;
    using Connect4.Structs;

    /// <summary>
    /// Class used for sending the current state of the game via the network interface
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Current state of the game board.
        /// </summary>
        public Slot[,] Board { get; set; } = new Slot[7, 6];
        /// <summary>
        /// Is it Player Ones turn next?
        /// </summary>
        public bool PlayerOnesTurn { get; set; }
        /// <summary>
        /// If a game is won, the winners Token will be sent here,otherwise, the value will be Token.None.
        /// </summary>
        public Token GameWonBy { get; set; }
        /// <summary>
        /// Counter for number of moves made in current match.
        /// </summary>
        public int MoveCounter { get; set; }
    }
}
