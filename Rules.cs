using System;
using System.Collections.Generic;
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
            board.SetValue(3, 1, 2);

            board.SetValue(2, 2, -1);
            board.SetValue(2, 4, -1);
            board.SetValue(2, 6, -1);

            board.SetValue(4, 2, -1);
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
                if (prvek.Length > 8)
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
            if (player == 1)
            {
                player = player * -1;
            }
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

                    while (board.IsValidCoordinates(toX + smery[indexSmeru, 0], toY + smery[indexSmeru, 1]) && destinationStone == 0)
                    {
                        int nextX = toX + smery[indexSmeru, 0]; //2-1=1
                        int nextY = toY + smery[indexSmeru, 1]; //1-0=1
                        int nextstone = board.GetValue(nextX, nextY);

                        if (board.IsValidCoordinates(nextX,nextY) && nextstone == 0)
                        {
                            Console.WriteLine("Ano");
                        }
                        else
                        {
                            break;
                        }
                    }
                    //else
                    //{
                    //    //tady bude kód pokud narazí na kámen soupeře (možná skok)
                    //    if (destinationStone != stone && destinationStone != 0)
                    //    {
                    //        //Console.WriteLine("|{0}| |{1}| |{2}| |{3}| --> |{4}| |{5}| |{6}| |{7}| ve smeru {8}", fromX, fromY, stone, 0, toX, toY, 0, stone, indexSmeru);
                    //        if (board.GetValue(toX + smery[indexSmeru, 0], toY + smery[indexSmeru, 1]) == 0)
                    //        {
                    //            int[] move = { fromX, fromY, stone, 0, toX, toY, destinationStone, 0, toX + smery[indexSmeru, 0], toY + smery[indexSmeru, 1], 0, stone };
                    //            TryToJump(move, new int[] { });
                    //            //Console.WriteLine("Pole je volné, skok je možný");
                    //            //ListMove.Add(new int[] { fromX, fromY, stone, 0, toX, toY, destinationStone, 0, toX + smery[indexSmeru, 0], toY + smery[indexSmeru, 1], 0, stone });
                    //        }
                    //    }
                    //}
                    break;
                }
            }
        }
        public void TryToJump(int[] move, int[] oldMoves)
        {
            ListMove.Add(move);
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
