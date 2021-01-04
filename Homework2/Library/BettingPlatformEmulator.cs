using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class BettingPlatformEmulator
    {
        public List<Player> Players { get; private set; }

        public Player ActivePlayer { get; private set; }

        public Account Account { get; private set; }

        public BetService BetService { get; private set; }

        public PaymentService PaymentService { get; private set; }

        public BettingPlatformEmulator()
        {
            PaymentService = new PaymentService(this);
            BetService = new BetService();
            Players = new List<Player>();
            Account = new Account("USD");
        }

        private void PrintMainMenu()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Stop");
        }

        private void PrintPlayerMenu()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Get Odds");
            Console.WriteLine("4. Bet");
            Console.WriteLine("5. Get Ballans");
            Console.WriteLine("6. Logout");
        }

        private void ExecuteMainMenu()
        {
            PrintMainMenu();
            int commandNumber;
            string command;
            do
            {
                Console.WriteLine("Enter the number of command (1,2 or 3)...");
                command = Console.ReadLine();
            }
            while (!int.TryParse(command, out commandNumber) || commandNumber < 1 || commandNumber > 3);
            switch (commandNumber)
            {
                case 1:
                    Register();
                    break;
                case 2:
                    Login();
                    break;
                case 3:
                    Exit();
                    break;
            }
        }

        private void ExecutePlayerMenu()
        {
            PrintPlayerMenu();
            int commandNumber;
            string command;
            do
            {
                Console.WriteLine("Enter the number of command (1,2 or 6)...");
                command = Console.ReadLine();
            }
            while (!int.TryParse(command, out commandNumber) || commandNumber < 1 || commandNumber > 6);
            switch (commandNumber)
            {
                case 1:
                    Deposit();
                    break;
                case 2:
                    Withdraw();
                    break;
                case 3:
                    GetOdds();
                    break;
                case 4:
                    Bet();
                    break;
                case 5:
                    GetBalance();
                    break;
                case 6:
                    Logout();
                    break;
            }
        }

        public void Start()
        {
            if (ActivePlayer == null)
            {
                ExecuteMainMenu();
            }
            else
            {
                ExecutePlayerMenu();
            }
            Start();
        }

        private void Register()
        {
            Console.WriteLine("Enter your name, please...");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter your last name, please...");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter your email, please...");
            string email = Console.ReadLine();
            while (Players.Where(x => x.Email == email).Count() != 0)
            {
                Console.WriteLine("User with this email already exist");
                Console.WriteLine("Enter your email, please...");
                email = Console.ReadLine();
            }
            Console.WriteLine("Enter your password, please...");
            string password = Console.ReadLine();
            bool validCurrency = false;
            while(!validCurrency)
            {
                Console.WriteLine("Enter currency of your account, please...");
                string currency = Console.ReadLine();
                try
                {
                    Player newPlayer = new Player(firstName, lastName, email, password, currency);
                    validCurrency = true;
                    Players.Add(newPlayer);
                }
                catch(NotSupportedException)
                {
                    Console.WriteLine("Unsupported currency");
                }
            }
        }

        private void Login()
        {
            Console.WriteLine("Enter your email, please...");
            string email = Console.ReadLine();
            Player loginPlayer = Players.Where(p => p.Email == email).FirstOrDefault();
            if (loginPlayer == null)
            {
                Console.WriteLine("Invalid email");
                return;
            }
            Console.WriteLine("Enter your password, please...");
            string password = Console.ReadLine();
            bool passwordValid = loginPlayer.IsPasswordValid(password);
            if (!passwordValid)
            {
                Console.WriteLine("Invalid password");
                return;
            }
            ActivePlayer = loginPlayer;
        }

        private void Logout()
        {
            ActivePlayer = null;
        }

        private void GetBalance()
        {
            Console.WriteLine($"Your ballans: {ActivePlayer.Account.GetBalance(ActivePlayer.Account.Currency)} {ActivePlayer.Account.Currency}");
        }

        private void Deposit()
        {
            decimal amount;
            Console.WriteLine("Enter amount for deposit, please...");
            if (!decimal.TryParse(Console.ReadLine(), out amount) || amount<=0)
            {
                Console.WriteLine("You entered incorrect amount");
                return;
            }
            Console.WriteLine("Enter currency for deposit, please...");
            string currency = Console.ReadLine();
            if (!CurrencyConverter.CurrencySupport(currency))
            {
                Console.WriteLine("You entered unsupported currency");
                return;
            }

            try
            {
                PaymentService.StartDeposit(amount, currency);
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.Try again later...");
                return;
            }

            ActivePlayer.Deposit(amount, currency);
            Account.Deposit(amount, currency);
        }

        private void Withdraw()
        {
            decimal amount;
            Console.WriteLine("Enter amount for withdraw, please...");
            if (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("You entered incorrect amount");
                return;
            }
            Console.WriteLine("Enter currency for withdraw, please...");
            string currency = Console.ReadLine();
            if (!CurrencyConverter.CurrencySupport(currency))
            {
                Console.WriteLine("You entered unsupported currency");
                return;
            }

            try
            {
                PaymentService.StartWithdrawal(amount, currency);
            }
            catch (LimitExceededException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
                return;
            }
            catch (InsufficientFundsException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Please, try to make a transaction with lower amount or change the payment method");
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.Try again later...");
                return;
            }

            try
            {
                ActivePlayer.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is insufficient funds on your account");
                return;
            }

            try
            { 
                Account.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is some problem on the platform side. Please try it later");
                ActivePlayer.Deposit(amount, currency);
                return;
            }
        }

        private void GetOdds()
        {
            Console.WriteLine($"Actual odd: {BetService.GetOdds()}");
        }

        private void Bet()
        {
            Console.WriteLine($"Actual odd: {BetService.Odd}");
            decimal amount;
            Console.WriteLine("Enter amount for bet, please...");
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("You entered incorrect amount");
                return;
            }
            Console.WriteLine("Enter currency for bet, please...");
            string currency = Console.ReadLine();
            try
            {
                ActivePlayer.Withdraw(amount, currency);
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("You entered unsupported currency");
                return;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is insufficient funds on your account");
                return;
            }
            decimal betResult = BetService.Bet(amount);
            if (betResult != 0m)
            {
                Console.WriteLine($"You won! Bet: {amount} {currency}, Odd: {BetService.Odd}, Earn: {betResult} {currency}!");
                ActivePlayer.Deposit(betResult, currency);
            }
            else
            {
                Console.WriteLine($"You lost! Bet: {amount} {currency}, Odd: {BetService.Odd}!");
            }
        }

        private void Exit()
        { 
            Console.WriteLine("Thank you for using our betting platform emulator! Good bye!");
            Environment.Exit(0);
        }
    }
}
