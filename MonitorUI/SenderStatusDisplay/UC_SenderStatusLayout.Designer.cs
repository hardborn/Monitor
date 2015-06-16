namespace Nova.Monitoring.UI.SenderStatusDisplay
{
    partial class UC_SenderStatusLayout
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
            ResourceDispose();
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.doubleBufferPanel_ScrollPanel = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.doubleBufferPanel_Draw = new Nova.Control.DoubleBufferControl.DoubleBufferPanel();
            this.doubleBufferPanel_ScrollPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // doubleBufferPanel_ScrollPanel
            // 
            this.doubleBufferPanel_ScrollPanel.AutoScroll = true;
            this.doubleBufferPanel_ScrollPanel.Controls.Add(this.doubleBufferPanel_Draw);
            this.doubleBufferPanel_ScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferPanel_ScrollPanel.Location = new System.Drawing.Point(0, 0);
            this.doubleBufferPanel_ScrollPanel.Name = "doubleBufferPanel_ScrollPanel";
            this.doubleBufferPanel_ScrollPanel.Size = new System.Drawing.Size(174, 143);
            this.doubleBufferPanel_ScrollPanel.TabIndex = 0;
            // 
            // doubleBufferPanel_Draw
            // 
            this.doubleBufferPanel_Draw.Location = new System.Drawing.Point(3, 0);
            this.doubleBufferPanel_Draw.Name = "doubleBufferPanel_Draw";
            this.doubleBufferPanel_Draw.Size = new System.Drawing.Size(146, 140);
            this.doubleBufferPanel_Draw.TabIndex = 0;
            this.doubleBufferPanel_Draw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.doubleBufferPanel_Draw_MouseMove);
            this.doubleBufferPanel_Draw.Paint += new System.Windows.Forms.PaintEventHandler(this.doubleBufferPanel_Draw_Paint);
            // 
            // UC_SenderStatusLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.doubleBufferPanel_ScrollPanel);
            this.Name = "UC_SenderStatusLayout";
            this.Size = new System.Drawing.Size(174, 143);
            this.doubleBufferPanel_ScrollPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.DoubleBufferControl.DoubleBufferPanel doubleBufferPanel_ScrollPanel;
        private Nova.Control.DoubleBufferControl.DoubleBufferPanel doubleBufferPanel_Draw;
    }
}
