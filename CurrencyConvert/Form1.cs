using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CurrencyConvert.Data;
using CurrencyConvert.Models;
using CurrencyConvert.Services;
using CurrencyConvertService;
using System.Threading.Tasks;

namespace CurrencyConvert
{

    public partial class Form1 : Form
    {
        private const string DefaultBaseCurrency = "EUR";
        private const string InvalidKeyMessage = "Invalid key, try again.";

        private readonly CurrencyData _currencyData = new CurrencyData();
        private readonly string[] _defaultConvertCurrenciesList = {"USD", "GBP", "RON"};

        private CurrencyConvertCalculator _currencyCalculator;

        public Form1()
        {
            InitializeComponent();
        }


        #region EVENTS
        private async void apiKeyValidationButton_Click(object sender, EventArgs e)
        {
            await ValidateKey();
        }
        private void getRatesButton_Click(object sender, EventArgs e)
        {
            ConvertRatesGrid();
        }
        private void baseCurrency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var senderData = (ComboBox) sender;
            string currencySymbol = (string) this._currencyData.NameToCode[senderData.SelectedValue.ToString()];

            ChangeBaseCurrency(currencySymbol);
        }
        private void convertToButton_Click(object sender, EventArgs e)
        {
            ApplyToAndFromCurrencyConversion();

        }
        private void switchCurrenciesConvertButton_Click(object sender, EventArgs e)
        {
            SwitchCurrencies();
        }
        private void convertFromAmountInput_ValueChanged(object sender, EventArgs e)
        {
            ApplyToAndFromCurrencyConversion();
        }
        private void ratesDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            ConvertRatesGrid();
        }
        #endregion


        #region APP STARTUP

        private async Task ValidateKey()
        {
            Cursor.Current = Cursors.WaitCursor;

            var key = apiKeyValidationInput.Text;

            ResponseMessageDto symbolsData = await DataRequestService.GetTypesDataListAsync(key);
            ResponseMessageDto currenciesRates = await DataRequestService.GetRatesAsync(key);

            if (symbolsData != null && currenciesRates != null)
            {
                ValidKeyActions(key, symbolsData, currenciesRates);
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

        private void ValidKeyActions(string key, ResponseMessageDto typesData, ResponseMessageDto ratesData)
        {
            StoreCurrenciesRates(ratesData);
            InitializeData(typesData);

            new ApiUrlConstructors(key);
            apiKeyValidationInfo.ForeColor = Color.Black;
            apiKeyValidationInfo.Text = "";

            apiKeyValidationButton.Enabled = false;
            apiKeyValidationInput.ReadOnly = true;
            controlsPanel.Visible = true;
        }

        private void StoreCurrenciesRates(ResponseMessageDto ratesData)
        {
            Dictionary<string, float> rates = ratesData.Rates;
            _currencyCalculator = new CurrencyConvertCalculator(rates);
        }


        #endregion


        private void SwitchCurrencies()
        {
            var convertFromCurrencyValue = convertFromDropdownInput.SelectedValue;
            convertFromDropdownInput.SelectedItem = convertToDropdownInput.SelectedItem;
            convertToDropdownInput.SelectedItem = convertFromCurrencyValue;

            ApplyToAndFromCurrencyConversion();
        }

        private void ChangeBaseCurrency(string currencySymbol)
        {
            _currencyData.BaseCurrency = currencySymbol;
            currentCurrencyDisplayText.Text = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);

            ConvertRatesGrid();
        }

        private void ApplyToAndFromCurrencyConversion()
        {
            float convertAmount = (float) convertFromAmountInput.Value;

            string toCurrency = (string) _currencyData.NameToCode[convertToDropdownInput.SelectedValue.ToString()];
            string fromCurrency = (string) _currencyData.NameToCode[convertFromDropdownInput.SelectedValue.ToString()];

            var convertResult = _currencyCalculator.ConvertFromAndTo(fromCurrency, toCurrency, convertAmount);
            convertResultTextbox.Text = convertResult.ToString();
        }

        
        private void DisplayRatesInGridView()
        {
            foreach (DataGridViewRow row in ratesDataGridView.Rows)
            {
                var currentCellCurrencyCode = (string) _currencyData.NameToCode[row.Cells[0].Value.ToString()];

                row.Cells[1].Value = _currencyCalculator.ConvertFromAndTo(_currencyData.BaseCurrency, currentCellCurrencyCode, 1);
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




        #region INITIALIZE
        private void InitializeData(ResponseMessageDto typesData)
        {
            AddCurrenciesNamesAndKeys(typesData);

            SetCurrenciesDefaultOrExistingValues();
            CurrentCurrencyInitialize();

            DataGridDefine();
            DataGridPopulate();

            ToAndFromConvertInitialize();
            ConvertRatesGrid();

            ratesDataGridView.CurrentCellDirtyStateChanged += ratesDataGridView_CurrentCellDirtyStateChanged;
        }

        private void AddCurrenciesNamesAndKeys(ResponseMessageDto typesData)
        {
            foreach (var symbolItem in typesData.Symbols)
            {
                _currencyData.NameToCode.Add(symbolItem.Value, symbolItem.Key);
            }
        }
        private void SetCurrenciesDefaultOrExistingValues()
        {
            _currencyData.BaseCurrency = _currencyData.NameToCode.ContainsValue(DefaultBaseCurrency)
                ? DefaultBaseCurrency
                : _currencyData.NameToCode.Values.First();

            bool allCurrenciesExist = CheckCurrenciesExistence(_defaultConvertCurrenciesList);
            for (int i = 0; i < _defaultConvertCurrenciesList.Length; i++)
            {
                _currencyData.ConvertCurrencyList[i] = allCurrenciesExist
                    ? _defaultConvertCurrenciesList[i]
                    : _currencyData.NameToCode.Values.Skip(i).Take(1).Single();
            }
        }
        private void CurrentCurrencyInitialize()
        {

            currentCurrencySelectDropdown.DataSource = _currencyData.NameToCode.Keys.ToList();
            currentCurrencySelectDropdown.SelectedItem = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
            currentCurrencySelectDropdown.DropDownStyle = ComboBoxStyle.DropDownList;

            currentCurrencyDisplayText.Text = _currencyData.CodeEnumToLongName(_currencyData.BaseCurrency);
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
        private void ConvertRatesGrid()
        {
            DisplayRatesInGridView();
            UpdateCurrencyListValues();
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
        #endregion

    }
}
