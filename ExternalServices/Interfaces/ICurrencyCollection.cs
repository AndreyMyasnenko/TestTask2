using System;
using System.Collections.Generic;

namespace ExternalServices.Interfaces
{
    public interface ICurrencyCollection
    {
        IReadOnlyCollection<Currency> GetCurrencies();
    }

}
