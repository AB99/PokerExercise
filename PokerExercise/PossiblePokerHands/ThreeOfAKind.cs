using System;
using System.Collections.Generic;

namespace PokerExercise.PossiblePokerHands
{
    public class ThreeOfAKind : KickerDependantHand, IPossiblePokerHand
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
