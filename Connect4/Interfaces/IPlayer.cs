namespace Connect4.Interfaces;

using Connect4.Enums;

public interface IPlayer
{
    Color Color { get; set; }
    string Name { get; set; }
}
