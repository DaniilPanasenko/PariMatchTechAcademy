using System;
using Library;

namespace Task_1._2
{
    class Program
    {
        static Account[] accounts;

        static void QuickSort(Account[] sortedAccounts, int leftBound, int rightBound)
        {
            if (leftBound >= rightBound)
            {
                return;
            }
            int marker = leftBound;
            for (int i = leftBound; i <= rightBound; i++)
            {
                if (sortedAccounts[i].Id <= sortedAccounts[rightBound].Id)
                {
                    Account temp = sortedAccounts[marker];
                    sortedAccounts[marker] = sortedAccounts[i];
                    sortedAccounts[i] = temp;
                    marker++;
                }
            }
            QuickSort(sortedAccounts, leftBound, marker - 2);
            QuickSort(sortedAccounts, marker, rightBound);
        }

        static Account[] GetSortedAccountsByQuickSort()
        {
            Account[] sortedAccounts = new Account[accounts.Length];
            Array.Copy(accounts, sortedAccounts, accounts.Length);
            QuickSort(sortedAccounts, 0, sortedAccounts.Length - 1);
            return sortedAccounts;
        }

        static void Main(string[] args)
        {
            accounts = new Account[1000000];
            for (var i = 0; i < 1000000; i++)
            {
                accounts[i] = new Account("UAH");
            }
            Account[] sortedAccounts = GetSortedAccountsByQuickSort();
            Console.WriteLine("First ten accounts are:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Account ID {i + 1}: {sortedAccounts[i].Id}");
            }
            Console.WriteLine("\nLast ten accounts are:");
            for (int i = sortedAccounts.Length - 1; i >= sortedAccounts.Length - 10; i--)
            {
                Console.WriteLine($"Account ID {sortedAccounts.Length - i}: {sortedAccounts[i].Id}");
            }
        }
    }
}
