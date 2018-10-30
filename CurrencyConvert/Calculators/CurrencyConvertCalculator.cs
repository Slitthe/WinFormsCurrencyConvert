using System;
using System.Collections.Generic;

namespace CurrencyConvertService.Calculators
{
    public class CurrencyConvertCalculator
    {
        private readonly Dictionary<string, float> _currencyRates;

        public CurrencyConvertCalculator(Dictionary<string, float> currencies)
        {
            _currencyRates = currencies;
        }


        public float ConvertCurrencyAmount(string sourceCurrencyCode, string targetCurrencyCode, float amountToConvert)
        {
            float convertRate = GetCurrencyConvertRatio(sourceCurrencyCode, targetCurrencyCode);

            return amountToConvert * convertRate;
        }



        private float GetCurrencyConvertRatio(string fromCurrencyCode, string toCurrencyCode)
        {
            if (_currencyRates.ContainsKey(fromCurrencyCode) && _currencyRates.ContainsKey(toCurrencyCode))
            {
                float rate = _currencyRates[toCurrencyCode] / _currencyRates[fromCurrencyCode];
                return rate;
            }

            throw new InvalidOperationException("Cannot convert to/from a currency that does not exist in the list.");
        }
    }
}
