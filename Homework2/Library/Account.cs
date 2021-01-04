using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Account
    {
        public int Id { get; private set; }

        public string Currency { get; private set; }

        public decimal Amount { get; private set; }

        public Account(string currency)
        {
            Random random = new Random();
            int newId;
            do
            {
                newId = random.Next(100000, 100000000);
            }
            while (!AccountIdentifiers.TryAddIdentifier(newId));
            Id = newId;
            if (!CurrencyConverter.CurrencySupport(currency)) throw new NotSupportedException(); 
            Currency = currency;
            Amount = 0;
        }

        public void Deposit(decimal amount, string currency)
        {
            if (!CurrencyConverter.CurrencySupport(currency)) throw new NotSupportedException(); 
            this.Amount += CurrencyConverter.Convert(amount, currency, this.Currency);
        }

        public void Withdraw(decimal amount, string currency)
        {
            if (!CurrencyConverter.CurrencySupport(currency)) throw new NotSupportedException(); 
            decimal amountToWithdraw = CurrencyConverter.Convert(amount, currency, this.Currency);
            if (this.Amount < amountToWithdraw) throw new InvalidOperationException();
            this.Amount -= amountToWithdraw;
        }

        public decimal GetBalance(string currency)
        {
            return Math.Round(CurrencyConverter.Convert(this.Amount, this.Currency, currency),2);
        }
    }

    static class AccountIdentifiers
    {
        private static HashSet<int> identifiers = new HashSet<int>();

        public static bool TryAddIdentifier(int id)
        {
            if (identifiers.Contains(id)) return false;
            identifiers.Add(id);
            return true;
        }
    }
}
