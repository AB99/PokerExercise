using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise.Hands
{
    public class HighCard : IPokerHandCategory
    {
        public bool Applies(IPlayer player)
        {
            return true;
        }

        public List<IPlayer> FindWinningHandsInThisCategory(List<IPlayer> players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (players.Any(p => p == null))
                throw new ArgumentException(nameof(players) + " can not contain null", nameof(players));

            if (players.Count == 0)
                return players;

            var handSize = players.First().Hand.Count();

            if (players.Any(p => p.Hand.Count() != handSize))
                throw new ArgumentException(nameof(players) + " have different sized hands", nameof(players));

            List<IPlayer> possibleWinners = players;
            Rank? highestRankAtPosition = null;

            for (int i = 0; i < handSize; i++)
            {
                highestRankAtPosition = possibleWinners.Select(p => p.Hand[i].Rank).Max();
                possibleWinners = possibleWinners.Where(p => p.Hand[i].Rank == highestRankAtPosition).ToList();

                if (possibleWinners.Count(p => p.Hand[i].Rank == highestRankAtPosition) == 1)
                {
                    return new List<IPlayer> {possibleWinners.Single(p => p.Hand[i].Rank == highestRankAtPosition)};
                }

            }

            return players.Where(p => p.Hand[handSize - 1].Rank == highestRankAtPosition).ToList();
        }
    }
}
