using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion
{
    static class Utils
    {
        public static T GetCard<T>(IEnumerable<ICard> cards) where T : class
        {
            foreach (ICard card in cards)
            {
                T retval = card as T;
                if (retval != null)
                    return retval;
            }
            return null;
        }
    }
}
