using System;

namespace Connect_Four
{
    class Players
    {
        protected string[] playerNames;
        protected char p1Icon;
        
        public Players(string[] playerNames, char p1Icon)
        {
            this.playerNames = playerNames;
            this.p1Icon = p1Icon;
        }

        public void SetPlayerName(string playerName, int sel)
        { playerNames[sel] = playerName; }

        public void SetPlayerIcon(char p1Icon)
        { this.p1Icon; }

        public string GetPlayerName(int sel)
        { return playerNames[sel]; }

        public char GetP1Icon()
        { return p1Icon; }
    }
}