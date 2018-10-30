using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using CurrencyConvertService;


namespace CurrencyConvert
{

    public partial class Form1 : Form
    {
        private const string InvalidKeyMessage = "Invalid key, try again.";
        private List<string> ConvertCurrencyList { get; }

        private ConvertService _currencyConvertService = new ConvertService();

        public Form1()
        {
            ConvertCurrencyList = new List<string>(3) { "USD", "GBP", "RON"};
            InitializeComponent();
        }

        #region EVENTS
        private async void apiKeyValidationButton_Click(object sender, EventArgs e)
        {
            await ValidateRequestsData();
        }
        private void getRatesButton_Click(object sender, EventArgs e)
        {
            ConvertRatesGrid();
        }
        private void baseCurrency_SelectionChangeCommitted(object sender, EventArgs e)
        { 
            var senderData = (ComboBox) sender;
            string currencySymbol = _currencyConvertService.ConvertData.CurrencyLongNameToCode(senderData.SelectedValue.ToString());

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


        private async Task ValidateRequestsData()
        {
            Cursor.Current = Cursors.WaitCursor;

            var isInitializeSuccesful = await _currencyConvertService.InitializeServiceAttempt();

            if (isInitializeSuccesful)
            {

                ValidRequestsActions();
            }
            else
            {
                InvalidRequestsActions();
            }

            Cursor.Current = Cursors.Default;
        }

        private void InvalidRequestsActions()
        {
            apiKeyValidationInfo.ForeColor = Color.DarkRed;
            apiKeyValidationInfo.Text = InvalidKeyMessage;
        }

        private void ValidRequestsActions()
        {
            InitializeData();
            
            apiKeyValidationInfo.ForeColor = Color.Black;
            apiKeyValidationInfo.Text = "";

            startApplicationButton.Visible = false;
            controlsPanel.Visible = true;
        }

        #endregion


        #region INITIALIZE
        private void InitializeData(/*ResponseMessageDto typesData*/)
        {

            CurrentCurrencyInitialize();

            DataGridDefine();
            DataGridPopulate();

            ToAndFromConvertInitialize();
            ConvertRatesGrid();

            ratesDataGridView.CurrentCellDirtyStateChanged += ratesDataGridView_CurrentCellDirtyStateChanged;
        }

        private void CurrentCurrencyInitialize()
        {
            string baseCurrencyAsLongName = _currencyConvertService.ConvertData.CurrencyCodeToLongName(_currencyConvertService.ConvertData.BaseCurrency);
            var longNamesValuesList = _currencyConvertService.ConvertData.CurrenciesCodeToLongName.Values.ToList();

            currentCurrencySelectDropdown.DataSource = longNamesValuesList;
            currentCurrencySelectDropdown.SelectedItem = baseCurrencyAsLongName;
            currentCurrencySelectDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            
            currentCurrencyDisplayText.Text = baseCurrencyAsLongName;

        }
        private void DataGridDefine()
        {
            var longNamesValuesList = _currencyConvertService.ConvertData.CurrenciesCodeToLongName.Values.ToList();
            var currencyCol = new DataGridViewComboBoxColumn()
            {
                Name = "currency",
                HeaderText = "Currency",
                ReadOnly = false,
                ValueType = typeof(string),
                DataSource = longNamesValuesList

            };
            var currencyRate = new DataGridViewTextBoxColumn
            {
                Name = "rate",
                HeaderText = "Rate",
                ReadOnly = true,
                ValueType = typeof(float)
            };

            ratesDataGridView.Columns.Add(currencyCol);
            ratesDataGridView.Columns.Add(currencyRate);

        }
        private void DataGridPopulate()
        {
            foreach (string currency in ConvertCurrencyList)
            {
                string currentRowName = _currencyConvertService.ConvertData.CurrencyCodeToLongName(currency);
                ratesDataGridView.Rows.Add(currentRowName, 0);
            }
        }

        private void ToAndFromConvertInitialize()
        {
            var currenciesLongNamesList = _currencyConvertService.ConvertData.CurrenciesCodeToLongName.Values.ToList();
            string baseCurrencyAsLongName = _currencyConvertService.ConvertData.CurrencyCodeToLongName(_currencyConvertService.ConvertData.BaseCurrency);

            convertFromDropdownInput.DataSource = currenciesLongNamesList;
            convertToDropdownInput.DataSource = currenciesLongNamesList;

            convertFromAmountInput.Maximum = decimal.MaxValue;
            convertFromAmountInput.Minimum = 0;

            convertFromDropdownInput.SelectedItem = baseCurrencyAsLongName;
            convertToDropdownInput.SelectedItem = baseCurrencyAsLongName;

            convertFromDropdownInput.DropDownStyle = ComboBoxStyle.DropDownList;
            convertToDropdownInput.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ConvertRatesGrid()
        {
            DisplayRatesInGridView();
            UpdateCurrencyListValues();
        }
        #endregion


        private void SwitchCurrencies()
        {
            object convertFromCurrencyValue = convertFromDropdownInput.SelectedValue;
            convertFromDropdownInput.SelectedItem = convertToDropdownInput.SelectedItem;
            convertToDropdownInput.SelectedItem = convertFromCurrencyValue;

            ApplyToAndFromCurrencyConversion();
        }

        private void ChangeBaseCurrency(string currencySymbol)
        {
            _currencyConvertService.ConvertData.SetBaseCurrency(currencySymbol);

            string changeCurrencyLongName = _currencyConvertService.ConvertData.CurrencyCodeToLongName(_currencyConvertService.ConvertData.BaseCurrency);
            currentCurrencyDisplayText.Text = changeCurrencyLongName;

            ConvertRatesGrid();
        }

        private void ApplyToAndFromCurrencyConversion()
        {
            float convertAmount = (float)convertFromAmountInput.Value;

            string toCurrency = _currencyConvertService.ConvertData.CurrencyLongNameToCode(convertToDropdownInput.SelectedValue.ToString());
            string fromCurrency = _currencyConvertService.ConvertData.CurrencyLongNameToCode(convertFromDropdownInput.SelectedValue.ToString());

            float convertResult = _currencyConvertService.ConvertCalculator.ConvertCurrencyAmount(fromCurrency, toCurrency, convertAmount);
            convertResultTextbox.Text = convertResult.ToString(CultureInfo.InvariantCulture);
        }


        private void DisplayRatesInGridView()
        {
            foreach (DataGridViewRow row in ratesDataGridView.Rows)
            {
                string currentCellCurrencyCode = _currencyConvertService.ConvertData.CurrencyLongNameToCode(row.Cells[0].Value.ToString());

                float convertedCurrencyCurrencyAmount = _currencyConvertService.ConvertCalculator.ConvertCurrencyAmount(
                    _currencyConvertService.ConvertData.BaseCurrency, currentCellCurrencyCode, 1);

                row.Cells[1].Value = convertedCurrencyCurrencyAmount;
            }
        }
        

        private void UpdateCurrencyListValues()
        {
            var rows = ratesDataGridView.Rows;

            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewRow currentRow = rows[i];
                string currentRate = _currencyConvertService.ConvertData.CurrencyLongNameToCode(currentRow.Cells[0].Value.ToString());

                ConvertCurrencyList[i] = currentRate;
            }
        }
        

    }
}
