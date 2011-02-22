using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Curse : IVictoryCard
    {
        #region IVictoryCard Members

        public int VictoryPoints
        {
            get { return -1; }
        }

        #endregion

        #region ICard Members

        public CardType Type
        {
            get { return CardType.Victory; }
        }

        public string Name
        {
            get { return "Curse"; }
        }

        public int Cost
        {
            get { return 0; }
        }

        public Enum CardEnum
        {
            get { return Cards.Curse; }
        }

        #endregion
    }
}
