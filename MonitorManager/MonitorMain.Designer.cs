namespace Nova.Monitoring.UI.MonitorManager
{
    partial class MonitorMain
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
            AutoDisposed();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorMain));
            this.contextMenuStrip_Main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_ReReadScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_OpenMain = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_OpenBrightAllConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip_Main
            // 
            this.contextMenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ReReadScreen,
            this.toolStripMenuItem_OpenMain,
            this.toolStripMenuItem_OpenBrightAllConfig,
            this.ToolStripMenuItem_Exit});
            this.contextMenuStrip_Main.Name = "contextMenuStrip_Main";
            this.contextMenuStrip_Main.Size = new System.Drawing.Size(165, 92);
            // 
            // toolStripMenuItem_ReReadScreen
            // 
            this.toolStripMenuItem_ReReadScreen.Name = "toolStripMenuItem_ReReadScreen";
            this.toolStripMenuItem_ReReadScreen.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem_ReReadScreen.Text = "重新读屏";
            this.toolStripMenuItem_ReReadScreen.Click += new System.EventHandler(this.toolStripMenuItem_ReReadScreen_Click);
            // 
            // toolStripMenuItem_OpenMain
            // 
            this.toolStripMenuItem_OpenMain.Name = "toolStripMenuItem_OpenMain";
            this.toolStripMenuItem_OpenMain.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem_OpenMain.Text = "打开用户界面(&O)";
            this.toolStripMenuItem_OpenMain.Click += new System.EventHandler(this.toolStripMenuItem_HeartbeatConfig_Click);
            // 
            // toolStripMenuItem_OpenBrightAllConfig
            // 
            this.toolStripMenuItem_OpenBrightAllConfig.Name = "toolStripMenuItem_OpenBrightAllConfig";
            this.toolStripMenuItem_OpenBrightAllConfig.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem_OpenBrightAllConfig.Text = "亮度高级配置";
            this.toolStripMenuItem_OpenBrightAllConfig.Click += new System.EventHandler(this.toolStripMenuItem_OpenBrightAllConfig_Click);
            // 
            // ToolStripMenuItem_Exit
            // 
            this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
            this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(164, 22);
            this.ToolStripMenuItem_Exit.Text = "退出";
            this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip_Main;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "MonitorSite";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // MonitorMain
            // 
            this.ClientSize = new System.Drawing.Size(283, 189);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MonitorMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监控终端";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonitorMain_FormClosing);
            this.Load += new System.EventHandler(this.MonitorMain_Load);
            this.contextMenuStrip_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_OpenMain;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ReReadScreen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_OpenBrightAllConfig;
    }
}