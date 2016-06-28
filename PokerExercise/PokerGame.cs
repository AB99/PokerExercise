using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise
{
    public class PokerGame
    {
        public IPlayer CalculateWinner(IList<IPlayer> players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (players.Any(p => p == null))
                throw new ArgumentNullException(nameof(players));

            throw new NotImplementedException();
        }
    }
}
