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
        List<ICard> Hand { get; set; }
        Stack<ICard> Deck { get; set; }
        Stack<ICard> DiscardPile { get; set; }
        List<Permanent> Permanents { get; set; }
        List<Land> Lands { get; set; }

        public Player(string name, PlayerType type){
            Name = name;
            Type = type;
            Energy = 0;
        }

        public void Draw() {
            Hand.Add(Deck.Pop());
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

            Hand.Remove(card);
        }

        private void PlayLand(Land land) {
            land.Used = true;
            Lands.Add(land);
        }

        private void PlayInstantaneous(Instantaneous instantaneous)
        {
            //add effects

            DiscardPile.Push(instantaneous);
        }

        private void PlayPermanent(Permanent permanent){ 
            
            Permanents.add(permanent);
        }

        public void UseLand(int id) {
            foreach(Land l in Lands) {
                if(l.id == id) {
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