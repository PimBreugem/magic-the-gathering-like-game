using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/* 
Jonas de Boer 0983180
Pim Breugem 0992420
 */

namespace Assignment
{
    public class Board {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Stack<Instantaneous> InterruptionStack { get; set; }

        public Board() {

            InterruptionStack = new Stack<Instantaneous>();
        }
    }

    public class LandOnBoard
    {
        public Land Land { get; set; }
        public bool Used { get; set; }

        public LandOnBoard(Land land)
        {
            Land = land;
            Used = true;
        }

        public void Reset() { Used = false; }
        public Dictionary<CardColor, int> UseLand()
        {
            if (!Used)
            {
                Used = true;
                return new Dictionary<CardColor, int> { { Land.Color, 1 } };
            }
            return null;
        }
    }

    public class PermanentOnBoard
    {
        public Permanent Permanent { get; set; }
        public int Defence { get; set; }
        public int Attack { get; set; }
        public List<Effect> AppliedEffects { get; set; }
        public PermanentOnBoard(Permanent permanent)
        {
            Permanent = permanent;
            Attack = permanent.Attack;
            Defence = permanent.Defence;
            AppliedEffects = new List<Effect>();
        }
        public void AddEffect(Effect effect)
        {
            if (effect.PermanentAction != null)
            {
                Permanent perm = new Permanent(Permanent.Name, Permanent.Color, Permanent.Cost, Permanent.Effects, Permanent.ActivationEffect, Defence, Attack);
                effect.PermanentAction(perm);
                Attack = perm.Attack;
                Defence = perm.Defence;
                AppliedEffects.Add(effect);
            }
        }

        public bool RemoveEffectIfExists(Effect effect)
        {
            if (AppliedEffects.Contains(effect))
            {
                AppliedEffects.Remove(effect);
                Permanent perm = new Permanent(Permanent.Name, Permanent.Color, Permanent.Cost, Permanent.Effects, Permanent.ActivationEffect, Permanent.Defence, Permanent.Attack);
                foreach (Effect eff in AppliedEffects)
                {
                    if (eff.PermanentAction != null)
                    {
                        eff.PermanentAction(perm);
                    }
                }
                Attack = perm.Attack;
                Defence = perm.Defence;
                return true;
            }
            return false;
        }
    }
}