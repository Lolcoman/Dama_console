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
            //vytvořime kopii seznamu všech tahů a pak ještě kolekci těch nejlepších tahů, které vrátíme
            List<int[]> nejlepsiTahy = new List<int[]>();
            List<int[]> tahy = new List<int[]>();
            foreach (int[] tah in rules.GetMovesList())
            {
                tahy.Add(tah.Clone() as int[]);
            }
            if (tahy.Count <= 1) //Pokud seznam obsahuje jen jeden, nebo žádný tah, tak se rovnou vrátí. Protože je povinost skákat.
            {
                return tahy;
            }

            /*Vytvoříme smyčku, která projde všechny tahy a zase, jak už to známe, tak je provedeme. 
            Po tom, co data zpracujeme, tak je zase provedeme zpět.
            A tady, úplně na konci, až máme všechno hotové, tak uděláme to poslední rules.MovesGenerate(), protože jsme provedli všechny tahy zpět. 
            Deska je v původním stavu, ale v tom seznamu možných tahů je nějaké poslední hodnota z těch průchodů...
            */

            int hodnotaNejlepsihoTahu = -MAX;

            foreach (int[] tah in tahy)
            {
                board.Move(tah, false, false);
                rules.ChangePlayer();
                rules.MovesGenerate();

                /*Tady bude první volání minimaxu, které nám vrátí nejvýhodnější hodnotu v daném směru. 
                Takže budeme vědět jak moc výhodný je pro nás tento směr. 
                Pokud je je lepší, než ty co už máme, tak je nahradíme. Pokud je stejný, tak ho k nim přidáme. 
                Pokud je horší, tak na to serem a jdem na další z možných tahů...
                Tyto Ify
                */

                int hodnota = -MiniMax(hloubka - 1);
                if (hodnota >= hodnotaNejlepsihoTahu)
                {
                    if (hodnota > hodnotaNejlepsihoTahu)
                    {
                        nejlepsiTahy.Clear();
                        hodnotaNejlepsihoTahu = hodnota;
                    }
                    nejlepsiTahy.Add(tah.Clone() as int[]);
                }

                board.Move(tah, false, true);
                rules.ChangePlayer();
            }
            rules.ChangePlayer();
            return nejlepsiTahy;
        }

        private int MiniMax(int hloubka)
        {
            if (rules.IsGameFinished())
            {
                int minePesak, mineDama, enemyPesak, enemyDama;
                CountStones(out minePesak, out mineDama, out enemyPesak, out enemyDama);

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
                CountStones(out minePesak, out mineDama, out enemyPesak, out enemyDama);
                hodnota = hodnota + minePesak * 2; //vlastní pěšák
                hodnota = hodnota + mineDama * 4; //vlastní dáma
                hodnota = hodnota - enemyPesak * 3; //nepřátelský pěšák
                hodnota = hodnota - enemyDama * 6; //nepřátelská dáma

                return hodnota;
            }

            int ohodnoceni = -MAX;
            List<int[]> tahy = new List<int[]>(); //generování tahů
            foreach (int[] tah in rules.GetMovesList())
            {
                tahy.Add(tah.Clone() as int[]);
            }
            foreach (int[] tah in tahy)
            {
                board.Move(tah, false, false);
                rules.ChangePlayer();
                rules.MovesGenerate();
                ohodnoceni = Math.Max(ohodnoceni, -MiniMax(hloubka - 1));
                board.Move(tah, false, true);
                rules.ChangePlayer();
            }

            if (ohodnoceni > MNOHO)
            {
                ohodnoceni = ohodnoceni - 1;
            }
            if (ohodnoceni < -MNOHO)
            {
                ohodnoceni = ohodnoceni + 1;
            }
            return ohodnoceni;
        }

        private void CountStones(out int minePesak, out int mineDama, out int enemyPesak, out int enemyDama)
        {
            int bilyPesak, bilaDama, cernyPesak, cernaDama;
            board.CountStones(out bilyPesak, out bilaDama, out cernyPesak, out cernaDama);

            minePesak = rules.PlayerOnMove() > 0 ? bilyPesak : cernyPesak;
            mineDama = rules.PlayerOnMove() > 0 ? bilaDama : cernaDama;
            enemyPesak = rules.PlayerOnMove() > 0 ? cernyPesak : bilyPesak;
            enemyDama = rules.PlayerOnMove() > 0 ? cernaDama : bilaDama;
        }
    }
}


