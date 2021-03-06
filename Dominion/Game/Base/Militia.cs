﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Game.Base
{
    public class Militia : IActionCard
    {
        #region IActionCard Members

        void IActionCard.Play(Dominion.Engine.Game game, Player player, Turn turn, object sidedata)
        {
            turn.AddTreasure(2);

            foreach (KeyValuePair<IAI, Player> opponent in game.Players)
            {
                if (object.ReferenceEquals(opponent.Value, player))
                    continue;

                // FIXME: should have a better way of handling reaction cards that puts
                // the reaction in the reaction card, not the attack card
                bool safe = false;
                foreach (ICard card in opponent.Value.Hand)
                {
                    if (card is Moat)
                    {
                        safe = true;
                        break;
                    }
                }
                if (safe)
                    continue;

                int expectedDiscards = opponent.Value.Hand.Count() - 3;
                if (expectedDiscards != 0)
                {
                    IEnumerable<ICard> discards = opponent.Key.ChooseExternalDiscard(opponent.Value, opponent.Value.Hand.Count() - 3);
                    int count = 0;
                    foreach (ICard card in discards)
                    {
                        opponent.Value.DiscardFromHand(card);
                        ++count;
                    }
                    if (count != expectedDiscards)
                        throw new Exception("ChooseExternalDiscard did not choose correct card count");
                }
            }
        }

        #endregion

        #region ICard Members

        public Enum CardEnum
        {
            get { return Cards.Militia; }
        }

        public CardType Type
        {
            get { return CardType.Action | CardType.Attack; }
        }

        public string Name
        {
            get { return "Militia"; }
        }

        public int Cost
        {
            get { return 4; }
        }

        #endregion
    }
}
