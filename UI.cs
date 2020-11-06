using System;
using System.Collections.Generic;
using System.Text;

namespace Damakonzole
{
    class UI
    {
        public Board board = new Board();
        public GameDriver gameDriver = new GameDriver();
        public Rules rules = new Rules();
        public void Start()
        {
            rules.InitBoard();
            board.Print();
        }
        public void Game()
        {
            while (true)
            {
                Move();
                board.Print();
            }

        }

        public void Move()
        {
            Console.WriteLine("Zadej souradnice pro pohyb kamene X:");
            int posX = int.Parse(Console.ReadLine());
            Console.WriteLine("Zadej souradnice pro pohyb kamene Y:");
            int posY = int.Parse(Console.ReadLine());
            Console.WriteLine("Kámen X:{0} Y:{1}",posX,posY);
            Console.WriteLine("Zadej souradnice kam se chcete pohnout:");
            int posX1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Zadej souradnice kam se chcete pohnout:");
            int posY1 = int.Parse(Console.ReadLine());
            gameDriver.SetValueOnBoard(posX1, posY1, board.Coords(posX,posY));
            gameDriver.SetValueOnBoard(posX, posY, 0);
            //board.Coords(posX, posY);
        }
    }
}
