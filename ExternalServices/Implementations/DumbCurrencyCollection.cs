using ExternalServices.Interfaces;
using System;
using System.Collections.Generic;

namespace ExternalServices.Implementations
{
    public class DumbCurrencyCollection : ICurrencyCollection
    {
        public IReadOnlyCollection<Currency> GetCurrencies()
        {
            return new List<Currency>() { Currency.USD, Currency.EUR, Currency.RUR };
        }
    }
}
