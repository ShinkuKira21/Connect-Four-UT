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

            Assert.Equal('O', play.GetPlayerIcon(0));
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
       protected string[] names =
       { "John", "Mary" };

        protected Players play;
        protected Grid grid;

        [Fact]
        public void GridSetupTest()
        {
            play = new Players(names, 'O');
            grid = new Grid(ref play, 4, gridIcon: '-');

            Assert.Equal(IdealGrid(), grid.OutputGrid());
        }

        [Fact]
        public void DefaultGridTest()
        {
            play = new Players(names, 'X');
            grid = new Grid(ref play);

            Assert.Equal(DefaultGrid(), grid.OutputGrid());
        }

        [Fact]
        public void GridMoveTest()
        {
            play = new Players(names, 'X');
            grid = new Grid(ref play, 4, gridIcon: '-');

            grid.MakeMove(0, 2);
            grid.MakeMove(1, 2);
           
            Assert.NotEqual(IdealGrid(), grid.OutputGrid());
        }

        [Fact]
        public void GridValidation1()
        {
            // Check if pieces overlap 
            // Compares two different grids with same actions
            /* justification:
             * If the pieces would overlap each other,
             * Stacking won't exist and test would fail.
             * 
             * If pieces were places outside the array limits,
             * then a memory error will occur, which doesn't.
             */

            play = new Players(names, 'X');
            Grid copyGrid = new Grid(ref play, 4);
            grid = new Grid(ref play, 4);

            copyGrid.MakeMove(0, 2);
            copyGrid.MakeMove(1, 2);
            copyGrid.MakeMove(0, 2);
            copyGrid.MakeMove(1, 2);
            copyGrid.MakeMove(0, 2);

            grid.MakeMove(0, 2);
            grid.MakeMove(1, 2);
            grid.MakeMove(0, 2);
            grid.MakeMove(1, 2);
            grid.MakeMove(0, 2);

            Assert.Equal(copyGrid.OutputGrid(), grid.OutputGrid());
            Assert.Equal(ValidationGridNoOverlap(), grid.OutputGrid());
        }

        /* Static Visual Representation Grid Expectations */

        string DefaultGrid()
        {
            /* EXPECTED OUTPUT:
             *    1   2   3   4   5   6   
             * 1  .   .   .   .   .   .
             * 2  .   .   .   .   .   .
             * 3  .   .   .   .   .   .
             * 4  .   .   .   .   .   .   
             * 5  .   .   .   .   .   .
             * 6  .   .   .   .   .   .
             * 7  .   .   .   .   .   .
             */

            string defaultGrid = "    1   2   3   4   5   6   "
                + "\n1   .   .   .   .   .   .   "
                + "\n2   .   .   .   .   .   .   "
                + "\n3   .   .   .   .   .   .   "
                + "\n4   .   .   .   .   .   .   "
                + "\n5   .   .   .   .   .   .   "
                + "\n6   .   .   .   .   .   .   "
                + "\n7   .   .   .   .   .   .   \n";
            return defaultGrid;
        }

        string IdealGrid()
        {
            /* EXPECTED OUTPUT:
             *     1   2   3   4   
             * 1   -   -   -   -   
             * 2   -   -   -   -   
             * 3   -   -   -   -   
             * 4   -   -   -   -   
             */

            string idealGrid = "    1   2   3   4   "
                + "\n1   -   -   -   -   "
                + "\n2   -   -   -   -   "
                + "\n3   -   -   -   -   "
                + "\n4   -   -   -   -   \n";

            return idealGrid;
        }

        string ValidationGridNoOverlap()
        {
            /* EXPECTED OUTPUT:
             *    1   2   3   4   
             * 1  .   .   O   .   
             * 2  .   .   X   .   
             * 3  .   .   O   .   
             * 4  .   .   X   .   
             */

            string validationGrid = "    1   2   3   4   "
                + "\n1   .   .   O   .   "
                + "\n2   .   .   X   .   "
                + "\n3   .   .   O   .   "
                + "\n4   .   .   X   .   \n";

            return validationGrid;
        }
    }
}
