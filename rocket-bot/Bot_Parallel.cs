using System;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var tasks = Enumerable.Range(1, threadsCount)
               .Select(i => Task.Run(() =>
               SearchBestMove(rocket, new Random(random.Next()), iterationsCount)));

            Task.WhenAll(tasks);
            var t = tasks.OrderBy(z => z.Result.Item2).Last();
            
            var bestMove = t.Result;
            var newRocket = rocket.Move(bestMove.Item1, level);
            return newRocket;
        }
    }
}