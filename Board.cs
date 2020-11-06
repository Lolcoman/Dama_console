using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class Board
    {   
        static int[,] board = new int[8,8];

        /// <summary>
        /// Metoda nastaví hodnotu na zvolených souřadnicích
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="hodnota"></param>
        public void SetValue(int posX, int posY, int value)
        {
            board[posX, posY] = value;
        }

        /// <summary>
        /// Vykreslení desky
        /// </summary>
        public void Print()
        {
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 7; x++)
                {
                    Console.Write(" " + board[x, y] + " ");
                }
                Console.WriteLine("\n");
            }
        }
        public int Coords(int posX, int posY)
        {
            int a;
            a = board[posX, posY];
            return a;
        }
    }
}
