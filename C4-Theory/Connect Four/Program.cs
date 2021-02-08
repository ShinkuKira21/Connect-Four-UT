using System;

namespace Connect_Four
{
    class Program
    {
        protected static string[] names =
        { "John", "Mary" };
        protected static Players play;
        protected static Grid grid;

        static void Main(string[] args)
        {
            PlayerSetup();
            GridSetup();
        }

        static void PlayerSetup()
        {
            play = new Players(names, 'O');

            // inverted
            play.SetPlayerName("James", 0);
            play.SetPlayerName("Wiliams", 1);
            play.SetPlayerIcon('X');
        }

        static void GridSetup()
        {
            // default grid
            grid = new Grid(ref play);

            grid.DrawGrid();

            grid.MakeMove(0, 2);
            grid.MakeMove(1, 2);
            grid.MakeMove(0, 2);
            grid.MakeMove(1, 2);

            grid.DrawGrid();
        }
    }
}
