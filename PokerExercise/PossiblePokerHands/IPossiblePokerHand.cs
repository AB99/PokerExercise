using System.Collections.Generic;

namespace PokerExercise.PossiblePokerHands
{
    public interface IPossiblePokerHand
    {
        bool PlayerHasHand(IPlayer player);
        List<IPlayer> FindWinningPlayersWithThisHand(List<IPlayer> players);
    }
}
