using Nova.Monitoring.MonitorDataManager;
namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_MonitorConfigManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MonitorConfigManager));
            this.tableLayoutPanel_Config = new System.Windows.Forms.TableLayoutPanel();
            this.panel_FuncList = new System.Windows.Forms.Panel();
            this.crystalButton_EMailLog = new Nova.Control.CrystalButton();
            this.crystalButton_NotifySetting = new Nova.Control.CrystalButton();
            this.crystalButton_BrightnessConfig = new Nova.Control.CrystalButton();
            this.crystalButton_ControlLog = new Nova.Control.CrystalButton();
            this.crystalButton_ControlConfig = new Nova.Control.CrystalButton();
            this.crystalButton_DataAltarmConfig = new Nova.Control.CrystalButton();
            this.crystalButton_MonitorCardPowerConfig = new Nova.Control.CrystalButton();
            this.crystalButton_HWConfig = new Nova.Control.CrystalButton();
            this.crystalButton_RefreshConfig = new Nova.Control.CrystalButton();
            this.panel_ScreenMessage = new System.Windows.Forms.Panel();
            this.comboBox_Screen = new System.Windows.Forms.ComboBox();
            this.label_ScreenSelected = new System.Windows.Forms.Label();
            this.panel_Config = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_Config.SuspendLayout();
            this.panel_FuncList.SuspendLayout();
            this.panel_ScreenMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Config
            // 
            this.tableLayoutPanel_Config.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel_Config.ColumnCount = 2;
            this.tableLayoutPanel_Config.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel_Config.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Config.Controls.Add(this.panel_FuncList, 0, 0);
            this.tableLayoutPanel_Config.Controls.Add(this.panel_ScreenMessage, 1, 0);
            this.tableLayoutPanel_Config.Controls.Add(this.panel_Config, 1, 1);
            this.tableLayoutPanel_Config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Config.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Config.Name = "tableLayoutPanel_Config";
            this.tableLayoutPanel_Config.RowCount = 2;
            this.tableLayoutPanel_Config.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel_Config.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Config.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Config.Size = new System.Drawing.Size(741, 538);
            this.tableLayoutPanel_Config.TabIndex = 0;
            // 
            // panel_FuncList
            // 
            this.panel_FuncList.Controls.Add(this.crystalButton_EMailLog);
            this.panel_FuncList.Controls.Add(this.crystalButton_NotifySetting);
            this.panel_FuncList.Controls.Add(this.crystalButton_BrightnessConfig);
            this.panel_FuncList.Controls.Add(this.crystalButton_ControlLog);
            this.panel_FuncList.Controls.Add(this.crystalButton_ControlConfig);
            this.panel_FuncList.Controls.Add(this.crystalButton_DataAltarmConfig);
            this.panel_FuncList.Controls.Add(this.crystalButton_MonitorCardPowerConfig);
            this.panel_FuncList.Controls.Add(this.crystalButton_HWConfig);
            this.panel_FuncList.Controls.Add(this.crystalButton_RefreshConfig);
            this.panel_FuncList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_FuncList.Location = new System.Drawing.Point(4, 4);
            this.panel_FuncList.Name = "panel_FuncList";
            this.tableLayoutPanel_Config.SetRowSpan(this.panel_FuncList, 2);
            this.panel_FuncList.Size = new System.Drawing.Size(100, 530);
            this.panel_FuncList.TabIndex = 0;
            // 
            // crystalButton_EMailLog
            // 
            this.crystalButton_EMailLog.AutoEllipsis = true;
            this.crystalButton_EMailLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_EMailLog.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_EMailLog.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_EMailLog.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_EMailLog.ButtonCornorRadius = 3;
            this.crystalButton_EMailLog.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_EMailLog.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_EMailLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_EMailLog.Enabled = false;
            this.crystalButton_EMailLog.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_EMailLog.IsButtonFoucs = false;
            this.crystalButton_EMailLog.Location = new System.Drawing.Point(0, 315);
            this.crystalButton_EMailLog.Name = "crystalButton_EMailLog";
            this.crystalButton_EMailLog.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_EMailLog.TabIndex = 16;
            this.crystalButton_EMailLog.Text = "邮件日志";
            this.crystalButton_EMailLog.Transparency = 50;
            this.crystalButton_EMailLog.UseVisualStyleBackColor = true;
            this.crystalButton_EMailLog.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_NotifySetting
            // 
            this.crystalButton_NotifySetting.AutoEllipsis = true;
            this.crystalButton_NotifySetting.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_NotifySetting.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_NotifySetting.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_NotifySetting.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_NotifySetting.ButtonCornorRadius = 3;
            this.crystalButton_NotifySetting.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_NotifySetting.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_NotifySetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_NotifySetting.Enabled = false;
            this.crystalButton_NotifySetting.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_NotifySetting.IsButtonFoucs = false;
            this.crystalButton_NotifySetting.Location = new System.Drawing.Point(0, 270);
            this.crystalButton_NotifySetting.Name = "crystalButton_NotifySetting";
            this.crystalButton_NotifySetting.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_NotifySetting.TabIndex = 15;
            this.crystalButton_NotifySetting.Text = "邮件设置";
            this.crystalButton_NotifySetting.Transparency = 50;
            this.crystalButton_NotifySetting.UseVisualStyleBackColor = true;
            this.crystalButton_NotifySetting.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_BrightnessConfig
            // 
            this.crystalButton_BrightnessConfig.AutoEllipsis = true;
            this.crystalButton_BrightnessConfig.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_BrightnessConfig.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_BrightnessConfig.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_BrightnessConfig.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_BrightnessConfig.ButtonCornorRadius = 3;
            this.crystalButton_BrightnessConfig.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_BrightnessConfig.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_BrightnessConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_BrightnessConfig.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_BrightnessConfig.IsButtonFoucs = false;
            this.crystalButton_BrightnessConfig.Location = new System.Drawing.Point(0, 225);
            this.crystalButton_BrightnessConfig.Name = "crystalButton_BrightnessConfig";
            this.crystalButton_BrightnessConfig.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_BrightnessConfig.TabIndex = 14;
            this.crystalButton_BrightnessConfig.Text = "亮度调节";
            this.crystalButton_BrightnessConfig.Transparency = 50;
            this.crystalButton_BrightnessConfig.UseVisualStyleBackColor = true;
            this.crystalButton_BrightnessConfig.Visible = false;
            this.crystalButton_BrightnessConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_ControlLog
            // 
            this.crystalButton_ControlLog.AutoEllipsis = true;
            this.crystalButton_ControlLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_ControlLog.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_ControlLog.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_ControlLog.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_ControlLog.ButtonCornorRadius = 3;
            this.crystalButton_ControlLog.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_ControlLog.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_ControlLog.Enabled = false;
            this.crystalButton_ControlLog.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_ControlLog.IsButtonFoucs = false;
            this.crystalButton_ControlLog.Location = new System.Drawing.Point(0, 315);
            this.crystalButton_ControlLog.Name = "crystalButton_ControlLog";
            this.crystalButton_ControlLog.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_ControlLog.TabIndex = 13;
            this.crystalButton_ControlLog.Text = "控制日志";
            this.crystalButton_ControlLog.Transparency = 50;
            this.crystalButton_ControlLog.UseVisualStyleBackColor = true;
            this.crystalButton_ControlLog.Visible = false;
            this.crystalButton_ControlLog.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_ControlConfig
            // 
            this.crystalButton_ControlConfig.AutoEllipsis = true;
            this.crystalButton_ControlConfig.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_ControlConfig.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_ControlConfig.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_ControlConfig.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_ControlConfig.ButtonCornorRadius = 3;
            this.crystalButton_ControlConfig.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_ControlConfig.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_ControlConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_ControlConfig.Enabled = false;
            this.crystalButton_ControlConfig.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_ControlConfig.IsButtonFoucs = false;
            this.crystalButton_ControlConfig.Location = new System.Drawing.Point(0, 180);
            this.crystalButton_ControlConfig.Name = "crystalButton_ControlConfig";
            this.crystalButton_ControlConfig.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_ControlConfig.TabIndex = 10;
            this.crystalButton_ControlConfig.Text = "控制配置";
            this.crystalButton_ControlConfig.Transparency = 50;
            this.crystalButton_ControlConfig.UseVisualStyleBackColor = true;
            this.crystalButton_ControlConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_DataAltarmConfig
            // 
            this.crystalButton_DataAltarmConfig.AutoEllipsis = true;
            this.crystalButton_DataAltarmConfig.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_DataAltarmConfig.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_DataAltarmConfig.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_DataAltarmConfig.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_DataAltarmConfig.ButtonCornorRadius = 3;
            this.crystalButton_DataAltarmConfig.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_DataAltarmConfig.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_DataAltarmConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_DataAltarmConfig.Enabled = false;
            this.crystalButton_DataAltarmConfig.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_DataAltarmConfig.IsButtonFoucs = false;
            this.crystalButton_DataAltarmConfig.Location = new System.Drawing.Point(0, 135);
            this.crystalButton_DataAltarmConfig.Name = "crystalButton_DataAltarmConfig";
            this.crystalButton_DataAltarmConfig.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_DataAltarmConfig.TabIndex = 9;
            this.crystalButton_DataAltarmConfig.Text = "数据告警配置";
            this.crystalButton_DataAltarmConfig.Transparency = 50;
            this.crystalButton_DataAltarmConfig.UseVisualStyleBackColor = true;
            this.crystalButton_DataAltarmConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_MonitorCardPowerConfig
            // 
            this.crystalButton_MonitorCardPowerConfig.AutoEllipsis = true;
            this.crystalButton_MonitorCardPowerConfig.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_MonitorCardPowerConfig.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_MonitorCardPowerConfig.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_MonitorCardPowerConfig.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_MonitorCardPowerConfig.ButtonCornorRadius = 3;
            this.crystalButton_MonitorCardPowerConfig.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_MonitorCardPowerConfig.ButtonSelectedColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_MonitorCardPowerConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_MonitorCardPowerConfig.Enabled = false;
            this.crystalButton_MonitorCardPowerConfig.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_MonitorCardPowerConfig.IsButtonFoucs = false;
            this.crystalButton_MonitorCardPowerConfig.Location = new System.Drawing.Point(0, 90);
            this.crystalButton_MonitorCardPowerConfig.Name = "crystalButton_MonitorCardPowerConfig";
            this.crystalButton_MonitorCardPowerConfig.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_MonitorCardPowerConfig.TabIndex = 7;
            this.crystalButton_MonitorCardPowerConfig.Text = "外设电源配置";
            this.crystalButton_MonitorCardPowerConfig.Transparency = 50;
            this.crystalButton_MonitorCardPowerConfig.UseVisualStyleBackColor = true;
            this.crystalButton_MonitorCardPowerConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_HWConfig
            // 
            this.crystalButton_HWConfig.AutoEllipsis = true;
            this.crystalButton_HWConfig.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_HWConfig.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_HWConfig.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_HWConfig.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_HWConfig.ButtonCornorRadius = 3;
            this.crystalButton_HWConfig.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_HWConfig.ButtonSelectedColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_HWConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_HWConfig.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_HWConfig.IsButtonFoucs = false;
            this.crystalButton_HWConfig.Location = new System.Drawing.Point(0, 45);
            this.crystalButton_HWConfig.Name = "crystalButton_HWConfig";
            this.crystalButton_HWConfig.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_HWConfig.TabIndex = 1;
            this.crystalButton_HWConfig.Text = "硬件配置";
            this.crystalButton_HWConfig.Transparency = 50;
            this.crystalButton_HWConfig.UseVisualStyleBackColor = true;
            this.crystalButton_HWConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // crystalButton_RefreshConfig
            // 
            this.crystalButton_RefreshConfig.AutoEllipsis = true;
            this.crystalButton_RefreshConfig.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_RefreshConfig.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_RefreshConfig.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RefreshConfig.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_RefreshConfig.ButtonCornorRadius = 3;
            this.crystalButton_RefreshConfig.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RefreshConfig.ButtonSelectedColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_RefreshConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.crystalButton_RefreshConfig.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_RefreshConfig.IsButtonFoucs = false;
            this.crystalButton_RefreshConfig.Location = new System.Drawing.Point(0, 0);
            this.crystalButton_RefreshConfig.Name = "crystalButton_RefreshConfig";
            this.crystalButton_RefreshConfig.Size = new System.Drawing.Size(100, 45);
            this.crystalButton_RefreshConfig.TabIndex = 0;
            this.crystalButton_RefreshConfig.Text = "刷新周期";
            this.crystalButton_RefreshConfig.Transparency = 50;
            this.crystalButton_RefreshConfig.UseVisualStyleBackColor = true;
            this.crystalButton_RefreshConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // panel_ScreenMessage
            // 
            this.panel_ScreenMessage.Controls.Add(this.comboBox_Screen);
            this.panel_ScreenMessage.Controls.Add(this.label_ScreenSelected);
            this.panel_ScreenMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ScreenMessage.Location = new System.Drawing.Point(111, 4);
            this.panel_ScreenMessage.Name = "panel_ScreenMessage";
            this.panel_ScreenMessage.Size = new System.Drawing.Size(626, 44);
            this.panel_ScreenMessage.TabIndex = 1;
            // 
            // comboBox_Screen
            // 
            this.comboBox_Screen.DisplayMember = "Data";
            this.comboBox_Screen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Screen.Font = new System.Drawing.Font("宋体", 9F);
            this.comboBox_Screen.FormattingEnabled = true;
            this.comboBox_Screen.Location = new System.Drawing.Point(111, 11);
            this.comboBox_Screen.Name = "comboBox_Screen";
            this.comboBox_Screen.Size = new System.Drawing.Size(254, 20);
            this.comboBox_Screen.TabIndex = 10;
            this.comboBox_Screen.ValueMember = "Data";
            this.comboBox_Screen.SelectedIndexChanged += new System.EventHandler(this.comboBox_Screen_SelectedIndexChanged);
            this.comboBox_Screen.DataSourceChanged += new System.EventHandler(this.comboBox_Screen_DataSourceChanged);
            // 
            // label_ScreenSelected
            // 
            this.label_ScreenSelected.AutoEllipsis = true;
            this.label_ScreenSelected.Location = new System.Drawing.Point(5, 5);
            this.label_ScreenSelected.Name = "label_ScreenSelected";
            this.label_ScreenSelected.Size = new System.Drawing.Size(101, 32);
            this.label_ScreenSelected.TabIndex = 9;
            this.label_ScreenSelected.Text = "选择配屏:";
            this.label_ScreenSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Config
            // 
            this.panel_Config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Config.Location = new System.Drawing.Point(111, 55);
            this.panel_Config.Name = "panel_Config";
            this.panel_Config.Size = new System.Drawing.Size(626, 479);
            this.panel_Config.TabIndex = 2;
            // 
            // Frm_MonitorConfigManager
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(741, 538);
            this.Controls.Add(this.tableLayoutPanel_Config);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_MonitorConfigManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监控终端--配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MonitorConfigManager_FormClosing);
            this.tableLayoutPanel_Config.ResumeLayout(false);
            this.panel_FuncList.ResumeLayout(false);
            this.panel_ScreenMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Config;
        private System.Windows.Forms.Panel panel_FuncList;
        private Nova.Control.CrystalButton crystalButton_RefreshConfig;
        private Nova.Control.CrystalButton crystalButton_HWConfig;
        private System.Windows.Forms.Panel panel_ScreenMessage;
        private System.Windows.Forms.ComboBox comboBox_Screen;
        private System.Windows.Forms.Label label_ScreenSelected;
        private System.Windows.Forms.Panel panel_Config;
        private Nova.Control.CrystalButton crystalButton_ControlLog;
        private Nova.Control.CrystalButton crystalButton_ControlConfig;
        private Nova.Control.CrystalButton crystalButton_DataAltarmConfig;
        private Nova.Control.CrystalButton crystalButton_MonitorCardPowerConfig;
        private Nova.Control.CrystalButton crystalButton_BrightnessConfig;
        private Nova.Control.CrystalButton crystalButton_NotifySetting;
        private Nova.Control.CrystalButton crystalButton_EMailLog;
    }
}

