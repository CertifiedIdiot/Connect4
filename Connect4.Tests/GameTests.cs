using Connect4.Enums;
using Connect4.Game;
using Connect4.Structs;
using System;
using Xunit;

namespace Connect4.Tests
{
    public class GameTests
    {
        readonly Game.Game game;
        public GameTests()
        {
            game = new Game.Game(null!, true);
            var c = new Slot { State = Token.None };
            var X = new Slot { State = Token.PlayerOne };
            var O = new Slot { State = Token.PlayerTwo };
            game.Board = new Slot[,]
            {
                //left side, rows
               //0,1,2,3,4,5
                {c,c,c,c,c,c},//0
                {c,c,c,c,c,c},//1
                {O,O,O,X,X,X},//2 columns
                {c,c,c,X,X,X},//3 bottom
                {c,c,c,c,c,c},//4
                {c,c,c,c,c,c},//5
                {c,c,c,c,c,c},//6
                
            };
        }

        [Fact]
        public void MakeMove_AttemptInFullColumn_ShouldReturnFalse()
        {
            Assert.False(game.MakeMove(2));
        }

        [Fact]
        public void MakeMove_ValidMove_ShouldReturnTrue()
        {
            Assert.True(game.MakeMove(1));
        }

        [Fact]
        public void MakeMove_ValidMove_ShouldRaiseBoardChangeEvent()
        {
            Assert.Raises<EventArgs>(handler => game.BoardChangedEvent += handler, handler => game.BoardChangedEvent -= handler, () => game.MakeMove(1));
        }
        [Fact]
        public void MakeMove_InWinningPosition_ShouldRaiseGameOverEvent()
        {
            Assert.Raises<GameOverEventArgs>(handler => game.GameOverEvent += handler, handler => game.GameOverEvent -= handler, () => game.MakeMove(3));
        }
    }

}