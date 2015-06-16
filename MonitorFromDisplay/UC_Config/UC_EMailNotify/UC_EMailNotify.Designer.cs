using Nova.Monitoring.Common;
namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_EMailNotify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_EMailNotify));
            this.groupBox_senderSetting = new System.Windows.Forms.GroupBox();
            this.checkBox_SSLOpen = new System.Windows.Forms.CheckBox();
            this.eMailNotifyConfigBS = new System.Windows.Forms.BindingSource(this.components);
            this.label_SSl = new System.Windows.Forms.Label();
            this.crystalButton_useDefault = new Nova.Control.CrystalButton();
            this.label_senderPort = new System.Windows.Forms.Label();
            this.label_portInfo = new System.Windows.Forms.Label();
            this.label_smtpServer = new System.Windows.Forms.Label();
            this.label_senderAddr = new System.Windows.Forms.Label();
            this.label_smtpServerInfo = new System.Windows.Forms.Label();
            this.label_userAddr = new System.Windows.Forms.Label();
            this.linkLabel_modifySender = new System.Windows.Forms.LinkLabel();
            this.checkBox_SystemRecover = new System.Windows.Forms.CheckBox();
            this.checkBox_enableNotify = new System.Windows.Forms.CheckBox();
            this.panel_PeriodicReport = new System.Windows.Forms.Panel();
            this.linkLabel_PeriodicReport = new System.Windows.Forms.LinkLabel();
            this.checkBox_SystemRefresh = new System.Windows.Forms.CheckBox();
            this.groupBox_receiver = new System.Windows.Forms.GroupBox();
            this.dataGridView_receiver = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.crystalButton_modifyRecv = new Nova.Control.CrystalButton();
            this.crystalButton_deleteRecv = new Nova.Control.CrystalButton();
            this.crystalButton_addRecv = new Nova.Control.CrystalButton();
            this.groupBox_emailInfo = new System.Windows.Forms.GroupBox();
            this.label_example = new System.Windows.Forms.Label();
            this.textBox_SendSource = new System.Windows.Forms.TextBox();
            this.label_errorNotifyAddr = new System.Windows.Forms.Label();
            this.label_NotifyTip = new System.Windows.Forms.Label();
            this.panel_ConfigBase.SuspendLayout();
            this.groupBox_senderSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eMailNotifyConfigBS)).BeginInit();
            this.panel_PeriodicReport.SuspendLayout();
            this.groupBox_receiver.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_receiver)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox_emailInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.label_NotifyTip);
            this.panel_ConfigBase.Controls.Add(this.groupBox_emailInfo);
            this.panel_ConfigBase.Controls.Add(this.groupBox_receiver);
            this.panel_ConfigBase.Controls.Add(this.panel_PeriodicReport);
            this.panel_ConfigBase.Controls.Add(this.checkBox_SystemRecover);
            this.panel_ConfigBase.Controls.Add(this.checkBox_enableNotify);
            this.panel_ConfigBase.Controls.Add(this.groupBox_senderSetting);
            this.panel_ConfigBase.Size = new System.Drawing.Size(609, 446);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Location = new System.Drawing.Point(511, 459);
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // groupBox_senderSetting
            // 
            this.groupBox_senderSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_senderSetting.Controls.Add(this.checkBox_SSLOpen);
            this.groupBox_senderSetting.Controls.Add(this.label_SSl);
            this.groupBox_senderSetting.Controls.Add(this.crystalButton_useDefault);
            this.groupBox_senderSetting.Controls.Add(this.label_senderPort);
            this.groupBox_senderSetting.Controls.Add(this.label_portInfo);
            this.groupBox_senderSetting.Controls.Add(this.label_smtpServer);
            this.groupBox_senderSetting.Controls.Add(this.label_senderAddr);
            this.groupBox_senderSetting.Controls.Add(this.label_smtpServerInfo);
            this.groupBox_senderSetting.Controls.Add(this.label_userAddr);
            this.groupBox_senderSetting.Controls.Add(this.linkLabel_modifySender);
            this.groupBox_senderSetting.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.eMailNotifyConfigBS, "NotifyConfig.EnableNotify", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.groupBox_senderSetting.Location = new System.Drawing.Point(4, 110);
            this.groupBox_senderSetting.Name = "groupBox_senderSetting";
            this.groupBox_senderSetting.Size = new System.Drawing.Size(598, 100);
            this.groupBox_senderSetting.TabIndex = 4;
            this.groupBox_senderSetting.TabStop = false;
            this.groupBox_senderSetting.Text = "邮件发布者";
            // 
            // checkBox_SSLOpen
            // 
            this.checkBox_SSLOpen.AutoEllipsis = true;
            this.checkBox_SSLOpen.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.eMailNotifyConfigBS, "NotifyConfig.IsEnableSsl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_SSLOpen.Enabled = false;
            this.checkBox_SSLOpen.Location = new System.Drawing.Point(439, 43);
            this.checkBox_SSLOpen.Name = "checkBox_SSLOpen";
            this.checkBox_SSLOpen.Size = new System.Drawing.Size(103, 20);
            this.checkBox_SSLOpen.TabIndex = 14;
            this.checkBox_SSLOpen.Text = "启用";
            this.checkBox_SSLOpen.UseVisualStyleBackColor = true;
            // 
            // eMailNotifyConfigBS
            // 
            this.eMailNotifyConfigBS.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_EMailNotify_VM);
            // 
            // label_SSl
            // 
            this.label_SSl.AutoEllipsis = true;
            this.label_SSl.Location = new System.Drawing.Point(324, 43);
            this.label_SSl.Name = "label_SSl";
            this.label_SSl.Size = new System.Drawing.Size(106, 20);
            this.label_SSl.TabIndex = 13;
            this.label_SSl.Text = "SSL加密：";
            // 
            // crystalButton_useDefault
            // 
            this.crystalButton_useDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_useDefault.AutoEllipsis = true;
            this.crystalButton_useDefault.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_useDefault.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_useDefault.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_useDefault.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_useDefault.ButtonCornorRadius = 5;
            this.crystalButton_useDefault.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_useDefault.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_useDefault.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_useDefault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_useDefault.IsButtonFoucs = false;
            this.crystalButton_useDefault.Location = new System.Drawing.Point(512, 67);
            this.crystalButton_useDefault.Name = "crystalButton_useDefault";
            this.crystalButton_useDefault.Size = new System.Drawing.Size(80, 28);
            this.crystalButton_useDefault.TabIndex = 1;
            this.crystalButton_useDefault.Text = "使用默认";
            this.crystalButton_useDefault.Transparency = 50;
            this.crystalButton_useDefault.UseVisualStyleBackColor = false;
            this.crystalButton_useDefault.Click += new System.EventHandler(this.crystalButton_useDefault_Click);
            // 
            // label_senderPort
            // 
            this.label_senderPort.AutoEllipsis = true;
            this.label_senderPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eMailNotifyConfigBS, "NotifyConfig.Port", true));
            this.label_senderPort.Location = new System.Drawing.Point(439, 17);
            this.label_senderPort.Name = "label_senderPort";
            this.label_senderPort.Size = new System.Drawing.Size(47, 20);
            this.label_senderPort.TabIndex = 12;
            this.label_senderPort.Text = "25";
            // 
            // label_portInfo
            // 
            this.label_portInfo.AutoEllipsis = true;
            this.label_portInfo.Location = new System.Drawing.Point(324, 17);
            this.label_portInfo.Name = "label_portInfo";
            this.label_portInfo.Size = new System.Drawing.Size(106, 20);
            this.label_portInfo.TabIndex = 11;
            this.label_portInfo.Text = "端口：";
            // 
            // label_smtpServer
            // 
            this.label_smtpServer.AutoEllipsis = true;
            this.label_smtpServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eMailNotifyConfigBS, "NotifyConfig.SmtpServer", true));
            this.label_smtpServer.Location = new System.Drawing.Point(115, 43);
            this.label_smtpServer.Name = "label_smtpServer";
            this.label_smtpServer.Size = new System.Drawing.Size(200, 20);
            this.label_smtpServer.TabIndex = 10;
            this.label_smtpServer.Text = "smtp.163.com";
            // 
            // label_senderAddr
            // 
            this.label_senderAddr.AutoEllipsis = true;
            this.label_senderAddr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eMailNotifyConfigBS, "NotifyConfig.EmailAddr", true));
            this.label_senderAddr.Location = new System.Drawing.Point(115, 17);
            this.label_senderAddr.Name = "label_senderAddr";
            this.label_senderAddr.Size = new System.Drawing.Size(200, 20);
            this.label_senderAddr.TabIndex = 8;
            this.label_senderAddr.Text = "zhangsan@163.com";
            // 
            // label_smtpServerInfo
            // 
            this.label_smtpServerInfo.AutoEllipsis = true;
            this.label_smtpServerInfo.Location = new System.Drawing.Point(4, 43);
            this.label_smtpServerInfo.Name = "label_smtpServerInfo";
            this.label_smtpServerInfo.Size = new System.Drawing.Size(105, 20);
            this.label_smtpServerInfo.TabIndex = 5;
            this.label_smtpServerInfo.Text = "SMTP服务器：";
            // 
            // label_userAddr
            // 
            this.label_userAddr.AutoEllipsis = true;
            this.label_userAddr.Location = new System.Drawing.Point(4, 17);
            this.label_userAddr.Name = "label_userAddr";
            this.label_userAddr.Size = new System.Drawing.Size(105, 20);
            this.label_userAddr.TabIndex = 0;
            this.label_userAddr.Text = "邮箱地址：";
            // 
            // linkLabel_modifySender
            // 
            this.linkLabel_modifySender.AutoEllipsis = true;
            this.linkLabel_modifySender.Location = new System.Drawing.Point(4, 72);
            this.linkLabel_modifySender.Name = "linkLabel_modifySender";
            this.linkLabel_modifySender.Size = new System.Drawing.Size(253, 20);
            this.linkLabel_modifySender.TabIndex = 0;
            this.linkLabel_modifySender.TabStop = true;
            this.linkLabel_modifySender.Text = "修改发布者";
            this.linkLabel_modifySender.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_modifySender.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_modifySender_LinkClicked);
            // 
            // checkBox_SystemRecover
            // 
            this.checkBox_SystemRecover.AutoEllipsis = true;
            this.checkBox_SystemRecover.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.eMailNotifyConfigBS, "NotifyConfig.EnableRecoverNotify", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_SystemRecover.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.eMailNotifyConfigBS, "NotifyConfig.EnableNotify", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.checkBox_SystemRecover.Location = new System.Drawing.Point(8, 31);
            this.checkBox_SystemRecover.Name = "checkBox_SystemRecover";
            this.checkBox_SystemRecover.Size = new System.Drawing.Size(244, 20);
            this.checkBox_SystemRecover.TabIndex = 6;
            this.checkBox_SystemRecover.Text = "启用系统恢复通知";
            this.checkBox_SystemRecover.UseVisualStyleBackColor = true;
            // 
            // checkBox_enableNotify
            // 
            this.checkBox_enableNotify.AutoEllipsis = true;
            this.checkBox_enableNotify.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.eMailNotifyConfigBS, "NotifyConfig.EnableNotify", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_enableNotify.Location = new System.Drawing.Point(8, 4);
            this.checkBox_enableNotify.Name = "checkBox_enableNotify";
            this.checkBox_enableNotify.Size = new System.Drawing.Size(244, 20);
            this.checkBox_enableNotify.TabIndex = 5;
            this.checkBox_enableNotify.Text = "启用邮件通知";
            this.checkBox_enableNotify.UseVisualStyleBackColor = true;
            // 
            // panel_PeriodicReport
            // 
            this.panel_PeriodicReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_PeriodicReport.Controls.Add(this.linkLabel_PeriodicReport);
            this.panel_PeriodicReport.Controls.Add(this.checkBox_SystemRefresh);
            this.panel_PeriodicReport.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.eMailNotifyConfigBS, "NotifyConfig.EnableNotify", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.panel_PeriodicReport.Location = new System.Drawing.Point(0, 57);
            this.panel_PeriodicReport.Name = "panel_PeriodicReport";
            this.panel_PeriodicReport.Size = new System.Drawing.Size(606, 50);
            this.panel_PeriodicReport.TabIndex = 7;
            this.panel_PeriodicReport.TabStop = true;
            // 
            // linkLabel_PeriodicReport
            // 
            this.linkLabel_PeriodicReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_PeriodicReport.AutoEllipsis = true;
            this.linkLabel_PeriodicReport.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.eMailNotifyConfigBS, "NotifyConfig.TimeEMailNotify", true));
            this.linkLabel_PeriodicReport.Location = new System.Drawing.Point(24, 26);
            this.linkLabel_PeriodicReport.Name = "linkLabel_PeriodicReport";
            this.linkLabel_PeriodicReport.Size = new System.Drawing.Size(578, 20);
            this.linkLabel_PeriodicReport.TabIndex = 1;
            this.linkLabel_PeriodicReport.TabStop = true;
            this.linkLabel_PeriodicReport.Text = "设置定期发送系统运行报告邮件";
            this.linkLabel_PeriodicReport.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel_PeriodicReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_PeriodicReport_LinkClicked);
            // 
            // checkBox_SystemRefresh
            // 
            this.checkBox_SystemRefresh.AutoEllipsis = true;
            this.checkBox_SystemRefresh.Checked = true;
            this.checkBox_SystemRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SystemRefresh.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.eMailNotifyConfigBS, "NotifyConfig.TimeEMailNotify", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_SystemRefresh.Location = new System.Drawing.Point(8, 1);
            this.checkBox_SystemRefresh.Name = "checkBox_SystemRefresh";
            this.checkBox_SystemRefresh.Size = new System.Drawing.Size(244, 20);
            this.checkBox_SystemRefresh.TabIndex = 0;
            this.checkBox_SystemRefresh.Text = "启用发送系统报告邮件";
            this.checkBox_SystemRefresh.UseVisualStyleBackColor = true;
            // 
            // groupBox_receiver
            // 
            this.groupBox_receiver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_receiver.Controls.Add(this.dataGridView_receiver);
            this.groupBox_receiver.Controls.Add(this.panel2);
            this.groupBox_receiver.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.eMailNotifyConfigBS, "NotifyConfig.EnableNotify", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.groupBox_receiver.Location = new System.Drawing.Point(8, 212);
            this.groupBox_receiver.Name = "groupBox_receiver";
            this.groupBox_receiver.Size = new System.Drawing.Size(594, 140);
            this.groupBox_receiver.TabIndex = 8;
            this.groupBox_receiver.TabStop = false;
            this.groupBox_receiver.Text = "收件人";
            // 
            // dataGridView_receiver
            // 
            this.dataGridView_receiver.AllowUserToAddRows = false;
            this.dataGridView_receiver.AllowUserToDeleteRows = false;
            this.dataGridView_receiver.AllowUserToResizeColumns = false;
            this.dataGridView_receiver.AllowUserToResizeRows = false;
            this.dataGridView_receiver.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_receiver.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_receiver.ColumnHeadersHeight = 20;
            this.dataGridView_receiver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_receiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_receiver.Location = new System.Drawing.Point(3, 17);
            this.dataGridView_receiver.Name = "dataGridView_receiver";
            this.dataGridView_receiver.ReadOnly = true;
            this.dataGridView_receiver.RowHeadersVisible = false;
            this.dataGridView_receiver.RowTemplate.Height = 23;
            this.dataGridView_receiver.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_receiver.Size = new System.Drawing.Size(533, 120);
            this.dataGridView_receiver.TabIndex = 4;
            this.dataGridView_receiver.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.crystalButton_modifyRecv);
            this.panel2.Controls.Add(this.crystalButton_deleteRecv);
            this.panel2.Controls.Add(this.crystalButton_addRecv);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(536, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(55, 120);
            this.panel2.TabIndex = 1;
            // 
            // crystalButton_modifyRecv
            // 
            this.crystalButton_modifyRecv.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_modifyRecv.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_modifyRecv.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_modifyRecv.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_modifyRecv.ButtonCornorRadius = 5;
            this.crystalButton_modifyRecv.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_modifyRecv.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_modifyRecv.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_modifyRecv.Image = ((System.Drawing.Image)(resources.GetObject("crystalButton_modifyRecv.Image")));
            this.crystalButton_modifyRecv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_modifyRecv.IsButtonFoucs = false;
            this.crystalButton_modifyRecv.Location = new System.Drawing.Point(11, 49);
            this.crystalButton_modifyRecv.Name = "crystalButton_modifyRecv";
            this.crystalButton_modifyRecv.Size = new System.Drawing.Size(32, 28);
            this.crystalButton_modifyRecv.TabIndex = 1;
            this.crystalButton_modifyRecv.Transparency = 50;
            this.crystalButton_modifyRecv.UseVisualStyleBackColor = false;
            this.crystalButton_modifyRecv.Click += new System.EventHandler(this.crystalButton_modifyRecv_Click);
            // 
            // crystalButton_deleteRecv
            // 
            this.crystalButton_deleteRecv.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_deleteRecv.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_deleteRecv.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_deleteRecv.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_deleteRecv.ButtonCornorRadius = 5;
            this.crystalButton_deleteRecv.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_deleteRecv.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_deleteRecv.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_deleteRecv.Image = ((System.Drawing.Image)(resources.GetObject("crystalButton_deleteRecv.Image")));
            this.crystalButton_deleteRecv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_deleteRecv.IsButtonFoucs = false;
            this.crystalButton_deleteRecv.Location = new System.Drawing.Point(11, 83);
            this.crystalButton_deleteRecv.Name = "crystalButton_deleteRecv";
            this.crystalButton_deleteRecv.Size = new System.Drawing.Size(32, 32);
            this.crystalButton_deleteRecv.TabIndex = 2;
            this.crystalButton_deleteRecv.Transparency = 50;
            this.crystalButton_deleteRecv.UseVisualStyleBackColor = false;
            this.crystalButton_deleteRecv.Click += new System.EventHandler(this.crystalButton_deleteRecv_Click);
            // 
            // crystalButton_addRecv
            // 
            this.crystalButton_addRecv.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_addRecv.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_addRecv.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_addRecv.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_addRecv.ButtonCornorRadius = 5;
            this.crystalButton_addRecv.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_addRecv.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_addRecv.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_addRecv.Image = ((System.Drawing.Image)(resources.GetObject("crystalButton_addRecv.Image")));
            this.crystalButton_addRecv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_addRecv.IsButtonFoucs = false;
            this.crystalButton_addRecv.Location = new System.Drawing.Point(11, 11);
            this.crystalButton_addRecv.Name = "crystalButton_addRecv";
            this.crystalButton_addRecv.Size = new System.Drawing.Size(32, 32);
            this.crystalButton_addRecv.TabIndex = 0;
            this.crystalButton_addRecv.Transparency = 50;
            this.crystalButton_addRecv.UseVisualStyleBackColor = false;
            this.crystalButton_addRecv.Click += new System.EventHandler(this.crystalButton_addRecv_Click);
            // 
            // groupBox_emailInfo
            // 
            this.groupBox_emailInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_emailInfo.Controls.Add(this.label_example);
            this.groupBox_emailInfo.Controls.Add(this.textBox_SendSource);
            this.groupBox_emailInfo.Controls.Add(this.label_errorNotifyAddr);
            this.groupBox_emailInfo.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.eMailNotifyConfigBS, "NotifyConfig.EnableNotify", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.groupBox_emailInfo.Location = new System.Drawing.Point(10, 355);
            this.groupBox_emailInfo.Name = "groupBox_emailInfo";
            this.groupBox_emailInfo.Size = new System.Drawing.Size(591, 53);
            this.groupBox_emailInfo.TabIndex = 9;
            this.groupBox_emailInfo.TabStop = false;
            this.groupBox_emailInfo.Text = "邮件信息";
            // 
            // label_example
            // 
            this.label_example.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_example.AutoEllipsis = true;
            this.label_example.Location = new System.Drawing.Point(287, 22);
            this.label_example.Name = "label_example";
            this.label_example.Size = new System.Drawing.Size(301, 24);
            this.label_example.TabIndex = 2;
            this.label_example.Text = "（如：A小区，B广场等）";
            // 
            // textBox_SendSource
            // 
            this.textBox_SendSource.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.eMailNotifyConfigBS, "NotifyConfig.EmailSendSource", true));
            this.textBox_SendSource.Location = new System.Drawing.Point(120, 24);
            this.textBox_SendSource.Name = "textBox_SendSource";
            this.textBox_SendSource.Size = new System.Drawing.Size(145, 21);
            this.textBox_SendSource.TabIndex = 0;
            this.textBox_SendSource.Text = "A小区";
            // 
            // label_errorNotifyAddr
            // 
            this.label_errorNotifyAddr.AutoEllipsis = true;
            this.label_errorNotifyAddr.Location = new System.Drawing.Point(12, 22);
            this.label_errorNotifyAddr.Name = "label_errorNotifyAddr";
            this.label_errorNotifyAddr.Size = new System.Drawing.Size(102, 24);
            this.label_errorNotifyAddr.TabIndex = 0;
            this.label_errorNotifyAddr.Text = "邮件发出地：";
            // 
            // label_NotifyTip
            // 
            this.label_NotifyTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_NotifyTip.AutoEllipsis = true;
            this.label_NotifyTip.Location = new System.Drawing.Point(12, 416);
            this.label_NotifyTip.Name = "label_NotifyTip";
            this.label_NotifyTip.Size = new System.Drawing.Size(589, 20);
            this.label_NotifyTip.TabIndex = 3;
            this.label_NotifyTip.Text = "提示：显示屏已注册NovaiCare，请禁用本地邮件通知，以免收到重复邮件。";
            // 
            // UC_EMailNotify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UC_EMailNotify";
            this.Size = new System.Drawing.Size(626, 507);
            this.panel_ConfigBase.ResumeLayout(false);
            this.groupBox_senderSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eMailNotifyConfigBS)).EndInit();
            this.panel_PeriodicReport.ResumeLayout(false);
            this.groupBox_receiver.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_receiver)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox_emailInfo.ResumeLayout(false);
            this.groupBox_emailInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_SystemRecover;
        private System.Windows.Forms.CheckBox checkBox_enableNotify;
        private System.Windows.Forms.GroupBox groupBox_senderSetting;
        private System.Windows.Forms.CheckBox checkBox_SSLOpen;
        private System.Windows.Forms.Label label_SSl;
        private Nova.Control.CrystalButton crystalButton_useDefault;
        private System.Windows.Forms.Label label_senderPort;
        private System.Windows.Forms.Label label_portInfo;
        private System.Windows.Forms.Label label_smtpServer;
        private System.Windows.Forms.Label label_senderAddr;
        private System.Windows.Forms.Label label_smtpServerInfo;
        private System.Windows.Forms.Label label_userAddr;
        private System.Windows.Forms.LinkLabel linkLabel_modifySender;
        private System.Windows.Forms.GroupBox groupBox_emailInfo;
        private System.Windows.Forms.Label label_example;
        private System.Windows.Forms.TextBox textBox_SendSource;
        private System.Windows.Forms.Label label_errorNotifyAddr;
        private System.Windows.Forms.GroupBox groupBox_receiver;
        private System.Windows.Forms.DataGridView dataGridView_receiver;
        private System.Windows.Forms.Panel panel2;
        private Nova.Control.CrystalButton crystalButton_modifyRecv;
        private Nova.Control.CrystalButton crystalButton_deleteRecv;
        private Nova.Control.CrystalButton crystalButton_addRecv;
        private System.Windows.Forms.Panel panel_PeriodicReport;
        private System.Windows.Forms.LinkLabel linkLabel_PeriodicReport;
        private System.Windows.Forms.CheckBox checkBox_SystemRefresh;
        private System.Windows.Forms.BindingSource eMailNotifyConfigBS;
        private System.Windows.Forms.Label label_NotifyTip;

    }
}
