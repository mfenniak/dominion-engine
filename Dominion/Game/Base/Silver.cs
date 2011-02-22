using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    class Silver : ITreasureCard
    {
        #region ITreasureCard Members

        public int TreasureValue
        {
            get { return 2; }
        }

        #endregion

        #region ICard Members

        public CardType Type
        {
            get { return CardType.Treasure; }
        }

        public string Name
        {
            get { return "Silver"; }
        }

        public int Cost
        {
            get { return 3; }
        }

        public Enum CardEnum
        {
            get { return Cards.Silver; }
        }

        #endregion
    }
}
