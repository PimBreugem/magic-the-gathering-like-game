using System;
using System.Collections.Generic;
using System.Linq;

/* 
Jonas de Boer 0983180
Pim Breugem 0992420
 */

namespace Assignment
{
	public class GameLogic
	{
		public Board Board { get; set; }
		public Player Winner { get; set; }
		public GameLogic(string playerOneName, string playerTwoName, int startingHealth, List<ICard> playerOneDeck, List<ICard> playerTwoDeck)
		{
			if(CheckDeckRules(playerOneName, playerOneDeck) && CheckDeckRules(playerTwoName, playerTwoDeck)){
				Board = new Board();

				// Shuffeling and drawing starting hand
				Console.WriteLine("Decks are checked, shuffeling cards...");
				var random = new Random();
				Stack<ICard> ShuffeledPlayerOneDeck = new Stack<ICard>(playerOneDeck.OrderBy(item => random.Next()).ToList());
				Stack<ICard> ShuffeledPlayerTwoDeck = new Stack<ICard>(playerTwoDeck.OrderBy(item => random.Next()).ToList());
				

				// Starting the game
				Console.WriteLine("Decks are shuffeld, initializing the game...");
				Board.Player1 = new Player(playerOneName, PlayerType.ONE, startingHealth, ShuffeledPlayerOneDeck);
				Board.Player2 = new Player(playerTwoName, PlayerType.TWO, startingHealth, ShuffeledPlayerTwoDeck);

				// Start taking turns until someone doesn't have lives
				var currentType = PlayerType.ONE;
				while(Winner == null && Board.Player1.PlayerHealth > 0 && Board.Player2.PlayerHealth > 0)
                {
					PlayerTurn(currentType);
					currentType = (currentType == PlayerType.ONE ? PlayerType.TWO : PlayerType.ONE);
                }

				// Winner is declared
				if (Winner == null) { Winner = (Board.Player1.PlayerHealth > 0 ? Board.Player1 : Board.Player2); }
				Console.WriteLine($"PLayer {Winner.Name} won the game!");
			}
		}

		public void PlayerTurn(PlayerType playerType)
        {
			Player CurrentPlayer = (Board.Player1.Type == playerType ? Board.Player1 : Board.Player2);
			Player OpponentPlayer = (Board.Player1.Type != playerType ? Board.Player1 : Board.Player2);

			// START OF PREPERATION FASE
			foreach (LandOnBoard land in CurrentPlayer.Lands) { land.Reset(); } //Reset all the lands

			// If Effects expire remove them from both yourself or opponent effects
			List<Effect> RemoveOwnEffects = new List<Effect>();
			List<Effect> RemoveOpponentEffects = new List<Effect>();
			foreach (PermanentOnBoard permanent in CurrentPlayer.Permanents)
            {
				foreach(Effect effect in permanent.Permanent.Effects)
                {
					if(effect.Duration != null)
                    {
						if(effect.Duration-- <= 0)
                        {
							if (!effect.TargetOtherPlayer) { RemoveOwnEffects.Add(effect); }
							else { RemoveOpponentEffects.Add(effect); }
                        }
                    }
                }
            }

			// Save remove all efects in remove own effects
			foreach (PermanentOnBoard permanent in CurrentPlayer.Permanents)
            {
				foreach(Effect effect in RemoveOwnEffects)
                {
					if (permanent.RemoveEffectIfExists(effect)) { break; } // Only remove one effect
				}
            }

			// Save remove all effects in remove opponent effects
			foreach (PermanentOnBoard permanent in OpponentPlayer.Permanents)
            {
				foreach(Effect effect in RemoveOpponentEffects)
                {
                    if (permanent.RemoveEffectIfExists(effect)) { break; } // Only remove one effect

				}
			} // END OF PREPERATION FASE


			// DRAWING FASE
			if (!CurrentPlayer.DrawCard())
            {
				Winner = OpponentPlayer;
				return;
            }

			// START OF MAIN PHASE
			/* This is where we cannot automate implementation further 
			 
			Player can choose the cards he or she want to play
			
			 */

        }

		public bool CheckDeckRules(string playerName, List<ICard> deck)
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
