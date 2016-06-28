using System.Collections.Generic;
using NUnit.Framework;

namespace PokerExercise.Tests
{
    [TestFixture]
    public class PokerGameIntegrationTests
    {
        private PokerGame _pokerGame;

        [SetUp]
        public void SetUp()
        {
            _pokerGame = new PokerGame();
        }

        [Test]
        public void CalculateWinner_Example1_ReturnsJoe()
        {
            var joe = TestUtils.CreatePlayer("Joe",
                new List<Card>
                {
                    TestUtils.CreateCard(Rank.Three, Suit.Hearts),
                    TestUtils.CreateCard(Rank.Six, Suit.Hearts),
                    TestUtils.CreateCard(Rank.Eight, Suit.Hearts),
                    TestUtils.CreateCard(Rank.Jack, Suit.Hearts),
                    TestUtils.CreateCard(Rank.King, Suit.Hearts),
                });

            var jen = TestUtils.CreatePlayer("Jen",
                new List<Card>
                {
                    TestUtils.CreateCard(Rank.Three, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Three, Suit.Diamonds),
                    TestUtils.CreateCard(Rank.Three, Suit.Spades),
                    TestUtils.CreateCard(Rank.Eight, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Ten, Suit.Diamonds),
                });

            var bob = TestUtils.CreatePlayer("Bob",
                new List<Card>
                {
                    TestUtils.CreateCard(Rank.Two, Suit.Hearts),
                    TestUtils.CreateCard(Rank.Five, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Seven, Suit.Spades),
                    TestUtils.CreateCard(Rank.Ten, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Ace, Suit.Clubs),
                });

            var players = new List<IPlayer> {joe, jen, bob};

            List<IPlayer> winners = _pokerGame.CalculateWinners(players);
            Assert.That(winners.Count, Is.EqualTo(1));
            Assert.That(winners[0], Is.EqualTo(joe));
        }


        [Test]
        public void CalculateWinner_Example2_ReturnsJen()
        {
            var joe = TestUtils.CreatePlayer("Joe",
                new List<Card>
                {
                    TestUtils.CreateCard(Rank.Three, Suit.Hearts),
                    TestUtils.CreateCard(Rank.Four, Suit.Diamonds),
                    TestUtils.CreateCard(Rank.Nine, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Nine, Suit.Diamonds),
                    TestUtils.CreateCard(Rank.Queen, Suit.Hearts),
                });

            var jen = TestUtils.CreatePlayer("Jen",
                new List<Card>
                {
                    TestUtils.CreateCard(Rank.Five, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Seven, Suit.Diamonds),
                    TestUtils.CreateCard(Rank.Nine, Suit.Hearts),
                    TestUtils.CreateCard(Rank.Nine, Suit.Spades),
                    TestUtils.CreateCard(Rank.Queen, Suit.Spades),
                });

            var bob = TestUtils.CreatePlayer("Bob",
                new List<Card>
                {
                    TestUtils.CreateCard(Rank.Two, Suit.Hearts),
                    TestUtils.CreateCard(Rank.Two, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Five, Suit.Spades),
                    TestUtils.CreateCard(Rank.Ten, Suit.Clubs),
                    TestUtils.CreateCard(Rank.Ace, Suit.Hearts),
                });

            var players = new List<IPlayer> { joe, jen, bob };

            List<IPlayer> winners = _pokerGame.CalculateWinners(players);
            Assert.That(winners.Count, Is.EqualTo(1));
            Assert.That(winners[0], Is.EqualTo(jen));
        }
    }
}
