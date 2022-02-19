using Xunit;

namespace Connect4.Tests
{
    public class GameTests
    {
        private readonly Game.Game game;
        public GameTests()
        {
            game = new Game.Game(null!, true);
        }

        [Theory]
        [InlineData(1, 6, false)]
        [InlineData(1, 5, true)]
        public void MakeMoveBoundsTest(int column, int times, bool expected)
        {
            for (int i = 0; i < times; i++)
            {
                game.MakeMove(column);
            }

            Assert.Equal(expected, game.MakeMove(column));
        }
    }
}
