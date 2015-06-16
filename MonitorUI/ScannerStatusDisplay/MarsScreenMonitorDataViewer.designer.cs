namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class MarsScreenMonitorDataViewer
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
            this.tableLayoutPanel_TemperatureLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_Display = new System.Windows.Forms.Panel();
            this.panel_Info = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_ColorMotice = new System.Windows.Forms.Panel();
            this.groupBox_ScalingRate = new System.Windows.Forms.GroupBox();
            this.label_Value = new System.Windows.Forms.Label();
            this.vScrollBar_PixelLength = new System.Windows.Forms.VScrollBar();
            this.doubleBufferPanel1 = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.label_NoneUpdate = new System.Windows.Forms.Label();
            this.label_LastUpdateTimeValue = new System.Windows.Forms.Label();
            this.label_LastUpdateTime = new System.Windows.Forms.Label();
            this.tableLayoutPanel_TemperatureLayout.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox_ScalingRate.SuspendLayout();
            this.doubleBufferPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_TemperatureLayout
            // 
            this.tableLayoutPanel_TemperatureLayout.ColumnCount = 2;
            this.tableLayoutPanel_TemperatureLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_TemperatureLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel_TemperatureLayout.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel_TemperatureLayout.Controls.Add(this.panel_Info, 0, 2);
            this.tableLayoutPanel_TemperatureLayout.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel_TemperatureLayout.Controls.Add(this.doubleBufferPanel1, 0, 1);
            this.tableLayoutPanel_TemperatureLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_TemperatureLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_TemperatureLayout.Name = "tableLayoutPanel_TemperatureLayout";
            this.tableLayoutPanel_TemperatureLayout.RowCount = 3;
            this.tableLayoutPanel_TemperatureLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_TemperatureLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel_TemperatureLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel_TemperatureLayout.Size = new System.Drawing.Size(475, 350);
            this.tableLayoutPanel_TemperatureLayout.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel_Display);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(359, 197);
            this.panel2.TabIndex = 0;
            // 
            // panel_Display
            // 
            this.panel_Display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Display.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.panel_Display.Location = new System.Drawing.Point(10, 10);
            this.panel_Display.Name = "panel_Display";
            this.panel_Display.Size = new System.Drawing.Size(338, 178);
            this.panel_Display.TabIndex = 19;
            // 
            // panel_Info
            // 
            this.tableLayoutPanel_TemperatureLayout.SetColumnSpan(this.panel_Info, 2);
            this.panel_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Info.Location = new System.Drawing.Point(3, 235);
            this.panel_Info.Name = "panel_Info";
            this.panel_Info.Size = new System.Drawing.Size(469, 112);
            this.panel_Info.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel_ColorMotice);
            this.panel1.Controls.Add(this.groupBox_ScalingRate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(368, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel_TemperatureLayout.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(104, 226);
            this.panel1.TabIndex = 20;
            // 
            // panel_ColorMotice
            // 
            this.panel_ColorMotice.BackColor = System.Drawing.Color.Transparent;
            this.panel_ColorMotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ColorMotice.Location = new System.Drawing.Point(0, 115);
            this.panel_ColorMotice.Name = "panel_ColorMotice";
            this.panel_ColorMotice.Size = new System.Drawing.Size(104, 111);
            this.panel_ColorMotice.TabIndex = 17;
            // 
            // groupBox_ScalingRate
            // 
            this.groupBox_ScalingRate.Controls.Add(this.label_Value);
            this.groupBox_ScalingRate.Controls.Add(this.vScrollBar_PixelLength);
            this.groupBox_ScalingRate.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_ScalingRate.ForeColor = System.Drawing.Color.Black;
            this.groupBox_ScalingRate.Location = new System.Drawing.Point(0, 0);
            this.groupBox_ScalingRate.Name = "groupBox_ScalingRate";
            this.groupBox_ScalingRate.Size = new System.Drawing.Size(104, 115);
            this.groupBox_ScalingRate.TabIndex = 16;
            this.groupBox_ScalingRate.TabStop = false;
            this.groupBox_ScalingRate.Text = "缩放率";
            // 
            // label_Value
            // 
            this.label_Value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Value.AutoSize = true;
            this.label_Value.Location = new System.Drawing.Point(31, 99);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(11, 12);
            this.label_Value.TabIndex = 8;
            this.label_Value.Text = "1";
            // 
            // vScrollBar_PixelLength
            // 
            this.vScrollBar_PixelLength.LargeChange = 1;
            this.vScrollBar_PixelLength.Location = new System.Drawing.Point(4, 18);
            this.vScrollBar_PixelLength.Maximum = 800;
            this.vScrollBar_PixelLength.Minimum = 10;
            this.vScrollBar_PixelLength.Name = "vScrollBar_PixelLength";
            this.vScrollBar_PixelLength.Size = new System.Drawing.Size(20, 90);
            this.vScrollBar_PixelLength.TabIndex = 6;
            this.vScrollBar_PixelLength.Value = 10;
            this.vScrollBar_PixelLength.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_PixelLength_Scroll);
            // 
            // doubleBufferPanel1
            // 
            this.doubleBufferPanel1.Controls.Add(this.label_NoneUpdate);
            this.doubleBufferPanel1.Controls.Add(this.label_LastUpdateTimeValue);
            this.doubleBufferPanel1.Controls.Add(this.label_LastUpdateTime);
            this.doubleBufferPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferPanel1.Location = new System.Drawing.Point(3, 206);
            this.doubleBufferPanel1.Name = "doubleBufferPanel1";
            this.doubleBufferPanel1.Size = new System.Drawing.Size(359, 23);
            this.doubleBufferPanel1.TabIndex = 21;
            // 
            // label_NoneUpdate
            // 
            this.label_NoneUpdate.AutoSize = true;
            this.label_NoneUpdate.Location = new System.Drawing.Point(276, 7);
            this.label_NoneUpdate.Name = "label_NoneUpdate";
            this.label_NoneUpdate.Size = new System.Drawing.Size(11, 12);
            this.label_NoneUpdate.TabIndex = 2;
            this.label_NoneUpdate.Text = "0";
            // 
            // label_LastUpdateTimeValue
            // 
            this.label_LastUpdateTimeValue.AutoSize = true;
            this.label_LastUpdateTimeValue.Location = new System.Drawing.Point(276, 7);
            this.label_LastUpdateTimeValue.Name = "label_LastUpdateTimeValue";
            this.label_LastUpdateTimeValue.Size = new System.Drawing.Size(41, 12);
            this.label_LastUpdateTimeValue.TabIndex = 1;
            this.label_LastUpdateTimeValue.Text = "label1";
            // 
            // label_LastUpdateTime
            // 
            this.label_LastUpdateTime.AutoSize = true;
            this.label_LastUpdateTime.Location = new System.Drawing.Point(3, 7);
            this.label_LastUpdateTime.Name = "label_LastUpdateTime";
            this.label_LastUpdateTime.Size = new System.Drawing.Size(131, 12);
            this.label_LastUpdateTime.TabIndex = 0;
            this.label_LastUpdateTime.Text = "当前监控数据获取时间:";
            // 
            // MarsScreenMonitorDataViewer
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.tableLayoutPanel_TemperatureLayout);
            this.DoubleBuffered = true;
            this.Name = "MarsScreenMonitorDataViewer";
            this.Size = new System.Drawing.Size(475, 350);
            this.VisibleChanged += new System.EventHandler(this.MarsScreenMonitorDataViewer_VisibleChanged);
            this.tableLayoutPanel_TemperatureLayout.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox_ScalingRate.ResumeLayout(false);
            this.groupBox_ScalingRate.PerformLayout();
            this.doubleBufferPanel1.ResumeLayout(false);
            this.doubleBufferPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_TemperatureLayout;
        private System.Windows.Forms.Panel panel_Display;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_ColorMotice;
        private System.Windows.Forms.GroupBox groupBox_ScalingRate;
        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.VScrollBar vScrollBar_PixelLength;
        private Control.DoubleBufferControl.DoubleBufferPanel doubleBufferPanel1;
        private System.Windows.Forms.Label label_NoneUpdate;
        private System.Windows.Forms.Label label_LastUpdateTimeValue;
        private System.Windows.Forms.Label label_LastUpdateTime;
        private System.Windows.Forms.Panel panel_Info;
        private System.Windows.Forms.Panel panel2;

    }
}
