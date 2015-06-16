namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class UC_RowLineCorNotice
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
            this.label = new System.Windows.Forms.Label();
            this.label_OK = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_Error = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Unknown
            // 
            this.label_Unknown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Unknown.AutoSize = true;
            this.label_Unknown.Location = new System.Drawing.Point(26, 199);
            this.label_Unknown.Name = "label_Unknown";
            this.label_Unknown.Size = new System.Drawing.Size(29, 12);
            this.label_Unknown.TabIndex = 19;
            this.label_Unknown.Text = "未知";
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label.BackColor = System.Drawing.Color.Gray;
            this.label.Location = new System.Drawing.Point(-1, 192);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(25, 25);
            this.label.TabIndex = 18;
            // 
            // label_OK
            // 
            this.label_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_OK.AutoSize = true;
            this.label_OK.Location = new System.Drawing.Point(26, 109);
            this.label_OK.Name = "label_OK";
            this.label_OK.Size = new System.Drawing.Size(29, 12);
            this.label_OK.TabIndex = 17;
            this.label_OK.Text = "正常";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.BackColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(-1, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 25);
            this.label3.TabIndex = 16;
            // 
            // label_Error
            // 
            this.label_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Error.AutoSize = true;
            this.label_Error.Location = new System.Drawing.Point(26, 153);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(29, 12);
            this.label_Error.TabIndex = 15;
            this.label_Error.Text = "故障";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(-1, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 25);
            this.label1.TabIndex = 14;
            // 
            // UC_RowLineCorNotice
            // 
            this.Controls.Add(this.label_Unknown);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label_OK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_Error);
            this.Controls.Add(this.label1);
            this.Name = "UC_RowLineCorNotice";
            this.Size = new System.Drawing.Size(80, 223);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Unknown;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label_OK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_Error;
        private System.Windows.Forms.Label label1;
    }
}
