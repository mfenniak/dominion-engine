using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    interface ITreasureCard : ICard
    {
        int TreasureValue
        {
            get;
        }
    }
}
