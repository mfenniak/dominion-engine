using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Moat : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Moat; }
        }

        public CardType Type
        {
            get { return CardType.Action | CardType.Reaction; }
        }

        public string Name
        {
            get { return "Moat"; }
        }

        public int Cost
        {
            get { return 2; }
        }

        #endregion
    }
}
