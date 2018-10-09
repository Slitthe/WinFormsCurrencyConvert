using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CurrencyConvert.Data;
using CurrencyConvert.Models;
using CurrencyConvert.Services;

namespace CurrencyConvert
{
    
    public partial class Form1 : Form
    {
        private const string DefualtBaseCurrency = "EUR";
        private const string InvalidKeyMessage = "Invalid key, try again.";

        private readonly CurrencyData _currencyData = new CurrencyData();
        private readonly string[] _defaultConvertCurrenciesList = new string[3] {"USD", "GBP", "RON"};

        private ApiUrlConstructors _apiUrlConstructors;

        public Form1()
        {
            InitializeComponent();
        }

        #region CHECK API KEY
        private async void apiKeyValidationButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var key = apiKeyValidationInput.Text;
            ResponseMessageDto symbolsData = await DataRequestService.GetTypesDataListAsync(key);

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
            apiKeyValidationInfo.Text = InvalidKeyMessage;
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
        #endregion


        #region DATA INTIALIZATION
        private void DataInitialization(ResponseMessageDto typesData)
        {
            AddCurrenciesNamesAndKeys(typesData);

            SetCurrenciesDefaultOrExistingValues();
            CurrentCurrencyInitialize();

            DataGridDefine();
            DataGridPopulate();

            ToAndFromConvertInitialize();
        }

        private void AddCurrenciesNamesAndKeys(ResponseMessageDto typesData)
        {
            foreach (var symbolItem in typesData.Symbols)
            {
                _currencyData.NameToCode.Add(symbolItem.Value, symbolItem.Key);
                _currencyData.CurrencyList.Add(symbolItem.Key);
            }
        }

        private void CurrentCurrencyInitialize()
        {

            currentCurrencySelectDropdown.DataSource = _currencyData.NameToCode.Keys.ToList();
            currentCurrencySelectDropdown.SelectedItem = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
            currentCurrencySelectDropdown.DropDownStyle = ComboBoxStyle.DropDownList;

            currentCurrencyDisplayText.Text = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
        }

        private void SetCurrenciesDefaultOrExistingValues()
        {
            _currencyData.BaseCurrency = _currencyData.NameToCode.ContainsValue(DefualtBaseCurrency) ? DefualtBaseCurrency
                : _currencyData.NameToCode.Values.First();

            bool allCurrenciesExist = CheckCurrenciesExistence(_defaultConvertCurrenciesList);
            for (int i = 0; i < _defaultConvertCurrenciesList.Length; i++)
            {
                _currencyData.ConvertCurrencyList[i] = allCurrenciesExist ? _defaultConvertCurrenciesList[i] : _currencyData.NameToCode.Values.Skip(i).Take(1).Single();
            }
        }

        private bool CheckCurrenciesExistence(string[] defaultCurrenciesConvertList)
        {
            bool allCurrenciesExist = true;
            foreach (var defaultCurrency in defaultCurrenciesConvertList)
            {
                if (!_currencyData.NameToCode.ContainsValue(defaultCurrency))
                {
                    allCurrenciesExist = false;
                }
            }

            return allCurrenciesExist;
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
        #endregion


        #region CURRENCY CONVERT RATES
        private void getRatesButton_Click(object sender, EventArgs e)
        {
            UpdateCurrencyListValues();
            GetAndDisplayRates();
        }
        private async void GetAndDisplayRates()
        {
            string ratesUrl = _apiUrlConstructors.GetGetRatesUrl(_currencyData.ConvertCurrencyList, _currencyData.BaseCurrency);

            ResponseMessageDto responseData = await DataRequestService.RequestDataAsync(ratesUrl);

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
        private void UpdateCurrencyListValues()
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


        #region CURRENCY CONVERT TO AND FROM
        private void baseCurrency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var senderData = (ComboBox) sender;
            string value = (string) this._currencyData.NameToCode[senderData.SelectedValue.ToString()];
            
            _currencyData.BaseCurrency = value;
            currentCurrencyDisplayText.Text = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
        }
        private async void convertToButton_Click(object sender, EventArgs e)
        {
            float convertAmount = (float) convertFromAmountInput.Value;

            
            string toCurrency = (string) _currencyData.NameToCode[convertToDropdownInput.SelectedValue.ToString()];
            string fromCurrency = (string) _currencyData.NameToCode[convertFromDropdownInput.SelectedValue.ToString()];

            
            string ratesUrl = _apiUrlConstructors.GetGetRatesUrl(new List<string>() {fromCurrency}, toCurrency);
            ResponseMessageDto responseMessage = await DataRequestService.RequestDataAsync(ratesUrl);

            if (responseMessage != null)
            {
                var convertedRates = CurrencyCalculator.GetRate(responseMessage.Rates, fromCurrency);
                float rate = convertedRates[fromCurrency] / convertedRates[toCurrency];
                convertResultTextbox.Text = (rate * convertAmount).ToString();
            }
        }

        private void switchCurrenciesConvertButton_Click(object sender, EventArgs e)
        {
            var convertFromCurrencyValue = convertFromDropdownInput.SelectedValue;
            convertFromDropdownInput.SelectedItem = convertToDropdownInput.SelectedItem;
            convertToDropdownInput.SelectedItem = convertFromCurrencyValue;
        }
        #endregion

    }
}
