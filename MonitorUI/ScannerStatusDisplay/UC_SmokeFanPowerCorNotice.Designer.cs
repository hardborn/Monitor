namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class UC_SmokeFanPowerCorNotice
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
            this.label_Unknown = new System.Windows.Forms.Label();
            this.label_InvalidPic = new System.Windows.Forms.Label();
            this.label_OK = new System.Windows.Forms.Label();
            this.label_AlarmValue = new System.Windows.Forms.Label();
            this.label_AlarmPic = new System.Windows.Forms.Label();
            this.label_Normal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Unknown
            // 
            this.label_Unknown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Unknown.AutoSize = true;
            this.label_Unknown.Location = new System.Drawing.Point(27, 166);
            this.label_Unknown.Name = "label_Unknown";
            this.label_Unknown.Size = new System.Drawing.Size(29, 12);
            this.label_Unknown.TabIndex = 17;
            this.label_Unknown.Text = "未知";
            // 
            // label_InvalidPic
            // 
            this.label_InvalidPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_InvalidPic.BackColor = System.Drawing.Color.Gray;
            this.label_InvalidPic.Location = new System.Drawing.Point(0, 159);
            this.label_InvalidPic.Name = "label_InvalidPic";
            this.label_InvalidPic.Size = new System.Drawing.Size(25, 25);
            this.label_InvalidPic.TabIndex = 16;
            // 
            // label_OK
            // 
            this.label_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_OK.AutoSize = true;
            this.label_OK.Location = new System.Drawing.Point(27, 76);
            this.label_OK.Name = "label_OK";
            this.label_OK.Size = new System.Drawing.Size(29, 12);
            this.label_OK.TabIndex = 15;
            this.label_OK.Text = "正常";
            // 
            // label_AlarmValue
            // 
            this.label_AlarmValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_AlarmValue.AutoSize = true;
            this.label_AlarmValue.Location = new System.Drawing.Point(27, 122);
            this.label_AlarmValue.Name = "label_AlarmValue";
            this.label_AlarmValue.Size = new System.Drawing.Size(29, 12);
            this.label_AlarmValue.TabIndex = 13;
            this.label_AlarmValue.Text = "告警";
            // 
            // label_AlarmPic
            // 
            this.label_AlarmPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_AlarmPic.BackColor = System.Drawing.Color.Yellow;
            this.label_AlarmPic.Location = new System.Drawing.Point(0, 114);
            this.label_AlarmPic.Name = "label_AlarmPic";
            this.label_AlarmPic.Size = new System.Drawing.Size(25, 25);
            this.label_AlarmPic.TabIndex = 12;
            // 
            // label_Normal
            // 
            this.label_Normal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Normal.BackColor = System.Drawing.Color.Green;
            this.label_Normal.Location = new System.Drawing.Point(0, 69);
            this.label_Normal.Name = "label_Normal";
            this.label_Normal.Size = new System.Drawing.Size(25, 25);
            this.label_Normal.TabIndex = 14;
            // 
            // UC_SmokeFanPowerCorNotice
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.label_Unknown);
            this.Controls.Add(this.label_InvalidPic);
            this.Controls.Add(this.label_OK);
            this.Controls.Add(this.label_Normal);
            this.Controls.Add(this.label_AlarmValue);
            this.Controls.Add(this.label_AlarmPic);
            this.Name = "UC_SmokeFanPowerCorNotice";
            this.Size = new System.Drawing.Size(82, 199);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Unknown;
        private System.Windows.Forms.Label label_InvalidPic;
        private System.Windows.Forms.Label label_OK;
        private System.Windows.Forms.Label label_Normal;
        private System.Windows.Forms.Label label_AlarmValue;
        private System.Windows.Forms.Label label_AlarmPic;

    }
}
