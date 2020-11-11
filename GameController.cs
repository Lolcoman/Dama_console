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
        public void Game()
        {
            rules.InitBoard();
            ui.PrintBoard();

            while (true)
            {
                int[] vstup;
                bool platnyvstup = false;
                while (!platnyvstup)
                {
                    vstup = ui.InputUser();
                    if (vstup[0] >= 0)
                    {
                        platnyvstup = IsCheckMove(vstup);
                    }
                }
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

        public bool IsCheckMove(int[]move)
        {
            if (rules.IsCellEmpty(move[2], move[3]))
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