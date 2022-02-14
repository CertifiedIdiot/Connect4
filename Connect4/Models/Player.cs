namespace Connect4.Models
{
    using Connect4.Enums;
    /// <summary>
    /// Cless representing one of the two players in a Connect 4 game
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Gets or sets the players name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = "";
        /// <summary>
        /// Gets or sets the color of the player.
        /// </summary>
        /// <value>
        /// The color of the player.
        /// </value>
        public Colors Color { get; set; }
    }
}
