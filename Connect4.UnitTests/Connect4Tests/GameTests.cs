
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Game;

namespace Connect4Tests.Connect4Tests
{
    [TestFixture]
    public class GameTests
    {
        private Game game;
        [SetUp]
        public void SetUp()
        {
            game = new Game();
        }

        [Test]
        public void MakeMove_ColumnIsBetweenZeroAndSevenAndRowIsBeggarThanZero_ReturnTrue()
        {

            var result = game.MakeMove(3);
            //var Row = game.CheckColumn(1);
            //game.ActivePlayer = game.PlayerTwo;
            Assert.IsTrue(result);


        }





    }
}
