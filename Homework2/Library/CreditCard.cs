using System;
namespace Library
{
    public class CreditCard: PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public CreditCard()
        {
            Name = "CreditCard";
        }

        private bool ValidationCreditCardNumber(string creditCard)
        {
            if (creditCard.Length != 16) return false;
            if (creditCard[0] != '4' && creditCard[0] != '5') return false;
            foreach (var ch in creditCard)
            {
                if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }

        private bool ValidationExpiryDate(string expiryDate)
        {
            if (expiryDate.Length != 5) return false;
            if (expiryDate[2] != '/') return false;
            string monthString = expiryDate.Substring(0, 2);
            string yearString = expiryDate.Substring(3);
            ushort month, year;
            if (!ushort.TryParse(monthString, out month) || month < 1 || month > 12) return false;
            if (!ushort.TryParse(yearString, out year)) return false;
            if (year < DateTime.Now.Year % 100 || (year == DateTime.Now.Year % 100 && month < DateTime.Now.Month)) return false;
            return true;
        }

        private bool ValidationCVV(string cvv)
        {
            if (cvv.Length != 3) return false;
            foreach (var ch in cvv)
            {
                if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }

        public void StartDeposit(decimal amount, string currency)
        {
            if (CurrencyConverter.Convert(amount, currency, "UAH") > 3000)
            {
                throw new LimitExceededException("Exceeded internet limit in 3000 UAH");
            }
            Console.WriteLine("Enter credit card number (16 numbers)...");
            string creditCard = Console.ReadLine();
            while (!ValidationCreditCardNumber(creditCard))
            {
                Console.WriteLine("Credit card number is not valid");
                Console.WriteLine("Enter credit card number (16 numbers)...");
                creditCard = Console.ReadLine();
            }
            Console.WriteLine("Enter credit card expiry date (mm/yy)...");
            string expiryDate = Console.ReadLine();
            while (!ValidationExpiryDate(expiryDate))
            {
                Console.WriteLine("Credit card expiry date is not valid");
                Console.WriteLine("Enter credit card expiry date (mm/yy)...");
                expiryDate = Console.ReadLine();
            }
            Console.WriteLine("Enter credit card CVV (3 numbers)...");
            string cvv = Console.ReadLine();
            while (!ValidationCVV(cvv))
            {
                Console.WriteLine("Credit card CVV is not valid");
                Console.WriteLine("Enter credit card CVV (3 numbers)...");
                cvv = Console.ReadLine();
            }
            Console.WriteLine($"You’ve withdraw {amount} {currency} from your {creditCard} card successfully");
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            if (CurrencyConverter.Convert(amount, currency, "UAH") > 3000m)
            {
                throw new LimitExceededException("Exceeded internet limit in 3000 UAH");
            }
            Console.WriteLine("Enter credit card number (16 numbers)...");
            string creditCard = Console.ReadLine();
            while (!ValidationCreditCardNumber(creditCard))
            {
                Console.WriteLine("Credit card is not valid");
                Console.WriteLine("Enter credit card number (16 numbers)...");
                creditCard = Console.ReadLine();
            }
            Console.WriteLine($"You’ve deposit {amount} {currency} to your {creditCard} card successfully");
        }
    }
}
