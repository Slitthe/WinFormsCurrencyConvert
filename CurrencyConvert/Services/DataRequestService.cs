using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CurrencyConvert.Models;
using Newtonsoft.Json;

namespace CurrencyConvert.Services
{
    public static class DataRequestService
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<ResponseMessageDto> GetTypesDataListAsync(string apiKey)
        {
            Uri checkKeyUrl = new UriBuilder($"http://data.fixer.io/api/symbols?access_key={apiKey}").Uri;

            ResponseMessageDto checkKeyUrlRequest = await RequestDataAsync(checkKeyUrl);

            if (checkKeyUrlRequest == null || !checkKeyUrlRequest.Success)
            {
                return null;
            }

            return checkKeyUrlRequest;
        }

        public static async Task<ResponseMessageDto> GetRatesAsync(string apiKey)
        {
            Uri getRatesUrl = new UriBuilder($"http://data.fixer.io/api/latest?access_key={apiKey}").Uri;

            ResponseMessageDto getRatesResponse = await RequestDataAsync(getRatesUrl);

            if (getRatesResponse.Rates == null)
            {
                return null;
            }

            return getRatesResponse;

        }

        public static async Task<ResponseMessageDto>  RequestDataAsync(Uri url)
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
