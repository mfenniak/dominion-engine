using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Market : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            turn.AddBuys(1);
            turn.AddTreasure(1);
            turn.AddActions(1);
            turn.AddCards(1);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Market; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Market"; }
        }

        public int Cost
        {
            get { return 5; }
        }

        #endregion
    }
}
