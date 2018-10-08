using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvert.Data
{
    class CurrencyData
    {
        public readonly List<string> CurrencyList;
        public string BaseCurrency { get; set; }
        public readonly Dictionary<string, string> NameToCode;
        public string[] ConvertCurrencyList { get; set; }

        public CurrencyData()
        {
            ConvertCurrencyList = new string[3];
            CurrencyList = new List<string>();
            NameToCode = new Dictionary<string, string>();
        }

        public string CodeEnumToLongName(string code)
        {
            return NameToCode.Single(item => item.Value == code).Key;
        }

    }
}
