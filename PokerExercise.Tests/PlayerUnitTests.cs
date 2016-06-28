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

            var exception = Assert.Throws<ArgumentNullException>(() => player = new Player(name, ValidHand));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(name)));
        }

        [Test]
        public void ConstructorTest_NameIsEmpty_Throws()
        {
            string name = string.Empty;
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(name, ValidHand));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(name)));
        }

        [Test]
        public void ConstructorTest_HandIsNull_Throws()
        {
            Card[] hand = null;
            Player player;

            var exception = Assert.Throws<ArgumentNullException>(() => player = new Player(ValidName, hand));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }

        [Test]
        public void ConstructorTest_HandContainsNull_Throws()
        {
            List<Card> hand = new List<Card>(ValidHand) { [1] = null };
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }

        [Test]
        public void ConstructorTest_HandLessThanFiveCards_Throws()
        {
            List<Card> hand = new List<Card>(ValidHand);
            hand.RemoveAt(1);
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }

        [Test]
        public void ConstructorTest_HandMoreThanFiveCards_Throws()
        {
            List<Card> hand = new List<Card>(ValidHand) {CreateCard(Rank.King, Suit.Diamonds)};
            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }


        [Test]
        public void ConstructorTest_HandAllSameSuit_DoesNotThrow()
        {
            var suit = Suit.Spades;

            var hand = new List<Card>()
            {
                CreateCard(Rank.Ace, suit),
                CreateCard(Rank.Two, suit),
                CreateCard(Rank.Three, suit),
                CreateCard(Rank.Four, suit),
                CreateCard(Rank.Five, suit),
            };

            Player player;

            Assert.DoesNotThrow(() => player = new Player(ValidName, hand.ToArray()));
        }

        [Test]
        public void ConstructorTest_HandContainsFiveOfAKind_Throws()
        {
            var rank = Rank.Ace;
            var suit = Suit.Spades;

            var hand = new List<Card>()
            {
                CreateCard(rank, suit),
                CreateCard(rank, suit),
                CreateCard(rank, suit),
                CreateCard(rank, suit),
                CreateCard(rank, suit),
            };

            Player player;

            var exception = Assert.Throws<ArgumentException>(() => player = new Player(ValidName, hand.ToArray()));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(hand)));
        }


        [Test]
        public void ConstructorTest_ValidParams_PropertySetAsExpected()
        {
            var player = new Player(ValidName, ValidHand);

            Assert.That(player.Name, Is.EqualTo(ValidName));
            Assert.That(player.Hand, Is.EqualTo(ValidHand));
        }



        #region utils

        private const string ValidName = "Bob";
        private static readonly Card[] ValidHand =
        {
            CreateCard(Rank.Five, Suit.Hearts),
            CreateCard(Rank.Two, Suit.Diamonds),
            CreateCard(Rank.Ace, Suit.Spades),
            CreateCard(Rank.Nine, Suit.Spades),
            CreateCard(Rank.Jack, Suit.Clubs),
        };

        private static Card CreateCard (Rank rank, Suit suit)
        {
            return new Card(rank, suit);   
        }

        #endregion
    }
}
