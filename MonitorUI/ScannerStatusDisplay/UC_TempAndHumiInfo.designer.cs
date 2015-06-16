namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class UC_TempAndHumiInfo
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
            this.label_BeyondAverageCount = new System.Windows.Forms.Label();
            this.label_BeyondAverage = new System.Windows.Forms.Label();
            this.label_AverageValue = new System.Windows.Forms.Label();
            this.label_Average = new System.Windows.Forms.Label();
            this.label_Max = new System.Windows.Forms.Label();
            this.label_Min = new System.Windows.Forms.Label();
            this.seperatePanel_StatisticsInfo = new Nova.Control.Panel.SeperatePanel();
            this.seperatePanel_FaultAlarmInfo = new Nova.Control.Panel.SeperatePanel();
            this.label_TempAlarmCnt = new System.Windows.Forms.Label();
            this.label_AlrmInfo = new System.Windows.Forms.Label();
            this.linkLabel_MinValue = new System.Windows.Forms.LinkLabel();
            this.linkLabel_MaxValue = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label_BeyondAverageCount
            // 
            this.label_BeyondAverageCount.AutoSize = true;
            this.label_BeyondAverageCount.Location = new System.Drawing.Point(565, 47);
            this.label_BeyondAverageCount.Name = "label_BeyondAverageCount";
            this.label_BeyondAverageCount.Size = new System.Drawing.Size(11, 12);
            this.label_BeyondAverageCount.TabIndex = 26;
            this.label_BeyondAverageCount.Text = "0";
            // 
            // label_BeyondAverage
            // 
            this.label_BeyondAverage.AutoEllipsis = true;
            this.label_BeyondAverage.Location = new System.Drawing.Point(305, 42);
            this.label_BeyondAverage.Name = "label_BeyondAverage";
            this.label_BeyondAverage.Size = new System.Drawing.Size(254, 19);
            this.label_BeyondAverage.TabIndex = 25;
            this.label_BeyondAverage.Text = "过平均值的接收卡数量:";
            this.label_BeyondAverage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_AverageValue
            // 
            this.label_AverageValue.AutoSize = true;
            this.label_AverageValue.Location = new System.Drawing.Point(239, 47);
            this.label_AverageValue.Name = "label_AverageValue";
            this.label_AverageValue.Size = new System.Drawing.Size(11, 12);
            this.label_AverageValue.TabIndex = 24;
            this.label_AverageValue.Text = "0";
            // 
            // label_Average
            // 
            this.label_Average.AutoEllipsis = true;
            this.label_Average.Location = new System.Drawing.Point(0, 47);
            this.label_Average.Name = "label_Average";
            this.label_Average.Size = new System.Drawing.Size(232, 19);
            this.label_Average.TabIndex = 23;
            this.label_Average.Text = "平均温度:";
            this.label_Average.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Max
            // 
            this.label_Max.AutoEllipsis = true;
            this.label_Max.Location = new System.Drawing.Point(305, 23);
            this.label_Max.Name = "label_Max";
            this.label_Max.Size = new System.Drawing.Size(254, 19);
            this.label_Max.TabIndex = 20;
            this.label_Max.Text = "最高温度:";
            this.label_Max.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Min
            // 
            this.label_Min.AutoEllipsis = true;
            this.label_Min.Location = new System.Drawing.Point(1, 23);
            this.label_Min.Name = "label_Min";
            this.label_Min.Size = new System.Drawing.Size(232, 19);
            this.label_Min.TabIndex = 19;
            this.label_Min.Text = "最低温度:";
            this.label_Min.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // seperatePanel_StatisticsInfo
            // 
            this.seperatePanel_StatisticsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_StatisticsInfo.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_StatisticsInfo.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_StatisticsInfo.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_StatisticsInfo.Location = new System.Drawing.Point(3, -1);
            this.seperatePanel_StatisticsInfo.Name = "seperatePanel_StatisticsInfo";
            this.seperatePanel_StatisticsInfo.SeperateLineHeight = 2;
            this.seperatePanel_StatisticsInfo.Size = new System.Drawing.Size(634, 19);
            this.seperatePanel_StatisticsInfo.TabIndex = 27;
            this.seperatePanel_StatisticsInfo.Text = "统计信息";
            // 
            // seperatePanel_FaultAlarmInfo
            // 
            this.seperatePanel_FaultAlarmInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_FaultAlarmInfo.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_FaultAlarmInfo.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_FaultAlarmInfo.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_FaultAlarmInfo.Location = new System.Drawing.Point(0, 72);
            this.seperatePanel_FaultAlarmInfo.Name = "seperatePanel_FaultAlarmInfo";
            this.seperatePanel_FaultAlarmInfo.SeperateLineHeight = 2;
            this.seperatePanel_FaultAlarmInfo.Size = new System.Drawing.Size(637, 19);
            this.seperatePanel_FaultAlarmInfo.TabIndex = 28;
            this.seperatePanel_FaultAlarmInfo.Text = "告警信息";
            // 
            // label_TempAlarmCnt
            // 
            this.label_TempAlarmCnt.AutoSize = true;
            this.label_TempAlarmCnt.Location = new System.Drawing.Point(239, 94);
            this.label_TempAlarmCnt.Name = "label_TempAlarmCnt";
            this.label_TempAlarmCnt.Size = new System.Drawing.Size(11, 12);
            this.label_TempAlarmCnt.TabIndex = 32;
            this.label_TempAlarmCnt.Text = "0";
            // 
            // label_AlrmInfo
            // 
            this.label_AlrmInfo.AutoEllipsis = true;
            this.label_AlrmInfo.Location = new System.Drawing.Point(1, 94);
            this.label_AlrmInfo.Name = "label_AlrmInfo";
            this.label_AlrmInfo.Size = new System.Drawing.Size(231, 19);
            this.label_AlrmInfo.TabIndex = 31;
            this.label_AlrmInfo.Text = "温度告警的接收卡数量:";
            this.label_AlrmInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel_MinValue
            // 
            this.linkLabel_MinValue.AutoSize = true;
            this.linkLabel_MinValue.Location = new System.Drawing.Point(239, 26);
            this.linkLabel_MinValue.Name = "linkLabel_MinValue";
            this.linkLabel_MinValue.Size = new System.Drawing.Size(11, 12);
            this.linkLabel_MinValue.TabIndex = 33;
            this.linkLabel_MinValue.TabStop = true;
            this.linkLabel_MinValue.Text = "0";
            this.linkLabel_MinValue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_MinValue_LinkClicked);
            // 
            // linkLabel_MaxValue
            // 
            this.linkLabel_MaxValue.AutoSize = true;
            this.linkLabel_MaxValue.Location = new System.Drawing.Point(565, 26);
            this.linkLabel_MaxValue.Name = "linkLabel_MaxValue";
            this.linkLabel_MaxValue.Size = new System.Drawing.Size(11, 12);
            this.linkLabel_MaxValue.TabIndex = 34;
            this.linkLabel_MaxValue.TabStop = true;
            this.linkLabel_MaxValue.Text = "0";
            this.linkLabel_MaxValue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_MaxValue_LinkClicked);
            // 
            // UC_TempAndHumiInfo
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.linkLabel_MaxValue);
            this.Controls.Add(this.linkLabel_MinValue);
            this.Controls.Add(this.label_TempAlarmCnt);
            this.Controls.Add(this.label_AlrmInfo);
            this.Controls.Add(this.seperatePanel_FaultAlarmInfo);
            this.Controls.Add(this.seperatePanel_StatisticsInfo);
            this.Controls.Add(this.label_BeyondAverageCount);
            this.Controls.Add(this.label_BeyondAverage);
            this.Controls.Add(this.label_AverageValue);
            this.Controls.Add(this.label_Average);
            this.Controls.Add(this.label_Max);
            this.Controls.Add(this.label_Min);
            this.Name = "UC_TempAndHumiInfo";
            this.Size = new System.Drawing.Size(637, 120);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_BeyondAverageCount;
        private System.Windows.Forms.Label label_BeyondAverage;
        private System.Windows.Forms.Label label_AverageValue;
        private System.Windows.Forms.Label label_Average;
        private System.Windows.Forms.Label label_Max;
        private System.Windows.Forms.Label label_Min;
        private Nova.Control.Panel.SeperatePanel seperatePanel_StatisticsInfo;
        private Nova.Control.Panel.SeperatePanel seperatePanel_FaultAlarmInfo;
        private System.Windows.Forms.Label label_TempAlarmCnt;
        private System.Windows.Forms.Label label_AlrmInfo;
        private System.Windows.Forms.LinkLabel linkLabel_MinValue;
        private System.Windows.Forms.LinkLabel linkLabel_MaxValue;
    }
}
