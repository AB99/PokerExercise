using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise
{
    public class Player
    {
        public Player(string name, Card [] hand)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name) + " can not be null or empty.", nameof(name));

            if (hand.Count() != 5)
                throw new ArgumentException(nameof(hand) + " must contain 5 items", nameof(hand));

            if (hand.Any(c => c == null))
                throw new ArgumentException(nameof(hand) + " must not contain null items", nameof(hand));

            Name = name;
            Hand = hand;
        }

        public string Name { get; }
        public Card[] Hand { get; }
    }
}
