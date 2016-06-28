﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise.Hands
{
    public class OnePair : PokerHandCategory, IPokerHandCategory
    {
        public bool Applies(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            return player.Hand.Any(card => player.Hand.Count(c => c.Rank == card.Rank) > 1);
        }

        public List<IPlayer> FindWinningPlayersInThisCategory(List<IPlayer> players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (players.Any(p => p == null))
                throw new ArgumentException(nameof(players) + " can not contain null", nameof(players));

            if (players.Count == 0)
                return players;

            players = players.Where(Applies).ToList();

            //Find highest pair among the players
            Rank? highestPair = null;

            foreach (IPlayer player in players)
            {
                Rank playersHighestPair = player.Hand.Where(card => player.Hand.Count(c => c.Rank == card.Rank) > 1)
                    .Select(c => c.Rank)
                    .Max();

                if (playersHighestPair > highestPair || highestPair == null)
                {
                    highestPair = playersHighestPair;
                }
            }

            List<IPlayer> possibleWinners = players.Where(player => player.Hand.Count(c => c.Rank == highestPair) > 1).ToList();
            List<KickersInPlayersHand> kickersToCompare = 
                possibleWinners.Select(p => new KickersInPlayersHand(p, new List<Card>(p.Hand))).ToList();

            foreach (var kicker in kickersToCompare)
            {
                List<Card> kickersToRemove = kicker.Kickers.Where(k => k.Rank == highestPair).ToList();
                kicker.Kickers.Remove(kickersToRemove[0]);
                kicker.Kickers.Remove(kickersToRemove[1]);
            }

            return FindPlayersWithWinningKickers(kickersToCompare);
        }
    }
}