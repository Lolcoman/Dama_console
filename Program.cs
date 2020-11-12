using System;

namespace Damakonzole
{
    class Program
    {
        static void Main(string[] args)
        {
            //GameController gameController = new GameController();
            //gameController.Game();

            Board board = new Board();
            UI ui = new UI(board);
            Rules rules = new Rules(board);
            rules.InitBoard();
            ui.PrintBoard();
            int[] pohyb = ui.InputUser();
            int[] kompletniPohyb = rules.FullMove(pohyb);

            Console.Write("Pole zadaného pohybu:\t\t|");
            for (int i = 0; i < pohyb.Length; i++)
            {
                Console.Write("{0}|", pohyb[i]);
            }

            Console.WriteLine();

            Console.Write("Pole kompletního pohybu:\t|");
            for (int i = 0; i < kompletniPohyb.Length; i++)
            {
                Console.Write("{0}|", kompletniPohyb[i]);
            }
            Console.WriteLine();

            Console.WriteLine("Stiskni ENTER pro ukonceni");
            Console.ReadLine();
        }
    }
}
