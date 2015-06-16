namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config
{
    partial class UC_ScreenSummary
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
            this.screenLabel = new System.Windows.Forms.Label();
            this.screenNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // screenLabel
            // 
            this.screenLabel.AutoEllipsis = true;
            this.screenLabel.AutoSize = true;
            this.screenLabel.Location = new System.Drawing.Point(41, 10);
            this.screenLabel.Name = "screenLabel";
            this.screenLabel.Size = new System.Drawing.Size(31, 13);
            this.screenLabel.TabIndex = 0;
            this.screenLabel.Text = "屏体";
            // 
            // screenNameTextBox
            // 
            this.screenNameTextBox.Location = new System.Drawing.Point(92, 5);
            this.screenNameTextBox.MaxLength = 20;
            this.screenNameTextBox.Name = "screenNameTextBox";
            this.screenNameTextBox.Size = new System.Drawing.Size(197, 20);
            this.screenNameTextBox.TabIndex = 1;
            this.screenNameTextBox.TextChanged += new System.EventHandler(this.screenNameTextBox_TextChanged);
            // 
            // UC_ScreenSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.screenNameTextBox);
            this.Controls.Add(this.screenLabel);
            this.Name = "UC_ScreenSummary";
            this.Size = new System.Drawing.Size(374, 36);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label screenLabel;
        private System.Windows.Forms.TextBox screenNameTextBox;
    }
}
