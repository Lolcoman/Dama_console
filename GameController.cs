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

        /// <summary>
        /// Hlavní herní smyčka
        /// </summary>
        public void Game()
        {
            rules.InitBoard();
            rules.InitPlayer();
            rules.MovesGenerate();

            while (true)
            {
                ui.PrintBoard();
                int[] vstup = null;
                int[] plnyVstup = null;
                bool platnyVstup = false;
                while (!platnyVstup)
                {
                    vstup = ui.InputUser(rules.PlayerOnMove()); //pokud -1 tak se podmínka neprovede protože -1 >= 0, pokud 0 tak se provede 0=0 a zkontroluje se platnost tahu

                    if (vstup[0] == -2)
                    {
                        if (vstup.Length > 1)
                        {
                            ui.PrintHelpMove(rules.GetMovesList(vstup[1], vstup[2]));
                        }
                        else
                        {
                            ui.PrintHelpMove(rules.GetMovesList());
                        }
                    }
                    if (vstup[0] >= 0) //pokud je zadán správný pohyb tj A2-B3
                    {
                        plnyVstup = rules.FullMove(vstup);

                        platnyVstup = plnyVstup[0] != -1; //ověření zda je táhnuto dle pravidel
                        if (!platnyVstup) //pokud není vypíše uživately chybu
                        {
                            ui.Mistake(); //chyba
                        }
                    }
                }
                board.Move(plnyVstup,true,false); //pokud je zadáno správně, metoda nastaví pohyb na desce
                if (rules.ListMove.Count == 0) //pokud je ListMove prázdné tak se změní hráč na tahu a vygenerují se pro něj nové možné tahy
                {
                    rules.ChangePlayer();
                    rules.MovesGenerate();
                }
                else
                {
                    continue;
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
    }
}