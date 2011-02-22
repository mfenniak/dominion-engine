using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;
using Dominion.Game.Base;

namespace Dominion.AI.Mathieu
{
    public class Actions1 : IAI
    {
        private KeyValueStore param = new KeyValueStore();
        public KeyValueStore TuningParameters
        {
            get { return param; }
        }

        private int Count(IPlayer player, string key)
        {
            return player.State.Get("Count_" + key, key == "Cards" ? 10 : 0);
        }

        private void Increment(IPlayer player, string key)
        {
            player.State.Set("Count_" + key, Count(player, key) + 1);
        }

        public void ActionPhase(IGame game, ITurn turn, IPlayer player)
        {
            ICard card;

            while (turn.Actions > 0)
            {
                card = Utils.GetCard<Village>(player.Hand);
                if (card != null)
                {
                    turn.PlayAction(card, null);
                    continue;
                }

                card = Utils.GetCard<Market>(player.Hand);
                if (card != null)
                {
                    turn.PlayAction(card, null);
                    continue;
                }

                card = Utils.GetCard<Cellar>(player.Hand);
                if (card != null)
                {
                    IList<ICard> discards = new List<ICard>();
                    foreach (ICard cardInHand in player.Hand)
                    {
                        if (!Object.ReferenceEquals(cardInHand, card))
                        {
                            if ((cardInHand.Type & CardType.Victory) == CardType.Victory &&
                                (cardInHand.Type & CardType.Action) != CardType.Action)
                                discards.Add(cardInHand);
                            else if ((Game.Base.Cards)cardInHand.CardEnum == Game.Base.Cards.Cellar)
                                discards.Add(cardInHand);
                        }
                    }
                    if (discards.Count > 0)
                    {
                        turn.PlayAction(card, discards);
                        continue;
                    }
                }

                card = Utils.GetCard<Smithy>(player.Hand);
                if (card != null)
                {
                    turn.PlayAction(card, null);
                    continue;
                }

                card = Utils.GetCard<Mine>(player.Hand);
                if (card != null)
                {
                    ICard targetCard = Utils.GetCard<Copper>(player.Hand);
                    if (targetCard != null && game.HasAvailable(Game.Base.Cards.Silver))
                    {
                        turn.PlayAction(card, new Mine.MineData { Card = targetCard, TargetType = Game.Base.Cards.Silver });
                        continue;
                    }

                    targetCard = Utils.GetCard<Silver>(player.Hand);
                    if (targetCard != null && game.HasAvailable(Game.Base.Cards.Gold))
                    {
                        turn.PlayAction(card, new Mine.MineData { Card = targetCard, TargetType = Game.Base.Cards.Gold });
                        continue;
                    }
                }

                card = Utils.GetCard<Militia>(player.Hand);
                if (card != null)
                {
                    turn.PlayAction(card, null);
                    continue;
                }

                card = Utils.GetCard<Woodcutter>(player.Hand);
                if (card != null)
                {
                    turn.PlayAction(card, null);
                    continue;
                }

                card = Utils.GetCard<Moat>(player.Hand);
                if (card != null)
                {
                    turn.PlayAction(card, null);
                    continue;
                }

                break;
            }
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
                bool bought = true;
                if (turn.Treasure >= 8 && game.HasAvailable(Game.Base.Cards.Province))
                    turn.BuyCard(Game.Base.Cards.Province);
                else if (turn.Treasure >= 6 && game.HasAvailable(Game.Base.Cards.Gold))
                    turn.BuyCard(Game.Base.Cards.Gold);
                else if (turn.Treasure >= 5 && game.HasAvailable(Game.Base.Cards.Mine) && (Count(player, "Mine") / Count(player, "Cards")) < TuningParameters.Get<double>("MineRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Mine);
                    Increment(player, "Mine");
                }
                else if (turn.Treasure >= 5 && game.HasAvailable(Game.Base.Cards.Market) && (Count(player, "Market") / Count(player, "Cards")) < TuningParameters.Get<double>("MarketRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Market);
                    Increment(player, "Market");
                }
                else if (turn.Treasure >= 4 && game.HasAvailable(Game.Base.Cards.Smithy) && (Count(player, "Smithy") / Count(player, "Cards")) < TuningParameters.Get<double>("SmithyRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Smithy);
                    Increment(player, "Smithy");
                }
                else if (turn.Treasure >= 4 && game.HasAvailable(Game.Base.Cards.Militia) && (Count(player, "Militia") / Count(player, "Cards")) < TuningParameters.Get<double>("MilitiaRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Militia);
                    Increment(player, "Militia");
                }
                else if (turn.Treasure >= 3 && game.HasAvailable(Game.Base.Cards.Village) && (Count(player, "Village") / Count(player, "Cards")) < TuningParameters.Get<double>("VillageRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Village);
                    Increment(player, "Village");
                }
                else if (turn.Treasure >= 3 && game.HasAvailable(Game.Base.Cards.Woodcutter) && (Count(player, "Woodcutter") / Count(player, "Cards")) < TuningParameters.Get<double>("WoodcutterRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Woodcutter);
                    Increment(player, "Woodcutter");
                }
                else if (turn.Treasure >= 3 && game.HasAvailable(Game.Base.Cards.Silver))
                    turn.BuyCard(Game.Base.Cards.Silver);
                else if (turn.Treasure >= 2 && game.HasAvailable(Game.Base.Cards.Cellar) && (Count(player, "Cellar") / Count(player, "Cards")) < TuningParameters.Get<double>("CellarRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Cellar);
                    Increment(player, "Cellar");
                }
                else if (turn.Treasure >= 2 && game.HasAvailable(Game.Base.Cards.Moat) && (Count(player, "Moat") / Count(player, "Cards")) < TuningParameters.Get<double>("MoatRatio", 0))
                {
                    turn.BuyCard(Game.Base.Cards.Moat);
                    Increment(player, "Moat");
                }
                else if (turn.Treasure >= 2 && game.HasAvailable(Game.Base.Cards.Estate))
                    turn.BuyCard(Game.Base.Cards.Estate);
                else
                    bought = false;

                if (bought)
                {
                    Increment(player, "Cards");
                    continue;
                }
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
    }
}
