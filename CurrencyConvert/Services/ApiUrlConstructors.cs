using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConvert.Services
{
    class ApiUrlConstructors
    {
        private const string BaseApi = "http://data.fixer.io/api/";
        private const string KeyParamName = "access_key";

        private readonly string _apiKey;

        public ApiUrlConstructors(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string GetGetRatesUrl(IEnumerable<string> currencyList, string baseCurrency)
        {
            var baseApi = BaseApi + "latest";

            var currencySymbols = currencyList
                .Concat(new List<string> {baseCurrency})
                .Select(currencySymbol => currencySymbol.ToString())
                .ToArray();

            string symbolsToConvertTo = string.Join(",", currencySymbols);

            var urlParams = new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("symbols", symbolsToConvertTo)
                };
            return AddUrlParameters(urlParams, baseApi);
        }


        private string AddUrlParameters(IEnumerable<Tuple<string, string>> urlParams, string url)
        {
            var paramsAsKeyValuePairs = new List<string>()
            {
                $"{KeyParamName}={_apiKey}"
            };

            foreach (var urlParam in urlParams)
            {
                paramsAsKeyValuePairs.Add($"{urlParam.Item1}={urlParam.Item2}");
            }
            string joinedParams = string.Join("&", paramsAsKeyValuePairs);

            var urlWithParams = $"{url}?{joinedParams}";
            return urlWithParams;
        }




    }
}
