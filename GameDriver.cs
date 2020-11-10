using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class GameDriver
    {
        public Board board = new Board();
        Rules rules;
        UI ui;


        public GameDriver()
        {
            rules = new Rules(this);
            ui = new UI(this);

        }
        public void Game()
        {
            rules.InitBoard();
            ui.PrintBoard();

            while (true)
            {
                ui.Moving();
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
