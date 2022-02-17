namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Game;
    using Connect4.Network;
    using Connect4_ConsoleUI.GameUI;
    using Connect4_ConsoleUI.UIProperties;
    using System;
    using System.Data.Common;

    internal class QuickTest
    {
        readonly Game game;
        bool gameWon = false;
        public QuickTest(INetwork network, bool goFirst)
        {
            game = Connect4Factory.GetGame(network, goFirst);
            game.BoardChangedEvent += Game_BoardChangedEvent;
            game.GameWonEvent += Game_GameWonEvent;
            RenderGame.StartScreen(); // TODO: Comment out to skip intro. Temporary place for the startscreen, to be used in future menus instead. Only used for testing.
            RenderGame.RenderBasicGameElements();
            UpdatePlayerPositions();
        }

        private void Game_GameWonEvent(object? sender, string e)
        {
            gameWon = true;
            Console.Clear();
            RenderGame.WinSplashscreen($"{game.ActivePlayer.Name} won!");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);  //Moves console "exit messages" further down, for testing purposes.
        }

        ~QuickTest()
        {
            game.BoardChangedEvent -= Game_BoardChangedEvent;
            game.GameWonEvent -= Game_GameWonEvent;
        }

        public void Run()
        {
            var counter = 1;
            do
            {
                RenderGame.RenderGameInfo($"{game.ActivePlayer.Name} - Pick a column number from below.", counter, game.ActivePlayer);
                //_ = int.TryParse(Console.ReadLine(), out int num);
                bool validMove = game.MakeMove(GetChosenColumn() - 1);
                if (validMove) counter++;
            } while (counter < 43 && !gameWon);
        }
        private void Game_BoardChangedEvent(object? sender, string e) => UpdatePlayerPositions();

        private void UpdatePlayerPositions() => RenderGameElement.PlayerPositions(game.Board);

        private static int GetChosenColumn() => Console.ReadKey(true).Key switch
        {
            ConsoleKey.D1 => 1,
            ConsoleKey.D2 => 2,
            ConsoleKey.D3 => 3,
            ConsoleKey.D4 => 4,
            ConsoleKey.D5 => 5,
            ConsoleKey.D6 => 6,
            ConsoleKey.D7 => 7,
            _ => 0,
        };
    }
}
