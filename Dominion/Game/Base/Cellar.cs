using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Cellar : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            IEnumerable<ICard> cards = (IEnumerable<ICard>)sidedata;
            int count = 0;
            foreach (ICard card in cards)
            {
                if (Object.ReferenceEquals(card, this))
                    throw new Exception("Attempted to discard the Cellar card that was being played");
                player.DiscardFromHand(card);
                ++count;
            }
            player.Draw(count);
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Cellar; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Cellar"; }
        }

        public int Cost
        {
            get { return 2; }
        }

        #endregion
    }
}
