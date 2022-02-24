namespace Connect4
{
    using Connect4.Crypto;
    using Connect4.Enums;
    using Connect4.Interfaces;
    using Connect4.Models;
    using Connect4.Network;

    /// <summary>
    /// Factory class for managing class dependencies.
    /// </summary>
    public static class Connect4Factory
    {
        /// <summary>
        /// Gets an instance of the Connect4 <see cref="Game"/> class with concrete implementations of its dependencies.
        /// </summary>
        /// <returns>A Connect4 <see cref="Game"/> object.</returns>
        public static Game.Game GetGame(INetwork network, bool isPlayerOne, bool singlePlayer = false) => new(network, GetCrypto(),isPlayerOne, singlePlayer);
        /// <summary>
        /// Gets a concrete class that implements the <see cref="IPlayer"/> interface.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="number">The <see cref="Token"/> with the players number.</param>
        /// <returns></returns>
        public static IPlayer GetPlayer(string name = "", Token number = Token.None) => new Player() { Name = name, PlayerNumber = number };
        /// <summary>
        /// Gets an instance of a concrete class that implements the <see cref="INetwork"/> interface.
        /// </summary>
        /// <returns></returns>
        public static INetwork GetServer() => new Server();
        /// <summary>
        /// Gets an instance of a concrete class that implements the <see cref="INetwork"/> interface.
        /// </summary>
        /// <returns></returns>
        public static INetwork GetClient() => new Client();
        public static ICrypto GetCrypto() => new AesCrypto();
    }
}
