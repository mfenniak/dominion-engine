using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    public interface ICard
    {
        Enum CardEnum
        {
            get;
        }

        CardType Type
        {
            get;
        }

        string Name
        {
            get;
        }

        int Cost
        {
            get;
        }
    }
}
