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

            while (true)
            {
                ui.PrintBoard();
                int[] vstup = null;
                bool platnyVstup = false;
                while (!platnyVstup)
                {
                    vstup = ui.InputUser();
                    if (vstup[0] >= 0)
                    {
                        platnyVstup = rules.IsCheckMove(vstup);
                        if (!platnyVstup)
                        {
                            ui.Mistake();
                        }
                    }
                }
                board.Input(vstup);
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