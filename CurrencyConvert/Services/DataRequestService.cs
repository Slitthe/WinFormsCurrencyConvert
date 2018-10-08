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

        public static async Task<ResponseMessageDto> GetTypesDataList(string apiKey)
        {
            string checkKeyUrl = "http://data.fixer.io/api/symbols?access_key=" + apiKey;

            ResponseMessageDto checkKeyUrlRequest = await RequestData(checkKeyUrl);

            // check if response is True then return the data else just return null
            return checkKeyUrlRequest.Success ? checkKeyUrlRequest : null;
        }

        public static async Task<ResponseMessageDto> RequestData(string url)
        {
            Cursor.Current = Cursors.WaitCursor;
            ResponseMessageDto deserializedResponse;
            try
            {
                HttpResponseMessage responseMessage =
                    await Client.GetAsync(url);

                HttpStatusCode statusCode = responseMessage.StatusCode;

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
