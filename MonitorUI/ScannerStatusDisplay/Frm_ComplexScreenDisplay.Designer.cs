namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class Frm_ComplexScreenDisplay
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dbfPanel_ComplexScreenDisplay = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.SuspendLayout();
            // 
            // dbfPanel_ComplexScreenDisplay
            // 
            this.dbfPanel_ComplexScreenDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbfPanel_ComplexScreenDisplay.Location = new System.Drawing.Point(12, 12);
            this.dbfPanel_ComplexScreenDisplay.Name = "dbfPanel_ComplexScreenDisplay";
            this.dbfPanel_ComplexScreenDisplay.Size = new System.Drawing.Size(594, 463);
            this.dbfPanel_ComplexScreenDisplay.TabIndex = 0;
            this.dbfPanel_ComplexScreenDisplay.VisibleChanged += new System.EventHandler(this.Frm_ComplexScreenDisplay_VisibleChanged);
            // 
            // Frm_ComplexScreenDisplay
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(618, 487);
            this.Controls.Add(this.dbfPanel_ComplexScreenDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ComplexScreenDisplay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查看复杂屏监控数据";
            this.Load += new System.EventHandler(this.Frm_ComplexScreenDisplay_Load);
            this.VisibleChanged += new System.EventHandler(this.Frm_ComplexScreenDisplay_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.DoubleBufferControl.DoubleBufferPanel dbfPanel_ComplexScreenDisplay;
    }
}