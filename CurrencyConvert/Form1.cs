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


            //convertFromDropdownInput

            convertFromAmountInput.Maximum = decimal.MaxValue;
            convertFromAmountInput.Minimum = 0;

            convertToDropdownInput.DataSource = Enum.GetValues(typeof(Currencies));
            convertFromDropdownInput.DataSource = Enum.GetValues(typeof(Currencies));

            convertFromDropdownInput.SelectedItem = _currencyData.BaseCurrency;
            convertToDropdownInput.SelectedItem = _currencyData.BaseCurrency;
        }


        // EVENTS
        private void apiKeyValidationButton_Click(object sender, EventArgs e)
        {
            CheckApiKey();
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
            apiKeyValidationInfo.Text = "";
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

            var convertedRates = CurrencyCalculator.GetRate(deserializedResponse.Rates, _currencyData.BaseCurrency);

            foreach (var rate in convertedRates)
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

        private async void convertToButton_Click(object sender, EventArgs e)
        {
            float convertAmount = (float)convertFromAmountInput.Value;
            // get rates using the source and target currencies
            Currencies fromCurrency = (Currencies)convertFromDropdownInput.SelectedValue;
            // get the source currency
            Currencies toCurrency = (Currencies) convertToDropdownInput.SelectedValue;


            // get the target currency
            var ratesApi = _apiConstructors.GetGetRatesUrl(new List<Currencies>() {fromCurrency}, toCurrency);
            
            HttpResponseMessage responseMessage = await client.GetAsync(ratesApi);
            HttpStatusCode statusCode = responseMessage.StatusCode;
            string responseString = await responseMessage.Content.ReadAsStringAsync();
            ResponseMessageDto deserializedResponse = JsonConvert.DeserializeObject<ResponseMessageDto>(responseString);

            var convertedRates = CurrencyCalculator.GetRate(deserializedResponse.Rates, fromCurrency);
            // calculate and display the rate


            var rate = convertedRates[fromCurrency] / convertedRates[toCurrency];
            convertResultTextbox.Text = (rate * convertAmount).ToString();

        }
    }
}
