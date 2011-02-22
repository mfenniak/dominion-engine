using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Woodcutter : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            turn.AddTreasure(2);
            turn.AddBuys(1);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Woodcutter; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Woodcutter"; }
        }

        public int Cost
        {
            get { return 3; }
        }

        #endregion
    }
}
