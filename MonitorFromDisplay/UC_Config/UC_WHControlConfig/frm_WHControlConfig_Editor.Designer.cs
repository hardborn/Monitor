namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class frm_WHControlConfig_Editor
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
            this.panel_CtrlInfo = new System.Windows.Forms.Panel();
            this.panel_ControlContent = new System.Windows.Forms.Panel();
            this.comboBox_ControlType = new System.Windows.Forms.ComboBox();
            this.label_ControlType = new System.Windows.Forms.Label();
            this.button_cancel = new Nova.Control.CrystalButton();
            this.button_OK = new Nova.Control.CrystalButton();
            this.label_Tip = new System.Windows.Forms.Label();
            this.panel_CtrlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_CtrlInfo
            // 
            this.panel_CtrlInfo.Controls.Add(this.panel_ControlContent);
            this.panel_CtrlInfo.Controls.Add(this.comboBox_ControlType);
            this.panel_CtrlInfo.Controls.Add(this.label_ControlType);
            this.panel_CtrlInfo.Location = new System.Drawing.Point(2, 3);
            this.panel_CtrlInfo.Name = "panel_CtrlInfo";
            this.panel_CtrlInfo.Size = new System.Drawing.Size(590, 354);
            this.panel_CtrlInfo.TabIndex = 4;
            // 
            // panel_ControlContent
            // 
            this.panel_ControlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_ControlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_ControlContent.Location = new System.Drawing.Point(3, 35);
            this.panel_ControlContent.Name = "panel_ControlContent";
            this.panel_ControlContent.Size = new System.Drawing.Size(584, 316);
            this.panel_ControlContent.TabIndex = 2;
            // 
            // comboBox_ControlType
            // 
            this.comboBox_ControlType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox_ControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ControlType.FormattingEnabled = true;
            this.comboBox_ControlType.Location = new System.Drawing.Point(259, 8);
            this.comboBox_ControlType.Name = "comboBox_ControlType";
            this.comboBox_ControlType.Size = new System.Drawing.Size(127, 21);
            this.comboBox_ControlType.TabIndex = 1;
            // 
            // label_ControlType
            // 
            this.label_ControlType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_ControlType.AutoEllipsis = true;
            this.label_ControlType.Location = new System.Drawing.Point(110, 7);
            this.label_ControlType.Name = "label_ControlType";
            this.label_ControlType.Size = new System.Drawing.Size(147, 25);
            this.label_ControlType.TabIndex = 0;
            this.label_ControlType.Text = "控制类型";
            this.label_ControlType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.button_cancel.Location = new System.Drawing.Point(512, 370);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 28);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "取消";
            this.button_cancel.Transparency = 50;
            this.button_cancel.UseVisualStyleBackColor = false;
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
            this.button_OK.Location = new System.Drawing.Point(406, 370);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 28);
            this.button_OK.TabIndex = 6;
            this.button_OK.Text = "确定";
            this.button_OK.Transparency = 50;
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_Tip
            // 
            this.label_Tip.AutoEllipsis = true;
            this.label_Tip.ForeColor = System.Drawing.Color.Red;
            this.label_Tip.Location = new System.Drawing.Point(2, 374);
            this.label_Tip.Name = "label_Tip";
            this.label_Tip.Size = new System.Drawing.Size(244, 32);
            this.label_Tip.TabIndex = 7;
            this.label_Tip.Text = "提示：不同策略之间的温度差不能小于5℃！";
            // 
            // frm_WHControlConfig_Editor
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(595, 410);
            this.Controls.Add(this.label_Tip);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.panel_CtrlInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_WHControlConfig_Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "控制配置";
            this.Load += new System.EventHandler(this.WHControlConfig_Editor_Load);
            this.panel_CtrlInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_CtrlInfo;
        private System.Windows.Forms.Panel panel_ControlContent;
        private System.Windows.Forms.ComboBox comboBox_ControlType;
        private System.Windows.Forms.Label label_ControlType;
        private Nova.Control.CrystalButton button_cancel;
        private Nova.Control.CrystalButton button_OK;
        private System.Windows.Forms.Label label_Tip;
    }
}