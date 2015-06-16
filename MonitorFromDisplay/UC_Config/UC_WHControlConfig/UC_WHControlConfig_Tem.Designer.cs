namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class UC_WHControlConfig_Tem
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
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            this.numericUpDown_minTem = new System.Windows.Forms.NumericUpDown();
            this.uCWHControlConfigTemVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_maxTem = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_TemType = new System.Windows.Forms.ComboBox();
            this.label_tem_unit = new System.Windows.Forms.Label();
            this.seperatePanel_tem_action = new Nova.Control.Panel.SeperatePanel();
            this.numericUpDown_brightness = new System.Windows.Forms.NumericUpDown();
            this.label_brightness_unit = new System.Windows.Forms.Label();
            this.label_ScreenList = new System.Windows.Forms.Label();
            this.checkBoxComboBox_ScreenList = new PresentationControls.CheckBoxComboBox();
            this.groupBox_PwoerManager = new System.Windows.Forms.GroupBox();
            this.label_And = new System.Windows.Forms.Label();
            this.radioButton_OpenBrightness = new System.Windows.Forms.RadioButton();
            this.radioButton_Brightness = new System.Windows.Forms.RadioButton();
            this.groupBox_Brightness = new System.Windows.Forms.GroupBox();
            this.checkBox_Brightness = new System.Windows.Forms.CheckBox();
            this.panel_Brightness = new System.Windows.Forms.Panel();
            this.label_BrightTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_minTem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCWHControlConfigTemVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maxTem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_brightness)).BeginInit();
            this.groupBox_Brightness.SuspendLayout();
            this.panel_Brightness.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalButton_OK
            // 
            this.crystalButton_OK.Location = new System.Drawing.Point(380, 590);
            // 
            // numericUpDown_minTem
            // 
            this.numericUpDown_minTem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDown_minTem.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCWHControlConfigTemVMBindingSource, "GreaterThan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_minTem.Location = new System.Drawing.Point(234, 7);
            this.numericUpDown_minTem.Name = "numericUpDown_minTem";
            this.numericUpDown_minTem.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown_minTem.TabIndex = 10;
            this.numericUpDown_minTem.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_minTem.ValueChanged += new System.EventHandler(this.numericUpDown_minTem_ValueChanged);
            // 
            // uCWHControlConfigTemVMBindingSource
            // 
            this.uCWHControlConfigTemVMBindingSource.DataSource = typeof(Nova.Monitoring.MonitorDataManager.UC_WHControlConfig_Tem_VM);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Location = new System.Drawing.Point(212, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = ">";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_maxTem
            // 
            this.numericUpDown_maxTem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDown_maxTem.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCWHControlConfigTemVMBindingSource, "LessThan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_maxTem.Location = new System.Drawing.Point(234, 31);
            this.numericUpDown_maxTem.Name = "numericUpDown_maxTem";
            this.numericUpDown_maxTem.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown_maxTem.TabIndex = 10;
            this.numericUpDown_maxTem.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericUpDown_maxTem.ValueChanged += new System.EventHandler(this.numericUpDown_maxTem_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.Location = new System.Drawing.Point(212, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "<";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_TemType
            // 
            this.comboBox_TemType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox_TemType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.uCWHControlConfigTemVMBindingSource, "ConditionAlgor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboBox_TemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TemType.FormattingEnabled = true;
            this.comboBox_TemType.Location = new System.Drawing.Point(16, 22);
            this.comboBox_TemType.Name = "comboBox_TemType";
            this.comboBox_TemType.Size = new System.Drawing.Size(141, 21);
            this.comboBox_TemType.TabIndex = 11;
            this.comboBox_TemType.SelectedIndexChanged += new System.EventHandler(this.comboBox_TemType_SelectedIndexChanged);
            // 
            // label_tem_unit
            // 
            this.label_tem_unit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_tem_unit.Location = new System.Drawing.Point(338, 18);
            this.label_tem_unit.Name = "label_tem_unit";
            this.label_tem_unit.Size = new System.Drawing.Size(27, 25);
            this.label_tem_unit.TabIndex = 6;
            this.label_tem_unit.Text = "℃";
            this.label_tem_unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seperatePanel_tem_action
            // 
            this.seperatePanel_tem_action.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seperatePanel_tem_action.ColorGradualType = Nova.Control.Panel.GradualColorPanel.GradualType.LeftToRight;
            this.seperatePanel_tem_action.GradualStartColor = System.Drawing.Color.Blue;
            this.seperatePanel_tem_action.GradualStopColor = System.Drawing.Color.LightBlue;
            this.seperatePanel_tem_action.Location = new System.Drawing.Point(9, 55);
            this.seperatePanel_tem_action.Name = "seperatePanel_tem_action";
            this.seperatePanel_tem_action.SeperateLineHeight = 2;
            this.seperatePanel_tem_action.Size = new System.Drawing.Size(483, 19);
            this.seperatePanel_tem_action.TabIndex = 22;
            this.seperatePanel_tem_action.Text = "动作";
            // 
            // numericUpDown_brightness
            // 
            this.numericUpDown_brightness.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDown_brightness.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.uCWHControlConfigTemVMBindingSource, "Brightness", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_brightness.Location = new System.Drawing.Point(133, 11);
            this.numericUpDown_brightness.Name = "numericUpDown_brightness";
            this.numericUpDown_brightness.Size = new System.Drawing.Size(127, 20);
            this.numericUpDown_brightness.TabIndex = 10;
            this.numericUpDown_brightness.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericUpDown_brightness.ValueChanged += new System.EventHandler(this.numericUpDown_brightness_ValueChanged);
            // 
            // label_brightness_unit
            // 
            this.label_brightness_unit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_brightness_unit.Location = new System.Drawing.Point(274, 9);
            this.label_brightness_unit.Name = "label_brightness_unit";
            this.label_brightness_unit.Size = new System.Drawing.Size(27, 25);
            this.label_brightness_unit.TabIndex = 6;
            this.label_brightness_unit.Text = "%";
            this.label_brightness_unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_ScreenList
            // 
            this.label_ScreenList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_ScreenList.Location = new System.Drawing.Point(377, 31);
            this.label_ScreenList.Name = "label_ScreenList";
            this.label_ScreenList.Size = new System.Drawing.Size(117, 25);
            this.label_ScreenList.TabIndex = 35;
            this.label_ScreenList.Text = "显示屏列表";
            this.label_ScreenList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_ScreenList.Visible = false;
            // 
            // checkBoxComboBox_ScreenList
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxComboBox_ScreenList.CheckBoxProperties = checkBoxProperties1;
            this.checkBoxComboBox_ScreenList.DisplayMemberSingleItem = "";
            this.checkBoxComboBox_ScreenList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkBoxComboBox_ScreenList.FormattingEnabled = true;
            this.checkBoxComboBox_ScreenList.Location = new System.Drawing.Point(375, 7);
            this.checkBoxComboBox_ScreenList.Name = "checkBoxComboBox_ScreenList";
            this.checkBoxComboBox_ScreenList.Size = new System.Drawing.Size(127, 21);
            this.checkBoxComboBox_ScreenList.TabIndex = 36;
            this.checkBoxComboBox_ScreenList.Visible = false;
            // 
            // groupBox_PwoerManager
            // 
            this.groupBox_PwoerManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_PwoerManager.Location = new System.Drawing.Point(9, 133);
            this.groupBox_PwoerManager.Name = "groupBox_PwoerManager";
            this.groupBox_PwoerManager.Size = new System.Drawing.Size(483, 314);
            this.groupBox_PwoerManager.TabIndex = 37;
            this.groupBox_PwoerManager.TabStop = false;
            this.groupBox_PwoerManager.Text = "电源管理";
            // 
            // label_And
            // 
            this.label_And.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_And.AutoEllipsis = true;
            this.label_And.Location = new System.Drawing.Point(163, 17);
            this.label_And.Name = "label_And";
            this.label_And.Size = new System.Drawing.Size(41, 35);
            this.label_And.TabIndex = 6;
            this.label_And.Text = "且";
            this.label_And.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButton_OpenBrightness
            // 
            this.radioButton_OpenBrightness.Location = new System.Drawing.Point(305, 4);
            this.radioButton_OpenBrightness.Name = "radioButton_OpenBrightness";
            this.radioButton_OpenBrightness.Size = new System.Drawing.Size(32, 17);
            this.radioButton_OpenBrightness.TabIndex = 38;
            this.radioButton_OpenBrightness.Text = "智能亮度";
            this.radioButton_OpenBrightness.UseVisualStyleBackColor = true;
            this.radioButton_OpenBrightness.Visible = false;
            // 
            // radioButton_Brightness
            // 
            this.radioButton_Brightness.Checked = true;
            this.radioButton_Brightness.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.uCWHControlConfigTemVMBindingSource, "IsControlBrightness", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioButton_Brightness.Location = new System.Drawing.Point(308, 27);
            this.radioButton_Brightness.Name = "radioButton_Brightness";
            this.radioButton_Brightness.Size = new System.Drawing.Size(40, 17);
            this.radioButton_Brightness.TabIndex = 38;
            this.radioButton_Brightness.TabStop = true;
            this.radioButton_Brightness.Text = "亮度调整到";
            this.radioButton_Brightness.UseVisualStyleBackColor = true;
            this.radioButton_Brightness.Visible = false;
            this.radioButton_Brightness.CheckedChanged += new System.EventHandler(this.radioButton_Brightness_CheckedChanged);
            // 
            // groupBox_Brightness
            // 
            this.groupBox_Brightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Brightness.Controls.Add(this.checkBox_Brightness);
            this.groupBox_Brightness.Controls.Add(this.panel_Brightness);
            this.groupBox_Brightness.Location = new System.Drawing.Point(2, 74);
            this.groupBox_Brightness.Name = "groupBox_Brightness";
            this.groupBox_Brightness.Size = new System.Drawing.Size(496, 58);
            this.groupBox_Brightness.TabIndex = 37;
            this.groupBox_Brightness.TabStop = false;
            this.groupBox_Brightness.Text = "亮度调节";
            // 
            // checkBox_Brightness
            // 
            this.checkBox_Brightness.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_Brightness.AutoEllipsis = true;
            this.checkBox_Brightness.Location = new System.Drawing.Point(24, 15);
            this.checkBox_Brightness.Name = "checkBox_Brightness";
            this.checkBox_Brightness.Size = new System.Drawing.Size(122, 38);
            this.checkBox_Brightness.TabIndex = 1;
            this.checkBox_Brightness.Text = "亮度调节";
            this.checkBox_Brightness.UseVisualStyleBackColor = true;
            this.checkBox_Brightness.CheckedChanged += new System.EventHandler(this.checkBox_Brightness_CheckedChanged);
            // 
            // panel_Brightness
            // 
            this.panel_Brightness.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Brightness.Controls.Add(this.label_BrightTo);
            this.panel_Brightness.Controls.Add(this.radioButton_Brightness);
            this.panel_Brightness.Controls.Add(this.numericUpDown_brightness);
            this.panel_Brightness.Controls.Add(this.radioButton_OpenBrightness);
            this.panel_Brightness.Controls.Add(this.label_brightness_unit);
            this.panel_Brightness.Enabled = false;
            this.panel_Brightness.Location = new System.Drawing.Point(155, 12);
            this.panel_Brightness.Name = "panel_Brightness";
            this.panel_Brightness.Size = new System.Drawing.Size(335, 43);
            this.panel_Brightness.TabIndex = 0;
            // 
            // label_BrightTo
            // 
            this.label_BrightTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_BrightTo.AutoEllipsis = true;
            this.label_BrightTo.Location = new System.Drawing.Point(8, 3);
            this.label_BrightTo.Name = "label_BrightTo";
            this.label_BrightTo.Size = new System.Drawing.Size(119, 37);
            this.label_BrightTo.TabIndex = 39;
            this.label_BrightTo.Text = "亮度调整到";
            this.label_BrightTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UC_WHControlConfig_Tem
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.groupBox_PwoerManager);
            this.Controls.Add(this.groupBox_Brightness);
            this.Controls.Add(this.checkBoxComboBox_ScreenList);
            this.Controls.Add(this.label_ScreenList);
            this.Controls.Add(this.seperatePanel_tem_action);
            this.Controls.Add(this.comboBox_TemType);
            this.Controls.Add(this.numericUpDown_maxTem);
            this.Controls.Add(this.numericUpDown_minTem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_tem_unit);
            this.Controls.Add(this.label_And);
            this.Controls.Add(this.label1);
            this.Name = "UC_WHControlConfig_Tem";
            this.Controls.SetChildIndex(this.panel_ConfigBase, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label_And, 0);
            this.Controls.SetChildIndex(this.label_tem_unit, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.numericUpDown_minTem, 0);
            this.Controls.SetChildIndex(this.numericUpDown_maxTem, 0);
            this.Controls.SetChildIndex(this.comboBox_TemType, 0);
            this.Controls.SetChildIndex(this.seperatePanel_tem_action, 0);
            this.Controls.SetChildIndex(this.label_ScreenList, 0);
            this.Controls.SetChildIndex(this.checkBoxComboBox_ScreenList, 0);
            this.Controls.SetChildIndex(this.groupBox_Brightness, 0);
            this.Controls.SetChildIndex(this.crystalButton_OK, 0);
            this.Controls.SetChildIndex(this.groupBox_PwoerManager, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_minTem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCWHControlConfigTemVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maxTem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_brightness)).EndInit();
            this.groupBox_Brightness.ResumeLayout(false);
            this.panel_Brightness.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown_minTem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_maxTem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_TemType;
        private System.Windows.Forms.Label label_tem_unit;
        private Nova.Control.Panel.SeperatePanel seperatePanel_tem_action;
        private System.Windows.Forms.NumericUpDown numericUpDown_brightness;
        private System.Windows.Forms.Label label_brightness_unit;
        private System.Windows.Forms.Label label_ScreenList;
        private PresentationControls.CheckBoxComboBox checkBoxComboBox_ScreenList;
        private System.Windows.Forms.GroupBox groupBox_PwoerManager;
        private System.Windows.Forms.BindingSource uCWHControlConfigTemVMBindingSource;
        private System.Windows.Forms.Label label_And;
        private System.Windows.Forms.RadioButton radioButton_OpenBrightness;
        private System.Windows.Forms.RadioButton radioButton_Brightness;
        private System.Windows.Forms.GroupBox groupBox_Brightness;
        private System.Windows.Forms.Panel panel_Brightness;
        private System.Windows.Forms.CheckBox checkBox_Brightness;
        private System.Windows.Forms.Label label_BrightTo;

    }
}
