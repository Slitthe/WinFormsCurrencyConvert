using System;
using System.Collections.Generic;

namespace CurrencyConvertService.Calculators
{
    public class CurrencyConvertCalculator
    {
        private readonly Dictionary<string, float> _currencyCodeToRate;

        public CurrencyConvertCalculator(Dictionary<string, float> currenciesRates)
        {
            _currencyCodeToRate = currenciesRates;
        }

        public float ConvertCurrencyAmount(string baseCurrencyCode, string targetCurrencyCode, float amountToConvert)
        {
            float convertRate = GetCurrencyConvertRatio(baseCurrencyCode, targetCurrencyCode);

            float convertResult = amountToConvert * convertRate;

            return convertResult;
        }



        private float GetCurrencyConvertRatio(string sourceCurrencyCode, string targetCurrencyCode)
        {
            bool keyCodesExist = _currencyCodeToRate.ContainsKey(sourceCurrencyCode) &&
                                 _currencyCodeToRate.ContainsKey(targetCurrencyCode);

            if(keyCodesExist)
            {
                float sourceCurrencyRate = _currencyCodeToRate[sourceCurrencyCode];
                float targetCurrencyRate = _currencyCodeToRate[targetCurrencyCode];

                float sourceToTargetConvertRate = sourceCurrencyRate / targetCurrencyRate;

                return sourceToTargetConvertRate;
            }

            throw new InvalidOperationException(@"Source or target currency does not exist in the list.");
        }
    }
}
