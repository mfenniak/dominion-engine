using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.AI
{
    public class Basic : IAI
    {
        #region IAI Members

        public void ActionPhase(IGame game, ITurn turn, IPlayer player)
        {
        }

        public void BuyPhase(IGame game, ITurn turn, IPlayer player)
        {
            foreach (ICard card in player.Hand)
            {
                if ((card.Type & CardType.Treasure) == CardType.Treasure)
                    turn.PlayTreasure(card);
            }

            for (int i = 0; i < turn.Buys; i++)
            {
                if (turn.Treasure >= 8 && game.HasAvailable(Game.Base.Cards.Province))
                    turn.BuyCard(Game.Base.Cards.Province);
                else if (turn.Treasure >= 6 && game.HasAvailable(Game.Base.Cards.Gold))
                    turn.BuyCard(Game.Base.Cards.Gold);
                else if (turn.Treasure >= 3 && game.HasAvailable(Game.Base.Cards.Silver))
                    turn.BuyCard(Game.Base.Cards.Silver);
            }
        }

        #endregion
    }
}
