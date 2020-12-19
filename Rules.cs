using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damakonzole
{
    class Rules
    {
        private Board board;

        public List<int[]> ListMove = new List<int[]>();
        private int[,] smery =
        {
            {-1,0}, //vlevo 0
            {-1,1},  //diag.vlevo 1
            {0,1},  //nahoru 2
            {1,1},  //diag. doprava 3
            {1,0},  //doprava 4
            {1,-1}, //dozadu vpravo 5
            {0,-1}, //dozadu 6
            {-1,-1},//dozadu vlevo 7

        };


        //privátní proměnná hráče
        private int player;

        public Rules(Board bo)
        {
            board = bo;
        }
        /// <summary>
        /// Metoda pro sestaví desky dle pravidel dámy
        /// </summary>
        public void InitBoard()
        {
            //for (int posY = 0; posY < 8; posY++)
            //{
            //    for (int posX = 0; posX < 8; posX++)
            //    {
            //        if (posY <= 1)
            //        {
            //            board.SetValue(posX, posY, 1);
            //        }
            //        else if (posY >= 6)
            //        {
            //            board.SetValue(posX, posY, -1);
            //        }
            //        else
            //        {
            //            board.SetValue(posX, posY, 0);
            //        }
            //    }
            //}

            board.SetValue(2, 0, 1);
            board.SetValue(3, 0, 1);

            board.SetValue(1, 1, -1);
            board.SetValue(1, 3, -1);
            board.SetValue(3, 1, -1);
            board.SetValue(3, 3, -1);
            board.SetValue(5, 2 , -1);
        }
        /// <summary>
        /// Metoda která vrací celý kompletní tah a porovná tah v seznamuTahu
        /// </summary>
        /// <param name="pohyb"></param>
        /// <returns></returns>
        public int[] FullMove(int[] pohyb)
        {
            int X1 = pohyb[0];
            int Y1 = pohyb[1];
            int X2 = pohyb[2];
            int Y2 = pohyb[3];
            foreach (int[] prvek in ListMove)
            {
                if (prvek.Length == 8) //Je to tah!
                {
                    if (X1==prvek[0] && Y1==prvek[1] && X2==prvek[4] && Y2==prvek[5]) //porovnání pokud jde jen o tah
                    {
                        return prvek;
                    }
                }
                if (prvek.Length > 8) //Je to skok
                {
                    if (X1 == prvek[0] && Y1 == prvek[1] && X2 == prvek[8] && Y2 == prvek[9]) //porovnání pokud se jedná o skok
                    {
                        return prvek;
                    }
                }
            }
            return new int[] { -1 };
        }

        /// <summary>
        /// Inicializace hráče na tahu
        /// </summary>
        public void InitPlayer()
        {
            player = 1;
        }
        /// <summary>
        /// Metoda pro zjištění hráče na tahu
        /// </summary>
        /// <returns>hodnota</returns>
        public int PlayerOnMove()
        {
            return player;
        }
        /// <summary>
        /// Meotda pro změnu hráče
        /// </summary>
        public void ChangePlayer()
        {
            player = player * -1;
        }

        /// <summary>
        /// Vytvoření tahů
        /// </summary>
        /// <param name="fromX"></param>
        /// <param name="fromY"></param>
        public void TryToMove(int fromX, int fromY)
        {
            int stone = board.GetValue(fromX, fromY); //3,1 = 1

            //X=3,Y=1
            for (int indexSmeru = 0; indexSmeru <= 7; indexSmeru++)
            {
                int hloubka = 0; //proměná hloubky vypisu tahu, kvůli kamenu
                int toX = fromX; //fromX = 3, tj. toX = 3
                int toY = fromY; //fromY = 1, tj. toY = 1

                while (board.IsValidCoordinates(toX + smery[indexSmeru, 0], toY + smery[indexSmeru, 1])) //[3,2] je true
                {
                    hloubka = hloubka + 1;
                    if ((stone == -1 || stone == 1)&& hloubka > 1) //pokud je tah pěšák černý, nebo bílý a hloubka je větší než 1, tak se smyčka přeruší
                    {
                        break;
                    }

                    if (stone == -1 && indexSmeru >= 1 && indexSmeru <= 3) //pro černého
                    {
                        break;
                    }

                    if (stone == 1 && indexSmeru >= 5 && indexSmeru <= 7) //otočení pro bílého
                    {
                        break;
                    }

                    //VYPIS
                    toX = toX + smery[indexSmeru, 0]; //3+-1=2, první se začíná vlevo
                    toY = toY + smery[indexSmeru, 1]; //1+0=1
                    //toX = 2
                    //toY = 1
                    int destinationStone = board.GetValue(toX, toY); //pole kam dopadne kamen

                    if (stone < 0 && destinationStone < 0) //pokud hodnota kamene je menší než 0 a pole dopadu menší než 0, tak true a ukončí se, kontrola svého kamene 
                        break;
                    if (stone > 0 && destinationStone > 0) //pokud hodnota kamene je větší než 0 a pole dopadu větší než 0, tak true a ukončí se, kontrola svého kamene 
                        break;
                    if (destinationStone == 0) //pokud pole dopadu je 0 tak true a uloží se do listu tahů
                    {
                        ListMove.Add(new int[] { fromX, fromY, stone, 0, toX, toY, 0, stone });
                    }

                    //Skoky
                    else
                    {
                        if ((stone > 0 && destinationStone > 0) || (stone < 0 && destinationStone < 0))
                        {
                            break;
                        }
                        //toX = 3, toY = 1
                        int nextX = toX; //nové proměnné pro další souřadnice
                        int nextY = toY;
                        int hloubkaSkoku = 0;
                        while (board.IsValidCoordinates(nextX + smery[indexSmeru, 0], nextY + smery[indexSmeru, 1])) //ověření platnosti
                        {
                            hloubkaSkoku = hloubkaSkoku + 1;
                            if ((stone == -1 || stone == 1) && hloubkaSkoku > 1) //pokud se jedná o pěšáka tak pouze o jedno pole za něj ve směru
                            {
                                break;
                            }

                            //souřadnice pro daný směr
                            nextX = nextX + smery[indexSmeru, 0]; //2-1=1
                            nextY = nextY + smery[indexSmeru, 1]; //1-0=1 
                            int nextstone = board.GetValue(nextX, nextY); //hodnota následujícího pole 

                            if (nextstone != 0) //pokud není prázdné tak break cyklu
                            {
                                break;
                            }

                            int[] skok = { fromX, fromY, stone, 0, toX, toY, destinationStone, 0, nextX, nextY, 0, stone };
                            TryToJump(skok, new int[] { });
                        }
                        break;
                    }
                }
            }
        }
        //C4-D5-E6 = |2|3|1|0|-|3|4|-1|0|-|4|5|0|1|
        //E6-D6-C6 = |4|5|1|0|-|3|5|-1|0|-|2|5|0|1|
        //int[] skok = { fromX, fromY, stone, 0, toX, toY, destinationStone, 0, nextX, nextY, 0, stone };
        //C4-D5-E6 {0,1,2,3,4,5,6,7,8,9,10,11}
        public void TryToJump(int[] move, int[] oldMove)
        {
            board.Move(move, false, false);
            int stone = board.GetValue(move[8], move[9]);
            int fromX = move[8];
            int fromY = move[9];

            for (int indexSmeru = 0; indexSmeru <= 7; indexSmeru++)
            {
                int hloubka = 0;
                int thruX = fromX;
                int thruY = fromY;
                while (board.IsValidCoordinates(thruX + smery[indexSmeru, 0], thruY + smery[indexSmeru, 1])) //Prohledávání polí kolem výchozího kamene
                {

                    //OMEZENÍ NA KAMENY
                    hloubka = hloubka + 1;
                    if ((stone == -1 || stone == 1) && hloubka > 1) //pokud je tah pěšák černý, nebo bílý a hloubka je větší než 1, tak se smyčka přeruší
                    {
                        break;
                    }
                    if (stone == -1 && indexSmeru >= 1 && indexSmeru <= 3) //pro černého
                    {
                        break;
                    }
                    if (stone == 1 && indexSmeru >= 5 && indexSmeru <= 7) //otočení pro bílého
                    {
                        break;
                    }

                    thruX = thruX + smery[indexSmeru, 0];
                    thruY = thruY + smery[indexSmeru, 1];
                    int thruStone = board.GetValue(thruX, thruY);

                    if (thruStone == 0) //podmínka na prázdné pole, které nehledáme
                    {
                        break;
                    }

                    if ((stone > 0 && thruStone > 0) || (stone < 0 && thruStone < 0)) //podmínka na vlastní kámen, který nemůže být přeskočen
                    {
                        break;
                    }
                    int hloubkaSkoku = 0;
                    int destX = thruX;
                    int destY = thruY;

                    while (board.IsValidCoordinates(destX + smery[indexSmeru, 0], destY + smery[indexSmeru, 1]))
                    {
                        hloubkaSkoku = hloubkaSkoku + 1;
                        if ((stone == -1 || stone == 1) && hloubkaSkoku > 1) //pokud se jedná o pěšáka tak pouze o jedno pole za něj ve směru
                        {
                            break;
                        }

                        destX = destX + smery[indexSmeru, 0];
                        destY = destY + smery[indexSmeru, 1];
                        int destinationStone = board.GetValue(destX, destY);

                        if (destinationStone != 0) //pokud pole není rovno 0 tak break
                        { 
                            break;
                        }

                        //pokud vše je OK, tak se uloží do listu
                        int[] skok = { fromX, fromY, stone, 0, thruX, thruY, thruStone, 0, destX, destY, 0, stone };

                        //Do newOldMove vložíme kopii oldMove
                        int[] newOldMove = (int[])oldMove.Clone();
                        newOldMove = newOldMove.Concat(move).ToArray();

                        TryToJump(skok, newOldMove);
                    }
                    break;
                }
            }
            ListMove.Add(oldMove.Concat(move).ToArray());
            board.Move(move, false, true);
        }

        /// <summary>       
        /// Genereuje seznam tahu 
        /// </summary>
        public void MovesGenerate()
        {
            ListMove.Clear();
            for (int posY = 0; posY < 8; posY++)
            {
                for (int posX = 0; posX < 8; posX++)
                {
                    if ((board.GetValue(posX,posY) < 0 && PlayerOnMove() < 0) || (board.GetValue(posX,posY) > 0 && PlayerOnMove() > 0))
                    {
                        TryToMove(posX, posY);
                    }
                }
            }
            //Pravidlo na výběr nejdelšího tahu/skoku
            int maxDelka = 0;
            for (int i = 0; i < ListMove.Count; i++) //Cyklus který projede celý list a najde největší prvek
            {
                //Console.WriteLine(ListMove[i].Length);

                if (ListMove[i].Length > maxDelka) //Pokud je delká větší než maxDelka tak se přiřadí do proměnné
                {
                    maxDelka = ListMove[i].Length; //Přiřazení
                }    
            }
            for (int i = ListMove.Count - 1; i >= 0; i--) //Cyklus, který porovná všechny prvky v poli a smaže ty co jsou menší než maxDelka, ListMove.Count=6, pokaždé co se provede odstranění se musí počet prvků snížit o jeden, protože se smazal 
            {
                if (ListMove[i].Length < maxDelka)
                {
                    ListMove.RemoveAt(i);
                }
            }

            //Smyčka pro povýšení kamene na dámu, pokud dosáhne konce desky
            foreach (int[] move in ListMove)
            {
                int delka = move.Length;
                if (move[delka - 1] == 1 && move[delka - 3] == 7)
                {
                    move[delka - 1] = 2;
                }
                if (move[delka - 1] == -1 && move[delka - 3] == 0)
                {
                    move[delka - 1] = -2;
                }
            }
        }
        /// <summary>
        /// Vrátí všechny tahy
        /// </summary>
        /// <returns></returns>
        public List<int[]> GetMovesList()
        {
            return GetMovesList(-1, -1);
        }
        /// <summary>
        /// Vrátí pouze tahy vybrané figurky
        /// </summary>
        /// <param name="filterX"></param>
        /// <param name="filterY"></param>
        /// <returns></returns>
        public List<int[]> GetMovesList(int filterX, int filterY)
        {
            List<int[]> NovySeznamTahu = new List<int[]>();
            foreach (int[] pohyb in ListMove)
            {
                if (filterX == -1)
                {
                    NovySeznamTahu.Add((int[])pohyb.Clone());
                }
                else
                {
                    if (pohyb[0] == filterX && pohyb[1] == filterY)
                    {
                        NovySeznamTahu.Add((int[])pohyb.Clone());
                    }
                }
            }
            return NovySeznamTahu;
        }
    }
}
