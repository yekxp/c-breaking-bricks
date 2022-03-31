using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ScoreService
{
    public interface IScoreService
    {
        void AddScore(Score score);
        double ScoreCounter(int brickCounter);
    }
}
