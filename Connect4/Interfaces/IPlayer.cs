namespace Connect4.Interfaces;

using Connect4.Enums;

/// <summary>
/// Interface representing one of the two players in a Connect 4 game
/// </summary>
public interface IPlayer
{
    /// <summary>
    /// Gets or sets if the player is number one or two
    /// </summary>
    /// <value>
    /// The player number of the player.
    /// </value>
    Owner PlayerNumber { get; set; }
    /// <summary>
    /// Gets or sets the players name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    string Name { get; set; }
}
