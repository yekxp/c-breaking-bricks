using System;
using Core;
using Core.Core;
using Core.ScoreService;

namespace BreakingBricks
{
    public class Ui
    {
        private string Player { get; set; }
        private int Lives { get; set; }
        public IScoreService _tableScore = new ScoreServiceEF();
        private Field _field;
        public void PlayGame()
        {
            
            
            again:
            System.Console.Clear();
            WelcomePrint();
            System.Console.Write("Your name: ");
            Player = Convert.ToString(System.Console.ReadLine());
            _field = new Field(10, 10);
            Lives = 5;
            _field.Score = 0;
            
            repeat:
            System.Console.Clear();
            ScoreLivesPrint();
            _field.FillField();
            _field.PrintField();
     
            while (Lives > 0)
            {

                int x;
                int y;
                System.Console.WriteLine();
                System.Console.Write("Coordinate of the brick row [0-9]: ");
                var str1 = System.Console.ReadLine();
                System.Console.Write("Coordinate of the brick column [0-9]: ");
                var str2 = System.Console.ReadLine();
                System.Console.WriteLine("--------------------------------------");
                System.Console.WriteLine();
                System.Console.Clear();
               
                
                while (!int.TryParse(str1, out x) || !int.TryParse(str2, out y) || x is > 9 or < 0 || y is > 9 or < 0)
                {
                    System.Console.WriteLine("SCORE:" + _field.Score);
                    System.Console.WriteLine("Your lives: " + Lives);
                    System.Console.WriteLine("---------------------");
                    System.Console.WriteLine();
                    _field.PrintField();
                    System.Console.WriteLine();
                    System.Console.WriteLine("WRONG INPUT AGAIN");
                    System.Console.WriteLine("------------------");
                    System.Console.Write("Input row coordinate [0-9]: ");
                    str1 = System.Console.ReadLine();
                    System.Console.Write("Input row coordinate [0-9]: ");
                    str2 = System.Console.ReadLine();
                    
                }
                
                _field.BrickCounter = 0;
                var k = _field.MarkBrick(x, y, 0, _field.BrickCounter);
                if (k == 0)
                    Lives--;
                System.Console.BackgroundColor = ConsoleColor.Black;
                
                _field.VerticallyCheck();
                _field.HorizonatallyCheck();
                _field.Score += _tableScore.ScoreCounter(_field.BrickCounter);
                
                ScoreLivesPrint();
                _field.BrickCounter = 0;
                _field.PrintField();
                
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.WriteLine();


                if (_field.IsSolved())
                {
                    System.Console.WriteLine("YOU WIN!");
                    System.Console.WriteLine("Next level? [Y/N] or another char to close program");
                    QuestionPrint();
                    var answer = Convert.ToChar(System.Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (answer.ToString().ToUpper())
                    {
                        case "Y" or "y":
                            ScoreLivesPrint();
                            goto repeat;
                            
                        case "N" or "n":
                            CaseN();
                            Lives = -1;
                            break;
                    }
                    
                }
                else if (Lives == 0)
                {
                    System.Console.WriteLine("{0,25}", "GAME OVER");
                    System.Console.WriteLine("Do you want play again? [Y/N] or another char to close program");
                    QuestionPrint();
                    
                    var answer = Convert.ToChar(System.Console.ReadLine() ?? throw new InvalidOperationException());
                    switch (answer.ToString().ToUpper())
                    {
                        case "Y" or "y":
                            CaseN();
                            goto again;
                        case "N" or "n":
                            CaseN();
                            break;
                    }
                }



            }

        }

        private void CaseN()
        {
            System.Console.WriteLine("Comment to the game: ");
            var comment = System.Console.ReadLine();
            
            System.Console.WriteLine("Hodnotenie levelu: 1 - 5 (1 - the worst; 5 - the best)");
            int review = Convert.ToInt32(System.Console.ReadLine() ?? throw new InvalidOperationException());
            
            while (review < 1 || review > 5)
            { 
                System.Console.WriteLine("Hodnotenie levelu: 1 - 5 (1 - the worst; 5 - the best)");
                review = Convert.ToInt32(System.Console.ReadLine());
            }
            
            _tableScore.AddScore(new Score { Name = Player, Points = _field.Score });
            //_tableScore.PrintScore();
        }

        private void WelcomePrint()
        {
            System.Console.WriteLine("-----------------------------------------------------------------------");
            System.Console.WriteLine("WELCOME TO BREAKING BRICKS");
            System.Console.WriteLine("Your job is to break all the bricks.");
            System.Console.WriteLine("You have five lives. You'll lose your life if you only break one brick.");
            System.Console.WriteLine("-----------------------------------------------------------------------");
            System.Console.WriteLine();
        }

        private void ScoreLivesPrint()
        {
            System.Console.WriteLine("SCORE:" + _field.Score);
            System.Console.WriteLine("Your lives: " + Lives);
            System.Console.WriteLine("---------------------");
            System.Console.WriteLine();
        }

        private void QuestionPrint()
        {
            System.Console.WriteLine("[Y] - Play again");
            System.Console.WriteLine("[N] - Add comment, review and show high score");
            System.Console.Write("[Answer]: ");
            System.Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}