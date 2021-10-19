using ExternalServices;
using ExternalServices.Interfaces;
using System;
using System.Linq;

namespace ExchangeManager
{
    public class Manager
    {
        private readonly ICurrencyCollection _currencyCollection;
        private readonly ICurrencyRates _currencyRates;
        private readonly ILogger _logger;

        public Manager(ICurrencyCollection currencyCollection, ICurrencyRates currencyRates, ILogger logger)
        {
            if (currencyCollection is null)
            {
                throw new ArgumentNullException(nameof(currencyCollection));
            }

            if (currencyRates is null)
            {
                throw new ArgumentNullException(nameof(currencyRates));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _currencyCollection = currencyCollection;
            _currencyRates = currencyRates;
            _logger = logger;
        }

        /// <summary>
        /// Конвертирует деньги из одной валюты в другую
        /// </summary>
        /// <param name="currencyFrom">Исходная валюта</param>
        /// <param name="currencyTo">Конечная валюта</param>
        /// <param name="amount">Сумма</param>
        /// <returns>Возвращает конвертированную сумму</returns>
        public decimal Convert(Currency currencyFrom, Currency currencyTo, decimal amount)
        {
            CheckAvailableCurrency(currencyFrom);
            decimal exchangeRateFrom = GetExchangeRate(currencyFrom);

            CheckAvailableCurrency(currencyTo);
            decimal exchangeRateTo = GetExchangeRate(currencyTo);

            return amount * exchangeRateFrom / exchangeRateTo;
        }

        /// <summary>
        /// Складывает в разных валютах две суммы
        /// </summary>
        /// <param name="currency1">Валюта первого слагаемого</param>
        /// <param name="amount1">Сумма первого слагаемого</param>
        /// <param name="currency2">Валюта второго слагаемого</param>
        /// <param name="amount2">Сумма второго слогаемого</param>
        /// <param name="resultCurrency">Валюта результата</param>
        /// <returns>Итоговая сумма в валюте resultCurrency</returns>
        public decimal Add(Currency currency1, decimal amount1, Currency currency2, decimal amount2, Currency resultCurrency)
        {
            CheckAvailableCurrency(currency1);
            decimal exchangeRate1 = GetExchangeRate(currency1);

            CheckAvailableCurrency(currency2);
            decimal exchangeRate2 = GetExchangeRate(currency2);

            CheckAvailableCurrency(resultCurrency);
            decimal exchangeRateResult = GetExchangeRate(resultCurrency);

            return (amount1 * exchangeRate1 + amount2 * exchangeRate2) / exchangeRateResult;
        }

        public decimal Substract(Currency currency1, decimal amount1, Currency currency2, decimal amount2, Currency resultCurrency)
        {
            //TODO: like Add method
            throw new NotImplementedException();
        }

        private void CheckAvailableCurrency(Currency currencyFrom)
        {
            if (!_currencyCollection.GetCurrencies().Contains(currencyFrom))
            {
                var ex = new CurrencyUnavailableException(currencyFrom.ToString());
                _logger.LogException(ex);
                throw ex;
            }
        }

        private decimal GetExchangeRate(Currency currencyFrom)
        {
            if (!_currencyRates.TryGetExchangeRate(currencyFrom, out decimal exchangeRateFrom))
            {
                var ex = new CurrencyRateUnavailableException(currencyFrom.ToString());
                _logger.LogException(ex);
                throw ex;
            }

            return exchangeRateFrom;
        }
    }
}
