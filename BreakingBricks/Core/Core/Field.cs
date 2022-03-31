using System;

namespace Core.Core
{
    public class Field
    {
        private readonly Brick[,] _bricks;
        public int Row { get; }
        public int Coll { get; }
        public double Score { get; set; }
        public int BrickCounter { get; set; }
        private readonly char[] _colours = {'B', 'G', 'R'};

        //Konstruktor na nastavenie poctu riadkov a stlpcov pola
        //Naplnenie pola hodnotami ziskanymi z pola _myC
        public Field(int row, int coll)
        {
            Row = row;
            Coll = coll;
            _bricks = new Brick[Row, Coll];
            FillField();
        }

        public Brick GetBrick(int row, int coll)
        {
            return _bricks[row, coll];
        }


        //Naplnenie pola ziskanim random farby z pola naslednym ulozenim v nasom hracom poly
        public void FillField()
        {

            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Coll; j++)
                {
                    var random = new Random();
                    var randomNumber = random.Next(0, _colours.Length);
                    _bricks[i, j] = new Brick(_colours[randomNumber]);
                }
            }
        }


        
        //Praskanie brickov pomocou rekurzivneho vnorovania
        //Vratenie 99 pri zadani hodnot mimo hracieho pola
        //Vratenie 1 ak by uzivatel zadal hodnotu miesta ktore je prazdne teda ' '
        public int MarkBrick(int row, int col, int count, int score)
        {
            if (row > Row - 1 || row < 0 || col < 0 || col > Coll - 1)
            {
                return 99;
            }
            var brick = _bricks[row, col].Colour;
            BrickCounter++;
            if (brick == ' ')
                return 1;

           
            Console.BackgroundColor = brick switch
            {
                'B' => ConsoleColor.Blue,
                'R' => ConsoleColor.Red,
                'G' => ConsoleColor.Green,
                _ => ConsoleColor.Black
            };

            _bricks[row, col] = new Brick(' ');
            
            if (row < Row - 1 && _bricks[row + 1, col].Colour == brick)
                MarkBrick(row + 1, col, count++, score);
            if (row > 0 && _bricks[row - 1, col].Colour == brick)
                MarkBrick(row - 1, col, count++, score);
            if (col < Coll - 1 && _bricks[row, col + 1].Colour == brick)
                MarkBrick(row, col + 1, count++, score);
            if (col > 0 && _bricks[row, col - 1].Colour == brick)
                MarkBrick(row, col - 1, count++, score);
            
            return count;
        }


        
        //Posunutie hracieho pola vertikalne
        public void VerticallyCheck()
        {
            var k = 0;
            while (k < 9)
            {
                for (var i = 0; i < Row - 1; i++)
                    for (var j = 0; j < Coll; j++)
                        if (_bricks[i + 1, j].Colour == ' ')
                        {
                            _bricks[i + 1, j] = new Brick(_bricks[i, j].Colour);
                            _bricks[i, j] = new Brick(' ');
                        }
                k++;
            }
        }

        //Posunutie hracieho pola horizontalne
        public void HorizonatallyCheck()
        {
            for (var i = 1; i < Coll - 1; i++)
            {
                if (_bricks[Row - 1, i].Colour != ' ') continue;
                if (i <= 5)
                {
                    for (var j = i; j > 0; j--)
                    {
                        for (var k = 0; k < Row; k++)
                        {
                            _bricks[k, j] = new Brick(_bricks[k, j - 1].Colour);
                            _bricks[k, j - 1] = new Brick(' ');
                        }
                    }
                }
                else
                {
                      
                    for (var j = i; j < Coll-1; j++)
                    {
                        for (var k = 0; k < Row; k++)
                        {
                            _bricks[k, j] = new Brick(_bricks[k, j + 1].Colour);
                            _bricks[k, j + 1] = new Brick(' ');
                        }
                    }
                }
            }
        }

   

        //Hra je skoncena ak sa na kazdom mieste nachadza ' '
        public bool IsSolved()
        {
            var count = 0;
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Coll; j++)
                {
                    if (_bricks[i, j].Colour == ' ')
                    {
                        count++;
                    }
                }
            }

            return count == Row * Coll;
        }
        
        //Vypis hracieho pola
        //Pozn. print sa nachadza v Classe Field pre lepsiu pracu s hracim polom
        //Mozne presunutie do Classy UI pre buducnost
        //*
        public void PrintField()
        {
            Console.Write("  ");
            for (var coordinateRow = 0; coordinateRow < Row; coordinateRow++)
            {
                Console.Write(" " + coordinateRow + " ");
                Console.Write(" ");
            }
            Console.WriteLine();
            for (var row = 0; row < Row; row++)
            {
                
                
                for (var column = 0; column < Coll; column++)
                {
                    var tile = _bricks[row, column];
                    if (column == 0)
                    {
                        Console.Write(row + " ");
                    }
                    switch (tile.Colour)
                    {
                        case 'B':
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("{0,3}", tile.Colour);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(" ");
                            break;
                        case 'R':
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("{0,3}", tile.Colour);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(" ");
                            break;
                        case 'G':
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("{0,3}", tile.Colour);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(" ");
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("    ");
                            break;
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }
    }
}
