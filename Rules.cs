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
            //Nelze záporné hodnoty, protože  - - je +
            {0,1}, //nahoru 0
            {1,1}, //diag. doprava 1  
            {1,0}, //doprava 2 
            {1,1}, //dozadu vpravo 3
            {0,1}, //dozadu 4
            {1,1}, //dozadu vlevo 5
            {1,0}, //vlevo 6 
            {1,1}  //diag.vlevo 7 
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

        public void Dama(int X, int Y)
        {
            /*{0,1},  //nahoru 0
            {1,1},  //diag. doprava 1  
            {1,0},  //doprava 2 
            {1,-1}, //dozadu vpravo 3
            {0,-1}, //dozadu 4
            {-1,-1},//dozadu vlevo 5
            {-1,0}, //vlevo 6 
            {-1,1}  //diag.vlevo 7 
            */
            //X=3,Y=1
            int stone = board.GetValue(X, Y);
            int minusY = 0, minusX = 0;

            while (Y != 7)
            {
                if (board.IsValidCoordinates(X + smery[0, 0], Y + smery[0, 1]))
                {
                    if (board.GetValue(X + smery[0, 0], Y + smery[0, 1]) == 0) //rovně 3,2 == {0,1}, nahoru 0
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X, Y + smery[0, 1], 0, stone });
                    }
                }
                minusY = Y++;
            }
            Y -= minusY;

            while (Y != 7 && X != 7)
            {
                if (board.IsValidCoordinates(X + smery[1, 0], Y + smery[1, 1]))
                {
                    if (board.GetValue(X + smery[1, 0], Y + smery[1, 1]) == 0) //diagonálně vpravo 4,2 == {1,1},diag. doprava 1
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X + 1, Y + 1, 0, stone });
                    }
                }
                minusX = X++;
                minusY = Y++;
            }
            Y -= minusY;
            X -= minusX;

            while (X != 7)
            {
                if (board.IsValidCoordinates(X + smery[2, 0], Y + smery[2, 1]))
                {
                    if (board.GetValue(X + smery[2, 0], Y + smery[2, 1]) == 0) //vpravo 4,1 == {1,0}, doprava 2
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X + 1, Y, 0, stone });
                    }
                }
                minusX = X++;
            }
            X -= minusX;

            while (Y != -1) //D2 = [3,1]
            {
                if (board.IsValidCoordinates(X + smery[3, 0], Y - smery[3, 1]))
                {
                    if (board.GetValue(X + smery[3, 0], Y - smery[3, 1]) == 0) //diagonálně vpravo vzad 4,0 == {1,-1}, dozadu vpravo 3
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X + 1, Y - 1, 0, stone });
                    }
                }
                minusX = X++;
                minusY = Y--;
            }
            X -= minusX;
            Y -= minusY;

            while (Y > 0) //D2 = [3,1]
            {
                if (board.IsValidCoordinates(X + smery[4, 0], Y - smery[4, 1]))
                {
                    if (board.GetValue(X + smery[4, 0], Y - smery[4, 1]) == 0) //vzad 3,0 {0,1}, //dozadu 4
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X, Y - 1, 0, stone });
                    }
                }
                minusY = Y--;
            }
            Y -= minusY;

            while (X != -1)
            {
                if (board.IsValidCoordinates(X - smery[5, 0], Y - smery[5, 1]))
                {
                    if (board.GetValue(X - smery[5, 0], Y - smery[5, 1]) == 0) //diagonálně vlevo vzad 2,0 {1,1}, //dozadu vlevo 5
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X - 1, Y - 1, 0, stone });
                    }
                }
                minusX = X--;
                minusY = Y--;
            }
            Y -= minusY;
            X -= minusX;

            while (X != -1)
            {
                if (board.IsValidCoordinates(X - smery[6, 0], Y + smery[6, 1]))
                {
                    if (board.GetValue(X - smery[6, 0], Y + smery[6, 1]) == 0) // vlevo 2,1 {1,0}, //vlevo 6 
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X - 1, Y, 0, stone });
                    }
                }
                minusX = X--;
            }
            X -= minusX;

            while (X != -1)
            {
                if (board.IsValidCoordinates(X - smery[7, 0], Y + smery[7, 1]))
                {
                    if (board.GetValue(X - smery[7, 0], Y + smery[7, 1]) == 0) //diagonálně vlevo 2,2 {1,1}  //diag.vlevo 7 
                    {
                        ListMove.Add(new int[] { X, Y, stone, 0, X - 1, Y + 1, 0, stone });
                    }
                }
                minusX = X--;
                minusY = Y++;
            }
            X -= minusX;
            Y -= minusY;
        }
    }
}
