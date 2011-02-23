using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Chapel : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            IEnumerable<ICard> cards = (IEnumerable<ICard>)sidedata;
            int count = 0;
            foreach (ICard card in cards)
            {
                if (Object.ReferenceEquals(card, this))
                    throw new Exception("Attempted to trash the Chapel card that was being played");
                if (count == 4)
                    throw new Exception("Attempted to trash more than 4 cards permitted by Chapel");
                player.TrashCard(card);
                ++count;
            }
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Chapel; }
        }

        public CardType Type
        {
            get { return CardType.Action; }
        }

        public string Name
        {
            get { return "Chapel"; }
        }

        public int Cost
        {
            get { return 2; }
        }

        #endregion
    }
}
