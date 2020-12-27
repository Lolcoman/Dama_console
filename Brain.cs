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

        private int MiniMax(int hloubka, int[] pozice)
        {
            return 0;
        }

        private int Ohodnot(int hloubka)
        {
            //Ohodnocení výhry, prohry, remízy
            if (rules.IsGameFinished())
            {
                int bilyPesak, bilaDama, cernyPesak, cernaDama;
                board.CountStones(out bilyPesak, out bilaDama, out cernyPesak, out cernaDama);

                if (bilyPesak + bilaDama > cernyPesak + cernaDama) //Výhra
                {
                    return MAX;
                }
                if (bilyPesak + bilaDama < cernyPesak + cernaDama) //Prohra
                {
                    return -MAX;
                }
                if (bilyPesak + bilaDama == cernyPesak + cernaDama) //Remíza
                {
                    return 0;
                }
            }
            //Dosažení maximální hloubky, ohodnotí se koncové pozice
            if (hloubka == 0)
            {
                int hodnota = 0;
                int bilyPesak, bilaDama, cernyPesak, cernaDama;
                board.CountStones(out bilyPesak, out bilaDama, out cernyPesak, out cernaDama);
                hodnota = bilyPesak * 2;
                hodnota = bilaDama * 4;
                hodnota = cernyPesak * 3;
                hodnota = cernaDama * 6;

                return hodnota;
            }
            return 0;
        }
    }
}
