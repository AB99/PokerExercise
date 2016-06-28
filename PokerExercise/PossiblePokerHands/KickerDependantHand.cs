using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerExercise.PossiblePokerHands
{
    public class KickerDependantHand 
    {
        protected List<IPlayer> FindPlayersWithWinningKickers(List<KickersInPlayersHand> kickersToCompare)
        {
            if (kickersToCompare.Count == 0)
                return new List<IPlayer>();

            var kickerCount = kickersToCompare.First().Kickers.Count();

            if (kickersToCompare.Any(p => p.Kickers.Count() != kickerCount))
                throw new ArgumentException("Kicker counts are different", nameof(kickerCount));

            if (kickerCount == 0)
                return kickersToCompare.Select(k => k.Player).ToList();

            foreach (var kicker in kickersToCompare)
            {
                kicker.Kickers = kicker.Kickers.OrderByDescending(k => k.Rank).ToList();
            }

            Rank? highestRankAtPosition = null;
            List<KickersInPlayersHand> possibleWinners = kickersToCompare;

            for (int i = 0; i < kickerCount; i++)
            {
                highestRankAtPosition = possibleWinners.Select(p => p.Kickers[i].Rank).Max();
                possibleWinners = possibleWinners.Where(p => p.Kickers[i].Rank == highestRankAtPosition).ToList();

                if (possibleWinners.Count(p => p.Kickers[i].Rank == highestRankAtPosition) == 1)
                {
                    return new List<IPlayer> { possibleWinners.Single(p => p.Kickers[i].Rank == highestRankAtPosition).Player };
                }
            }

            return possibleWinners.Where(p => p.Kickers[kickerCount - 1].Rank == highestRankAtPosition).Select(k => k.Player).ToList();
        }

        protected class KickersInPlayersHand
        {
            public KickersInPlayersHand(IPlayer player, List<Card> kickers)
            {
                Player = player;
                Kickers = kickers;
            }

            public IPlayer Player { get; }
            public List<Card> Kickers { get; set; }
        }
    }
}
