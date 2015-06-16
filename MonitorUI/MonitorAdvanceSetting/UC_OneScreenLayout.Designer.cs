namespace Nova.Monitoring.UI.MonitorSetting
{
    partial class UC_OneScreenLayout
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
            this.doubleBufferTableLayoutPanel1 = new Nova.Control.DoubleBufferControl.DoubleBufferTableLayoutPanel();
            this.doubleBufferPanel_SettingZoon = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.doubleBufferPanel_ButtonZoon = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.groupBox_ScalingRate = new System.Windows.Forms.GroupBox();
            this.label_Value = new System.Windows.Forms.Label();
            this.vScrollBar_PixelLength = new System.Windows.Forms.VScrollBar();
            this.crystalButton_SetSelect = new Nova.Control.CrystalButton();
            this.crystalButton_Resume = new Nova.Control.CrystalButton();
            this.contextMenuStrip_Set = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Set = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleBufferTableLayoutPanel1.SuspendLayout();
            this.doubleBufferPanel_ButtonZoon.SuspendLayout();
            this.groupBox_ScalingRate.SuspendLayout();
            this.contextMenuStrip_Set.SuspendLayout();
            this.SuspendLayout();
            // 
            // doubleBufferTableLayoutPanel1
            // 
            this.doubleBufferTableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.doubleBufferTableLayoutPanel1.ColumnCount = 2;
            this.doubleBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.doubleBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.doubleBufferTableLayoutPanel1.Controls.Add(this.doubleBufferPanel_SettingZoon, 0, 0);
            this.doubleBufferTableLayoutPanel1.Controls.Add(this.doubleBufferPanel_ButtonZoon, 1, 0);
            this.doubleBufferTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.doubleBufferTableLayoutPanel1.Name = "doubleBufferTableLayoutPanel1";
            this.doubleBufferTableLayoutPanel1.RowCount = 1;
            this.doubleBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.doubleBufferTableLayoutPanel1.Size = new System.Drawing.Size(631, 490);
            this.doubleBufferTableLayoutPanel1.TabIndex = 0;
            // 
            // doubleBufferPanel_SettingZoon
            // 
            this.doubleBufferPanel_SettingZoon.BackColor = System.Drawing.Color.Transparent;
            this.doubleBufferPanel_SettingZoon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferPanel_SettingZoon.Location = new System.Drawing.Point(3, 3);
            this.doubleBufferPanel_SettingZoon.Name = "doubleBufferPanel_SettingZoon";
            this.doubleBufferPanel_SettingZoon.Size = new System.Drawing.Size(530, 484);
            this.doubleBufferPanel_SettingZoon.TabIndex = 0;
            // 
            // doubleBufferPanel_ButtonZoon
            // 
            this.doubleBufferPanel_ButtonZoon.Controls.Add(this.groupBox_ScalingRate);
            this.doubleBufferPanel_ButtonZoon.Controls.Add(this.crystalButton_SetSelect);
            this.doubleBufferPanel_ButtonZoon.Controls.Add(this.crystalButton_Resume);
            this.doubleBufferPanel_ButtonZoon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferPanel_ButtonZoon.Location = new System.Drawing.Point(539, 3);
            this.doubleBufferPanel_ButtonZoon.Name = "doubleBufferPanel_ButtonZoon";
            this.doubleBufferPanel_ButtonZoon.Size = new System.Drawing.Size(89, 484);
            this.doubleBufferPanel_ButtonZoon.TabIndex = 1;
            // 
            // groupBox_ScalingRate
            // 
            this.groupBox_ScalingRate.Controls.Add(this.label_Value);
            this.groupBox_ScalingRate.Controls.Add(this.vScrollBar_PixelLength);
            this.groupBox_ScalingRate.ForeColor = System.Drawing.Color.Black;
            this.groupBox_ScalingRate.Location = new System.Drawing.Point(2, 3);
            this.groupBox_ScalingRate.Name = "groupBox_ScalingRate";
            this.groupBox_ScalingRate.Size = new System.Drawing.Size(82, 134);
            this.groupBox_ScalingRate.TabIndex = 0;
            this.groupBox_ScalingRate.TabStop = false;
            this.groupBox_ScalingRate.Text = "缩放率";
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Location = new System.Drawing.Point(41, 114);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(23, 12);
            this.label_Value.TabIndex = 1;
            this.label_Value.Text = "0.4";
            // 
            // vScrollBar_PixelLength
            // 
            this.vScrollBar_PixelLength.LargeChange = 1;
            this.vScrollBar_PixelLength.Location = new System.Drawing.Point(14, 26);
            this.vScrollBar_PixelLength.Maximum = 400;
            this.vScrollBar_PixelLength.Minimum = 10;
            this.vScrollBar_PixelLength.Name = "vScrollBar_PixelLength";
            this.vScrollBar_PixelLength.Size = new System.Drawing.Size(20, 100);
            this.vScrollBar_PixelLength.TabIndex = 0;
            this.vScrollBar_PixelLength.TabStop = true;
            this.vScrollBar_PixelLength.Value = 40;
            this.vScrollBar_PixelLength.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_PixelLength_Scroll);
            // 
            // crystalButton_SetSelect
            // 
            this.crystalButton_SetSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_SetSelect.AutoEllipsis = true;
            this.crystalButton_SetSelect.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_SetSelect.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_SetSelect.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_SetSelect.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_SetSelect.ButtonCornorRadius = 3;
            this.crystalButton_SetSelect.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_SetSelect.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_SetSelect.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_SetSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_SetSelect.IsButtonFoucs = false;
            this.crystalButton_SetSelect.Location = new System.Drawing.Point(2, 407);
            this.crystalButton_SetSelect.Name = "crystalButton_SetSelect";
            this.crystalButton_SetSelect.Size = new System.Drawing.Size(85, 30);
            this.crystalButton_SetSelect.TabIndex = 1;
            this.crystalButton_SetSelect.Text = "设置";
            this.crystalButton_SetSelect.Transparency = 50;
            this.crystalButton_SetSelect.UseVisualStyleBackColor = false;
            this.crystalButton_SetSelect.Click += new System.EventHandler(this.crystalButton_SetSelect_Click);
            // 
            // crystalButton_Resume
            // 
            this.crystalButton_Resume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_Resume.AutoEllipsis = true;
            this.crystalButton_Resume.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_Resume.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Resume.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Resume.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_Resume.ButtonCornorRadius = 3;
            this.crystalButton_Resume.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Resume.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Resume.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_Resume.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_Resume.IsButtonFoucs = false;
            this.crystalButton_Resume.Location = new System.Drawing.Point(2, 448);
            this.crystalButton_Resume.Name = "crystalButton_Resume";
            this.crystalButton_Resume.Size = new System.Drawing.Size(85, 30);
            this.crystalButton_Resume.TabIndex = 2;
            this.crystalButton_Resume.Text = "恢复默认值";
            this.crystalButton_Resume.Transparency = 50;
            this.crystalButton_Resume.UseVisualStyleBackColor = false;
            this.crystalButton_Resume.Click += new System.EventHandler(this.crystalButton_Resume_Click);
            // 
            // contextMenuStrip_Set
            // 
            this.contextMenuStrip_Set.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Set});
            this.contextMenuStrip_Set.Name = "contextMenuStrip_Set";
            this.contextMenuStrip_Set.Size = new System.Drawing.Size(101, 26);
            // 
            // ToolStripMenuItem_Set
            // 
            this.ToolStripMenuItem_Set.Name = "ToolStripMenuItem_Set";
            this.ToolStripMenuItem_Set.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem_Set.Text = "设置";
            this.ToolStripMenuItem_Set.Click += new System.EventHandler(this.ToolStripMenuItem_Set_Click);
            // 
            // UC_OneScreenLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.doubleBufferTableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "UC_OneScreenLayout";
            this.Size = new System.Drawing.Size(631, 490);
            this.doubleBufferTableLayoutPanel1.ResumeLayout(false);
            this.doubleBufferPanel_ButtonZoon.ResumeLayout(false);
            this.groupBox_ScalingRate.ResumeLayout(false);
            this.groupBox_ScalingRate.PerformLayout();
            this.contextMenuStrip_Set.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.DoubleBufferControl.DoubleBufferTableLayoutPanel doubleBufferTableLayoutPanel1;
        private Nova.Control.DoubleBufferControl.DoubleBufferPanel doubleBufferPanel_SettingZoon;
        private Nova.Control.DoubleBufferControl.DoubleBufferPanel doubleBufferPanel_ButtonZoon;
        private Nova.Control.CrystalButton crystalButton_SetSelect;
        private Nova.Control.CrystalButton crystalButton_Resume;
        private System.Windows.Forms.GroupBox groupBox_ScalingRate;
        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.VScrollBar vScrollBar_PixelLength;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Set;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Set;


    }
}
