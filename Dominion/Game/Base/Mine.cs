using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Mine : IActionCard
    {
        public struct MineData
        {
            public ICard Card;
            public Enum TargetType;
        }

        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            MineData upgradeData = (MineData)sidedata;
            ICard origCard = (ICard)upgradeData.Card;

            if ((origCard.Type & CardType.Treasure) != CardType.Treasure)
                throw new Exception("Mine attempted on a non-treasure card");

            ICard newCard = game.DrawCard(upgradeData.TargetType);
            if (newCard == null)
                throw new Exception("Selected target type is not available for Mine");

            if ((origCard.Cost + 3) < newCard.Cost)
                throw new Exception("Target card is too expensive to mine into");

            player.TrashCard(origCard);
            player.AddToHand(newCard);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Mine; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Mine"; }
        }

        public int Cost
        {
            get { return 5; }
        }

        #endregion
    }
}
