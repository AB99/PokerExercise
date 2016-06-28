using NUnit.Framework;

namespace PokerExercise.Tests
{
    [TestFixture]
    public class CardUnitTests
    {
        [TestCase(Rank.Five, Suit.Diamonds)]
        [TestCase(Rank.Queen, Suit.Spades)]
        public void TestConstructor(Rank rank, Suit suit)
        {
            var card = new Card(rank, suit);

            Assert.That(card.Rank, Is.EqualTo(rank));
            Assert.That(card.Suit, Is.EqualTo(suit));
        }
    }
}
