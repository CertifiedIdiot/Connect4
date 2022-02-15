namespace Connect4
{
    using Connect4.Interfaces;
    using Connect4.Models;

    /// <summary>
    /// Factory class for managing class dependencies.
    /// </summary>
    public static class Connect4Factory
    {
        /// <summary>
        /// Gets an instance of the Connect4 game class with concrete implementations of its dependencies.
        /// </summary>
        /// <returns>A Connect4 Game object.</returns>
        public static Game.Game GetGame() => new(GetPlayer(), GetPlayer());
        private static IPlayer GetPlayer() => new Player();
    }
}
