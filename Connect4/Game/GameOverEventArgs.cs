namespace Connect4.Game
{
    using Connect4.Enums;

    public class GameOverEventArgs:EventArgs
    {
        public string Winner { get; }
        public GameOverEventArgs(string winner)
        {
            Winner = winner;
        }
    }
}