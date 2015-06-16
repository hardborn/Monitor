namespace Nova.Monitoring.UI.SenderStatusDisplay
{
    partial class UC_SenderStatus
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
            this.doubleBufferTableLayoutPanel1 = new Nova.Control.DoubleBufferControl.DoubleBufferTableLayoutPanel();
            this.doubleBufferPanel1 = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.label_SecderError = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_Unknown = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_DVIException = new System.Windows.Forms.Label();
            this.label_OK = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.doubleBufferPanel_SenderStatus = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.doubleBufferTableLayoutPanel1.SuspendLayout();
            this.doubleBufferPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // doubleBufferTableLayoutPanel1
            // 
            this.doubleBufferTableLayoutPanel1.ColumnCount = 2;
            this.doubleBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.doubleBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.doubleBufferTableLayoutPanel1.Controls.Add(this.doubleBufferPanel1, 1, 0);
            this.doubleBufferTableLayoutPanel1.Controls.Add(this.doubleBufferPanel_SenderStatus, 0, 0);
            this.doubleBufferTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.doubleBufferTableLayoutPanel1.Name = "doubleBufferTableLayoutPanel1";
            this.doubleBufferTableLayoutPanel1.RowCount = 1;
            this.doubleBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.doubleBufferTableLayoutPanel1.Size = new System.Drawing.Size(335, 264);
            this.doubleBufferTableLayoutPanel1.TabIndex = 0;
            // 
            // doubleBufferPanel1
            // 
            this.doubleBufferPanel1.Controls.Add(this.label_SecderError);
            this.doubleBufferPanel1.Controls.Add(this.label5);
            this.doubleBufferPanel1.Controls.Add(this.label_Unknown);
            this.doubleBufferPanel1.Controls.Add(this.label4);
            this.doubleBufferPanel1.Controls.Add(this.label_DVIException);
            this.doubleBufferPanel1.Controls.Add(this.label_OK);
            this.doubleBufferPanel1.Controls.Add(this.label2);
            this.doubleBufferPanel1.Controls.Add(this.label1);
            this.doubleBufferPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferPanel1.Location = new System.Drawing.Point(228, 3);
            this.doubleBufferPanel1.Name = "doubleBufferPanel1";
            this.doubleBufferPanel1.Size = new System.Drawing.Size(104, 258);
            this.doubleBufferPanel1.TabIndex = 0;
            // 
            // label_SecderError
            // 
            this.label_SecderError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_SecderError.AutoEllipsis = true;
            this.label_SecderError.Location = new System.Drawing.Point(31, 152);
            this.label_SecderError.Name = "label_SecderError";
            this.label_SecderError.Size = new System.Drawing.Size(73, 29);
            this.label_SecderError.TabIndex = 7;
            this.label_SecderError.Text = "发送卡故障";
            this.label_SecderError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.BackColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(3, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 26);
            this.label5.TabIndex = 6;
            // 
            // label_Unknown
            // 
            this.label_Unknown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Unknown.AutoEllipsis = true;
            this.label_Unknown.Location = new System.Drawing.Point(31, 224);
            this.label_Unknown.Name = "label_Unknown";
            this.label_Unknown.Size = new System.Drawing.Size(73, 29);
            this.label_Unknown.TabIndex = 5;
            this.label_Unknown.Text = "未知";
            this.label_Unknown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.BackColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(3, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 26);
            this.label4.TabIndex = 4;
            // 
            // label_DVIException
            // 
            this.label_DVIException.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_DVIException.AutoEllipsis = true;
            this.label_DVIException.Location = new System.Drawing.Point(31, 188);
            this.label_DVIException.Name = "label_DVIException";
            this.label_DVIException.Size = new System.Drawing.Size(73, 29);
            this.label_DVIException.TabIndex = 3;
            this.label_DVIException.Text = "DVI信号异常";
            this.label_DVIException.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_OK
            // 
            this.label_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_OK.AutoEllipsis = true;
            this.label_OK.Location = new System.Drawing.Point(31, 116);
            this.label_OK.Name = "label_OK";
            this.label_OK.Size = new System.Drawing.Size(73, 29);
            this.label_OK.TabIndex = 2;
            this.label_OK.Text = "正常";
            this.label_OK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(3, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 26);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(3, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 26);
            this.label1.TabIndex = 0;
            // 
            // doubleBufferPanel_SenderStatus
            // 
            this.doubleBufferPanel_SenderStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferPanel_SenderStatus.Location = new System.Drawing.Point(3, 3);
            this.doubleBufferPanel_SenderStatus.Name = "doubleBufferPanel_SenderStatus";
            this.doubleBufferPanel_SenderStatus.Size = new System.Drawing.Size(219, 258);
            this.doubleBufferPanel_SenderStatus.TabIndex = 1;
            // 
            // UC_SenderStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.doubleBufferTableLayoutPanel1);
            this.Name = "UC_SenderStatus";
            this.Size = new System.Drawing.Size(335, 264);
            this.Load += new System.EventHandler(this.UC_SenderStatus_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_SenderStatus_VisibleChanged);
            this.doubleBufferTableLayoutPanel1.ResumeLayout(false);
            this.doubleBufferPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.DoubleBufferControl.DoubleBufferTableLayoutPanel doubleBufferTableLayoutPanel1;
        private Nova.Control.DoubleBufferControl.DoubleBufferPanel doubleBufferPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_DVIException;
        private System.Windows.Forms.Label label_OK;
        private Nova.Control.DoubleBufferControl.DoubleBufferPanel doubleBufferPanel_SenderStatus;
        private System.Windows.Forms.Label label_Unknown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_SecderError;
        private System.Windows.Forms.Label label5;
    }
}
