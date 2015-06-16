namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_BrightnessConfig
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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_deleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.button_add = new Nova.Control.CrystalButton();
            this.button_deleteAll = new Nova.Control.CrystalButton();
            this.BrightnessConfig_BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.currentBrightnessLabel = new System.Windows.Forms.Label();
            this.currentBrightnessTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.brightnessConfigGroupBox = new System.Windows.Forms.GroupBox();
            this.dataGridView_BrightnessConfig = new System.Windows.Forms.DataGridView();
            this.openLightSensoreConfigButton = new Nova.Control.CrystalButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.alertInfoLabel = new System.Windows.Forms.Label();
            this.panel_ConfigBase.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessConfig_BindingSource)).BeginInit();
            this.brightnessConfigGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_BrightnessConfig)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.brightnessConfigGroupBox);
            this.panel_ConfigBase.Controls.Add(this.label1);
            this.panel_ConfigBase.Controls.Add(this.currentBrightnessTextBox);
            this.panel_ConfigBase.Controls.Add(this.currentBrightnessLabel);
            this.panel_ConfigBase.Size = new System.Drawing.Size(473, 389);
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Location = new System.Drawing.Point(295, 406);
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_add,
            this.ToolStripMenuItem_edit,
            this.ToolStripMenuItem_delete,
            this.ToolStripMenuItem_deleteAll});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(123, 92);
            // 
            // ToolStripMenuItem_add
            // 
            this.ToolStripMenuItem_add.Name = "ToolStripMenuItem_add";
            this.ToolStripMenuItem_add.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_add.Text = "添加";
            this.ToolStripMenuItem_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // ToolStripMenuItem_edit
            // 
            this.ToolStripMenuItem_edit.Enabled = false;
            this.ToolStripMenuItem_edit.Name = "ToolStripMenuItem_edit";
            this.ToolStripMenuItem_edit.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_edit.Text = "编辑";
            this.ToolStripMenuItem_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // ToolStripMenuItem_delete
            // 
            this.ToolStripMenuItem_delete.Enabled = false;
            this.ToolStripMenuItem_delete.Name = "ToolStripMenuItem_delete";
            this.ToolStripMenuItem_delete.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_delete.Text = "删除";
            this.ToolStripMenuItem_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // ToolStripMenuItem_deleteAll
            // 
            this.ToolStripMenuItem_deleteAll.Enabled = false;
            this.ToolStripMenuItem_deleteAll.Name = "ToolStripMenuItem_deleteAll";
            this.ToolStripMenuItem_deleteAll.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_deleteAll.Text = "清空列表";
            this.ToolStripMenuItem_deleteAll.Click += new System.EventHandler(this.button_deleteAll_Click);
            // 
            // button_add
            // 
            this.button_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add.AutoEllipsis = true;
            this.button_add.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_add.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.button_add.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_add.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_add.ButtonCornorRadius = 3;
            this.button_add.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_add.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_add.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_add.IsButtonFoucs = false;
            this.button_add.Location = new System.Drawing.Point(293, 14);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(70, 30);
            this.button_add.TabIndex = 99;
            this.button_add.Text = "添加";
            this.button_add.Transparency = 50;
            this.button_add.UseVisualStyleBackColor = false;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_deleteAll
            // 
            this.button_deleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_deleteAll.AutoEllipsis = true;
            this.button_deleteAll.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_deleteAll.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.button_deleteAll.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_deleteAll.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_deleteAll.ButtonCornorRadius = 3;
            this.button_deleteAll.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_deleteAll.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_deleteAll.Enabled = false;
            this.button_deleteAll.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_deleteAll.IsButtonFoucs = false;
            this.button_deleteAll.Location = new System.Drawing.Point(385, 14);
            this.button_deleteAll.Name = "button_deleteAll";
            this.button_deleteAll.Size = new System.Drawing.Size(70, 30);
            this.button_deleteAll.TabIndex = 99;
            this.button_deleteAll.Text = "清空列表";
            this.button_deleteAll.Transparency = 50;
            this.button_deleteAll.UseVisualStyleBackColor = false;
            this.button_deleteAll.Click += new System.EventHandler(this.button_deleteAll_Click);
            // 
            // BrightnessConfig_BindingSource
            // 
            this.BrightnessConfig_BindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.BrightnessConfig_BindingSource_ListChanged);
            // 
            // currentBrightnessLabel
            // 
            this.currentBrightnessLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentBrightnessLabel.AutoEllipsis = true;
            this.currentBrightnessLabel.Location = new System.Drawing.Point(229, 5);
            this.currentBrightnessLabel.Name = "currentBrightnessLabel";
            this.currentBrightnessLabel.Size = new System.Drawing.Size(163, 22);
            this.currentBrightnessLabel.TabIndex = 1;
            this.currentBrightnessLabel.Text = "当前屏亮度:";
            this.currentBrightnessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.currentBrightnessLabel.Visible = false;
            // 
            // currentBrightnessTextBox
            // 
            this.currentBrightnessTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentBrightnessTextBox.Enabled = false;
            this.currentBrightnessTextBox.Location = new System.Drawing.Point(398, 6);
            this.currentBrightnessTextBox.Name = "currentBrightnessTextBox";
            this.currentBrightnessTextBox.Size = new System.Drawing.Size(49, 21);
            this.currentBrightnessTextBox.TabIndex = 2;
            this.currentBrightnessTextBox.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(452, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "%";
            this.label1.Visible = false;
            // 
            // brightnessConfigGroupBox
            // 
            this.brightnessConfigGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.brightnessConfigGroupBox.Controls.Add(this.button_deleteAll);
            this.brightnessConfigGroupBox.Controls.Add(this.dataGridView_BrightnessConfig);
            this.brightnessConfigGroupBox.Controls.Add(this.button_add);
            this.brightnessConfigGroupBox.Location = new System.Drawing.Point(4, 30);
            this.brightnessConfigGroupBox.Name = "brightnessConfigGroupBox";
            this.brightnessConfigGroupBox.Size = new System.Drawing.Size(466, 356);
            this.brightnessConfigGroupBox.TabIndex = 4;
            this.brightnessConfigGroupBox.TabStop = false;
            this.brightnessConfigGroupBox.Text = "亮度调节配置";
            // 
            // dataGridView_BrightnessConfig
            // 
            this.dataGridView_BrightnessConfig.AllowUserToAddRows = false;
            this.dataGridView_BrightnessConfig.AllowUserToDeleteRows = false;
            this.dataGridView_BrightnessConfig.AllowUserToResizeRows = false;
            this.dataGridView_BrightnessConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_BrightnessConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_BrightnessConfig.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridView_BrightnessConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_BrightnessConfig.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView_BrightnessConfig.Location = new System.Drawing.Point(6, 50);
            this.dataGridView_BrightnessConfig.MultiSelect = false;
            this.dataGridView_BrightnessConfig.Name = "dataGridView_BrightnessConfig";
            this.dataGridView_BrightnessConfig.ReadOnly = true;
            this.dataGridView_BrightnessConfig.RowHeadersVisible = false;
            this.dataGridView_BrightnessConfig.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_BrightnessConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_BrightnessConfig.Size = new System.Drawing.Size(454, 300);
            this.dataGridView_BrightnessConfig.TabIndex = 0;
            this.dataGridView_BrightnessConfig.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_BrightnessConfig_CellMouseDown);
            this.dataGridView_BrightnessConfig.SelectionChanged += new System.EventHandler(this.dataGridView_BrightnessConfig_SelectionChanged);
            // 
            // openLightSensoreConfigButton
            // 
            this.openLightSensoreConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openLightSensoreConfigButton.AutoEllipsis = true;
            this.openLightSensoreConfigButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.openLightSensoreConfigButton.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.openLightSensoreConfigButton.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.openLightSensoreConfigButton.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.openLightSensoreConfigButton.ButtonCornorRadius = 3;
            this.openLightSensoreConfigButton.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.openLightSensoreConfigButton.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.openLightSensoreConfigButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.openLightSensoreConfigButton.IsButtonFoucs = false;
            this.openLightSensoreConfigButton.Location = new System.Drawing.Point(202, 406);
            this.openLightSensoreConfigButton.Name = "openLightSensoreConfigButton";
            this.openLightSensoreConfigButton.Size = new System.Drawing.Size(90, 30);
            this.openLightSensoreConfigButton.TabIndex = 99;
            this.openLightSensoreConfigButton.Text = "配置光探头";
            this.openLightSensoreConfigButton.Transparency = 50;
            this.openLightSensoreConfigButton.UseVisualStyleBackColor = false;
            this.openLightSensoreConfigButton.Click += new System.EventHandler(this.openOpticalProbeConfigButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.alertInfoLabel);
            this.panel1.Location = new System.Drawing.Point(14, 402);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 45);
            this.panel1.TabIndex = 100;
            // 
            // alertInfoLabel
            // 
            this.alertInfoLabel.AutoEllipsis = true;
            this.alertInfoLabel.AutoSize = true;
            this.alertInfoLabel.ForeColor = System.Drawing.Color.Red;
            this.alertInfoLabel.Location = new System.Drawing.Point(6, 9);
            this.alertInfoLabel.Name = "alertInfoLabel";
            this.alertInfoLabel.Size = new System.Drawing.Size(0, 12);
            this.alertInfoLabel.TabIndex = 0;
            // 
            // UC_BrightnessConfig
            // 
            this.Controls.Add(this.openLightSensoreConfigButton);
            this.Controls.Add(this.panel1);
            this.Name = "UC_BrightnessConfig";
            this.Controls.SetChildIndex(this.crystalButton_OK, 0);
            this.Controls.SetChildIndex(this.panel_ConfigBase, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.openLightSensoreConfigButton, 0);
            this.panel_ConfigBase.ResumeLayout(false);
            this.panel_ConfigBase.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessConfig_BindingSource)).EndInit();
            this.brightnessConfigGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_BrightnessConfig)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.CrystalButton button_add;
        private Nova.Control.CrystalButton button_deleteAll;
        private System.Windows.Forms.BindingSource BrightnessConfig_BindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_add;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_edit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_delete;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_deleteAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox currentBrightnessTextBox;
        private System.Windows.Forms.Label currentBrightnessLabel;
        private System.Windows.Forms.GroupBox brightnessConfigGroupBox;
        private Nova.Control.CrystalButton openLightSensoreConfigButton;
        private System.Windows.Forms.DataGridView dataGridView_BrightnessConfig;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label alertInfoLabel;
    }
}
