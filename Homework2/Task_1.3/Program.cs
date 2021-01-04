using System;
using Library;

namespace Task_1._3
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

        static void GetAccount(int id)
        {
            int leftBound = 0;
            int righBound = accounts.Length - 1;
            bool found = false;
            int tries = 0;
            while (leftBound <= righBound)
            {
                tries++;
                int middleValue = (leftBound + righBound) / 2;
                if (id == accounts[middleValue].Id)
                {
                    Console.WriteLine($"{id} was found at index {middleValue} by {tries} tries");
                    found = true;
                    break;
                }
                else if (id < accounts[middleValue].Id)
                {
                    righBound = middleValue - 1;
                }
                else
                {
                    leftBound = middleValue + 1;
                }
            }
            if (!found)
            {
                Console.WriteLine($"There is no account {id} in the list");
            }
        }

        static void Main(string[] args)
        {
            accounts = new Account[10000];
            for (var i = 0; i < 10000; i++)
            {
                accounts[i] = new Account("UAH");
            }
            accounts = GetSortedAccounts();
            Console.WriteLine("Find account by id:\n");
            string command = "";
            while (command != "exit")
            {
                Console.WriteLine("Enter account id or command exit");
                command = Console.ReadLine();
                int id;
                while(!int.TryParse(command, out id))
                {
                    Console.WriteLine("Not number");
                    Console.WriteLine("Enter account id or command exit");
                    command = Console.ReadLine();
                }
                GetAccount(id);
            }

        }
    }
}
