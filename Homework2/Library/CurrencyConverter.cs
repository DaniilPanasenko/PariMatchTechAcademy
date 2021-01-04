using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public static class CurrencyConverter
    {
        private static readonly Dictionary<string, int> currencies = new Dictionary<string, int>(){
            { "EUR", 0 },
            { "USD", 1 },
            { "UAH", 2 }};

        //                                                     EUR        USD        UAH
        private static readonly decimal[,] exchangeRates = { { 1m,        1.19m,     33.63m },//EUR
                                                             { 1m/1.19m,  1m,        28.36m },//USD
                                                             { 1m/33.63m, 1m/28.36m, 1m } };  //UAH

        public static decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            return amount * exchangeRates[currencies[fromCurrency], currencies[toCurrency]];
        }

        public static bool CurrencySupport(string currency)
        {
            return currencies.Keys.Contains(currency);
        }
    }
}
