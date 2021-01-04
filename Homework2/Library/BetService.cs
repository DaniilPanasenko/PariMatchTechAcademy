using System;
namespace Library
{
    public class BetService
    {
        public decimal Odd { get; private set; }

        public BetService()
        {
            Random random = new Random();
            Odd = random.Next(101, 2501) / 100m;
        }

        public float GetOdds()
        {
            Random random = new Random();
            Odd = random.Next(101, 2501) / 100m;
            return (float)Odd;
        }

        public bool IsWon()
        {
            int wonProbability = (int)(10000 / Odd);
            Random random = new Random();
            int randomWinnerNumber = random.Next(0, 10001);
            if (wonProbability >= randomWinnerNumber) return true;
            return false;
        }

        public decimal Bet(decimal amount)
        {
            return IsWon() ? Odd * amount : 0m;
        }
    }
}
