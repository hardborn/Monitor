namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_CycleConfig
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
            this.seperatePanel_CountSetting = new Nova.Control.Panel.SeperatePanel();
            this.label_RetryTimes = new System.Windows.Forms.Label();
            this.numericUpDown_RetryTimes = new System.Windows.Forms.NumericUpDown();
            this.uCCycleConfigVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label_Count = new System.Windows.Forms.Label();
            this.label_RefreshPeriod = new System.Windows.Forms.Label();
            this.numericUpDown_MonitorPeriodValue = new System.Windows.Forms.NumericUpDown();
            this.label_MonitorPeriodUnit = new System.Windows.Forms.Label();
            this.seperatePanel_AutoRefresh = new Nova.Control.Panel.SeperatePanel();
            this.checkBox_AutoRefresh = new System.Windows.Forms.CheckBox();
            this.crystalButton_RecommendTime = new Nova.Control.CrystalButton();
            this.panel_ConfigBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetryTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCCycleConfigVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MonitorPeriodValue)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.crystalButton_RecommendTime);
            this.panel_ConfigBase.Controls.Add(this.seperatePanel_CountSetting);
            this.panel_ConfigBase.Controls.Add(this.label_RetryTimes);
            this.panel_ConfigBase.Controls.Add(this.numericUpDown_RetryTimes);
            this.panel_ConfigBase.Controls.Add(this.label_Count);
            this.panel_ConfigBase.Controls.Add(this.label_RefreshPeriod);
            this.panel_ConfigBase.Controls.Add(this.numericUpDown_MonitorPeriodValue);
            this.panel_ConfigBase.Controls.Add(this.label_MonitorPeriodUnit);
            this.panel_ConfigBase.Controls.Add(this.seperatePanel_AutoRefresh);
            this.panel_ConfigBase.Controls.Add(this.checkBox_AutoRefresh);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // seperatePanel_CountSetting
            // 
            this.seperatePanel_CountSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_CountSetting.BackColor = System.Drawing.Color.Transparent;
            this.seperatePanel_CountSetting.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_CountSetting.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_CountSetting.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_CountSetting.Location = new System.Drawing.Point(5, 104);
            this.seperatePanel_CountSetting.Name = "seperatePanel_CountSetting";
            this.seperatePanel_CountSetting.SeperateLineHeight = 2;
            this.seperatePanel_CountSetting.Size = new System.Drawing.Size(445, 23);
            this.seperatePanel_CountSetting.TabIndex = 103;
            this.seperatePanel_CountSetting.TabStop = false;
            this.seperatePanel_CountSetting.Text = "重读次数设置";
            // 
            // label_RetryTimes
            // 
            this.label_RetryTimes.AutoEllipsis = true;
            this.label_RetryTimes.BackColor = System.Drawing.Color.Transparent;
            this.label_RetryTimes.Location = new System.Drawing.Point(20, 130);
            this.label_RetryTimes.Name = "label_RetryTimes";
            this.label_RetryTimes.Size = new System.Drawing.Size(209, 27);
            this.label_RetryTimes.TabIndex = 95;
            this.label_RetryTimes.Text = "读状态失败时的重读次数:";
            this.label_RetryTimes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_RetryTimes
            // 
            this.numericUpDown_RetryTimes.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCCycleConfigVMBindingSource, "RetryReadTimes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_RetryTimes.Location = new System.Drawing.Point(236, 135);
            this.numericUpDown_RetryTimes.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown_RetryTimes.Name = "numericUpDown_RetryTimes";
            this.numericUpDown_RetryTimes.Size = new System.Drawing.Size(74, 21);
            this.numericUpDown_RetryTimes.TabIndex = 101;
            // 
            // uCCycleConfigVMBindingSource
            // 
            this.uCCycleConfigVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_CycleConfig_VM);
            // 
            // label_Count
            // 
            this.label_Count.AutoEllipsis = true;
            this.label_Count.BackColor = System.Drawing.Color.Transparent;
            this.label_Count.Location = new System.Drawing.Point(312, 137);
            this.label_Count.Name = "label_Count";
            this.label_Count.Size = new System.Drawing.Size(57, 16);
            this.label_Count.TabIndex = 96;
            this.label_Count.Text = "次";
            // 
            // label_RefreshPeriod
            // 
            this.label_RefreshPeriod.AutoEllipsis = true;
            this.label_RefreshPeriod.BackColor = System.Drawing.Color.Transparent;
            this.label_RefreshPeriod.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCCycleConfigVMBindingSource, "IsCycleMonitor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.label_RefreshPeriod.Location = new System.Drawing.Point(147, 63);
            this.label_RefreshPeriod.Name = "label_RefreshPeriod";
            this.label_RefreshPeriod.Size = new System.Drawing.Size(82, 29);
            this.label_RefreshPeriod.TabIndex = 97;
            this.label_RefreshPeriod.Text = "刷新周期:";
            this.label_RefreshPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_MonitorPeriodValue
            // 
            this.numericUpDown_MonitorPeriodValue.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCCycleConfigVMBindingSource, "MonitorPeriod", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_MonitorPeriodValue.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCCycleConfigVMBindingSource, "IsCycleMonitor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.numericUpDown_MonitorPeriodValue.Location = new System.Drawing.Point(236, 67);
            this.numericUpDown_MonitorPeriodValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_MonitorPeriodValue.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_MonitorPeriodValue.Name = "numericUpDown_MonitorPeriodValue";
            this.numericUpDown_MonitorPeriodValue.Size = new System.Drawing.Size(74, 21);
            this.numericUpDown_MonitorPeriodValue.TabIndex = 100;
            this.numericUpDown_MonitorPeriodValue.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label_MonitorPeriodUnit
            // 
            this.label_MonitorPeriodUnit.AutoEllipsis = true;
            this.label_MonitorPeriodUnit.BackColor = System.Drawing.Color.Transparent;
            this.label_MonitorPeriodUnit.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCCycleConfigVMBindingSource, "IsCycleMonitor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.label_MonitorPeriodUnit.Location = new System.Drawing.Point(312, 68);
            this.label_MonitorPeriodUnit.Name = "label_MonitorPeriodUnit";
            this.label_MonitorPeriodUnit.Size = new System.Drawing.Size(32, 19);
            this.label_MonitorPeriodUnit.TabIndex = 98;
            this.label_MonitorPeriodUnit.Text = "S";
            // 
            // seperatePanel_AutoRefresh
            // 
            this.seperatePanel_AutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_AutoRefresh.BackColor = System.Drawing.Color.Transparent;
            this.seperatePanel_AutoRefresh.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_AutoRefresh.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_AutoRefresh.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_AutoRefresh.Location = new System.Drawing.Point(5, 33);
            this.seperatePanel_AutoRefresh.Name = "seperatePanel_AutoRefresh";
            this.seperatePanel_AutoRefresh.SeperateLineHeight = 2;
            this.seperatePanel_AutoRefresh.Size = new System.Drawing.Size(445, 23);
            this.seperatePanel_AutoRefresh.TabIndex = 102;
            this.seperatePanel_AutoRefresh.TabStop = false;
            this.seperatePanel_AutoRefresh.Text = "周期刷新";
            // 
            // checkBox_AutoRefresh
            // 
            this.checkBox_AutoRefresh.AutoEllipsis = true;
            this.checkBox_AutoRefresh.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_AutoRefresh.Checked = true;
            this.checkBox_AutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_AutoRefresh.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCCycleConfigVMBindingSource, "IsCycleMonitor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_AutoRefresh.Location = new System.Drawing.Point(20, 63);
            this.checkBox_AutoRefresh.Name = "checkBox_AutoRefresh";
            this.checkBox_AutoRefresh.Size = new System.Drawing.Size(120, 29);
            this.checkBox_AutoRefresh.TabIndex = 99;
            this.checkBox_AutoRefresh.Text = "自动刷新";
            this.checkBox_AutoRefresh.UseVisualStyleBackColor = false;
            // 
            // crystalButton_RecommendTime
            // 
            this.crystalButton_RecommendTime.AutoEllipsis = true;
            this.crystalButton_RecommendTime.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_RecommendTime.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_RecommendTime.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RecommendTime.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_RecommendTime.ButtonCornorRadius = 3;
            this.crystalButton_RecommendTime.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RecommendTime.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_RecommendTime.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.uCCycleConfigVMBindingSource, "IsCycleMonitor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.crystalButton_RecommendTime.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_RecommendTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_RecommendTime.IsButtonFoucs = false;
            this.crystalButton_RecommendTime.Location = new System.Drawing.Point(350, 62);
            this.crystalButton_RecommendTime.Name = "crystalButton_RecommendTime";
            this.crystalButton_RecommendTime.Size = new System.Drawing.Size(90, 30);
            this.crystalButton_RecommendTime.TabIndex = 104;
            this.crystalButton_RecommendTime.Text = "推荐周期";
            this.crystalButton_RecommendTime.Transparency = 50;
            this.crystalButton_RecommendTime.UseVisualStyleBackColor = false;
            this.crystalButton_RecommendTime.Visible = false;
            this.crystalButton_RecommendTime.Click += new System.EventHandler(this.crystalButton_RecommendTime_Click);
            // 
            // UC_CycleConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "UC_CycleConfig";
            this.panel_ConfigBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetryTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCCycleConfigVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MonitorPeriodValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.Panel.SeperatePanel seperatePanel_CountSetting;
        private System.Windows.Forms.Label label_RetryTimes;
        private System.Windows.Forms.NumericUpDown numericUpDown_RetryTimes;
        private System.Windows.Forms.Label label_Count;
        private System.Windows.Forms.Label label_RefreshPeriod;
        private System.Windows.Forms.NumericUpDown numericUpDown_MonitorPeriodValue;
        private System.Windows.Forms.Label label_MonitorPeriodUnit;
        private Nova.Control.Panel.SeperatePanel seperatePanel_AutoRefresh;
        private System.Windows.Forms.CheckBox checkBox_AutoRefresh;
        private System.Windows.Forms.BindingSource uCCycleConfigVMBindingSource;
        protected Nova.Control.CrystalButton crystalButton_RecommendTime;


    }
}
