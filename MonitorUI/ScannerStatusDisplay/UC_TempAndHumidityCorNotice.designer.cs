namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class UC_TempAndHumidityCorNotice
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Unknow = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_ = new System.Windows.Forms.Label();
            this.label_MinValue = new System.Windows.Forms.Label();
            this.label_Threshold = new System.Windows.Forms.Label();
            this.label_MaxValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Unknow
            // 
            this.label_Unknow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Unknow.AutoSize = true;
            this.label_Unknow.BackColor = System.Drawing.Color.Transparent;
            this.label_Unknow.Location = new System.Drawing.Point(26, 128);
            this.label_Unknow.Name = "label_Unknow";
            this.label_Unknow.Size = new System.Drawing.Size(29, 12);
            this.label_Unknow.TabIndex = 19;
            this.label_Unknow.Text = "未知";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackgroundImage = global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.ColorNotice;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 115);
            this.panel3.TabIndex = 23;
            // 
            // label_
            // 
            this.label_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_.BackColor = System.Drawing.Color.Gray;
            this.label_.Location = new System.Drawing.Point(3, 121);
            this.label_.Name = "label_";
            this.label_.Size = new System.Drawing.Size(20, 22);
            this.label_.TabIndex = 18;
            // 
            // label_MinValue
            // 
            this.label_MinValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_MinValue.AutoSize = true;
            this.label_MinValue.BackColor = System.Drawing.Color.Transparent;
            this.label_MinValue.Location = new System.Drawing.Point(27, 108);
            this.label_MinValue.Name = "label_MinValue";
            this.label_MinValue.Size = new System.Drawing.Size(29, 12);
            this.label_MinValue.TabIndex = 22;
            this.label_MinValue.Text = "20℃";
            // 
            // label_Threshold
            // 
            this.label_Threshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Threshold.AutoSize = true;
            this.label_Threshold.BackColor = System.Drawing.Color.Transparent;
            this.label_Threshold.Location = new System.Drawing.Point(26, 28);
            this.label_Threshold.Name = "label_Threshold";
            this.label_Threshold.Size = new System.Drawing.Size(29, 12);
            this.label_Threshold.TabIndex = 21;
            this.label_Threshold.Text = "40℃";
            // 
            // label_MaxValue
            // 
            this.label_MaxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_MaxValue.AutoSize = true;
            this.label_MaxValue.BackColor = System.Drawing.Color.Transparent;
            this.label_MaxValue.Location = new System.Drawing.Point(26, 5);
            this.label_MaxValue.Name = "label_MaxValue";
            this.label_MaxValue.Size = new System.Drawing.Size(29, 12);
            this.label_MaxValue.TabIndex = 20;
            this.label_MaxValue.Text = "80℃";
            // 
            // UC_TempAndHumidityCorNotice
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.label_MaxValue);
            this.Controls.Add(this.label_Unknow);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label_);
            this.Controls.Add(this.label_MinValue);
            this.Controls.Add(this.label_Threshold);
            this.DoubleBuffered = true;
            this.Name = "UC_TempAndHumidityCorNotice";
            this.Size = new System.Drawing.Size(83, 144);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Unknow;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label_;
        private System.Windows.Forms.Label label_MinValue;
        private System.Windows.Forms.Label label_Threshold;
        private System.Windows.Forms.Label label_MaxValue;
    }
}
