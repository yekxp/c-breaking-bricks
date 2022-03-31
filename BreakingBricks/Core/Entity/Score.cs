using System;

namespace Core.ScoreService
{
    //Class pre nastavanie udajov hraca, [Serializable] pre ulozenie udajov do file bin formatu 
    [Serializable]
    public class Score
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Points { get; set; }   
    }
}