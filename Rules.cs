using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class Rules
    {
        private Board board;
        public Rules(Board bo)
        {
            board = bo;
        }
        /// <summary>
        /// Metoda pro sestaví desky dle pravidel dámy
        /// </summary>
        public void InitBoard()
        {
            for (int posY = 0; posY < 8; posY++)
            {
                for (int posX = 0; posX < 8; posX++)
                {
                    if (posY <= 1)
                    {
                        board.SetValue(posX, posY, 1);
                    }
                    else if (posY >= 6)
                    {
                        board.SetValue(posX, posY, -1);
                    }
                    else
                    {
                        board.SetValue(posX, posY, 0);
                    }
                }
            }
        }
        /// <summary>
        /// Metoda která ověřuje zda se jedná o platný tah dle pravidel
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool IsCheckMove(int[] move)
        {
            if (PlayerOnMove() < 0 && board.GetValue(move[0], move[1]) > 0) //pokud je na tahu hráč menší než 0, tj černý, tak jestli figurka není větší než 1, tj. bílá
            {
                return false;
            }
            if (PlayerOnMove() > 0 && board.GetValue(move[0], move[1]) < 0) //opak kontroly hráče, zda netáhne bílý, černou
            {
                return false;
            }
            if (board.GetValue(move[2], move[3]) != 0) //zda se netáhne na prázdné pole
            {
                return false;
            }
            if (board.GetValue(move[0], move[1]) == 0) //zda se táhne figurkou
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Metoda která vrací celý kompletní tah
        /// </summary>
        /// <param name="pohyb"></param>
        /// <returns></returns>
        public int[] FullMove(int[] pohyb)
        {
            int X1 = pohyb[0];
            int Y1 = pohyb[1];
            int S0 = board.GetValue(pohyb[0], pohyb[1]);
            int S1 = 0;
            int X2 = pohyb[2];
            int Y2 = pohyb[3];
            int S2 = 0;
            int S3 = board.GetValue(pohyb[0], pohyb[1]);
            return new int[] { X1, Y1, S0, S1, X2, Y2, S2, S3 };
        }

        //privátní proměnná hráče
        private int player;

        /// <summary>
        /// Inicializace hráče na tahu
        /// </summary>
        public void InitPlayer()
        {
            player = 1;
        }
        /// <summary>
        /// Metoda pro zjištění hráče na tahu
        /// </summary>
        /// <returns>hodnota</returns>
        public int PlayerOnMove()
        {
            return player;
        }

        public void ChangePlayer()
        {
            if (player == 1)
            {
                player = player * -1;
            }
            //else if (player == -1)
            //{
            //    player = 1;
            //}
        }
    }
}
