using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConvert.Enums;

namespace CurrencyConvert.Models
{
    class CurrencyData
    {
        public Currencies BaseCurrency { get; set; }
        public Currencies[] ConvertCurrencyList { get; set; }
    }
}
