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
        public void Input(int[] move)
        {
            int x1 = move[0];
            int y1 = move[1];
            int x2 = move[2];
            int y2 = move[3];
            SetValue(x2, y2, GetValue(x1, y1));
            SetValue(x1, y1, 0);
        }
    }
}
