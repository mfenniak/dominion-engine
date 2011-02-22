using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    class Gold : ITreasureCard
    {
        #region ITreasureCard Members

        public int TreasureValue
        {
            get { return 3; }
        }

        #endregion

        #region ICard Members

        public CardType Type
        {
            get { return CardType.Treasure; }
        }

        public string Name
        {
            get { return "Gold"; }
        }

        public int Cost
        {
            get { return 6; }
        }

        public Enum CardEnum
        {
            get { return Cards.Gold; }
        }

        #endregion
    }
}
