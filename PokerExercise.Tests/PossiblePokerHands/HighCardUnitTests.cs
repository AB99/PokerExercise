﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PokerExercise.PossiblePokerHands;

namespace PokerExercise.Tests.PossiblePokerHands
{
    [TestFixture]
    public class HighCardUnitTests
    {
        private HighCard _highCard;

        [SetUp]
        public void SetUp()
        {
            _highCard = new HighCard();
        }

        [Test]
        public void PlayerHasHand_AlwaysReturnsTrue()
        {
            Assert.That(_highCard.PlayerHasHand(TestUtils.CreatePlayer()), Is.True);
        }


        [Test]
        public void FindWinningHandsInThisCategory_PlayersNull_Throws()
        {
            List<IPlayer> players = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _highCard.FindWinningPlayersWithThisHand(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PlayersContainsNull_Throws()
        {
            var player1 = TestUtils.CreatePlayer();
            IPlayer player2 = null;
            var player3 = TestUtils.CreatePlayer();
            var players = new List<IPlayer> { player1, player3, player2 };

            var exception = Assert.Throws<ArgumentException>(() => _highCard.FindWinningPlayersWithThisHand(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void FindWinningHandsInThisCategory_EmptyList_ReturnsEmptyList()
        {
            IList<IPlayer> result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer>());
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PlayersWithDifferentSizedHands_Throws()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card> { TestUtils.CreateCard() });
            var player2 = TestUtils.CreatePlayer(hand: new List<Card> { TestUtils.CreateCard(), TestUtils.CreateCard() });
            var players = new List<IPlayer> {player1, player2};

            Assert.Throws<ArgumentException>(() => _highCard.FindWinningPlayersWithThisHand(players));
        }


        [Test]
        public void FindWinningHandsInThisCategory_PlayersHighestCardsUnique_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Jack),
                TestUtils.CreateCard(Rank.Eight),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.King),
                TestUtils.CreateCard(Rank.Seven),
            });

            var result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer> {player1, player2, player3});

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player3));
        }

        [Test]
        public void FindWinningHandsInThisCategory_PlayersHighestCardsUniqueLowestFirst_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Nine),
                TestUtils.CreateCard(Rank.Ten),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Eight),
                TestUtils.CreateCard(Rank.Jack),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Seven),
                TestUtils.CreateCard(Rank.King),
            });

            var result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player3));
        }


        [Test]
        public void FindWinningHandsInThisCategory_HighestCardDuplicatedSecondCardUnique_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Eight),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Seven),
            });

            var result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer> {player1, player2, player3});

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player1));
        }

        [Test]
        public void FindWinningHandsInThisCategory_HighestCardDuplicatedButNotByAllPlayersSecondCardUnique_FindsHighest()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Five),
                TestUtils.CreateCard(Rank.Nine),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Eight),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Seven),
            });

            var result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(player2));
        }

        [Test]
        public void FindWinningHandsInThisCategory_AllCardsDuplicated_ReturnsAllPlayers()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten),
                TestUtils.CreateCard(Rank.Nine),
            });

            var result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Contains(player1), Is.True);
            Assert.That(result.Contains(player2), Is.True);
            Assert.That(result.Contains(player3), Is.True);
        }

        [Test]
        public void FindWinningHandsInThisCategory_AllCardsDuplicatedInRankButNotSuit_ReturnsAllPlayers()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten, Suit.Clubs),
            });

            var player2 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten, Suit.Spades),
            });

            var player3 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten, Suit.Diamonds),
            });

            var player4 = TestUtils.CreatePlayer(hand: new List<Card>
            {
                TestUtils.CreateCard(Rank.Ten, Suit.Hearts),
            });

            var result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3, player4});

            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result.Contains(player1), Is.True);
            Assert.That(result.Contains(player2), Is.True);
            Assert.That(result.Contains(player3), Is.True);
            Assert.That(result.Contains(player4), Is.True);
        }


        [Test]
        public void FindWinningHandsInThisCategory_PlayersHaveNoCards_ReturnsAllPlayers()
        {
            var player1 = TestUtils.CreatePlayer(hand: new List<Card>());
            var player2 = TestUtils.CreatePlayer(hand: new List<Card>());
            var player3 = TestUtils.CreatePlayer(hand: new List<Card>());

            var result = _highCard.FindWinningPlayersWithThisHand(new List<IPlayer> { player1, player2, player3 });

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Contains(player1), Is.True);
            Assert.That(result.Contains(player2), Is.True);
            Assert.That(result.Contains(player3), Is.True);
        }

    }
}
