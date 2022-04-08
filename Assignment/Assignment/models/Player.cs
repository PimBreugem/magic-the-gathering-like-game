using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment
{
    public class PermanentOnField
    {
        public Effect[] AppliedEffects { get; set; }
        public Permanent Permanent { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public PermanentOnField(Permanent permanent)
        {
            Permanent = permanent;
            Attack = permanent.Attack;
            Defence = permanent.Defence;
        }
    }
    public class Player {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public int PlayerHealth { get; set; }
        public Dictionary<CardColor, int> EnergyReserve { get; set; }
        public List<ICard> Hand { get; set; }
        public Stack<ICard> Deck { get; set; }
        public Stack<ICard> DiscardPile { get; set; }
        public List<PermanentOnField> Permanents { get; set; }
        public List<Land> Lands { get; set; }
        public Board BoardRef { get; set; }

        public Player(string name, PlayerType type, int health, Stack<ICard> deck, Board board){
            Name = name;
            Type = type;
            EnergyReserve = new Dictionary<CardColor, int>();
            PlayerHealth = health;
            Deck = deck;
            Hand = new List<ICard>();
            DiscardPile = new Stack<ICard>();
            Permanents = new List<PermanentOnField>();
            Lands = new List<Land>();
            BoardRef = board;
            DrawCard(7);
        }

        public void DrawCard(int num=1) {
            Console.WriteLine($"Player {Name}: Draws {(num == 1 ? "a" : num.ToString())} card{(num == 1 ? "" : "s")}");
            while(num-- > 0 && Deck.Count > 0)
            {
                Hand.Add(Deck.Pop());
            }
        }

        public void PlayCard(ICard card) {
            Console.WriteLine("playing card");
            
            if(card is Land land) {
                // Cannot use land if it is played first time
                land.Use();
                Lands.Add(land);

            }

            else if(card is Instantaneous instantaneous)
            {
                // do something
                DiscardPile.Push(instantaneous);
            }
            else {
                // permanent
                Permanents.Add(new PermanentOnField((Permanent)card));
            }
            Hand.Remove(card);

        }

        private void PlayPermanent(Permanent permanent){

            Permanents.Add(new PermanentOnField(permanent));
        }

        /* public void UseLand() {
            foreach(Land l in Lands) {
                if(l.Id == id) {
                    Energy += l.Use();
                }
            }
        } */
    }

    public enum PlayerType {
        ONE,
        TWO
    }
}