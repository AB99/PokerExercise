using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise.Hands
{
    public class OnePair : IPokerHandCategory
    {
        public bool Applies(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            return player.Hand.Any(card => player.Hand.Count(c => c.Rank == card.Rank) > 1);
        }

        public List<IPlayer> FindWinningHandsInThisCategory(List<IPlayer> players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (players.Any(p => p == null))
                throw new ArgumentException(nameof(players) + " can not contain null", nameof(players));

            if (players.Count == 0)
                return players;

            throw new NotImplementedException();
        }
    }
}
