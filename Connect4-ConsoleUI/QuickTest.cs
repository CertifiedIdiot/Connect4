namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Game;
    using Connect4.Interfaces;
    using Connect4_ConsoleUI.GameUI;
    using System;

    internal class QuickTest
    {
        readonly Game game;

        public QuickTest(INetwork network, bool goFirst)
        {
            game = Connect4Factory.GetGame(network, goFirst);
            game.BoardChangedEvent += Game_BoardChangedEvent;
            game.GameOverEvent += Game_GameWonEvent;
            RenderGame.StartRound();
        }

        ~QuickTest()
        {
            game.BoardChangedEvent -= Game_BoardChangedEvent;
            game.GameOverEvent -= Game_GameWonEvent;
        }
        private void Game_GameWonEvent(object? sender, string e)
        {
            if (e == "Draw.") RenderGame.WinSplashscreen($"     Draw!");
            else RenderGame.WinSplashscreen($"     {e} won!");
            Menus.PlayAgainMenu.Rematch(this);
        }

        private void Game_BoardChangedEvent(object? sender, string e) => UpdateUI();

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
