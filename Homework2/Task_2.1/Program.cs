using System;
using Library;

namespace Task_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            BetService betService = new BetService();
            for (int i = 0; i < 10; i++)
            {
                betService.GetOdds();
                decimal betAmount = betService.Bet(100m);
                Console.WriteLine($"I’ve bet 100 USD with the odd {betService.Odd} and I’ve earned {betAmount}");
            }
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                do
                {
                    betService.GetOdds();
                }
                while (betService.Odd <= 12m);
                decimal betAmount = betService.Bet(100m);
                Console.WriteLine($"I’ve bet 100 USD with the odd {betService.Odd} and I’ve earned {betAmount}");
            }
            Console.WriteLine();
            decimal myAmount = 10000m;   
            int iteration = 0;
            while (myAmount > 0m && myAmount <= 150000m)
            {
                decimal summaryBet = 0m;
                decimal betAmount = 0m;
                while (betAmount == 0m && myAmount > 0m && myAmount <= 150000m)
                {
                    float odd = betService.GetOdds();
                    if (odd <5) continue;
                    decimal currentBet = (summaryBet + myAmount/10000m) / ((decimal)odd - 1);
                    currentBet = Math.Round(currentBet, 2);
                    if (currentBet > myAmount) currentBet = myAmount;
                    summaryBet+=currentBet;
                    myAmount -= currentBet;
                    betAmount = betService.Bet(currentBet);
                    myAmount += betAmount;
                    iteration++;
                    Console.WriteLine($"{iteration}. I’ve bet {currentBet} USD with the odd {betService.Odd} and I’ve earned {Math.Round(betAmount,2)}. Total amount: {Math.Round(myAmount,2)}");
                }
            }
            
        }
    }
}
