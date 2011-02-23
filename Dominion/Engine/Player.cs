using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame = Dominion.Game.Base;
using System.Collections.ObjectModel;

namespace Dominion.Engine
{
    class Player : IPlayer
    {
        private Game game;
        private List<ICard> deck = new List<ICard>();
        private List<ICard> hand = new List<ICard>();
        private List<ICard> discard = new List<ICard>();
        private List<ICard> inplay = new List<ICard>();
        private int turns = 0;

        private KeyValueStore state = new KeyValueStore();

        public Player(Game game)
        {
            this.game = game;
        }

        public void Deal()
        {
            for (int i = 0; i < 7; i++)
                discard.Add(game.DrawCard(BaseGame.Cards.Copper));
            for (int i = 0; i < 3; i++)
                discard.Add(game.DrawCard(BaseGame.Cards.Estate));
            DoCleanup();
            turns = 0;
        }

        public void DoCleanup()
        {
            foreach (ICard card in inplay)
                discard.Add(card);
            foreach (ICard card in hand)
                discard.Add(card);
            inplay.Clear();
            hand.Clear();
            Draw(5);
            turns += 1;
        }

        public void Draw(int cnt)
        {
            for (int i = 0; i < cnt; i++)
            {
                ICard card = DrawCard();
                if (card != null)
                    hand.Add(card);
            }
        }

        private ICard DrawCard()
        {
            if (deck.Count == 0)
            {
                if (discard.Count == 0)
                    return null;

                int count = discard.Count;
                for (int c = 0; c < count; c++)
                {
                    int idx = game.Random.Next(discard.Count);
                    ICard card = discard[idx];
                    discard.RemoveAt(idx);
                    deck.Add(card);
                }
            }

            ICard retval = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return retval;
        }

        public void InPlay(ICard card)
        {
            if (!hand.Contains(card))
                throw new Exception("Attempted to play a card that's not in current hand");

            hand.Remove(card);
            inplay.Add(card);
        }

        public void DiscardFromHand(ICard card)
        {
            if (!hand.Contains(card))
                throw new Exception("Attempted to play a card that's not in current hand");
            hand.Remove(card);
            discard.Add(card);
        }
		
        public void TrashCard(ICard card)
        {
            if (!hand.Contains(card))
                throw new Exception("Attempted to trash a card not in-hand");
            hand.Remove(card);
            game.TrashCard(card);
        }
        
        public void AddToHand(ICard card)
        {
            hand.Add(card);
        }

        public void AddDiscard(ICard card)
        {
            discard.Add(card);
        }

        public int CountVictoryPoints()
        {
            int value = 0;
            value += CountVictoryPoints(hand);
            value += CountVictoryPoints(deck);
            value += CountVictoryPoints(discard);
            return value;
        }

        private int CountVictoryPoints(IList<ICard> cards)
        {
            int retval = 0;
            foreach (ICard card in cards)
            {
                IVictoryCard vc = card as IVictoryCard;
                if (vc != null)
                    retval += vc.VictoryPoints;
            }
            return retval;
        }

        public int Turns
        {
            get { return turns; }
        }

        #region IPlayer Members

        public IEnumerable<ICard> Hand
        {
            // Copy array so that it can be modified during enumeration
            get { return new List<ICard>(hand.ToArray()); }
        }

        public KeyValueStore State
        {
            get { return state; }
        }

        #endregion
    }
}
