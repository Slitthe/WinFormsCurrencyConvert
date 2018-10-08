using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvert.Models
{
    public class ResponseMessageDto
    {
        public bool Success { get; set; }
        public ErrorStatusCode Error { get; set; }
        public Dictionary<string, float> Rates { get; set; }
        public Dictionary<string, string> Symbols { get; set; }

        public DateTime Date { get; set; }
        public string Base { get; set; }
    }
}
