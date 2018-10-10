using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvertService
{
    public class CurrencyConvertCalculator
    {
        private Dictionary<string, float> currencyRates = new Dictionary<string, float>();

        public CurrencyConvertCalculator(Dictionary<string, float> currencies)
        {
            currencyRates = currencies;
        }
        public void AddCurrencyRate(string currencyCode, float currencyRate)
        {
            currencyRates.Add( currencyCode, currencyRate );
        }

        public float ConvertFromAndTo(string fromCode, string toCode, float amount)
        {
            var calculateResult =
                RatesCalculator.CalculateCurrencyConvertAmountResult(fromCode, toCode, currencyRates, amount);
            return calculateResult != -1 ? calculateResult : -1;
        } 
    }
}
