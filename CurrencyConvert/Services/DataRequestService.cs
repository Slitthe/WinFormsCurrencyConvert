using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CurrencyConvert.Models;
using Newtonsoft.Json;

namespace CurrencyConvert.Services
{
    public static class DataRequestService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> CheckKey(string apiKey)
        {
            string checkKeyUrl = "http://data.fixer.io/api/latest?access_key=" + apiKey;

            ResponseMessageDto checkKeyUrlRequest = await RequestData(checkKeyUrl);

            return checkKeyUrlRequest != null && checkKeyUrlRequest.Error == null;
        }

        public static async Task<ResponseMessageDto> RequestData(string url)
        {
            ResponseMessageDto deserializedResponse;
            try
            {
                HttpResponseMessage responseMessage =
                    await client.GetAsync(url);

                HttpStatusCode statusCode = responseMessage.StatusCode;

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
