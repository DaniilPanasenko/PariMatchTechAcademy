using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeNumbers.API.Models
{
    public class PrimesSettings
    {
        public int From { get; set; }

        public int To { get; set; }

        
        public PrimesSettings(int from, int to)
        {
            From = from;
            To = to;
        }

        
    }
}
