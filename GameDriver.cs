using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class GameDriver
    {
        public Board board = new Board();
        /// <summary>
        /// Nastavení hodnoty políčka
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="hodnota"></param>
        public void SetValueOnBoard(int posX, int posY, int value)
        {
            board.SetValue(posX, posY, value);
        }
    }
}
