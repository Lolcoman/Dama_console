using System;

namespace Damakonzole
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.Start();
            ui.Game();

            Console.WriteLine("Stiskni ENTER pro ukonceni");
            Console.ReadLine();
        }
    }
}
