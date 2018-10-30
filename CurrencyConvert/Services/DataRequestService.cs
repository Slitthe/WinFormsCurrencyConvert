using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using CurrencyConvertService;
using CurrencyConvertService.Helpers;
using CurrencyConvertService.Models;

namespace CurrencyConvert.Services
{
    public static class DataRequestService
    {
        private static readonly HttpClient Client = new HttpClient();



        public static async Task<ResponseMessageDto> GetSymbolsAsync()
        {
            Uri symbolsUrl = ApiUrlConstructors.GetSymbolsUrl();
            
            ResponseMessageDto checkKeyUrlRequest = await RequestDataAsync(symbolsUrl);

            if (checkKeyUrlRequest == null || !checkKeyUrlRequest.Success)
            {
                throw new HttpRequestException("The symbols data cannot be found. Please check the API url and key for its validity.");
            }

            return checkKeyUrlRequest;
        }

        public static async Task<ResponseMessageDto> GetRatesAsync()
        {
            Uri ratesUrl = ApiUrlConstructors.GetRatesUrl();
            
            ResponseMessageDto getRatesResponse = await RequestDataAsync(ratesUrl);

            if (getRatesResponse?.Rates == null)
            {
                throw new HttpRequestException("The rates data cannot be found. Please check the API url and key for its validity.");
            }

            return getRatesResponse;
        }

        private static async Task<ResponseMessageDto> RequestDataAsync(Uri url)
        {
            Cursor.Current = Cursors.WaitCursor;
            ResponseMessageDto deserializedResponse;
            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(url);
                string responseString = await responseMessage.Content.ReadAsStringAsync();

                deserializedResponse = JsonConvert.DeserializeObject<ResponseMessageDto>(responseString);
            }
            catch (HttpRequestException)
            {
                deserializedResponse = null;
            }

            Cursor.Current = Cursors.Default;
            return deserializedResponse;
        }
    }
}
