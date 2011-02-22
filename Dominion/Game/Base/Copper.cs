using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Copper : ITreasureCard
    {
        #region ITreasureCard Members

        public int TreasureValue
        {
            get { return 1; }
        }

        #endregion

        #region ICard Members

        public CardType Type
        {
            get { return CardType.Treasure; }
        }

        public string Name
        {
            get { return "Copper"; }
        }

        public int Cost
        {
            get { return 0; }
        }

        public Enum CardEnum
        {
            get { return Cards.Copper; }
        }

        #endregion
    }
}
