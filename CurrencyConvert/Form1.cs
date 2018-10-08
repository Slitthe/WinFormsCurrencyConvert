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
using CurrencyConvert.Data;
using CurrencyConvert.Models;
using CurrencyConvert.Services;
using Newtonsoft.Json;

namespace CurrencyConvert
{


    public partial class Form1 : Form
    {
        private ApiUrlConstructors _apiUrlConstructors;
        private readonly CurrencyData _currencyData = new CurrencyData();

        public Form1()
        {
            InitializeComponent();
            

        }

        #region DATA INTIALIZATION
        private void CurrentCurrencyInitialize()
        {
            SetCurrenciesDefaultOrExistingValues();

            currentCurrencySelectDropdown.DataSource = _currencyData.NameToCode.Keys.ToList();
            currentCurrencySelectDropdown.SelectedItem = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
            currentCurrencySelectDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            currentCurrencyDisplayText.Text = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
        }
        private void SetCurrenciesDefaultOrExistingValues()
        {
            _currencyData.BaseCurrency = _currencyData.NameToCode.ContainsValue("EUR") ? "EUR" : _currencyData.NameToCode.Values.First();

            string[] defualtCurrenciesConvertList = new string[3] { "USD", "GBP", "RON" };
            bool allCurrenciesExist = true;

            foreach (var defaultCurrency in defualtCurrenciesConvertList)
            {
                if (!_currencyData.NameToCode.ContainsValue(defaultCurrency))
                {
                    allCurrenciesExist = false;
                }
            }

            for (int i = 0; i < defualtCurrenciesConvertList.Length; i++)
            {
                _currencyData.ConvertCurrencyList[i] = allCurrenciesExist ? defualtCurrenciesConvertList[i] : _currencyData.NameToCode.Values.Skip(i).Take(1).Single();
            }
        }
        private void DataGridDefine()
        {
            var currencyCol = new DataGridViewComboBoxColumn()
            {
                Name = "currency",
                HeaderText = "Currency",
                ReadOnly = false,
                ValueType = typeof(string),
                DataSource = _currencyData.NameToCode.Keys.ToList()

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
        private void DataGridPopulate()
        {
            foreach (var currency in _currencyData.ConvertCurrencyList)
            {
                var currentRowName = _currencyData.CodeEnumToLongName(currency);
                ratesDataGridView.Rows.Add(currentRowName, 0);
            }
        }
        private void ToAndFromConvertInitialize()
        {
            convertFromAmountInput.Maximum = decimal.MaxValue;
            convertFromAmountInput.Minimum = 0;

            convertFromDropdownInput.DataSource = _currencyData.NameToCode.Keys.ToList();
            convertFromDropdownInput.SelectedItem = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
            convertFromDropdownInput.DropDownStyle = ComboBoxStyle.DropDownList;

            convertToDropdownInput.DataSource = _currencyData.NameToCode.Keys.ToList();
            convertToDropdownInput.SelectedItem = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
            convertToDropdownInput.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void DataInitialization(ResponseMessageDto typesData)
        {
            foreach (var symbolItem in typesData.Symbols)
            {
                _currencyData.NameToCode.Add(symbolItem.Value, symbolItem.Key);
                _currencyData.CurrencyList.Add(symbolItem.Key);
            }

            CurrentCurrencyInitialize();

            DataGridDefine();
            DataGridPopulate();

            ToAndFromConvertInitialize();
        }


        #endregion


        #region EVENTS
        private void apiKeyValidationButton_Click(object sender, EventArgs e)
        {
            CheckApiKey();
        }
        private void baseCurrency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var senderData = (ComboBox) sender;
            string value = (string) this._currencyData.NameToCode[senderData.SelectedValue.ToString()];
            
            _currencyData.BaseCurrency = value;
            currentCurrencyDisplayText.Text = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
        }
        private void getRatesButton_Click(object sender, EventArgs e)
        {
            UpdateRatesValues();
            GetAndDisplayRates();
        }
        private async void convertToButton_Click(object sender, EventArgs e)
        {
            float convertAmount = (float) convertFromAmountInput.Value;

            
            string toCurrency =
                (string) _currencyData.NameToCode[convertToDropdownInput.SelectedValue.ToString()];
            string fromCurrency =
                (string) _currencyData.NameToCode[convertFromDropdownInput.SelectedValue.ToString()];

            

            
            string ratesUrl = _apiUrlConstructors.GetGetRatesUrl(new List<string>() {fromCurrency}, toCurrency);
            ResponseMessageDto responseMessage = await DataRequestService.RequestData(ratesUrl);

            if (responseMessage != null)
            {
                var convertedRates = CurrencyCalculator.GetRate(responseMessage.Rates, fromCurrency);
                float rate = convertedRates[fromCurrency] / convertedRates[toCurrency];
                convertResultTextbox.Text = (rate * convertAmount).ToString();

            }

        }
        private void swichCurrenciesConvertButton_Click(object sender, EventArgs e)
        {
            var convertFromCurrencyValue = convertFromDropdownInput.SelectedValue;
            convertFromDropdownInput.SelectedItem = convertToDropdownInput.SelectedItem;
            convertToDropdownInput.SelectedItem = convertFromCurrencyValue;
        }
        #endregion


        #region Action methods
        private async void CheckApiKey()
        {
            Cursor.Current = Cursors.WaitCursor;

            var key = apiKeyValidationInput.Text;
            ResponseMessageDto symbolsData = await DataRequestService.GetTypesDataList(key);

            if (symbolsData != null)
            {
                ValidKeyActions(key, symbolsData);
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
            apiKeyValidationInfo.Text = "Invalid key, try again.";
        }
        private void ValidKeyActions(string key, ResponseMessageDto typesData)
        {
            DataInitialization(typesData);

            _apiUrlConstructors = new ApiUrlConstructors(key);
            apiKeyValidationInfo.ForeColor = Color.Black;
            apiKeyValidationInfo.Text = "";

            apiKeyValidationButton.Enabled = false;
            apiKeyValidationInput.ReadOnly = true;
            controlsPanel.Visible = true;
        }
        private async void GetAndDisplayRates()
        {
            string ratesUrl = _apiUrlConstructors.GetGetRatesUrl(_currencyData.ConvertCurrencyList, _currencyData.BaseCurrency);

            ResponseMessageDto responseData = await DataRequestService.RequestData(ratesUrl);

            if (responseData != null)
            {
                var convertedRates = CurrencyCalculator.GetRate(responseData.Rates, _currencyData.BaseCurrency);
                DisplayRatesInGridView(convertedRates);

            }

        }
        private void DisplayRatesInGridView(Dictionary<string, float> convertedRates)
        {
            foreach (var rate in convertedRates)
            {
                var currentRate = (string)rate.Key;
                foreach (DataGridViewRow row in ratesDataGridView.Rows)
                {

                    var currenctCellCurrency = (string)_currencyData.NameToCode[row.Cells[0].Value.ToString()];
                    if (currenctCellCurrency == currentRate)
                    {
                        row.Cells[1].Value = rate.Value;
                    }
                }
            }
        }
        private void UpdateRatesValues()
        {
            var rows = ratesDataGridView.Rows;

            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewRow currentRow = rows[i];
                string currentRate = (string)_currencyData.NameToCode[currentRow.Cells[0].Value.ToString()];

                _currencyData.ConvertCurrencyList[i] = currentRate;
            }

        }
        #endregion


    }
}
