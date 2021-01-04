using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Player
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public Account Account { get; private set; }

        public Player(string firstName, string lastName, string email, string password, string currency)
        {
            Random random = new Random();
            int newId;
            do
            {
                newId = random.Next(100000, 100000000);
            }
            while (!PlayerIdentifiers.TryAddIdentifier(newId));
            Id = newId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Account = new Account(currency);
        }

        public bool IsPasswordValid(string password)
        {
            return this.Password == password;
        }

        public void Deposit(decimal amount, string currency)
        {
            Account.Deposit(amount, currency);
        }

        public void Withdraw(decimal amount, string currency)
        {
            Account.Withdraw(amount, currency);
        }
    }

    static class PlayerIdentifiers
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
