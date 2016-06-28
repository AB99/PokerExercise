using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

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
            IPlayer[] players = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _pokerGame.CalculateWinner(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        [Test]
        public void CalculateWinner_PlayersContainsNull_Throws()
        {
            IPlayer[] players =
            {
                CreatePlayer(),
                null,
                CreatePlayer(),
            };

            var exception = Assert.Throws<ArgumentNullException>(() => _pokerGame.CalculateWinner(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        //[Test]
        //public void CalculateWinner_PlayersContainsNull_Throws()
        //{
        //}

        private static IPlayer CreatePlayer(string name = "Bob", Card[] hand = null)
        {
            hand = hand ?? new Card[0];

            var stubPlayer = new Mock<IPlayer>();
            stubPlayer.Setup(s => s.Name).Returns(name);
            stubPlayer.Setup(s => s.Hand).Returns(hand.ToArray);

            return stubPlayer.Object;
        }

    }
}
