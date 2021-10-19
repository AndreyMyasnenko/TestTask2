using System;

namespace ExchangeManager
{
    public class CurrencyUnavailableException : Exception
    {
        public CurrencyUnavailableException(string message) : base(message)
        {         
        }
    }
}
