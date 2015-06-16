using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.Common;
using PresentationControls;
using System.Collections;
using Nova.Resource.Language;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_WHControlConfig_Tem : UC_ConfigBase
    {
        private Hashtable _langTable;
        private UC_WHControlConfig_Tem_VM _temVM;

        public UC_WHControlConfig_Tem_VM TemVM
        {
            get { return _temVM; }
            set { _temVM = value; }
        }
        private UC_PowerManager _uc_powerCtrlCfg;
        #region 公共方法
        public UC_WHControlConfig_Tem(UC_WHControlConfig_Tem_VM stratInfo)
        {
            InitializeComponent();
            UpdateLang(CommonUI.ControlConfigLangPath);
            _temVM = new UC_WHControlConfig_Tem_VM(stratInfo.SN, stratInfo.ID, stratInfo.ConditionAlgor, stratInfo.LessThan, stratInfo.GreaterThan, stratInfo.IsControlBrightness, stratInfo.Brightness, stratInfo.PowerCtrlDic);
            InitialTemMV();
            if (_temVM.PowerCtrlDic.Count != 0)
            {
                groupBox_PwoerManager.Controls.Add(_uc_powerCtrlCfg);
                _uc_powerCtrlCfg.Dock = DockStyle.Fill;
                _uc_powerCtrlCfg.Visible = true;
            }
            else _uc_powerCtrlCfg.Visible = false;
        }
        public ValueRes IsOK()
        {
            if ((int)numericUpDown_minTem.Value > (int)numericUpDown_maxTem.Value) return ValueRes.MinBiggerThanMax;
            if ((int)numericUpDown_minTem.Value + 5 > (int)numericUpDown_maxTem.Value)
                return ValueRes.MinToMaxToSimilar;
            bool isValid = false;
            foreach (var item in _temVM.PowerCtrlDic.Values)
            {
                if (item != PowerCtrl_Type.still)
                {
                    isValid = true;
                    break;
                }
            }
            if (!checkBox_Brightness.Checked && !isValid) return ValueRes.invalidValue;
            return ValueRes.ok;
        }
        #endregion
        #region 私有函数
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_WHControlConfig_Tem", this);
        }
        private void InitialTemMV()
        {
            _uc_powerCtrlCfg = new UC_PowerManager();
            _uc_powerCtrlCfg.DataSaveEvent += UC_powerCtrlCfg_DataSaveEvent;
            _uc_powerCtrlCfg.SetPowerList(_temVM.PowerCtrlDic);
            comboBox_TemType.DisplayMember = "Name";
            comboBox_TemType.ValueMember = "Value";
            comboBox_TemType.Items.AddRange(_temVM.ConditionTypeList.ToArray());
            if (comboBox_TemType.Items.Count != 0)
            {
                comboBox_TemType.SelectedIndex = _temVM.ConditionTypeList.FindIndex(a => a.Value == _temVM.ConditionAlgor);
            }
            numericUpDown_minTem.Value = _temVM.GreaterThan;
            numericUpDown_maxTem.Value = _temVM.LessThan;
            checkBox_Brightness.Enabled = _temVM.IsEnableBrightCtrl;
            checkBox_Brightness.Checked = _temVM.IsControlBrightness;
            numericUpDown_brightness.Value = _temVM.Brightness;
            radioButton_Brightness.Enabled = _temVM.IsControlBrightness;
        }
        void UC_powerCtrlCfg_DataSaveEvent(Dictionary<string, PowerCtrl_Type> dicPowerStatusList)
        {
            _temVM.PowerCtrlDic = dicPowerStatusList;
        }
        #endregion
        private void comboBox_TemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_TemType.SelectedIndex < 0) return;
            _temVM.ConditionAlgor = _temVM.ConditionTypeList[comboBox_TemType.SelectedIndex].Value;
        }

        private void numericUpDown_minTem_ValueChanged(object sender, EventArgs e)
        {
            _temVM.GreaterThan = (int)numericUpDown_minTem.Value;
        }

        private void radioButton_Brightness_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_brightness.Enabled = radioButton_Brightness.Checked;
            _temVM.IsControlBrightness = radioButton_Brightness.Enabled;
        }

        private void numericUpDown_maxTem_ValueChanged(object sender, EventArgs e)
        {
            _temVM.LessThan = (int)numericUpDown_maxTem.Value;
        }

        private void numericUpDown_brightness_ValueChanged(object sender, EventArgs e)
        {
            _temVM.Brightness = (int)numericUpDown_brightness.Value;
        }
        private void checkBox_Brightness_CheckedChanged(object sender, EventArgs e)
        {
            panel_Brightness.Enabled = checkBox_Brightness.Checked;
            _temVM.IsControlBrightness = checkBox_Brightness.Checked;
        }

    }
    public enum ValueRes
    {
        ok,
        MinBiggerThanMax,
        MinToMaxToSimilar,
        invalidValue,
    }
}
