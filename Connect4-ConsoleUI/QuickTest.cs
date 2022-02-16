namespace Connect4_ConsoleUI
{
    using Connect4;
    using Connect4.Game;
    using Connect4_ConsoleUI.GameUI;
    using Connect4_ConsoleUI.UIProperties;
    using System;
    using System.Data.Common;

    internal class QuickTest
    {
        readonly Game game;
        bool gameWon = false;
        public QuickTest()
        {
            game = Connect4Factory.GetGame();
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
            var topMessage = " pick a column below:";
            do
            {
                // TODO: Clean out Run() and move RenderGameElement stuff into a better place
                Console.CursorVisible = false;
                RenderGame.RenderLeftInfoBox(counter, game.ActivePlayer);
                RenderGameElement.DisplayColumnNumbers();
                RenderGameElement.DisplayTopMessage(game.ActivePlayer.Name + topMessage);
                _ = int.TryParse(Console.ReadLine(), out int num);
                RenderGameElement.ClearNumber(game.ActivePlayer.Name + topMessage);
                bool validMove = game.MakeMove(num - 1);
                if (validMove) counter++;
            } while (counter < 43 && !gameWon);
        }
        private void Game_BoardChangedEvent(object? sender, string e) => UpdatePlayerPositions();

        private void UpdatePlayerPositions() => RenderGameElement.PlayerPositions(game.Board);
    }
}
