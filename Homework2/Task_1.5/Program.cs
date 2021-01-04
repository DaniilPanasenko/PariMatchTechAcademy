using System;
using Library;

namespace Task_1._5
{
    class Program
    {

        static void Main(string[] args)
        {
            Player player = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "USD");
            string password1 = "TheP@$$w0rd";
            bool login1 = player.IsPasswordValid(password1);
            Console.WriteLine($"Login with email {player.Email} and password {password1} is {login1}");
            string password2 = "Qwerty12345678";
            bool login2 = player.IsPasswordValid(password2);
            Console.WriteLine($"Login with email {player.Email} and password {password2} is {login2}");
            player.Deposit(100, "USD");
            player.Withdraw(50, "EUR");
            try
            {
                player.Withdraw(1000, "USD");
            }
            catch { }
            try
            {
                Player playerPLN = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "PLN");
            }
            catch { }
            Console.WriteLine(player.Account.GetBalance("UAH"));
        }
    }
}
