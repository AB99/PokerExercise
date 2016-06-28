using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerExercise.PossiblePokerHands
{
    public class OnePair : KickerDependantHand, IPossiblePokerHand
    {
        public bool PlayerHasHand(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            return player.Cards.Any(card => player.Cards.Count(c => c.Rank == card.Rank) > 1);
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

            //Calculate the highest value pair each player has
            Rank? highestPair = null;

            foreach (IPlayer player in players)
            {
                Rank playersHighestPair = player.Cards.Where(card => player.Cards.Count(c => c.Rank == card.Rank) > 1)
                    .Select(c => c.Rank)
                    .Max();

                if (playersHighestPair > highestPair || highestPair == null)
                {
                    highestPair = playersHighestPair;
                }
            }

            //Select those players who have a pair of that value
            List<IPlayer> possibleWinners = players.Where(player => player.Cards.Count(c => c.Rank == highestPair) > 1).ToList();

            //If there's a clear winner, return it, otherwise we have to look at the kickers
            if (possibleWinners.Count == 1)
                return possibleWinners;

            List<KickersInPlayersHand> kickersToCompare = 
                possibleWinners.Select(p => new KickersInPlayersHand(p, new List<Card>(p.Cards))).ToList();

            //Remove the higheset value pair from the kickers to be compared
            foreach (var kicker in kickersToCompare)
            {
                List<Card> kickersToRemove = kicker.Kickers.Where(k => k.Rank == highestPair).ToList();
                kicker.Kickers.Remove(kickersToRemove[0]);
                kicker.Kickers.Remove(kickersToRemove[1]);
            }

            //Choose a winner based off the kickers
            return FindPlayersWithWinningKickers(kickersToCompare);
        }
    }
}