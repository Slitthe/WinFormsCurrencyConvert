using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CurrencyConvert.Services
{
    class ApiUrlConstructors
    {
        private readonly Uri BaseApi = new Uri("http://data.fixer.io/api/");
        private const string KeyParamName = "access_key";

        private readonly string _apiKey;

        public ApiUrlConstructors(string apiKey)
        {
            _apiKey = apiKey;
        }

        public Uri GetGetRatesUrl(IEnumerable<string> currencyList, string baseCurrency)
        {
            UriBuilder baseApi = new UriBuilder($"{BaseApi}");
            baseApi.Path += "latest";

            var currencySymbols = currencyList
                .Concat( new List<string> {baseCurrency} )
                .Select( currencySymbol => currencySymbol.ToString() )
                .ToArray();

            string symbolsToConvertTo = string.Join(",", currencySymbols);
            var urlParams = new List<(string paramKey, string paramName)>()
            {
               ("symbols", symbolsToConvertTo)
            };

            return AddUrlParameters(urlParams, baseApi.Uri);
        }

        private Uri AddUrlParameters(IEnumerable<(string paramKey, string paramName)> urlParams, Uri url)
        {
            var uriBuilder = new UriBuilder(url);


            var urlParameters = HttpUtility.ParseQueryString("");
            urlParameters[KeyParamName] = _apiKey;


            foreach (var urlParam in urlParams)
            {
                urlParameters[urlParam.paramKey] = urlParam.paramName;
            }

            uriBuilder.Query = urlParameters.ToString();
            
            return uriBuilder.Uri;
        }




    }
}
