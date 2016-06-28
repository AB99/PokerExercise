using System;
using System.Collections.Generic;
using System.Linq;
using PokerExercise.Hands;
using PokerExercise.PossiblePokerHands;

namespace PokerExercise
{
    public class PokerGame
    {
        public List<IPossiblePokerHand> PossiblePokerHands = new List<IPossiblePokerHand>
        {
            new OnePair(),
            new HighCard(),
        };

        public List<IPlayer> CalculateWinners(List<IPlayer> players)
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
