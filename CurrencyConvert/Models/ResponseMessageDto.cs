using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConvert.Enums;

namespace CurrencyConvert.Models
{
    class ResponseMessageDto
    {
        public bool Success { get; set; }
        public ErrorStatusCode Error { get; set; }
        public Dictionary<Currencies, float> Rates { get; set; }
        public Dictionary<Currencies, string> Symbols { get; set; }

        public DateTime Date { get; set; }
        public Currencies Base { get; set; }
    }
}
