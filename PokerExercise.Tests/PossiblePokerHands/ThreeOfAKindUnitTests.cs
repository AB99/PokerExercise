using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PokerExercise.PossiblePokerHands;

namespace PokerExercise.Tests.PossiblePokerHands
{
    [TestFixture]
    public class ThreeOfAKindUnitTests
    {
        private ThreeOfAKind _threeOfAKind;

        [SetUp]
        public void SetUp()
        {
            _threeOfAKind = new ThreeOfAKind();
        }

        [Test]
        public void PlayerHasHand_PlayerNull_Throws()
        {
            IPlayer player = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _threeOfAKind.PlayerHasHand(player));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(player)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PlayersNull_Throws()
        {
            List<IPlayer> players = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _threeOfAKind.FindWinningPlayersWithThisHand(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PlayersContainsNull_Throws()
        {
            var player1 = TestUtils.CreatePlayer();
            IPlayer player2 = null;
            var player3 = TestUtils.CreatePlayer();
            var players = new List<IPlayer> { player1, player3, player2 };

            var exception = Assert.Throws<ArgumentException>(() => _threeOfAKind.FindWinningPlayersWithThisHand(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_EmptyList_ReturnsEmptyList()
        {
            IList<IPlayer> result = _threeOfAKind.FindWinningPlayersWithThisHand(new List<IPlayer>());
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void PlayerHasHand_ThreeOfAKindNotPresent_ReturnsFalse()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
                TestUtils.CreateCard(Rank.Eight),
            });

            Assert.That(_threeOfAKind.PlayerHasHand(player1), Is.False);
        }

        [Test]
        public void PlayerHasHand_ThreeOfAKindPresent_ReturnsTrue()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
            });

            Assert.That(_threeOfAKind.PlayerHasHand(player1), Is.True);
        }

        [Test]
        public void FindWinningHandsInThisCategory_NoThreeOfAKind_ReturnsEmptyList()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Nine),
                TestUtils.CreateCard(Rank.Nine),
                TestUtils.CreateCard(Rank.Eight),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Two),
                TestUtils.CreateCard(Rank.Two),
                TestUtils.CreateCard(Rank.Three),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Four),
                TestUtils.CreateCard(Rank.Four),
                TestUtils.CreateCard(Rank.Five),
            });

            var result = _threeOfAKind.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindWinningHandsInThisCategory_ThreeOfAKindUnique_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Nine),
                TestUtils.CreateCard(Rank.Nine),
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Two),
                TestUtils.CreateCard(Rank.Queen),
                TestUtils.CreateCard(Rank.Queen),
                TestUtils.CreateCard(Rank.Two),
                TestUtils.CreateCard(Rank.Queen),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Four),
                TestUtils.CreateCard(Rank.Four),
                TestUtils.CreateCard(Rank.Eight),
                TestUtils.CreateCard(Rank.Eight),
                TestUtils.CreateCard(Rank.Eight),
            });

            var result = _threeOfAKind.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player2));
        }
    }
}
