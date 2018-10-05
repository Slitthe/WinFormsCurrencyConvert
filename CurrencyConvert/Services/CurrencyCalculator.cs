using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConvert.Enums;

namespace CurrencyConvert.Services
{
    class CurrencyCalculator
    {
        public static Dictionary<Currencies, float> GetRate(Dictionary<Currencies, float> currencyList, Currencies baseCurrency)
        {
            var convertedList = new Dictionary<Currencies, float>();

            foreach (var currency in currencyList)
            {
                float baseRate = currencyList[baseCurrency];
                float targetRate = currencyList[currency.Key];

                float baseToTargetRate = baseRate / targetRate;

                convertedList.Add(currency.Key, baseToTargetRate);
            }

            return convertedList;
        }
    }
}
