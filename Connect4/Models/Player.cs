namespace Connect4.Models
{
    using Connect4.Enums;
    using Connect4.Interfaces;

    /// <summary>
    /// Class representing one of the two players in a Connect 4 game
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Gets or sets the players name.
        /// </summary>
        /// <value>
        /// The name of the player.
        /// </value>
        public string Name { get; set; } = "";
        /// <summary>
        /// Gets or sets if the player is number one or two
        /// </summary>
        /// <value>
        /// The player number of the player.
        /// </value>
        public Token PlayerNumber { get; set; }
    }
}
