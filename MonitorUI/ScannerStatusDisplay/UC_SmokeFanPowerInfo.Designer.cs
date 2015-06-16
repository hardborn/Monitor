namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class UC_SmokeFanPowerInfo
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
            this.label_TotalCountValue = new System.Windows.Forms.Label();
            this.label_SBTotalCount = new System.Windows.Forms.Label();
            this.seperatePanel_FaultAlarmInfo = new Nova.Control.Panel.SeperatePanel();
            this.label_AlarmCnt = new System.Windows.Forms.Label();
            this.label_AlrmCount = new System.Windows.Forms.Label();
            this.seperatePanel_StatisticsInfo = new Nova.Control.Panel.SeperatePanel();
            this.SuspendLayout();
            // 
            // label_TotalCountValue
            // 
            this.label_TotalCountValue.AutoSize = true;
            this.label_TotalCountValue.Location = new System.Drawing.Point(185, 25);
            this.label_TotalCountValue.Name = "label_TotalCountValue";
            this.label_TotalCountValue.Size = new System.Drawing.Size(11, 12);
            this.label_TotalCountValue.TabIndex = 16;
            this.label_TotalCountValue.Text = "0";
            // 
            // label_SBTotalCount
            // 
            this.label_SBTotalCount.AutoEllipsis = true;
            this.label_SBTotalCount.Location = new System.Drawing.Point(1, 26);
            this.label_SBTotalCount.Name = "label_SBTotalCount";
            this.label_SBTotalCount.Size = new System.Drawing.Size(182, 19);
            this.label_SBTotalCount.TabIndex = 13;
            this.label_SBTotalCount.Text = "接收卡总数量:";
            this.label_SBTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // seperatePanel_FaultAlarmInfo
            // 
            this.seperatePanel_FaultAlarmInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_FaultAlarmInfo.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_FaultAlarmInfo.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_FaultAlarmInfo.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_FaultAlarmInfo.Location = new System.Drawing.Point(0, 50);
            this.seperatePanel_FaultAlarmInfo.Name = "seperatePanel_FaultAlarmInfo";
            this.seperatePanel_FaultAlarmInfo.SeperateLineHeight = 2;
            this.seperatePanel_FaultAlarmInfo.Size = new System.Drawing.Size(537, 19);
            this.seperatePanel_FaultAlarmInfo.TabIndex = 29;
            this.seperatePanel_FaultAlarmInfo.Text = "告警信息";
            // 
            // label_AlarmCnt
            // 
            this.label_AlarmCnt.AutoSize = true;
            this.label_AlarmCnt.Location = new System.Drawing.Point(188, 76);
            this.label_AlarmCnt.Name = "label_AlarmCnt";
            this.label_AlarmCnt.Size = new System.Drawing.Size(11, 12);
            this.label_AlarmCnt.TabIndex = 34;
            this.label_AlarmCnt.Text = "0";
            // 
            // label_AlrmCount
            // 
            this.label_AlrmCount.AutoEllipsis = true;
            this.label_AlrmCount.Location = new System.Drawing.Point(0, 75);
            this.label_AlrmCount.Name = "label_AlrmCount";
            this.label_AlrmCount.Size = new System.Drawing.Size(182, 19);
            this.label_AlrmCount.TabIndex = 33;
            this.label_AlrmCount.Text = "告警的接收卡数量:";
            this.label_AlrmCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // seperatePanel_StatisticsInfo
            // 
            this.seperatePanel_StatisticsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_StatisticsInfo.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_StatisticsInfo.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_StatisticsInfo.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_StatisticsInfo.Location = new System.Drawing.Point(3, 1);
            this.seperatePanel_StatisticsInfo.Name = "seperatePanel_StatisticsInfo";
            this.seperatePanel_StatisticsInfo.SeperateLineHeight = 2;
            this.seperatePanel_StatisticsInfo.Size = new System.Drawing.Size(531, 19);
            this.seperatePanel_StatisticsInfo.TabIndex = 35;
            this.seperatePanel_StatisticsInfo.Text = "统计信息";
            // 
            // UC_SmokeFanPowerInfo
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.seperatePanel_StatisticsInfo);
            this.Controls.Add(this.label_AlarmCnt);
            this.Controls.Add(this.label_AlrmCount);
            this.Controls.Add(this.seperatePanel_FaultAlarmInfo);
            this.Controls.Add(this.label_TotalCountValue);
            this.Controls.Add(this.label_SBTotalCount);
            this.Name = "UC_SmokeFanPowerInfo";
            this.Size = new System.Drawing.Size(537, 99);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_TotalCountValue;
        private System.Windows.Forms.Label label_SBTotalCount;
        private Nova.Control.Panel.SeperatePanel seperatePanel_FaultAlarmInfo;
        private System.Windows.Forms.Label label_AlarmCnt;
        private System.Windows.Forms.Label label_AlrmCount;
        private Nova.Control.Panel.SeperatePanel seperatePanel_StatisticsInfo;
    }
}
