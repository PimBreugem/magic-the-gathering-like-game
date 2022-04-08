using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment
{
    public class Player {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public int PlayerHealth { get; set; }
        public int Energy { get; set; }
        public List<ICard> Hand { get; set; }
        public Stack<ICard> Deck { get; set; }
        public Stack<ICard> DiscardPile { get; set; }
        public List<Permanent> Permanents { get; set; }
        public List<Land> Lands { get; set; }
        public Board BoardRef { get; set; }

        public Player(string name, PlayerType type, int health, Stack<ICard> deck, Board board){
            Name = name;
            Type = type;
            Energy = 0;
            PlayerHealth = health;
            Deck = deck;
            Hand = new List<ICard>();
            DiscardPile = new Stack<ICard>();
            Permanents = new List<Permanent>();
            Lands = new List<Land>();
            BoardRef = board;
            Draw(7);
        }

        public void Draw(int num=1) {
            Console.WriteLine($"Player {Name}: Draws {(num == 1 ? "a" : num.ToString())} card{(num == 1 ? "" : "s")}");
            while(num-- > 0 && Deck.Count > 0)
            {
                Hand.Add(Deck.Pop());
            }
        }

        public void Play(ICard card) {
            Console.WriteLine("playing card");
            
            if(card is Land) {
                PlayLand((Land)card);
            }
            else if(card is Instantaneous)
            {
                PlayInstantaneous((Instantaneous)card);
            }
            else { 
                // permanent
            }
                    break;

            Hand.Remove(card);
        }

        private void PlayLand(Land land) {
            land.Used = true;
            Lands.Add(land);
        }

        private void PlayInstantaneous(Instantaneous instantaneous)
        {
            //add effects
            var otherPermanents = BoardRef.GetPermanentsOfPlayer(type == PlayerType.ONE ? PlayerType.TWO : PlayerType.ONE)

            DiscardPile.Push(instantaneous);
        }

        private void PlayPermanent(Permanent permanent){ 
            
            Permanents.Add(permanent);
        }

        public void UseLand(int id) {
            foreach(Land l in Lands) {
                if(l.Id == id) {
                    Energy += l.Use();
                }
            }
        }
    }

    public enum PlayerType {
        ONE,
        TWO
    }
}