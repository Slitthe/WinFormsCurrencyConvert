using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CurrencyConvertService.Models;

namespace CurrencyConvert.Services
{
    public static class ApiUrlConstructors
    {
        private static readonly Uri _baseApi = new Uri("http://data.fixer.io/api/");
        private static readonly string ApiKey = "099344cfbf5f7e75c5848a08f5cfa030";
        private static readonly string KeyParamName = "access_key";


        //public Uri GetGetRatesUrl(IEnumerable<string> currencyList, string baseCurrency)
        //{
        //    UriBuilder baseApi = new UriBuilder($"{BaseApi}");
        //    baseApi.Path += "latest";

        //    var currencySymbols = currencyList
        //        .Concat(new List<string> {baseCurrency})
        //        .Select(currencySymbol => currencySymbol.ToString())
        //        .ToArray();

        //    string symbolsToConvertTo = string.Join(",", currencySymbols);
        //    var urlParams = new List<(string paramKey, string paramName)>()
        //    {
        //        ("symbols", symbolsToConvertTo)
        //    };

        //    return AddUrlParameters(urlParams, baseApi.Uri);
        //}

        private static Uri AddUrlParameters(IEnumerable<(string paramKey, string paramName)> urlParams, Uri url)
        {
            var uriBuilder = new UriBuilder(url);


            var urlParameters = HttpUtility.ParseQueryString("");
            urlParameters[KeyParamName] = ApiKey;


            foreach (var urlParam in urlParams)
            {
                urlParameters[urlParam.paramKey] = urlParam.paramName;
            }

            uriBuilder.Query = urlParameters.ToString();

            return uriBuilder.Uri;
        }

        public static Uri GetSymbolsUrl() => new UriBuilder($"{_baseApi}symbols?access_key={ApiKey}").Uri;
        public static Uri GetRatesUrl() => new UriBuilder($"{_baseApi}latest?access_key={ApiKey}").Uri;

    }
}