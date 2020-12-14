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
        private void MakeMove(int[] move, bool tahZpet) //{ X1, Y1, S0, S1, X2, Y2, S2, S3 };
        {
            if (tahZpet)
            {
                SetValue(move[0], move[1], move[2]);
            }
            else
            {
                SetValue(move[0], move[1], move[3]);
            }
        }
        /// <summary>
        /// Metoda která prodeve nastavení hodnot z plného vstupu
        /// </summary>
        /// <param name="kompletniPohyb"></param>
        public void Move(int[] kompletniPohyb,bool ulozitDoHistorie, bool tahZpet)
        {
            if (tahZpet)
            {
                for (int i = kompletniPohyb.Length-4; i > 0 ; i = i - 4)
                {
                    MakeMove(new int[] { kompletniPohyb[i], kompletniPohyb[i + 1], kompletniPohyb[i + 2], kompletniPohyb[i + 3] }, tahZpet);
                }
            }
            else
            {
                for (int i = 0; i < kompletniPohyb.Length; i = i + 4)
                {
                    MakeMove(new int[] { kompletniPohyb[i], kompletniPohyb[i + 1], kompletniPohyb[i + 2], kompletniPohyb[i + 3] }, tahZpet);
                }
            }
            if (ulozitDoHistorie)
            {
                //Uložení tahu
            }

        }

        /// <summary>
        /// Metoda tahu vpřed
        /// </summary>
        /// <param name="kompletniPohyb"></param>
        /// <param name="rozliseni"></param>
        public void ForwardMove(int[] kompletniPohyb, int rozliseni)
        {
            if (rozliseni == 1)
            {
                for (int i = 0; i < kompletniPohyb.Length; i = i + 4)
                {
                    MakeMove(new int[] { kompletniPohyb[i], kompletniPohyb[i + 1], kompletniPohyb[i + 2], kompletniPohyb[i + 3] });
                }
            }
            else
            {
                for (int i = 0; i < kompletniPohyb.Length; i = i + 4)
                {
                    MakeMove(new int[] { kompletniPohyb[i], kompletniPohyb[i + 1], kompletniPohyb[i + 2], kompletniPohyb[i + 3] });
                }
            }
        }

        /// <summary>
        /// Metoda pro tah zpět
        /// </summary>
        /// <param name="kompletniPohyb"></param>
        public void BackMove(int[] kompletniPohyb)
        {
            for (int i = kompletniPohyb.Length; i > 0; i = i - 4)
            {
                MakeMove(new int[] { kompletniPohyb[i], kompletniPohyb[i - 1], kompletniPohyb[i - 2], kompletniPohyb[i - 3] });
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
            string vystup = "|";

            for (int i = 0; i < pohyb.Length; i = i + 4)
            {   
                //                     "| [a2] " " > o"             "|"        {1} =[0+0]= 0 + 97= 'a'  {2} =[1+0]= 1 + 1= 2        {3} =[2+0]= 0= " "   {4} =[3+0]= 1= "o"
                //                     "| [b3] " " > o"             "|"        {1} =[0+4]= 1 + 97= 'b'  {2} =[1+4]= 2 + 1= 3        {3} =[2+4]= 0= " ",  {4} =[3+4]= 1= "o"
                vystup = String.Format("{0} [{1}{2}] {3} --> {4}", vystup, (char)(pohyb[0 + i] + 'a'), pohyb[1 + i] + 1, StoneToString(pohyb[2 + i]), StoneToString(pohyb[3 + i]));
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
