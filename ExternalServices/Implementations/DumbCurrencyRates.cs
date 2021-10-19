using ExternalServices.Interfaces;
using System;
using System.Collections.Generic;

namespace ExternalServices.Implementations
{

    public class DumbCurrencyRates : ICurrencyRates
    {
        Dictionary<Currency, decimal> _exchangeRates = new Dictionary<Currency, decimal>()
        {
            {Currency.USD, 70.96m},
            {Currency.EUR, 82.68m},
            {Currency.RUR, 1.00m},
            {Currency.GBP, 97.84m}
        };   

        public bool TryGetExchangeRate(Currency currency, out decimal exchangeRate)
        {
            if (_exchangeRates.TryGetValue(currency, out exchangeRate))
            {
                return true;
            }

            return false;
        }
    }
}
