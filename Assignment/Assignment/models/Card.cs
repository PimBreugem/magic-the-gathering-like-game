using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

/* 
Jonas de Boer 0983180
Pim Breugem 0992420
 */

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
    }

    public interface ISpell : ICard
    {
        Dictionary<CardColor, int> Cost { get; set; }
        Effect ActivationEffect { get; set; }

    }

    public class Permanent : ISpell
    {
        public string Name { get; set; }
        public CardColor Color { get; set; }
        public Dictionary<CardColor, int> Cost { get; set; }
        public List<Effect> Effects { get; set; }
        public Effect ActivationEffect { get; set; }
        public int Defence { get; set; }
        public int Attack { get; set; }
        public Permanent (string name, CardColor color, Dictionary<CardColor, int> cost, List<Effect> effect, Effect activationEffect, int def, int att)
        {
            Name = name;
            Color = color;
            Cost = cost;
            Effects = effect;
            ActivationEffect = activationEffect;
            Defence = def;
            Attack = att;
        }
    }

    public class Instantaneous : ISpell
    { 
        public string Name { get; set; }
        public CardColor Color { get; set; }
        public Dictionary<CardColor, int> Cost { get; set; }
        public Effect ActivationEffect { get; set; }

        public Instantaneous(string name, CardColor color, Dictionary<CardColor, int> cost, Effect activationEffect) 
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
        public bool TargetOtherPlayer { get; set;}
        public Action<Permanent> PermanentAction { get; set; }
        public Action<Player> PlayerAction { get; set; }
        public Action<Land> LandAction { get; set; }

        public Effect(int? duration, Action<Permanent> permanentAction, bool targetOtherPlayer = false)
        {
            Duration = duration;
            PermanentAction = permanentAction;
            TargetOtherPlayer = targetOtherPlayer;
        }
    
        public Effect(int? duration, Action<Player> playerAction, bool targetOtherPlayer = false)
        {
            Duration = duration;
            PlayerAction = playerAction;
            TargetOtherPlayer = targetOtherPlayer;
        }

        public Effect(int? duration, Action<Land> landAction, bool targetOtherPlayer = false)
        {
            Duration = duration;
            LandAction = landAction;
            TargetOtherPlayer = targetOtherPlayer;
        }
    
    }

}
