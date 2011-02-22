using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    interface IActionCard : ICard
    {
        void Play(Game game, Player player, Turn turn, object sidedata);
    }
}
