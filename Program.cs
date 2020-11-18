using System;

namespace Damakonzole
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.Game();

            //Board board = new Board();

            //int[] move1 = new int[] { 0, 1, 0, 1, 1, 2, 0, 1 };
            //Console.WriteLine("Move1 Krok: \t{0}", board.PohybNaString(move1));

            //int[] move2 = new int[] { 1, 1, 1, 0, 2, 2, -1, 0, 3, 3, 0, 1 };
            //Console.WriteLine("Move2 Skok: \t{0}", board.PohybNaString(move2));

            Console.WriteLine("Stiskni ENTER pro ukonceni");
            Console.ReadLine();
        }
    }
}
