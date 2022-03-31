using Core;
using Core.Core;
using Core.ScoreService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        //Testovanie funkcie AddScore, platne meno, score, comment a review
        [TestMethod]
        public void TestAddScore()
        {

            var score = new ScoreService();
            Score player = new Score{ Id = 1, Name = "Jakub", Points = 1500 };
            score.AddScore(player);
            Assert.AreEqual("Jakub", player.Name);
            Assert.AreEqual(1500, player.Points);
            Assert.AreEqual(1, player.Id);
        }
        
        
        //Testovanie reset score a vymazanie filu
        [TestMethod]
        public void TestResetScore()
        {
            var service = new ScoreService();
            service.ResetScore();
            Assert.AreEqual(0, service.scores.Count);
        
        }
        
        
        //Testovanie ci vytvoreny file nie je vyrieseny
        [TestMethod]
        public void TestFillField()
        {
            
            var service = new Field(10,10);
            service.FillField();
            Assert.AreEqual(false, service.IsSolved());
            
        }
        
        
        //Testovanie navratovej hodnoty podla zadanych parametrov pre metodu MarkBrick
        [TestMethod]
        public void TestMarking()
        {
            
            var service = new Field(10,10);
            service.FillField();
            Assert.AreEqual(99,  service.MarkBrick(11, -1, 0, 100));
            Assert.AreEqual(99,  service.MarkBrick(-1, 9, 0, 100));
            Assert.AreNotEqual(99, service.MarkBrick(0, 9, 0, 100));
        }
        
        
        [TestMethod]
        public void TestScoreCounter()
        {
            
            var service = new ScoreService();
            Assert.AreEqual(50,  service.ScoreCounter(5));
            Assert.AreEqual(0,  service.ScoreCounter(0));
            Assert.AreEqual(1005000,  service.ScoreCounter(1000));
        }
        
        //Test ci vytvorene pole neobsahuje prazdny znak
        [TestMethod]
        public void Field()
        {

            var field = new Field(10, 10);
            field.FillField();
            for (var i = 0; i < field.Row; i++)
            {
                for (var j = 0; j < field.Coll; j++)
                {
                    Assert.AreNotEqual(' ', field.GetBrick(i, j).Colour);
                }
            }
           
        }
        
        


        
    }
}