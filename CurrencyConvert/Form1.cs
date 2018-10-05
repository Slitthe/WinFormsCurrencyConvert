using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CurrencyConvert.Enums;
using CurrencyConvert.Models;
using Newtonsoft.Json;

namespace CurrencyConvert
{


    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            CustomInitialize();
        }

        private void CustomInitialize()
        {
            baseCurrency.DataSource = Enum.GetValues(typeof(Currencies));
        }

        private void apiKeyValidationButton_Click(object sender, EventArgs e)
        {
            CheckApiKey();
        }

        private async void CheckApiKey()
        {
            Cursor.Current = Cursors.WaitCursor;

            var key = apiKeyValidationInput.Text;
            var isKeyValid = await CheckKey(key);

            if (isKeyValid)
            {
                ValidKeyActions();
            }
            else
            {
                InvalidKeyActions();
            }

            Cursor.Current = Cursors.Default;
        }

        private void InvalidKeyActions()
        {
            apiKeyValidationInfo.ForeColor = Color.DarkRed;
            apiKeyValidationInfo.Text = "The given key is incorrect.";
        }

        private void ValidKeyActions()
        {
            apiKeyValidationInfo.ForeColor = Color.Black;
            apiKeyValidationInfo.Text = "The key is correct.";

            apiKeyValidationButton.Enabled = false;
            apiKeyValidationInput.ReadOnly = true;
            controlsPanel.Visible = true;
        }


        private async Task<bool> CheckKey(string apiKey)
        {
            HttpResponseMessage responseMessage =
                await client.GetAsync("http://data.fixer.io/api/latest?access_key=" + apiKey);

            HttpStatusCode statusCode = responseMessage.StatusCode;

            string responseString = await responseMessage.Content.ReadAsStringAsync();

            ResponseMessageDto deserializedProduct = JsonConvert.DeserializeObject<ResponseMessageDto>(responseString);

            return deserializedProduct.Error == null;
        }

        private void baseCurrency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var senderData = (ComboBox) sender;
            var value = (Currencies)senderData.SelectedValue;
        }
    }
}
