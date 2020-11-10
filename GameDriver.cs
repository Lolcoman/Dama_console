using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class GameDriver
    {
        private Board board = new Board();
        private Rules rules;
        private UI ui;
        public GameDriver()
        {
            rules = new Rules(board);
            ui = new UI(board);
        }

        public void Game()
        {
            rules.InitBoard();
            ui.PrintBoard();

            while (true)
            {
                ui.Moving();
                Console.Clear();
                ui.PrintBoard();
            }

        }
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
