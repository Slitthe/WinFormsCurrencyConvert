using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace CurrencyConvertService.Helpers
{
    internal class ApiUrlConstructors
    {
        private readonly Uri _baseApi = new Uri("http://data.fixer.io/api/");
        private readonly string _apiKey = "f5707e763cc731ce1c49bf668d66945c";
        private readonly string _apiKeyQueryName = "access_key";

        public Uri GetSymbolsUrl()
        {
            Uri url = new Uri($"{_baseApi}symbols");
            url = AddApiKeyToUrl(url);

            return url;
        }

        public Uri GetRatesUrl()
        {
            Uri url = new Uri($"{_baseApi}latest");
            url = AddApiKeyToUrl(url);

            return url;
        }
        private Uri AddUrlParameters(IEnumerable<(string name, string value)> urlQueryParams, Uri url)
        {
            var uriBuilder = new UriBuilder(url);


            NameValueCollection urlParameters = HttpUtility.ParseQueryString("");

            foreach (var urlQueryParam in urlQueryParams)
            {
                urlParameters[urlQueryParam.name] = urlQueryParam.value;
            }

            uriBuilder.Query = urlParameters.ToString();

            return uriBuilder.Uri;
        }

        private Uri AddApiKeyToUrl(Uri url)
        {
            var apiKeyQueryParam = new List<(string name, string value)>
            {
                (_apiKeyQueryName, _apiKey)
            };

            return AddUrlParameters(apiKeyQueryParam, url);
        }
    }
}