namespace Connect4
{
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
        /// Gets an instance of the Connect4 game class with concrete implementations of its dependencies.
        /// </summary>
        /// <returns>A Connect4 Game object.</returns>
        public static Game.Game GetGame(INetwork network,bool goFirst) => new(network,goFirst);
        public static IPlayer GetPlayer(string name="",Owner number=Owner.None) => new Player() { Name=name,PlayerNumber=number};
        public static Server GetServer() => new Server();
        public static Client GetClient() => new Client();
    }
}
