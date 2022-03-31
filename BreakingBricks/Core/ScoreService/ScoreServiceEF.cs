using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Core.ScoreService
{
    public class ScoreServiceEF : IScoreService
    {
        public void AddScore(Score score)
        {
            using (var context = new BreakingBricksDbContext())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }

        }

        public double ScoreCounter(int brickCounter)
        {
            return brickCounter * 5 + Math.Pow(brickCounter, 2);
        }
    }
}
