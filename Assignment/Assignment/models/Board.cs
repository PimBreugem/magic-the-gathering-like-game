using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment
{
    public class Board {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        

        public List<Permanent> GetPermanentsOfPlayer(PlayerType type)
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

        public void ApplyToPermanents(List<int> Ids, PermanentEffect effect){
            foreach(Permanent p in Player1.Permanents.Concat(Player2.Permanents).ToList()){
                if(Ids.Contains(p.Id)){
                    p.AddEffect(effect)
                }
            }
        }   
    }
}