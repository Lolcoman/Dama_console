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

            while (true)
            {
                ui.PrintBoard();
                int[] vstup = null;
                int[] plnyVstup;
                List<int[]> vystup;
                bool platnyVstup = false;
                while (!platnyVstup)
                {
                    vstup = ui.InputUser(rules.PlayerOnMove()); //pokud -1 tak se podmínka neprovede protože -1 >= 0, pokud 0 tak se provede 0=0 a zkontroluje se platnost tahu

                    vystup = rules.GenerujSeznamTahu(vstup[0], vstup[1]);
                    //board.PohybNaString(vystup);
                    Console.WriteLine(board.PohybNaString(vystup.ToArray()));


                    if (vstup[0] < 0)
                    {
                        int[] move1 = vstup;
                        //Console.WriteLine("Move1 Krok: \t{0}", board.PohybNaString(move1));
                    }
                    if (vstup[0] >= 0) //pokud je zadán správný pohyb tj A2-B3
                    {
                        platnyVstup = rules.IsCheckMove(vstup); //ověření zda je táhnuto dle pravidel
                        if (!platnyVstup) //pokud není vypíše uživately chybu
                        {
                            ui.Mistake(); //chyba
                        }
                    }
                }
                plnyVstup = rules.FullMove(vstup);
                Console.WriteLine(board.PohybNaString(plnyVstup));
                board.Move(plnyVstup); //pokud je zadáno správně, metoda nastaví pohyb na desce
                //rules.ChangePlayer(); //změna hráče na tahu
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