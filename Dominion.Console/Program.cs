using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Engine;

namespace Dominion.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ComparisonPlay();
            //OptimizationLoop();
        }

        private static void ComparisonPlay()
        {
            IAI ai1 = new Dominion.AI.Mathieu.Actions1();
            IAI ai2 = new Dominion.AI.Basic();

            int[] wins = new int[] { 0, 0 };
            int ties = 0;
            Random random = new Random();

            for (int i = 0; i < 50000; i++)
            {
                Dominion.Engine.Game game = new Dominion.Engine.Game(random);
                game.AddActionCard(typeof(Dominion.Game.Base.Cellar));
                game.AddActionCard(typeof(Dominion.Game.Base.Market));
                game.AddActionCard(typeof(Dominion.Game.Base.Militia));
                game.AddActionCard(typeof(Dominion.Game.Base.Mine));
                game.AddActionCard(typeof(Dominion.Game.Base.Moat));
                game.AddActionCard(typeof(Dominion.Game.Base.Remodel));
                game.AddActionCard(typeof(Dominion.Game.Base.Smithy));
                game.AddActionCard(typeof(Dominion.Game.Base.Village));
                game.AddActionCard(typeof(Dominion.Game.Base.Woodcutter));
                game.AddActionCard(typeof(Dominion.Game.Base.Workshop));

                int p1idx, p2idx;
                if ((i % 2) == 0)  // alternate player start for fairness
                {
                    p1idx = game.AddPlayer(ai1);
                    p2idx = game.AddPlayer(ai2);
                }
                else
                {
                    p2idx = game.AddPlayer(ai2);
                    p1idx = game.AddPlayer(ai1);
                }

                int winneridx = game.RunGame();
                if (winneridx == p1idx)
                    wins[0] += 1;
                else if (winneridx == p2idx)
                    wins[1] += 1;
                else
                    ties += 1;
            }

            System.Console.WriteLine("{0} wins for player 1 ({1})", wins[0], ai1.GetType());
            System.Console.WriteLine("{0} wins for player 2 ({1})", wins[1], ai2.GetType());
            System.Console.WriteLine("{0} ties", ties);
        }

        private static void OptimizationLoop()
        {
            Dominion.AI.Mathieu.Actions1 ai1 = new Dominion.AI.Mathieu.Actions1();
            IAI ai2 = new Dominion.AI.Basic();

            Random random = new Random();

            string key = "CardsPerWoodcutter";
            double min = 10;
            double max = 20;
            double step = 1;

            int count = (int)((max - min) / step);
            for (int j = 0; j < count; j++)
            {
                ai1.TuningParameters.Set(key, min + (step * j));
                System.Console.WriteLine("{0} = {1}", key, ai1.TuningParameters.Get<double>(key));

                int[] wins = new int[] { 0, 0 };
                int ties = 0;

                for (int i = 0; i < 25000; i++)
                {
                    Dominion.Engine.Game game = new Dominion.Engine.Game(random);
                    game.AddActionCard(typeof(Dominion.Game.Base.Cellar));
                    game.AddActionCard(typeof(Dominion.Game.Base.Market));
                    game.AddActionCard(typeof(Dominion.Game.Base.Militia));
                    game.AddActionCard(typeof(Dominion.Game.Base.Mine));
                    game.AddActionCard(typeof(Dominion.Game.Base.Moat));
                    game.AddActionCard(typeof(Dominion.Game.Base.Remodel));
                    game.AddActionCard(typeof(Dominion.Game.Base.Smithy));
                    game.AddActionCard(typeof(Dominion.Game.Base.Village));
                    game.AddActionCard(typeof(Dominion.Game.Base.Woodcutter));
                    game.AddActionCard(typeof(Dominion.Game.Base.Workshop));

                    int p1idx, p2idx;
                    if ((i % 2) == 0)  // alternate player start for fairness
                    {
                        p1idx = game.AddPlayer(ai1);
                        p2idx = game.AddPlayer(ai2);
                    }
                    else
                    {
                        p2idx = game.AddPlayer(ai2);
                        p1idx = game.AddPlayer(ai1);
                    }

                    int winneridx = game.RunGame();
                    if (winneridx == p1idx)
                        wins[0] += 1;
                    else if (winneridx == p2idx)
                        wins[1] += 1;
                    else
                        ties += 1;
                }

                int total = wins[0] + wins[1] + ties;

                System.Console.WriteLine("{0} wins for player 1 ({1:0.0}%)", wins[0], ((double)wins[0] * 100) / total);
                System.Console.WriteLine("{0} wins for player 2 ({1:0.0}%)", wins[1], ((double)wins[1] * 100) / total);
                System.Console.WriteLine("{0} ties ({1:0.0}%)", ties, ((double)ties * 100) / total);
            }
        }
    }
}
