using System.Collections.Generic;
using System.Linq;

namespace CurrencyConvertService.Data
{

    public class CurrencyData
    {
        public readonly Dictionary<string, string> NameToCode;
        public string BaseCurrency { get; set; }
        public string[] ConvertCurrencyList { get; }

        public CurrencyData()
        {
            ConvertCurrencyList = new string[3];
            NameToCode = new Dictionary<string, string>();
        }

        public string CodeEnumToLongName(string code) => NameToCode.Single(item => item.Value == code).Key;
    }
}
