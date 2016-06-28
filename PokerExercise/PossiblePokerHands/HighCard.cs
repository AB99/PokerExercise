using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerExercise.PossiblePokerHands
{
    public class HighCard : KickerDependantHand, IPossiblePokerHand
    {
        public bool PlayerHasHand(IPlayer player)
        {
            return true;
        }

        public List<IPlayer> FindWinningPlayersWithThisHand(List<IPlayer> players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (players.Any(p => p == null))
                throw new ArgumentException(nameof(players) + " can not contain null", nameof(players));

            if (players.Count == 0)
                return players;

            var kickersToCompare = players.Select(p => new KickersInPlayersHand(p, new List<Card>(p.Cards))).ToList();
            return FindPlayersWithWinningKickers(kickersToCompare);
        }
    }
}
