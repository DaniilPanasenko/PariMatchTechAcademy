using System;
namespace Library
{
    public class Stereobank : Bank
    {
        public Stereobank() : base()
        {
            Name = "Stereobank";
            AvailableCards = new string[] { "Black", "White", "Iron"};
            SummaryInternetLimit = 7000m;
            TransactionLimit = 3000m;
            CurrencyLimit = "UAH";
        }
    }
}
