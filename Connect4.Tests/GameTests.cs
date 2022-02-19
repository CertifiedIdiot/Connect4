using Xunit;

namespace Connect4.Tests
{
    public class GameTests
    {
        public GameTests()
        {
            [Fact]
            void MakeMoveOutOfBoundsTest()
            {
                var game = new Game.Game(null!, true);

                for(int i = 0; i < 6; i++)
                {
                    game.MakeMove(1);
                }

                Assert.False(game.MakeMove(1));
            }

            [Fact]
            void MakeMoveInBoundsTest()
            {
                var game = new Game.Game(null!, true);

                for (int i = 0; i < 5; i++)
                {
                    game.MakeMove(6);
                }

                Assert.True(game.MakeMove(6));
            }
        }
    }
}