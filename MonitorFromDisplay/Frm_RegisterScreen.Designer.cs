namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_RegisterScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_RegisterScreen));
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.dynamicFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.screenNameLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.registerButton = new Nova.Control.CrystalButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.userNameTextBox.Location = new System.Drawing.Point(109, 41);
            this.userNameTextBox.MaxLength = 32;
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(148, 20);
            this.userNameTextBox.TabIndex = 0;
            // 
            // dynamicFlowLayoutPanel
            // 
            this.dynamicFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dynamicFlowLayoutPanel.AutoScroll = true;
            this.dynamicFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dynamicFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.dynamicFlowLayoutPanel.Location = new System.Drawing.Point(3, 32);
            this.dynamicFlowLayoutPanel.Name = "dynamicFlowLayoutPanel";
            this.dynamicFlowLayoutPanel.Size = new System.Drawing.Size(478, 125);
            this.dynamicFlowLayoutPanel.TabIndex = 0;
            this.dynamicFlowLayoutPanel.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dynamicFlowLayoutPanel);
            this.panel1.Controls.Add(this.screenNameLabel);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 160);
            this.panel1.TabIndex = 3;
            // 
            // screenNameLabel
            // 
            this.screenNameLabel.AutoEllipsis = true;
            this.screenNameLabel.AutoSize = true;
            this.screenNameLabel.Location = new System.Drawing.Point(48, 12);
            this.screenNameLabel.Name = "screenNameLabel";
            this.screenNameLabel.Size = new System.Drawing.Size(91, 13);
            this.screenNameLabel.TabIndex = 1;
            this.screenNameLabel.Text = "给您的屏起名字";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(25, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.userNameTextBox);
            this.panel2.Controls.Add(this.userNameLabel);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(5, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(484, 82);
            this.panel2.TabIndex = 4;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(66, 39);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoEllipsis = true;
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(48, 9);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(67, 13);
            this.userNameLabel.TabIndex = 2;
            this.userNameLabel.Text = "输入用户名";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(25, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // registerButton
            // 
            this.registerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.registerButton.AutoEllipsis = true;
            this.registerButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.registerButton.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.registerButton.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.registerButton.ButtonClickColor = System.Drawing.Color.Green;
            this.registerButton.ButtonCornorRadius = 3;
            this.registerButton.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.registerButton.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.registerButton.ForeColor = System.Drawing.Color.Black;
            this.registerButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.registerButton.IsButtonFoucs = false;
            this.registerButton.Location = new System.Drawing.Point(393, 280);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(78, 24);
            this.registerButton.TabIndex = 11;
            this.registerButton.Text = "注册";
            this.registerButton.Transparency = 50;
            this.registerButton.UseVisualStyleBackColor = false;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // Frm_RegisterScreen
            // 
            this.ClientSize = new System.Drawing.Size(493, 316);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.Name = "Frm_RegisterScreen";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "屏体注册";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.FlowLayoutPanel dynamicFlowLayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label screenNameLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Nova.Control.CrystalButton registerButton;
    }
}