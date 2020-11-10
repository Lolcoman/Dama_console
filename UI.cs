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
        //public void Start()
        //{
        //    rules.InitBoard();
        //    PrintBoard();
        //}
        //public void Game()
        //{
        //    rules.InitBoard();
        //    PrintBoard();

        //    while (true)
        //    {
        //        Moving();
        //        PrintBoard();
        //    }

        //}
        public void Moving()
        {
            Console.Write("Zadej pohyb ve formátu (A2-B3): ");
            int[] move = new int[] { -1 };
            while (move[0] < 0)
            {

                move = InputUser(); // X1,Y1,X2,Y2
                while (move[0] < 0 || move[0] >= 8 || move.Length > 4)
                {
                    Console.Write("I ty prase, zadej ty souřadnice jako člověk: ");
                    move = InputUser();
                }
                int x1 = move[0];
                int y1 = move[1];
                int x2 = move[2];
                int y2 = move[3];
                gameDriver.SetValueOnBoard(x2, y2, board.Coords(x1, y1));
                gameDriver.SetValueOnBoard(x1, y1, 0);

            }
        }
        public int[] InputUser()
        {
            //Vstup uživatele s převedením na malá písmena
            string input = Console.ReadLine().ToLower();
            if (input.Length != 5)
            {
                return new int[] { -1 };
            }

            char x1, y1, x2, y2; // X1Y1 vybrany kamen, X2Y2 kam pohnout
            x1 = input[0];
            int X1 = (int)(x1 - 'a'); //převod v tabulce ASCII
            y1 = input[1];
            int Y1 = (int)(y1 - '1'); //1, protože 0 není v herní desce
            x2 = input[3];
            int X2 = (int)(x2 - 'a');
            y2 = input[4];
            int Y2 = (int)(y2 - '1');
            return new int[] { X1, Y1, X2, Y2 };
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
