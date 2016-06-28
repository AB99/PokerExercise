using System;
using System.Collections.Generic;
using NUnit.Framework;
using PokerExercise.Hands;
using PokerExercise.PossiblePokerHands;

namespace PokerExercise.Tests
{
    [TestFixture]
    public class PokerGameUnitTests
    {
        private PokerGame _pokerGame;

        [SetUp]
        public void SetUp()
        {
            _pokerGame = new PokerGame();
        }

        [Test]
        public void CalculateWinner_PlayersNull_Throws()
        {
            List<IPlayer> players = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _pokerGame.CalculateWinners(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void CalculateWinner_PlayersContainsNull_Throws()
        {
            var players = new List<IPlayer>
            {
                TestUtils.CreatePlayer(),
                null,
                TestUtils.CreatePlayer(),
            };

            var exception = Assert.Throws<ArgumentNullException>(() => _pokerGame.CalculateWinners(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void CalculateWinner_MultiplePossibleHands_ReturnsWinnersInOrderOfPrecendence()
        {
            var players = new List<IPlayer>
            {
                TestUtils.CreatePlayer(),
                TestUtils.CreatePlayer(),
                TestUtils.CreatePlayer(),
                TestUtils.CreatePlayer(),
            };

            var winners1 = new List<IPlayer>();
            var pokerHand1 = TestUtils.CreatedPossiblePokerHand(players, winners1);
            
            var winners2 = new List<IPlayer> { players[0], players[1] };
            var pokerHand2 = TestUtils.CreatedPossiblePokerHand(players, winners2);

            var winners3 = new List<IPlayer> { players[2], players[3] };
            var pokerHand3 = TestUtils.CreatedPossiblePokerHand(players, winners3);

            _pokerGame.PossiblePokerHands = new List<IPossiblePokerHand> {pokerHand1, pokerHand2, pokerHand3};

            var result =_pokerGame.CalculateWinners(players);
            Assert.That(result, Is.EqualTo(winners2));
        }
    }
}
