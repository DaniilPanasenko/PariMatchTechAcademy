using System;
using System.Collections.Generic;

namespace Library
{
    public class Privet48: Bank
    {
        public Privet48():base()
        {
            Name = "Privet48";
            AvailableCards = new string[] { "Gold", "Platinum" };
            SummaryInternetLimit = 10000m;
            CurrencyLimit = "UAH";
        }
    }
}
