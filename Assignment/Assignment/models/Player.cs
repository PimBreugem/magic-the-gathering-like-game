using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment
{
    public class Player : ITarget
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public int PlayerHealth { get; set; }
        public Dictionary<CardColor,int> Energy { get; set; }
        public List<ICard> Hand { get; set; }
        public Stack<ICard> Deck { get; set; }
        public Stack<ICard> DiscardPile { get; set; }
        public List<Permanent> Permanents { get; set; }
        public List<Land> Lands { get; set; }
        public List<Effect> AppliedEffects { get; set; }

        public Player(string name, PlayerType type, int health, Stack<ICard> deck){
            Name = name;
            Type = type;
            Energy = new Dictionary<CardColor,int>();
            PlayerHealth = health;
            Deck = deck;
            Hand = new List<ICard>();
            DiscardPile = new Stack<ICard>();
            Permanents = new List<Permanent>();
            Lands = new List<Land>();
            AppliedEffects = new List<Effect>();
            Draw(7);
        }

        public Player(
                string name, 
                PlayerType type, 
                Dictionary<CardColor,int> energy, 
                int health, 
                List<ICard> deck, 
                List<ICard> hand, 
                Stack<ICard> discardPile, 
                List<Permanent> permanents,
                List<Land> lands){
            Name = name;
            Type = type;
            Energy = energy;
            PlayerHealth = health;
            Deck = deck;
            Hand = hand;
            DiscardPile = discardPile;
            Permanents = permanents;
            Lands = lands;
            AppliedEffects = new List<Effect>();
        }

        public void Draw(int num=1) {
            Print($"Draws {(num == 1 ? "a" : num.ToString())} card{(num == 1 ? "" : "s")}");
            while(num-- > 0 && Deck.Count > 0)
            {
                Hand.Add(Deck.Pop());
            }
        }

        public void Play(ICard card) {
            if(card is Land) {
                PlayLand((Land)card);
            }
            else if(card is Instantaneous)
            {
                PlayInstantaneous((Instantaneous)card);
            }
            else { 
                PlayPermanent((Permanent)card);
            }
            Hand.Remove(card);
        }

        public void ResetLands(){

            foreach(Land l in Lands)
            {
                l.Reset();
            }
        }

        private void PlayLand(Land land) {
            Print($"plays a {land.Color} Land");
            land.Used = true;
            Lands.Add(land);
        }

        private void SubtractEnergy(Dictionary<CardColor,int> cost) 
        {
            foreach(KeyValuePair<CardColor, int> cv in cost){
                Energy[cv.Key] -= cv.Value;
            }
        }

        private void PlayInstantaneous(Instantaneous instantaneous, List<ITarget> targets)
        {
            //add effects
            Print($"plays a [{instantaneous.Color} instant] {instantaneous.Name}");

            SubtractEnergy(instantaneous.Cost);

            foreach(ITarget t in targets) {
                t.AddEffect(instantaneous.ActivationEffect);
            }

            DiscardPile.Push(instantaneous);
        }

        private void PlayPermanent(Permanent permanent){ 
            Print($"plays a [{permanent.Color} permanent] {permanent.Name}");

            SubtractEnergy(permanent.Cost);
            Permanents.Add(permanent);
        }
        private void UseLand(Land land) {
            if(Energy.ContainsKey(land.Color)){
                Energy[land.Color] += land.Use();
            } else {
                Energy.Add(land.Color,land.Use());
            }
        }

        public Player WithEffects() 
        {
            Player res = new Player(Name,Type,Energy,Health,Deck,Hand,DiscardPile,Permanents,Lands);
            foreach(Effect e in AppliedEffects){
                res = e.PlayerAction(res);
            }
            return res;
        }


        public void PrintPlayerState(){
            Player withEffects = this.WithEffects();
            Print("");
            Print($"Cards: [Deck:${Deck.Count}, Hand:${Hand.Count}, DiscardPile:${DiscardPile.Count}]");
            Print($"Permanents: (name,attack,defense)");
            foreach(Permanent p in Permanents){
                Print($"    ({p.Name},{p.Attack},{p.Defence})");
            }
            Print($"Unused Lands: {withEffects.Lands.Count(l => !l.Used)}");
        }

        public bool EnoughEnergy(Dictionary<CardColor,int> cost) {
            Dictionary<CardColor,int> possibleEnergy = new Dictionary<CardColor, int>();

            foreach(Land l in Lands){
                if(possibleEnergy.ContainsKey(l.Color)){
                    possibleEnergy[l.Color]++;
                } else {
                    possibleEnergy.Add(l.Color,1);
                }
            }
            foreach(KeyValuePair<CardColor,int> cv in cost){
                if(!possibleEnergy.ContainsKey(cv.Key) || possibleEnergy[cv.Key] < cv.Value) {
                    return false;
                }
            }
            return true;
        }

        public void AddEffect(Effect effect){
            AppliedEffects.Add(effect);
        }

        public void Print(string s)
        {
            int playerNo = Type == PlayerType.ONE ? 1 : 2;
            Console.WriteLine($"[Player {playerNo}, {Name}] {s}");
        }
    }

    public enum PlayerType {
        ONE,
        TWO
    }
}