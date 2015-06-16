using Nova.LCT.GigabitSystem.Common;
using Nova.Monitoring.MonitorDataManager;
using Nova.Resource.Language;
using Nova.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class frm_BrightnessConfig : Frm_CommonBase
    {
        private List<ComboBoxItem> ComboBoxItemList;
        private BrightnessConfigInfo _brightnessCfg;
        private Hashtable _langTable;
        private List<BrightnessConfigInfo> _brightnessConfigList;
        private int _currentIndex = -1;
        public BrightnessConfigInfo BrightnessCfg
        {
            get { return _brightnessCfg; }
        }
        private List<DayOfWeek> WeekDayList = new List<DayOfWeek>()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };
        public frm_BrightnessConfig(BrightnessConfigInfo brightnessCfg, AutoBrightExtendData autoBrightData, List<BrightnessConfigInfo> brightnessConfigList)
        {
            InitializeComponent();
            UpdateLang(CommonUI.BrightnessConfigLangPath);
            ComboBoxItemList = new List<ComboBoxItem>() 
        {   new ComboBoxItem(BrightnessLangTable.CycleTypeTable[CycleType.everyday], CycleType.everyday), 
            new ComboBoxItem(BrightnessLangTable.CycleTypeTable[CycleType.workday], CycleType.workday), 
            new ComboBoxItem(BrightnessLangTable.CycleTypeTable[CycleType.userDefined], CycleType.userDefined) };
            if (brightnessCfg != null)
            {
                _brightnessCfg = brightnessCfg;
                _currentIndex = brightnessConfigList.FindIndex(a => a.Time.Equals(brightnessCfg.Time));
            }
            else _brightnessCfg = new BrightnessConfigInfo();
            if (autoBrightData == null || autoBrightData.UseLightSensorList == null || autoBrightData.UseLightSensorList.Count == 0)
            {
                tabControl_Brightness.TabPages.Remove(tabPage_Auto);
                _brightnessCfg.Type = SmartBrightAdjustType.FixBright;
            }
            _brightnessConfigList = brightnessConfigList;
        }
        #region 私有函数
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, this);
            MultiLanguageUtils.ReadLanguageResource(langResFile, "frm_BrightnessConfig_String", out _langTable);
        }
        private void InitialComboBox(ComboBox comboBox_Cycle)
        {
            if (comboBox_Cycle.Items.Count == 0)
            {
                comboBox_Cycle.DisplayMember = "Name";
                comboBox_Cycle.ValueMember = "Value";
                comboBox_Cycle.Items.AddRange(ComboBoxItemList.ToArray());
            }
            comboBox_Cycle.SelectedIndex = ComboBoxItemList.FindIndex(a => a.Value == _brightnessCfg.ExecutionCycle);
        }
        private void InitialTableLayoutPanel(CycleType cType, TabPage tPage)
        {
            foreach (var item in tPage.Controls)
            {
                tPage.Controls.Remove((System.Windows.Forms.Control)item);
            }
            TableLayoutPanel tPanel;
            AddControls(cType, out tPanel);
            tPage.Controls.Add(tPanel);
            tPanel.Dock = DockStyle.Fill;
        }
        private void AddControls(CycleType cType, out TableLayoutPanel tabPanel)
        {
            #region 初始化tabPanel
            tabPanel = new TableLayoutPanel();
            tabPanel.AutoSize = true;
            tabPanel.ColumnCount = 2;
            if (cType == CycleType.userDefined)
            {
                if (_brightnessCfg.Type == SmartBrightAdjustType.AutoBright)
                {
                    this.Size = new System.Drawing.Size(478, 242);
                    tabPanel.RowCount = 3;
                }
                else
                {
                    this.Size = new System.Drawing.Size(478, 272);
                    tabPanel.RowCount = 4;
                }
            }
            else
            {
                if (_brightnessCfg.Type == SmartBrightAdjustType.AutoBright)
                {
                    this.Size = new System.Drawing.Size(478, 202);
                    tabPanel.RowCount = 2;
                }
                else
                {
                    this.Size = new System.Drawing.Size(478, 242);
                    tabPanel.RowCount = 3;
                }
            }
            #endregion
            #region 初始化周期
            Panel panel_Cycle_Text = new Panel();
            panel_Cycle_Text.Location = new Point(0, 3);
            panel_Cycle_Text.Size = new System.Drawing.Size(100, 24);
            Label label_Cycle = new Label();
            panel_Cycle_Text.Controls.Add(label_Cycle);
            label_Cycle.Dock = DockStyle.Fill;
            label_Cycle.TextAlign = ContentAlignment.MiddleRight;
            label_Cycle.Text = CommonUI.GetCustomMessage(_langTable, "executecycle", "执行周期");

            Panel panel_Cycle = new Panel();
            panel_Cycle.Location = new Point(0, 3);
            panel_Cycle.Size = new System.Drawing.Size(298, 24);
            ComboBox comboBox_Cycle = new ComboBox();
            comboBox_Cycle.Size = new Size(150, 21);
            comboBox_Cycle.DropDownStyle = ComboBoxStyle.DropDownList;
            InitialComboBox(comboBox_Cycle);
            comboBox_Cycle.SelectedIndexChanged += comboBox_Cycle_SelectedIndexChanged;
            panel_Cycle.Controls.Add(comboBox_Cycle);
            #endregion
            #region 初始化星期
            Panel panel_Week = new Panel();
            panel_Week.Location = new Point(3, 3);
            panel_Week.Size = new System.Drawing.Size(320, 55);
            CheckBox monday = new CheckBox();
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Monday))
                monday.Checked = true;
            monday.CheckedChanged += CheckBox_CheckedChanged;
            monday.Text = BrightnessLangTable.DayTable[DayOfWeek.Monday];
            monday.Name = DayOfWeek.Monday.ToString();
            monday.AutoSize = true;
            CheckBox tuesday = new CheckBox();
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Tuesday))
                tuesday.Checked = true;
            tuesday.CheckedChanged += CheckBox_CheckedChanged;
            tuesday.Text = BrightnessLangTable.DayTable[DayOfWeek.Tuesday];
            tuesday.Name = DayOfWeek.Tuesday.ToString();
            CheckBox wednesday = new CheckBox();
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Wednesday))
                wednesday.Checked = true;
            wednesday.CheckedChanged += CheckBox_CheckedChanged;
            wednesday.Text = BrightnessLangTable.DayTable[DayOfWeek.Wednesday];
            wednesday.Name = DayOfWeek.Wednesday.ToString();
            CheckBox thursday = new CheckBox();
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Thursday))
                thursday.Checked = true;
            thursday.CheckedChanged += CheckBox_CheckedChanged;
            thursday.Text = BrightnessLangTable.DayTable[DayOfWeek.Thursday];
            thursday.Name = DayOfWeek.Thursday.ToString();
            CheckBox friday = new CheckBox();
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Friday))
                friday.Checked = true;
            friday.CheckedChanged += CheckBox_CheckedChanged;
            friday.Text = BrightnessLangTable.DayTable[DayOfWeek.Friday];
            friday.Name = DayOfWeek.Friday.ToString();
            CheckBox saturday = new CheckBox();
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Saturday))
                saturday.Checked = true;
            saturday.CheckedChanged += CheckBox_CheckedChanged;
            saturday.Text = BrightnessLangTable.DayTable[DayOfWeek.Saturday];
            saturday.Name = DayOfWeek.Saturday.ToString();
            CheckBox sunday = new CheckBox();
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Sunday))
                sunday.Checked = true;
            sunday.CheckedChanged += CheckBox_CheckedChanged;
            sunday.Text = BrightnessLangTable.DayTable[DayOfWeek.Sunday];
            sunday.Name = DayOfWeek.Sunday.ToString();
            panel_Week.Controls.Add(monday);
            panel_Week.Controls.Add(tuesday);
            panel_Week.Controls.Add(wednesday);
            panel_Week.Controls.Add(thursday);
            panel_Week.Controls.Add(friday);
            panel_Week.Controls.Add(saturday);
            panel_Week.Controls.Add(sunday);

            monday.AutoSize = true;
            tuesday.AutoSize = true;
            wednesday.AutoSize = true;
            thursday.AutoSize = true;
            friday.AutoSize = true;
            saturday.AutoSize = true;
            sunday.AutoSize = true;
            monday.Location = new Point(0, 3);
            tuesday.Location = new Point(80, 3);
            wednesday.Location = new Point(160, 3);
            thursday.Location = new Point(250, 3);
            friday.Location = new Point(0, 33);
            saturday.Location = new Point(80, 33);
            sunday.Location = new Point(160, 33);
            #endregion
            #region 初始化时间
            Panel panel_Cycle_Time_Text = new Panel();
            panel_Cycle_Time_Text.Location = new Point(0, 3);
            panel_Cycle_Time_Text.Size = new System.Drawing.Size(100, 24);
            Label label_Time = new Label();
            panel_Cycle_Time_Text.Controls.Add(label_Time);
            label_Time.Location = new Point(0, 3);
            label_Time.AutoSize = false;
            label_Time.Size = new System.Drawing.Size(100, 21);
            label_Time.TextAlign = ContentAlignment.MiddleRight;
            label_Time.Text = CommonUI.GetCustomMessage(_langTable, "text_time", "时间");

            Panel panel_Time = new Panel();
            panel_Time.Location = new Point(0, 3);
            panel_Time.Size = new System.Drawing.Size(298, 24);
            DateTimePicker dateTimePicker_DT = new DateTimePicker();
            dateTimePicker_DT.Value = _brightnessCfg.Time;
            dateTimePicker_DT.Location = new Point(0, 3);
            dateTimePicker_DT.Size = new System.Drawing.Size(150, 22);
            dateTimePicker_DT.Format = DateTimePickerFormat.Custom;
            dateTimePicker_DT.CustomFormat = "HH:mm:ss";
            dateTimePicker_DT.ValueChanged += DateTimePicker_DT_ValueChanged;
            dateTimePicker_DT.ShowUpDown = true;
            panel_Time.Controls.Add(dateTimePicker_DT);
            #endregion
            #region 初始化亮度
            Panel panel_Brightness_text = new Panel();
            panel_Brightness_text.Location = new Point(0, 3);
            panel_Brightness_text.Size = new System.Drawing.Size(100, 24);
            Label label_Brightness_test = new Label();
            label_Brightness_test.Location = new Point(0, 3);
            label_Brightness_test.Size = new Size(100, 22);
            label_Brightness_test.TextAlign = ContentAlignment.MiddleRight;
            label_Brightness_test.Text = CommonUI.GetCustomMessage(_langTable, "text_brightness", "亮度");
            panel_Brightness_text.Controls.Add(label_Brightness_test);

            Panel panel_Brightness = new Panel();
            panel_Brightness.Location = new Point(0, 3);
            panel_Brightness.Size = new System.Drawing.Size(298, 24);
            NumericUpDown numDown = new NumericUpDown();
            numDown.Location = new Point(0, 3);
            if (_brightnessCfg.Type == SmartBrightAdjustType.FixBright)
                numDown.Value = (decimal)_brightnessCfg.Brightness;
            numDown.Size = new Size(150, 22);
            numDown.ValueChanged += numDown_ValueChanged;
            panel_Brightness.Controls.Add(numDown);
            #endregion
            tabPanel.Controls.Add(panel_Cycle_Text, 0, 0);
            tabPanel.Controls.Add(panel_Cycle, 1, 0);
            if (cType == CycleType.userDefined)
            {
                tabPanel.Controls.Add(panel_Week, 1, 1);
                tabPanel.Controls.Add(panel_Cycle_Time_Text, 0, 2);
                tabPanel.Controls.Add(panel_Time, 1, 2);
            }
            else
            {
                tabPanel.Controls.Add(panel_Cycle_Time_Text, 0, 1);
                tabPanel.Controls.Add(panel_Time, 1, 1);
            }
            if (cType == CycleType.userDefined)
            {
                if (_brightnessCfg.Type == SmartBrightAdjustType.FixBright)
                {
                    tabPanel.Controls.Add(panel_Brightness_text, 0, 3);
                    tabPanel.Controls.Add(panel_Brightness, 1, 3);
                }
            }
            else
            {
                if (_brightnessCfg.Type == SmartBrightAdjustType.FixBright)
                {
                    tabPanel.Controls.Add(panel_Brightness_text, 0, 2);
                    tabPanel.Controls.Add(panel_Brightness, 1, 2);
                }
            }
        }

        void numDown_ValueChanged(object sender, EventArgs e)
        {
            _brightnessCfg.Brightness = (float)((NumericUpDown)sender).Value;
        }

        void DateTimePicker_DT_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker picker = (DateTimePicker)sender;
            if (picker == null) return;
            _brightnessCfg.Time = picker.Value;
        }

        void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cBox = (CheckBox)sender;
            DayOfWeek weekDay;
            int index = WeekDayList.FindIndex(a => a.ToString().ToLower() == cBox.Name.ToLower());
            if (index < 0) return;
            weekDay = WeekDayList[index];
            if (cBox.Checked)
            {
                if (!_brightnessCfg.DayList.Contains(weekDay))
                    _brightnessCfg.DayList.Add(weekDay);
            }
            else
            {
                if (_brightnessCfg.DayList.Contains(weekDay))
                    _brightnessCfg.DayList.Remove(weekDay);
            }
        }
        #endregion

        private void comboBox_Cycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comBox = (ComboBox)sender;
            if (comBox == null) return;
            if (comBox.SelectedIndex < 0) return;
            _brightnessCfg.ExecutionCycle = ComboBoxItemList[comBox.SelectedIndex].Value;
            if (_brightnessCfg.ExecutionCycle == CycleType.everyday || _brightnessCfg.ExecutionCycle == CycleType.userDefined)
                _brightnessCfg.DayList = new List<DayOfWeek>(WeekDayList);
            else if (_brightnessCfg.ExecutionCycle == CycleType.workday)
                _brightnessCfg.DayList = new List<DayOfWeek>(WeekDayList.FindAll(a => a != DayOfWeek.Saturday && a != DayOfWeek.Sunday));
            InitialTableLayoutPanel(ComboBoxItemList[comBox.SelectedIndex].Value,
tabControl_Brightness.SelectedTab);
        }

        private void frm_BrightnessConfig_Load(object sender, EventArgs e)
        {
            switch (_brightnessCfg.Type)
            {
                case SmartBrightAdjustType.FixBright:
                    tabControl_Brightness.SelectedTab = tabPage_Fasten;
                    InitialTableLayoutPanel(_brightnessCfg.ExecutionCycle, tabPage_Fasten);
                    break;
                case SmartBrightAdjustType.AutoBright:
                    tabControl_Brightness.SelectedTab = tabPage_Auto;
                    InitialTableLayoutPanel(_brightnessCfg.ExecutionCycle, tabPage_Auto);
                    break;
                default:
                    break;
            }
        }

        private void tabControl_Brightness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_Brightness.SelectedIndex == 0) _brightnessCfg.Type = SmartBrightAdjustType.FixBright;
            else _brightnessCfg.Type = SmartBrightAdjustType.AutoBright;
            InitialTableLayoutPanel(_brightnessCfg.ExecutionCycle, tabControl_Brightness.SelectedTab);
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (_brightnessCfg.DayList.Count == 0)
            {
                CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_timesempty", "请选择调节时段"));
                return;
            }
            if (_brightnessConfigList != null)
            {
                int index = _brightnessConfigList.FindIndex(a => a.Time.Equals(_brightnessCfg.Time));
                if (index >= 0 && index != _currentIndex)
                {
                    CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_brightnessconfig_repeat", "亮度配置列表时间重复，请重新编辑！"));
                    return;
                }
            }

            List<DayOfWeek> weekDayTmpList = new List<DayOfWeek>() 
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };
            List<DayOfWeek> weekDayList = new List<DayOfWeek>()
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };
            foreach (var item in weekDayList)
            {
                if (weekDayList.Contains(item) && !_brightnessCfg.DayList.Contains(item)) weekDayTmpList.Remove(item);
            }
            _brightnessCfg.DayList = weekDayTmpList;
            if (_brightnessCfg.DayList.Count == 7) _brightnessCfg.ExecutionCycle = CycleType.everyday;
            else if (_brightnessCfg.DayList.FindAll(a => (a == DayOfWeek.Saturday) || a == DayOfWeek.Sunday).Count > 0) _brightnessCfg.ExecutionCycle = CycleType.userDefined;
            else
            {
                if (_brightnessCfg.DayList.FindAll(a => (a == DayOfWeek.Monday) ||
                                                 (a == DayOfWeek.Tuesday) ||
                                                 (a == DayOfWeek.Wednesday) ||
                                                 (a == DayOfWeek.Thursday) ||
                                                 (a == DayOfWeek.Friday)
                                                 ).Count == 5)
                    _brightnessCfg.ExecutionCycle = CycleType.workday;
                else _brightnessCfg.ExecutionCycle = CycleType.userDefined;
            }

            _brightnessCfg.IsConfigEnable = true;
            _brightnessCfg.Time = new DateTime(2014, 1, 1, _brightnessCfg.Time.Hour, _brightnessCfg.Time.Minute, _brightnessCfg.Time.Second);
            //_brightnessCfg.DayList = weekDayList;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private class ComboBoxItem
        {
            private string _name;
            public string Name
            {
                get { return _name; }
            }
            private CycleType _value;
            public CycleType Value
            {
                get { return _value; }
            }
            public ComboBoxItem(string name, CycleType value)
            {
                _name = name;
                _value = value;
            }
        }
    }
}