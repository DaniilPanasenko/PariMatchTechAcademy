using System;
using System.Diagnostics;

namespace Task2_4
{
    class Game
    {
        public int Min { get; private set; }

        public int Max { get; private set; }

        public int Number { get; private set; }

        public int NormalMovesCount => (int)Math.Round(Math.Log2(Max - Min + 1));

        private int _fails = 0;

        public int Fails => _fails;

        public int Tries
        {
            get
            {
                int tries = _fails;
                if (IsWon) tries++;
                return tries;
            }
        }

        public bool IsWon { get; private set; }

        public int Score
        {
            get
            {
                if (!IsWon)
                {
                    return 0;
                }
                int points = (int)Math.Round((double)(NormalMovesCount - Fails) * 100 / NormalMovesCount, MidpointRounding.AwayFromZero);
                if (points < 1) return 1;
                return points;
            }
        }

        public Game (int min, int max)
        {
            Min = min;
            Max = max;
            Random rand = new Random();
            Number = rand.Next(min, max + 1);
            IsWon = false;
        }

        public bool Round(int number)
        {
            if (number >= Min && number <= Max) 
            {
                if(number == Number)
                {
                    Console.WriteLine($"You guessed the number {Number}, you won!");
                    IsWon = true;
                    return true;
                }
                else if (number < Number)
                {
                    _fails++;
                    Console.WriteLine("More.");
                }
                else
                {
                    _fails++;
                    Console.WriteLine("Less.");
                }
            }
            else
            {
                Console.WriteLine($"Your number is out of range. Range is [{Min}-{Max}]\n");
            }
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 2.4 Game 'more-less' by Daniil Panasenko\n");
            Console.WriteLine("Rules:\n" +
                "1.The user enters range of numbers.\n" +
                "2.The computer generates the number.\n" +
                "3.The user enters numbers.\n" +
                "4.If the user hasn't guessed: the computer prompts the number more or less.\n" +
                "5.The game ends when the user guesses the number.\n" +
                "6.If you want exit, enter the exit command.\n");
            int min, max;
            Console.WriteLine("Enter the min number [0-1.000.000] ...");
            while(!Int32.TryParse(Console.ReadLine(), out min) || min<0 || min >1000000)
            {
                Console.WriteLine("You entered wrong min range value\n");
                Console.WriteLine("Enter the min number [0-1.000.000] ...");
            }
            Console.WriteLine($"Enter the max number [{min}-1.000.000] ...");
            while(!Int32.TryParse(Console.ReadLine(), out max) || max < min || max > 1000000)
            {
                Console.WriteLine("You entered wrong max range value\n");
                Console.WriteLine($"Enter the max number [{min}-1.000.000] ...");
            }
            Stopwatch time = new Stopwatch();
            time.Start();
            Game game = new Game(min, max);
            bool endGame = false;
            while (!endGame)
            {
                Console.WriteLine("Enter the number...");
                string command = Console.ReadLine();
                if (command == "exit") break;
                int number;
                if(!Int32.TryParse(command, out number))
                {
                    Console.WriteLine("You entered unknown command\n");
                    continue;
                }
                endGame = game.Round(number);
            }
            time.Stop();
            Console.WriteLine("\nThank you for the game!");
            Console.WriteLine($"Youre score: {game.Score} points!");
            Console.WriteLine($"Youre tries: {game.Tries} tries!");
            Console.WriteLine($"Game time: {time.ElapsedMilliseconds/1000} seconds!");
        }
    }
}
