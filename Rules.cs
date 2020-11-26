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
            board.SetValue(3, 1, 1);

            board.SetValue(2, 2, -1);
            board.SetValue(2, 4, -1);
            board.SetValue(2, 6, -1);

            board.SetValue(4, 2, -1);
        }
        /// <summary>
        /// Metoda která ověřuje zda se jedná o platný tah dle pravidel
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool IsCheckMove(int[] move)
        {
            if (PlayerOnMove() < 0 && board.GetValue(move[0], move[1]) > 0) //pokud je na tahu hráč menší než 0, tj černý, tak jestli figurka není větší než 1, tj. bílá
            {
                return false;
            }
            if (PlayerOnMove() > 0 && board.GetValue(move[0], move[1]) < 0) //opak kontroly hráče, zda netáhne bílý, černou
            {
                return false;
            }
            if (board.GetValue(move[2], move[3]) != 0) //zda se netáhne na prázdné pole
            {
                return false;
            }
            if (board.GetValue(move[0], move[1]) == 0) //zda se táhne figurkou
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Metoda která vrací celý kompletní tah
        /// </summary>
        /// <param name="pohyb"></param>
        /// <returns></returns>
        public int[] FullMove(int[] pohyb)
        {
            int X1 = pohyb[0];
            int Y1 = pohyb[1];
            int S0 = board.GetValue(pohyb[0], pohyb[1]);
            int S1 = 0;
            int X2 = pohyb[2];
            int Y2 = pohyb[3];
            int S2 = 0;
            int S3 = board.GetValue(pohyb[0], pohyb[1]);
            return new int[] { X1, Y1, S0, S1, X2, Y2, S2, S3 };
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

        public void GenerateMoveList(int X, int Y) //Generace tahu kamene v rozsahu + 1 poličko, vypis všech jeho tahu
        {
            int stone = board.GetValue(X, Y);
            // X = 3, Y = 1
            if (board.IsValidCoordinates(X + smery[0, 0], Y + smery[0, 1]))
            {
                if (board.GetValue(X+smery[0,0],Y+smery[0,1])==0) //rovně 3,2
                {
                    ListMove.Add(new int[] { X, Y, stone, 0, X, Y + 1, 0, stone });
                }
            }
            if (board.IsValidCoordinates(X + smery[1,0], Y + smery[1,1]))
            {
                if (board.GetValue(X + smery[1, 0], Y + smery[1, 1]) == 0) //diagonálně vpravo 4,2
                {
                    ListMove.Add(new int[] { X, Y, stone, 0, X + 1, Y + 1, 0, stone });
                }
            }
            if (board.IsValidCoordinates(X + smery[2,0], Y + smery[2,1]))
            {
                if (board.GetValue(X + smery[2, 0], Y + smery[2, 1]) == 0) //vpravo 4,1
                {
                    ListMove.Add(new int[] { X, Y, stone, 0, X + 1, Y, 0, stone });
                }
            }
            if (board.IsValidCoordinates(X + smery[3,0], Y - smery[3,1]))
            {
                if (board.GetValue(X + smery[3, 0], Y - smery[3, 1]) == 0) //diagonálně vpravo vzad 4,0
                {
                    ListMove.Add(new int[] { X, Y, stone, 0, X + 1, Y - 1, 0, stone });
                }
            }
            if (board.IsValidCoordinates(X - smery[5,0], Y - smery[5,1]))
            {
                if (board.GetValue(X - smery[5, 0], Y - smery[5, 1]) == 0) //diagonálně vlevo vzad 2,0
                {
                    ListMove.Add(new int[] { X, Y, stone, 0, X - 1, Y - 1, 0, stone });
                }
            }
            if (board.IsValidCoordinates(X - smery[6,0], Y + smery[6,1]))
            {
                if (board.GetValue(X - smery[6, 0], Y + smery[6, 1]) == 0) // vlevo 2,1
                {
                    ListMove.Add(new int[] { X, Y, stone, 0, X - 1, Y, 0, stone });
                }
            }
            if (board.IsValidCoordinates(X - smery[7,0], Y + smery[7,1]))
            {
                if (board.GetValue(X - smery[7, 0], Y + smery[7, 1]) == 0) //diagonálně vlevo 2,2
                {
                    ListMove.Add(new int[] { X, Y, stone, 0, X - 1, Y + 1, 0, stone });
                }
            }
        }

        public void Dama(int fromX, int fromY)
        {
            int i = 1;
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
                    if (destinationStone == 0) //pokud poledopadu je 0 tak true a uloží se do listu tahů
                        ListMove.Add(new int[] { fromX, fromY, stone, 0, toX, toY, 0, stone });
                    else
                    {
                        //tady bude kód pokud narazí na kámen soupeře (možná skok)
                        if (destinationStone != stone && destinationStone != 0)
                        {
                            //Console.WriteLine("Můžeš přeskočit {0} kameny ve smeru {1}.", i, indexSmeru);
                            //i++;

                            //Console.WriteLine("|{0}| |{1}| |{2}| |{3}| --> |{4}| |{5}| |{6}| |{7}| ve smeru {8}", fromX, fromY, stone, 0, toX, toY, 0, stone, indexSmeru);
                            if (board.GetValue(toX + smery[indexSmeru, 0], toY + smery[indexSmeru, 1]) == 0)
                            {
                                //Console.WriteLine("Pole je volné, skok je možný");
                                ListMove.Add(new int[] { fromX, fromY, stone, 0, toX, toY, destinationStone, 0, toX + smery[indexSmeru, 0], toY + smery[indexSmeru, 1], 0, stone });
                            }
                        }
                    }
                }
            }
        }       
    }
}
