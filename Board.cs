using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class Board
    {
        static int[,] board = new int[8, 8];

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
        public void Move(int[] kompletniPohyb)
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
        public string PohybNaString(int[] pohyb) //return new int[] { X1, Y1, S0, S1, X2, Y2, S2, S3 };
                                                 //int[] move1 = new int[] { 0, 1, 0, 1, 1, 2, 0, 1 };
                                                                        // { 0, 1, 2, 3, 4, 5, 6, 7 }
        {
            //int x1, y1, x2, y2;
            //x1 = pohyb[0];
            //char X1 = (char)(x1 + '0');
            //y1 = pohyb[1];
            //char Y1 = (char)(y1 + '0');
            //x2 = pohyb[4];
            //char X2 = (char)(x2 + '0');
            //y2 = pohyb[5];
            //char Y2 = (char)(y2 + '0');

            string vystup = "|";

            for (int i = 0; i < pohyb.Length; i = i + 4)
            {   
                //                     "| [a2] " " > o"             "|"        {1} =[0+0]= 0 + 97= 'a'  {2} =[1+0]= 1 + 1= 2        {3} =[2+0]= 0= " "   {4} =[3+0]= 1= "o"
                //                     "| [b3] " " > o"             "|"        {1} =[0+4]= 1 + 97= 'b'  {2} =[1+4]= 2 + 1= 3        {3} =[2+4]= 0= " ",  {4} =[3+4]= 1= "o"
                vystup = String.Format("{0} [{1}{2}] {3} > {4}", vystup, (char)(pohyb[0 + i] + 'a'), pohyb[1 + i] + 1, StoneToString(pohyb[2 + i]), StoneToString(pohyb[3 + i]));
            }   
            return vystup;
        }
        private string StoneToString(int stone)
        {
            switch (stone)
            {
                case -2:
                    return "X";
                case -1:
                    return "x";
                case 1:
                    return "o";
                case 2:
                    return "O";
                default:
                    return " ";
            }
        }
    }
}
