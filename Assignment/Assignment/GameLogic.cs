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
