namespace Connect4.Game
{
    using Connect4.Enums;

    /// <summary>
    /// Game Over EventArgs
    /// </summary>
    public class GameOverEventArgs:EventArgs
    {
        /// <summary>
        /// The winner of the game.
        /// </summary>
        public string Winner { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverEventArgs"/> class.
        /// </summary>
        /// <param name="winner">The winner of the game.</param>
        public GameOverEventArgs(string winner) => Winner = winner;
    }
}