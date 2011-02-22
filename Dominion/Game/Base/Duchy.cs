using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    class Duchy : IVictoryCard
    {
        #region IVictoryCard Members

        public int VictoryPoints
        {
            get { return 3; }
        }

        #endregion

        #region ICard Members

        public CardType Type
        {
            get { return CardType.Victory; }
        }

        public string Name
        {
            get { return "Duchy"; }
        }

        public int Cost
        {
            get { return 5; }
        }

        public Enum CardEnum
        {
            get { return Cards.Duchy; }
        }

        #endregion
    }
}
