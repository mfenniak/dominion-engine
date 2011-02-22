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
            IAI ai1 = new Dominion.AI.Actions1();
            IAI ai2 = new Dominion.AI.Basic();

            int[] wins = new int[] { 0, 0 };
            int ties = 0;
            Random random = new Random();

            for (int i = 0; i < 50000; i++)
            {
                Dominion.Engine.Game game = new Dominion.Engine.Game(random);
                game.AddActionCard(typeof(Dominion.Game.Base.Woodcutter));

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
    }
}
