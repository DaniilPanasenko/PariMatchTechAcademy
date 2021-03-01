using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeNumbers.API.Interfaces;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API.Services
{
    public class PrimeNumbersService : IPrimeNumbersSevice
    {
        public async Task<List<int>> GetPrimesAsync(PrimesSettings settings)
        {
            var primes = new List<int>();

            for (int i = Math.Max(settings.From, 2); i <= settings.To; i++)
            {
                if (await IsPrimeAsync(i))
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        public Task<bool> IsPrimeAsync(int number)
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
