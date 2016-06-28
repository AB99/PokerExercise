using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerExercise.PossiblePokerHands;

namespace PokerExercise.Hands
{
    public class Flush : KickerDependantHand, IPossiblePokerHand
    {
        public bool PlayerHasHand(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public List<IPlayer> FindWinningPlayersWithThisHand(List<IPlayer> players)
        {
            throw new NotImplementedException();
        }
    }
}
