using System;
using Library;

namespace Task_3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            CreditCard creditCard;
            creditCard = new CreditCard();
            creditCard.StartDeposit(50,"USD");
            creditCard.StartWithdrawal(50, "USD");
            Bank bank;
            bank = new Privet48();
            bank.StartDeposit(50, "USD");
            bank = new Stereobank();
            bank.StartWithdrawal(50, "USD");
            GiftVoucher giftVoucher = new GiftVoucher();
            giftVoucher.StartDeposit(50, "USD");
            giftVoucher.StartDeposit(500, "USD");
        }
    }
}
