﻿using System;
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

            while (turn.Buys > 0)
            {
                if (turn.Treasure >= 8 && game.HasAvailable(Game.Base.Cards.Province))
                    turn.BuyCard(Game.Base.Cards.Province);
                else if (turn.Treasure >= 6 && game.HasAvailable(Game.Base.Cards.Gold))
                    turn.BuyCard(Game.Base.Cards.Gold);
                else if (turn.Treasure >= 3 && game.HasAvailable(Game.Base.Cards.Silver))
                    turn.BuyCard(Game.Base.Cards.Silver);
                else
                    break;
            }
        }

        public IEnumerable<ICard> ChooseExternalDiscard(IPlayer player, int numCards)
        {
            IList<ICard> retval = new List<ICard>();

            foreach (ICard card in player.Hand)
            {
                if ((card.Type & CardType.Treasure) != CardType.Treasure)
                    retval.Add(card);
                if (retval.Count == numCards)
                    break;
            }

            if (retval.Count == numCards)
                return retval;

            foreach (ICard card in player.Hand)
            {
                if (!retval.Contains(card))
                    retval.Add(card);
                if (retval.Count == numCards)
                    break;
            }

            if (retval.Count != numCards)
                throw new Exception("Unable to discard the number of cards selected");

            return retval;
        }

        #endregion
    }
}
