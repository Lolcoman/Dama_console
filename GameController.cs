using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class GameController
    {
        private Board board = new Board();
        private Rules rules;
        private UI ui;
        public GameController()
        {
            rules = new Rules(board);
            ui = new UI(board);
        }

        public int[] vstup;
        bool any = true;
        public void Game()
        {
            rules.InitBoard();
            ui.PrintBoard();

            while (true)
            {
                vstup = ui.InputUser();
                any = CheckMove();
                if (any)
                {
                    ui.Moving();
                }
                else
                {
                    vstup = ui.InputUser();
                }
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

        public bool CheckMove()
        {
            //Console.WriteLine(vstup[0]);
            //Console.WriteLine(vstup[1]);
            //Console.WriteLine(vstup[2]);
            //Console.WriteLine(vstup[3]);
            if (rules.IsCellEmpty(vstup[2], vstup[3]))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Pole není volné!");
                return false;
            }
        }
    }
}
