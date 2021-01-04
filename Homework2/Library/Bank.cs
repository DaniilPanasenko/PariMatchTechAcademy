using System;
using System.Collections.Generic;

namespace Library
{
    public abstract class Bank: PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    { 
        private Dictionary<string, decimal> usedInternetLimit = new Dictionary<string, decimal>();

        public decimal SummaryInternetLimit { get; protected set; }

        public decimal TransactionLimit { get; protected set; }

        public string CurrencyLimit { get; protected set; }

        public string[] AvailableCards { get; protected set; }

        public Bank()
        {
            TransactionLimit = -1;
            SummaryInternetLimit = -1;
        }

        private void Authorization(out int cardNum, out string login)
        {
            string password;
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}!");
            Console.WriteLine("Please, enter your login...");
            login = Console.ReadLine();
            Console.WriteLine("Please, enter your password...");
            password = Console.ReadLine();
            do
            {
                Console.WriteLine($"Hello Mr {login}. Pick a card to proceed the transaction");
                for (int i = 0; i < AvailableCards.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {AvailableCards[i]}");
                }
            }
            while ((!int.TryParse(Console.ReadLine(), out cardNum) || cardNum < 1 || cardNum > AvailableCards.Length));
        }

        public void StartDeposit(decimal amount, string currency)
        {
            if (TransactionLimit!=-1 && CurrencyConverter.Convert(amount, currency, CurrencyLimit) > TransactionLimit)
            {
                throw new LimitExceededException($"Exceeded internet limit in {TransactionLimit} {CurrencyLimit}");
            }
            int cardNum;
            string login;
            Authorization(out cardNum, out login);
            if (!usedInternetLimit.ContainsKey(login))
            {
                usedInternetLimit.Add(login, 0m);
            }
            decimal newUsedInternetLimit = CurrencyConverter.Convert(amount, currency, CurrencyLimit) + usedInternetLimit[login];
            if (SummaryInternetLimit != -1 && newUsedInternetLimit > SummaryInternetLimit)
            {
                throw new LimitExceededException($"Exceeded summary internet limit in {SummaryInternetLimit} {CurrencyLimit}");
            }
            usedInternetLimit[login] = newUsedInternetLimit;
            Console.WriteLine($"You’ve withdraw {amount} {currency} from your {AvailableCards[cardNum - 1]} card successfully");
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            if (TransactionLimit != -1 && CurrencyConverter.Convert(amount, currency, CurrencyLimit) > TransactionLimit)
            {
                throw new LimitExceededException("Exceeded internet limit in 3000 UAH");
            }
            int cardNum;
            string login;
            Authorization(out cardNum, out login);
            if (!usedInternetLimit.ContainsKey(login))
            {
                usedInternetLimit.Add(login, 0m);
            }
            decimal newUsedInternetLimit = CurrencyConverter.Convert(amount, currency, CurrencyLimit) + usedInternetLimit[login];
            if (SummaryInternetLimit != -1 && newUsedInternetLimit > SummaryInternetLimit)
            {
                throw new LimitExceededException($"Exceeded summary internet limit in {SummaryInternetLimit} {CurrencyLimit}");
            }
            usedInternetLimit[login] = newUsedInternetLimit;
            Console.WriteLine($"You’ve deposit {amount} {currency} to your {AvailableCards[cardNum - 1]} card successfully");
        }
    }
}
