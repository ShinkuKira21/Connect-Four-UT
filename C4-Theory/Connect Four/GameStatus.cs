using System;

namespace Connect_Four
{
    public class GameStatus
    {
        protected Players players;
        protected char[] playerIcons;
        protected Grid grid;
        public int winnerIndex;

        public GameStatus(ref Players players, ref Grid grid)
        {
            this.players = players;
            this.grid = grid;

            playerIcons = new char[2];

            for(int i = 0; i < 2; i++)
                playerIcons[i] = this.players.GetPlayerIcon(i);
        }

        public bool GetGameStatus()
        { 
            if ((winnerIndex = CheckGame()) != -1) return false; // false = gameOver
            else return true; //true = game is not finished 
        } 

        public int GetWinner()
        { return winnerIndex; }

        int CheckGame()
        {
            int count = 0;
            char[][] grid = this.grid.GetGrid();

            for(int i = 0; i < 2; i++)
            {
                // Check Diagonals 
                for (int j = 0; j < this.grid.GetXSize(); j++)
                {
                    count = 0;
                    for (int k = 0; k < this.grid.GetYSize(); k++)
                    {
                        if (j == k)
                        {               
                            if (count == 4)
                                return i;

                            if (grid[j][k] == playerIcons[i]) count++;
                            else count = 0;
                        }
                    }
                }

                if (count == 4)
                    return i;

                    // Check Straight D-U
                    for (int j = 0; j < this.grid.GetYSize(); j++)
                    {
                        count = 0;
                        for (int k = 0; k < this.grid.GetXSize(); k++)
                        {  
                            //Check within loop (Internal)
                            if (count == 4)
                                return i;

                            if (grid[k][j] == playerIcons[i]) count++;
                            else count = 0;
                        }
                    }

                // Check after loop (External)
                if (count == 4)
                    return i;

                // Check Horizontal L-R
                for(int j = 0; j < this.grid.GetXSize(); j++)
                {
                    for(int k = 0; k < this.grid.GetYSize(); k++)
                    {               
                        if (count == 4)
                            return i;

                        if (grid[j][k] == playerIcons[i]) count++;
                        else count = 0;
                    }
                }

                if (count == 4)
                    return i;
            }
                       
                        

            return -1; // No Winner Determined
        }
    }
}