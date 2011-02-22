using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    public interface IAI
    {
        void ActionPhase(IGame game, ITurn turn, IPlayer player);
        void BuyPhase(IGame game, ITurn turn, IPlayer player);
    }
}
