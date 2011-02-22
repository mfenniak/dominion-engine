using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    public enum CardType
    {
        Treasure = (1 << 0),
        Victory =  (1 << 1),
        Action =   (1 << 2),
        Attack =   (1 << 3),
        Reaction = (1 << 4),
    }

    enum TurnPhase
    {
        Action,
        Buy,
    }
}
