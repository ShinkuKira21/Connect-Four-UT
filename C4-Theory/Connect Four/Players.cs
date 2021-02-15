/* Author: Edward Patch */

namespace Connect_Four
{
    public class Players
    {
        protected string[] playerNames;
        protected char p1Icon;
        
        public Players(string[] playerNames, char p1Icon)
        {
            this.playerNames = new string[2];

            for (int i = 0; i < 2; i++)
                this.playerNames[i] = playerNames[i];

            if (CheckPlayerIcon(p1Icon))
                this.p1Icon = p1Icon;

            else this.p1Icon = 'X'; // set as X if fail
        }

        public void SetPlayerName(string playerName, int sel)
        { playerNames[sel] = playerName; }

        public void SetPlayerIcon(char p1Icon)
        {
            if (CheckPlayerIcon(p1Icon))
                this.p1Icon = p1Icon;
        }

        public string GetPlayerName(int sel)
        { return playerNames[sel]; }

        public char GetPlayerIcon(int sel)
        { 
            if(sel == 0) return p1Icon;
            else
            {
                if (p1Icon == 'X') return 'O';
                else return 'X';
            }
        }

        bool CheckPlayerIcon(char icon)
        {
            if (icon == 'X' || icon == 'O') return true;
            return false;
        }
    }
}