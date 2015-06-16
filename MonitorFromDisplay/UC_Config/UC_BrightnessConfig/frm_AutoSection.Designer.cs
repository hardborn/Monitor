namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    partial class frm_AutoSection
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.sectionConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.sectionCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sectionRemarkLabel = new System.Windows.Forms.Label();
            this.sectionHScrollBar = new System.Windows.Forms.HScrollBar();
            this.ledLabel = new System.Windows.Forms.Label();
            this.sectionNumberLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.environmentLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.minLedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.maxLedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.minEnvironmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.minLabel = new System.Windows.Forms.Label();
            this.maxLabel = new System.Windows.Forms.Label();
            this.maxEnvironmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.confirmButton = new Nova.Control.CrystalButton();
            this.cancelButton = new Nova.Control.CrystalButton();
            this.panel1.SuspendLayout();
            this.sectionConditionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minLedNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxLedNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minEnvironmentNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxEnvironmentNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.sectionConditionGroupBox);
            this.panel1.Location = new System.Drawing.Point(6, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 185);
            this.panel1.TabIndex = 0;
            // 
            // sectionConditionGroupBox
            // 
            this.sectionConditionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sectionConditionGroupBox.Controls.Add(this.sectionCountLabel);
            this.sectionConditionGroupBox.Controls.Add(this.label2);
            this.sectionConditionGroupBox.Controls.Add(this.label1);
            this.sectionConditionGroupBox.Controls.Add(this.sectionRemarkLabel);
            this.sectionConditionGroupBox.Controls.Add(this.sectionHScrollBar);
            this.sectionConditionGroupBox.Controls.Add(this.ledLabel);
            this.sectionConditionGroupBox.Controls.Add(this.sectionNumberLabel);
            this.sectionConditionGroupBox.Controls.Add(this.label9);
            this.sectionConditionGroupBox.Controls.Add(this.environmentLabel);
            this.sectionConditionGroupBox.Controls.Add(this.label8);
            this.sectionConditionGroupBox.Controls.Add(this.minLedNumericUpDown);
            this.sectionConditionGroupBox.Controls.Add(this.label7);
            this.sectionConditionGroupBox.Controls.Add(this.label6);
            this.sectionConditionGroupBox.Controls.Add(this.maxLedNumericUpDown);
            this.sectionConditionGroupBox.Controls.Add(this.minEnvironmentNumericUpDown);
            this.sectionConditionGroupBox.Controls.Add(this.minLabel);
            this.sectionConditionGroupBox.Controls.Add(this.maxLabel);
            this.sectionConditionGroupBox.Controls.Add(this.maxEnvironmentNumericUpDown);
            this.sectionConditionGroupBox.Location = new System.Drawing.Point(4, 4);
            this.sectionConditionGroupBox.Name = "sectionConditionGroupBox";
            this.sectionConditionGroupBox.Size = new System.Drawing.Size(397, 175);
            this.sectionConditionGroupBox.TabIndex = 2;
            this.sectionConditionGroupBox.TabStop = false;
            // 
            // sectionCountLabel
            // 
            this.sectionCountLabel.AutoSize = true;
            this.sectionCountLabel.Location = new System.Drawing.Point(368, 107);
            this.sectionCountLabel.Name = "sectionCountLabel";
            this.sectionCountLabel.Size = new System.Drawing.Size(13, 13);
            this.sectionCountLabel.TabIndex = 18;
            this.sectionCountLabel.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "-->";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "-->";
            // 
            // sectionRemarkLabel
            // 
            this.sectionRemarkLabel.AutoEllipsis = true;
            this.sectionRemarkLabel.Location = new System.Drawing.Point(34, 77);
            this.sectionRemarkLabel.Name = "sectionRemarkLabel";
            this.sectionRemarkLabel.Size = new System.Drawing.Size(345, 22);
            this.sectionRemarkLabel.TabIndex = 15;
            this.sectionRemarkLabel.Text = "之间的值按最大值和最小值分段线性调节";
            // 
            // sectionHScrollBar
            // 
            this.sectionHScrollBar.LargeChange = 1;
            this.sectionHScrollBar.Location = new System.Drawing.Point(117, 105);
            this.sectionHScrollBar.Maximum = 20;
            this.sectionHScrollBar.Minimum = 1;
            this.sectionHScrollBar.Name = "sectionHScrollBar";
            this.sectionHScrollBar.Size = new System.Drawing.Size(247, 17);
            this.sectionHScrollBar.TabIndex = 14;
            this.sectionHScrollBar.Value = 1;
            this.sectionHScrollBar.ValueChanged += new System.EventHandler(this.sectionHScrollBar_ValueChanged);
            // 
            // ledLabel
            // 
            this.ledLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ledLabel.AutoEllipsis = true;
            this.ledLabel.Location = new System.Drawing.Point(260, 17);
            this.ledLabel.Name = "ledLabel";
            this.ledLabel.Size = new System.Drawing.Size(131, 23);
            this.ledLabel.TabIndex = 10;
            this.ledLabel.Text = "屏体亮度";
            this.ledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sectionNumberLabel
            // 
            this.sectionNumberLabel.AutoEllipsis = true;
            this.sectionNumberLabel.Location = new System.Drawing.Point(56, 102);
            this.sectionNumberLabel.Name = "sectionNumberLabel";
            this.sectionNumberLabel.Size = new System.Drawing.Size(58, 23);
            this.sectionNumberLabel.TabIndex = 0;
            this.sectionNumberLabel.Text = "分段数:";
            this.sectionNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(378, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "%";
            // 
            // environmentLabel
            // 
            this.environmentLabel.AutoEllipsis = true;
            this.environmentLabel.Location = new System.Drawing.Point(32, 17);
            this.environmentLabel.Name = "environmentLabel";
            this.environmentLabel.Size = new System.Drawing.Size(137, 23);
            this.environmentLabel.TabIndex = 9;
            this.environmentLabel.Text = "环境亮度";
            this.environmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(378, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "%";
            // 
            // minLedNumericUpDown
            // 
            this.minLedNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.minLedNumericUpDown.Location = new System.Drawing.Point(299, 139);
            this.minLedNumericUpDown.Name = "minLedNumericUpDown";
            this.minLedNumericUpDown.Size = new System.Drawing.Size(76, 20);
            this.minLedNumericUpDown.TabIndex = 9;
            this.minLedNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(155, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "Lux(0-65534)";
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(155, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Lux(0-65534)";
            // 
            // maxLedNumericUpDown
            // 
            this.maxLedNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.maxLedNumericUpDown.Location = new System.Drawing.Point(299, 49);
            this.maxLedNumericUpDown.Name = "maxLedNumericUpDown";
            this.maxLedNumericUpDown.Size = new System.Drawing.Size(76, 20);
            this.maxLedNumericUpDown.TabIndex = 8;
            this.maxLedNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxLedNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // minEnvironmentNumericUpDown
            // 
            this.minEnvironmentNumericUpDown.Location = new System.Drawing.Point(71, 136);
            this.minEnvironmentNumericUpDown.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.minEnvironmentNumericUpDown.Name = "minEnvironmentNumericUpDown";
            this.minEnvironmentNumericUpDown.Size = new System.Drawing.Size(78, 20);
            this.minEnvironmentNumericUpDown.TabIndex = 8;
            this.minEnvironmentNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // minLabel
            // 
            this.minLabel.AutoEllipsis = true;
            this.minLabel.Location = new System.Drawing.Point(30, 138);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(35, 23);
            this.minLabel.TabIndex = 3;
            this.minLabel.Text = "小于";
            this.minLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // maxLabel
            // 
            this.maxLabel.AutoEllipsis = true;
            this.maxLabel.Location = new System.Drawing.Point(30, 48);
            this.maxLabel.Name = "maxLabel";
            this.maxLabel.Size = new System.Drawing.Size(35, 23);
            this.maxLabel.TabIndex = 0;
            this.maxLabel.Text = "大于";
            this.maxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // maxEnvironmentNumericUpDown
            // 
            this.maxEnvironmentNumericUpDown.Location = new System.Drawing.Point(71, 49);
            this.maxEnvironmentNumericUpDown.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.maxEnvironmentNumericUpDown.Name = "maxEnvironmentNumericUpDown";
            this.maxEnvironmentNumericUpDown.Size = new System.Drawing.Size(78, 20);
            this.maxEnvironmentNumericUpDown.TabIndex = 7;
            this.maxEnvironmentNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxEnvironmentNumericUpDown.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.AutoEllipsis = true;
            this.confirmButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.confirmButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.confirmButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.confirmButton.ButtonClickColor = System.Drawing.Color.Green;
            this.confirmButton.ButtonCornorRadius = 3;
            this.confirmButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.confirmButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.confirmButton.ForeColor = System.Drawing.Color.Black;
            this.confirmButton.IsButtonFoucs = false;
            this.confirmButton.Location = new System.Drawing.Point(209, 200);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(90, 30);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "确定";
            this.confirmButton.Transparency = 50;
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoEllipsis = true;
            this.cancelButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.cancelButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.cancelButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.cancelButton.ButtonClickColor = System.Drawing.Color.Green;
            this.cancelButton.ButtonCornorRadius = 3;
            this.cancelButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.cancelButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.IsButtonFoucs = false;
            this.cancelButton.Location = new System.Drawing.Point(309, 200);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            this.cancelButton.Transparency = 50;
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // frm_AutoSection
            // 
            this.AcceptButton = this.confirmButton;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(419, 243);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_AutoSection";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "快速分段配置";
            this.panel1.ResumeLayout(false);
            this.sectionConditionGroupBox.ResumeLayout(false);
            this.sectionConditionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minLedNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxLedNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minEnvironmentNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxEnvironmentNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox sectionConditionGroupBox;
        private System.Windows.Forms.Label environmentLabel;
        private System.Windows.Forms.NumericUpDown minEnvironmentNumericUpDown;
        private System.Windows.Forms.NumericUpDown maxEnvironmentNumericUpDown;
        private System.Windows.Forms.Label minLabel;
        private System.Windows.Forms.Label maxLabel;
        private System.Windows.Forms.Label sectionNumberLabel;
        private Nova.Control.CrystalButton confirmButton;
        private Nova.Control.CrystalButton cancelButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label ledLabel;
        private System.Windows.Forms.NumericUpDown minLedNumericUpDown;
        private System.Windows.Forms.NumericUpDown maxLedNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label sectionRemarkLabel;
        private System.Windows.Forms.HScrollBar sectionHScrollBar;
        private System.Windows.Forms.Label sectionCountLabel;
    }
}