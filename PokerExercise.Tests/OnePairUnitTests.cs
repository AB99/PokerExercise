using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PokerExercise.Hands;

namespace PokerExercise.Tests
{
    [TestFixture]
    public class OnePairUnitTests
    {
        private OnePair _onePair;

        [SetUp]
        public void SetUp()
        {
            _onePair = new OnePair();
        }



        [Test]
        public void Applies_PlayerNull_Throws()
        {
            IPlayer player = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _onePair.Applies(player));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(player)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_HandIsNull_Throws()
        {
            List<IPlayer> players = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _onePair.FindWinningHandsInThisCategory(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_HandContainsNull_Throws()
        {
            var player1 = TestUtils.CreatePlayer();
            IPlayer player2 = null;
            var player3 = TestUtils.CreatePlayer();
            var players = new List<IPlayer> { player1, player3, player2 };

            var exception = Assert.Throws<ArgumentException>(() => _onePair.FindWinningHandsInThisCategory(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_EmptyList_ReturnsEmptyList()
        {
            IList<IPlayer> result = _onePair.FindWinningHandsInThisCategory(new List<IPlayer>());
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Applies_NoPairPresent_ReturnsFalse()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
            });

            Assert.That(_onePair.Applies(player1), Is.False);
        }

        [Test]
        public void Applies_PairPresent_ReturnsTrue()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
            });

            Assert.That(_onePair.Applies(player1), Is.True);
        }

        [Test]
        public void FindWinningHandsInThisCategory_PairsAreUnique_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
                TestUtils.CreateCard(Rank.Nine),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Two),
                TestUtils.CreateCard(Rank.Two),
                TestUtils.CreateCard(Rank.Queen),
                TestUtils.CreateCard(Rank.Queen),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Four),
                TestUtils.CreateCard(Rank.Four),
                TestUtils.CreateCard(Rank.Eight),
                TestUtils.CreateCard(Rank.Eight),
            });

            var result = _onePair.FindWinningHandsInThisCategory(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player2));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PairsAreDuplicatdKickerUnique_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Eight),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Seven),
            });

            var result = _onePair.FindWinningHandsInThisCategory(new List<IPlayer> { player1, player2 });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player1));
        }

        [Test]
        public void FindWinningHandsInThisCategory_AllCardsDuplicated_ReturnsAllPlayers()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
            });

            var result = _onePair.FindWinningHandsInThisCategory(new List<IPlayer> { player1, player2 });

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Contains(player1), Is.True);
            Assert.That(result.Contains(player2), Is.True);
        }

    }
}
