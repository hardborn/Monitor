namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_CareServerConfig
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
            this.panel_CareRegister = new System.Windows.Forms.Panel();
            this.dataGridViewBaseEx_Care = new Nova.Control.DataGridViewBaseEx();
            this.label_Disclaimer = new System.Windows.Forms.Label();
            this.txt_CareRegisterUser = new System.Windows.Forms.TextBox();
            this.uCCareServerConfigVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label_CareUser = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.crystalButton_RefreshUser = new Nova.Control.CrystalButton();
            this.panel_ConfigBase.SuspendLayout();
            this.panel_CareRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaseEx_Care)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCCareServerConfigVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.crystalButton_RefreshUser);
            this.panel_ConfigBase.Controls.Add(this.panel_CareRegister);
            this.panel_ConfigBase.Controls.Add(this.label_Disclaimer);
            this.panel_ConfigBase.Controls.Add(this.txt_CareRegisterUser);
            this.panel_ConfigBase.Controls.Add(this.label_CareUser);
            this.panel_ConfigBase.Location = new System.Drawing.Point(3, 7);
            this.panel_ConfigBase.Size = new System.Drawing.Size(478, 375);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // panel_CareRegister
            // 
            this.panel_CareRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_CareRegister.AutoScroll = true;
            this.panel_CareRegister.Controls.Add(this.dataGridViewBaseEx_Care);
            this.panel_CareRegister.Location = new System.Drawing.Point(3, 36);
            this.panel_CareRegister.Name = "panel_CareRegister";
            this.panel_CareRegister.Size = new System.Drawing.Size(472, 333);
            this.panel_CareRegister.TabIndex = 106;
            // 
            // dataGridViewBaseEx_Care
            // 
            this.dataGridViewBaseEx_Care.AllowUserToAddRows = false;
            this.dataGridViewBaseEx_Care.AllowUserToDeleteRows = false;
            this.dataGridViewBaseEx_Care.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewBaseEx_Care.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBaseEx_Care.DivideLineColor = System.Drawing.Color.RoyalBlue;
            this.dataGridViewBaseEx_Care.DivideLineWidth = 1;
            this.dataGridViewBaseEx_Care.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBaseEx_Care.EnableHeadersVisualStyles = false;
            this.dataGridViewBaseEx_Care.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewBaseEx_Care.Name = "dataGridViewBaseEx_Care";
            this.dataGridViewBaseEx_Care.RowHeadersVisible = false;
            this.dataGridViewBaseEx_Care.RowTemplate.Height = 23;
            this.dataGridViewBaseEx_Care.SelectCellBorderColor = System.Drawing.Color.Transparent;
            this.dataGridViewBaseEx_Care.SelectCellHeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(123)))));
            this.dataGridViewBaseEx_Care.SelectCellLineWidth = 1;
            this.dataGridViewBaseEx_Care.Size = new System.Drawing.Size(472, 333);
            this.dataGridViewBaseEx_Care.TabIndex = 4;
            this.dataGridViewBaseEx_Care.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBaseEx_Care_CellClick);
            this.dataGridViewBaseEx_Care.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewBaseEx_Care_CellFormatting);
            this.dataGridViewBaseEx_Care.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBaseEx_Care_CellValidated);
            this.dataGridViewBaseEx_Care.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBaseEx_Care_CellValueChanged);
            this.dataGridViewBaseEx_Care.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewBaseEx_Care_DataError);
            // 
            // label_Disclaimer
            // 
            this.label_Disclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Disclaimer.AutoEllipsis = true;
            this.label_Disclaimer.ForeColor = System.Drawing.Color.Red;
            this.label_Disclaimer.Location = new System.Drawing.Point(254, 3);
            this.label_Disclaimer.Name = "label_Disclaimer";
            this.label_Disclaimer.Size = new System.Drawing.Size(221, 31);
            this.label_Disclaimer.TabIndex = 105;
            this.label_Disclaimer.Text = "新用户与已注册用户冲突时,新用户生效,并且不会通知.";
            // 
            // txt_CareRegisterUser
            // 
            this.txt_CareRegisterUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.uCCareServerConfigVMBindingSource, "UserId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txt_CareRegisterUser.Location = new System.Drawing.Point(68, 6);
            this.txt_CareRegisterUser.MaxLength = 20;
            this.txt_CareRegisterUser.Name = "txt_CareRegisterUser";
            this.txt_CareRegisterUser.Size = new System.Drawing.Size(106, 21);
            this.txt_CareRegisterUser.TabIndex = 104;
            this.txt_CareRegisterUser.TextChanged += new System.EventHandler(this.txt_CareRegisterUser_TextChanged);
            // 
            // uCCareServerConfigVMBindingSource
            // 
            this.uCCareServerConfigVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_CareServerConfig_VM);
            // 
            // label_CareUser
            // 
            this.label_CareUser.AutoEllipsis = true;
            this.label_CareUser.Location = new System.Drawing.Point(7, 8);
            this.label_CareUser.Name = "label_CareUser";
            this.label_CareUser.Size = new System.Drawing.Size(55, 19);
            this.label_CareUser.TabIndex = 103;
            this.label_CareUser.Text = "用户:";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "Uc_CareRegisters";
            this.bindingSource1.DataSource = this.uCCareServerConfigVMBindingSource;
            // 
            // crystalButton_RefreshUser
            // 
            this.crystalButton_RefreshUser.AutoEllipsis = true;
            this.crystalButton_RefreshUser.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_RefreshUser.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_RefreshUser.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RefreshUser.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_RefreshUser.ButtonCornorRadius = 3;
            this.crystalButton_RefreshUser.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_RefreshUser.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_RefreshUser.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_RefreshUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_RefreshUser.IsButtonFoucs = false;
            this.crystalButton_RefreshUser.Location = new System.Drawing.Point(180, 3);
            this.crystalButton_RefreshUser.Name = "crystalButton_RefreshUser";
            this.crystalButton_RefreshUser.Size = new System.Drawing.Size(70, 25);
            this.crystalButton_RefreshUser.TabIndex = 105;
            this.crystalButton_RefreshUser.Text = "刷新";
            this.crystalButton_RefreshUser.Transparency = 50;
            this.crystalButton_RefreshUser.UseVisualStyleBackColor = false;
            this.crystalButton_RefreshUser.Click += new System.EventHandler(this.crystalButton_RefreshUser_Click);
            // 
            // UC_CareServerConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "UC_CareServerConfig";
            this.panel_ConfigBase.ResumeLayout(false);
            this.panel_ConfigBase.PerformLayout();
            this.panel_CareRegister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaseEx_Care)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCCareServerConfigVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_CareRegister;
        private System.Windows.Forms.Label label_Disclaimer;
        private System.Windows.Forms.TextBox txt_CareRegisterUser;
        private System.Windows.Forms.Label label_CareUser;
        private System.Windows.Forms.BindingSource uCCareServerConfigVMBindingSource;
        private Nova.Control.DataGridViewBaseEx dataGridViewBaseEx_Care;
        private System.Windows.Forms.BindingSource bindingSource1;
        protected Nova.Control.CrystalButton crystalButton_RefreshUser;
    }
}
