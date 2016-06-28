using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace PokerExercise.Tests
{
    [TestFixture]
    public class PlayerUnitTests
    {
        [Test]
        public void ConstructorTest_NameIsNull_Throws()
        {
            string name = null;
            Player player;

            var exception = Assert.Throws<ArgumentNullException>(() => player = new Player(name, TestUtils.ValidHand));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(name)));
        }

        [Test]
        public void ConstructorTest_NameIsEmpty_Throws()
        {
            string name = string.Empty;
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(name, TestUtils.ValidHand));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(name)));
        }

        [Test]
        public void ConstructorTest_HandIsNull_Throws()
        {
            Card[] hand = null;
            Player player;

            var exception = Assert.Throws<ArgumentNullException>(() => player = new Player(TestUtils.ValidName, hand));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }

        [Test]
        public void ConstructorTest_HandContainsNull_Throws()
        {
            List<Card> hand = new List<Card>(TestUtils.ValidHand) { [1] = null };
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(TestUtils.ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }

        [Test]
        public void ConstructorTest_HandLessThanFiveCards_Throws()
        {
            List<Card> hand = new List<Card>(TestUtils.ValidHand);
            hand.RemoveAt(1);
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(TestUtils.ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }

        [Test]
        public void ConstructorTest_HandMoreThanFiveCards_Throws()
        {
            List<Card> hand = new List<Card>(TestUtils.ValidHand) {TestUtils.CreateCard(Rank.King, Suit.Diamonds)};
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(TestUtils.ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }


        [Test]
        public void ConstructorTest_HandAllSameSuit_DoesNotThrow()
        {
            var suit = Suit.Spades;

            var hand = new List<Card>()
            {
                TestUtils.CreateCard(Rank.Ace, suit), TestUtils.CreateCard(Rank.Two, suit), TestUtils.CreateCard(Rank.Three, suit), TestUtils.CreateCard(Rank.Four, suit), TestUtils.CreateCard(Rank.Five, suit),
            };

            Player player;

            Assert.DoesNotThrow(() => player = new Player(TestUtils.ValidName, hand.ToArray()));
        }

        [Test]
        public void ConstructorTest_HandContainsFiveOfAKind_Throws()
        {
            var rank = Rank.Ace;
            var suit = Suit.Spades;

            var hand = new List<Card>()
            {
                TestUtils.CreateCard(rank, suit), TestUtils.CreateCard(rank, suit), TestUtils.CreateCard(rank, suit), TestUtils.CreateCard(rank, suit), TestUtils.CreateCard(rank, suit),
            };

            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(TestUtils.ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }


        [Test]
        public void ConstructorTest_ValidParams_PropertySetAsExpected()
        {
            var player = new Player(TestUtils.ValidName, TestUtils.ValidHand);

            Assert.That(player.Name, Is.EqualTo(TestUtils.ValidName));
            Assert.That(player.Cards, Is.EqualTo(TestUtils.ValidHand));
        }
    }
}
