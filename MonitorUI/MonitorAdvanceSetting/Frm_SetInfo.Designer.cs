namespace Nova.Monitoring.UI.MonitorSetting
{
    partial class Frm_SetInfo
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.crystalButton_OK = new Nova.Control.CrystalButton();
            this.crystalButton_Cancel = new Nova.Control.CrystalButton();
            this.checkBox_IsConnect = new System.Windows.Forms.CheckBox();
            this.numericUpDown_Count = new System.Windows.Forms.NumericUpDown();
            this.label_Count = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Count)).BeginInit();
            this.SuspendLayout();
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_OK.AutoEllipsis = true;
            this.crystalButton_OK.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_OK.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_OK.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_OK.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_OK.ButtonCornorRadius = 3;
            this.crystalButton_OK.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_OK.ButtonSelectedColor = System.Drawing.Color.DeepSkyBlue;
            this.crystalButton_OK.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_OK.IsButtonFoucs = false;
            this.crystalButton_OK.Location = new System.Drawing.Point(27, 76);
            this.crystalButton_OK.Name = "crystalButton_OK";
            this.crystalButton_OK.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_OK.TabIndex = 2;
            this.crystalButton_OK.Text = "确定";
            this.crystalButton_OK.Transparency = 50;
            this.crystalButton_OK.UseVisualStyleBackColor = false;
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // crystalButton_Cancel
            // 
            this.crystalButton_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_Cancel.AutoEllipsis = true;
            this.crystalButton_Cancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_Cancel.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Cancel.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Cancel.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_Cancel.ButtonCornorRadius = 3;
            this.crystalButton_Cancel.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Cancel.ButtonSelectedColor = System.Drawing.Color.DeepSkyBlue;
            this.crystalButton_Cancel.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_Cancel.IsButtonFoucs = false;
            this.crystalButton_Cancel.Location = new System.Drawing.Point(138, 76);
            this.crystalButton_Cancel.Name = "crystalButton_Cancel";
            this.crystalButton_Cancel.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_Cancel.TabIndex = 3;
            this.crystalButton_Cancel.Text = "取消";
            this.crystalButton_Cancel.Transparency = 50;
            this.crystalButton_Cancel.UseVisualStyleBackColor = false;
            this.crystalButton_Cancel.Click += new System.EventHandler(this.crystalButton_Cancel_Click);
            // 
            // checkBox_IsConnect
            // 
            this.checkBox_IsConnect.AutoEllipsis = true;
            this.checkBox_IsConnect.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_IsConnect.Location = new System.Drawing.Point(39, 21);
            this.checkBox_IsConnect.Name = "checkBox_IsConnect";
            this.checkBox_IsConnect.Size = new System.Drawing.Size(179, 16);
            this.checkBox_IsConnect.TabIndex = 0;
            this.checkBox_IsConnect.Text = "连接";
            this.checkBox_IsConnect.UseVisualStyleBackColor = false;
            this.checkBox_IsConnect.CheckedChanged += new System.EventHandler(this.checkBox_IsConnect_CheckedChanged);
            // 
            // numericUpDown_Count
            // 
            this.numericUpDown_Count.Location = new System.Drawing.Point(93, 42);
            this.numericUpDown_Count.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_Count.Name = "numericUpDown_Count";
            this.numericUpDown_Count.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown_Count.TabIndex = 1;
            this.numericUpDown_Count.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label_Count
            // 
            this.label_Count.AutoEllipsis = true;
            this.label_Count.BackColor = System.Drawing.Color.Transparent;
            this.label_Count.Location = new System.Drawing.Point(37, 41);
            this.label_Count.Name = "label_Count";
            this.label_Count.Size = new System.Drawing.Size(50, 18);
            this.label_Count.TabIndex = 75;
            this.label_Count.Text = "个数:";
            this.label_Count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Frm_SetInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(237, 129);
            this.Controls.Add(this.checkBox_IsConnect);
            this.Controls.Add(this.label_Count);
            this.Controls.Add(this.numericUpDown_Count);
            this.Controls.Add(this.crystalButton_OK);
            this.Controls.Add(this.crystalButton_Cancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SetInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Frm_SetInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Count)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.CrystalButton crystalButton_OK;
        private Nova.Control.CrystalButton crystalButton_Cancel;
        private System.Windows.Forms.CheckBox checkBox_IsConnect;
        private System.Windows.Forms.NumericUpDown numericUpDown_Count;
        private System.Windows.Forms.Label label_Count;
    }
}