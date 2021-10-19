using System;

namespace ExchangeManager
{
    public class CurrencyRateUnavailableException : Exception
    {
        public CurrencyRateUnavailableException(string message) : base(message)
        { }
    }
}
