using Nova.Monitoring.MonitorDataManager;
namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_SmartBrightness
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
            uC_BrightnessConfigExample.CloseFormHandler -= uC_BrightnessConfigExample_CloseFormHandler;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SmartBrightness));
            this.crystalButton_Cancel = new Nova.Control.CrystalButton();
            this.uC_BrightnessConfigExample = new Nova.Monitoring.UI.MonitorFromDisplay.UC_BrightnessConfig();
            this.uC_BrightnessExample = new Nova.Monitoring.UI.MonitorFromDisplay.UC_Brightness();
            this.SuspendLayout();
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
            this.crystalButton_Cancel.Location = new System.Drawing.Point(646, 424);
            this.crystalButton_Cancel.Name = "crystalButton_Cancel";
            this.crystalButton_Cancel.Size = new System.Drawing.Size(90, 30);
            this.crystalButton_Cancel.TabIndex = 98;
            this.crystalButton_Cancel.Text = "取消";
            this.crystalButton_Cancel.Transparency = 50;
            this.crystalButton_Cancel.UseVisualStyleBackColor = false;
            this.crystalButton_Cancel.Click += new System.EventHandler(this.crystalButton_OK_Click);
            // 
            // uC_BrightnessConfigExample
            // 
            this.uC_BrightnessConfigExample.BackColor = System.Drawing.Color.AliceBlue;
            this.uC_BrightnessConfigExample.ConfigState = Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig.LightSensorConfigState.OK_State;
            this.uC_BrightnessConfigExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BrightnessConfigExample.HsLangTable = ((System.Collections.Hashtable)(resources.GetObject("uC_BrightnessConfigExample.HsLangTable")));
            this.uC_BrightnessConfigExample.Location = new System.Drawing.Point(0, 0);
            this.uC_BrightnessConfigExample.Name = "uC_BrightnessConfigExample";
            this.uC_BrightnessConfigExample.ScreenSN = null;
            this.uC_BrightnessConfigExample.Size = new System.Drawing.Size(759, 468);
            this.uC_BrightnessConfigExample.TabIndex = 99;
            // 
            // uC_BrightnessExample
            // 
            this.uC_BrightnessExample.BackColor = System.Drawing.Color.AliceBlue;
            this.uC_BrightnessExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BrightnessExample.HsLangTable = ((System.Collections.Hashtable)(resources.GetObject("uC_BrightnessExample.HsLangTable")));
            this.uC_BrightnessExample.Location = new System.Drawing.Point(0, 0);
            this.uC_BrightnessExample.Name = "uC_BrightnessExample";
            this.uC_BrightnessExample.ScreenSN = null;
            this.uC_BrightnessExample.Size = new System.Drawing.Size(759, 468);
            this.uC_BrightnessExample.TabIndex = 100;
            // 
            // Frm_SmartBrightness
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(759, 468);
            this.Controls.Add(this.crystalButton_Cancel);
            this.Controls.Add(this.uC_BrightnessExample);
            this.Controls.Add(this.uC_BrightnessConfigExample);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SmartBrightness";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能亮度调节";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SmartBrightness_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SmartBrightness_Load);
            this.ResumeLayout(false);

        }

        #endregion

        protected Nova.Control.CrystalButton crystalButton_Cancel;
        private UC_BrightnessConfig uC_BrightnessConfigExample;
        private UC_Brightness uC_BrightnessExample;
    }
}