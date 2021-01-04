using System;
using System.Collections.Generic;

namespace Task1_4
{
    class Program
    {
        static List<int> GetPrimes(int min, int max)
        {
            List<int> result = new List<int>();
            if (min < 2) min = 2;
            for (int i = min; i <= max; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            int min, max;
            Console.WriteLine("Task 1.4 Finding prime numbers by Daniil Panasenko\n");
            Console.WriteLine("Enter range...");
            Console.Write("Min: ");
            min = int.Parse(Console.ReadLine());
            Console.Write("Max: ");
            max = int.Parse(Console.ReadLine());
            List<int> primes = GetPrimes(min, max);
            Console.WriteLine($"\nPrime numbers in range [{min}, {max}]:");
            foreach(var prime in primes)
            {
                Console.Write(prime + " ");
            }
            Console.ReadKey();
        }
    }
}
