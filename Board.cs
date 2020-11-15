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
        private void MakeMove(int[] move) //{ X1, Y1, S0, S1, X2, Y2, S2, S3 };
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
        /// <summary>
        /// Metoda pro kontrolou zda není X a Y za hranicí pole
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsValidCoordinates(int x, int y)
        {
            if (x > 7)
            {
                return false;
            }
            if (x < 0)
            {
                return false;
            }
            if (y > 7)
            {
                return false;
            }
            if (y < 0)
            {
                return false;
            }
            return true;
        }
        public string ToChar(int[]pohyb) //return new int[] { X1, Y1, S0, S1, X2, Y2, S2, S3 };
        {

            //public int[] FullMove(int[] pohyb)
            //{
            //    int X1 = pohyb[0];
            //    int Y1 = pohyb[1];
            //    int S0 = board.GetValue(pohyb[0], pohyb[1]);
            //    int S1 = 0;
            //    int X2 = pohyb[2];
            //    int Y2 = pohyb[3];
            //    int S2 = 0;
            //    int S3 = board.GetValue(pohyb[0], pohyb[1]);
            //    return new int[] { X1, Y1, S0, S1, X2, Y2, S2, S3 };
            //}
        }
    }
}
