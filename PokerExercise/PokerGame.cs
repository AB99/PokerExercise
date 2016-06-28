using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerExercise.Hands;

namespace PokerExercise
{
    public class PokerGame
    {
        public List<IPossiblePokerHand> PossiblePokerHands = new List<IPossiblePokerHand>
        {
            new OnePair(),
            new HighCard(),
        };

        public List<IPlayer> CalculateWinner(List<IPlayer> players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (players.Any(p => p == null))
                throw new ArgumentNullException(nameof(players));

            foreach (IPossiblePokerHand possiblePokerHand in PossiblePokerHands)
            {
                var winners = possiblePokerHand.FindWinningPlayersWithThisHand(players);
                if (winners.Count > 0)
                {
                    return winners;
                }
            }

            throw new InvalidOperationException();
        }
    }
}
