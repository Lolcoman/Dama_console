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
        /// Metoda která ověří zda je dané pole prázdné pro pohyb
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public bool IsCellEmpty(int posX, int posY) //A2-B3 0,1 - 1,2
        {
            if (board.GetValue(posX, posY) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
