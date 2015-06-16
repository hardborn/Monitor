namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    partial class UC_ComplexScreenLayout
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
            this.dbfDataGridView_ComplexScreenInfo = new Nova.Control.DoubleBufferControl.DoublebufferDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dbfDataGridView_ComplexScreenInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dbfDataGridView_ComplexScreenInfo
            // 
            this.dbfDataGridView_ComplexScreenInfo.AllowUserToAddRows = false;
            this.dbfDataGridView_ComplexScreenInfo.AllowUserToDeleteRows = false;
            this.dbfDataGridView_ComplexScreenInfo.AllowUserToResizeColumns = false;
            this.dbfDataGridView_ComplexScreenInfo.AllowUserToResizeRows = false;
            this.dbfDataGridView_ComplexScreenInfo.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dbfDataGridView_ComplexScreenInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbfDataGridView_ComplexScreenInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbfDataGridView_ComplexScreenInfo.Location = new System.Drawing.Point(0, 0);
            this.dbfDataGridView_ComplexScreenInfo.Name = "dbfDataGridView_ComplexScreenInfo";
            this.dbfDataGridView_ComplexScreenInfo.RowHeadersVisible = false;
            this.dbfDataGridView_ComplexScreenInfo.RowTemplate.Height = 23;
            this.dbfDataGridView_ComplexScreenInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dbfDataGridView_ComplexScreenInfo.Size = new System.Drawing.Size(349, 325);
            this.dbfDataGridView_ComplexScreenInfo.TabIndex = 0;
            this.dbfDataGridView_ComplexScreenInfo.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfDataGridView_ComplexScreenInfo_CellMouseLeave);
            this.dbfDataGridView_ComplexScreenInfo.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dbfDataGridView_ComplexScreenInfo_CellMouseMove);
            this.dbfDataGridView_ComplexScreenInfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dbfDataGridView_ComplexScreenInfo_RowPostPaint);
            this.dbfDataGridView_ComplexScreenInfo.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfDataGridView_ComplexScreenInfo_CellMouseEnter);
            // 
            // UC_ComplexScreenLayout
            // 
            this.Controls.Add(this.dbfDataGridView_ComplexScreenInfo);
            this.Name = "UC_ComplexScreenLayout";
            this.Size = new System.Drawing.Size(349, 325);
            this.VisibleChanged += new System.EventHandler(this.UC_ComplexScreenLayout_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dbfDataGridView_ComplexScreenInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.DoubleBufferControl.DoublebufferDataGridView dbfDataGridView_ComplexScreenInfo;
    }
}
