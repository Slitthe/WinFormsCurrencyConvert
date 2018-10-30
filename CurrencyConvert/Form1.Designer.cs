namespace CurrencyConvert
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startApplicationButton = new System.Windows.Forms.Button();
            this.apiKeyValidationInfo = new System.Windows.Forms.Label();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.swichCurrenciesConvertButton = new System.Windows.Forms.Button();
            this.convertToButton = new System.Windows.Forms.Button();
            this.convertFromAmountInput = new System.Windows.Forms.NumericUpDown();
            this.convertToDropdownLabel = new System.Windows.Forms.Label();
            this.convertToDropdownInput = new System.Windows.Forms.ComboBox();
            this.convertResultLabel = new System.Windows.Forms.Label();
            this.convertResultTextbox = new System.Windows.Forms.TextBox();
            this.currencyConvertContainerLabel = new System.Windows.Forms.Label();
            this.convertFromDropdownLabel = new System.Windows.Forms.Label();
            this.convertFromDropdownInput = new System.Windows.Forms.ComboBox();
            this.convertFromAmountLabel = new System.Windows.Forms.Label();
            this.getRatesButton = new System.Windows.Forms.Button();
            this.ratesDataGridView = new System.Windows.Forms.DataGridView();
            this.currentCurrencyDisplayText = new System.Windows.Forms.Label();
            this.currentCurrencyDisplayLabel = new System.Windows.Forms.Label();
            this.currentCurrencySelectLabel = new System.Windows.Forms.Label();
            this.currentCurrencySelectDropdown = new System.Windows.Forms.ComboBox();
            this.controlsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.convertFromAmountInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // startApplicationButton
            // 
            this.startApplicationButton.Location = new System.Drawing.Point(184, 9);
            this.startApplicationButton.Margin = new System.Windows.Forms.Padding(0);
            this.startApplicationButton.Name = "startApplicationButton";
            this.startApplicationButton.Size = new System.Drawing.Size(98, 23);
            this.startApplicationButton.TabIndex = 2;
            this.startApplicationButton.Text = "Start Application";
            this.startApplicationButton.UseVisualStyleBackColor = true;
            this.startApplicationButton.Click += new System.EventHandler(this.apiKeyValidationButton_Click);
            // 
            // apiKeyValidationInfo
            // 
            this.apiKeyValidationInfo.Location = new System.Drawing.Point(151, 37);
            this.apiKeyValidationInfo.Name = "apiKeyValidationInfo";
            this.apiKeyValidationInfo.Size = new System.Drawing.Size(157, 17);
            this.apiKeyValidationInfo.TabIndex = 3;
            this.apiKeyValidationInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // controlsPanel
            // 
            this.controlsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlsPanel.Controls.Add(this.panel1);
            this.controlsPanel.Controls.Add(this.getRatesButton);
            this.controlsPanel.Controls.Add(this.ratesDataGridView);
            this.controlsPanel.Controls.Add(this.currentCurrencyDisplayText);
            this.controlsPanel.Controls.Add(this.currentCurrencyDisplayLabel);
            this.controlsPanel.Controls.Add(this.currentCurrencySelectLabel);
            this.controlsPanel.Controls.Add(this.currentCurrencySelectDropdown);
            this.controlsPanel.Location = new System.Drawing.Point(15, 57);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(462, 404);
            this.controlsPanel.TabIndex = 4;
            this.controlsPanel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.swichCurrenciesConvertButton);
            this.panel1.Controls.Add(this.convertToButton);
            this.panel1.Controls.Add(this.convertFromAmountInput);
            this.panel1.Controls.Add(this.convertToDropdownLabel);
            this.panel1.Controls.Add(this.convertToDropdownInput);
            this.panel1.Controls.Add(this.convertResultLabel);
            this.panel1.Controls.Add(this.convertResultTextbox);
            this.panel1.Controls.Add(this.currencyConvertContainerLabel);
            this.panel1.Controls.Add(this.convertFromDropdownLabel);
            this.panel1.Controls.Add(this.convertFromDropdownInput);
            this.panel1.Controls.Add(this.convertFromAmountLabel);
            this.panel1.Location = new System.Drawing.Point(8, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 142);
            this.panel1.TabIndex = 15;
            // 
            // swichCurrenciesConvertButton
            // 
            this.swichCurrenciesConvertButton.Location = new System.Drawing.Point(234, 5);
            this.swichCurrenciesConvertButton.Name = "swichCurrenciesConvertButton";
            this.swichCurrenciesConvertButton.Size = new System.Drawing.Size(83, 24);
            this.swichCurrenciesConvertButton.TabIndex = 20;
            this.swichCurrenciesConvertButton.Text = "Switch";
            this.swichCurrenciesConvertButton.UseVisualStyleBackColor = true;
            this.swichCurrenciesConvertButton.Click += new System.EventHandler(this.switchCurrenciesConvertButton_Click);
            // 
            // convertToButton
            // 
            this.convertToButton.Location = new System.Drawing.Point(323, 4);
            this.convertToButton.Name = "convertToButton";
            this.convertToButton.Size = new System.Drawing.Size(120, 25);
            this.convertToButton.TabIndex = 19;
            this.convertToButton.Text = "Convert";
            this.convertToButton.UseVisualStyleBackColor = true;
            this.convertToButton.Click += new System.EventHandler(this.convertToButton_Click);
            // 
            // convertFromAmountInput
            // 
            this.convertFromAmountInput.CausesValidation = false;
            this.convertFromAmountInput.DecimalPlaces = 5;
            this.convertFromAmountInput.Location = new System.Drawing.Point(6, 91);
            this.convertFromAmountInput.Name = "convertFromAmountInput";
            this.convertFromAmountInput.Size = new System.Drawing.Size(123, 20);
            this.convertFromAmountInput.TabIndex = 16;
            this.convertFromAmountInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.convertFromAmountInput.ValueChanged += new System.EventHandler(this.convertFromAmountInput_ValueChanged);
            // 
            // convertToDropdownLabel
            // 
            this.convertToDropdownLabel.AutoSize = true;
            this.convertToDropdownLabel.Location = new System.Drawing.Point(166, 29);
            this.convertToDropdownLabel.Name = "convertToDropdownLabel";
            this.convertToDropdownLabel.Size = new System.Drawing.Size(62, 13);
            this.convertToDropdownLabel.TabIndex = 15;
            this.convertToDropdownLabel.Text = "Convert to: ";
            // 
            // convertToDropdownInput
            // 
            this.convertToDropdownInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.convertToDropdownInput.FormattingEnabled = true;
            this.convertToDropdownInput.Location = new System.Drawing.Point(169, 51);
            this.convertToDropdownInput.Name = "convertToDropdownInput";
            this.convertToDropdownInput.Size = new System.Drawing.Size(121, 21);
            this.convertToDropdownInput.TabIndex = 17;
            // 
            // convertResultLabel
            // 
            this.convertResultLabel.AutoSize = true;
            this.convertResultLabel.Location = new System.Drawing.Point(307, 31);
            this.convertResultLabel.Name = "convertResultLabel";
            this.convertResultLabel.Size = new System.Drawing.Size(40, 13);
            this.convertResultLabel.TabIndex = 16;
            this.convertResultLabel.Text = "Result:";
            // 
            // convertResultTextbox
            // 
            this.convertResultTextbox.Location = new System.Drawing.Point(310, 52);
            this.convertResultTextbox.Name = "convertResultTextbox";
            this.convertResultTextbox.ReadOnly = true;
            this.convertResultTextbox.Size = new System.Drawing.Size(121, 20);
            this.convertResultTextbox.TabIndex = 18;
            // 
            // currencyConvertContainerLabel
            // 
            this.currencyConvertContainerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currencyConvertContainerLabel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.currencyConvertContainerLabel.Enabled = false;
            this.currencyConvertContainerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currencyConvertContainerLabel.Location = new System.Drawing.Point(3, 6);
            this.currencyConvertContainerLabel.Name = "currencyConvertContainerLabel";
            this.currencyConvertContainerLabel.Size = new System.Drawing.Size(225, 23);
            this.currencyConvertContainerLabel.TabIndex = 14;
            this.currencyConvertContainerLabel.Text = "Currency Convertor";
            // 
            // convertFromDropdownLabel
            // 
            this.convertFromDropdownLabel.AutoSize = true;
            this.convertFromDropdownLabel.Location = new System.Drawing.Point(3, 31);
            this.convertFromDropdownLabel.Name = "convertFromDropdownLabel";
            this.convertFromDropdownLabel.Size = new System.Drawing.Size(73, 13);
            this.convertFromDropdownLabel.TabIndex = 6;
            this.convertFromDropdownLabel.Text = "Convert from: ";
            // 
            // convertFromDropdownInput
            // 
            this.convertFromDropdownInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.convertFromDropdownInput.FormattingEnabled = true;
            this.convertFromDropdownInput.Location = new System.Drawing.Point(6, 51);
            this.convertFromDropdownInput.Name = "convertFromDropdownInput";
            this.convertFromDropdownInput.Size = new System.Drawing.Size(121, 21);
            this.convertFromDropdownInput.TabIndex = 8;
            // 
            // convertFromAmountLabel
            // 
            this.convertFromAmountLabel.AutoSize = true;
            this.convertFromAmountLabel.Location = new System.Drawing.Point(3, 75);
            this.convertFromAmountLabel.Name = "convertFromAmountLabel";
            this.convertFromAmountLabel.Size = new System.Drawing.Size(46, 13);
            this.convertFromAmountLabel.TabIndex = 7;
            this.convertFromAmountLabel.Text = "Amount:";
            // 
            // getRatesButton
            // 
            this.getRatesButton.Location = new System.Drawing.Point(343, 163);
            this.getRatesButton.Name = "getRatesButton";
            this.getRatesButton.Size = new System.Drawing.Size(104, 32);
            this.getRatesButton.TabIndex = 5;
            this.getRatesButton.Text = "Get Rates";
            this.getRatesButton.UseVisualStyleBackColor = true;
            this.getRatesButton.Click += new System.EventHandler(this.getRatesButton_Click);
            // 
            // ratesDataGridView
            // 
            this.ratesDataGridView.AllowUserToAddRows = false;
            this.ratesDataGridView.AllowUserToDeleteRows = false;
            this.ratesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ratesDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ratesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ratesDataGridView.Location = new System.Drawing.Point(8, 54);
            this.ratesDataGridView.Name = "ratesDataGridView";
            this.ratesDataGridView.Size = new System.Drawing.Size(439, 103);
            this.ratesDataGridView.TabIndex = 4;
            // 
            // currentCurrencyDisplayText
            // 
            this.currentCurrencyDisplayText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentCurrencyDisplayText.Location = new System.Drawing.Point(103, 30);
            this.currentCurrencyDisplayText.Name = "currentCurrencyDisplayText";
            this.currentCurrencyDisplayText.Size = new System.Drawing.Size(217, 21);
            this.currentCurrencyDisplayText.TabIndex = 3;
            this.currentCurrencyDisplayText.Text = "EUR";
            this.currentCurrencyDisplayText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentCurrencyDisplayLabel
            // 
            this.currentCurrencyDisplayLabel.Location = new System.Drawing.Point(8, 30);
            this.currentCurrencyDisplayLabel.Name = "currentCurrencyDisplayLabel";
            this.currentCurrencyDisplayLabel.Size = new System.Drawing.Size(89, 21);
            this.currentCurrencyDisplayLabel.TabIndex = 2;
            this.currentCurrencyDisplayLabel.Text = "Current:";
            this.currentCurrencyDisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // currentCurrencySelectLabel
            // 
            this.currentCurrencySelectLabel.Location = new System.Drawing.Point(8, 6);
            this.currentCurrencySelectLabel.Name = "currentCurrencySelectLabel";
            this.currentCurrencySelectLabel.Size = new System.Drawing.Size(89, 21);
            this.currentCurrencySelectLabel.TabIndex = 1;
            this.currentCurrencySelectLabel.Text = "Base Currency";
            this.currentCurrencySelectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // currentCurrencySelectDropdown
            // 
            this.currentCurrencySelectDropdown.FormattingEnabled = true;
            this.currentCurrencySelectDropdown.Location = new System.Drawing.Point(103, 6);
            this.currentCurrencySelectDropdown.Name = "currentCurrencySelectDropdown";
            this.currentCurrencySelectDropdown.Size = new System.Drawing.Size(217, 21);
            this.currentCurrencySelectDropdown.TabIndex = 0;
            this.currentCurrencySelectDropdown.SelectionChangeCommitted += new System.EventHandler(this.baseCurrency_SelectionChangeCommitted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 473);
            this.Controls.Add(this.controlsPanel);
            this.Controls.Add(this.apiKeyValidationInfo);
            this.Controls.Add(this.startApplicationButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(505, 512);
            this.MinimumSize = new System.Drawing.Size(505, 512);
            this.Name = "Form1";
            this.Text = "Currency Convert";
            this.controlsPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.convertFromAmountInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startApplicationButton;
        private System.Windows.Forms.Label apiKeyValidationInfo;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox currentCurrencySelectDropdown;
        private System.Windows.Forms.Label currentCurrencySelectLabel;
        private System.Windows.Forms.Label currentCurrencyDisplayLabel;
        private System.Windows.Forms.Label currentCurrencyDisplayText;
        private System.Windows.Forms.DataGridView ratesDataGridView;
        private System.Windows.Forms.Button getRatesButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label currencyConvertContainerLabel;
        private System.Windows.Forms.Label convertFromDropdownLabel;
        private System.Windows.Forms.ComboBox convertFromDropdownInput;
        private System.Windows.Forms.Label convertFromAmountLabel;
        private System.Windows.Forms.Label convertToDropdownLabel;
        private System.Windows.Forms.ComboBox convertToDropdownInput;
        private System.Windows.Forms.Label convertResultLabel;
        private System.Windows.Forms.TextBox convertResultTextbox;
        private System.Windows.Forms.NumericUpDown convertFromAmountInput;
        private System.Windows.Forms.Button convertToButton;
        private System.Windows.Forms.Button swichCurrenciesConvertButton;
    }
}

