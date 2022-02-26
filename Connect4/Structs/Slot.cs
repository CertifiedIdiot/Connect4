namespace Connect4.Structs
{
    using Connect4.Enums;

    /// <summary>
    /// Representation of one of the slots or cells of the Conenct4 game board.
    /// </summary>
    public struct Slot
    {
        /// <summary>
        /// Gets or sets the state of the slot.
        /// </summary>
        /// <value>
        /// The state can be either None for unoccupied or PlayerOne or PlayerTwo depending on wich player has placed a token in the slot.
        /// </value>
        public Token State { get; set; }
    }
}
