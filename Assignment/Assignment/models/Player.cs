using System;
using System.Collections;
using System.Collections.Generic;

/* 
Jonas de Boer 098318
Pim Breugem 0992420
 */

namespace Assignment
{

    public class Player {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public int PlayerHealth { get; set; }
        public Dictionary<CardColor, int> EnergyReserve { get; set; }
        public List<ICard> Hand { get; set; }
        public Stack<ICard> Deck { get; set; }
        public Stack<ICard> DiscardPile { get; set; }
        public List<PermanentOnBoard> Permanents { get; set; }
        public List<LandOnBoard> Lands { get; set; }

        public Player(string name, PlayerType type, int health, Stack<ICard> deck){
            Name = name;
            Type = type;
            EnergyReserve = new Dictionary<CardColor, int>();
            PlayerHealth = health;
            Deck = deck;
            Hand = new List<ICard>();
            DiscardPile = new Stack<ICard>();
            Permanents = new List<PermanentOnBoard>();
            Lands = new List<LandOnBoard>();
            DrawCard(7);
        }

        public bool DrawCard(int num=1) {
            Console.WriteLine($"Player {Name}: Draws {(num == 1 ? "a" : num.ToString())} card{(num == 1 ? "" : "s")}");
            while(num-- > 0)
            {
                if (Deck.Count == 0) { return false; }
                Hand.Add(Deck.Pop());
            }
            return true;
        }

        public void PlayCard(ICard card) {
            Console.WriteLine("playing card");
            
            if(card is Land land) {
                Lands.Add(new LandOnBoard(land));

            }

            else if(card is Instantaneous instantaneous)
            {
                // do something
                DiscardPile.Push(instantaneous);
            }
            else {
                // permanent
                Permanents.Add(new PermanentOnBoard((Permanent)card));
            }
            Hand.Remove(card);

        }
    }

    public enum PlayerType {
        ONE,
        TWO
    }
}