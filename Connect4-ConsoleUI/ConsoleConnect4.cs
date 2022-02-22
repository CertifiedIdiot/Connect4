namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Game;
    using Connect4.Interfaces;
    using Connect4_ConsoleUI.GameUI;
    using System;

    internal class ConsoleConnect4
    {
        /// <summary>
        /// The instance of the <see cref="Game"/> class used for this session.
        /// </summary>
        readonly Game game;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleConnect4"/> class.
        /// </summary>
        /// <param name="network">Instance of a concrete class that implements <see cref="INetwork"/> or <see langword="null"/> for a hot seat game.</param>
        /// <param name="isPlayerOne"><see langword="true"/> if this instance should belong to Player One, <see langword="false"/> if not.</param>
        public ConsoleConnect4(INetwork network, bool isPlayerOne, bool singlePlayer = false)
        {
            game = Connect4Factory.GetGame(network, isPlayerOne, singlePlayer);
            game.BoardChangedEvent += Game_BoardChangedEvent;
            game.GameOverEvent += Game_GameWonEvent;
            RenderGame.StartRound();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ConsoleConnect4"/> class.
        /// </summary>
        ~ConsoleConnect4()
        {
            game.BoardChangedEvent -= Game_BoardChangedEvent;
            game.GameOverEvent -= Game_GameWonEvent;
        }

        /// <summary>
        /// Event handler method for the <see cref="Game.GameOverEvent"/>.
        /// </summary>
        /// <param name="sender">The <see cref="Game"/> that sent the event.</param>
        /// <param name="e">The <see cref="GameOverEventArgs"/> instance containing the event data.</param>
        private void Game_GameWonEvent(object? sender, GameOverEventArgs e)
        {
            if (e.Winner == "Draw.") RenderGame.WinSplashscreen("             Draw!");
            else RenderGame.WinSplashscreen($"     {e.Winner} won!");
            Menus.PlayAgainMenu.Rematch(this);
        }

        /// <summary>
        /// Event handler method for the <see cref="Game.BoardChangedEvent"/>.
        /// </summary>
        /// <param name="sender">The <see cref="Game"/> that sent the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Game_BoardChangedEvent(object? sender, EventArgs e) => UpdateUI();

        /// <summary>
        /// Runs this instance, making sure the UI is drawn and the match starts. Will loop through getting input for moves and updating UI until the match is over.
        /// </summary>
        public void Run()
        {
            game.SetupNewGame();
            DrawUI();
            game.Start();

            do
            {
                UpdateUI();
                game.MakeMove(GetChosenColumn() - 1);
            } while (game.MoveCounter < 43);
        }

        /// <summary>
        /// Draws the UI.
        /// </summary>
        private void DrawUI()
        {
            RenderGame.RenderBasicGameElements();
            RenderGameElement.DisplayColumnNumbers();
            UpdateUI();
        }

        /// <summary>
        /// Updates elements of the UI to reflect current information about board state, active player and move number.
        /// </summary>
        private void UpdateUI() => RenderGame.RenderGameInfo($"        {game.ActivePlayer.Name} - Pick a column number from below.         ", game.MoveCounter, game.ActivePlayer, game.Board);

        /// <summary>
        /// Checks what key the player pressed.
        /// </summary>
        /// <returns>The number of the column the player wants to place a token in or 0 if the key pressed does not correspond to a column number.</returns>
        private static int GetChosenColumn()
        {
            var input = Console.ReadKey(true);
            return char.IsDigit(input.KeyChar) ? int.Parse(input.KeyChar.ToString()) : 0;
        }
    }
}
