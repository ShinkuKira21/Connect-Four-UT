/* Author: Edward Patch */


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

        [Fact]
        public void PlayerConstructorIconConstraint()
        {
            // X will be selected as default if O is not chose
            play = new Players(names, 'P');

            Assert.NotEqual('P', play.GetPlayerIcon(0));
            Assert.Equal('X', play.GetPlayerIcon(0));
        }

        [Fact]
        public void PlayerEditIconConstraint()
        {
            // if player icon is changed to
            // invalid character, then 
            // no changes will be made.
            play = new Players(names, 'O');

            play.SetPlayerIcon('.');

            Assert.NotEqual('.', play.GetPlayerIcon(0));
            Assert.Equal('O', play.GetPlayerIcon(0));
        }

        [Fact]
        public void Player2IconTest()
        {
            // Player 2 is the opposite
            // piece to player 1
            play = new Players(names, 'O');

            Assert.Equal('X', play.GetPlayerIcon(1));

            play.SetPlayerIcon('X');

            Assert.Equal('O', play.GetPlayerIcon(1));
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

    public class xUTGameStatus
    {
        protected string[] names =
       { "John", "Mary" };

        protected Players play;
        protected Grid grid;
        protected GameStatus gs;

        [Fact]
        public void VerticalMoveWinCheck()
        {
            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            VerticalMove(1, 0);

            // if winner is John
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            VerticalMove();
            Assert.False(gs.GetGameStatus());
            Assert.Equal(0, gs.GetWinner());

            grid.ClearGrid();

            VerticalMove(bWinGame: false);
            Assert.True(gs.GetGameStatus()); // No winner, Game Continuing
        }

        [Fact]
        public void HorizontalMoveWinCheck()
        {
            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            HorizontalMove(1, 0);

            // if winner is John
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            HorizontalMove();
            Assert.False(gs.GetGameStatus());
            Assert.Equal(0, gs.GetWinner());

            grid.ClearGrid();

            HorizontalMove(bWinGame: false);
            Assert.True(gs.GetGameStatus()); // No winner, Game Continuing
        }

        [Fact]
        public void DiagonalTLBRMoveWinCheck()
        {
            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            DiagonalTLBRMove(1, 0);

            // if winner is John
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            DiagonalTLBRMove();
            Assert.False(gs.GetGameStatus());
            Assert.Equal(0, gs.GetWinner());

            grid.ClearGrid();

            DiagonalTLBRMove(bWinGame: false);
            Assert.True(gs.GetGameStatus()); // No winner, Game Continuing
        }

        [Fact]
        public void DiagonalTRBLMoveCheck()
        {
            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            DiagonalTRBLMove(1, 0);

            // if winner is John
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            DiagonalTRBLMove();
            Assert.False(gs.GetGameStatus());
            Assert.Equal(0, gs.GetWinner());

            grid.ClearGrid();

            DiagonalTRBLMove(bWinGame: false);
            Assert.True(gs.GetGameStatus()); // No winner, Game Continuings
        }

        void VerticalMove(int winner = 0, int loser = 1, bool bWinGame = true)
        {
            // Debug Vertical 
            int horizontal = 5;
            grid.MakeMove(loser, horizontal);
            grid.MakeMove(loser, horizontal);
            grid.MakeMove(loser, horizontal);
            grid.MakeMove(winner, horizontal);
            grid.MakeMove(winner, horizontal);
            grid.MakeMove(winner, horizontal);
            if(bWinGame) grid.MakeMove(winner, horizontal);
        }

        void HorizontalMove(int winner = 0, int loser = 1, bool bWinGame = true)
        {
            // Debug Horizontal 
            grid.MakeMove(loser, 0);
            grid.MakeMove(winner, 1);
            grid.MakeMove(loser, 2);
            grid.MakeMove(winner, 3);
            grid.MakeMove(loser, 4);
            grid.MakeMove(winner, 5);

            grid.MakeMove(loser, 0);
            grid.MakeMove(loser, 1);
            grid.MakeMove(winner, 2);
            grid.MakeMove(winner, 3);
            grid.MakeMove(winner, 4);
            if(bWinGame) grid.MakeMove(winner, 5);
        }

        void DiagonalTLBRMove(int winner = 0, int loser = 1, bool bWinGame = true)
        {
            //Debug Diagonal TL-BR
            grid.MakeMove(loser, 0);
            grid.MakeMove(winner, 0);
            grid.MakeMove(winner, 0);
            grid.MakeMove(loser, 0);
            grid.MakeMove(winner, 0);
            grid.MakeMove(winner, 0);
            grid.MakeMove(winner, 0);

            grid.MakeMove(winner, 1);
            grid.MakeMove(winner, 1);
            grid.MakeMove(winner, 1);
            grid.MakeMove(loser, 1);
            grid.MakeMove(loser, 1);
            if(bWinGame) grid.MakeMove(winner, 1);

            grid.MakeMove(loser, 2);
            grid.MakeMove(winner, 2);
            grid.MakeMove(winner, 2);
            grid.MakeMove(loser, 2);
            grid.MakeMove(winner, 2);

            grid.MakeMove(loser, 3);
            grid.MakeMove(loser, 3);
            grid.MakeMove(loser, 3);
            grid.MakeMove(winner, 3);
        }

        void DiagonalTRBLMove(int winner = 0, int loser = 1, bool bWinGame = true)
        {
            //Debug Diagonal TR-BL
            grid.MakeMove(loser, 5);
            grid.MakeMove(winner, 5);
            grid.MakeMove(winner, 5);
            grid.MakeMove(loser, 5);
            grid.MakeMove(winner, 5);
            grid.MakeMove(winner, 5);
            grid.MakeMove(winner, 5);

            grid.MakeMove(winner, 4);
            grid.MakeMove(winner, 4);
            grid.MakeMove(winner, 4);
            grid.MakeMove(loser, 4);
            grid.MakeMove(loser, 4);
            if (bWinGame) grid.MakeMove(winner, 4);

            grid.MakeMove(loser, 3);
            grid.MakeMove(winner, 3);
            grid.MakeMove(winner, 3);
            grid.MakeMove(loser, 3);
            grid.MakeMove(winner, 3);

            grid.MakeMove(loser, 2);
            grid.MakeMove(loser, 2);
            grid.MakeMove(loser, 2);
            grid.MakeMove(winner, 2);
        }
    }

    class xUTVBlockMoveTheory
    {

    }
}
