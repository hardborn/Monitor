namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_PowerControlConfig
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
            this.crystalButton_RefreshPower = new Nova.Control.CrystalButton();
            this.dataGridViewBaseEx_FuncCardPower = new Nova.Control.DataGridViewBaseEx();
            this.uCPowerControlConfigVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label_PowerDescription = new System.Windows.Forms.Label();
            this.panel_ConfigBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaseEx_FuncCardPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCPowerControlConfigVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.label_PowerDescription);
            this.panel_ConfigBase.Controls.Add(this.crystalButton_RefreshPower);
            this.panel_ConfigBase.Controls.Add(this.dataGridViewBaseEx_FuncCardPower);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // crystalButton_RefreshPower
            // 
            this.crystalButton_RefreshPower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_RefreshPower.AutoEllipsis = true;
            this.crystalButton_RefreshPower.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_RefreshPower.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_RefreshPower.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RefreshPower.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_RefreshPower.ButtonCornorRadius = 3;
            this.crystalButton_RefreshPower.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RefreshPower.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_RefreshPower.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_RefreshPower.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_RefreshPower.IsButtonFoucs = false;
            this.crystalButton_RefreshPower.Location = new System.Drawing.Point(361, 333);
            this.crystalButton_RefreshPower.Name = "crystalButton_RefreshPower";
            this.crystalButton_RefreshPower.Size = new System.Drawing.Size(90, 30);
            this.crystalButton_RefreshPower.TabIndex = 100;
            this.crystalButton_RefreshPower.Text = "更新多功能卡";
            this.crystalButton_RefreshPower.Transparency = 50;
            this.crystalButton_RefreshPower.UseVisualStyleBackColor = false;
            this.crystalButton_RefreshPower.Click += new System.EventHandler(this.crystalButton_RefreshPower_Click);
            // 
            // dataGridViewBaseEx_FuncCardPower
            // 
            this.dataGridViewBaseEx_FuncCardPower.AllowUserToAddRows = false;
            this.dataGridViewBaseEx_FuncCardPower.AllowUserToDeleteRows = false;
            this.dataGridViewBaseEx_FuncCardPower.AllowUserToResizeColumns = false;
            this.dataGridViewBaseEx_FuncCardPower.AllowUserToResizeRows = false;
            this.dataGridViewBaseEx_FuncCardPower.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewBaseEx_FuncCardPower.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewBaseEx_FuncCardPower.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBaseEx_FuncCardPower.DivideLineColor = System.Drawing.Color.RoyalBlue;
            this.dataGridViewBaseEx_FuncCardPower.DivideLineWidth = 1;
            this.dataGridViewBaseEx_FuncCardPower.EnableHeadersVisualStyles = false;
            this.dataGridViewBaseEx_FuncCardPower.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewBaseEx_FuncCardPower.Name = "dataGridViewBaseEx_FuncCardPower";
            this.dataGridViewBaseEx_FuncCardPower.RowHeadersVisible = false;
            this.dataGridViewBaseEx_FuncCardPower.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewBaseEx_FuncCardPower.RowTemplate.Height = 23;
            this.dataGridViewBaseEx_FuncCardPower.SelectCellBorderColor = System.Drawing.Color.Transparent;
            this.dataGridViewBaseEx_FuncCardPower.SelectCellHeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(123)))));
            this.dataGridViewBaseEx_FuncCardPower.SelectCellLineWidth = 1;
            this.dataGridViewBaseEx_FuncCardPower.Size = new System.Drawing.Size(467, 324);
            this.dataGridViewBaseEx_FuncCardPower.TabIndex = 99;
            this.dataGridViewBaseEx_FuncCardPower.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewBaseEx_FuncCardPower_CellFormatting);
            this.dataGridViewBaseEx_FuncCardPower.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBaseEx_FuncCardPower_CellValidated);
            // 
            // uCPowerControlConfigVMBindingSource
            // 
            this.uCPowerControlConfigVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_PowerControlConfig_VM);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "PowerConfig";
            this.bindingSource1.DataSource = this.uCPowerControlConfigVMBindingSource;
            // 
            // label_PowerDescription
            // 
            this.label_PowerDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_PowerDescription.AutoEllipsis = true;
            this.label_PowerDescription.Location = new System.Drawing.Point(10, 332);
            this.label_PowerDescription.Name = "label_PowerDescription";
            this.label_PowerDescription.Size = new System.Drawing.Size(345, 31);
            this.label_PowerDescription.TabIndex = 101;
            this.label_PowerDescription.Text = "说明:电源别名用于策略选择时使用，不允许为空，不允许重复。";
            // 
            // UC_PowerControlConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "UC_PowerControlConfig";
            this.panel_ConfigBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaseEx_FuncCardPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCPowerControlConfigVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected Nova.Control.CrystalButton crystalButton_RefreshPower;
        private Nova.Control.DataGridViewBaseEx dataGridViewBaseEx_FuncCardPower;
        private System.Windows.Forms.BindingSource uCPowerControlConfigVMBindingSource;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label_PowerDescription;
    }
}
