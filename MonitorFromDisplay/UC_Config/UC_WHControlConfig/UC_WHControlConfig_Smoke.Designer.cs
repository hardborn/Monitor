namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_WHControlConfig_Smoke
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_smokeCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_smokeCount = new System.Windows.Forms.NumericUpDown();
            this.uCWHControlConfigSmokeVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.seperatePanel_smoke_action = new Nova.Control.Panel.SeperatePanel();
            this.label_ScreenList = new System.Windows.Forms.Label();
            this.checkBoxComboBox_ScreenList1 = new System.Windows.Forms.ComboBox();
            this.panel_PowerList = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_smokeCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCWHControlConfigSmokeVMBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label_smokeCount
            // 
            this.label_smokeCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_smokeCount.AutoEllipsis = true;
            this.label_smokeCount.Location = new System.Drawing.Point(1, 2);
            this.label_smokeCount.Name = "label_smokeCount";
            this.label_smokeCount.Size = new System.Drawing.Size(144, 58);
            this.label_smokeCount.TabIndex = 7;
            this.label_smokeCount.Text = "烟雾告警箱体个数";
            this.label_smokeCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.Location = new System.Drawing.Point(151, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = ">";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_smokeCount
            // 
            this.numericUpDown_smokeCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDown_smokeCount.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCWHControlConfigSmokeVMBindingSource, "GreaterThan", true));
            this.numericUpDown_smokeCount.Location = new System.Drawing.Point(184, 23);
            this.numericUpDown_smokeCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_smokeCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_smokeCount.Name = "numericUpDown_smokeCount";
            this.numericUpDown_smokeCount.Size = new System.Drawing.Size(127, 20);
            this.numericUpDown_smokeCount.TabIndex = 11;
            this.numericUpDown_smokeCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown_smokeCount.ValueChanged += new System.EventHandler(this.numericUpDown_smokeCount_ValueChanged);
            // 
            // uCWHControlConfigSmokeVMBindingSource
            // 
            this.uCWHControlConfigSmokeVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_WHControlConfig_Smoke_VM);
            // 
            // seperatePanel_smoke_action
            // 
            this.seperatePanel_smoke_action.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_smoke_action.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_smoke_action.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_smoke_action.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_smoke_action.Location = new System.Drawing.Point(14, 61);
            this.seperatePanel_smoke_action.Name = "seperatePanel_smoke_action";
            this.seperatePanel_smoke_action.SeperateLineHeight = 2;
            this.seperatePanel_smoke_action.Size = new System.Drawing.Size(409, 21);
            this.seperatePanel_smoke_action.TabIndex = 33;
            this.seperatePanel_smoke_action.Text = "动作";
            // 
            // label_ScreenList
            // 
            this.label_ScreenList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_ScreenList.Location = new System.Drawing.Point(36, 191);
            this.label_ScreenList.Name = "label_ScreenList";
            this.label_ScreenList.Size = new System.Drawing.Size(117, 25);
            this.label_ScreenList.TabIndex = 24;
            this.label_ScreenList.Text = "显示屏列表";
            this.label_ScreenList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_ScreenList.Visible = false;
            // 
            // checkBoxComboBox_ScreenList1
            // 
            this.checkBoxComboBox_ScreenList1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkBoxComboBox_ScreenList1.FormattingEnabled = true;
            this.checkBoxComboBox_ScreenList1.Location = new System.Drawing.Point(192, 193);
            this.checkBoxComboBox_ScreenList1.Name = "checkBoxComboBox_ScreenList1";
            this.checkBoxComboBox_ScreenList1.Size = new System.Drawing.Size(121, 21);
            this.checkBoxComboBox_ScreenList1.TabIndex = 35;
            this.checkBoxComboBox_ScreenList1.Visible = false;
            // 
            // panel_PowerList
            // 
            this.panel_PowerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_PowerList.Location = new System.Drawing.Point(5, 83);
            this.panel_PowerList.Name = "panel_PowerList";
            this.panel_PowerList.Size = new System.Drawing.Size(432, 89);
            this.panel_PowerList.TabIndex = 36;
            // 
            // UC_WHControlConfig_Smoke
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.panel_PowerList);
            this.Controls.Add(this.checkBoxComboBox_ScreenList1);
            this.Controls.Add(this.seperatePanel_smoke_action);
            this.Controls.Add(this.label_ScreenList);
            this.Controls.Add(this.numericUpDown_smokeCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_smokeCount);
            this.Name = "UC_WHControlConfig_Smoke";
            this.Size = new System.Drawing.Size(440, 181);
            this.Controls.SetChildIndex(this.panel_ConfigBase, 0);
            this.Controls.SetChildIndex(this.label_smokeCount, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.numericUpDown_smokeCount, 0);
            this.Controls.SetChildIndex(this.label_ScreenList, 0);
            this.Controls.SetChildIndex(this.seperatePanel_smoke_action, 0);
            this.Controls.SetChildIndex(this.checkBoxComboBox_ScreenList1, 0);
            this.Controls.SetChildIndex(this.panel_PowerList, 0);
            this.Controls.SetChildIndex(this.crystalButton_OK, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_smokeCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCWHControlConfigSmokeVMBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_smokeCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_smokeCount;
        private Nova.Control.Panel.SeperatePanel seperatePanel_smoke_action;
        private System.Windows.Forms.Label label_ScreenList;
        private System.Windows.Forms.ComboBox checkBoxComboBox_ScreenList1;
        private System.Windows.Forms.Panel panel_PowerList;
        private System.Windows.Forms.BindingSource uCWHControlConfigSmokeVMBindingSource;
    }
}
