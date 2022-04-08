using System;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Effect> effects = new List<Effect>{
                new Effect(2,permanent => 
                    Permanent(
                        permanent.Name,
                        permanent.Color,
                        permanent.Effects,
                        permanent.ActivationEffect,
                        permanent.Defence-2,
                        permanent.Attack,
                        permanent.CanDefend,
                        permanent.AppliedEffects
                    ))
            };
            List<ICard> deck1 = new List<ICard>
            {
                new Land("Red Land 1",CardColor.Red),
                new Land("Red Land 2",CardColor.Red),
                new Land("Red Land 3",CardColor.Red),
                new Land("Red Land 4",CardColor.Red),
                new Land("Blue Land 1",CardColor.Blue),
                new Land("Blue Land 2",CardColor.Blue),
                new Land("Blue Land 3",CardColor.Blue),
                new Land("Blue Land 4",CardColor.Blue),
                new Instantaneous("Bolt",CardColor.Blue,new Dictionary<CardColor,int>{{CardColor.Blue,2}})
                
                
                
            };

        }
    }
}
