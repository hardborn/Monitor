namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_Brightness
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
            this.panel_ConfigBase.SuspendLayout();
            this.groupBox_AutoBrightConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ReadLuxCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoBrightPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.groupBox_AutoBrightConfig);
            this.panel_ConfigBase.Size = new System.Drawing.Size(473, 387);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Location = new System.Drawing.Point(295, 406);
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // groupBox_AutoBrightConfig
            // 
            this.groupBox_AutoBrightConfig.Controls.Add(this.checkBox_IsGradualEnable);
            this.groupBox_AutoBrightConfig.Controls.Add(this.checkBox_IsSmartEnable);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_Notice);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_Unit);
            this.groupBox_AutoBrightConfig.Controls.Add(this.numericUpDown_ReadLuxCnt);
            this.groupBox_AutoBrightConfig.Controls.Add(this.numericUpDown_AutoBrightPeriod);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_ReadLuxCnt);
            this.groupBox_AutoBrightConfig.Controls.Add(this.label_AutoBrightPeriod);
            this.groupBox_AutoBrightConfig.Controls.Add(this.seperatePanel_AutoBrightInfo);
            this.groupBox_AutoBrightConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_AutoBrightConfig.Location = new System.Drawing.Point(0, 0);
            this.groupBox_AutoBrightConfig.Name = "groupBox_AutoBrightConfig";
            this.groupBox_AutoBrightConfig.Size = new System.Drawing.Size(473, 387);
            this.groupBox_AutoBrightConfig.TabIndex = 2;
            this.groupBox_AutoBrightConfig.TabStop = false;
            // 
            // checkBox_IsGradualEnable
            // 
            this.checkBox_IsGradualEnable.AutoEllipsis = true;
            this.checkBox_IsGradualEnable.Checked = true;
            this.checkBox_IsGradualEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_IsGradualEnable.Location = new System.Drawing.Point(16, 33);
            this.checkBox_IsGradualEnable.Name = "checkBox_IsGradualEnable";
            this.checkBox_IsGradualEnable.Size = new System.Drawing.Size(225, 20);
            this.checkBox_IsGradualEnable.TabIndex = 28;
            this.checkBox_IsGradualEnable.Text = "开户亮度渐变";
            this.checkBox_IsGradualEnable.UseVisualStyleBackColor = true;
            // 
            // checkBox_IsSmartEnable
            // 
            this.checkBox_IsSmartEnable.AutoEllipsis = true;
            this.checkBox_IsSmartEnable.Checked = true;
            this.checkBox_IsSmartEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_IsSmartEnable.Location = new System.Drawing.Point(247, 20);
            this.checkBox_IsSmartEnable.Name = "checkBox_IsSmartEnable";
            this.checkBox_IsSmartEnable.Size = new System.Drawing.Size(225, 19);
            this.checkBox_IsSmartEnable.TabIndex = 28;
            this.checkBox_IsSmartEnable.Text = "开启智能亮度";
            this.checkBox_IsSmartEnable.UseVisualStyleBackColor = true;
            this.checkBox_IsSmartEnable.Visible = false;
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
            this.label_Notice.Size = new System.Drawing.Size(440, 108);
            this.label_Notice.TabIndex = 27;
            this.label_Notice.Text = "提示:自动亮度调节模式下，每次调节屏体亮度前均需要对读到的N次的光探头值去掉最大值和最小求平均值，然后根据环境亮度和屏体亮度组成的曲线调节屏体亮度!";
            // 
            // label_Unit
            // 
            this.label_Unit.AutoSize = true;
            this.label_Unit.BackColor = System.Drawing.Color.Transparent;
            this.label_Unit.Location = new System.Drawing.Point(245, 116);
            this.label_Unit.Name = "label_Unit";
            this.label_Unit.Size = new System.Drawing.Size(11, 12);
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
            this.numericUpDown_ReadLuxCnt.Size = new System.Drawing.Size(78, 21);
            this.numericUpDown_ReadLuxCnt.TabIndex = 25;
            this.numericUpDown_ReadLuxCnt.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDown_AutoBrightPeriod
            // 
            this.numericUpDown_AutoBrightPeriod.Location = new System.Drawing.Point(163, 112);
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
            this.numericUpDown_AutoBrightPeriod.Size = new System.Drawing.Size(78, 21);
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
            this.label_ReadLuxCnt.Size = new System.Drawing.Size(141, 20);
            this.label_ReadLuxCnt.TabIndex = 24;
            this.label_ReadLuxCnt.Text = "读环境亮度的次数:";
            this.label_ReadLuxCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_AutoBrightPeriod
            // 
            this.label_AutoBrightPeriod.AutoEllipsis = true;
            this.label_AutoBrightPeriod.Location = new System.Drawing.Point(16, 111);
            this.label_AutoBrightPeriod.Name = "label_AutoBrightPeriod";
            this.label_AutoBrightPeriod.Size = new System.Drawing.Size(141, 20);
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
            this.seperatePanel_AutoBrightInfo.Location = new System.Drawing.Point(6, 77);
            this.seperatePanel_AutoBrightInfo.Name = "seperatePanel_AutoBrightInfo";
            this.seperatePanel_AutoBrightInfo.SeperateLineHeight = 2;
            this.seperatePanel_AutoBrightInfo.Size = new System.Drawing.Size(461, 19);
            this.seperatePanel_AutoBrightInfo.TabIndex = 23;
            this.seperatePanel_AutoBrightInfo.Text = "自动亮度调节信息";
            // 
            // UC_Brightness
            // 
            this.Name = "UC_Brightness";
            this.panel_ConfigBase.ResumeLayout(false);
            this.groupBox_AutoBrightConfig.ResumeLayout(false);
            this.groupBox_AutoBrightConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ReadLuxCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoBrightPeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_AutoBrightConfig;
        private Nova.Control.Panel.SeperatePanel seperatePanel_AutoBrightInfo;
        private System.Windows.Forms.Label label_ReadLuxCnt;
        private System.Windows.Forms.Label label_AutoBrightPeriod;
        private System.Windows.Forms.Label label_Unit;
        private System.Windows.Forms.NumericUpDown numericUpDown_ReadLuxCnt;
        private System.Windows.Forms.NumericUpDown numericUpDown_AutoBrightPeriod;
        private System.Windows.Forms.Label label_Notice;
        private System.Windows.Forms.CheckBox checkBox_IsGradualEnable;
        private System.Windows.Forms.CheckBox checkBox_IsSmartEnable;
    }
}
