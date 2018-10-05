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
using CurrencyConvert.Services;
using Newtonsoft.Json;

namespace CurrencyConvert
{


    public partial class Form1 : Form
    {
        private string _key;
        private ApiConstructors _apiConstructors;

        private readonly CurrencyData _currencyData = new CurrencyData();

        private readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            CustomInitialize();

            DefineRatesDataGridView();
            InitializeDefaultCurrencyValues();

            PopulateDataGridViewWithDefaultValues();

        }

        private void PopulateDataGridViewWithDefaultValues()
        {
            foreach (var currency in _currencyData.ConvertCurrencyList)
            {
                ratesDataGridView.Rows.Add(currency, 0);
            }
        }

        private void DefineRatesDataGridView()
        {
            var currencyCol = new DataGridViewComboBoxColumn()
            {
                Name = "currency",
                HeaderText = "Currency",
                ReadOnly = false,
                ValueType = typeof(Currencies),
                DataSource = Enum.GetValues(typeof(Currencies))
                
            };
            var currencyRate = new DataGridViewTextBoxColumn
            {
                Name = "rate",
                HeaderText = "Rate",
                ReadOnly = true,
                ValueType = typeof(int)
            };



            ratesDataGridView.Columns.Add(currencyCol);
            ratesDataGridView.Columns.Add(currencyRate);
        }

        private void InitializeDefaultCurrencyValues()
        {
            _currencyData.BaseCurrency = Currencies.EUR;
            _currencyData.ConvertCurrencyList = new Currencies[3] { Currencies.USD, Currencies.GBP, Currencies.RON };
            currentCurrencySelectDropdown.SelectedItem = _currencyData.BaseCurrency;
        }

        private void CustomInitialize()
        {
            currentCurrencySelectDropdown.DataSource = Enum.GetValues(typeof(Currencies));
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
                ValidKeyActions(key);
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

        private void ValidKeyActions(string key)
        {
            _apiConstructors = new ApiConstructors(key);
            _key = key;
            apiKeyValidationInfo.ForeColor = Color.Black;
            apiKeyValidationInfo.Text = "The key is correct.";

            apiKeyValidationButton.Enabled = false;
            apiKeyValidationInput.ReadOnly = true;
            controlsPanel.Visible = true;
        }


        private async void GetAndDisplayRates()
        {
            var ratesApi = _apiConstructors.GetGetRatesUrl(_currencyData.ConvertCurrencyList, _currencyData.BaseCurrency);

            HttpResponseMessage responseMessage = await client.GetAsync(ratesApi);

            HttpStatusCode statusCode = responseMessage.StatusCode;

            string responseString = await responseMessage.Content.ReadAsStringAsync();

            ResponseMessageDto deserializedResponse = JsonConvert.DeserializeObject<ResponseMessageDto>(responseString);

            foreach (var rate in deserializedResponse.Rates)
            {
                var currentRate = (Currencies)rate.Key;
                foreach (DataGridViewRow row in ratesDataGridView.Rows)
                {
                    
                    var currenctCellCurrency = (Currencies)row.Cells[0].Value;
                    if (currenctCellCurrency== currentRate)
                    {
                        row.Cells[1].Value = rate.Value;
                    }
                }
            }
        }

        private async Task<bool> CheckKey(string apiKey)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("http://data.fixer.io/api/latest?access_key=" + apiKey);

            HttpStatusCode statusCode = responseMessage.StatusCode;

            string responseString = await responseMessage.Content.ReadAsStringAsync();

            ResponseMessageDto deserializedProduct = JsonConvert.DeserializeObject<ResponseMessageDto>(responseString);

            return deserializedProduct.Error == null;
        }

        private void baseCurrency_SelectionChangeCommitted(object sender, EventArgs e)
        {

            // 1. change the Base currency both visually and in the program's data
            // 2. if the currency is different than the previous one, re-fetch the current rates
            var senderData = (ComboBox) sender;
            var value = (Currencies)senderData.SelectedValue;

            _currencyData.BaseCurrency = value;
            currentCurrencyDisplayText.Text = value.ToString();
        }

        private void getRatesButton_Click(object sender, EventArgs e)
        {
            UpdateRatesValues();
            // iterate through the data grid view row and get the rates
            GetAndDisplayRates();
        }

        private void UpdateRatesValues()
        {
            
            var rows = ratesDataGridView.Rows;

            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewRow currentRow = rows[i];
                Currencies currentRate = (Currencies)currentRow.Cells[0].Value;

                _currencyData.ConvertCurrencyList[i] = currentRate;
            }

        }
    }
}
