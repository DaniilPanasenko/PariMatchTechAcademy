using System;
using Library;

namespace Task_3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService paymentService = new PaymentService();
            string command = "";
            while (command != "exit")
            {
                Console.WriteLine("\nCommand list:");
                Console.WriteLine("deposit");
                Console.WriteLine("withdrawal");
                Console.WriteLine("exit");
                command = Console.ReadLine();
                if (command != "deposit" && command != "withdrawal" && command != "exit")
                {
                    Console.WriteLine("Unknown command");
                    continue;
                }
                if (command == "exit") break;
                Console.WriteLine("Enter amount...");
                decimal amount;
                while(!decimal.TryParse(Console.ReadLine(),out amount) || amount<0)
                {
                    Console.WriteLine("Invalid amount.");
                    Console.WriteLine("Enter amount...");
                }
                Console.WriteLine("Enter currency...");
                string currency = Console.ReadLine();
                while (currency!="UAH" && currency != "USD" && currency != "EUR")
                {
                    Console.WriteLine("Supported currencies: UAH, USD, EUR.");
                    Console.WriteLine("Enter currency...");
                    currency = Console.ReadLine();
                }
                if (command == "deposit")
                {
                    paymentService.StartDeposit(amount, currency);
                }
                if (command == "withdrawal")
                {
                    paymentService.StartWithdrawal(amount, currency);
                }
            }

        }
    }
}
