using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConvert.Enums;

namespace CurrencyConvert.Services
{
    class ApiUrlConstructors
    {
        public ApiUrlConstructors(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string GetGetRatesUrl(IEnumerable<Currencies> currencyList, Currencies baseCurrency)
        {
            var baseApi = BaseApi + "latest";

            var currencySymbols = currencyList
                .Concat(new List<Currencies> {baseCurrency})
                .Select(currencySymbol => currencySymbol.ToString())
                .ToArray();

            string symbolsToConvertTo = string.Join(",", currencySymbols);

            var urlParams = new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("base", Currencies.EUR.ToString()),
                    new Tuple<string, string>("symbols", symbolsToConvertTo)
                };
            return AddUrlParameters(urlParams, baseApi);
        }

        // need to add an url to get the one currency to another convert rate

        private string AddUrlParameters(IEnumerable<Tuple<string, string>> urlParams, string url)
        {
            var paramsAsKeyValuePairs = new List<string>()
            {
                $"access_key={_apiKey}"
            };

            foreach (var urlParam in urlParams)
            {
                paramsAsKeyValuePairs.Add($"{urlParam.Item1}={urlParam.Item2}");
            }
            string joinedParams = string.Join("&", paramsAsKeyValuePairs);

            var urlWithParams = $"{url}?{joinedParams}";
            return urlWithParams;
        }

        private readonly string _apiKey;
        private const string BaseApi = "http://data.fixer.io/api/";
        
    }
}
