namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_PowerManager
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
            this.label_Still = new System.Windows.Forms.Label();
            this.label_Close = new System.Windows.Forms.Label();
            this.label_Open = new System.Windows.Forms.Label();
            this.panel_Switch = new System.Windows.Forms.Panel();
            this.panel_PowerManager = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel_Switch.SuspendLayout();
            this.panel_PowerManager.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Size = new System.Drawing.Size(456, 388);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Location = new System.Drawing.Point(368, 410);
            // 
            // label_Still
            // 
            this.label_Still.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Still.Location = new System.Drawing.Point(180, 9);
            this.label_Still.Name = "label_Still";
            this.label_Still.Size = new System.Drawing.Size(75, 20);
            this.label_Still.TabIndex = 13;
            this.label_Still.Text = "保持";
            this.label_Still.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Close
            // 
            this.label_Close.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Close.Location = new System.Drawing.Point(90, 9);
            this.label_Close.Name = "label_Close";
            this.label_Close.Size = new System.Drawing.Size(75, 20);
            this.label_Close.TabIndex = 12;
            this.label_Close.Text = "关";
            this.label_Close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Open
            // 
            this.label_Open.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Open.Location = new System.Drawing.Point(-3, 9);
            this.label_Open.Name = "label_Open";
            this.label_Open.Size = new System.Drawing.Size(75, 20);
            this.label_Open.TabIndex = 11;
            this.label_Open.Text = "开";
            this.label_Open.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Switch
            // 
            this.panel_Switch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Switch.Controls.Add(this.label_Open);
            this.panel_Switch.Controls.Add(this.label_Close);
            this.panel_Switch.Controls.Add(this.label_Still);
            this.panel_Switch.Location = new System.Drawing.Point(221, 3);
            this.panel_Switch.Name = "panel_Switch";
            this.panel_Switch.Size = new System.Drawing.Size(265, 37);
            this.panel_Switch.TabIndex = 39;
            // 
            // panel_PowerManager
            // 
            this.panel_PowerManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_PowerManager.Controls.Add(this.panel1);
            this.panel_PowerManager.Location = new System.Drawing.Point(3, 46);
            this.panel_PowerManager.Name = "panel_PowerManager";
            this.panel_PowerManager.Size = new System.Drawing.Size(504, 202);
            this.panel_PowerManager.TabIndex = 40;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 32);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Location = new System.Drawing.Point(31, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButton3
            // 
            this.radioButton3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(401, 10);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(14, 13);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(309, 10);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(217, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // UC_PowerManager
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.panel_PowerManager);
            this.Controls.Add(this.panel_Switch);
            this.Name = "UC_PowerManager";
            this.Size = new System.Drawing.Size(492, 240);
            this.Controls.SetChildIndex(this.panel_ConfigBase, 0);
            this.Controls.SetChildIndex(this.panel_Switch, 0);
            this.Controls.SetChildIndex(this.panel_PowerManager, 0);
            this.Controls.SetChildIndex(this.crystalButton_OK, 0);
            this.panel_Switch.ResumeLayout(false);
            this.panel_PowerManager.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Still;
        private System.Windows.Forms.Label label_Close;
        private System.Windows.Forms.Label label_Open;
        private System.Windows.Forms.Panel panel_Switch;
        private System.Windows.Forms.Panel panel_PowerManager;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}
