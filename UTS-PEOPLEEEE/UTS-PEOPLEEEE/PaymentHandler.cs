using System;
using System.Collections.Generic;

namespace UTS_PEOPLEEEE
{
    public class PaymentHandler<TCurrency> where TCurrency : Currency
    {
        private readonly Dictionary<string, TCurrency> _currencies;

        public PaymentHandler(Dictionary<string, TCurrency> currencies)
        {
            _currencies = currencies;
        }

        public bool Pay(string currencyCode, double amountInBaseCurrency)
        {
            if (!_currencies.ContainsKey(currencyCode))
            {
                Console.WriteLine("Mata uang tidak dikenali.");
                return false;
            }

            var currency = _currencies[currencyCode];
            double convertedAmount = amountInBaseCurrency * currency.ConversionRate;

            Console.WriteLine($"Dibayar sebesar {currency.Symbol} {convertedAmount:F2} ({currency.Name})");
            return true;
        }
    }
}
