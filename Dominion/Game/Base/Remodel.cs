using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Remodel : IActionCard
    {
        public struct RemodelData
        {
            public ICard Card;
            public Enum TargetType;
        }

        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            RemodelData upgradeData = (RemodelData)sidedata;
            ICard origCard = (ICard)upgradeData.Card;

            ICard newCard = game.DrawCard(upgradeData.TargetType);
            if (newCard == null)
                throw new Exception("Selected target type is not available");

            if ((origCard.Cost + 2) < newCard.Cost)
                throw new Exception("Target card is too expensive to remodel");

            game.TrashCard(origCard);
            player.AddDiscard(newCard);
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
