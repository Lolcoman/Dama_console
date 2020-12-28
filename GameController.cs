using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Damakonzole
{
    class GameController
    {
        private Board board = new Board();
        private Rules rules;
        private UI ui;
        private Brain brain;

        private int player1 = 0;
        private int player2 = 0;

        //private int bilyPesak = 0;
        //private int cernyPesak = 0;
        //private int bilaDama = 0;
        //private int cernaDama = 0;

        public GameController()
        {
            rules = new Rules(board);
            ui = new UI(board);
            brain = new Brain(board, rules);
        }

        /// <summary>
        /// Hlavní herní smyčka
        /// </summary>
        public void Game()
        {
            rules.InitBoard();
            ui.SelectPlayer(out player1,out player2);
            rules.InitPlayer();
            rules.MovesGenerate();

            while (!rules.IsGameFinished())
            {
                Console.Clear();    
                ui.PrintBoard();

                //Tahy počítače
                if (rules.PlayerOnMove() == 1 && player1 > 0)
                {
                    //int[] move = brain.GetRandomMove(rules.GetMovesList());
                    int[] move = brain.GetBestMove(player1);
                    board.Move(move, true, false);
                    rules.ChangePlayer();
                    rules.MovesGenerate();
                    Thread.Sleep(1000);
                    continue;
                }
                
                if (rules.PlayerOnMove() == -1 && player2 > 0)
                {
                    //int[] move = brain.GetRandomMove(rules.GetMovesList());
                    int[] move = brain.GetBestMove(player2);
                    board.Move(move, true, false);
                    rules.ChangePlayer();
                    rules.MovesGenerate();
                    Thread.Sleep(1000);
                    continue;
                }

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
                board.Move(plnyVstup, true, false); //pokud je zadáno správně, metoda nastaví pohyb na desce
                if (rules.ListMove.Count == 0) //pokud je ListMove prázdné tak se změní hráč na tahu a vygenerují se pro něj nové možné tahy
                {
                    rules.ChangePlayer();
                    rules.MovesGenerate();
                }
                else
                {
                    continue;
                }
                //rules.Win();
            }
            ui.PrintBoard();
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