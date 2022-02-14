using Connect4.Interfaces;
using Connect4.Structs;

namespace Connect4.Game
{
    public class Game
    {
        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
        public Slot[,] Board { get; set; } = new Slot[7, 6];
        public Game(IPlayer player1, IPlayer player2)
        {
            PlayerOne = player1;
            PlayerTwo = player2;
        }
    }
}