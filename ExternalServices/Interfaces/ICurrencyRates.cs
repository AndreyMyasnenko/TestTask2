using System;

namespace ExternalServices.Interfaces
{
    public interface ICurrencyRates
    {
        bool TryGetExchangeRate(Currency currency, out decimal exchangeRate);
    }
}
