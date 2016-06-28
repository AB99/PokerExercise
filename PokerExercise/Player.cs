using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise
{
    public class Player : IPlayer
    {
        public Player(string name, Card [] cards)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (name == string.Empty)
                throw new ArgumentException(nameof(name) + " can not be empty.", nameof(name));

            if (cards == null)
                throw new ArgumentNullException(nameof(cards));

            if (cards.Count() != 5)
                throw new ArgumentException(nameof(cards) + " must contain 5 items", nameof(cards));
                
            if (cards.Any(c => c == null))
                throw new ArgumentException(nameof(cards) + " must not contain null items", nameof(cards));

            if (cards.All(c => c.Suit == cards[0].Suit && c.Rank == cards[0].Rank))
                throw new ArgumentException(nameof(cards) + " must not contain null items", nameof(cards));

            Name = name;
            Cards = cards;
        }

        public string Name { get; }
        public Card[] Cards { get; }
    }
}
