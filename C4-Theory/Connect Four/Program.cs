using System;

namespace Connect_Four
{
    class Program
    {
        protected static string[] names =
        { "John", "Mary" };
        protected static Players play;
        protected static Grid grid;
        protected static GameStatus gs;

        static void Main(string[] args)
        {
            PlayerSetup();
            GridSetup();
            GameSetup();
        }

        static void PlayerSetup()
        {
            play = new Players(names, 'O');

            play.SetPlayerName("James", 0);
            play.SetPlayerName("Wiliams", 1);

            play.SetPlayerIcon('X');
        }

        static void GridSetup()
        {
            // default grid
            grid = new Grid(ref play);

            grid.DrawGrid();

            // Debug Vertical 
            //int horizontal = 5;
            //grid.MakeMove(1, horizontal);
            //grid.MakeMove(1, horizontal);
            //grid.MakeMove(1, horizontal);
            //grid.MakeMove(0, horizontal);
            //grid.MakeMove(0, horizontal);
            //grid.MakeMove(0, horizontal);
            //grid.MakeMove(0, horizontal);

            // Debug Horizontal 
            //grid.MakeMove(0, 0);
            //grid.MakeMove(1, 1);
            //grid.MakeMove(0, 2);
            //grid.MakeMove(1, 3);
            //grid.MakeMove(0, 4);
            //grid.MakeMove(1, 5);

            //grid.MakeMove(0, 0);
            //grid.MakeMove(0, 1);
            //grid.MakeMove(1, 2);
            //grid.MakeMove(1, 3);
            //grid.MakeMove(1, 4);
            //grid.MakeMove(1, 5);

            //Debug Diagonal TL-BR
            //grid.MakeMove(0, 0);
            //grid.MakeMove(0, 0);
            //grid.MakeMove(0, 0);
            //grid.MakeMove(1, 0);
            //grid.MakeMove(0, 0);
            //grid.MakeMove(0, 0);
            //grid.MakeMove(0, 0);

            //grid.MakeMove(0, 1);
            //grid.MakeMove(0, 1);
            //grid.MakeMove(0, 1);
            //grid.MakeMove(1, 1);
            //grid.MakeMove(1, 1);
            //grid.MakeMove(1, 1);

            //grid.MakeMove(1, 2);
            //grid.MakeMove(0, 2);
            //grid.MakeMove(0, 2);
            //grid.MakeMove(1, 2);
            //grid.MakeMove(0, 2);

            //grid.MakeMove(0, 3);
            //grid.MakeMove(1, 3);
            //grid.MakeMove(1, 3);
            //grid.MakeMove(0, 3);

            //Debug Diagonal TR - BL
            grid.MakeMove(1, 5);
            grid.MakeMove(0, 5);
            grid.MakeMove(0, 5);
            grid.MakeMove(1, 5);
            grid.MakeMove(0, 5);
            grid.MakeMove(0, 5);
            grid.MakeMove(0, 5);

            grid.MakeMove(0, 4);
            grid.MakeMove(0, 4);
            grid.MakeMove(0, 4);
            grid.MakeMove(1, 4);
            grid.MakeMove(1, 4);
            grid.MakeMove(0, 4);

            grid.MakeMove(1, 3);
            grid.MakeMove(0, 3);
            grid.MakeMove(0, 3);
            grid.MakeMove(1, 3);
            grid.MakeMove(0, 3);

            grid.MakeMove(0, 2);
            grid.MakeMove(1, 2);
            grid.MakeMove(1, 2);
            grid.MakeMove(0, 2);

            grid.DrawGrid();
        }

        static void GameSetup()
        {
            gs = new GameStatus(ref play, ref grid);

            if (gs.GetGameStatus())
                Console.WriteLine("Game Continuing");

            else
                Console.WriteLine("Game Ended | Winner: " + play.GetPlayerName(gs.GetWinner()));
        }
    }
}
