using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class UI
    {
        public Board board = new Board();
        public GameDriver gameDriver = new GameDriver();
        public Rules rules = new Rules();
        public void Start()
        {
            rules.InitBoard();
            PrintBoard();
        }
        public void Game()
        {
            while (true)
            {
                Move();
                PrintBoard();
            }

        }

        public void Move()
        {
            Console.WriteLine("Zadej souradnice pro pohyb kamene X:");
            int posX = int.Parse(Console.ReadLine());
            Console.WriteLine("Zadej souradnice pro pohyb kamene Y:");
            int posY = int.Parse(Console.ReadLine());
            Console.WriteLine("Kámen X:{0} Y:{1}",posX,posY);
            Console.WriteLine("Zadej souradnice kam se chcete pohnout:");
            int posX1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Zadej souradnice kam se chcete pohnout:");
            int posY1 = int.Parse(Console.ReadLine());
            gameDriver.SetValueOnBoard(posX1, posY1, board.Coords(posX,posY));
            gameDriver.SetValueOnBoard(posX, posY, 0);
            //board.Coords(posX, posY);
        }
        /// <summary>
        /// Vykreslení desky
        /// </summary>
        public void PrintBoard()
        {
            Console.WriteLine("     A  B  C  D  E  F  G  H");
            Console.WriteLine("   ╔════════════════════════╗");
            for (int y = 7; y >= 0; y--)
            {
                Console.Write(" {0} ║", y + 1); //výpis souřadnic vlevo
                for (int x = 0; x < 8; x++)
                {
                    int figure = board.GetValue(x, y);
                    string set;
                    switch (figure)
                    {
                        case 2:
                            set = "O"; //bílá dáma
                            break;
                        case 1:
                            set = "o"; //bílá 
                                break;
                        case -1:
                            set = "x"; //černá
                                break;
                        case -2:
                            set = "X"; //černá dáma
                            break;
                        default:
                            set = " ";
                            break;
                    }
                    Console.Write(" {0} ",set);
                }
                Console.Write("║ {0}\n", y + 1); //výpis souřadnic vpravo
            }
            Console.WriteLine("   ╚════════════════════════╝");
            Console.WriteLine("     A  B  C  D  E  F  G  H");

        }
    }
}
