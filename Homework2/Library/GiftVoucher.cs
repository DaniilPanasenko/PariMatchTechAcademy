using System;
using System.Collections.Generic;

namespace Library
{
    public class GiftVoucher: PaymentMethodBase, ISupportDeposit
    {
        private static List<string> usedGiftVouchers = new List<string>();

        public GiftVoucher()
        {
            Name = "GiftVoucher";
        }

        private bool TryUseGiftVoucher(string code)
        {
            if (usedGiftVouchers.Contains(code)) return false;
            usedGiftVouchers.Add(code);
            return true;
        }

        public void StartDeposit(decimal amount, string currency)
        {
            if (amount != 100 && amount != 500 && amount != 1000)
            {
                Console.WriteLine("Gift voucher amount can be 100, 500 or 1000");
                return;
            }
            bool validGiftCard = false;
            string code = "";
            while (!validGiftCard)
            {
                Console.WriteLine("Please, enter the gift voucher code (10 numbers)");
                code = Console.ReadLine();
                validGiftCard = true;
                if (code.Length != 10) validGiftCard = false;
                foreach(var ch in code)
                {
                    if (!Char.IsDigit(ch)) validGiftCard = false;
                }
                if (!validGiftCard) Console.WriteLine("Invalid code");
            }
            if (!TryUseGiftVoucher(code))
            {
                throw new LimitExceededException("This gift vaucher already used");
            }
            Console.WriteLine($"You’ve used {amount} {currency} gift voucher successfully");
        }
    }
}
