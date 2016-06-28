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
                TestUtils.CreatePlayer(),
                null,
                TestUtils.CreatePlayer(),
            };

            var exception = Assert.Throws<ArgumentNullException>(() => _pokerGame.CalculateWinner(players));
            Assert.That(exception.ParamName, Is.EqualTo(nameof(players)));
        }

        //[Test]
        //public void CalculateWinner_PlayersContainsNull_Throws()
        //{
        //}
    }
}
