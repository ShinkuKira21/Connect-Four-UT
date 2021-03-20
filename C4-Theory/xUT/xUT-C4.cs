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
            // Expected: 
            // Tests if names[i] matches GetPlayerName(i)
            // O to match play.GetPlayerIcon(0)

            play = new Players(names, 'O');

            for(int i = 0; i < 2; i++)
                Assert.Equal(names[i], play.GetPlayerName(i));

            Assert.Equal('O', play.GetPlayerIcon(0));
        }

        [Fact]
        public void PlayerEditTest()
        {
            // Expected:
            // Tests if the setters work appropriately in the Players class.
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
            // Expected: 
            // P should be reset to X as default. P is not X or O.

            // X will be selected as default if O is not chose
            play = new Players(names, 'P');

            Assert.NotEqual('P', play.GetPlayerIcon(0));
            Assert.Equal('X', play.GetPlayerIcon(0));
        }

        [Fact]
        public void PlayerEditIconConstraint()
        {
            // Exppected:
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
            // Expected:
            // To test the grid, an IdealGrid is created,
            // which is cross-referenced with the actual
            // OutputGrid. If they match then they pass.

            // Tested 3 time. Spacing was wrong between characters.

            play = new Players(names, 'O');
            grid = new Grid(ref play, 4, gridIcon: '-');

            Assert.Equal(IdealGrid(), grid.OutputGrid());
        }

        [Fact]
        public void DefaultGridTest()
        {
            // Expected:
            // To test the default grid
            // against the Grid OutputGrid.
           
            play = new Players(names, 'X');
            grid = new Grid(ref play);

            Assert.Equal(DefaultGrid(), grid.OutputGrid());
        }

        [Fact]
        public void GridMoveTest()
        {
            // Expected:
            // To test if a move is made by a player.
            // This test is conducted by checking if IdealGrid() 
            // is not equal to the generated grid from the Grid class. 
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
            /* Expected:
             * Check if GameStatus is false, 
             * as game should end if winner is found.
             * 
             * Check if the winner playerID one, 
             * which is Mary.
             * 
             * Check if GameStatus is false.
             * Check if the winner is John.
             * 
             * Check if GameStatus is true,
             * No winner.
             */

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            VerticalMove(1, 0);

            // if winner is Mary
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            // if winner is John
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
            /* Expected:
             * Check if GameStatus is false, 
             * as game should end if winner is found.
             * 
             * Check if the winner playerID one, 
             * which is Mary.
             * 
             * Check if GameStatus is false.
             * Check if the winner is John.
             * 
             * Check if GameStatus is true,
             * No winner.
             */

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            HorizontalMove(1, 0);

            // if winner is Mary
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            // if winner is John
            HorizontalMove();
            Assert.False(gs.GetGameStatus());
            Assert.Equal(0, gs.GetWinner());

            grid.ClearGrid();

            // No winner, Game Continuing
            HorizontalMove(bWinGame: false);
            Assert.True(gs.GetGameStatus()); 
        }

        [Fact]
        public void DiagonalTLBRMoveWinCheck()
        {
            /* Expected:
             * Check if GameStatus is false, 
             * as game should end if winner is found.
             * 
             * Check if the winner playerID one, 
             * which is Mary.
             * 
             * Check if GameStatus is false.
             * Check if the winner is John.
             * 
             * Check if GameStatus is true,
             * No winner.
             */

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            DiagonalTLBRMove(1, 0);

            // if winner is Mary
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            // if winner is John
            DiagonalTLBRMove();
            Assert.False(gs.GetGameStatus());
            Assert.Equal(0, gs.GetWinner());

            grid.ClearGrid();

            DiagonalTLBRMove(bWinGame: false);
            Assert.True(gs.GetGameStatus()); // No winner, Game Continuing
        }

        [Fact]
        public void DiagonalTRBLMoveWinCheck()
        {
            /* Expected:
             * Check if GameStatus is false, 
             * as game should end if winner is found.
             * 
             * Check if the winner playerID one, 
             * which is Mary.
             * 
             * Check if GameStatus is false.
             * Check if the winner is John.
             * 
             * Check if GameStatus is true,
             * No winner.
             */

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            gs = new GameStatus(ref play, ref grid);

            DiagonalTRBLMove(1, 0);

            // if winner is Mary
            Assert.False(gs.GetGameStatus()); // this updates the winner (if false gameOver)
            Assert.Equal(1, gs.GetWinner());

            grid.ClearGrid();

            // if winner is John
            DiagonalTRBLMove();
            Assert.False(gs.GetGameStatus());
            Assert.Equal(0, gs.GetWinner());

            grid.ClearGrid();

            // No winner, Game Continuing
            DiagonalTRBLMove(bWinGame: false);
            Assert.True(gs.GetGameStatus()); 
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

    public class xUTBlockMoveTheory
    {
        protected string[] names =
       { "John", "Mary" };

        protected Players play;
        protected Grid grid;

        protected char[][] board;

        [Fact]
        public void VerticalElimination()
        {
            // Scenario Tests : Vertical Block Strategy | Reference: 
            // Software Engineering, Author: Edward Patch
            // Sub-Section III-C, Vertical Strategy - Plan A.

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            board = grid.GetGrid();
            VerticalMove();

            Assert.True(EliminateMove(board));
        }

        [Fact]
        public void HorizontalElimination()
        {
            // Scenario Tests : Horizontal Block Strategy | Reference: 
            // Software Engineering, Author: Edward Patch
            // Sub-Section III-C, Horizontal Strategy - Plan A.

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            board = grid.GetGrid();
            HorizontalMove();

            Assert.True(EliminateMove(board));
        }

        [Fact]
        public void DiagonalTLBRElimination()
        {
            // Scenario Tests : TL Diagonal Block Strategy | Reference: 
            // Software Engineering, Author: Edward Patch
            // Sub-Section III-C, TL Diagonal Strategy - Plan A.

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            board = grid.GetGrid();
            DiagonalTLBRMove();

            Assert.True(EliminateMove(board));
        }

        [Fact]
        public void DiagonalTRBLElimination()
        {
            // Scenario Tests : TR Diagonal Block Strategy | Reference: 
            // Software Engineering, Author: Edward Patch
            // Sub-Section III-C, TR Diagonal Strategy - Plan A.

            play = new Players(names, 'X');
            grid = new Grid(ref play);
            board = grid.GetGrid();
            DiagonalTRBLMove();

            Assert.True(EliminateMove(board));
        }

        void VerticalMove(int horizontal = 0, int player1 = 0, int player2 = 1)
        {
            // Debug Vertical 
            grid.MakeMove(player2, horizontal);
            grid.MakeMove(player2, horizontal);
            grid.MakeMove(player2, horizontal);
            grid.MakeMove(player1, 3);

            if(horizontal + 2 < grid.GetYSize())
            {
                grid.MakeMove(player1, horizontal + 1);
                grid.MakeMove(player1, horizontal + 2);
            }
        }

        void HorizontalMove(int player1 = 0, int player2 = 1)
        {
            // Debug Horizontal 
            grid.MakeMove(player2, 0);
            grid.MakeMove(player1, 1);
            grid.MakeMove(player2, 2);
            grid.MakeMove(player1, 3);
            grid.MakeMove(player2, 4);
            grid.MakeMove(player1, 5);

            grid.MakeMove(player2, 0);
            grid.MakeMove(player2, 1);
            grid.MakeMove(player1, 2);
            grid.MakeMove(player1, 3);
            grid.MakeMove(player1, 4);
            grid.MakeMove(player2, 5);
        }

        void DiagonalTLBRMove(int player1 = 0, int player2 = 1)
        {
            //Debug Diagonal TL-BR
            grid.MakeMove(player2, 0);
            grid.MakeMove(player1, 0);
            grid.MakeMove(player1, 0);
            grid.MakeMove(player2, 0);
            grid.MakeMove(player1, 0);
            grid.MakeMove(player1, 0);
            grid.MakeMove(player1, 0);

            grid.MakeMove(player1, 1);
            grid.MakeMove(player1, 1);
            grid.MakeMove(player1, 1);
            grid.MakeMove(player2, 1);
            grid.MakeMove(player2, 1);
            grid.MakeMove(player2, 1);

            grid.MakeMove(player2, 2);
            grid.MakeMove(player1, 2);
            grid.MakeMove(player1, 2);
            grid.MakeMove(player2, 2);
            grid.MakeMove(player1, 2);

            grid.MakeMove(player2, 3);
            grid.MakeMove(player2, 3);
            grid.MakeMove(player2, 3);
            grid.MakeMove(player1, 3);
        }

        void DiagonalTRBLMove(int player1 = 0, int player2 = 1)
        {
            //Debug Diagonal TR-BL
            grid.MakeMove(player2, 5);
            grid.MakeMove(player1, 5);
            grid.MakeMove(player1, 5);
            grid.MakeMove(player2, 5);
            grid.MakeMove(player1, 5);
            grid.MakeMove(player1, 5);
            grid.MakeMove(player1, 5);

            grid.MakeMove(player1, 4);
            grid.MakeMove(player1, 4);
            grid.MakeMove(player1, 4);
            grid.MakeMove(player2, 4);
            grid.MakeMove(player2, 4);
            grid.MakeMove(player1, 4);

            grid.MakeMove(player2, 3);
            grid.MakeMove(player1, 3);
            grid.MakeMove(player1, 3);
            grid.MakeMove(player2, 3);
            grid.MakeMove(player1, 3);

            grid.MakeMove(player2, 2);
            grid.MakeMove(player2, 2);
            grid.MakeMove(player2, 2);
            grid.MakeMove(player1, 2);
        }

        bool EliminateMove(char[][] board)
        {
            // Two problems with these methods are dropping pieces higher than 1 row,
            // and XOXX (won't swipe) but XXXO will (Solution:
            // Count system where only swipe if there is a gap of two for example
            // XO-X (will return to naught) or XOXO will only count to two.

            // Another problem with this method,
            // if XOX- exists and for argument sake, player X puts another piece
            // XOXX (it will currently swipe if called in a looping system)
            // The function will have to know which turn is whos.
            
            // Vertical Block
            for (int i = 0; i < grid.GetXSize(); i++)
                for (int j = 0; j < grid.GetYSize(); j++)
                    if (i + 3 < grid.GetXSize())
                        if (board[i][j] == board[i + 1][j] && board[i][j] == board[i+2][j] && board[i][j] != board[i+3][j] && board[i+3][j] != grid.GetGridIcon())
                        {
                            board[i][j] = board[i+3][j];
                            board[i + 1][j] = grid.GetGridIcon();
                            board[i + 2][j] = grid.GetGridIcon();

                            return true;
                        }

            // Horrizontal Block
            for (int i = 0; i < grid.GetXSize(); i++)
                for (int j = 0; j < grid.GetYSize(); j++)
                    if (j + 3 < grid.GetYSize())
                        if (board[i][j] == board[i][j + 1] && board[i][j] == board[i][j + 2] && board[i][j] != board[i][j + 3] && board[i][j + 3] != grid.GetGridIcon())
                        {
                            // this will only drop the layer above only
                            // solution: would need a external function to check
                            // if there is nothing above the gridIcon.

                            board[i][j] = board[i-1][j];
                            board[i][j + 1] = board[i-1][j];
                            board[i][j + 2] = board[i-1][j];

                            return true;
                        }

            // Diagonal Block TR - BL - Plan B
            for (int i = 0; i < grid.GetXSize(); i++)
                for (int j = 0; j < grid.GetYSize(); j++)
                    if (i + 3 < grid.GetXSize() && j - 3 >= 0)
                        if (board[i][j] == board[i + 1][j - 1] && board[i][j] == board[i + 2][j - 2] && board[i][j] != board[i + 3][j - 3] && board[i+3][j-3] == grid.GetGridIcon())
                        {
                            /* Not too sure here ||
                             * Here's my theory -
                             * The coordinates below will be reset to grid Icon 
                             * or upper piece
                             * x:0, x:1, x:3
                             * y:3, y:2, y:0
                             * 
                             * The coordinates below will drop to above pieces
                             * x:2 -> x:3, x:0 -> x:1
                             * y:0 -> y:0, y:2 -> y:2
                             */

                            // only gets upper row (it's broken logic)
                            board[i][j] = board[i - 1][j];
                            board[i][j + 1] = board[i - 1][j + 1];
                            board[i][j + 2] = board[i - 1][j + 2];

                            return true;
                        }

            // Diagonal Block TL - BR - Plan A
            for (int i = 0; i < grid.GetXSize(); i++)
                for (int j = 0; j < grid.GetYSize(); j++)
                    if (board[i][j] == board[i + 1][j + 1] && board[i][j] == board[i + 2][j + 2] && board[i][j] != board[i + 3][j + 3] && board[i + 3][j + 3] == grid.GetGridIcon())
                    {
                        /* Not too sure here ||
                            * Here's my theory -
                            * The coordinates below will be reset to grid Icon 
                            * or upper piece
                            * x:0, x:1, x:3
                            * y:3, y:2, y:0
                            * 
                            * The coordinates below will drop to above pieces
                            * x:2 -> x:3, x:0 -> x:1
                            * y:0 -> y:0, y:2 -> y:2
                            */

                        // only gets upper row
                        board[i][j] = board[i - 1][j];
                        board[i + 1][j + 1] = board[i - 1][j];
                        board[i + 2][j + 2] = board[i - 1][j];

                        return true;
                    }

            return false;
        }
    }
}
