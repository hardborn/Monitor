namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_MonitorDataList
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
            UC_Closing();
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dbDataGridView_MonitorInfo = new Nova.Control.DoubleBufferControl.DoublebufferDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataGridView_MonitorInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dbDataGridView_MonitorInfo
            // 
            this.dbDataGridView_MonitorInfo.AllowUserToAddRows = false;
            this.dbDataGridView_MonitorInfo.AllowUserToDeleteRows = false;
            this.dbDataGridView_MonitorInfo.AllowUserToResizeColumns = false;
            this.dbDataGridView_MonitorInfo.AllowUserToResizeRows = false;
            this.dbDataGridView_MonitorInfo.BackgroundColor = System.Drawing.Color.White;
            this.dbDataGridView_MonitorInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbDataGridView_MonitorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbDataGridView_MonitorInfo.EnableHeadersVisualStyles = false;
            this.dbDataGridView_MonitorInfo.Location = new System.Drawing.Point(0, 0);
            this.dbDataGridView_MonitorInfo.Name = "dbDataGridView_MonitorInfo";
            this.dbDataGridView_MonitorInfo.ReadOnly = true;
            this.dbDataGridView_MonitorInfo.RowHeadersVisible = false;
            this.dbDataGridView_MonitorInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dbDataGridView_MonitorInfo.RowTemplate.Height = 23;
            this.dbDataGridView_MonitorInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dbDataGridView_MonitorInfo.Size = new System.Drawing.Size(202, 110);
            this.dbDataGridView_MonitorInfo.TabIndex = 0;
            this.dbDataGridView_MonitorInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbDataGridView_MonitorInfo_CellClick);
            this.dbDataGridView_MonitorInfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dbDataGridView_MonitorInfo_CellFormatting);
            this.dbDataGridView_MonitorInfo.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbDataGridView_MonitorInfo_CellMouseEnter);
            this.dbDataGridView_MonitorInfo.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbDataGridView_MonitorInfo_CellMouseLeave);
            // 
            // UC_MonitorDataList
            // 
            this.Controls.Add(this.dbDataGridView_MonitorInfo);
            this.Name = "UC_MonitorDataList";
            this.Size = new System.Drawing.Size(202, 110);
            ((System.ComponentModel.ISupportInitialize)(this.dbDataGridView_MonitorInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.DoubleBufferControl.DoublebufferDataGridView dbDataGridView_MonitorInfo;
    }
}
