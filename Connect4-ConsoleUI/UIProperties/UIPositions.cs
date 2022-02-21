namespace Connect4_ConsoleUI.UIProperties
{
    public static class UIPositions
    {
        /// <summary>
        /// Gets or sets the gameboard X axis/horizontal position. Most other graphical elements are relative to the position of the gameboard,
        /// and will move accordingly when the gameboard is moved.
        /// </summary>
        /// <value>
        /// The gameboard X position.
        /// </value>
        internal static int GameBoardXPos { get; set; } = 33;
        /// <summary>
        /// Gets or sets the gameboard Y axis/vertical position. Most other graphical elements are relative to the position of the gameboard,
        /// and will move accordingly when the gameboard is moved.
        /// </summary>
        /// <value>
        /// The gameboard Y position.
        /// </value>
        internal static int GameBoardYPos { get; set; } = 10;
        internal static int BackgroundTableXpos { get; set; } = 12;
        internal static int BackgroundTableYpos { get; set; } = Console.WindowHeight - 12;
    }
}
