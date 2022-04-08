using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
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
        int Id { get; set; }
        string Name { get; set; }
        CardColor Color { get; set; }
        PlayerType player { get; set; }
    }

    public class Land: ICard
    {
        // Card properties
        public int Id { get; set; }
        public string Name { get; set; }
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

    public interface ISpell : ICard
    {
       // CardColor color { get; set; }
       // void Play();
        int Cost { get; set; }
    }


    public interface IEffect<T>
    {
        int Duration { get; set; }
        Func<T, T> Action { get; set; }

        T Apply(T p);
    }


    public class PlayerEffect : IEffect<Player>
    {
        public int Duration { get; set; }
        public Func<Player,Player> Action { get; set; }

        public PlayerEffect (int dur, Func<Player, Player> act)
        {
            Duration = dur;
            Action = act;
        }

        public Player Apply(Player p){
            return Action(p);
        }
    }

    public class PermanentEffect : IEffect<Permanent>
    {
        public int Duration { get; set; }
        public Func<Permanent,Permanent> Action { get; set; }

        public PermanentEffect (int dur, Func<Permanent, Permanent> act)
        {
            Duration = dur;
            Action = act;
        }

        public Permanent Apply(Permanent p){
            return Action(p);
        }
    }

    public class Permanent : ISpell
    {
        // Card properties
        public int Id { get; set; }
        public CardColor Color { get; set; }
        public PlayerType player { get; set; }

        // Spell properties
        public int Cost { get; set; }
        public IEffect Effect { get; set; }

        // Personal properties
        public List<PermanentEffect> appliedEffects { get; set; }
        public string Name { get; set; } 
        public int Defence { get; set; }
        public int Attack { get; set; }

        public Permanent (string name, int def, int att)
        {
            Name = name;
            Defence = def;
            Attack = att;
        }

        public void AddEffect(PermanentEffect effect) {
            appliedEffects.Add(effect);
        }

    }

    public class Instantaneous : ISpell
    { 
        // Card properties
        public int Id { get; set; }
        public string Name 
        public CardColor Color { get; set; }
        public PlayerType player { get; set; }
        // Spell properties
        public int Cost { get; set; }
        public IEffect Effect { get; set; }
    }

}
