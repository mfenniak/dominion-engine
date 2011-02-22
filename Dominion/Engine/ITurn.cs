using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    public interface ITurn
    {
        int Actions
        {
            get;
        }

        int Buys
        {
            get;
        }

        int Treasure
        {
            get;
        }

        void PlayAction(ICard card, object sidedata);
        void PlayTreasure(ICard card);
        void BuyCard(Enum cardType);
    }
}
