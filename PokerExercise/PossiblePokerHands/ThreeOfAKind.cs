using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerExercise.PossiblePokerHands
{
    public class ThreeOfAKind : KickerDependantHand, IPossiblePokerHand
    {
        public bool PlayerHasHand(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            return player.Cards.Any(card => player.Cards.Count(c => c.Rank == card.Rank) > 2);        
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

            Rank? highestTriplet = null;

            foreach (IPlayer player in players)
            {
                Rank playersHighestPair = player.Cards.Where(card => player.Cards.Count(c => c.Rank == card.Rank) > 2)
                    .Select(c => c.Rank)
                    .Max();

                if (playersHighestPair > highestTriplet || highestTriplet == null)
                {
                    highestTriplet = playersHighestPair;
                }
            }

            //NB: can't have two hands with the same triplet, so don't have to worry about kickers
            List<IPlayer> possibleWinners = players.Where(player => player.Cards.Count(c => c.Rank == highestTriplet) > 2).ToList();
            return possibleWinners;
        }
    }
}
