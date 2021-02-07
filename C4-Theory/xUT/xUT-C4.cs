using System;
using Xunit;
using Connect_Four;

namespace xUT
{
    public class xUTPlayer
    {
        protected string[] names =
        { "John", "Mary" };
        protected Players play;

        [Fact]
        public void PlayerTest()
        {
            play = new Players(names, 'O');

            for(int i = 0; i < 2; i++)
                Assert.Equal(names[i], play.GetPlayerName(i));

            Assert.Equal('O', play.GetPlayerIcon());
        }

        [Fact]
        public void PlayerEditTest()
        {
            play = new Players(names, 'O');

            // inverted names
            play.SetPlayerName("Mary", 0);
            play.SetPlayerName("John", 1);
            play.SetPlayerIcon('X');

            for (int i = 0; i < 2; i++)
                Assert.NotEqual(names[i], play.GetPlayerName(i));

            Assert.Equal('X', play.GetPlayerIcon(0));
        }
    }

    public class xUTGrid
    {
        [Fact]
        public void GridSetupTest()
        {
            
        }
    }
}
