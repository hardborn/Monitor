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

namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    public partial class Frm_BrightnessAdjustmentSetting : Frm_CommonBase
    {
        public Frm_BrightnessAdjustmentSetting()
        {
            InitializeComponent();
        }

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
        public Frm_BrightnessAdjustmentSetting(BrightnessConfigInfo brightnessCfg, AutoBrightExtendData autoBrightData, List<BrightnessConfigInfo> brightnessConfigList)
        {
            InitializeComponent();

            UpdateLang(CommonUI.BrightnessConfigLangPath);

            _brightnessConfigList = brightnessConfigList;
            _brightnessCfg = brightnessCfg;
            if (_brightnessCfg != null)
            {
                _currentIndex = brightnessConfigList.FindIndex(a => a.Time.Equals(brightnessCfg.Time));               
            }
            else
            {
                _brightnessCfg = new BrightnessConfigInfo();
            }

            InitializeViewData();

            //if (autoBrightData == null || autoBrightData.UseLightSensorList == null || autoBrightData.UseLightSensorList.Count == 0)
            //{
            //    HideBrightnessValueOptions();
            //    _brightnessCfg.Type = SmartBrightAdjustType.FixBright;
            //    autoBrightnessRadioButton.Enabled = false;
            //}

            LoadBrightnessAdjustmentSetting(_brightnessCfg);

        }

        private void HideBrightnessValueOptions()
        {
            this.brightnessNumericUpDown.Visible = false;
            this.label_brightnessValue.Visible = false;
        }

        private void InitializeViewData()
        {
            this.brightnessNumericUpDown.ValueChanged += numDown_ValueChanged;
            this.brightnessDateTimePicker.ValueChanged += DateTimePicker_DT_ValueChanged;
            this.mondayCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            this.tuesdayCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            this.wednesdayCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            this.thursdayCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            this.fridayCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            this.saturdayCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            this.sundayCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            this.brightnessComboBox.SelectedIndexChanged += comboBox_Cycle_SelectedIndexChanged;
            this.fixedBrightnessRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            this.autoBrightnessRadioButton.CheckedChanged += RadioButton_CheckedChanged;


            ComboBoxItemList = new List<ComboBoxItem>() 
            {  
                new ComboBoxItem(BrightnessLangTable.CycleTypeTable[CycleType.everyday], CycleType.everyday), 
                new ComboBoxItem(BrightnessLangTable.CycleTypeTable[CycleType.workday], CycleType.workday),
                new ComboBoxItem(BrightnessLangTable.CycleTypeTable[CycleType.userDefined], CycleType.userDefined) 
            };

            this.brightnessComboBox.DisplayMember = "Name";
            this.brightnessComboBox.ValueMember = "Value";
            this.brightnessComboBox.Items.AddRange(ComboBoxItemList.ToArray());                       
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton == null)
            {
                return;
            }
            if (radioButton.Name == "fixedBrightnessRadioButton" && radioButton.Checked)
            {

                this.label_brightnessValue.Visible = true;
                this.brightnessNumericUpDown.Visible = true;
                _brightnessCfg.Type = SmartBrightAdjustType.FixBright;
                LoadBrightnessAdjustmentSetting(_brightnessCfg);                
            }
            if (radioButton.Name == "fixedBrightnessRadioButton" && !radioButton.Checked)
            {
                this.label_brightnessValue.Visible = false;
                this.brightnessNumericUpDown.Visible = false;
                _brightnessCfg.Type = SmartBrightAdjustType.AutoBright;
                LoadBrightnessAdjustmentSetting(_brightnessCfg);               
            }
        }


        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, this);
            MultiLanguageUtils.ReadLanguageResource(langResFile, "frm_BrightnessConfig_String", out _langTable);
        }

        private void LoadBrightnessAdjustmentSetting(BrightnessConfigInfo brightnessInfo)
        {
            
            this.brightnessComboBox.SelectedIndex = ComboBoxItemList.FindIndex(a => a.Value == _brightnessCfg.ExecutionCycle);
            LoadBrightnessAdjustmentTypeData(brightnessInfo);
            LoadExecutionCycleData(brightnessInfo);
            LoadExecutionTimeData(brightnessInfo);
            LoadBrightnessValueData(brightnessInfo);
        }

        private void LoadBrightnessAdjustmentTypeData(BrightnessConfigInfo brightnessInfo)
        {
            if (brightnessInfo.Type == SmartBrightAdjustType.AutoBright)
            {
                this.autoBrightnessRadioButton.Checked = true;
                this.fixedBrightnessRadioButton.Checked = false;
                this.label_brightnessValue.Visible = false;
                this.brightnessNumericUpDown.Visible = false;
            }
            else if (brightnessInfo.Type == SmartBrightAdjustType.FixBright)
            {
                this.fixedBrightnessRadioButton.Checked = true;
                this.autoBrightnessRadioButton.Checked = false;
                this.label_brightnessValue.Visible = true;
                this.brightnessNumericUpDown.Visible = true;
            }
        }

        private void LoadBrightnessValueData(BrightnessConfigInfo brightnessInfo)
        {
            this.brightnessNumericUpDown.Value = Convert.ToDecimal(brightnessInfo.Brightness);
        }

        private void LoadExecutionTimeData(BrightnessConfigInfo brightnessInfo)
        {
            this.brightnessDateTimePicker.Value = brightnessInfo.Time;
        }

        private void LoadExecutionCycleData(BrightnessConfigInfo brightnessInfo)
        {
            if (brightnessInfo.ExecutionCycle == CycleType.userDefined)
            {
                SetExecutionCycleEnable(true);
                LoadExecutionCycleDataToView();
            }
            else if (brightnessInfo.ExecutionCycle == CycleType.everyday)
            {
                SetExecutionCycleEnable(false);
                LoadExecutionCycleDataToView();
            }
            else if (brightnessInfo.ExecutionCycle == CycleType.workday)
            {
                SetExecutionCycleEnable(false);
                LoadExecutionCycleDataToView();
            }
        }

        private void LoadExecutionCycleDataToView()
        {
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Monday))
            {
                this.mondayCheckBox.Checked = true;
            }
            else
            {
                this.mondayCheckBox.Checked = false;
            }

            if (_brightnessCfg.DayList.Contains(DayOfWeek.Tuesday))
            {
                this.tuesdayCheckBox.Checked = true;
            }
            else
            {
                this.tuesdayCheckBox.Checked = false;
            }

            if (_brightnessCfg.DayList.Contains(DayOfWeek.Wednesday))
            {
                this.wednesdayCheckBox.Checked = true;
            }
            else
            {
                this.wednesdayCheckBox.Checked = false;
            }

            if (_brightnessCfg.DayList.Contains(DayOfWeek.Thursday))
            {
                this.thursdayCheckBox.Checked = true;
            }
            else
            {
                this.thursdayCheckBox.Checked = false;
            }
            if (_brightnessCfg.DayList.Contains(DayOfWeek.Friday))
            {
                this.fridayCheckBox.Checked = true;
            }
            else
            {
                this.fridayCheckBox.Checked = false;
            }

            if (_brightnessCfg.DayList.Contains(DayOfWeek.Saturday))
            {
                this.saturdayCheckBox.Checked = true;
            }
            else
            {
                this.saturdayCheckBox.Checked = false;
            }

            if (_brightnessCfg.DayList.Contains(DayOfWeek.Sunday))
            {
                this.sundayCheckBox.Checked = true;
            }
            else
            {
                this.sundayCheckBox.Checked = false;
            }
        }

        private void SetExecutionCycleEnable(bool enable)
        {
            this.mondayCheckBox.Enabled = enable;
            this.tuesdayCheckBox.Enabled = enable;
            this.wednesdayCheckBox.Enabled = enable;
            this.thursdayCheckBox.Enabled = enable;
            this.fridayCheckBox.Enabled = enable;
            this.saturdayCheckBox.Enabled = enable;
            this.sundayCheckBox.Enabled = enable;
        }


        private void numDown_ValueChanged(object sender, EventArgs e)
        {
            _brightnessCfg.Brightness = (float)Math.Round(((NumericUpDown)sender).Value,0);
        }

        private void DateTimePicker_DT_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker picker = (DateTimePicker)sender;
            if (picker == null)
                return;
            _brightnessCfg.Time = picker.Value;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cBox = (CheckBox)sender;
            DayOfWeek weekDay;
            int index = WeekDayList.FindIndex(a => a.ToString().ToLower() == cBox.Tag.ToString().ToLower());
            if (index < 0)
                return;
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


        private void comboBox_Cycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comBox = (ComboBox)sender;
            if (comBox == null)
                return;
            if (comBox.SelectedIndex < 0)
                return;
            _brightnessCfg.ExecutionCycle = ComboBoxItemList[comBox.SelectedIndex].Value;

            if (_brightnessCfg.ExecutionCycle == CycleType.everyday)
            {
                _brightnessCfg.DayList = new List<DayOfWeek>(WeekDayList);
                LoadExecutionCycleData(_brightnessCfg);
            }
            else if (_brightnessCfg.ExecutionCycle == CycleType.userDefined)
            {
                LoadExecutionCycleData(_brightnessCfg);
            }
            else if (_brightnessCfg.ExecutionCycle == CycleType.workday)
            {
                _brightnessCfg.DayList = new List<DayOfWeek>(WeekDayList.FindAll(a => a != DayOfWeek.Saturday && a != DayOfWeek.Sunday));
                LoadExecutionCycleData(_brightnessCfg);
            }
        }
          

        private void confirmButton_Click(object sender, EventArgs e)
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
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
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
