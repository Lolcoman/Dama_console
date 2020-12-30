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

        //proměnné hráčů, pro uživatele 0, 1-4 obtížnost PC
        private int player1 = 0;
        private int player2 = 0;

        int kolo = 0; //Počítadlo kol

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
            rules.InitBoard(); //inicializace desky
            ui.SelectPlayer(out player1,out player2); //výběr hráče na tahu
            rules.InitPlayer(); //inicializace hráče na tahu
            rules.MovesGenerate(); //vygenerování všech tahů pro aktuálního hráče tj. 1-bílý

            while (!rules.IsGameFinished()) //cyklus dokud platí že oba hráči mají figurky, jinak konec
            {
                Console.Clear();    
                ui.PrintBoard();

                //Tahy počítače
                if (rules.PlayerOnMove() == 1 && player1 > 0) //pokud hráč na tahu je 1 a player1 > 0 tak true, provede tah a continue na dalšího hráče
                {
                    ui.PocetKol(kolo); //vypis počtu kol
                    int[] move = brain.GetBestMove(player1); //tah se vybere pomocí GetBestMove
                    board.Move(move, true, false); //provedení pohybu

                    kolo++; //přičtení do počítadla kol

                    rules.ChangePlayer(); 
                    rules.MovesGenerate();
                    //Thread.Sleep(2000);
                    continue;
                }
                
                if (rules.PlayerOnMove() == -1 && player2 > 0) //pokud hráč na tahu je -1 a player2 > 0 tak true, provede tah a continue
                {
                    ui.PocetKol(kolo);

                    int[] move = brain.GetBestMove(player2);
                    board.Move(move, true, false);
                    rules.ChangePlayer();
                    rules.MovesGenerate();
                    //Thread.Sleep(2000);
                    continue;
                }


                //Tahy Hráče
                int[] vstup = null;
                int[] plnyVstup = null;
                bool platnyVstup = false;

                while (!platnyVstup) //Dokud je vstup !playtnyVstup tak pokračuje
                {
                    ui.PocetKol(kolo);
                    vstup = ui.InputUser(rules.PlayerOnMove()); //pokud -1 tak se podmínka neprovede protože -1 >= 0, pokud 0 tak se provede 0=0 a zkontroluje se platnost tahu
   
                    if (vstup[0] == -2) //Pokud hráč do konzole zadá HELP
                    {
                        if (vstup.Length > 1) //Pokud ještě zadá pro jakou figurku chce help
                        {
                            ui.PrintHelpMove(rules.GetMovesList(vstup[1], vstup[2])); //pro zadanou figurku
                        }
                        else //Vypíše všechny možné tahy hráče na tahu
                        {
                            ui.PrintHelpMove(rules.GetMovesList()); //všechny možné tahy hráče
                        }
                    }
                    if (vstup[0] >= 0) //pokud je zadán správný pohyb tj A2-B3
                    {
                        plnyVstup = rules.FullMove(vstup); //převedení na kompletní pohyb který se skládá ze 4 souřadnic X,Y, stav před, stav po

                        platnyVstup = plnyVstup[0] != -1; //ověření zda je táhnuto dle pravidel, typ bool ve while cyklu

                        if (!platnyVstup) //pokud není vypíše uživately chybu
                        {
                            ui.Mistake(); //chyba
                        }
                    }
                }
                board.Move(plnyVstup, true, false); //pokud je zadáno správně, metoda nastaví pohyb na desce

                if (rules.PlayerOnMove() < 0 && player1 < 0 || player2 > 0) //Počítadlo kol, zvýší pokaždé když je na tahu černý, tj. bílý pak černý = 1 kolo!
                {
                    kolo++;
                }

                if (rules.ListMove.Count == 0) //pokud je ListMove prázdnej tak se změní hráč na tahu a vygenerují se pro něj nové možné tahy
                {
                    rules.ChangePlayer();
                    rules.MovesGenerate();
                }
                else //pokud v listu stále je možnost, tak pokračuje hráč, vícenásobné skoky
                {
                    continue;
                }
            }
            ui.PrintBoard();
        }
        /// <summary>
        /// Metoda pro nastavení hodnoty políčka
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