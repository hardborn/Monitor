namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    partial class frm_OpticalProbeConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            FrmClosed();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.confirmButton = new Nova.Control.CrystalButton();
            this.cancelButton = new Nova.Control.CrystalButton();
            this.workspacePanel = new System.Windows.Forms.Panel();
            this.DisableGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.currentBrightnessTextBox = new System.Windows.Forms.TextBox();
            this.label_CurrentLight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.brightnessNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.enableCheckBox = new System.Windows.Forms.CheckBox();
            this.mappingGroupBox = new System.Windows.Forms.GroupBox();
            this.AddMappingItemButton = new Nova.Control.CrystalButton();
            this.brightnessDataGridView = new System.Windows.Forms.DataGridView();
            this.EnvironmentBrightnessColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedBrightnessColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.autoSectionButton = new Nova.Control.CrystalButton();
            this.opticalProbeGroupBox = new System.Windows.Forms.GroupBox();
            this.crystalButton_BrightTest = new Nova.Control.CrystalButton();
            this.refreshOpticalProbeCrystalButton = new Nova.Control.CrystalButton();
            this.opticalProbeDataGridView = new System.Windows.Forms.DataGridView();
            this.EnableColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PositionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentBrightnessColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemarkColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workspacePanel.SuspendLayout();
            this.DisableGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessNumericUpDown)).BeginInit();
            this.mappingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessDataGridView)).BeginInit();
            this.opticalProbeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opticalProbeDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.AutoEllipsis = true;
            this.confirmButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.confirmButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.confirmButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.confirmButton.ButtonClickColor = System.Drawing.Color.Green;
            this.confirmButton.ButtonCornorRadius = 3;
            this.confirmButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.confirmButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.confirmButton.ForeColor = System.Drawing.Color.Black;
            this.confirmButton.IsButtonFoucs = false;
            this.confirmButton.Location = new System.Drawing.Point(443, 458);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(90, 30);
            this.confirmButton.TabIndex = 2;
            this.confirmButton.Text = "确定";
            this.confirmButton.Transparency = 50;
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoEllipsis = true;
            this.cancelButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.cancelButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.cancelButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.cancelButton.ButtonClickColor = System.Drawing.Color.Green;
            this.cancelButton.ButtonCornorRadius = 3;
            this.cancelButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.cancelButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.IsButtonFoucs = false;
            this.cancelButton.Location = new System.Drawing.Point(548, 458);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 30);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "取消";
            this.cancelButton.Transparency = 50;
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // workspacePanel
            // 
            this.workspacePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workspacePanel.Controls.Add(this.DisableGroupBox);
            this.workspacePanel.Controls.Add(this.mappingGroupBox);
            this.workspacePanel.Controls.Add(this.opticalProbeGroupBox);
            this.workspacePanel.Location = new System.Drawing.Point(6, 12);
            this.workspacePanel.Name = "workspacePanel";
            this.workspacePanel.Size = new System.Drawing.Size(642, 443);
            this.workspacePanel.TabIndex = 0;
            // 
            // DisableGroupBox
            // 
            this.DisableGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DisableGroupBox.Controls.Add(this.label3);
            this.DisableGroupBox.Controls.Add(this.currentBrightnessTextBox);
            this.DisableGroupBox.Controls.Add(this.label_CurrentLight);
            this.DisableGroupBox.Controls.Add(this.label1);
            this.DisableGroupBox.Controls.Add(this.brightnessNumericUpDown);
            this.DisableGroupBox.Controls.Add(this.enableCheckBox);
            this.DisableGroupBox.Location = new System.Drawing.Point(3, 3);
            this.DisableGroupBox.Name = "DisableGroupBox";
            this.DisableGroupBox.Size = new System.Drawing.Size(636, 55);
            this.DisableGroupBox.TabIndex = 1;
            this.DisableGroupBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(609, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "%";
            this.label3.Visible = false;
            // 
            // currentBrightnessTextBox
            // 
            this.currentBrightnessTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentBrightnessTextBox.Enabled = false;
            this.currentBrightnessTextBox.Location = new System.Drawing.Point(559, 16);
            this.currentBrightnessTextBox.Name = "currentBrightnessTextBox";
            this.currentBrightnessTextBox.Size = new System.Drawing.Size(43, 21);
            this.currentBrightnessTextBox.TabIndex = 5;
            this.currentBrightnessTextBox.Visible = false;
            // 
            // label_CurrentLight
            // 
            this.label_CurrentLight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_CurrentLight.AutoEllipsis = true;
            this.label_CurrentLight.Location = new System.Drawing.Point(448, 14);
            this.label_CurrentLight.Name = "label_CurrentLight";
            this.label_CurrentLight.Size = new System.Drawing.Size(105, 22);
            this.label_CurrentLight.TabIndex = 4;
            this.label_CurrentLight.Text = "当前亮度";
            this.label_CurrentLight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_CurrentLight.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(433, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "%";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // brightnessNumericUpDown
            // 
            this.brightnessNumericUpDown.Enabled = false;
            this.brightnessNumericUpDown.Location = new System.Drawing.Point(363, 17);
            this.brightnessNumericUpDown.Name = "brightnessNumericUpDown";
            this.brightnessNumericUpDown.Size = new System.Drawing.Size(66, 21);
            this.brightnessNumericUpDown.TabIndex = 2;
            this.brightnessNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // enableCheckBox
            // 
            this.enableCheckBox.AutoEllipsis = true;
            this.enableCheckBox.Location = new System.Drawing.Point(26, 10);
            this.enableCheckBox.Name = "enableCheckBox";
            this.enableCheckBox.Size = new System.Drawing.Size(330, 32);
            this.enableCheckBox.TabIndex = 0;
            this.enableCheckBox.Text = "读取环境亮度失败时，亮度调节到";
            this.enableCheckBox.UseVisualStyleBackColor = true;
            this.enableCheckBox.CheckedChanged += new System.EventHandler(this.enableCheckBox_CheckedChanged);
            // 
            // mappingGroupBox
            // 
            this.mappingGroupBox.Controls.Add(this.AddMappingItemButton);
            this.mappingGroupBox.Controls.Add(this.brightnessDataGridView);
            this.mappingGroupBox.Controls.Add(this.autoSectionButton);
            this.mappingGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mappingGroupBox.Location = new System.Drawing.Point(0, 198);
            this.mappingGroupBox.Name = "mappingGroupBox";
            this.mappingGroupBox.Size = new System.Drawing.Size(642, 245);
            this.mappingGroupBox.TabIndex = 0;
            this.mappingGroupBox.TabStop = false;
            this.mappingGroupBox.Text = "亮度映射表(环境亮度|屏体亮度)";
            // 
            // AddMappingItemButton
            // 
            this.AddMappingItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddMappingItemButton.AutoEllipsis = true;
            this.AddMappingItemButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.AddMappingItemButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.AddMappingItemButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.AddMappingItemButton.ButtonClickColor = System.Drawing.Color.Green;
            this.AddMappingItemButton.ButtonCornorRadius = 3;
            this.AddMappingItemButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.AddMappingItemButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.AddMappingItemButton.ForeColor = System.Drawing.Color.Black;
            this.AddMappingItemButton.IsButtonFoucs = false;
            this.AddMappingItemButton.Location = new System.Drawing.Point(542, 11);
            this.AddMappingItemButton.Name = "AddMappingItemButton";
            this.AddMappingItemButton.Size = new System.Drawing.Size(90, 30);
            this.AddMappingItemButton.TabIndex = 1;
            this.AddMappingItemButton.Text = "添加配置";
            this.AddMappingItemButton.Transparency = 50;
            this.AddMappingItemButton.UseVisualStyleBackColor = false;
            this.AddMappingItemButton.Click += new System.EventHandler(this.AddMappingItemButton_Click);
            // 
            // brightnessDataGridView
            // 
            this.brightnessDataGridView.AllowUserToAddRows = false;
            this.brightnessDataGridView.AllowUserToResizeColumns = false;
            this.brightnessDataGridView.AllowUserToResizeRows = false;
            this.brightnessDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.brightnessDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.brightnessDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.brightnessDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnvironmentBrightnessColumn,
            this.LedBrightnessColumn,
            this.DeleteButtonColumn});
            this.brightnessDataGridView.Location = new System.Drawing.Point(3, 47);
            this.brightnessDataGridView.Name = "brightnessDataGridView";
            this.brightnessDataGridView.ReadOnly = true;
            this.brightnessDataGridView.RowHeadersVisible = false;
            this.brightnessDataGridView.RowTemplate.Height = 23;
            this.brightnessDataGridView.Size = new System.Drawing.Size(636, 195);
            this.brightnessDataGridView.TabIndex = 0;
            this.brightnessDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.brightnessDataGridView_CellClick);
            this.brightnessDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.brightnessDataGridView_CellValidating);
            this.brightnessDataGridView.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.brightnessDataGridView_RowValidated);
            this.brightnessDataGridView.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.brightnessDataGridView_RowValidating);
            this.brightnessDataGridView.Validated += new System.EventHandler(this.brightnessDataGridView_Validated);
            // 
            // EnvironmentBrightnessColumn
            // 
            this.EnvironmentBrightnessColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EnvironmentBrightnessColumn.HeaderText = "环境亮度(Lux)";
            this.EnvironmentBrightnessColumn.Name = "EnvironmentBrightnessColumn";
            this.EnvironmentBrightnessColumn.ReadOnly = true;
            this.EnvironmentBrightnessColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LedBrightnessColumn
            // 
            this.LedBrightnessColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LedBrightnessColumn.HeaderText = "屏体亮度(%)";
            this.LedBrightnessColumn.Name = "LedBrightnessColumn";
            this.LedBrightnessColumn.ReadOnly = true;
            this.LedBrightnessColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DeleteButtonColumn
            // 
            this.DeleteButtonColumn.HeaderText = "操作";
            this.DeleteButtonColumn.Name = "DeleteButtonColumn";
            this.DeleteButtonColumn.ReadOnly = true;
            this.DeleteButtonColumn.Text = "删除";
            this.DeleteButtonColumn.UseColumnTextForButtonValue = true;
            this.DeleteButtonColumn.Width = 80;
            // 
            // autoSectionButton
            // 
            this.autoSectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.autoSectionButton.AutoEllipsis = true;
            this.autoSectionButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.autoSectionButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.autoSectionButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.autoSectionButton.ButtonClickColor = System.Drawing.Color.Green;
            this.autoSectionButton.ButtonCornorRadius = 3;
            this.autoSectionButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.autoSectionButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.autoSectionButton.ForeColor = System.Drawing.Color.Black;
            this.autoSectionButton.IsButtonFoucs = false;
            this.autoSectionButton.Location = new System.Drawing.Point(437, 11);
            this.autoSectionButton.Name = "autoSectionButton";
            this.autoSectionButton.Size = new System.Drawing.Size(90, 30);
            this.autoSectionButton.TabIndex = 1;
            this.autoSectionButton.Text = "快速分段";
            this.autoSectionButton.Transparency = 50;
            this.autoSectionButton.UseVisualStyleBackColor = false;
            this.autoSectionButton.Click += new System.EventHandler(this.autoSectionButton_Click);
            // 
            // opticalProbeGroupBox
            // 
            this.opticalProbeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.opticalProbeGroupBox.Controls.Add(this.crystalButton_BrightTest);
            this.opticalProbeGroupBox.Controls.Add(this.refreshOpticalProbeCrystalButton);
            this.opticalProbeGroupBox.Controls.Add(this.opticalProbeDataGridView);
            this.opticalProbeGroupBox.Location = new System.Drawing.Point(0, 64);
            this.opticalProbeGroupBox.Name = "opticalProbeGroupBox";
            this.opticalProbeGroupBox.Size = new System.Drawing.Size(642, 128);
            this.opticalProbeGroupBox.TabIndex = 2;
            this.opticalProbeGroupBox.TabStop = false;
            this.opticalProbeGroupBox.Text = "光探头配置表";
            // 
            // crystalButton_BrightTest
            // 
            this.crystalButton_BrightTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_BrightTest.AutoEllipsis = true;
            this.crystalButton_BrightTest.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_BrightTest.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_BrightTest.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_BrightTest.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_BrightTest.ButtonCornorRadius = 3;
            this.crystalButton_BrightTest.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_BrightTest.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_BrightTest.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_BrightTest.IsButtonFoucs = false;
            this.crystalButton_BrightTest.Location = new System.Drawing.Point(437, 10);
            this.crystalButton_BrightTest.Name = "crystalButton_BrightTest";
            this.crystalButton_BrightTest.Size = new System.Drawing.Size(90, 30);
            this.crystalButton_BrightTest.TabIndex = 101;
            this.crystalButton_BrightTest.Text = "光探头测试";
            this.crystalButton_BrightTest.Transparency = 50;
            this.crystalButton_BrightTest.UseVisualStyleBackColor = false;
            this.crystalButton_BrightTest.Click += new System.EventHandler(this.crystalButton_BrightTest_Click);
            // 
            // refreshOpticalProbeCrystalButton
            // 
            this.refreshOpticalProbeCrystalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshOpticalProbeCrystalButton.AutoEllipsis = true;
            this.refreshOpticalProbeCrystalButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.refreshOpticalProbeCrystalButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.refreshOpticalProbeCrystalButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.refreshOpticalProbeCrystalButton.ButtonClickColor = System.Drawing.Color.Green;
            this.refreshOpticalProbeCrystalButton.ButtonCornorRadius = 3;
            this.refreshOpticalProbeCrystalButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.refreshOpticalProbeCrystalButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.refreshOpticalProbeCrystalButton.ForeColor = System.Drawing.Color.Black;
            this.refreshOpticalProbeCrystalButton.IsButtonFoucs = false;
            this.refreshOpticalProbeCrystalButton.Location = new System.Drawing.Point(542, 10);
            this.refreshOpticalProbeCrystalButton.Name = "refreshOpticalProbeCrystalButton";
            this.refreshOpticalProbeCrystalButton.Size = new System.Drawing.Size(90, 30);
            this.refreshOpticalProbeCrystalButton.TabIndex = 2;
            this.refreshOpticalProbeCrystalButton.Text = "刷新";
            this.refreshOpticalProbeCrystalButton.Transparency = 50;
            this.refreshOpticalProbeCrystalButton.UseVisualStyleBackColor = false;
            this.refreshOpticalProbeCrystalButton.Click += new System.EventHandler(this.refreshOpticalProbeCrystalButton_Click);
            // 
            // opticalProbeDataGridView
            // 
            this.opticalProbeDataGridView.AllowUserToAddRows = false;
            this.opticalProbeDataGridView.AllowUserToDeleteRows = false;
            this.opticalProbeDataGridView.AllowUserToResizeColumns = false;
            this.opticalProbeDataGridView.AllowUserToResizeRows = false;
            this.opticalProbeDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.opticalProbeDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.opticalProbeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.opticalProbeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnableColumn,
            this.PositionColumn,
            this.CurrentBrightnessColumn,
            this.RemarkColumn});
            this.opticalProbeDataGridView.Location = new System.Drawing.Point(3, 43);
            this.opticalProbeDataGridView.Name = "opticalProbeDataGridView";
            this.opticalProbeDataGridView.RowHeadersVisible = false;
            this.opticalProbeDataGridView.RowTemplate.Height = 23;
            this.opticalProbeDataGridView.Size = new System.Drawing.Size(636, 79);
            this.opticalProbeDataGridView.TabIndex = 0;
            this.opticalProbeDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.opticalProbeDataGridView_CellContentClick);
            this.opticalProbeDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.opticalProbeDataGridView_CellValueChanged);
            this.opticalProbeDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.opticalProbeDataGridView_CurrentCellDirtyStateChanged);
            // 
            // EnableColumn
            // 
            this.EnableColumn.HeaderText = "是否启用";
            this.EnableColumn.Name = "EnableColumn";
            this.EnableColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EnableColumn.Width = 120;
            // 
            // PositionColumn
            // 
            this.PositionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PositionColumn.HeaderText = "位置";
            this.PositionColumn.Name = "PositionColumn";
            this.PositionColumn.ReadOnly = true;
            this.PositionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CurrentBrightnessColumn
            // 
            this.CurrentBrightnessColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CurrentBrightnessColumn.HeaderText = "当前亮度值";
            this.CurrentBrightnessColumn.Name = "CurrentBrightnessColumn";
            this.CurrentBrightnessColumn.ReadOnly = true;
            this.CurrentBrightnessColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RemarkColumn
            // 
            this.RemarkColumn.HeaderText = "备注";
            this.RemarkColumn.Name = "RemarkColumn";
            this.RemarkColumn.Width = 140;
            // 
            // frm_OpticalProbeConfig
            // 
            this.AcceptButton = this.confirmButton;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(653, 503);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.workspacePanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "frm_OpticalProbeConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "光探头配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_OpticalProbeConfig_FormClosing);
            this.workspacePanel.ResumeLayout(false);
            this.DisableGroupBox.ResumeLayout(false);
            this.DisableGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessNumericUpDown)).EndInit();
            this.mappingGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.brightnessDataGridView)).EndInit();
            this.opticalProbeGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opticalProbeDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel workspacePanel;
        private System.Windows.Forms.GroupBox mappingGroupBox;
        private System.Windows.Forms.DataGridView brightnessDataGridView;
        private System.Windows.Forms.GroupBox opticalProbeGroupBox;
        private System.Windows.Forms.DataGridView opticalProbeDataGridView;
        private Nova.Control.CrystalButton autoSectionButton;
        private Nova.Control.CrystalButton confirmButton;
        private Nova.Control.CrystalButton cancelButton;
        private Nova.Control.CrystalButton AddMappingItemButton;
        private System.Windows.Forms.GroupBox DisableGroupBox;
        private System.Windows.Forms.CheckBox enableCheckBox;
        private System.Windows.Forms.NumericUpDown brightnessNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox currentBrightnessTextBox;
        private System.Windows.Forms.Label label_CurrentLight;
        private Nova.Control.CrystalButton refreshOpticalProbeCrystalButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnvironmentBrightnessColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedBrightnessColumn;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteButtonColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnableColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PositionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentBrightnessColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemarkColumn;
        private Nova.Control.CrystalButton crystalButton_BrightTest;
    }
}