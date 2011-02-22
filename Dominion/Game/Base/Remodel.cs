using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Remodel : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Remodel; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Remodel"; }
        }

        public int Cost
        {
            get { return 4; }
        }

        #endregion
    }
}
