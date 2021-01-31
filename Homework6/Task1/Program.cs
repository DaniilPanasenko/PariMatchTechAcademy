using System;
using System.Diagnostics;
using System.Linq;

namespace Task1
{
    class Program
    {
        static int LINQPrimeNumbers(int min, int max)
        {
            min = Math.Max(min, 2);
            if (max < min) return 0;
            return Enumerable.Range(min, max - min + 1)
                .Where(x =>
                    Enumerable.Range(2, (int)Math.Sqrt(x) - 1)
                        .All(y => x % y != 0))
                .Count();
        }

        static int PLINQPrimeNumbers(int min, int max)
        {
            min = Math.Max(min, 2);
            if (max < min) return 0;
            return Enumerable.Range(min, max - min + 1)
                .AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .Where(x =>
                    Enumerable.Range(2, (int)Math.Sqrt(x) - 1)
                        .All(y => x % y != 0))
                .Count();
        }

        static int InputInteger(string message)
        {
            int value;
            do
            {
                Console.WriteLine(message);
            }
            while (!int.TryParse(Console.ReadLine().Trim(), out value));
            return value;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 1. Search primes numbers with LINQ and PLINQ by Daniil Panasenko\n");
            while (true)
            {
                int min = InputInteger("Enter the minimum number of the range");
                int max = InputInteger("Enter the maximum number of the range");
                Console.WriteLine("\nMenu:\n1 - LINQ\n2 - PLINQ\n");
                int menuItem = 0;
                while (menuItem < 1 || menuItem > 2)
                {
                    menuItem = InputInteger("Choose item from the menu [1-2]");
                }
                if (menuItem == 1)
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    int result = LINQPrimeNumbers(min, max);
                    stopWatch.Stop();
                    Console.WriteLine($"{result} prime numbers in range [{min}-{max}]");
                    Console.WriteLine("Time for executing LINQ: " + stopWatch.Elapsed);
                }
                else
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    int result = PLINQPrimeNumbers(min, max);
                    stopWatch.Stop();
                    Console.WriteLine($"{result} prime numbers in range [{min}-{max}]");
                    Console.WriteLine("Time for executing PLINQ: " + stopWatch.Elapsed);
                }
                Console.WriteLine("\nPress Enter to restart or press any key to exit...\n");
                var key = Console.ReadKey();
                if (key.Key != ConsoleKey.Enter) return;
            }
        }
    }
}
