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
          //  play = new Connect_Four::Players(names, p1Icon);
            Assert.Equal(2, 2);
        }
    }

    public class xUTGrid
    {
        [Fact]
        public void GridTest()
        {
            
        }
    }
}
