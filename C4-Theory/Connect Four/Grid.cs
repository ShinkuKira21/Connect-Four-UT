using System;

namespace Connect_Four 
{
    public class Grid
    {
        protected char[][] grid;
        int x, y; // Grid size
        char gridIcon;

        // default: 3 x 3 | Icon: .
        public Grid()
        {
            x = 3;
            y = 3;

            gridIcon = '.';
        }

        // default: ySize: -1 (Sets y to x) Icon: .
        public Grid(int xSize, int ySize = -1, char gridIcon = '.')
        {
            if(CheckSizes(xSize, ref ySize))
                { x = xSize; y = ySize; } 

            if(CheckIcon(gridIcon))
                this.gridIcon = gridIcon;
        }

        public char GetGridIcon()
        { return gridIcon; }

        //  returns string of grid.
        public string OutputGrid()
        {
            string gridOutput = null;

            for(int i = 0; i < x; i++)
                gridOutput += i + "   ";

            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                    gridOutput += grid[i][j] + "   ";

                gridOutput += "\n";
            }

            return gridOutput;
        }

        // draws grid
        public void DrawGrid()
        {
            Console.WriteLine(OutputGrid());
        }

        bool CheckSizes(int x, ref int y)
        {
            bool bConstraintSize = false;

            if(x > 0 && x < 30) 
                bConstraintSize = true;
            
            if(y != -1)
                if(y > 0 && y < 30)
                    bConstraintSize = true;

            else 
            {
                bConstraintSize = true;
                y = x;
            }

            return bConstraintSize;
        }

        bool CheckIcon(char gridIcon)
        {
            if(gridIcon == 'x' || gridIcon == 'X')
                return false;
            
            if(gridIcon == 'O' || gridIcon == 'o')
                return false;

            return true;
        }

        void InitialiseGrid()
        {

        }
    }
}