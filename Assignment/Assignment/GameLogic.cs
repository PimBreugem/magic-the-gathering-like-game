using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
	public class GameLogic
	{
		public Player PlayerOne { get; set; }
		public Player PlayerTwo { get; set; }
		public GameLogic(string playerOneName, string playerTwoName, int startingHealth, List<ICard> playerOneDeck, List<ICard> playerTwoDeck)
		{
			if(CheckDeckRules(playerOneName, playerOneDeck) && CheckDeckRules(playerTwoName, playerTwoDeck)){
				Board board = new Board();

				// Shuffeling and drawing starting hand
				Console.WriteLine("Decks are checked, shuffeling cards...");
				var random = new Random();
				Stack<ICard> ShuffeledPlayerOneDeck = new Stack<ICard>(playerOneDeck.OrderBy(item => random.Next()).ToList());
				Stack<ICard> ShuffeledPlayerTwoDeck = new Stack<ICard>(playerTwoDeck.OrderBy(item => random.Next()).ToList());
				

				// Starting the game
				Console.WriteLine("Decks are shuffeld, initializing the game...");
				PlayerOne = new Player(playerOneName, PlayerType.ONE, startingHealth, ShuffeledPlayerOneDeck, board);
				PlayerTwo = new Player(playerTwoName, PlayerType.TWO, startingHealth, ShuffeledPlayerTwoDeck, board);
				board.Player1 = PlayerOne;
				board.Player2 = PlayerTwo;

			}
		}

		public void PlayerTurn(PlayerType playerType)
        {
			Player CurrentPlayer = (PlayerOne.Type == playerType ? PlayerOne : PlayerTwo);

			// Preparation
			foreach(Land land in CurrentPlayer.Lands) { land.Reset(); } //Reset all lands
			foreach(PermanentOnField permanentOnField in CurrentPlayer.Permanents) // Reset all the the states of permanents
			{
				permanentOnField.Attack = permanentOnField.Permanent.Attack;
				permanentOnField.Defence = permanentOnField.Permanent.Defence;
				permanentOnField.AppliedEffects = null;
				permanentOnField.Permanent.CanDefend = true;

				// Check all effects
				foreach(Effect effect in permanentOnField.Permanent.Effects) // Check if all the permanent's effects are still valid: use case can be that when a permanent spaws the first 3 turns it has a buff
                {
					if(effect.Duration != null || effect.Duration-- <= 0)
                    {
						permanentOnField.Permanent.Effects.Remove(effect);
                    }
                }
			}
			foreach(PermanentOnField permanentOnField in CurrentPlayer.Permanents) // Add all the effects of each permanent to the field
            {
				foreach(Effect effect in permanentOnField.Permanent.Effects)
                {
					if(effect.PermanentAction != null)
                    {
						// Run effect on all permanent in the field including the other players

                    }
                }
            }


        }

		public Boolean CheckDeckRules(string playerName, List<ICard> deck)
        {
			Console.WriteLine($"Checking the deck of {playerName}...");
			if(deck.Count != 37)
            {
				throw new Exception($"Invalid Deck of {playerName}: Deck should contain 37 cards");
            }

			// Cards can have only 3 copies in each deck, we think with an exception on lands... Not specified in the assignment
			Dictionary<string, int> CopyCheck = new Dictionary<string, int>();
			foreach(ICard card in deck)
            {
				if (!Object.ReferenceEquals(card.GetType(), typeof(Land))) {
					if (CopyCheck.ContainsKey(card.Name))
                    {
						int value = CopyCheck[card.Name];
						CopyCheck.Remove(card.Name);
						CopyCheck[card.Name] = value + 1;
                    } else
                    {
						CopyCheck[card.Name] = 1;
                    }
				}
			}

			// Check if there is a value higher than 3
			if(!CopyCheck.Values.ToList().All(value => value <= 3))
            {
				throw new Exception($"Invalid Deck of {playerName}: Each non land card may only be 3 times in the deck");
			}

			return true;
		}
	}
	public static class Extensions
    {
		public static List<ICard> PopRange<ICard>(this Stack<ICard> stack, int amount)
		{
			var result = new List<ICard>(amount);
			while (amount-- > 0 && stack.Count > 0)
			{
				result.Add(stack.Pop());
			}
			return result;
		}
	}
}
