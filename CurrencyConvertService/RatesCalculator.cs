using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvertService
{
    public static class RatesCalculator
    {

        public static float GetCurrenciesConvertRatio(string fromCurrencyCode, string toCurrencyCode, Dictionary<string, float> currencies)
        {
            if (currencies.ContainsKey(fromCurrencyCode) && currencies.ContainsKey(toCurrencyCode))
            {
                float rate = currencies[toCurrencyCode] / currencies[fromCurrencyCode];
                return rate;
            }

            return -1;
        }

        public static float CalculateCurrencyConvertAmountResult(string fromCurrency, string toCurrency, Dictionary<string, float> currencies, float amountToConvert)
        {
            var convertRate = GetCurrenciesConvertRatio(fromCurrency, toCurrency, currencies);

            if (convertRate != -1)
            {
                return amountToConvert * convertRate;
            }

            return -1;
        }
    }
}
