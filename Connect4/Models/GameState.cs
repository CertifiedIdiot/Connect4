namespace Connect4.Models
{
    using Connect4.Interfaces;
    using Connect4.Structs;

    public class GameState
    {
        public Slot[,] Board { get; set; } = new Slot[7, 6];
        public bool PlayerOnesTurn { get; set; }
    }
}
