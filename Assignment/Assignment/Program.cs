using System;
using System.Collections.Generic;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ICard> deck = new List<ICard>();
            for(int i = 0; i < 10; i++) { deck.Add(new Land("Jungle", CardColor.GREEN)); }
            for (int i = 0; i < 10; i++) { deck.Add(new Land("Plains", CardColor.WHITE)); }
            for (int i = 0; i < 3; i++) { deck.Add(new Permanent("Lion", CardColor.WHITE, 2, null, null, 2, 3)); }
            for (int i = 0; i < 3; i++) { deck.Add(new Permanent("Monkey", CardColor.GREEN, 2, null, null, 3, 3)); }
            for (int i = 0; i < 3; i++) { deck.Add(new Permanent("Cow", CardColor.WHITE, 2, null, null, 0, 5)); }
            for (int i = 0; i < 3; i++) { deck.Add(new Permanent("Snake", CardColor.GREEN, 2, null, null, 4, 2)); }
            for (int i = 0; i < 3; i++) { deck.Add(new Permanent("Spider", CardColor.GREEN, 2, null, null, 2, 2)); }
            for (int i = 0; i < 2; i++) { deck.Add(new Permanent("Giraffe", CardColor.WHITE, 2, null, null, 3, 2)); }
            GameLogic game = new GameLogic("Pim", "Jonas", 10, deck, deck);

        }
    }
}
