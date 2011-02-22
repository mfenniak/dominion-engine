using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Village : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            player.Draw(1);
            turn.AddActions(2);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Village; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Village"; }
        }

        public int Cost
        {
            get { return 3; }
        }

        #endregion
    }
}
