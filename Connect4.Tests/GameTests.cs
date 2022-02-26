using Connect4.Enums;
using Connect4.Game;
using Connect4.Interfaces;
using Connect4.Models;
using Connect4.Structs;
using Moq;
using System;
using Xunit;

namespace Connect4.Tests
{
    public class GameTests
    {
        readonly Game.Game game;
        readonly private Slot[,] testBoard;
        readonly Mock<INetwork> mock;
        readonly INetwork moqNet;
        readonly Game.Game gameWithMockNet;

        public GameTests()
        {
            game = new Game.Game(null!, null!, true);
            var c = new Slot { State = Token.None };
            var X = new Slot { State = Token.PlayerOne };
            var O = new Slot { State = Token.PlayerTwo };
            testBoard = new Slot[,]
            {
                //left side, rows
                //0,1,2,3,4,5
                {c, c, c, c, c, c}, //0
                {c, c, c, c, c, c}, //1
                {O, O, O, X, X, X}, //2 columns
                {c, c, c, X, X, X}, //3 bottom
                {c, c, c, c, c, c}, //4
                {c, c, c, c, c, c}, //5
                {c, c, c, c, c, c}, //6
            };
            game.Board = testBoard;
            var mockINetwork = new Mock<INetwork>();
            mockINetwork.Setup(x => x.Receive()).Returns(JsonHandler.Serialize(new GameState()));
            this.mock = mockINetwork;
            moqNet = mock.Object;
            gameWithMockNet = new Game.Game(moqNet, null!, true) { Board = testBoard };
        }

        [Fact]
        public void MakeMove_ValidMove_ShouldChangeActivePlayerAfterwards()
        {
            var expected = game.PlayerTwo;
            game.MakeMove(1);
            var actual = game.ActivePlayer;
            Assert.Equal(expected, actual);
            expected = game.PlayerOne;
            game.MakeMove(1);
            actual = game.ActivePlayer;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeMove_AttemptInFullColumn_ShouldReturnFalse() => Assert.False(game.MakeMove(2));

        [Fact]
        public void MakeMove_ValidMove_ShouldReturnTrue() => Assert.True(game.MakeMove(1));

        [Theory]
        [InlineData(-1)]
        [InlineData(7)]
        public void MakeMove_NonExistingColum_ShouldReturnFalse(int column) => Assert.False(game.MakeMove(column));

        [Fact]
        public void MakeMove_ValidMove_ShouldRaiseBoardChangeEvent()
        {
            Assert.Raises<EventArgs>(
                handler => game.BoardChangedEvent += handler,
                handler => game.BoardChangedEvent -= handler,
                () => game.MakeMove(1));
        }

        [Fact]
        public void MakeMove_InWinningPosition_ShouldRaiseGameOverEvent()
        {
            var gameover = Assert.Raises<GameOverEventArgs>(
                handler => game.GameOverEvent += handler,
                handler => game.GameOverEvent -= handler,
                () => game.MakeMove(3));
            Assert.Equal("Player 1", gameover.Arguments.Winner);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        public void Start_WithNetWorkDiffrentValuesOfIsPlayerOne_ShouldCallNetworkRecieveAccordingly(bool isPlayerOne, int times)
        {
            var sut = new Game.Game(moqNet, null!, isPlayerOne);

            sut.Start();

            mock.Verify(x => x.Receive(), Times.Exactly(times));
        }

        [Fact]
        public void MakeMove_WithNetwork_ShouldCallNetworkMethods()
        {
            gameWithMockNet.MakeMove(1);

            mock.Verify(x => x.Send(It.IsAny<string>()), Times.Exactly(1));
            mock.Verify(x => x.Receive(), Times.Exactly(1));
        }

        [Fact]
        public void MakeMove_GameWonWithNetwork_ShouldNotEnterRecieveState()
        {
            gameWithMockNet.MakeMove(3);

            mock.Verify(x => x.Send(It.IsAny<string>()), Times.AtLeastOnce);
            mock.Verify(x => x.Receive(), Times.Never());
        }

        [Fact]
        public void Receiving_GameWon_ShouldTriggerGameOverEvent()
        {
            mock.Setup(x => x.Receive()).Returns(JsonHandler.Serialize(new GameState() { GameWonBy = Token.PlayerTwo }));

            var gameOver = Assert.Raises<GameOverEventArgs>(
                x => gameWithMockNet.GameOverEvent += x,
                x => gameWithMockNet.GameOverEvent -= x,
                () => gameWithMockNet.MakeMove(1));
            Assert.Equal("Player 2", gameOver.Arguments.Winner);
        }

        [Fact]
        public void Receiving_MoveCounter43_ShouldTriggerGameOverEvent()
        {
            mock.Setup(x => x.Receive()).Returns(JsonHandler.Serialize(new GameState() { MoveCounter = 43 }));

            var gameOver = Assert.Raises<GameOverEventArgs>(
                x => gameWithMockNet.GameOverEvent += x,
                x => gameWithMockNet.GameOverEvent -= x,
                () => gameWithMockNet.MakeMove(1));
            Assert.Equal("Draw.", gameOver.Arguments.Winner);
        }

        [Theory]
        [InlineData(1, "Draw.")]
        [InlineData(3, "Player 1")]
        public void MakeMove_MoveNumber42_ShouldRaiseGameOverEventWithCorrectArgs(int column, string winner)
        {
            game.MoveCounter = 42;
            var gameover = Assert.Raises<GameOverEventArgs>(
                x => game.GameOverEvent += x,
                x => game.GameOverEvent -= x,
                () => game.MakeMove(column));
            Assert.Equal(winner, gameover.Arguments.Winner);
        }

        [Fact]
        public void MakeMove_WithNetwork_ShouldSendExpectedData()
        {
            gameWithMockNet.Board[1, 5].State = Token.PlayerOne;
            var expected = JsonHandler.Serialize(new GameState()
            {
                PlayerOnesTurn = false,
                Board = gameWithMockNet.Board,
                GameWonBy = Token.None,
                MoveCounter = 2
            });
            gameWithMockNet.Board[1, 5].State = Token.None;

            gameWithMockNet.MakeMove(1);

            mock.Verify(x => x.Send(expected), Times.Exactly(1));
        }

        [Fact]
        public void SetupNewGame_ShouldRaiseBoardChangedEvent()
        {
            Assert.Raises<EventArgs>(
                handler => game.BoardChangedEvent += handler,
                handler => game.BoardChangedEvent -= handler,
                () => game.SetupNewGame());
        }

        [Fact]
        public void SetupNewGame_ShouldLetLooserGoFirst()
        {
            var expected = game.PlayerTwo;
            game.MakeMove(3); //winning move by player 1
            game.SetupNewGame();

            Assert.Equal(expected, game.ActivePlayer);
        }

        [Fact]
        public void SetupNewGame_ShouldResetBoardAndMoveCounter()
        {
            var expectedBoard = new Slot[7, 6];
            const int expectedMoveCounter = 1;

            game.SetupNewGame();

            Assert.Equal(expectedBoard, game.Board);
            Assert.Equal(expectedMoveCounter, game.MoveCounter);
        }

        [Fact]
        public void MakeMove_InASinglePlayerGame_ShouldTriggerAComputerMove()
        {
            var sut = new Game.Game(null!, null!, true, true);
            const int expectedMoveCounter = 3;
            sut.MakeMove(1);
            var actual = sut.MoveCounter;
            var playerTwoTokensFound = 0;
            foreach (var slot in sut.Board)
            {
                if (slot.State == Token.PlayerTwo) playerTwoTokensFound++;
            }
            Assert.Equal(1, playerTwoTokensFound);
            Assert.Equal(expectedMoveCounter, actual);
        }

        [Fact]
        public void Start_InASinglePlayerGameWherePlayerTwoGoesFirst_ShouldTriggerAComputerMove()
        {
            var sut = new Game.Game(null!, null!, true, true);
            sut.ActivePlayer = sut.PlayerTwo;

            const int expectedMoveCounter = 2;

            sut.Start();

            var playerTwoTokensFound = 0;
            foreach (var slot in sut.Board)
            {
                if (slot.State == Token.PlayerTwo) playerTwoTokensFound++;
            }

            var actual = sut.MoveCounter;

            Assert.Equal(1, playerTwoTokensFound);
            Assert.Equal(expectedMoveCounter, actual);
        }
    }
}