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
        BROWN
    }

    public interface ICard
    {
        string Name { get; set; }
        CardColor Color { get; set; }
    }

    public class Land: ICard
    {
        public string Name { get; set; }
        public CardColor Color { get; set; }
        public List<Effect> AppliedEffects { get; set; }

        public Land(string name, CardColor color) 
        {
            Name = name;
            Color = color;
        }
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
        int Cost { get; set; }

    }

    public class Permanent : ISpell
    {
        public string Name { get; set; }
        public CardColor Color { get; set; }
        public int Cost { get; set; }
        public List<Effect> Effects { get; set; }
        public Effect ActivationEffect { get; set; }
        public int Defence { get; set; }
        public int Attack { get; set; }
        public bool CanDefend { get; set; }

        public Permanent (string name, CardColor color, int cost, List<Effect> effect, Effect activationEffect, int def, int att)
        {
            Name = name;
            Color = color;
            Cost = cost;
            Effects = effect;
            ActivationEffect = activationEffect;
            Defence = def;
            Attack = att;
            CanDefend = true;
        }
    }

    public class Instantaneous : ISpell
    { 
        public string Name { get; set; }
        public CardColor Color { get; set; }
        public int Cost { get; set; }
        public Effect ActivationEffect { get; set; }

        public Instantaneous(string name, CardColor color, int cost, Effect activationEffect) 
        {
            Name = name;
            Color = color;
            Cost = cost;
            ActivationEffect = activationEffect;
        }

    }


    public class Effect
    {
        public int? Duration { get; set; }
        public Boolean TargetOtherPlayer { get; set;}
        public Func<Permanent> PermanentAction { get; set; }
        public Func<Player> PlayerAction { get; set; }
        public Func<Land> LandAction { get; set; }

        public Effect(int duration, Func<Permanent> permanentAction, Boolean targetOtherPlayer = false)
        {
            Duration = duration;
            PermanentAction = permanentAction;
            TargetOtherPlayer = targetOtherPlayer;
        }
    
        public Effect(int duration, Func<Player> playerAction, Boolean targetOtherPlayer = false)
        {
            Duration = duration;
            PlayerAction = playerAction;
            TargetOtherPlayer = targetOtherPlayer;
        }

        public Effect(int duration, Func<Land> landAction, Boolean targetOtherPlayer = false)
        {
            Duration = duration;
            LandAction = landAction;
            TargetOtherPlayer = targetOtherPlayer;
        }
    
    }

}
