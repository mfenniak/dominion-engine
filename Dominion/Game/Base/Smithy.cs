using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Smithy : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            player.Draw(3);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Smithy; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Smithy"; }
        }

        public int Cost
        {
            get { return 4; }
        }

        #endregion
    }
}
