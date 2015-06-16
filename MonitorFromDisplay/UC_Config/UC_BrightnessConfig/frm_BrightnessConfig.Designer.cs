namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class frm_BrightnessConfig
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
            this.button_OK = new Nova.Control.CrystalButton();
            this.button_cancel = new Nova.Control.CrystalButton();
            this.panel_Main = new System.Windows.Forms.Panel();
            this.tabControl_Brightness = new System.Windows.Forms.TabControl();
            this.tabPage_Fasten = new System.Windows.Forms.TabPage();
            this.tabPage_Auto = new System.Windows.Forms.TabPage();
            this.panel_Main.SuspendLayout();
            this.tabControl_Brightness.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.AutoEllipsis = true;
            this.button_OK.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_OK.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.button_OK.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_OK.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_OK.ButtonCornorRadius = 3;
            this.button_OK.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_OK.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_OK.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_OK.IsButtonFoucs = false;
            this.button_OK.Location = new System.Drawing.Point(263, 202);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 28);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "确定";
            this.button_OK.Transparency = 50;
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.AutoEllipsis = true;
            this.button_cancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_cancel.BottonActivatedColor = System.Drawing.Color.DeepSkyBlue;
            this.button_cancel.ButtonBusyBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_cancel.ButtonClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_cancel.ButtonCornorRadius = 3;
            this.button_cancel.ButtonFreeBorderColor = System.Drawing.Color.LightSlateGray;
            this.button_cancel.ButtonSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_cancel.IsButtonFoucs = false;
            this.button_cancel.Location = new System.Drawing.Point(369, 202);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 28);
            this.button_cancel.TabIndex = 1;
            this.button_cancel.Text = "取消";
            this.button_cancel.Transparency = 50;
            this.button_cancel.UseVisualStyleBackColor = false;
            // 
            // panel_Main
            // 
            this.panel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Main.Controls.Add(this.tabControl_Brightness);
            this.panel_Main.Location = new System.Drawing.Point(8, 6);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(447, 190);
            this.panel_Main.TabIndex = 2;
            // 
            // tabControl_Brightness
            // 
            this.tabControl_Brightness.Controls.Add(this.tabPage_Fasten);
            this.tabControl_Brightness.Controls.Add(this.tabPage_Auto);
            this.tabControl_Brightness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Brightness.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Brightness.Name = "tabControl_Brightness";
            this.tabControl_Brightness.SelectedIndex = 0;
            this.tabControl_Brightness.Size = new System.Drawing.Size(447, 190);
            this.tabControl_Brightness.TabIndex = 0;
            this.tabControl_Brightness.SelectedIndexChanged += new System.EventHandler(this.tabControl_Brightness_SelectedIndexChanged);
            // 
            // tabPage_Fasten
            // 
            this.tabPage_Fasten.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Fasten.Name = "tabPage_Fasten";
            this.tabPage_Fasten.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Fasten.Size = new System.Drawing.Size(439, 164);
            this.tabPage_Fasten.TabIndex = 0;
            this.tabPage_Fasten.Text = "固定亮度";
            this.tabPage_Fasten.UseVisualStyleBackColor = true;
            // 
            // tabPage_Auto
            // 
            this.tabPage_Auto.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Auto.Name = "tabPage_Auto";
            this.tabPage_Auto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Auto.Size = new System.Drawing.Size(439, 164);
            this.tabPage_Auto.TabIndex = 1;
            this.tabPage_Auto.Text = "自动亮度";
            this.tabPage_Auto.UseVisualStyleBackColor = true;
            // 
            // frm_BrightnessConfig
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(462, 234);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BrightnessConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置亮度";
            this.Load += new System.EventHandler(this.frm_BrightnessConfig_Load);
            this.panel_Main.ResumeLayout(false);
            this.tabControl_Brightness.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.CrystalButton button_OK;
        private Nova.Control.CrystalButton button_cancel;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.TabControl tabControl_Brightness;
        private System.Windows.Forms.TabPage tabPage_Fasten;
        private System.Windows.Forms.TabPage tabPage_Auto;
    }
}