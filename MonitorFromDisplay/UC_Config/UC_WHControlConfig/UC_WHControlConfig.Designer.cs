namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_WHControlConfig
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
            this.panel_ControlConfig = new System.Windows.Forms.Panel();
            this.dataGridView_ControlConfig = new System.Windows.Forms.DataGridView();
            this.seperatePanel_ControlConfigInfoList = new Nova.Control.Panel.SeperatePanel();
            this.vMBaseListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.uCWHControlConfigVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip_Add = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip_Save = new System.Windows.Forms.ToolTip(this.components);
            this.button_deleteAll = new Nova.Control.CrystalButton();
            this.button_delete = new Nova.Control.CrystalButton();
            this.button_edit = new Nova.Control.CrystalButton();
            this.button_Add = new Nova.Control.CrystalButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_deleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.label_tip = new System.Windows.Forms.Label();
            this.panel_ConfigBase.SuspendLayout();
            this.panel_ControlConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ControlConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vMBaseListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCWHControlConfigVMBindingSource)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Controls.Add(this.panel_ControlConfig);
            // 
            // crystalButton_OK
            // 
            this.toolTip_Save.SetToolTip(this.crystalButton_OK, "永久保存修改后的策略");
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // panel_ControlConfig
            // 
            this.panel_ControlConfig.Controls.Add(this.dataGridView_ControlConfig);
            this.panel_ControlConfig.Controls.Add(this.seperatePanel_ControlConfigInfoList);
            this.panel_ControlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ControlConfig.Location = new System.Drawing.Point(0, 0);
            this.panel_ControlConfig.Name = "panel_ControlConfig";
            this.panel_ControlConfig.Size = new System.Drawing.Size(473, 380);
            this.panel_ControlConfig.TabIndex = 0;
            // 
            // dataGridView_ControlConfig
            // 
            this.dataGridView_ControlConfig.AllowUserToAddRows = false;
            this.dataGridView_ControlConfig.AllowUserToDeleteRows = false;
            this.dataGridView_ControlConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_ControlConfig.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridView_ControlConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ControlConfig.Location = new System.Drawing.Point(8, 24);
            this.dataGridView_ControlConfig.MultiSelect = false;
            this.dataGridView_ControlConfig.Name = "dataGridView_ControlConfig";
            this.dataGridView_ControlConfig.ReadOnly = true;
            this.dataGridView_ControlConfig.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_ControlConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_ControlConfig.Size = new System.Drawing.Size(462, 353);
            this.dataGridView_ControlConfig.TabIndex = 22;
            this.dataGridView_ControlConfig.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_ControlConfig_RowPostPaint);
            this.dataGridView_ControlConfig.SelectionChanged += new System.EventHandler(this.dataGridView_ControlConfig_SelectionChanged);
            // 
            // seperatePanel_ControlConfigInfoList
            // 
            this.seperatePanel_ControlConfigInfoList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_ControlConfigInfoList.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_ControlConfigInfoList.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_ControlConfigInfoList.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_ControlConfigInfoList.Location = new System.Drawing.Point(3, 3);
            this.seperatePanel_ControlConfigInfoList.Name = "seperatePanel_ControlConfigInfoList";
            this.seperatePanel_ControlConfigInfoList.SeperateLineHeight = 2;
            this.seperatePanel_ControlConfigInfoList.Size = new System.Drawing.Size(448, 19);
            this.seperatePanel_ControlConfigInfoList.TabIndex = 21;
            this.seperatePanel_ControlConfigInfoList.Text = "控制信息列表";
            // 
            // vMBaseListBindingSource
            // 
            this.vMBaseListBindingSource.DataMember = "VM_BaseList";
            this.vMBaseListBindingSource.DataSource = this.uCWHControlConfigVMBindingSource;
            // 
            // uCWHControlConfigVMBindingSource
            // 
            this.uCWHControlConfigVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_WHControlConfig_VM);
            // 
            // button_deleteAll
            // 
            this.button_deleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.button_deleteAll.Location = new System.Drawing.Point(170, 402);
            this.button_deleteAll.Name = "button_deleteAll";
            this.button_deleteAll.Size = new System.Drawing.Size(69, 30);
            this.button_deleteAll.TabIndex = 100;
            this.button_deleteAll.Text = "清空列表";
            this.button_deleteAll.Transparency = 50;
            this.button_deleteAll.UseVisualStyleBackColor = false;
            this.button_deleteAll.Click += new System.EventHandler(this.button_deleteAll_Click);
            // 
            // button_delete
            // 
            this.button_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_delete.AutoEllipsis = true;
            this.button_delete.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_delete.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.button_delete.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_delete.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_delete.ButtonCornorRadius = 3;
            this.button_delete.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_delete.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_delete.Enabled = false;
            this.button_delete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_delete.IsButtonFoucs = false;
            this.button_delete.Location = new System.Drawing.Point(118, 402);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(51, 30);
            this.button_delete.TabIndex = 101;
            this.button_delete.Text = "删除";
            this.button_delete.Transparency = 50;
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_edit
            // 
            this.button_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_edit.AutoEllipsis = true;
            this.button_edit.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_edit.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.button_edit.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_edit.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_edit.ButtonCornorRadius = 3;
            this.button_edit.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_edit.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_edit.Enabled = false;
            this.button_edit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_edit.IsButtonFoucs = false;
            this.button_edit.Location = new System.Drawing.Point(66, 402);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(51, 30);
            this.button_edit.TabIndex = 102;
            this.button_edit.Text = "编辑";
            this.button_edit.Transparency = 50;
            this.button_edit.UseVisualStyleBackColor = false;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // button_Add
            // 
            this.button_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Add.AutoEllipsis = true;
            this.button_Add.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_Add.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.button_Add.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Add.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_Add.ButtonCornorRadius = 3;
            this.button_Add.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Add.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_Add.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_Add.IsButtonFoucs = false;
            this.button_Add.Location = new System.Drawing.Point(14, 402);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(51, 30);
            this.button_Add.TabIndex = 103;
            this.button_Add.Text = "添加";
            this.button_Add.Transparency = 50;
            this.button_Add.UseVisualStyleBackColor = false;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_add,
            this.ToolStripMenuItem_edit,
            this.ToolStripMenuItem_delete,
            this.ToolStripMenuItem_deleteAll});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 92);
            // 
            // ToolStripMenuItem_add
            // 
            this.ToolStripMenuItem_add.Name = "ToolStripMenuItem_add";
            this.ToolStripMenuItem_add.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_add.Text = "添加";
            this.ToolStripMenuItem_add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // ToolStripMenuItem_edit
            // 
            this.ToolStripMenuItem_edit.Enabled = false;
            this.ToolStripMenuItem_edit.Name = "ToolStripMenuItem_edit";
            this.ToolStripMenuItem_edit.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_edit.Text = "编辑";
            this.ToolStripMenuItem_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // ToolStripMenuItem_delete
            // 
            this.ToolStripMenuItem_delete.Enabled = false;
            this.ToolStripMenuItem_delete.Name = "ToolStripMenuItem_delete";
            this.ToolStripMenuItem_delete.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_delete.Text = "删除";
            this.ToolStripMenuItem_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // ToolStripMenuItem_deleteAll
            // 
            this.ToolStripMenuItem_deleteAll.Enabled = false;
            this.ToolStripMenuItem_deleteAll.Name = "ToolStripMenuItem_deleteAll";
            this.ToolStripMenuItem_deleteAll.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_deleteAll.Text = "清空列表";
            this.ToolStripMenuItem_deleteAll.Click += new System.EventHandler(this.button_deleteAll_Click);
            // 
            // label_tip
            // 
            this.label_tip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_tip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label_tip.ForeColor = System.Drawing.Color.Red;
            this.label_tip.Location = new System.Drawing.Point(0, 0);
            this.label_tip.Name = "label_tip";
            this.label_tip.Size = new System.Drawing.Size(500, 450);
            this.label_tip.TabIndex = 104;
            this.label_tip.Text = "label1";
            this.label_tip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_WHControlConfig
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.label_tip);
            this.Controls.Add(this.button_deleteAll);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_edit);
            this.Controls.Add(this.button_Add);
            this.Name = "UC_WHControlConfig";
            this.Controls.SetChildIndex(this.button_Add, 0);
            this.Controls.SetChildIndex(this.button_edit, 0);
            this.Controls.SetChildIndex(this.button_delete, 0);
            this.Controls.SetChildIndex(this.button_deleteAll, 0);
            this.Controls.SetChildIndex(this.label_tip, 0);
            this.Controls.SetChildIndex(this.crystalButton_OK, 0);
            this.Controls.SetChildIndex(this.panel_ConfigBase, 0);
            this.panel_ConfigBase.ResumeLayout(false);
            this.panel_ControlConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ControlConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vMBaseListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCWHControlConfigVMBindingSource)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_ControlConfig;
        private Nova.Control.Panel.SeperatePanel seperatePanel_ControlConfigInfoList;
        private System.Windows.Forms.DataGridView dataGridView_ControlConfig;
        private System.Windows.Forms.BindingSource uCWHControlConfigVMBindingSource;
        private System.Windows.Forms.BindingSource vMBaseListBindingSource;
        private System.Windows.Forms.ToolTip toolTip_Add;
        private System.Windows.Forms.ToolTip toolTip_Save;
        private Nova.Control.CrystalButton button_deleteAll;
        private Nova.Control.CrystalButton button_delete;
        private Nova.Control.CrystalButton button_edit;
        private Nova.Control.CrystalButton button_Add;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_add;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_edit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_delete;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_deleteAll;
        private System.Windows.Forms.Label label_tip;

    }
}
