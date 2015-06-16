namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_ReceiverChanged
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ReceiverChanged));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_receiver = new System.Windows.Forms.TextBox();
            this.label_receverName = new System.Windows.Forms.Label();
            this.textBox_emailAddr = new System.Windows.Forms.TextBox();
            this.label_userAddr = new System.Windows.Forms.Label();
            this.errorProvider_showError = new System.Windows.Forms.ErrorProvider(this.components);
            this.crystalButton_close = new Nova.Control.CrystalButton();
            this.crystalButton_apply = new Nova.Control.CrystalButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_showError)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_receiver);
            this.groupBox1.Controls.Add(this.label_receverName);
            this.groupBox1.Controls.Add(this.textBox_emailAddr);
            this.groupBox1.Controls.Add(this.label_userAddr);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // textBox_receiver
            // 
            this.errorProvider_showError.SetIconPadding(this.textBox_receiver, 4);
            this.textBox_receiver.Location = new System.Drawing.Point(138, 17);
            this.textBox_receiver.Name = "textBox_receiver";
            this.textBox_receiver.Size = new System.Drawing.Size(170, 21);
            this.textBox_receiver.TabIndex = 0;
            this.textBox_receiver.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_receiver_Validating);
            // 
            // label_receverName
            // 
            this.label_receverName.AutoEllipsis = true;
            this.label_receverName.Location = new System.Drawing.Point(24, 20);
            this.label_receverName.Name = "label_receverName";
            this.label_receverName.Size = new System.Drawing.Size(100, 20);
            this.label_receverName.TabIndex = 27;
            this.label_receverName.Text = "收件人：";
            // 
            // textBox_emailAddr
            // 
            this.errorProvider_showError.SetIconPadding(this.textBox_emailAddr, 4);
            this.textBox_emailAddr.Location = new System.Drawing.Point(138, 46);
            this.textBox_emailAddr.Name = "textBox_emailAddr";
            this.textBox_emailAddr.Size = new System.Drawing.Size(170, 21);
            this.textBox_emailAddr.TabIndex = 1;
            this.textBox_emailAddr.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_emailAddr_Validating);
            // 
            // label_userAddr
            // 
            this.label_userAddr.AutoEllipsis = true;
            this.label_userAddr.Location = new System.Drawing.Point(24, 49);
            this.label_userAddr.Name = "label_userAddr";
            this.label_userAddr.Size = new System.Drawing.Size(100, 20);
            this.label_userAddr.TabIndex = 25;
            this.label_userAddr.Text = "邮箱地址：";
            // 
            // errorProvider_showError
            // 
            this.errorProvider_showError.ContainerControl = this;
            this.errorProvider_showError.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider_showError.Icon")));
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
            this.crystalButton_close.Location = new System.Drawing.Point(263, 98);
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
            this.crystalButton_apply.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_apply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_apply.IsButtonFoucs = false;
            this.crystalButton_apply.Location = new System.Drawing.Point(168, 98);
            this.crystalButton_apply.Name = "crystalButton_apply";
            this.crystalButton_apply.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_apply.TabIndex = 1;
            this.crystalButton_apply.Text = "添加";
            this.crystalButton_apply.Transparency = 50;
            this.crystalButton_apply.UseVisualStyleBackColor = false;
            this.crystalButton_apply.Click += new System.EventHandler(this.crystalButton_apply_Click);
            // 
            // Frm_ReceiverChanged
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(354, 138);
            this.Controls.Add(this.crystalButton_close);
            this.Controls.Add(this.crystalButton_apply);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frm_ReceiverChanged";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_ReceiverChanged";
            this.Load += new System.EventHandler(this.Frm_ReceiverChanged_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_showError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_receiver;
        private System.Windows.Forms.Label label_receverName;
        private System.Windows.Forms.TextBox textBox_emailAddr;
        private System.Windows.Forms.Label label_userAddr;
        private System.Windows.Forms.ErrorProvider errorProvider_showError;
        private Nova.Control.CrystalButton crystalButton_close;
        private Nova.Control.CrystalButton crystalButton_apply;
    }
}