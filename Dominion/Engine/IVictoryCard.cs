using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    interface IVictoryCard : ICard
    {
        int VictoryPoints
        {
            get;
        }
    }
}
