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
        /// Metoda pro získaní hodnot z pole board
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public int GetValue(int posX, int posY)
        {
            return board[posX, posY];
        }
        /// <summary>
        /// Metoda pro nastavení hodnoty v poli
        /// </summary>
        /// <param name="move"></param>
        public void MakeMove(int[] move) //{ X1, Y1, S0, S1, X2, Y2, S2, S3 };
        {
            SetValue(move[0], move[1], move[3]);
        }
        /// <summary>
        /// Metoda která prodeve nastavení hodnot z plného vstupu
        /// </summary>
        /// <param name="kompletniPohyb"></param>
        public void Move(int[]kompletniPohyb)
        {
            for (int i = 0; i < kompletniPohyb.Length; i = i + 4)
            {
                MakeMove(new int[] { kompletniPohyb[i], kompletniPohyb[i + 1], kompletniPohyb[i + 2], kompletniPohyb[i + 3] });
            }
        }
    }
}
