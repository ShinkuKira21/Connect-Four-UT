using System;

namespace Connect_Four
{
    public class GameStatus
    {
        protected Players players;
        protected Grid grid;

        public GameStatus(ref Players players, ref Grid grid)
        {
            this.players = players;
            this.grid = grid;
        }

        public bool GetGameStatus(char grid)
        { return false; }

        
    }
}