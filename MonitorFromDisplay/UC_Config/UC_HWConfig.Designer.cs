using System.Windows.Forms;
namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_HWConfig
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
            this.panel_IsMonitor = new System.Windows.Forms.Panel();
            this.label_HWDescription = new System.Windows.Forms.Label();
            this.checkBox_Humidity = new System.Windows.Forms.CheckBox();
            this.uCHWConfigVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel_Power = new System.Windows.Forms.Panel();
            this.panel_Powertent = new System.Windows.Forms.Panel();
            this.numericUpDown_MCSamePowerCount = new System.Windows.Forms.NumericUpDown();
            this.radioButton_SamePowerCount = new System.Windows.Forms.RadioButton();
            this.radioButton_DifferentPowerCount = new System.Windows.Forms.RadioButton();
            this.crystalButton_PowerCountSetting = new Nova.Control.CrystalButton();
            this.checkBox_MCPower = new System.Windows.Forms.CheckBox();
            this.checkBox_Smoke = new System.Windows.Forms.CheckBox();
            this.panel_FanConfig = new System.Windows.Forms.Panel();
            this.panel_Fantent = new System.Windows.Forms.Panel();
            this.label_FanPulseUnit = new System.Windows.Forms.Label();
            this.label_FanPulse = new System.Windows.Forms.Label();
            this.numericUpDown_FanPulse = new System.Windows.Forms.NumericUpDown();
            this.radioButton_DifferentFanCount = new System.Windows.Forms.RadioButton();
            this.crystalButton_FanCountSetting = new Nova.Control.CrystalButton();
            this.radioButton_SameFanCount = new System.Windows.Forms.RadioButton();
            this.numericUpDown_MCSameFanCount = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Fan = new System.Windows.Forms.CheckBox();
            this.checkBox_RowLine = new System.Windows.Forms.CheckBox();
            this.checkBox_IsUpdateGeneralStatus = new System.Windows.Forms.CheckBox();
            this.checkBox_IsConnectMC = new System.Windows.Forms.CheckBox();
            this.panel_ConfigBase.SuspendLayout();
            this.panel_IsMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uCHWConfigVMBindingSource)).BeginInit();
            this.panel_Power.SuspendLayout();
            this.panel_Powertent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MCSamePowerCount)).BeginInit();
            this.panel_FanConfig.SuspendLayout();
            this.panel_Fantent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FanPulse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MCSameFanCount)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.checkBox_IsConnectMC);
            this.panel_ConfigBase.Controls.Add(this.panel_IsMonitor);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // panel_IsMonitor
            // 
            this.panel_IsMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_IsMonitor.Controls.Add(this.label_HWDescription);
            this.panel_IsMonitor.Controls.Add(this.checkBox_Humidity);
            this.panel_IsMonitor.Controls.Add(this.panel_Power);
            this.panel_IsMonitor.Controls.Add(this.checkBox_Smoke);
            this.panel_IsMonitor.Controls.Add(this.panel_FanConfig);
            this.panel_IsMonitor.Controls.Add(this.checkBox_RowLine);
            this.panel_IsMonitor.Controls.Add(this.checkBox_IsUpdateGeneralStatus);
            this.panel_IsMonitor.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCHWConfigVMBindingSource, "IsMonitorCardConnected", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.panel_IsMonitor.Location = new System.Drawing.Point(3, 44);
            this.panel_IsMonitor.Name = "panel_IsMonitor";
            this.panel_IsMonitor.Size = new System.Drawing.Size(455, 328);
            this.panel_IsMonitor.TabIndex = 28;
            // 
            // label_HWDescription
            // 
            this.label_HWDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_HWDescription.AutoEllipsis = true;
            this.label_HWDescription.Location = new System.Drawing.Point(5, 296);
            this.label_HWDescription.Name = "label_HWDescription";
            this.label_HWDescription.Size = new System.Drawing.Size(440, 26);
            this.label_HWDescription.TabIndex = 15;
            this.label_HWDescription.Text = "说明：全屏配置的默认来源于第一个配置，后期更改单屏不影响全屏信息。";
            // 
            // checkBox_Humidity
            // 
            this.checkBox_Humidity.AutoEllipsis = true;
            this.checkBox_Humidity.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_Humidity.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "IsRefreshHumidity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_Humidity.Location = new System.Drawing.Point(18, 6);
            this.checkBox_Humidity.Name = "checkBox_Humidity";
            this.checkBox_Humidity.Size = new System.Drawing.Size(257, 21);
            this.checkBox_Humidity.TabIndex = 17;
            this.checkBox_Humidity.Text = "刷新湿度";
            this.checkBox_Humidity.UseVisualStyleBackColor = false;
            // 
            // uCHWConfigVMBindingSource
            // 
            this.uCHWConfigVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_HWConfig_VM);
            // 
            // panel_Power
            // 
            this.panel_Power.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Power.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Power.Controls.Add(this.panel_Powertent);
            this.panel_Power.Controls.Add(this.checkBox_MCPower);
            this.panel_Power.Location = new System.Drawing.Point(7, 187);
            this.panel_Power.Name = "panel_Power";
            this.panel_Power.Size = new System.Drawing.Size(445, 105);
            this.panel_Power.TabIndex = 27;
            // 
            // panel_Powertent
            // 
            this.panel_Powertent.Controls.Add(this.numericUpDown_MCSamePowerCount);
            this.panel_Powertent.Controls.Add(this.radioButton_SamePowerCount);
            this.panel_Powertent.Controls.Add(this.radioButton_DifferentPowerCount);
            this.panel_Powertent.Controls.Add(this.crystalButton_PowerCountSetting);
            this.panel_Powertent.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCHWConfigVMBindingSource, "MCPower.IsRefreshPower", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.panel_Powertent.Location = new System.Drawing.Point(10, 25);
            this.panel_Powertent.Name = "panel_Powertent";
            this.panel_Powertent.Size = new System.Drawing.Size(427, 75);
            this.panel_Powertent.TabIndex = 28;
            // 
            // numericUpDown_MCSamePowerCount
            // 
            this.numericUpDown_MCSamePowerCount.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCHWConfigVMBindingSource, "MCPower.PowerCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_MCSamePowerCount.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCHWConfigVMBindingSource, "MCPower.AllPowerOfCabinetSame", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.numericUpDown_MCSamePowerCount.Location = new System.Drawing.Point(280, 10);
            this.numericUpDown_MCSamePowerCount.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.numericUpDown_MCSamePowerCount.Name = "numericUpDown_MCSamePowerCount";
            this.numericUpDown_MCSamePowerCount.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown_MCSamePowerCount.TabIndex = 8;
            this.numericUpDown_MCSamePowerCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // radioButton_SamePowerCount
            // 
            this.radioButton_SamePowerCount.AutoEllipsis = true;
            this.radioButton_SamePowerCount.Checked = true;
            this.radioButton_SamePowerCount.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "MCPower.AllPowerOfCabinetSame", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioButton_SamePowerCount.Location = new System.Drawing.Point(5, 3);
            this.radioButton_SamePowerCount.Name = "radioButton_SamePowerCount";
            this.radioButton_SamePowerCount.Size = new System.Drawing.Size(269, 30);
            this.radioButton_SamePowerCount.TabIndex = 6;
            this.radioButton_SamePowerCount.TabStop = true;
            this.radioButton_SamePowerCount.Text = "每块监控卡上电源个数设置相同";
            this.radioButton_SamePowerCount.UseVisualStyleBackColor = true;
            this.radioButton_SamePowerCount.Click += new System.EventHandler(this.radioButton_SamePowerCount_Click);
            // 
            // radioButton_DifferentPowerCount
            // 
            this.radioButton_DifferentPowerCount.AutoEllipsis = true;
            this.radioButton_DifferentPowerCount.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "MCPower.AllPowerOfCabinetDif", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioButton_DifferentPowerCount.Location = new System.Drawing.Point(5, 39);
            this.radioButton_DifferentPowerCount.Name = "radioButton_DifferentPowerCount";
            this.radioButton_DifferentPowerCount.Size = new System.Drawing.Size(269, 30);
            this.radioButton_DifferentPowerCount.TabIndex = 7;
            this.radioButton_DifferentPowerCount.Text = "每块监控卡上单独设置电源个数";
            this.radioButton_DifferentPowerCount.UseVisualStyleBackColor = true;
            this.radioButton_DifferentPowerCount.Click += new System.EventHandler(this.radioButton_DifferentPowerCount_Click);
            // 
            // crystalButton_PowerCountSetting
            // 
            this.crystalButton_PowerCountSetting.AutoEllipsis = true;
            this.crystalButton_PowerCountSetting.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_PowerCountSetting.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_PowerCountSetting.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_PowerCountSetting.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_PowerCountSetting.ButtonCornorRadius = 3;
            this.crystalButton_PowerCountSetting.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_PowerCountSetting.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_PowerCountSetting.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCHWConfigVMBindingSource, "MCPower.AllPowerOfCabinetDif", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.crystalButton_PowerCountSetting.Enabled = false;
            this.crystalButton_PowerCountSetting.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_PowerCountSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_PowerCountSetting.IsButtonFoucs = false;
            this.crystalButton_PowerCountSetting.Location = new System.Drawing.Point(280, 42);
            this.crystalButton_PowerCountSetting.Name = "crystalButton_PowerCountSetting";
            this.crystalButton_PowerCountSetting.Size = new System.Drawing.Size(78, 24);
            this.crystalButton_PowerCountSetting.TabIndex = 9;
            this.crystalButton_PowerCountSetting.Text = "设置";
            this.crystalButton_PowerCountSetting.Transparency = 50;
            this.crystalButton_PowerCountSetting.UseVisualStyleBackColor = false;
            this.crystalButton_PowerCountSetting.Click += new System.EventHandler(this.crystalButton_PowerCountSetting_Click);
            // 
            // checkBox_MCPower
            // 
            this.checkBox_MCPower.AutoEllipsis = true;
            this.checkBox_MCPower.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_MCPower.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "MCPower.IsRefreshPower", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_MCPower.Location = new System.Drawing.Point(5, 6);
            this.checkBox_MCPower.Name = "checkBox_MCPower";
            this.checkBox_MCPower.Size = new System.Drawing.Size(435, 18);
            this.checkBox_MCPower.TabIndex = 5;
            this.checkBox_MCPower.Text = "刷新监控卡电源";
            this.checkBox_MCPower.UseVisualStyleBackColor = false;
            // 
            // checkBox_Smoke
            // 
            this.checkBox_Smoke.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Smoke.AutoEllipsis = true;
            this.checkBox_Smoke.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_Smoke.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "IsRefreshSmoke", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_Smoke.Location = new System.Drawing.Point(281, 6);
            this.checkBox_Smoke.Name = "checkBox_Smoke";
            this.checkBox_Smoke.Size = new System.Drawing.Size(164, 21);
            this.checkBox_Smoke.TabIndex = 19;
            this.checkBox_Smoke.Text = "刷新烟雾";
            this.checkBox_Smoke.UseVisualStyleBackColor = false;
            // 
            // panel_FanConfig
            // 
            this.panel_FanConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_FanConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_FanConfig.Controls.Add(this.panel_Fantent);
            this.panel_FanConfig.Controls.Add(this.checkBox_Fan);
            this.panel_FanConfig.Location = new System.Drawing.Point(7, 59);
            this.panel_FanConfig.Name = "panel_FanConfig";
            this.panel_FanConfig.Size = new System.Drawing.Size(445, 127);
            this.panel_FanConfig.TabIndex = 25;
            // 
            // panel_Fantent
            // 
            this.panel_Fantent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Fantent.Controls.Add(this.label_FanPulseUnit);
            this.panel_Fantent.Controls.Add(this.label_FanPulse);
            this.panel_Fantent.Controls.Add(this.numericUpDown_FanPulse);
            this.panel_Fantent.Controls.Add(this.radioButton_DifferentFanCount);
            this.panel_Fantent.Controls.Add(this.crystalButton_FanCountSetting);
            this.panel_Fantent.Controls.Add(this.radioButton_SameFanCount);
            this.panel_Fantent.Controls.Add(this.numericUpDown_MCSameFanCount);
            this.panel_Fantent.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCHWConfigVMBindingSource, "FanInfo.IsRefreshFan", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.panel_Fantent.Location = new System.Drawing.Point(11, 24);
            this.panel_Fantent.Name = "panel_Fantent";
            this.panel_Fantent.Size = new System.Drawing.Size(429, 100);
            this.panel_Fantent.TabIndex = 28;
            // 
            // label_FanPulseUnit
            // 
            this.label_FanPulseUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_FanPulseUnit.AutoEllipsis = true;
            this.label_FanPulseUnit.Location = new System.Drawing.Point(361, 9);
            this.label_FanPulseUnit.Name = "label_FanPulseUnit";
            this.label_FanPulseUnit.Size = new System.Drawing.Size(63, 19);
            this.label_FanPulseUnit.TabIndex = 15;
            this.label_FanPulseUnit.Text = "个/转";
            // 
            // label_FanPulse
            // 
            this.label_FanPulse.AutoEllipsis = true;
            this.label_FanPulse.Location = new System.Drawing.Point(23, 6);
            this.label_FanPulse.Name = "label_FanPulse";
            this.label_FanPulse.Size = new System.Drawing.Size(250, 19);
            this.label_FanPulse.TabIndex = 13;
            this.label_FanPulse.Text = "风扇脉冲:";
            this.label_FanPulse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_FanPulse
            // 
            this.numericUpDown_FanPulse.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCHWConfigVMBindingSource, "FanInfo.FanSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_FanPulse.Location = new System.Drawing.Point(279, 7);
            this.numericUpDown_FanPulse.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_FanPulse.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_FanPulse.Name = "numericUpDown_FanPulse";
            this.numericUpDown_FanPulse.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown_FanPulse.TabIndex = 14;
            this.numericUpDown_FanPulse.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButton_DifferentFanCount
            // 
            this.radioButton_DifferentFanCount.AutoEllipsis = true;
            this.radioButton_DifferentFanCount.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "FanInfo.AllFanOfCabinetDif", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioButton_DifferentFanCount.Location = new System.Drawing.Point(4, 66);
            this.radioButton_DifferentFanCount.Name = "radioButton_DifferentFanCount";
            this.radioButton_DifferentFanCount.Size = new System.Drawing.Size(269, 30);
            this.radioButton_DifferentFanCount.TabIndex = 7;
            this.radioButton_DifferentFanCount.Text = "每个箱体单独设置风扇个数";
            this.radioButton_DifferentFanCount.UseVisualStyleBackColor = true;
            this.radioButton_DifferentFanCount.Click += new System.EventHandler(this.radioButton_DifferentFanCount_Click);
            // 
            // crystalButton_FanCountSetting
            // 
            this.crystalButton_FanCountSetting.AutoEllipsis = true;
            this.crystalButton_FanCountSetting.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_FanCountSetting.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_FanCountSetting.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_FanCountSetting.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_FanCountSetting.ButtonCornorRadius = 3;
            this.crystalButton_FanCountSetting.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_FanCountSetting.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_FanCountSetting.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCHWConfigVMBindingSource, "FanInfo.AllFanOfCabinetDif", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.crystalButton_FanCountSetting.Enabled = false;
            this.crystalButton_FanCountSetting.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_FanCountSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_FanCountSetting.IsButtonFoucs = false;
            this.crystalButton_FanCountSetting.Location = new System.Drawing.Point(279, 69);
            this.crystalButton_FanCountSetting.Name = "crystalButton_FanCountSetting";
            this.crystalButton_FanCountSetting.Size = new System.Drawing.Size(78, 24);
            this.crystalButton_FanCountSetting.TabIndex = 9;
            this.crystalButton_FanCountSetting.Text = "设置";
            this.crystalButton_FanCountSetting.Transparency = 50;
            this.crystalButton_FanCountSetting.UseVisualStyleBackColor = false;
            this.crystalButton_FanCountSetting.Click += new System.EventHandler(this.crystalButton_FanCountSetting_Click);
            // 
            // radioButton_SameFanCount
            // 
            this.radioButton_SameFanCount.AutoEllipsis = true;
            this.radioButton_SameFanCount.Checked = true;
            this.radioButton_SameFanCount.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "FanInfo.AllFanOfCabinetSame", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioButton_SameFanCount.Location = new System.Drawing.Point(4, 31);
            this.radioButton_SameFanCount.Name = "radioButton_SameFanCount";
            this.radioButton_SameFanCount.Size = new System.Drawing.Size(269, 30);
            this.radioButton_SameFanCount.TabIndex = 6;
            this.radioButton_SameFanCount.TabStop = true;
            this.radioButton_SameFanCount.Text = "每个箱体风扇个数设置相同";
            this.radioButton_SameFanCount.UseVisualStyleBackColor = true;
            this.radioButton_SameFanCount.Click += new System.EventHandler(this.radioButton_SameFanCount_Click);
            // 
            // numericUpDown_MCSameFanCount
            // 
            this.numericUpDown_MCSameFanCount.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCHWConfigVMBindingSource, "FanInfo.FanCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_MCSameFanCount.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCHWConfigVMBindingSource, "FanInfo.AllFanOfCabinetSame", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.numericUpDown_MCSameFanCount.Location = new System.Drawing.Point(279, 37);
            this.numericUpDown_MCSameFanCount.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_MCSameFanCount.Name = "numericUpDown_MCSameFanCount";
            this.numericUpDown_MCSameFanCount.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown_MCSameFanCount.TabIndex = 8;
            this.numericUpDown_MCSameFanCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // checkBox_Fan
            // 
            this.checkBox_Fan.AutoEllipsis = true;
            this.checkBox_Fan.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_Fan.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "FanInfo.IsRefreshFan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_Fan.Location = new System.Drawing.Point(10, 3);
            this.checkBox_Fan.Name = "checkBox_Fan";
            this.checkBox_Fan.Size = new System.Drawing.Size(430, 18);
            this.checkBox_Fan.TabIndex = 5;
            this.checkBox_Fan.Text = "刷新风扇";
            this.checkBox_Fan.UseVisualStyleBackColor = false;
            // 
            // checkBox_RowLine
            // 
            this.checkBox_RowLine.AutoEllipsis = true;
            this.checkBox_RowLine.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_RowLine.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "IsRefreshCabinet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_RowLine.Location = new System.Drawing.Point(18, 32);
            this.checkBox_RowLine.Name = "checkBox_RowLine";
            this.checkBox_RowLine.Size = new System.Drawing.Size(257, 21);
            this.checkBox_RowLine.TabIndex = 21;
            this.checkBox_RowLine.Text = "刷新箱体状态";
            this.checkBox_RowLine.UseVisualStyleBackColor = false;
            // 
            // checkBox_IsUpdateGeneralStatus
            // 
            this.checkBox_IsUpdateGeneralStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_IsUpdateGeneralStatus.AutoEllipsis = true;
            this.checkBox_IsUpdateGeneralStatus.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_IsUpdateGeneralStatus.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "IsRefreshCabinetDoor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_IsUpdateGeneralStatus.Location = new System.Drawing.Point(281, 32);
            this.checkBox_IsUpdateGeneralStatus.Name = "checkBox_IsUpdateGeneralStatus";
            this.checkBox_IsUpdateGeneralStatus.Size = new System.Drawing.Size(164, 21);
            this.checkBox_IsUpdateGeneralStatus.TabIndex = 23;
            this.checkBox_IsUpdateGeneralStatus.Text = "刷新箱门状态";
            this.checkBox_IsUpdateGeneralStatus.UseVisualStyleBackColor = false;
            // 
            // checkBox_IsConnectMC
            // 
            this.checkBox_IsConnectMC.AutoEllipsis = true;
            this.checkBox_IsConnectMC.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_IsConnectMC.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCHWConfigVMBindingSource, "IsMonitorCardConnected", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_IsConnectMC.Location = new System.Drawing.Point(21, 17);
            this.checkBox_IsConnectMC.Name = "checkBox_IsConnectMC";
            this.checkBox_IsConnectMC.Size = new System.Drawing.Size(430, 21);
            this.checkBox_IsConnectMC.TabIndex = 15;
            this.checkBox_IsConnectMC.Text = "连接监控卡";
            this.checkBox_IsConnectMC.UseVisualStyleBackColor = false;
            // 
            // UC_HWConfig
            // 
            this.Name = "UC_HWConfig";
            this.panel_ConfigBase.ResumeLayout(false);
            this.panel_IsMonitor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uCHWConfigVMBindingSource)).EndInit();
            this.panel_Power.ResumeLayout(false);
            this.panel_Powertent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MCSamePowerCount)).EndInit();
            this.panel_FanConfig.ResumeLayout(false);
            this.panel_Fantent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FanPulse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MCSameFanCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Power;
        private System.Windows.Forms.NumericUpDown numericUpDown_MCSamePowerCount;
        private System.Windows.Forms.RadioButton radioButton_DifferentPowerCount;
        private Nova.Control.CrystalButton crystalButton_PowerCountSetting;
        private System.Windows.Forms.RadioButton radioButton_SamePowerCount;
        private System.Windows.Forms.CheckBox checkBox_MCPower;
        private System.Windows.Forms.Panel panel_FanConfig;
        private System.Windows.Forms.RadioButton radioButton_SameFanCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_MCSameFanCount;
        private Nova.Control.CrystalButton crystalButton_FanCountSetting;
        private System.Windows.Forms.RadioButton radioButton_DifferentFanCount;
        private System.Windows.Forms.CheckBox checkBox_Fan;
        private System.Windows.Forms.CheckBox checkBox_IsUpdateGeneralStatus;
        private System.Windows.Forms.CheckBox checkBox_IsConnectMC;
        private System.Windows.Forms.CheckBox checkBox_RowLine;
        private System.Windows.Forms.CheckBox checkBox_Smoke;
        private System.Windows.Forms.CheckBox checkBox_Humidity;
        private System.Windows.Forms.Label label_FanPulseUnit;
        private System.Windows.Forms.NumericUpDown numericUpDown_FanPulse;
        private System.Windows.Forms.Label label_FanPulse;
        private System.Windows.Forms.BindingSource uCHWConfigVMBindingSource;
        private System.Windows.Forms.Panel panel_Fantent;
        private System.Windows.Forms.Panel panel_Powertent;
        private System.Windows.Forms.Panel panel_IsMonitor;
        private Label label_HWDescription;

    }
}
