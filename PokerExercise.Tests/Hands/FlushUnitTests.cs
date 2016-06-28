using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PokerExercise.Hands;
using PokerExercise.PossiblePokerHands;

namespace PokerExercise.Tests.Hands
{
    [TestFixture]
    public class FlushUnitTests
    {
        private Flush _flush;

        [SetUp]
        public void SetUp()
        {
            _flush = new Flush();
        }

        [Test]
        public void Applies_PlayerNull_Throws()
        {
            IPlayer player = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _flush.PlayerHasHand(player));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(player)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PlayersNull_Throws()
        {
            List<IPlayer> players = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _flush.FindWinningPlayersWithThisHand(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PlayersContainsNull_Throws()
        {
            var player1 = TestUtils.CreatePlayer();
            IPlayer player2 = null;
            var player3 = TestUtils.CreatePlayer();
            var players = new List<IPlayer> { player1, player3, player2 };

            var exception = Assert.Throws<ArgumentException>(() => _flush.FindWinningPlayersWithThisHand(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_EmptyList_ReturnsEmptyList()
        {
            IList<IPlayer> result = _flush.FindWinningPlayersWithThisHand(new List<IPlayer>());
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Applies_FlushNotPresent_ReturnsFalse()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Hearts),
            });

            Assert.That(_flush.PlayerHasHand(player1), Is.False);
        }

        [Test]
        public void Applies_FlushPresent_ReturnsTrue()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Spades),
            });

            Assert.That(_flush.PlayerHasHand(player1), Is.True);
        }

        [Test]
        public void FindWinningHandsInThisCategory_NoFlush_ReturnsEmptyList()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Hearts),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Diamonds),
                TestUtils.CreateCard(suit: Suit.Spades),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(suit: Suit.Clubs),
                TestUtils.CreateCard(suit: Suit.Spades),
                TestUtils.CreateCard(suit: Suit.Spades),
            });

            var result = _flush.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindWinningHandsInThisCategory_Flush_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Nine, Suit.Spades),
                TestUtils.CreateCard(Rank.Ten, Suit.Spades),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Two, Suit.Diamonds),
                TestUtils.CreateCard(Rank.Queen, Suit.Diamonds),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Four, Suit.Hearts),
                TestUtils.CreateCard(Rank.Eight, Suit.Hearts),
            });

            var result = _flush.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player2));
        }

        [Test]
        public void FindWinningHandsInThisCategory_FlushReverseOrder_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten, Suit.Spades),
                TestUtils.CreateCard(Rank.Nine, Suit.Spades),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Queen, Suit.Diamonds),
                TestUtils.CreateCard(Rank.Two, Suit.Diamonds),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Eight, Suit.Hearts),
                TestUtils.CreateCard(Rank.Four, Suit.Hearts),
            });

            var result = _flush.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player2));
        }
    }
}
