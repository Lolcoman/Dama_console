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
    }
}
