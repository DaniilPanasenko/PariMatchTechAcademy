using System;
using Library;

namespace Task_1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Account accountEUR = new Account("EUR");
            Account accountUSD = new Account("USD");
            Account accountUAH = new Account("UAH");
            accountEUR.Deposit(10, "EUR");
            accountEUR.Withdraw(3, "UAH");
            accountUAH.Deposit(121, "USD");
            try
            {
                accountUSD.Withdraw(5, "USD");
            }
            catch { }
            try
            {
                Account accountPLN = new Account("PLN");
            }
            catch { }
            Console.WriteLine($"Account with currency EUR has {accountEUR.GetBalance("EUR")} balance");
            Console.WriteLine($"Account with currency USD has {accountUSD.GetBalance("USD")} balance");
            Console.WriteLine($"Account with currency UAH has {accountUAH.GetBalance("UAH")} balance");
        }
    }
}
