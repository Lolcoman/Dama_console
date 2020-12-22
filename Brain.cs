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
            int randomIndex = random.Next(count); //Vybere náhodné číslo od 0, nezáporné až po "celkový počet tahů"
            int[] randomMove = moves[randomIndex]; //Zkopíruje konkrétní náhodně vybraný tah a vrátí jej
            return new int[] { };
        }
    }
}
