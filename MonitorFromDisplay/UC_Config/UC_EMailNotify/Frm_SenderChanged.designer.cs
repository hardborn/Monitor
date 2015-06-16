namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_SenderChanged
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SenderChanged));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_SSLOpen = new System.Windows.Forms.CheckBox();
            this.label_SSl = new System.Windows.Forms.Label();
            this.numberTextBox_port = new Nova.Control.NumberTextBox();
            this.textBox_smtpServer = new System.Windows.Forms.TextBox();
            this.textBox_passWord = new System.Windows.Forms.TextBox();
            this.textBox_emailAddr = new System.Windows.Forms.TextBox();
            this.label_smtpPort = new System.Windows.Forms.Label();
            this.label_smtpServer = new System.Windows.Forms.Label();
            this.label_emailPw = new System.Windows.Forms.Label();
            this.label_userAddr = new System.Windows.Forms.Label();
            this.crystalButton_close = new Nova.Control.CrystalButton();
            this.crystalButton_apply = new Nova.Control.CrystalButton();
            this.errorProvider_showError = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_showError)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_SSLOpen);
            this.groupBox1.Controls.Add(this.label_SSl);
            this.groupBox1.Controls.Add(this.numberTextBox_port);
            this.groupBox1.Controls.Add(this.textBox_smtpServer);
            this.groupBox1.Controls.Add(this.textBox_passWord);
            this.groupBox1.Controls.Add(this.textBox_emailAddr);
            this.groupBox1.Controls.Add(this.label_smtpPort);
            this.groupBox1.Controls.Add(this.label_smtpServer);
            this.groupBox1.Controls.Add(this.label_emailPw);
            this.groupBox1.Controls.Add(this.label_userAddr);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // checkBox_SSLOpen
            // 
            this.checkBox_SSLOpen.AutoEllipsis = true;
            this.checkBox_SSLOpen.Location = new System.Drawing.Point(134, 132);
            this.checkBox_SSLOpen.Name = "checkBox_SSLOpen";
            this.checkBox_SSLOpen.Size = new System.Drawing.Size(170, 20);
            this.checkBox_SSLOpen.TabIndex = 25;
            this.checkBox_SSLOpen.Text = "启用";
            this.checkBox_SSLOpen.UseVisualStyleBackColor = true;
            // 
            // label_SSl
            // 
            this.label_SSl.AutoEllipsis = true;
            this.label_SSl.Location = new System.Drawing.Point(20, 134);
            this.label_SSl.Name = "label_SSl";
            this.label_SSl.Size = new System.Drawing.Size(100, 20);
            this.label_SSl.TabIndex = 24;
            this.label_SSl.Text = "SSL加密：";
            // 
            // numberTextBox_port
            // 
            this.errorProvider_showError.SetIconPadding(this.numberTextBox_port, 4);
            this.numberTextBox_port.IsTrimZeroBefore = true;
            this.numberTextBox_port.Location = new System.Drawing.Point(134, 98);
            this.numberTextBox_port.Name = "numberTextBox_port";
            this.numberTextBox_port.Size = new System.Drawing.Size(170, 21);
            this.numberTextBox_port.TabIndex = 3;
            this.numberTextBox_port.Text = "25";
            this.numberTextBox_port.TextAlignChanged += new System.EventHandler(this.numberTextBox_port_Validating);
            // 
            // textBox_smtpServer
            // 
            this.errorProvider_showError.SetIconPadding(this.textBox_smtpServer, 4);
            this.textBox_smtpServer.Location = new System.Drawing.Point(134, 71);
            this.textBox_smtpServer.Name = "textBox_smtpServer";
            this.textBox_smtpServer.Size = new System.Drawing.Size(170, 21);
            this.textBox_smtpServer.TabIndex = 2;
            this.textBox_smtpServer.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_smtpServer_Validating);
            // 
            // textBox_passWord
            // 
            this.errorProvider_showError.SetIconPadding(this.textBox_passWord, 4);
            this.textBox_passWord.Location = new System.Drawing.Point(134, 44);
            this.textBox_passWord.Name = "textBox_passWord";
            this.textBox_passWord.PasswordChar = '*';
            this.textBox_passWord.Size = new System.Drawing.Size(170, 21);
            this.textBox_passWord.TabIndex = 1;
            this.textBox_passWord.UseSystemPasswordChar = true;
            this.textBox_passWord.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_passWord_Validating);
            // 
            // textBox_emailAddr
            // 
            this.errorProvider_showError.SetIconPadding(this.textBox_emailAddr, 4);
            this.textBox_emailAddr.Location = new System.Drawing.Point(134, 17);
            this.textBox_emailAddr.Name = "textBox_emailAddr";
            this.textBox_emailAddr.Size = new System.Drawing.Size(170, 21);
            this.textBox_emailAddr.TabIndex = 0;
            this.textBox_emailAddr.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_emailAddr_Validating);
            // 
            // label_smtpPort
            // 
            this.label_smtpPort.AutoEllipsis = true;
            this.label_smtpPort.Location = new System.Drawing.Point(20, 104);
            this.label_smtpPort.Name = "label_smtpPort";
            this.label_smtpPort.Size = new System.Drawing.Size(100, 20);
            this.label_smtpPort.TabIndex = 23;
            this.label_smtpPort.Text = "端口：";
            // 
            // label_smtpServer
            // 
            this.label_smtpServer.AutoEllipsis = true;
            this.label_smtpServer.Location = new System.Drawing.Point(20, 76);
            this.label_smtpServer.Name = "label_smtpServer";
            this.label_smtpServer.Size = new System.Drawing.Size(100, 20);
            this.label_smtpServer.TabIndex = 22;
            this.label_smtpServer.Text = "SMTP服务器：";
            // 
            // label_emailPw
            // 
            this.label_emailPw.AutoEllipsis = true;
            this.label_emailPw.Location = new System.Drawing.Point(20, 48);
            this.label_emailPw.Name = "label_emailPw";
            this.label_emailPw.Size = new System.Drawing.Size(100, 20);
            this.label_emailPw.TabIndex = 21;
            this.label_emailPw.Text = "邮箱密码：";
            // 
            // label_userAddr
            // 
            this.label_userAddr.AutoEllipsis = true;
            this.label_userAddr.Location = new System.Drawing.Point(20, 20);
            this.label_userAddr.Name = "label_userAddr";
            this.label_userAddr.Size = new System.Drawing.Size(100, 20);
            this.label_userAddr.TabIndex = 20;
            this.label_userAddr.Text = "邮箱地址：";
            // 
            // crystalButton_close
            // 
            this.crystalButton_close.AutoEllipsis = true;
            this.crystalButton_close.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_close.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_close.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_close.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_close.ButtonCornorRadius = 5;
            this.crystalButton_close.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_close.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.crystalButton_close.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_close.IsButtonFoucs = false;
            this.crystalButton_close.Location = new System.Drawing.Point(243, 166);
            this.crystalButton_close.Name = "crystalButton_close";
            this.crystalButton_close.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_close.TabIndex = 2;
            this.crystalButton_close.Text = "关闭";
            this.crystalButton_close.Transparency = 50;
            this.crystalButton_close.UseVisualStyleBackColor = false;
            // 
            // crystalButton_apply
            // 
            this.crystalButton_apply.AutoEllipsis = true;
            this.crystalButton_apply.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_apply.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_apply.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_apply.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_apply.ButtonCornorRadius = 5;
            this.crystalButton_apply.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_apply.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_apply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.crystalButton_apply.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_apply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_apply.IsButtonFoucs = false;
            this.crystalButton_apply.Location = new System.Drawing.Point(148, 166);
            this.crystalButton_apply.Name = "crystalButton_apply";
            this.crystalButton_apply.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_apply.TabIndex = 1;
            this.crystalButton_apply.Text = "修改";
            this.crystalButton_apply.Transparency = 50;
            this.crystalButton_apply.UseVisualStyleBackColor = false;
            this.crystalButton_apply.Click += new System.EventHandler(this.crystalButton_apply_Click);
            // 
            // errorProvider_showError
            // 
            this.errorProvider_showError.ContainerControl = this;
            this.errorProvider_showError.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider_showError.Icon")));
            // 
            // Frm_SenderChanged
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(344, 206);
            this.Controls.Add(this.crystalButton_close);
            this.Controls.Add(this.crystalButton_apply);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frm_SenderChanged";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改发布者";
            this.Load += new System.EventHandler(this.Frm_SenderChanged_Load);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.Frm_SenderChanged_Validating);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_showError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_smtpServer;
        private System.Windows.Forms.TextBox textBox_passWord;
        private System.Windows.Forms.TextBox textBox_emailAddr;
        private System.Windows.Forms.Label label_smtpPort;
        private System.Windows.Forms.Label label_smtpServer;
        private System.Windows.Forms.Label label_emailPw;
        private System.Windows.Forms.Label label_userAddr;
        private Nova.Control.CrystalButton crystalButton_close;
        private Nova.Control.CrystalButton crystalButton_apply;
        private System.Windows.Forms.ErrorProvider errorProvider_showError;
        private Nova.Control.NumberTextBox numberTextBox_port;
        private System.Windows.Forms.CheckBox checkBox_SSLOpen;
        private System.Windows.Forms.Label label_SSl;
    }
}