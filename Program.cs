using System;

namespace Damakonzole
{
    class Program
    {
        static void Main(string[] args)
        {
            GameDriver gameDriver = new GameDriver();
            gameDriver.Game();

            Console.WriteLine("Stiskni ENTER pro ukonceni");
            Console.ReadLine();
        }
    }
}
