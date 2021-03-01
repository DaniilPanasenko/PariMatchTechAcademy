using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API.Interfaces
{
    public interface IPrimeNumbersSevice
    {
        public Task<bool> IsPrimeAsync(int number);

        public Task<List<int>> GetPrimesAsync(PrimesSettings settings);
    }
}
