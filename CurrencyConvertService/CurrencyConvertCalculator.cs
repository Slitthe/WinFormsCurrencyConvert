using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvertService
{
    public class CurrencyConvertCalculator
    {
        private readonly Dictionary<string, float> _currencyRates = new Dictionary<string, float>();

        public CurrencyConvertCalculator(Dictionary<string, float> currencies)
        {
            _currencyRates = currencies;
        }

        public float ConvertFromAndTo(string fromCode, string toCode, float amount)
        {
            var calculateResult = RatesCalculator.CalculateCurrencyConvertAmountResult(fromCode, toCode, _currencyRates, amount);
            return calculateResult != -1 ? calculateResult : -1;
        } 
    }
}
