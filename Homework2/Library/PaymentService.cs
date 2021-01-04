using System;
using System.Linq;

namespace Library
{
    public class PaymentService
    {
        public PaymentMethodBase[] AvailablePaymentMethod { get; private set; }

        private BettingPlatformEmulator BettingPlatformEmulator { get; set; }

        public PaymentService()
        {
            AvailablePaymentMethod = new PaymentMethodBase[] { new CreditCard(), new Privet48(), new Stereobank(), new GiftVoucher() };
        }

        public PaymentService(BettingPlatformEmulator bettingPlatformEmulator)
        {
            AvailablePaymentMethod = new PaymentMethodBase[] { new CreditCard(), new Privet48(), new Stereobank(), new GiftVoucher() };
            BettingPlatformEmulator = bettingPlatformEmulator;
        }

        public void StartDeposit(decimal amount, string currency)
        {
            Random random = new Random();
            int systemError = random.Next(0, 100);
            if (systemError == 0 || systemError == 1)
            {
                throw new Exception();
            }
            int[] positions = new int[0];
            Console.WriteLine("Choose deposit way:");
            for (int i = 0; i < AvailablePaymentMethod.Length; i++)
            {
                if (AvailablePaymentMethod[i] is ISupportDeposit)
                {
                    Console.WriteLine($"{ positions.Length + 1 }. {AvailablePaymentMethod[i].Name}");
                    Array.Resize(ref positions, positions.Length + 1);
                    positions[positions.Length - 1] = i;
                }
            }
            int commandNumber;
            while (!int.TryParse(Console.ReadLine(), out commandNumber) || commandNumber < 1 || commandNumber > positions.Length)
            {
                Console.WriteLine($"Incorrect deposit way (1-{positions.Length}), please repeat command");
            }
            ISupportDeposit depositMethod = (ISupportDeposit)AvailablePaymentMethod[commandNumber - 1];
            depositMethod.StartDeposit(amount, currency);
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            Random random = new Random();
            int systemError = random.Next(0, 100);
            if (systemError == 0 || systemError == 1)
            {
                throw new Exception();
            }
            if (BettingPlatformEmulator?.ActivePlayer.Account.GetBalance(currency) < amount)
            {
                throw new InsufficientFundsException("There is insufficient funds on your account");
            }
            if (BettingPlatformEmulator?.Account.GetBalance(currency) < amount)
            {
                throw new InsufficientFundsException("There is some problem on the platform side. Please try it later");
            }
            int[] positions = new int[0];
            Console.WriteLine("Choose withdrawal way:");
            for (int i = 0; i < AvailablePaymentMethod.Length; i++)
            {
                if (AvailablePaymentMethod[i] is ISupportWithdrawal)
                {
                    Console.WriteLine($"{ positions.Length + 1 }. {AvailablePaymentMethod[i].Name}");
                    Array.Resize(ref positions, positions.Length + 1);
                    positions[positions.Length - 1] = i;
                }
            }
            int commandNumber;
            while (!int.TryParse(Console.ReadLine(), out commandNumber) || commandNumber < 1 || commandNumber > positions.Length)
            {
                Console.WriteLine($"Incorrect withdrawal way (1-{positions.Length}), please repeat command");
            }
            ISupportWithdrawal depositMethod = (ISupportWithdrawal)AvailablePaymentMethod[commandNumber - 1];
            depositMethod.StartWithdrawal(amount, currency);
        }
    }
}
