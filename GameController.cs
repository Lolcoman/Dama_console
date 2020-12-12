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
                bool platnyVstup = false;
                rules.MovesGenerate();
                while (!platnyVstup)
                {
                    //rules.MovesGenerate();
                    vstup = ui.InputUser(rules.PlayerOnMove()); //pokud -1 tak se podmínka neprovede protože -1 >= 0, pokud 0 tak se provede 0=0 a zkontroluje se platnost tahu

                    if (vstup[0] == -2)
                    {
                        //if (vstup.Length > 1)
                        //{
                        //    rules.ListMove.Clear();
                        //    if ((board.GetValue(vstup[1], vstup[2]) < 0 && rules.PlayerOnMove() < 0) || (board.GetValue(vstup[1], vstup[2]) > 0 && rules.PlayerOnMove() > 0))
                        //    {
                        //        rules.Dama(vstup[1], vstup[2]);
                        //    }
                        //    else
                        //    {
                        //        ui.Mistake();
                        //    }
                        //}
                        foreach (int[] prvek in rules.ListMove)
                        {
                            ui.PrintHelpMove(rules.ListMove);
                        }

                        //TEST-vypsání tahu jako inty, pak smazat!!
                        //foreach (int[] pohyb in rules.ListMove)
                        //{
                        //    for (int i = 0; i < pohyb.Length; i++)
                        //    {
                        //        if (i/1 == 0)
                        //        {
                        //            Console.WriteLine();
                        //        }
                        //        Console.Write(pohyb[i]);
                        //    }
                        //}
                        //Console.WriteLine();

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

                Console.Write("plnyVstup: |");
                for (int i = 0; i < plnyVstup.Length; i++)
                {
                    Console.Write("{0}|",plnyVstup[i]);
                }
                Console.Write("\n");

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