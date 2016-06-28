using System.Collections.Generic;
using System.Linq;
using Moq;

namespace PokerExercise.Tests
{
    public class TestUtils
    {
        public static readonly Card[] ValidHand =
        {
            CreateCard(Rank.Five, Suit.Hearts),
            CreateCard(Rank.Two, Suit.Diamonds),
            CreateCard(Rank.Ace, Suit.Spades),
            CreateCard(Rank.Nine, Suit.Spades),
            CreateCard(Rank.Jack, Suit.Clubs),
        };

        public const string ValidName = "Bob";

        public static Card CreateCard (Rank? rank = null, Suit? suit = null)
        {
            rank = rank ?? Rank.Five;
            suit = suit ?? Suit.Spades;

            return new Card(rank.Value, suit.Value);   
        }   

        public static IPlayer CreatePlayer(string name = "Bob", IList<Card> hand = null)
        {
            hand = hand ?? new List<Card>();

            var stubPlayer = new Mock<IPlayer>();
            stubPlayer.Setup(s => s.Name).Returns(name);
            stubPlayer.Setup(s => s.Hand).Returns(hand.ToArray);

            return stubPlayer.Object;
        }

    }
}