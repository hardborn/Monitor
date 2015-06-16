namespace Nova.Monitoring.UI.MonitorSetting
{
    partial class Frm_FanPowerAdvanceSetting
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
            this.tabControl_ScreenLayout = new System.Windows.Forms.TabControl();
            this.tabPage_Scree2 = new System.Windows.Forms.TabPage();
            this.label_Notice = new System.Windows.Forms.Label();
            this.crystalButton_OK = new Nova.Control.CrystalButton();
            this.crystalButton_Cancel = new Nova.Control.CrystalButton();
            this.tabControl_ScreenLayout.SuspendLayout();
            this.tabPage_Scree2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_ScreenLayout
            // 
            this.tabControl_ScreenLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_ScreenLayout.Controls.Add(this.tabPage_Scree2);
            this.tabControl_ScreenLayout.Location = new System.Drawing.Point(12, 12);
            this.tabControl_ScreenLayout.Name = "tabControl_ScreenLayout";
            this.tabControl_ScreenLayout.SelectedIndex = 0;
            this.tabControl_ScreenLayout.Size = new System.Drawing.Size(525, 340);
            this.tabControl_ScreenLayout.TabIndex = 0;
            // 
            // tabPage_Scree2
            // 
            this.tabPage_Scree2.Controls.Add(this.label_Notice);
            this.tabPage_Scree2.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Scree2.Name = "tabPage_Scree2";
            this.tabPage_Scree2.Size = new System.Drawing.Size(517, 314);
            this.tabPage_Scree2.TabIndex = 1;
            this.tabPage_Scree2.Text = "屏2";
            this.tabPage_Scree2.UseVisualStyleBackColor = true;
            // 
            // label_Notice
            // 
            this.label_Notice.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Notice.ForeColor = System.Drawing.Color.Green;
            this.label_Notice.Location = new System.Drawing.Point(84, 96);
            this.label_Notice.Name = "label_Notice";
            this.label_Notice.Size = new System.Drawing.Size(316, 89);
            this.label_Notice.TabIndex = 0;
            this.label_Notice.Text = "当前无屏体信息!";
            this.label_Notice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_OK.AutoEllipsis = true;
            this.crystalButton_OK.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_OK.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_OK.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_OK.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_OK.ButtonCornorRadius = 3;
            this.crystalButton_OK.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_OK.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_OK.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_OK.IsButtonFoucs = false;
            this.crystalButton_OK.Location = new System.Drawing.Point(357, 358);
            this.crystalButton_OK.Name = "crystalButton_OK";
            this.crystalButton_OK.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_OK.TabIndex = 0;
            this.crystalButton_OK.Text = "确定";
            this.crystalButton_OK.Transparency = 50;
            this.crystalButton_OK.UseVisualStyleBackColor = false;
            this.crystalButton_OK.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // crystalButton_Cancel
            // 
            this.crystalButton_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_Cancel.AutoEllipsis = true;
            this.crystalButton_Cancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_Cancel.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Cancel.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Cancel.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_Cancel.ButtonCornorRadius = 3;
            this.crystalButton_Cancel.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Cancel.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Cancel.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_Cancel.IsButtonFoucs = false;
            this.crystalButton_Cancel.Location = new System.Drawing.Point(455, 358);
            this.crystalButton_Cancel.Name = "crystalButton_Cancel";
            this.crystalButton_Cancel.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_Cancel.TabIndex = 1;
            this.crystalButton_Cancel.Text = "取消";
            this.crystalButton_Cancel.Transparency = 50;
            this.crystalButton_Cancel.UseVisualStyleBackColor = false;
            this.crystalButton_Cancel.Click += new System.EventHandler(this.crystalButton_Cancel_Click);
            // 
            // Frm_FanPowerAdvanceSetting
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(549, 400);
            this.Controls.Add(this.crystalButton_OK);
            this.Controls.Add(this.crystalButton_Cancel);
            this.Controls.Add(this.tabControl_ScreenLayout);
            this.DoubleBuffered = true;
            this.Name = "Frm_FanPowerAdvanceSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "监控高级设置";
            this.Load += new System.EventHandler(this.Frm_AdvanceSetting_Load);
            this.tabControl_ScreenLayout.ResumeLayout(false);
            this.tabPage_Scree2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_ScreenLayout;
        private System.Windows.Forms.TabPage tabPage_Scree2;
        private System.Windows.Forms.Label label_Notice;
        private Nova.Control.CrystalButton crystalButton_OK;
        private Nova.Control.CrystalButton crystalButton_Cancel;
    }
}