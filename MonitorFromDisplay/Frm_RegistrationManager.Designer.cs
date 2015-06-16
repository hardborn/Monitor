namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_RegistrationManager
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_RegistrationManager));
            this.linkLabel_About = new System.Windows.Forms.LinkLabel();
            this.refreshButton = new Nova.Control.CrystalButton();
            this.modifyRegistrationButton = new Nova.Control.CrystalButton();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.screenRegistationInfoDataGridView = new System.Windows.Forms.DataGridView();
            this.ScreenNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScreenHeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScreenWidthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScreenRegistationStateColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.userLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.screenRegistationInfoDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel_About
            // 
            this.linkLabel_About.AutoSize = true;
            this.linkLabel_About.Location = new System.Drawing.Point(18, 309);
            this.linkLabel_About.Name = "linkLabel_About";
            this.linkLabel_About.Size = new System.Drawing.Size(83, 12);
            this.linkLabel_About.TabIndex = 12;
            this.linkLabel_About.TabStop = true;
            this.linkLabel_About.Text = "关于NovaiCare";
            this.linkLabel_About.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_About_LinkClicked);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.AutoEllipsis = true;
            this.refreshButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.refreshButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.refreshButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.refreshButton.ButtonClickColor = System.Drawing.Color.Green;
            this.refreshButton.ButtonCornorRadius = 3;
            this.refreshButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.refreshButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.refreshButton.ForeColor = System.Drawing.Color.Black;
            this.refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refreshButton.IsButtonFoucs = false;
            this.refreshButton.Location = new System.Drawing.Point(445, 16);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(78, 24);
            this.refreshButton.TabIndex = 11;
            this.refreshButton.Text = "刷新";
            this.refreshButton.Transparency = 50;
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // modifyRegistrationButton
            // 
            this.modifyRegistrationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modifyRegistrationButton.AutoEllipsis = true;
            this.modifyRegistrationButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.modifyRegistrationButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.modifyRegistrationButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.modifyRegistrationButton.ButtonClickColor = System.Drawing.Color.Green;
            this.modifyRegistrationButton.ButtonCornorRadius = 3;
            this.modifyRegistrationButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.modifyRegistrationButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.modifyRegistrationButton.ForeColor = System.Drawing.Color.Black;
            this.modifyRegistrationButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.modifyRegistrationButton.IsButtonFoucs = false;
            this.modifyRegistrationButton.Location = new System.Drawing.Point(445, 303);
            this.modifyRegistrationButton.Name = "modifyRegistrationButton";
            this.modifyRegistrationButton.Size = new System.Drawing.Size(78, 24);
            this.modifyRegistrationButton.TabIndex = 10;
            this.modifyRegistrationButton.Text = "修改注册信息";
            this.modifyRegistrationButton.Transparency = 50;
            this.modifyRegistrationButton.UseVisualStyleBackColor = false;
            this.modifyRegistrationButton.Click += new System.EventHandler(this.modifyRegistrationButton_Click);
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(73, 23);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(0, 12);
            this.userNameLabel.TabIndex = 1;
            // 
            // screenRegistationInfoDataGridView
            // 
            this.screenRegistationInfoDataGridView.AllowUserToAddRows = false;
            this.screenRegistationInfoDataGridView.AllowUserToDeleteRows = false;
            this.screenRegistationInfoDataGridView.AllowUserToResizeRows = false;
            this.screenRegistationInfoDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screenRegistationInfoDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.screenRegistationInfoDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.screenRegistationInfoDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.screenRegistationInfoDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.screenRegistationInfoDataGridView.ColumnHeadersHeight = 30;
            this.screenRegistationInfoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ScreenNameColumn,
            this.ScreenHeightColumn,
            this.ScreenWidthColumn,
            this.ScreenRegistationStateColumn});
            this.screenRegistationInfoDataGridView.Location = new System.Drawing.Point(3, 60);
            this.screenRegistationInfoDataGridView.Name = "screenRegistationInfoDataGridView";
            this.screenRegistationInfoDataGridView.ReadOnly = true;
            this.screenRegistationInfoDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.screenRegistationInfoDataGridView.RowHeadersVisible = false;
            this.screenRegistationInfoDataGridView.RowHeadersWidth = 20;
            this.screenRegistationInfoDataGridView.RowTemplate.Height = 23;
            this.screenRegistationInfoDataGridView.Size = new System.Drawing.Size(542, 227);
            this.screenRegistationInfoDataGridView.TabIndex = 3;
            // 
            // ScreenNameColumn
            // 
            this.ScreenNameColumn.HeaderText = "屏名称";
            this.ScreenNameColumn.Name = "ScreenNameColumn";
            this.ScreenNameColumn.ReadOnly = true;
            // 
            // ScreenHeightColumn
            // 
            this.ScreenHeightColumn.HeaderText = "高度";
            this.ScreenHeightColumn.Name = "ScreenHeightColumn";
            this.ScreenHeightColumn.ReadOnly = true;
            // 
            // ScreenWidthColumn
            // 
            this.ScreenWidthColumn.HeaderText = "宽度";
            this.ScreenWidthColumn.Name = "ScreenWidthColumn";
            this.ScreenWidthColumn.ReadOnly = true;
            // 
            // ScreenRegistationStateColumn
            // 
            this.ScreenRegistationStateColumn.HeaderText = "注册状态";
            this.ScreenRegistationStateColumn.Name = "ScreenRegistationStateColumn";
            this.ScreenRegistationStateColumn.ReadOnly = true;
            this.ScreenRegistationStateColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // userLabel
            // 
            this.userLabel.AutoEllipsis = true;
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(31, 23);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(35, 12);
            this.userLabel.TabIndex = 0;
            this.userLabel.Text = "用户:";
            // 
            // Frm_RegistrationManager
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(548, 339);
            this.Controls.Add(this.linkLabel_About);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.modifyRegistrationButton);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.screenRegistationInfoDataGridView);
            this.Controls.Add(this.userLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_RegistrationManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Care注册";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_RegistrationManager_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.screenRegistationInfoDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.DataGridView screenRegistationInfoDataGridView;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScreenNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScreenHeightColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScreenWidthColumn;
        private System.Windows.Forms.DataGridViewImageColumn ScreenRegistationStateColumn;
        private Nova.Control.CrystalButton modifyRegistrationButton;
        private Nova.Control.CrystalButton refreshButton;
        private System.Windows.Forms.LinkLabel linkLabel_About;
    }
}