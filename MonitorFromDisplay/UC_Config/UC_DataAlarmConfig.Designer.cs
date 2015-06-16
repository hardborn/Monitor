namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_DataAlarmConfig
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
            this.label_TempUnit = new System.Windows.Forms.Label();
            this.uCDataAlarmConfigVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.linkLabel_TempType = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label_WhenPower = new System.Windows.Forms.Label();
            this.label_PowerWarn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_WhenFan = new System.Windows.Forms.Label();
            this.label_FanWarn = new System.Windows.Forms.Label();
            this.numericUpDown_FanThreshold = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_HumidityThreshold = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label_HumidityWarn = new System.Windows.Forms.Label();
            this.label_WhenHumidity = new System.Windows.Forms.Label();
            this.label_TempWarn = new System.Windows.Forms.Label();
            this.numericUpDown_TemperatureThreshold = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label_WhenTemp = new System.Windows.Forms.Label();
            this.numericUpDown_Power = new System.Windows.Forms.NumericUpDown();
            this.label_PowerWarnError = new System.Windows.Forms.Label();
            this.label_WhenPowerError = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_PowerError = new System.Windows.Forms.NumericUpDown();
            this.label_AlarmDescription = new System.Windows.Forms.Label();
            this.tempCheckBox = new System.Windows.Forms.CheckBox();
            this.humidityCheckBox = new System.Windows.Forms.CheckBox();
            this.fanCheckBox = new System.Windows.Forms.CheckBox();
            this.powercheckBox = new System.Windows.Forms.CheckBox();
            this.powerErrorCheckBox = new System.Windows.Forms.CheckBox();
            this.panel_ConfigBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uCDataAlarmConfigVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FanThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumidityThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TemperatureThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Power)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PowerError)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.label_AlarmDescription);
            this.panel_ConfigBase.Controls.Add(this.numericUpDown_PowerError);
            this.panel_ConfigBase.Controls.Add(this.numericUpDown_Power);
            this.panel_ConfigBase.Controls.Add(this.label_TempUnit);
            this.panel_ConfigBase.Controls.Add(this.linkLabel_TempType);
            this.panel_ConfigBase.Controls.Add(this.label7);
            this.panel_ConfigBase.Controls.Add(this.label4);
            this.panel_ConfigBase.Controls.Add(this.label_WhenPowerError);
            this.panel_ConfigBase.Controls.Add(this.label_PowerWarnError);
            this.panel_ConfigBase.Controls.Add(this.label_WhenPower);
            this.panel_ConfigBase.Controls.Add(this.label_PowerWarn);
            this.panel_ConfigBase.Controls.Add(this.label3);
            this.panel_ConfigBase.Controls.Add(this.label_WhenFan);
            this.panel_ConfigBase.Controls.Add(this.label_FanWarn);
            this.panel_ConfigBase.Controls.Add(this.numericUpDown_FanThreshold);
            this.panel_ConfigBase.Controls.Add(this.numericUpDown_HumidityThreshold);
            this.panel_ConfigBase.Controls.Add(this.label2);
            this.panel_ConfigBase.Controls.Add(this.label_HumidityWarn);
            this.panel_ConfigBase.Controls.Add(this.label_WhenHumidity);
            this.panel_ConfigBase.Controls.Add(this.label_TempWarn);
            this.panel_ConfigBase.Controls.Add(this.numericUpDown_TemperatureThreshold);
            this.panel_ConfigBase.Controls.Add(this.label1);
            this.panel_ConfigBase.Controls.Add(this.label_WhenTemp);
            this.panel_ConfigBase.Controls.Add(this.powerErrorCheckBox);
            this.panel_ConfigBase.Controls.Add(this.powercheckBox);
            this.panel_ConfigBase.Controls.Add(this.fanCheckBox);
            this.panel_ConfigBase.Controls.Add(this.humidityCheckBox);
            this.panel_ConfigBase.Controls.Add(this.tempCheckBox);
            this.panel_ConfigBase.Size = new System.Drawing.Size(590, 406);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Location = new System.Drawing.Point(514, 433);
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // label_TempUnit
            // 
            this.label_TempUnit.BackColor = System.Drawing.Color.Transparent;
            this.label_TempUnit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.uCDataAlarmConfigVMBindingSource, "StrTempFlag", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label_TempUnit.Location = new System.Drawing.Point(197, 41);
            this.label_TempUnit.Name = "label_TempUnit";
            this.label_TempUnit.Size = new System.Drawing.Size(13, 22);
            this.label_TempUnit.TabIndex = 116;
            this.label_TempUnit.Text = "℃";
            this.label_TempUnit.TextChanged += new System.EventHandler(this.label_TempUnit_TextChanged);
            // 
            // uCDataAlarmConfigVMBindingSource
            // 
            this.uCDataAlarmConfigVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_DataAlarmConfig_VM);
            // 
            // linkLabel_TempType
            // 
            this.linkLabel_TempType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_TempType.AutoEllipsis = true;
            this.linkLabel_TempType.Location = new System.Drawing.Point(438, 38);
            this.linkLabel_TempType.Name = "linkLabel_TempType";
            this.linkLabel_TempType.Size = new System.Drawing.Size(146, 22);
            this.linkLabel_TempType.TabIndex = 102;
            this.linkLabel_TempType.TabStop = true;
            this.linkLabel_TempType.Text = "华氏温度";
            this.linkLabel_TempType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel_TempType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_TempType_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(120, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 114;
            this.label4.Text = "<";
            // 
            // label_WhenPower
            // 
            this.label_WhenPower.AutoEllipsis = true;
            this.label_WhenPower.BackColor = System.Drawing.Color.Transparent;
            this.label_WhenPower.Location = new System.Drawing.Point(42, 173);
            this.label_WhenPower.Name = "label_WhenPower";
            this.label_WhenPower.Size = new System.Drawing.Size(75, 30);
            this.label_WhenPower.TabIndex = 113;
            this.label_WhenPower.Text = "当电压";
            this.label_WhenPower.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_PowerWarn
            // 
            this.label_PowerWarn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_PowerWarn.AutoEllipsis = true;
            this.label_PowerWarn.BackColor = System.Drawing.Color.Transparent;
            this.label_PowerWarn.Location = new System.Drawing.Point(197, 173);
            this.label_PowerWarn.Name = "label_PowerWarn";
            this.label_PowerWarn.Size = new System.Drawing.Size(193, 30);
            this.label_PowerWarn.TabIndex = 112;
            this.label_PowerWarn.Text = "V时,显示警告信息.";
            this.label_PowerWarn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(120, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "<";
            // 
            // label_WhenFan
            // 
            this.label_WhenFan.AutoEllipsis = true;
            this.label_WhenFan.BackColor = System.Drawing.Color.Transparent;
            this.label_WhenFan.Location = new System.Drawing.Point(42, 126);
            this.label_WhenFan.Name = "label_WhenFan";
            this.label_WhenFan.Size = new System.Drawing.Size(75, 30);
            this.label_WhenFan.TabIndex = 110;
            this.label_WhenFan.Text = "当转速";
            this.label_WhenFan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_FanWarn
            // 
            this.label_FanWarn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_FanWarn.AutoEllipsis = true;
            this.label_FanWarn.BackColor = System.Drawing.Color.Transparent;
            this.label_FanWarn.Location = new System.Drawing.Point(197, 126);
            this.label_FanWarn.Name = "label_FanWarn";
            this.label_FanWarn.Size = new System.Drawing.Size(193, 30);
            this.label_FanWarn.TabIndex = 109;
            this.label_FanWarn.Text = "转/分钟时,显示警告信息.";
            this.label_FanWarn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_FanThreshold
            // 
            this.numericUpDown_FanThreshold.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCDataAlarmConfigVMBindingSource, "FanSpeedThreshold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_FanThreshold.Location = new System.Drawing.Point(134, 131);
            this.numericUpDown_FanThreshold.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_FanThreshold.Name = "numericUpDown_FanThreshold";
            this.numericUpDown_FanThreshold.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown_FanThreshold.TabIndex = 101;
            this.numericUpDown_FanThreshold.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown_HumidityThreshold
            // 
            this.numericUpDown_HumidityThreshold.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCDataAlarmConfigVMBindingSource, "HumidityThreshold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_HumidityThreshold.Location = new System.Drawing.Point(134, 84);
            this.numericUpDown_HumidityThreshold.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDown_HumidityThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_HumidityThreshold.Name = "numericUpDown_HumidityThreshold";
            this.numericUpDown_HumidityThreshold.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown_HumidityThreshold.TabIndex = 100;
            this.numericUpDown_HumidityThreshold.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(120, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 108;
            this.label2.Text = ">";
            // 
            // label_HumidityWarn
            // 
            this.label_HumidityWarn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_HumidityWarn.AutoEllipsis = true;
            this.label_HumidityWarn.BackColor = System.Drawing.Color.Transparent;
            this.label_HumidityWarn.Location = new System.Drawing.Point(197, 79);
            this.label_HumidityWarn.Name = "label_HumidityWarn";
            this.label_HumidityWarn.Size = new System.Drawing.Size(193, 30);
            this.label_HumidityWarn.TabIndex = 106;
            this.label_HumidityWarn.Text = "%时,显示警告信息.";
            this.label_HumidityWarn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_WhenHumidity
            // 
            this.label_WhenHumidity.AutoEllipsis = true;
            this.label_WhenHumidity.BackColor = System.Drawing.Color.Transparent;
            this.label_WhenHumidity.Location = new System.Drawing.Point(42, 79);
            this.label_WhenHumidity.Name = "label_WhenHumidity";
            this.label_WhenHumidity.Size = new System.Drawing.Size(75, 30);
            this.label_WhenHumidity.TabIndex = 107;
            this.label_WhenHumidity.Text = "当湿度";
            this.label_WhenHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_TempWarn
            // 
            this.label_TempWarn.AutoEllipsis = true;
            this.label_TempWarn.BackColor = System.Drawing.Color.Transparent;
            this.label_TempWarn.Location = new System.Drawing.Point(216, 32);
            this.label_TempWarn.Name = "label_TempWarn";
            this.label_TempWarn.Size = new System.Drawing.Size(216, 30);
            this.label_TempWarn.TabIndex = 103;
            this.label_TempWarn.Text = "时,显示警告信息.";
            this.label_TempWarn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_TemperatureThreshold
            // 
            this.numericUpDown_TemperatureThreshold.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCDataAlarmConfigVMBindingSource, "TemperatureThreshold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_TemperatureThreshold.DataBindings.Add(new System.Windows.Forms.Binding("Maximum", this.uCDataAlarmConfigVMBindingSource, "MaxTempValue", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.numericUpDown_TemperatureThreshold.DataBindings.Add(new System.Windows.Forms.Binding("Minimum", this.uCDataAlarmConfigVMBindingSource, "MinTempValue", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.numericUpDown_TemperatureThreshold.Location = new System.Drawing.Point(134, 37);
            this.numericUpDown_TemperatureThreshold.Maximum = new decimal(new int[] {
            79,
            0,
            0,
            0});
            this.numericUpDown_TemperatureThreshold.Minimum = new decimal(new int[] {
            19,
            0,
            0,
            -2147483648});
            this.numericUpDown_TemperatureThreshold.Name = "numericUpDown_TemperatureThreshold";
            this.numericUpDown_TemperatureThreshold.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown_TemperatureThreshold.TabIndex = 99;
            this.numericUpDown_TemperatureThreshold.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(120, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = ">";
            // 
            // label_WhenTemp
            // 
            this.label_WhenTemp.AutoEllipsis = true;
            this.label_WhenTemp.BackColor = System.Drawing.Color.Transparent;
            this.label_WhenTemp.Location = new System.Drawing.Point(42, 32);
            this.label_WhenTemp.Name = "label_WhenTemp";
            this.label_WhenTemp.Size = new System.Drawing.Size(75, 30);
            this.label_WhenTemp.TabIndex = 104;
            this.label_WhenTemp.Text = "当温度";
            this.label_WhenTemp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_Power
            // 
            this.numericUpDown_Power.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCDataAlarmConfigVMBindingSource, "VoltageThreshold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_Power.DecimalPlaces = 1;
            this.numericUpDown_Power.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Power.Location = new System.Drawing.Point(134, 178);
            this.numericUpDown_Power.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDown_Power.Name = "numericUpDown_Power";
            this.numericUpDown_Power.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown_Power.TabIndex = 117;
            this.numericUpDown_Power.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_Power.ValueChanged += new System.EventHandler(this.numericUpDown_Power_ValueChanged);
            // 
            // label_PowerWarnError
            // 
            this.label_PowerWarnError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_PowerWarnError.AutoEllipsis = true;
            this.label_PowerWarnError.BackColor = System.Drawing.Color.Transparent;
            this.label_PowerWarnError.Location = new System.Drawing.Point(197, 220);
            this.label_PowerWarnError.Name = "label_PowerWarnError";
            this.label_PowerWarnError.Size = new System.Drawing.Size(193, 30);
            this.label_PowerWarnError.TabIndex = 112;
            this.label_PowerWarnError.Text = "V时,显示故障信息.";
            this.label_PowerWarnError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_WhenPowerError
            // 
            this.label_WhenPowerError.AutoEllipsis = true;
            this.label_WhenPowerError.BackColor = System.Drawing.Color.Transparent;
            this.label_WhenPowerError.Location = new System.Drawing.Point(42, 220);
            this.label_WhenPowerError.Name = "label_WhenPowerError";
            this.label_WhenPowerError.Size = new System.Drawing.Size(75, 30);
            this.label_WhenPowerError.TabIndex = 113;
            this.label_WhenPowerError.Text = "当电压";
            this.label_WhenPowerError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(120, 229);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 114;
            this.label7.Text = "<";
            // 
            // numericUpDown_PowerError
            // 
            this.numericUpDown_PowerError.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCDataAlarmConfigVMBindingSource, "VoltageErrorThreshold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_PowerError.DecimalPlaces = 1;
            this.numericUpDown_PowerError.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_PowerError.Location = new System.Drawing.Point(134, 225);
            this.numericUpDown_PowerError.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDown_PowerError.Name = "numericUpDown_PowerError";
            this.numericUpDown_PowerError.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown_PowerError.TabIndex = 117;
            this.numericUpDown_PowerError.Value = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            this.numericUpDown_PowerError.ValueChanged += new System.EventHandler(this.numericUpDown_Power_ValueChanged);
            // 
            // label_AlarmDescription
            // 
            this.label_AlarmDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_AlarmDescription.AutoEllipsis = true;
            this.label_AlarmDescription.Location = new System.Drawing.Point(18, 321);
            this.label_AlarmDescription.Name = "label_AlarmDescription";
            this.label_AlarmDescription.Size = new System.Drawing.Size(566, 54);
            this.label_AlarmDescription.TabIndex = 118;
            this.label_AlarmDescription.Text = "说明：全屏配置的默认来源于第一个配置，后期更改单屏不影响全屏信息。";
            // 
            // tempCheckBox
            // 
            this.tempCheckBox.AutoEllipsis = true;
            this.tempCheckBox.AutoSize = true;
            this.tempCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCDataAlarmConfigVMBindingSource, "IsEnableTempAlarm", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tempCheckBox.Location = new System.Drawing.Point(20, 39);
            this.tempCheckBox.Name = "tempCheckBox";
            this.tempCheckBox.Size = new System.Drawing.Size(86, 17);
            this.tempCheckBox.TabIndex = 119;
            this.tempCheckBox.Text = "启用配置项";
            this.tempCheckBox.UseVisualStyleBackColor = true;
            this.tempCheckBox.Visible = false;
            // 
            // humidityCheckBox
            // 
            this.humidityCheckBox.AutoEllipsis = true;
            this.humidityCheckBox.AutoSize = true;
            this.humidityCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCDataAlarmConfigVMBindingSource, "IsEnableHumidityAlarm", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.humidityCheckBox.Location = new System.Drawing.Point(20, 86);
            this.humidityCheckBox.Name = "humidityCheckBox";
            this.humidityCheckBox.Size = new System.Drawing.Size(86, 17);
            this.humidityCheckBox.TabIndex = 120;
            this.humidityCheckBox.Text = "启用配置项";
            this.humidityCheckBox.UseVisualStyleBackColor = true;
            this.humidityCheckBox.Visible = false;
            // 
            // fanCheckBox
            // 
            this.fanCheckBox.AutoEllipsis = true;
            this.fanCheckBox.AutoSize = true;
            this.fanCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCDataAlarmConfigVMBindingSource, "IsEnableFanSpeedAlarm", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fanCheckBox.Location = new System.Drawing.Point(20, 133);
            this.fanCheckBox.Name = "fanCheckBox";
            this.fanCheckBox.Size = new System.Drawing.Size(86, 17);
            this.fanCheckBox.TabIndex = 121;
            this.fanCheckBox.Text = "启用配置项";
            this.fanCheckBox.UseVisualStyleBackColor = true;
            this.fanCheckBox.Visible = false;
            // 
            // powercheckBox
            // 
            this.powercheckBox.AutoEllipsis = true;
            this.powercheckBox.AutoSize = true;
            this.powercheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCDataAlarmConfigVMBindingSource, "IsEnableVoltageAlarm", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.powercheckBox.Location = new System.Drawing.Point(20, 180);
            this.powercheckBox.Name = "powercheckBox";
            this.powercheckBox.Size = new System.Drawing.Size(86, 17);
            this.powercheckBox.TabIndex = 122;
            this.powercheckBox.Text = "启用配置项";
            this.powercheckBox.UseVisualStyleBackColor = true;
            this.powercheckBox.Visible = false;
            // 
            // powerErrorCheckBox
            // 
            this.powerErrorCheckBox.AutoEllipsis = true;
            this.powerErrorCheckBox.AutoSize = true;
            this.powerErrorCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCDataAlarmConfigVMBindingSource, "IsEnableVoltageErrorAlarm", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.powerErrorCheckBox.Location = new System.Drawing.Point(20, 227);
            this.powerErrorCheckBox.Name = "powerErrorCheckBox";
            this.powerErrorCheckBox.Size = new System.Drawing.Size(86, 17);
            this.powerErrorCheckBox.TabIndex = 123;
            this.powerErrorCheckBox.Text = "启用配置项";
            this.powerErrorCheckBox.UseVisualStyleBackColor = true;
            this.powerErrorCheckBox.Visible = false;
            // 
            // UC_DataAlarmConfig
            // 
            this.Name = "UC_DataAlarmConfig";
            this.Size = new System.Drawing.Size(629, 488);
            this.panel_ConfigBase.ResumeLayout(false);
            this.panel_ConfigBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uCDataAlarmConfigVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FanThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumidityThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TemperatureThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Power)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PowerError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_TempUnit;
        private System.Windows.Forms.LinkLabel linkLabel_TempType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_WhenPower;
        private System.Windows.Forms.Label label_PowerWarn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_WhenFan;
        private System.Windows.Forms.Label label_FanWarn;
        private System.Windows.Forms.NumericUpDown numericUpDown_FanThreshold;
        private System.Windows.Forms.NumericUpDown numericUpDown_HumidityThreshold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_HumidityWarn;
        private System.Windows.Forms.Label label_WhenHumidity;
        private System.Windows.Forms.Label label_TempWarn;
        private System.Windows.Forms.NumericUpDown numericUpDown_TemperatureThreshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_WhenTemp;
        private System.Windows.Forms.NumericUpDown numericUpDown_Power;
        private System.Windows.Forms.BindingSource uCDataAlarmConfigVMBindingSource;
        private System.Windows.Forms.NumericUpDown numericUpDown_PowerError;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_WhenPowerError;
        private System.Windows.Forms.Label label_PowerWarnError;
        private System.Windows.Forms.Label label_AlarmDescription;
        private System.Windows.Forms.CheckBox powerErrorCheckBox;
        private System.Windows.Forms.CheckBox powercheckBox;
        private System.Windows.Forms.CheckBox fanCheckBox;
        private System.Windows.Forms.CheckBox humidityCheckBox;
        private System.Windows.Forms.CheckBox tempCheckBox;
    }
}
