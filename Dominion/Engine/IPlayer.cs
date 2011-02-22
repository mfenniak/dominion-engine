using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    public interface IPlayer
    {
        IEnumerable<ICard> Hand { get; }
    }
}
