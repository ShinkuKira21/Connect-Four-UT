using System;

namespace Connect_Four
{
    class Program
    {
        protected static string[] names =
       { "John", "Mary" };
        protected static Players play;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            play = new Players(names, 'O');

            

            // inverted
            play.SetPlayerName("Ma", 0);
            play.SetPlayerName("Jo", 1);
            play.SetPlayerIcon('X');

            for (int i = 0; i < 2; i++)
                Console.Write(names[i] + ", ");

            Console.WriteLine();

            Console.WriteLine(play.GetPlayerName(0) + ", " + play.GetPlayerName(1));
        }
    }
}
