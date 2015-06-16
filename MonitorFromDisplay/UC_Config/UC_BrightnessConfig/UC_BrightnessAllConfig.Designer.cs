namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    partial class UC_BrightnessAllConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_BrightnessAllConfig));
            this.brightnessSettingTabControl = new System.Windows.Forms.TabControl();
            this.globSettingTabPage = new System.Windows.Forms.TabPage();
            this.openOpticalProbeConfigButton = new Nova.Control.CrystalButton();
            this.groupBox_AutoBrightConfig = new System.Windows.Forms.GroupBox();
            this.checkBox_IsGradualEnable = new System.Windows.Forms.CheckBox();
            this.checkBox_IsSmartEnable = new System.Windows.Forms.CheckBox();
            this.label_Notice = new System.Windows.Forms.Label();
            this.label_Unit = new System.Windows.Forms.Label();
            this.numericUpDown_ReadLuxCnt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_AutoBrightPeriod = new System.Windows.Forms.NumericUpDown();
            this.label_ReadLuxCnt = new System.Windows.Forms.Label();
            this.label_AutoBrightPeriod = new System.Windows.Forms.Label();
            this.seperatePanel_AutoBrightInfo = new Nova.Control.Panel.SeperatePanel();
            this.brightnessConfigTabPage = new System.Windows.Forms.TabPage();
            this.uC_BrightnessConfig1 = new Nova.Monitoring.UI.MonitorFromDisplay.UC_BrightnessConfig();
            this.panel_ConfigBase.SuspendLayout();
            this.brightnessSettingTabControl.SuspendLayout();
            this.globSettingTabPage.SuspendLayout();
            this.groupBox_AutoBrightConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ReadLuxCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoBrightPeriod)).BeginInit();
            this.brightnessConfigTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_ConfigBase.Controls.Add(this.brightnessSettingTabControl);
            this.panel_ConfigBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ConfigBase.Location = new System.Drawing.Point(0, 0);
            this.panel_ConfigBase.Size = new System.Drawing.Size(640, 480);
            // 
            // brightnessSettingTabControl
            // 
            this.brightnessSettingTabControl.Controls.Add(this.globSettingTabPage);
            this.brightnessSettingTabControl.Controls.Add(this.brightnessConfigTabPage);
            this.brightnessSettingTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brightnessSettingTabControl.Location = new System.Drawing.Point(0, 0);
            this.brightnessSettingTabControl.Name = "brightnessSettingTabControl";
            this.brightnessSettingTabControl.SelectedIndex = 0;
            this.brightnessSettingTabControl.Size = new System.Drawing.Size(640, 480);
            this.brightnessSettingTabControl.TabIndex = 0;
            // 
            // globSettingTabPage
            // 
            this.globSettingTabPage.BackColor = System.Drawing.Color.AliceBlue;
            this.globSettingTabPage.Controls.Add(this.openOpticalProbeConfigButton);
            this.globSettingTabPage.Controls.Add(this.groupBox_AutoBrightConfig);
            this.globSettingTabPage.Location = new System.Drawing.Point(4, 22);
            this.globSettingTabPage.Name = "globSettingTabPage";
            this.globSettingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.globSettingTabPage.Size = new System.Drawing.Size(632, 454);
            this.globSettingTabPage.TabIndex = 0;
            this.globSettingTabPage.Text = "全局设置";
            // 
            // openOpticalProbeConfigButton
            // 
            this.openOpticalProbeConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openOpticalProbeConfigButton.AutoEllipsis = true;
            this.openOpticalProbeConfigButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.openOpticalProbeConfigButton.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.openOpticalProbeConfigButton.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.openOpticalProbeConfigButton.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.openOpticalProbeConfigButton.ButtonCornorRadius = 3;
            this.openOpticalProbeConfigButton.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.openOpticalProbeConfigButton.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.openOpticalProbeConfigButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.openOpticalProbeConfigButton.IsButtonFoucs = false;
            this.openOpticalProbeConfigButton.Location = new System.Drawing.Point(505, 401);
            this.openOpticalProbeConfigButton.Name = "openOpticalProbeConfigButton";
            this.openOpticalProbeConfigButton.Size = new System.Drawing.Size(90, 30);
            this.openOpticalProbeConfigButton.TabIndex = 100;
            this.openOpticalProbeConfigButton.Text = "保存";
            this.openOpticalProbeConfigButton.Transparency = 50;
            this.openOpticalProbeConfigButton.UseVisualStyleBackColor = false;
            // 
            // groupBox_AutoBrightConfig
            // 
            this.groupBox_AutoBrightConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_AutoBrightConfig.Controls.Add(this.checkBox_IsGradualEnable);
            this.groupBox_AutoBrightConfig.Controls.Add(this.checkBox_IsSmartEnable);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_Notice);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_Unit);
            this.groupBox_AutoBrightConfig.Controls.Add(this.numericUpDown_ReadLuxCnt);
            this.groupBox_AutoBrightConfig.Controls.Add(this.numericUpDown_AutoBrightPeriod);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_ReadLuxCnt);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_AutoBrightPeriod);
            this.groupBox_AutoBrightConfig.Controls.Add(this.seperatePanel_AutoBrightInfo);
            this.groupBox_AutoBrightConfig.Location = new System.Drawing.Point(3, 3);
            this.groupBox_AutoBrightConfig.Name = "groupBox_AutoBrightConfig";
            this.groupBox_AutoBrightConfig.Size = new System.Drawing.Size(626, 376);
            this.groupBox_AutoBrightConfig.TabIndex = 3;
            this.groupBox_AutoBrightConfig.TabStop = false;
            // 
            // checkBox_IsGradualEnable
            // 
            this.checkBox_IsGradualEnable.AutoEllipsis = true;
            this.checkBox_IsGradualEnable.Checked = true;
            this.checkBox_IsGradualEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_IsGradualEnable.Location = new System.Drawing.Point(16, 51);
            this.checkBox_IsGradualEnable.Name = "checkBox_IsGradualEnable";
            this.checkBox_IsGradualEnable.Size = new System.Drawing.Size(225, 19);
            this.checkBox_IsGradualEnable.TabIndex = 28;
            this.checkBox_IsGradualEnable.Text = "开户亮度渐变";
            this.checkBox_IsGradualEnable.UseVisualStyleBackColor = true;
            // 
            // checkBox_IsSmartEnable
            // 
            this.checkBox_IsSmartEnable.AutoEllipsis = true;
            this.checkBox_IsSmartEnable.Checked = true;
            this.checkBox_IsSmartEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_IsSmartEnable.Location = new System.Drawing.Point(16, 20);
            this.checkBox_IsSmartEnable.Name = "checkBox_IsSmartEnable";
            this.checkBox_IsSmartEnable.Size = new System.Drawing.Size(225, 19);
            this.checkBox_IsSmartEnable.TabIndex = 28;
            this.checkBox_IsSmartEnable.Text = "开启智能亮度";
            this.checkBox_IsSmartEnable.UseVisualStyleBackColor = true;
            // 
            // label_Notice
            // 
            this.label_Notice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Notice.BackColor = System.Drawing.Color.Transparent;
            this.label_Notice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Notice.ForeColor = System.Drawing.Color.Blue;
            this.label_Notice.Location = new System.Drawing.Point(15, 190);
            this.label_Notice.Name = "label_Notice";
            this.label_Notice.Size = new System.Drawing.Size(593, 73);
            this.label_Notice.TabIndex = 27;
            this.label_Notice.Text = "提示:自动亮度调节模式下，每次调节屏体亮度前均需要对读到的N次的光探头值去掉最大值和最小求平均值，然后根据环境亮度和屏体亮度组成的曲线调节屏体亮度!";
            // 
            // label_Unit
            // 
            this.label_Unit.AutoSize = true;
            this.label_Unit.BackColor = System.Drawing.Color.Transparent;
            this.label_Unit.Location = new System.Drawing.Point(245, 120);
            this.label_Unit.Name = "label_Unit";
            this.label_Unit.Size = new System.Drawing.Size(14, 13);
            this.label_Unit.TabIndex = 26;
            this.label_Unit.Text = "S";
            // 
            // numericUpDown_ReadLuxCnt
            // 
            this.numericUpDown_ReadLuxCnt.Location = new System.Drawing.Point(163, 145);
            this.numericUpDown_ReadLuxCnt.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_ReadLuxCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_ReadLuxCnt.Name = "numericUpDown_ReadLuxCnt";
            this.numericUpDown_ReadLuxCnt.Size = new System.Drawing.Size(78, 20);
            this.numericUpDown_ReadLuxCnt.TabIndex = 25;
            this.numericUpDown_ReadLuxCnt.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDown_AutoBrightPeriod
            // 
            this.numericUpDown_AutoBrightPeriod.Location = new System.Drawing.Point(163, 116);
            this.numericUpDown_AutoBrightPeriod.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.numericUpDown_AutoBrightPeriod.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_AutoBrightPeriod.Name = "numericUpDown_AutoBrightPeriod";
            this.numericUpDown_AutoBrightPeriod.Size = new System.Drawing.Size(78, 20);
            this.numericUpDown_AutoBrightPeriod.TabIndex = 25;
            this.numericUpDown_AutoBrightPeriod.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label_ReadLuxCnt
            // 
            this.label_ReadLuxCnt.AutoEllipsis = true;
            this.label_ReadLuxCnt.Location = new System.Drawing.Point(16, 146);
            this.label_ReadLuxCnt.Name = "label_ReadLuxCnt";
            this.label_ReadLuxCnt.Size = new System.Drawing.Size(141, 19);
            this.label_ReadLuxCnt.TabIndex = 24;
            this.label_ReadLuxCnt.Text = "读环境亮度的次数:";
            this.label_ReadLuxCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_AutoBrightPeriod
            // 
            this.label_AutoBrightPeriod.AutoEllipsis = true;
            this.label_AutoBrightPeriod.Location = new System.Drawing.Point(16, 115);
            this.label_AutoBrightPeriod.Name = "label_AutoBrightPeriod";
            this.label_AutoBrightPeriod.Size = new System.Drawing.Size(141, 19);
            this.label_AutoBrightPeriod.TabIndex = 24;
            this.label_AutoBrightPeriod.Text = "环境亮度检测周期:";
            this.label_AutoBrightPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // seperatePanel_AutoBrightInfo
            // 
            this.seperatePanel_AutoBrightInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_AutoBrightInfo.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_AutoBrightInfo.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_AutoBrightInfo.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_AutoBrightInfo.Location = new System.Drawing.Point(6, 86);
            this.seperatePanel_AutoBrightInfo.Name = "seperatePanel_AutoBrightInfo";
            this.seperatePanel_AutoBrightInfo.SeperateLineHeight = 2;
            this.seperatePanel_AutoBrightInfo.Size = new System.Drawing.Size(614, 19);
            this.seperatePanel_AutoBrightInfo.TabIndex = 23;
            this.seperatePanel_AutoBrightInfo.Text = "自动亮度调节信息";
            // 
            // brightnessConfigTabPage
            // 
            this.brightnessConfigTabPage.Controls.Add(this.uC_BrightnessConfig1);
            this.brightnessConfigTabPage.Location = new System.Drawing.Point(4, 22);
            this.brightnessConfigTabPage.Name = "brightnessConfigTabPage";
            this.brightnessConfigTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.brightnessConfigTabPage.Size = new System.Drawing.Size(632, 454);
            this.brightnessConfigTabPage.TabIndex = 1;
            this.brightnessConfigTabPage.Text = "亮度配置";
            this.brightnessConfigTabPage.UseVisualStyleBackColor = true;
            // 
            // uC_BrightnessConfig1
            // 
            this.uC_BrightnessConfig1.BackColor = System.Drawing.Color.AliceBlue;
            this.uC_BrightnessConfig1.ConfigState = Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig.LightSensorConfigState.OK_State;
            this.uC_BrightnessConfig1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BrightnessConfig1.HsLangTable = ((System.Collections.Hashtable)(resources.GetObject("uC_BrightnessConfig1.HsLangTable")));
            this.uC_BrightnessConfig1.Location = new System.Drawing.Point(3, 3);
            this.uC_BrightnessConfig1.Name = "uC_BrightnessConfig1";
            this.uC_BrightnessConfig1.ScreenSN = null;
            this.uC_BrightnessConfig1.Size = new System.Drawing.Size(626, 448);
            this.uC_BrightnessConfig1.TabIndex = 0;
            // 
            // UC_BrightnessAllConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UC_BrightnessAllConfig";
            this.Size = new System.Drawing.Size(640, 480);
            this.panel_ConfigBase.ResumeLayout(false);
            this.brightnessSettingTabControl.ResumeLayout(false);
            this.globSettingTabPage.ResumeLayout(false);
            this.groupBox_AutoBrightConfig.ResumeLayout(false);
            this.groupBox_AutoBrightConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ReadLuxCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoBrightPeriod)).EndInit();
            this.brightnessConfigTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl brightnessSettingTabControl;
        private System.Windows.Forms.TabPage globSettingTabPage;
        private System.Windows.Forms.TabPage brightnessConfigTabPage;
        private System.Windows.Forms.GroupBox groupBox_AutoBrightConfig;
        private System.Windows.Forms.CheckBox checkBox_IsGradualEnable;
        private System.Windows.Forms.CheckBox checkBox_IsSmartEnable;
        private System.Windows.Forms.Label label_Notice;
        private System.Windows.Forms.Label label_Unit;
        private System.Windows.Forms.NumericUpDown numericUpDown_ReadLuxCnt;
        private System.Windows.Forms.NumericUpDown numericUpDown_AutoBrightPeriod;
        private System.Windows.Forms.Label label_ReadLuxCnt;
        private System.Windows.Forms.Label label_AutoBrightPeriod;
        private Nova.Control.Panel.SeperatePanel seperatePanel_AutoBrightInfo;
        private MonitorFromDisplay.UC_BrightnessConfig uC_BrightnessConfig1;
        public Nova.Control.CrystalButton openOpticalProbeConfigButton;
    }
}
