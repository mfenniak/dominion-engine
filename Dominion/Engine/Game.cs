using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame = Dominion.Game.Base;

namespace Dominion.Engine
{
    public class Game : IGame
    {
        private IList<KeyValuePair<IAI, Player>> players = new List<KeyValuePair<IAI, Player>>();
        private IDictionary<Enum, IList<ICard>> stacks = new Dictionary<Enum, IList<ICard>>();
        private Random random;

        public Game(Random random)
        {
            this.random = random;
        }

        public Random Random
        {
            get { return random; }
        }

        public int AddPlayer(IAI ai)
        {
            players.Add(new KeyValuePair<IAI, Player>(ai, new Player(this)));
            return players.Count - 1;
        }

        internal IEnumerable<KeyValuePair<IAI, Player>> Players
        {
            get { return players; }
        }

        public void AddActionCard(Type cardType)
        {
            IList<ICard> stack = new List<ICard>();
            Enum cardEnumValue = null;
            for (int i = 0; i < 10; i++)
            {
                ICard card = (ICard)Activator.CreateInstance(cardType);
                if (i == 0)
                    cardEnumValue = card.CardEnum;
                stack.Add(card);
            }
            stacks[cardEnumValue] = stack;
        }

        private void CreateBaseStacks()
        {
            stacks[BaseGame.Cards.Copper] = new List<ICard>();
            stacks[BaseGame.Cards.Silver] = new List<ICard>();
            stacks[BaseGame.Cards.Gold] = new List<ICard>();
            stacks[BaseGame.Cards.Estate] = new List<ICard>();
            stacks[BaseGame.Cards.Duchy] = new List<ICard>();
            stacks[BaseGame.Cards.Province] = new List<ICard>();
            stacks[BaseGame.Cards.Curse] = new List<ICard>();

            int numVictory = 0;
            if (players.Count <= 2)
                numVictory = 8;
            else
                numVictory = 12;
            int numEstates = (3 * players.Count) + numVictory;

            for (int i = 0; i < numEstates; i++)
                stacks[BaseGame.Cards.Estate].Add(new BaseGame.Estate());
            for (int i = 0; i < numVictory; i++)
                stacks[BaseGame.Cards.Duchy].Add(new BaseGame.Duchy());
            for (int i = 0; i < numVictory; i++)
                stacks[BaseGame.Cards.Province].Add(new BaseGame.Province());

            for (int i = 0; i < 60; i++)
                stacks[BaseGame.Cards.Copper].Add(new BaseGame.Copper());
            for (int i = 0; i < 40; i++)
                stacks[BaseGame.Cards.Silver].Add(new BaseGame.Silver());
            for (int i = 0; i < 30; i++)
                stacks[BaseGame.Cards.Gold].Add(new BaseGame.Gold());

            int numCurse = 0;
            if (players.Count == 2)
                numCurse = 10;
            else if (players.Count == 3)
                numCurse = 20;
            else if (players.Count == 4)
                numCurse = 30;
            for (int i = 0; i < numCurse; i++)
                stacks[BaseGame.Cards.Curse].Add(new BaseGame.Curse());
        }

        public int RunGame()
        {
            CreateBaseStacks();
            foreach (KeyValuePair<IAI, Player> kvp in players)
                kvp.Value.Deal();

            while (!GameIsOver())
            {
                foreach (KeyValuePair<IAI, Player> kvp in players)
                {
                    Turn turn = new Turn(this, kvp.Value);

                    turn.ActionPhase();
                    kvp.Key.ActionPhase(this, turn, kvp.Value);

                    turn.BuyPhase();
                    kvp.Key.BuyPhase(this, turn, kvp.Value);

                    kvp.Value.DoCleanup();

                    if (GameIsOver())
                        break;
                }
            }

            int[] vps = new int[players.Count];
            int maxvp = 0;
            for (int i = 0; i < players.Count; i++)
            {
                vps[i] = players[i].Value.CountVictoryPoints();
                maxvp = Math.Max(vps[i], maxvp);
            }

            int pc = 0;
            int winnerIdx = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (vps[i] == maxvp)
                {
                    ++pc;
                    winnerIdx = i;
                }
            }

            if (pc == 1)
            {
                // outright winner based upon victory points
                return winnerIdx;
            }

            int minturns = int.MaxValue;
            for (int i = 0; i < players.Count; i++)
                minturns = Math.Min(players[i].Value.Turns, minturns);
            pc = 0;
            winnerIdx = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Value.Turns == minturns && vps[i] == maxvp)
                {
                    ++pc;
                    winnerIdx = i;
                }
            }
            if (pc == 1)
            {
                // outright winner based upon tied vp & fewest turns
                return winnerIdx;
            }

            // tied game
            return -1;
        }

        private bool GameIsOver()
        {
            if (!HasAvailable(BaseGame.Cards.Province))
                return true;

            int empty = 0;
            foreach (KeyValuePair<Enum, IList<ICard>> kvp in stacks)
            {
                if (kvp.Value.Count == 0)
                    ++empty;
            }
            if (empty >= 3)
                return true;

            return false;
        }

        public ICard DrawCard(Enum cardType)
        {
            IList<ICard> cards = stacks[cardType];
            if (cards.Count == 0)
                return null;
            ICard retval = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return retval;
        }

        public void TrashCard(ICard card)
        {
        }

        #region IGame Members

        public bool HasAvailable(Enum cardType)
        {
            IList<ICard> cards;
            if (!stacks.TryGetValue(cardType, out cards))
                return false;
            return cards.Count > 0;
        }

        #endregion
    }
}
