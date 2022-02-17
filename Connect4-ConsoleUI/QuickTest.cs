namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Game;
    using Connect4.Network;
    using Connect4_ConsoleUI.GameUI;
    using System;

    internal class QuickTest
    {
        readonly Game game;

        public QuickTest(INetwork network, bool goFirst)
        {
            game = Connect4Factory.GetGame(network, goFirst);
            game.BoardChangedEvent += Game_BoardChangedEvent;
            game.GameWonEvent += Game_GameWonEvent;
            RenderGame.StartScreen(); // TODO: Comment out to skip intro. Temporary place for the startscreen, to be used in future menus instead. Only used for testing.
        }

        ~QuickTest()
        {
            game.BoardChangedEvent -= Game_BoardChangedEvent;
            game.GameWonEvent -= Game_GameWonEvent;
        }
        private void Game_GameWonEvent(object? sender, string e)
        {
            Console.Clear();
            RenderGame.WinSplashscreen($"     {e} won!");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);  //Moves console "exit messages" further down, for testing purposes.
            Run();
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
        private void UpdateUI() => RenderGame.RenderGameInfo($"{game.ActivePlayer.Name} - Pick a column number from below.", counter, game.ActivePlayer, game.Board);

        private static int GetChosenColumn()
        {
            var input = Console.ReadKey(true);
            return char.IsDigit(input.KeyChar) ? int.Parse(input.KeyChar.ToString()) : 0;
        }

    }
}
