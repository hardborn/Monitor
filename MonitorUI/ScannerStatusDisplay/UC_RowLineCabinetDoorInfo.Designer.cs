namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class UC_RowLineCabinetDoorInfo
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
            this.seperatePanel_StatisticsInfo = new Nova.Control.Panel.SeperatePanel();
            this.label_SBTotalCount = new System.Windows.Forms.Label();
            this.label_ErrorSBCount = new System.Windows.Forms.Label();
            this.label_ErrorCountValue = new System.Windows.Forms.Label();
            this.label_TotalCountValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // seperatePanel_StatisticsInfo
            // 
            this.seperatePanel_StatisticsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_StatisticsInfo.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_StatisticsInfo.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_StatisticsInfo.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_StatisticsInfo.Location = new System.Drawing.Point(3, 3);
            this.seperatePanel_StatisticsInfo.Name = "seperatePanel_StatisticsInfo";
            this.seperatePanel_StatisticsInfo.SeperateLineHeight = 2;
            this.seperatePanel_StatisticsInfo.Size = new System.Drawing.Size(554, 19);
            this.seperatePanel_StatisticsInfo.TabIndex = 51;
            this.seperatePanel_StatisticsInfo.Text = "统计信息";
            // 
            // label_SBTotalCount
            // 
            this.label_SBTotalCount.AutoEllipsis = true;
            this.label_SBTotalCount.Location = new System.Drawing.Point(2, 31);
            this.label_SBTotalCount.Name = "label_SBTotalCount";
            this.label_SBTotalCount.Size = new System.Drawing.Size(178, 19);
            this.label_SBTotalCount.TabIndex = 47;
            this.label_SBTotalCount.Text = "接收卡总数量:";
            this.label_SBTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ErrorSBCount
            // 
            this.label_ErrorSBCount.AutoEllipsis = true;
            this.label_ErrorSBCount.Location = new System.Drawing.Point(290, 31);
            this.label_ErrorSBCount.Name = "label_ErrorSBCount";
            this.label_ErrorSBCount.Size = new System.Drawing.Size(178, 19);
            this.label_ErrorSBCount.TabIndex = 48;
            this.label_ErrorSBCount.Text = "故障的接收卡数量:";
            this.label_ErrorSBCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ErrorCountValue
            // 
            this.label_ErrorCountValue.AutoSize = true;
            this.label_ErrorCountValue.Location = new System.Drawing.Point(474, 34);
            this.label_ErrorCountValue.Name = "label_ErrorCountValue";
            this.label_ErrorCountValue.Size = new System.Drawing.Size(11, 12);
            this.label_ErrorCountValue.TabIndex = 49;
            this.label_ErrorCountValue.Text = "0";
            // 
            // label_TotalCountValue
            // 
            this.label_TotalCountValue.AutoSize = true;
            this.label_TotalCountValue.Location = new System.Drawing.Point(186, 34);
            this.label_TotalCountValue.Name = "label_TotalCountValue";
            this.label_TotalCountValue.Size = new System.Drawing.Size(11, 12);
            this.label_TotalCountValue.TabIndex = 50;
            this.label_TotalCountValue.Text = "0";
            // 
            // UC_RowLineCabinetDoorInfo
            // 
            this.Controls.Add(this.seperatePanel_StatisticsInfo);
            this.Controls.Add(this.label_TotalCountValue);
            this.Controls.Add(this.label_ErrorCountValue);
            this.Controls.Add(this.label_ErrorSBCount);
            this.Controls.Add(this.label_SBTotalCount);
            this.Name = "UC_RowLineCabinetDoorInfo";
            this.Size = new System.Drawing.Size(557, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Nova.Control.Panel.SeperatePanel seperatePanel_StatisticsInfo;
        private System.Windows.Forms.Label label_SBTotalCount;
        private System.Windows.Forms.Label label_ErrorSBCount;
        private System.Windows.Forms.Label label_ErrorCountValue;
        private System.Windows.Forms.Label label_TotalCountValue;
    }
}
