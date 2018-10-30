using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConvertService.Data
{

    /// <summary>
    /// Contains the data required to convert currencies
    /// </summary>
    public class CurrencyConvertData
    {
        public readonly Dictionary<string, string> CurrenciesCodeToLongName;


        private const string DefaultBaseCurrency = "EUR";
        public string BaseCurrency { get; private set; }

        public CurrencyConvertData(Dictionary<string, string> currenciesLongNamesToCode)
        {
            CurrenciesCodeToLongName = currenciesLongNamesToCode;
            BaseCurrency = DefaultBaseCurrency;
        }
        public CurrencyConvertData(Dictionary<string, string> currenciesLongNamesToCode, string baseCurrency)
        {
            CurrenciesCodeToLongName = currenciesLongNamesToCode;
            BaseCurrency = baseCurrency;
        }

        public void SetBaseCurrency(string newBaseCurrency)
        {
            if (newBaseCurrency.Length != 3)
            {
                throw new ArgumentException("Currencies codes can only be 3 characters long.");
            }

            BaseCurrency = newBaseCurrency;
        }


        public string CurrencyCodeToLongName(string code)
        {
            var returnVal = CurrenciesCodeToLongName[code];
            return returnVal;
        }
        public string CurrencyLongNameToCode(string longName)
        {
            string returnVal = CurrenciesCodeToLongName.Single(item => item.Value == longName).Key;
            return returnVal;
        }
    }
}
