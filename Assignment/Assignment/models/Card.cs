using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assignment
{
    public enum CardColor {
        RED,
        WHITE,
        GREEN,
        BLUE,
        BROWN,
        NONE
    }
    public interface ICard
    {
        public int Id { get; set; }
        public CardColor Color { get; set; }
        public PlayerType player { get; set; }
    }

    class Land: ICard
    {
        // Card properties
        public int Id { get; set; }
        public CardColor Color { get; set; }
        public PlayerType player { get; set; }

        // Personal properties
        public bool Used { get; set; }

        public int Use() {
            if(!Used) {
                Used = true;
                return 1;
            }
            return 0;
        }

        public void Reset() {
            Used = false;
        }
    }

    interface ISpell : ICard
    {
       // CardColor color { get; set; }
       // void Play();
        int Cost { get; set; }
        Effect Effect { get; set; }
    }


    class Effect
    {
        public int Duration { get; set; }
        public Permanent[] Targets { get; set; }
        public Func<Permanent, Permanent> EntityAction { get; set; }

        public Effect (int dur, Permanent[] tar, Func<Permanent, Permanent> act)
        {
            Duration = dur;
            Targets = tar;
            EntityAction = act;
        }

        public void apply() {
            Permanent[] newEntities = new Permanent[Targets.Length];
            int i = 0;
            foreach(Permanent e in Targets){
                newEntities[i] = EntityAction(e);
            }
            Targets = newEntities;
        }
    }

    class Permanent : ISpell
    {
        // Card properties
        public int Id { get; set; }
        public CardColor Color { get; set; }
        public PlayerType player { get; set; }

        // Spell properties
        public int Cost { get; set; }
        public Effect Effect { get; set; }

        // Personal properties
        public List<Effect> appliedEffects { get; set; }
        public string Name { get; set; } 
        public int Defence { get; set; }
        public int Attack { get; set; }

        public Permanent (string name, int def, int att)
        {
            Name = name;
            Defence = def;
            Attack = att;
        }

        public void AddEffect(Effect effect) {
            appliedEffects.Add(effect);
        }

    }

    class Instantaneous : ISpell 
    {
        // Card properties
        public int Id { get; set; }
        public CardColor Color { get; set; }
        public PlayerType player { get; set; }
        // Spell properties
        public int Cost { get; set; }
        public Effect Effect { get; set; }
    }

}
