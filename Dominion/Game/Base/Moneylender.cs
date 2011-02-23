using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Moneylender : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            Copper copper = sidedata as Copper;
            if (copper == null)
                throw new Exception("Moneylender needs a copper");

            player.TrashCard(copper);
            turn.AddTreasure(3);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Moneylender; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Moneylender"; }
        }

        public int Cost
        {
            get { return 4; }
        }

        #endregion
    }
}
