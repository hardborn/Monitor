namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    partial class Frm_AddMappingItem
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
            this.environmentLabel = new System.Windows.Forms.Label();
            this.ledLabel = new System.Windows.Forms.Label();
            this.ConfirmButton = new Nova.Control.CrystalButton();
            this.CancelButton = new Nova.Control.CrystalButton();
            this.EnvironmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LedNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // environmentLabel
            // 
            this.environmentLabel.AutoEllipsis = true;
            this.environmentLabel.Location = new System.Drawing.Point(13, 20);
            this.environmentLabel.Name = "environmentLabel";
            this.environmentLabel.Size = new System.Drawing.Size(86, 23);
            this.environmentLabel.TabIndex = 0;
            this.environmentLabel.Text = "环境亮度";
            this.environmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ledLabel
            // 
            this.ledLabel.AutoEllipsis = true;
            this.ledLabel.Location = new System.Drawing.Point(13, 57);
            this.ledLabel.Name = "ledLabel";
            this.ledLabel.Size = new System.Drawing.Size(86, 23);
            this.ledLabel.TabIndex = 2;
            this.ledLabel.Text = "Led亮度";
            this.ledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.AutoEllipsis = true;
            this.ConfirmButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.ConfirmButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.ConfirmButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.ConfirmButton.ButtonClickColor = System.Drawing.Color.Green;
            this.ConfirmButton.ButtonCornorRadius = 3;
            this.ConfirmButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.ConfirmButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.ConfirmButton.ForeColor = System.Drawing.Color.Black;
            this.ConfirmButton.IsButtonFoucs = false;
            this.ConfirmButton.Location = new System.Drawing.Point(102, 94);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(90, 30);
            this.ConfirmButton.TabIndex = 4;
            this.ConfirmButton.Text = "确定";
            this.ConfirmButton.Transparency = 50;
            this.ConfirmButton.UseVisualStyleBackColor = false;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.AutoEllipsis = true;
            this.CancelButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.CancelButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.CancelButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.CancelButton.ButtonClickColor = System.Drawing.Color.Green;
            this.CancelButton.ButtonCornorRadius = 3;
            this.CancelButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.CancelButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.ForeColor = System.Drawing.Color.Black;
            this.CancelButton.IsButtonFoucs = false;
            this.CancelButton.Location = new System.Drawing.Point(198, 93);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(90, 30);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "取消";
            this.CancelButton.Transparency = 50;
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // EnvironmentNumericUpDown
            // 
            this.EnvironmentNumericUpDown.Location = new System.Drawing.Point(105, 21);
            this.EnvironmentNumericUpDown.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.EnvironmentNumericUpDown.Name = "EnvironmentNumericUpDown";
            this.EnvironmentNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.EnvironmentNumericUpDown.TabIndex = 6;
            // 
            // LedNumericUpDown
            // 
            this.LedNumericUpDown.Location = new System.Drawing.Point(106, 58);
            this.LedNumericUpDown.Name = "LedNumericUpDown";
            this.LedNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.LedNumericUpDown.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(198, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Lux(0-65534)";
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(198, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "%";
            // 
            // Frm_AddMappingItem
            // 
            this.AcceptButton = this.ConfirmButton;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(293, 136);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LedNumericUpDown);
            this.Controls.Add(this.EnvironmentNumericUpDown);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.ledLabel);
            this.Controls.Add(this.environmentLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frm_AddMappingItem";
            this.Text = "添加映射项";
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LedNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label environmentLabel;
        private System.Windows.Forms.Label ledLabel;
        private Nova.Control.CrystalButton ConfirmButton;
        private Nova.Control.CrystalButton CancelButton;
        private System.Windows.Forms.NumericUpDown EnvironmentNumericUpDown;
        private System.Windows.Forms.NumericUpDown LedNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
    }
}