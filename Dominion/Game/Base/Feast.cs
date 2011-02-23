using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Feast : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            Enum newCardType = (Enum)sidedata;

            ICard newCard = game.DrawCard(newCardType);
            if (newCard == null)
                throw new Exception("Selected target type is not available");

            if (newCard.Cost > 5)
                throw new Exception("Target card is too expensive for Workshop");

            player.AddDiscard(newCard);
            player.TrashCard(this);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Feast; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Feast"; }
        }

        public int Cost
        {
            get { return 4; }
        }

        #endregion
    }
}
