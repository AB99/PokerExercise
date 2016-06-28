using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise
{
    public class Player : IPlayer
    {
        public Player(string name, Card [] hand)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (name == string.Empty)
                throw new ArgumentException(nameof(name) + " can not be empty.", nameof(name));

            if (hand == null)
                throw new ArgumentNullException(nameof(hand));

            if (hand.Count() != 5)
                throw new ArgumentException(nameof(hand) + " must contain 5 items", nameof(hand));
                
            if (hand.Any(c => c == null))
                throw new ArgumentException(nameof(hand) + " must not contain null items", nameof(hand));

            if (hand.All(c => c.Suit == hand[0].Suit && c.Rank == hand[0].Rank))
                throw new ArgumentException(nameof(hand) + " must not contain null items", nameof(hand));

            Name = name;
            Hand = hand;
        }

        public string Name { get; }
        public Card[] Hand { get; }
    }
}
