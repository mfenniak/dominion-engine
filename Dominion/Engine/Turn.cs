using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Engine
{
    class Turn : ITurn
    {
        private TurnPhase phase;
        private Game game;
        private Player player;

        private int actions = 1;
        private int treasure = 0;
        private int buys = 1;

        public Turn(Game game, Player player)
        {
            this.game = game;
            this.player = player;
        }

        public void ActionPhase()
        {
            phase = TurnPhase.Action;
        }

        public void BuyPhase()
        {
            phase = TurnPhase.Buy;
        }

        public void AddBuys(int buys)
        {
            this.buys += buys;
        }

        public void AddTreasure(int treasure)
        {
            this.treasure += treasure;
        }

        #region ITurn Members

        public int Buys
        {
            get { return buys; }
        }

        public int Treasure
        {
            get { return treasure; }
        }

        public int Actions
        {
            get { return actions; }
        }

        public void PlayAction(ICard card, object sidedata)
        {
            if (phase != TurnPhase.Action)
                throw new Exception("Cannot play action cards when not in the action phase");
            else if (actions == 0)
                throw new Exception("No remaining actions");

            IActionCard ac = card as IActionCard;
            if (ac == null)
                throw new Exception("Card does not implement IActionCard");

            ac.Play(game, player, this, sidedata);
        }

        public void PlayTreasure(ICard card)
        {
            if (phase != TurnPhase.Buy)
                throw new Exception("Cannot play treasures when not in the buy phase");

            ITreasureCard tc = card as ITreasureCard;
            if (tc == null)
                throw new Exception("Card does not implement ITreasureCard");

            player.InPlay(card);
            treasure += tc.TreasureValue;
        }

        public void BuyCard(Enum cardType)
        {
            ICard card = game.DrawCard(cardType);
            if (card == null)
                throw new Exception("Card not available");
            else if (card.Cost > treasure)
                throw new Exception("Can't afford that card");
            else if (Buys == 0)
                throw new Exception("No buys remaining");

            treasure -= card.Cost;
            player.AddDiscard(card);
        }

        #endregion
    }
}
