using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvert.Services
{
    class CurrencyCalculator
    {
        public static Dictionary<string, float> GetRate(Dictionary<string, float> currencyList, string baseCurrency)
        {
            var convertedList = new Dictionary<string, float>();

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
