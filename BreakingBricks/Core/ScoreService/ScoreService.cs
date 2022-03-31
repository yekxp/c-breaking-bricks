using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Core.ScoreService
{
    public class ScoreService : IScoreService
    {
        
      
        private const string FileName = "score.bin";
        public List<Score> scores = new List<Score>();

        public void AddScore(Score score)
        {
            scores.Add(score);
            SaveScores();
        }
        
        public double ScoreCounter(int brickCounter)
        {
            return brickCounter * 5 + Math.Pow(brickCounter, 2);
        }
        
        public void PrintScore()
        {
            LoadScores();
            var sorted = scores.OrderBy(p => p.Points).ThenBy(p => p.Name);
            Console.WriteLine("*********HIGH SCORE*********");
            Console.WriteLine();
            foreach (var t in sorted.Reverse())
            {
                Console.WriteLine(t.Name);
                Console.WriteLine("Points: " + t.Points);
                Console.WriteLine("----------------");
                Console.WriteLine();
            }
        }

        public void ResetScore()
        {
            scores = new List<Score>();
            File.Delete(FileName);
        }
        
        public void SaveScores()
        {
            using var fs = File.OpenWrite(FileName);
            var bf = new BinaryFormatter();
            bf.Serialize(fs, scores);
        }
        
        public void LoadScores()
        {
            if (!File.Exists(FileName)) return;
            using var fs = File.OpenRead(FileName);
            var bf = new BinaryFormatter();
            scores = (List<Score>)bf.Deserialize(fs);
        }
    }
}