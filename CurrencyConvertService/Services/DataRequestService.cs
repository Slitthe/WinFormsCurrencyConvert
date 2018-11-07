using System;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyConvertService.Helpers;
using CurrencyConvertService.Models;
using Newtonsoft.Json;

namespace CurrencyConvertService.Services
{
    public class DataRequestService
    {
        private readonly ApiUrlConstructors _apiUrlConstructors;

        public DataRequestService()
        {
                _apiUrlConstructors = new ApiUrlConstructors();
        }
        

        public async Task<ResponseMessageDto> GetSymbolsAsync()
        {
            Uri symbolsUrl = _apiUrlConstructors.GetSymbolsUrl();

            ResponseMessageDto symbolsResponseData = await RequestDataAsync(symbolsUrl);
            if (symbolsResponseData == null || !symbolsResponseData.Success)
            {
                throw new HttpRequestException("The request to get the currenc symbols list failed.");

            }

            return symbolsResponseData;
        }

        public async Task<ResponseMessageDto> GetRatesAsync()
        {
            Uri ratesUrl = _apiUrlConstructors.GetRatesUrl();

            ResponseMessageDto getRatesResponse = await RequestDataAsync(ratesUrl);
            if (getRatesResponse?.Rates == null)
            {
               throw new HttpRequestException("The request to get the currency rates failed.");
            }

            return getRatesResponse;

        }

        private async Task<ResponseMessageDto> RequestDataAsync(Uri url)
        {
            var httpClient = new HttpClient();
            ResponseMessageDto deserializedResponse;

            try
            {
                 HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
                string responseString = await responseMessage.Content.ReadAsStringAsync();

                deserializedResponse = JsonConvert.DeserializeObject<ResponseMessageDto>(responseString);
            }
            catch (HttpRequestException)
            {
                deserializedResponse = null;
            }
            
            return deserializedResponse;
        }
    }
}