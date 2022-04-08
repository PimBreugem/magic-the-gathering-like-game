using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    public class Board {
        /*
        originalPermanents = [ permx(4,4, pl1), permy(3,5,pl2)  ]
        permeffects = [ 
            {ref(permx), Effect(att-1 && def-1)},
            {ref(permy), Effect(att+1 && def-4)}
        ]
        shownPermanents = [ perm(3,3,pl1), perm(4,1,pl2) ]

        */
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Dictionary<Permanent,Effect> PermanentEffects { get; set; }
        public Dictionary<Player, Effect> PlayerEffects { get; set; }
        public Dictionary<Land, Effect> LandEffects { get; set; }
        public Stack<Instantaneous> InterruptionStack { get; set; }

        public Board() {
            PermanentEffects = new Dictionary<Permanent, Effect>();
            PlayerEffects = new Dictionary<Player,Effect>();
            LandEffects = new Dictionary<Land,Effect>();
            InterruptionStack = new Stack<Instantaneous>();
        }

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

        public void ApplyToPermanents(List<Permanent> permanents, Effect effect){
            foreach(Permanent p in permanents){
                PermanentEffects.Add(p,effect);
            }
        }   
    }
}