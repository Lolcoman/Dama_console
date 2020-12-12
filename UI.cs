﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class UI
    {
        private Board board;
        public UI(Board bo)
        {
            board = bo;
        }
        public int[] InputUser(int playerOnMove) //příjmá int, který hráč je na tahu
        {
            Console.Write("{0}. Zadej pohyb ve formátu (A2-B3) nebo 'help' pro nápovědu: ",playerOnMove > 0 ? "BÍLÝ s o/O" : "ČERNÝ s x/X"); //použití ternárního operátoru pro informaci pro uživatele který hráč je na tahu, pokud > 0 tak hraje bílý
            //Vstup uživatele s převedením na malá písmena
            //Špatný vstup vrácena -1, Správný vstup vráceno pole {X1,Y1,X2,Y2}
            //Ověření správnosti provádní až třída GAME CONTROLLER  

            string input = Console.ReadLine().ToLower();

            if (input == "help") //Pro nápovědu všech možných tahů!
            {
                Console.Write("Zadej pro jaký kámen chceš nápovědu nebo 'all' pro všechny tahy:");
                string help = Console.ReadLine().ToLower();
                if (help.Length == 0)
                {
                    return new int[] { -1 };
                }
                if (help == "all")
                {
                    return new int[] { -2 };
                }

                //Kontrola správného zadání
                char helpX = help[0];
                int helpX1 = (int)(helpX - 'a');
                char helpY = help[1];
                int helpY1 = (int)(helpY - '1');
                if (board.IsValidCoordinates(helpX1,helpY1))
                {
                    return new int[] { -2, helpX1, helpY1 };
                }

                return new int[] { -1 };
                //return new int[] { -2, helpX1, helpY1 };
            }

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
            return Parsing(X1, Y1, X2, Y2);
        }

        /// <summary>
        /// Vykreslení desky
        /// </summary>
        public void PrintBoard()
        {
            //Console.Clear();
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
                    Console.Write(" {0} ", set);
                }
                Console.Write("║ {0}\n", y + 1); //výpis souřadnic vpravo
            }
            Console.WriteLine("   ╚════════════════════════╝");
            Console.WriteLine("     A  B  C  D  E  F  G  H");
        }

        /// <summary>
        /// Metoda pro tisk chyby
        /// </summary>
        public void Mistake()
        {
            Console.WriteLine("Špatně zadáno!");
        }

        public int[] Parsing(int X1, int Y1, int X2, int Y2)
        {
            if (X1 > 7 || X1 < 0) //pokud X1 je větší než 7 a zároveň X1 je menší než 0 tak vrať -1
            {
                return new int[] { -1 };
            }
            if (Y1 > 7 || Y1 < 0) //pokud Y1 je větší než 7 a zároveň Y1 je menší než 0 tak vrať -1
            {
                return new int[] { -1 };
            }
            if (X2 > 7 || X2 < 0) //pokud X2 je větší než 7 a zároveň X2 je menší než 0 tak vrať -1
            {
                return new int[] { -1 };
            }
            if (Y2 > 7 || Y2 < 0) //pokud Y2 je větší než 7 a zároveň Y2 je menší než 0 tak vrať -1
            {
                return new int[] { -1 };
            }
            return new int[] { X1, Y1, X2, Y2 };
        }
        public void PrintHelpMove(List<int[]> seznam)
        {
            if (seznam.Count == 0)
            {
                Console.WriteLine("Prázdný seznam tahů!");
            }
            foreach (int[] prvek in seznam)
            {
                Console.WriteLine(board.PohybNaString(prvek));
            }   
        }

    }
}
