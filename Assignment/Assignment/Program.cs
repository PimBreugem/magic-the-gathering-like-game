using System;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Permanent[] entities = {
                new Permanent("Aap",5,5),
                new Permanent("Beer",10,3),
                new Permanent("Leeuw",5,14)
            };

            Console.WriteLine(entities[0].Defence.ToString());

            Effect x = new Effect(
                1,
                entities,
                e => new Permanent(e.Name, e.Defence - 2, e.Attack)
            );
            x.apply();

            entities = x.Targets;
            
            Console.WriteLine(entities[0].Defence.ToString());
        }
    }
}
