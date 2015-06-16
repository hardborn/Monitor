namespace Nova.Monitoring.UI.MonitorSetting
{
    partial class UC_ComplexLayout
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dbfDataGridView_ComplexLayout = new Nova.Control.DoubleBufferControl.DoublebufferDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dbfDataGridView_ComplexLayout)).BeginInit();
            this.SuspendLayout();
            // 
            // dbfDataGridView_ComplexLayout
            // 
            this.dbfDataGridView_ComplexLayout.AllowUserToAddRows = false;
            this.dbfDataGridView_ComplexLayout.AllowUserToDeleteRows = false;
            this.dbfDataGridView_ComplexLayout.AllowUserToResizeRows = false;
            this.dbfDataGridView_ComplexLayout.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dbfDataGridView_ComplexLayout.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dbfDataGridView_ComplexLayout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dbfDataGridView_ComplexLayout.DefaultCellStyle = dataGridViewCellStyle2;
            this.dbfDataGridView_ComplexLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbfDataGridView_ComplexLayout.EnableHeadersVisualStyles = false;
            this.dbfDataGridView_ComplexLayout.Location = new System.Drawing.Point(0, 0);
            this.dbfDataGridView_ComplexLayout.Name = "dbfDataGridView_ComplexLayout";
            this.dbfDataGridView_ComplexLayout.RowHeadersVisible = false;
            this.dbfDataGridView_ComplexLayout.RowTemplate.Height = 23;
            this.dbfDataGridView_ComplexLayout.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dbfDataGridView_ComplexLayout.Size = new System.Drawing.Size(586, 435);
            this.dbfDataGridView_ComplexLayout.TabIndex = 0;
            this.dbfDataGridView_ComplexLayout.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfDataGridView_ComplexLayout_CellContentClick);
            this.dbfDataGridView_ComplexLayout.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfDataGridView_ComplexLayout_CellContentDoubleClick);
            this.dbfDataGridView_ComplexLayout.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfDataGridView_ComplexLayout_CellValueChanged);
            // 
            // UC_ComplexLayout
            // 
            this.Controls.Add(this.dbfDataGridView_ComplexLayout);
            this.Name = "UC_ComplexLayout";
            this.Size = new System.Drawing.Size(586, 435);
            this.Load += new System.EventHandler(this.UC_ComplexLayout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbfDataGridView_ComplexLayout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.DoubleBufferControl.DoublebufferDataGridView dbfDataGridView_ComplexLayout;
    }
}
