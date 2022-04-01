using System;
using System.IO;

namespace Assignment
{
    public class Board {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        

        public Board()
        {
            Player1 = new Player("Jonas", PlayerType.ONE);
            Player2 = new Player("Pim", PlayerType.TWO);
        }

    }
}