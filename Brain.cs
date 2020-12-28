using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class Brain
    {
        const int MNOHO = 96;
        const int MAX = 99;

        private Random random = new Random();
        private Rules rules;
        private Board board;

        public Brain(Board boa, Rules rul)
        {
            board = boa;
            rules = rul;
        }

        public int[] GetRandomMove(List<int[]> moves)
        {
            int count = moves.Count; //Zjistí celkový počet tahů
            int randomIndex = random.Next(count - 1); //Vybere náhodné číslo od 0, nezáporné až po "celkový počet tahů"
            int[] randomMove = moves[randomIndex]; //Zkopíruje konkrétní náhodně vybraný tah a vrátí jej
            return randomMove;
        }

        public int[] GetBestMove(int hloubka)
        {
            return GetRandomMove(GetBestMoves(hloubka));
        }

        private List<int[]> GetBestMoves(int hloubka)
        {
            return null;
        }
        //    else
        //    {
        //        List <int[]> tahy = rules.GetMovesList(); //generování tahů
        //        int[] bestMove = GetRandomMove(moves);
        //        if (rules.PlayerOnMove() == 1)
        //        {
        //            int maxOhodnoceni = -MAX;
        //            foreach (int[] move in moves)
        //            {
        //                board.Move(move,true,false);
        //                int ohodnoceni = MiniMax(hloubka - 1);
        //                rules.ChangePlayer();
        //                board.Move(move, true, true);
        //                if (ohodnoceni > maxOhodnoceni)
        //                {
        //                    maxOhodnoceni = ohodnoceni;
        //                    bestMove = move;
        //                }
        //            }
        //            return bestMove;
        //        }
        //        if (rules.PlayerOnMove() == -1)
        //        {
        //            int minOhodnoceni = MAX;
        //            foreach (int[] move in moves)
        //            {
        //                board.Move(move, true, false);
        //                int ohodnoceni = MiniMax(hloubka - 1);
        //                rules.ChangePlayer();
        //                board.Move(move, true, true);
        //                if (ohodnoceni < minOhodnoceni)
        //                {
        //                    minOhodnoceni = ohodnoceni;
        //                    bestMove = move;
        //                }
        //            }
        //            return bestMove;
        //        }
        //    }
        //    return 0;
        //}
        private int MiniMax(int hloubka)
        {
            if (rules.IsGameFinished())
            {
                int minePesak, mineDama, enemyPesak, enemyDama;
                board.CountStones(out minePesak, out mineDama, out enemyPesak, out enemyDama);

                if (minePesak + mineDama > enemyPesak + enemyDama) //Výhra
                {
                    return MAX;
                }
                if (minePesak + mineDama < enemyPesak + enemyDama) //Prohra
                {
                    return -MAX;
                }
                if (minePesak + mineDama == enemyPesak + enemyDama) //Remíza
                {
                    return 0;
                }
            }

            if (hloubka == 0) //Koncová pozice, ohodnocení pozic
            {
                int hodnota = 0;
                int minePesak, mineDama, enemyPesak, enemyDama;
                board.CountStones(out minePesak, out mineDama, out enemyPesak, out enemyDama);
                hodnota = hodnota + minePesak * 2; //vlastní pěšák
                hodnota = hodnota + mineDama * 4; //vlastní dáma
                hodnota = hodnota - enemyPesak * 3; //nepřátelský pěšák
                hodnota = hodnota - enemyDama * 6; //nepřátelská dáma

                return hodnota;
            }
            else
            {
                int ohodnoceni = -MAX;
                List<int[]> tahy = rules.GetMovesList(); //generování tahů
                foreach (int[] tah in tahy)
                {
                    board.Move(tah,true,false);
                    ohodnoceni = Math.Max(ohodnoceni, -MiniMax(hloubka - 1));
                    return ohodnoceni;
                }
            }
            return 0;
        }
    }
}


