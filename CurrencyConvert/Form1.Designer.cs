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
            this.apiKeyValidationInput = new System.Windows.Forms.TextBox();
            this.apiKeyValidationLabel = new System.Windows.Forms.Label();
            this.apiKeyValidationButton = new System.Windows.Forms.Button();
            this.apiKeyValidationInfo = new System.Windows.Forms.Label();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.ratesDataGridView = new System.Windows.Forms.DataGridView();
            this.currentCurrencyDisplayText = new System.Windows.Forms.Label();
            this.currentCurrencyDisplayLabel = new System.Windows.Forms.Label();
            this.currentCurrencySelectLabel = new System.Windows.Forms.Label();
            this.currentCurrencySelectDropdown = new System.Windows.Forms.ComboBox();
            this.getRatesButton = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ratesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // apiKeyValidationInput
            // 
            this.apiKeyValidationInput.Location = new System.Drawing.Point(85, 11);
            this.apiKeyValidationInput.MinimumSize = new System.Drawing.Size(4, 23);
            this.apiKeyValidationInput.Name = "apiKeyValidationInput";
            this.apiKeyValidationInput.Size = new System.Drawing.Size(154, 20);
            this.apiKeyValidationInput.TabIndex = 0;
            // 
            // apiKeyValidationLabel
            // 
            this.apiKeyValidationLabel.Location = new System.Drawing.Point(12, 9);
            this.apiKeyValidationLabel.Name = "apiKeyValidationLabel";
            this.apiKeyValidationLabel.Size = new System.Drawing.Size(67, 23);
            this.apiKeyValidationLabel.TabIndex = 1;
            this.apiKeyValidationLabel.Text = "Api key";
            this.apiKeyValidationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // apiKeyValidationButton
            // 
            this.apiKeyValidationButton.Location = new System.Drawing.Point(245, 11);
            this.apiKeyValidationButton.Name = "apiKeyValidationButton";
            this.apiKeyValidationButton.Size = new System.Drawing.Size(98, 23);
            this.apiKeyValidationButton.TabIndex = 2;
            this.apiKeyValidationButton.Text = "Check Key";
            this.apiKeyValidationButton.UseVisualStyleBackColor = true;
            this.apiKeyValidationButton.Click += new System.EventHandler(this.apiKeyValidationButton_Click);
            // 
            // apiKeyValidationInfo
            // 
            this.apiKeyValidationInfo.Location = new System.Drawing.Point(115, 38);
            this.apiKeyValidationInfo.Name = "apiKeyValidationInfo";
            this.apiKeyValidationInfo.Size = new System.Drawing.Size(157, 17);
            this.apiKeyValidationInfo.TabIndex = 3;
            this.apiKeyValidationInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // controlsPanel
            // 
            this.controlsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlsPanel.Controls.Add(this.getRatesButton);
            this.controlsPanel.Controls.Add(this.ratesDataGridView);
            this.controlsPanel.Controls.Add(this.currentCurrencyDisplayText);
            this.controlsPanel.Controls.Add(this.currentCurrencyDisplayLabel);
            this.controlsPanel.Controls.Add(this.currentCurrencySelectLabel);
            this.controlsPanel.Controls.Add(this.currentCurrencySelectDropdown);
            this.controlsPanel.Location = new System.Drawing.Point(15, 58);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(711, 553);
            this.controlsPanel.TabIndex = 4;
            // 
            // ratesDataGridView
            // 
            this.ratesDataGridView.AllowUserToAddRows = false;
            this.ratesDataGridView.AllowUserToDeleteRows = false;
            this.ratesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ratesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ratesDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ratesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ratesDataGridView.Location = new System.Drawing.Point(386, 4);
            this.ratesDataGridView.Name = "ratesDataGridView";
            this.ratesDataGridView.Size = new System.Drawing.Size(312, 103);
            this.ratesDataGridView.TabIndex = 4;
            // 
            // currentCurrencyDisplayText
            // 
            this.currentCurrencyDisplayText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentCurrencyDisplayText.Location = new System.Drawing.Point(103, 30);
            this.currentCurrencyDisplayText.Name = "currentCurrencyDisplayText";
            this.currentCurrencyDisplayText.Size = new System.Drawing.Size(59, 21);
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
            this.currentCurrencySelectDropdown.Size = new System.Drawing.Size(121, 21);
            this.currentCurrencySelectDropdown.TabIndex = 0;
            this.currentCurrencySelectDropdown.SelectionChangeCommitted += new System.EventHandler(this.baseCurrency_SelectionChangeCommitted);
            // 
            // getRatesButton
            // 
            this.getRatesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getRatesButton.Location = new System.Drawing.Point(593, 114);
            this.getRatesButton.Name = "getRatesButton";
            this.getRatesButton.Size = new System.Drawing.Size(104, 32);
            this.getRatesButton.TabIndex = 5;
            this.getRatesButton.Text = "Get Rates";
            this.getRatesButton.UseVisualStyleBackColor = true;
            this.getRatesButton.Click += new System.EventHandler(this.getRatesButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 623);
            this.Controls.Add(this.controlsPanel);
            this.Controls.Add(this.apiKeyValidationInfo);
            this.Controls.Add(this.apiKeyValidationButton);
            this.Controls.Add(this.apiKeyValidationLabel);
            this.Controls.Add(this.apiKeyValidationInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.controlsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ratesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox apiKeyValidationInput;
        private System.Windows.Forms.Label apiKeyValidationLabel;
        private System.Windows.Forms.Button apiKeyValidationButton;
        private System.Windows.Forms.Label apiKeyValidationInfo;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox currentCurrencySelectDropdown;
        private System.Windows.Forms.Label currentCurrencySelectLabel;
        private System.Windows.Forms.Label currentCurrencyDisplayLabel;
        private System.Windows.Forms.Label currentCurrencyDisplayText;
        private System.Windows.Forms.DataGridView ratesDataGridView;
        private System.Windows.Forms.Button getRatesButton;
    }
}

