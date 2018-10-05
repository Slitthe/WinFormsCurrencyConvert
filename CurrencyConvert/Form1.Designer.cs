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
            this.baseCurrency = new System.Windows.Forms.ComboBox();
            this.controlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // apiKeyValidationInput
            // 
            this.apiKeyValidationInput.Location = new System.Drawing.Point(85, 11);
            this.apiKeyValidationInput.MinimumSize = new System.Drawing.Size(0, 23);
            this.apiKeyValidationInput.Name = "apiKeyValidationInput";
            this.apiKeyValidationInput.Size = new System.Drawing.Size(154, 23);
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
            this.controlsPanel.Controls.Add(this.baseCurrency);
            this.controlsPanel.Location = new System.Drawing.Point(15, 58);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(701, 557);
            this.controlsPanel.TabIndex = 4;
            // 
            // baseCurrency
            // 
            this.baseCurrency.FormattingEnabled = true;
            this.baseCurrency.Location = new System.Drawing.Point(24, 24);
            this.baseCurrency.Name = "baseCurrency";
            this.baseCurrency.Size = new System.Drawing.Size(121, 21);
            this.baseCurrency.TabIndex = 0;
            this.baseCurrency.SelectionChangeCommitted += new System.EventHandler(this.baseCurrency_SelectionChangeCommitted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 627);
            this.Controls.Add(this.controlsPanel);
            this.Controls.Add(this.apiKeyValidationInfo);
            this.Controls.Add(this.apiKeyValidationButton);
            this.Controls.Add(this.apiKeyValidationLabel);
            this.Controls.Add(this.apiKeyValidationInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.controlsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox apiKeyValidationInput;
        private System.Windows.Forms.Label apiKeyValidationLabel;
        private System.Windows.Forms.Button apiKeyValidationButton;
        private System.Windows.Forms.Label apiKeyValidationInfo;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox baseCurrency;
    }
}

