using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    public class Board {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Stack<Instantaneous> InterruptionStack { get; set; }

        public Board() {

            InterruptionStack = new Stack<Instantaneous>();
        }

        public List<PermanentOnField> GetPermanentsOfPlayer(PlayerType type)
        {
            return type switch
            {
                PlayerType.ONE => Player1.Permanents,
                PlayerType.TWO => Player2.Permanents,
                _ => null
            };
        }

        public Player GetPlayer(PlayerType type)
        {
            return type switch
            {
                PlayerType.ONE => Player1,
                PlayerType.TWO => Player2,
                _ => null
            };
        }

        public void ApplyToPermanents(List<Permanent> permanents, Effect effect){
            foreach(Permanent p in permanents){
                
            }
        }   
    }
}