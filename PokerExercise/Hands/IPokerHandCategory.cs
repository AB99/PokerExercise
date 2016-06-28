using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExercise.Hands
{
    public interface IPokerHandCategory
    {
        bool Applies(IPlayer player);
        List<IPlayer> FindWinningPlayersInThisCategory(List<IPlayer> players);
    }
}
