using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Province : IVictoryCard
    {
        #region IVictoryCard Members

        public int VictoryPoints
        {
            get { return 6; }
        }

        #endregion

        #region ICard Members

        public CardType Type
        {
            get { return CardType.Victory; }
        }

        public string Name
        {
            get { return "Province"; }
        }

        public int Cost
        {
            get { return 8; }
        }

        public Enum CardEnum
        {
            get { return Cards.Province; }
        }

        #endregion
    }
}
