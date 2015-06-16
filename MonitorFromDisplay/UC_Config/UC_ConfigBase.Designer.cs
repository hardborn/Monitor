namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_ConfigBase
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
            UnRegister();
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.crystalButton_OK = new Nova.Control.CrystalButton();
            this.panel_ConfigBase = new System.Windows.Forms.Panel();
            this.SuspendLayout();
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
            this.crystalButton_OK.Location = new System.Drawing.Point(385, 402);
            this.crystalButton_OK.Name = "crystalButton_OK";
            this.crystalButton_OK.Size = new System.Drawing.Size(90, 30);
            this.crystalButton_OK.TabIndex = 97;
            this.crystalButton_OK.Text = "保存";
            this.crystalButton_OK.Transparency = 50;
            this.crystalButton_OK.UseVisualStyleBackColor = false;
            // 
            // panel_ConfigBase
            // 
            this.panel_ConfigBase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_ConfigBase.Location = new System.Drawing.Point(14, 7);
            this.panel_ConfigBase.Name = "panel_ConfigBase";
            this.panel_ConfigBase.Size = new System.Drawing.Size(473, 380);
            this.panel_ConfigBase.TabIndex = 98;
            // 
            // UC_ConfigBase
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.panel_ConfigBase);
            this.Controls.Add(this.crystalButton_OK);
            this.Name = "UC_ConfigBase";
            this.Size = new System.Drawing.Size(500, 450);
            this.VisibleChanged += new System.EventHandler(this.UC_ConfigBase_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel_ConfigBase;
        protected Nova.Control.CrystalButton crystalButton_OK;
    }
}
