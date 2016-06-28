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
            var joe = new Player("Joe",
                new []
                {
                    new Card(Rank.Three, Suit.Hearts),
                    new Card(Rank.Six, Suit.Hearts),
                    new Card(Rank.Eight, Suit.Hearts),
                    new Card(Rank.Jack, Suit.Hearts),
                    new Card(Rank.King, Suit.Hearts),
                });

            var jen = new Player("Jen",
                new[]
                {
                    new Card(Rank.Three, Suit.Clubs),
                    new Card(Rank.Three, Suit.Diamonds),
                    new Card(Rank.Three, Suit.Spades),
                    new Card(Rank.Eight, Suit.Clubs),
                    new Card(Rank.Ten, Suit.Diamonds),
                });

            var bob = new Player("Bob",
                new[]
                {
                    new Card(Rank.Two, Suit.Hearts),
                    new Card(Rank.Five, Suit.Clubs),
                    new Card(Rank.Seven, Suit.Spades),
                    new Card(Rank.Ten, Suit.Clubs),
                    new Card(Rank.Ace, Suit.Clubs),
                });

            var players = new List<IPlayer> {joe, jen, bob};

            List<IPlayer> winners = _pokerGame.CalculateWinners(players);
            Assert.That(winners.Count, Is.EqualTo(1));
            Assert.That(winners[0], Is.EqualTo(joe));
        }


        [Test]
        public void CalculateWinner_Example2_ReturnsJen()
        {
            var joe = new Player("Joe",
                new[]
                {
                    new Card(Rank.Three, Suit.Hearts),
                    new Card(Rank.Four, Suit.Diamonds),
                    new Card(Rank.Nine, Suit.Clubs),
                    new Card(Rank.Nine, Suit.Diamonds),
                    new Card(Rank.Queen, Suit.Hearts),
                });

            var jen = new Player("Jen",
                new[]
                {
                    new Card(Rank.Five, Suit.Clubs),
                    new Card(Rank.Seven, Suit.Diamonds),
                    new Card(Rank.Nine, Suit.Hearts),
                    new Card(Rank.Nine, Suit.Spades),
                    new Card(Rank.Queen, Suit.Spades),
                });

            var bob = new Player("Bob",
                new[]
                {
                    new Card(Rank.Two, Suit.Hearts),
                    new Card(Rank.Two, Suit.Clubs),
                    new Card(Rank.Five, Suit.Spades),
                    new Card(Rank.Ten, Suit.Clubs),
                    new Card(Rank.Ace, Suit.Hearts),
                });

            var players = new List<IPlayer> { joe, jen, bob };

            List<IPlayer> winners = _pokerGame.CalculateWinners(players);
            Assert.That(winners.Count, Is.EqualTo(1));
            Assert.That(winners[0], Is.EqualTo(jen));
        }
    }
}
