using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeNumbers.Tests
{
    public static class PrimeNumbers
    {
        public static async Task<List<int>> GetPrimesAsync(int from, int to)
        {
            var primes = new List<int>();

            for (int i = Math.Max(from, 2); i <= to; i++)
            {
                if (await IsPrimeAsync(i))
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        public static Task<bool> IsPrimeAsync(int number)
        {
            bool isPrime = true;
            if (number < 2)
            {
                isPrime = false;
            }
            else
            {
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }
            return Task.FromResult(isPrime);
        }
    }
}
