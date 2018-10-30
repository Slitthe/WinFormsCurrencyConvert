using System;
using System.Collections.Generic;

namespace CurrencyConvertService.Models
{
    /// <summary>
    /// DTO For the full message sent back from the API
    /// </summary>
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
