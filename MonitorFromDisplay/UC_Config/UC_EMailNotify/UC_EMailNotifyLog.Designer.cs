namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_EMailNotifyLog
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.crystalButton_reduceDate = new Nova.Control.CrystalButton();
            this.crystalButton_addDate = new Nova.Control.CrystalButton();
            this.label_logTime = new System.Windows.Forms.Label();
            this.dTP_logTime = new System.Windows.Forms.DateTimePicker();
            this.crystalButton_refresh = new Nova.Control.CrystalButton();
            this.crystalButton_deleLog = new Nova.Control.CrystalButton();
            this.dataGridView_logShow = new System.Windows.Forms.DataGridView();
            this.notifyContentBS = new System.Windows.Forms.BindingSource(this.components);
            this.label_NotifyTip = new System.Windows.Forms.Label();
            this.Column_NotifyTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_ReceiveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_MsgContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_NotifyState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_ConfigBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_logShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notifyContentBS)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_ConfigBase.Controls.Add(this.label_NotifyTip);
            this.panel_ConfigBase.Controls.Add(this.dataGridView_logShow);
            this.panel_ConfigBase.Controls.Add(this.crystalButton_refresh);
            this.panel_ConfigBase.Controls.Add(this.crystalButton_deleLog);
            this.panel_ConfigBase.Controls.Add(this.crystalButton_reduceDate);
            this.panel_ConfigBase.Controls.Add(this.crystalButton_addDate);
            this.panel_ConfigBase.Controls.Add(this.label_logTime);
            this.panel_ConfigBase.Controls.Add(this.dTP_logTime);
            this.panel_ConfigBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ConfigBase.Location = new System.Drawing.Point(0, 0);
            this.panel_ConfigBase.Size = new System.Drawing.Size(500, 450);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Visible = false;
            // 
            // crystalButton_reduceDate
            // 
            this.crystalButton_reduceDate.AutoEllipsis = true;
            this.crystalButton_reduceDate.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_reduceDate.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_reduceDate.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_reduceDate.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_reduceDate.ButtonCornorRadius = 3;
            this.crystalButton_reduceDate.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_reduceDate.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_reduceDate.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_reduceDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_reduceDate.IsButtonFoucs = false;
            this.crystalButton_reduceDate.Location = new System.Drawing.Point(101, 12);
            this.crystalButton_reduceDate.Name = "crystalButton_reduceDate";
            this.crystalButton_reduceDate.Size = new System.Drawing.Size(24, 24);
            this.crystalButton_reduceDate.TabIndex = 3;
            this.crystalButton_reduceDate.Text = "-";
            this.crystalButton_reduceDate.Transparency = 50;
            this.crystalButton_reduceDate.UseVisualStyleBackColor = false;
            this.crystalButton_reduceDate.Click += new System.EventHandler(this.crystalButton_reduceDate_Click);
            // 
            // crystalButton_addDate
            // 
            this.crystalButton_addDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_addDate.AutoEllipsis = true;
            this.crystalButton_addDate.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_addDate.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_addDate.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_addDate.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_addDate.ButtonCornorRadius = 3;
            this.crystalButton_addDate.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_addDate.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_addDate.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_addDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_addDate.IsButtonFoucs = false;
            this.crystalButton_addDate.Location = new System.Drawing.Point(283, 12);
            this.crystalButton_addDate.Name = "crystalButton_addDate";
            this.crystalButton_addDate.Size = new System.Drawing.Size(24, 24);
            this.crystalButton_addDate.TabIndex = 6;
            this.crystalButton_addDate.Text = "+";
            this.crystalButton_addDate.Transparency = 50;
            this.crystalButton_addDate.UseVisualStyleBackColor = false;
            this.crystalButton_addDate.Click += new System.EventHandler(this.crystalButton_addDate_Click);
            // 
            // label_logTime
            // 
            this.label_logTime.AutoEllipsis = true;
            this.label_logTime.Location = new System.Drawing.Point(4, 15);
            this.label_logTime.Name = "label_logTime";
            this.label_logTime.Size = new System.Drawing.Size(91, 20);
            this.label_logTime.TabIndex = 4;
            this.label_logTime.Text = "日志时间：";
            // 
            // dTP_logTime
            // 
            this.dTP_logTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dTP_logTime.Location = new System.Drawing.Point(143, 12);
            this.dTP_logTime.Name = "dTP_logTime";
            this.dTP_logTime.Size = new System.Drawing.Size(134, 21);
            this.dTP_logTime.TabIndex = 5;
            // 
            // crystalButton_refresh
            // 
            this.crystalButton_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_refresh.AutoEllipsis = true;
            this.crystalButton_refresh.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_refresh.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_refresh.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_refresh.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_refresh.ButtonCornorRadius = 3;
            this.crystalButton_refresh.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_refresh.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_refresh.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_refresh.IsButtonFoucs = false;
            this.crystalButton_refresh.Location = new System.Drawing.Point(319, 9);
            this.crystalButton_refresh.Name = "crystalButton_refresh";
            this.crystalButton_refresh.Size = new System.Drawing.Size(81, 30);
            this.crystalButton_refresh.TabIndex = 7;
            this.crystalButton_refresh.Text = "刷新";
            this.crystalButton_refresh.Transparency = 50;
            this.crystalButton_refresh.UseVisualStyleBackColor = false;
            this.crystalButton_refresh.Click += new System.EventHandler(this.crystalButton_refresh_Click);
            // 
            // crystalButton_deleLog
            // 
            this.crystalButton_deleLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_deleLog.AutoEllipsis = true;
            this.crystalButton_deleLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_deleLog.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_deleLog.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_deleLog.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_deleLog.ButtonCornorRadius = 3;
            this.crystalButton_deleLog.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_deleLog.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_deleLog.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_deleLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_deleLog.IsButtonFoucs = false;
            this.crystalButton_deleLog.Location = new System.Drawing.Point(406, 9);
            this.crystalButton_deleLog.Name = "crystalButton_deleLog";
            this.crystalButton_deleLog.Size = new System.Drawing.Size(81, 30);
            this.crystalButton_deleLog.TabIndex = 8;
            this.crystalButton_deleLog.Text = "删除日志";
            this.crystalButton_deleLog.Transparency = 50;
            this.crystalButton_deleLog.UseVisualStyleBackColor = false;
            this.crystalButton_deleLog.Click += new System.EventHandler(this.crystalButton_deleLog_Click);
            // 
            // dataGridView_logShow
            // 
            this.dataGridView_logShow.AllowUserToAddRows = false;
            this.dataGridView_logShow.AllowUserToDeleteRows = false;
            this.dataGridView_logShow.AllowUserToOrderColumns = true;
            this.dataGridView_logShow.AllowUserToResizeColumns = false;
            this.dataGridView_logShow.AllowUserToResizeRows = false;
            this.dataGridView_logShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_logShow.AutoGenerateColumns = false;
            this.dataGridView_logShow.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_logShow.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_logShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_logShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_NotifyTime,
            this.Column_ReceiveName,
            this.Column_Title,
            this.Column_MsgContent,
            this.Column_NotifyState});
            this.dataGridView_logShow.DataSource = this.notifyContentBS;
            this.dataGridView_logShow.Location = new System.Drawing.Point(6, 45);
            this.dataGridView_logShow.Name = "dataGridView_logShow";
            this.dataGridView_logShow.ReadOnly = true;
            this.dataGridView_logShow.RowHeadersVisible = false;
            this.dataGridView_logShow.RowTemplate.Height = 23;
            this.dataGridView_logShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_logShow.Size = new System.Drawing.Size(491, 368);
            this.dataGridView_logShow.TabIndex = 9;
            this.dataGridView_logShow.TabStop = false;
            this.dataGridView_logShow.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_logShow_CellFormatting);
            // 
            // notifyContentBS
            // 
            this.notifyContentBS.DataMember = "NotifyContentList";
            this.notifyContentBS.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_EMailNotifyLog_VM);
            // 
            // label_NotifyTip
            // 
            this.label_NotifyTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_NotifyTip.AutoEllipsis = true;
            this.label_NotifyTip.Location = new System.Drawing.Point(4, 416);
            this.label_NotifyTip.Name = "label_NotifyTip";
            this.label_NotifyTip.Size = new System.Drawing.Size(493, 23);
            this.label_NotifyTip.TabIndex = 10;
            this.label_NotifyTip.Text = "提示：启用本地邮件通知后，可查看日志。";
            // 
            // Column_NotifyTime
            // 
            this.Column_NotifyTime.DataPropertyName = "SendEMailTime";
            this.Column_NotifyTime.HeaderText = "通知日期";
            this.Column_NotifyTime.Name = "Column_NotifyTime";
            this.Column_NotifyTime.ReadOnly = true;
            // 
            // Column_ReceiveName
            // 
            this.Column_ReceiveName.DataPropertyName = "Receiver";
            this.Column_ReceiveName.HeaderText = "收信人";
            this.Column_ReceiveName.Name = "Column_ReceiveName";
            this.Column_ReceiveName.ReadOnly = true;
            // 
            // Column_Title
            // 
            this.Column_Title.DataPropertyName = "MsgTitle";
            this.Column_Title.FillWeight = 150F;
            this.Column_Title.HeaderText = "标题";
            this.Column_Title.Name = "Column_Title";
            this.Column_Title.ReadOnly = true;
            // 
            // Column_MsgContent
            // 
            this.Column_MsgContent.DataPropertyName = "MsgContent";
            this.Column_MsgContent.HeaderText = "通知内容";
            this.Column_MsgContent.Name = "Column_MsgContent";
            this.Column_MsgContent.ReadOnly = true;
            // 
            // Column_NotifyState
            // 
            this.Column_NotifyState.DataPropertyName = "NotifyState";
            this.Column_NotifyState.HeaderText = "发送状态";
            this.Column_NotifyState.Name = "Column_NotifyState";
            this.Column_NotifyState.ReadOnly = true;
            // 
            // UC_EMailNotifyLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UC_EMailNotifyLog";
            this.Load += new System.EventHandler(this.UC_EMailNotifyLog_Load);
            this.panel_ConfigBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_logShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notifyContentBS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_logShow;
        private Nova.Control.CrystalButton crystalButton_refresh;
        private Nova.Control.CrystalButton crystalButton_deleLog;
        private Nova.Control.CrystalButton crystalButton_reduceDate;
        private Nova.Control.CrystalButton crystalButton_addDate;
        private System.Windows.Forms.Label label_logTime;
        private System.Windows.Forms.DateTimePicker dTP_logTime;
        private System.Windows.Forms.BindingSource notifyContentBS;
        private System.Windows.Forms.Label label_NotifyTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_NotifyTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ReceiveName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_MsgContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_NotifyState;
    }
}
