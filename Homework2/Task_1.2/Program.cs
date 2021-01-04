using System;
using Library;

namespace Task_1._2
{
    class Program
    {
        static Account[] accounts;

        static Account[] GetSortedAccounts()
        {
            Account[] sortedAccounts = new Account[accounts.Length];
            Array.Copy(accounts, sortedAccounts, accounts.Length);
            Account temp;
            for (int i = 0; i < sortedAccounts.Length - 1; i++)
            {
                for (int j = i + 1; j < sortedAccounts.Length; j++)
                {
                    if (sortedAccounts[i].Id > sortedAccounts[j].Id)
                    {
                        temp = sortedAccounts[i];
                        sortedAccounts[i] = sortedAccounts[j];
                        sortedAccounts[j] = temp;
                    }
                }
            }
            return sortedAccounts;
        }

        static void Main(string[] args)
        {
            accounts = new Account[1000000];
            for(var i=0; i<1000000; i++)
            {
                accounts[i] = new Account("UAH");
            }
            Account[] sortedAccounts = GetSortedAccounts();
            Console.WriteLine("First ten accounts are:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Account ID {i+1}: {sortedAccounts[i].Id}");
            }
            Console.WriteLine("\nLast ten accounts are:");
            for (int i = sortedAccounts.Length-1; i >= sortedAccounts.Length - 10; i--)
            {
                Console.WriteLine($"Account ID {sortedAccounts.Length - i}: {sortedAccounts[i].Id}");
            }
        }
    }
}
