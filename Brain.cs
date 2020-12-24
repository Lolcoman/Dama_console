using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class Brain
    {
        private Random random = new Random();
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
            if (hloubka == 0)
            {

            }
            pozice = (rules.GetMovesList(pozice[1], pozice[2])); //Vygeneruje všechny možné tahy z této pozice
            return 0;
        }
    }
}
