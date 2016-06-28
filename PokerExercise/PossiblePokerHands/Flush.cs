using System;
using System.Collections.Generic;
using System.Linq;
using PokerExercise.PossiblePokerHands;

namespace PokerExercise.Hands
{
    public class Flush : KickerDependantHand, IPossiblePokerHand
    {
        public bool PlayerHasHand(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                if (player.Cards.All(c => c.Suit == suit))
                    return true;
            }

            return false;
        }

        public List<IPlayer> FindWinningPlayersWithThisHand(List<IPlayer> players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (players.Any(p => p == null))
                throw new ArgumentException(nameof(players) + " can not contain null", nameof(players));

            if (players.Count == 0)
                return players;

            players = players.Where(PlayerHasHand).ToList();

            var kickersToCompare = players.Select(player => new KickersInPlayersHand(player, new List<Card>(player.Cards))).ToList();
            return FindPlayersWithWinningKickers(kickersToCompare);
        }
    }
}
