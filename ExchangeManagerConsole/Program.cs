using ExchangeManager;
using ExternalServices.Implementations;
using System;
using ExternalServices;
using ExternalServices.Interfaces;

namespace ExchangeManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var currencies = new DumbCurrencyCollection();
            var currencyRates = new DumbCurrencyRates();
            var logger = new DumbLogger();
            Manager manager = new Manager(currencies, currencyRates, logger);

            try
            {
                Console.WriteLine(manager.Convert(Currency.USD, Currency.EUR, 10));
                Console.WriteLine(manager.Add(Currency.USD, 1, Currency.EUR, -13, Currency.RUR));
                Console.WriteLine(manager.Add(Currency.GBP, 1, Currency.EUR, -13, Currency.RUR));
            }
            catch (CurrencyUnavailableException cue)
            {
                Console.WriteLine("Валюта недоступна");
            }
            catch (CurrencyRateUnavailableException crue)
            {
                Console.WriteLine("Курс валют недоступен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            }

        }

        class DumbLogger : ILogger
        {
            //test
            public void LogException(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
