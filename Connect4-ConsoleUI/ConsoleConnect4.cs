namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Game;
    using Connect4.Interfaces;
    using Connect4_ConsoleUI.GameUI;
    using System;

    internal class ConsoleConnect4
    {
        readonly Game game;

        public ConsoleConnect4(INetwork network, bool goFirst)
        {
            game = Connect4Factory.GetGame(network, goFirst);
            game.BoardChangedEvent += Game_BoardChangedEvent;
            game.GameOverEvent += Game_GameWonEvent;
            RenderGame.StartRound();
        }

        ~ConsoleConnect4()
        {
            game.BoardChangedEvent -= Game_BoardChangedEvent;
            game.GameOverEvent -= Game_GameWonEvent;
        }

        private void Game_GameWonEvent(object? sender, GameOverEventArgs e)
        {
            if (e.Winner == "Draw.") RenderGame.WinSplashscreen("             Draw!");
            else RenderGame.WinSplashscreen($"     {e.Winner} won!");
            Menus.PlayAgainMenu.Rematch(this);
        }

        private void Game_BoardChangedEvent(object? sender, EventArgs e) => UpdateUI();

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
        private void DrawUI()
        {
            RenderGame.RenderBasicGameElements();
            RenderGameElement.DisplayColumnNumbers();
            UpdateUI();
        }
        private void UpdateUI() => RenderGame.RenderGameInfo($"{game.ActivePlayer.Name} - Pick a column number from below.", game.MoveCounter, game.ActivePlayer, game.Board);

        private static int GetChosenColumn()
        {
            var input = Console.ReadKey(true);
            return char.IsDigit(input.KeyChar) ? int.Parse(input.KeyChar.ToString()) : 0;
        }
    }
}
