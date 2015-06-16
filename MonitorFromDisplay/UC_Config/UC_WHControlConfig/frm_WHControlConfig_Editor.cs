using Nova.Monitoring.Common;
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
    public partial class frm_WHControlConfig_Editor : Frm_CommonBase
    {
        private Hashtable _langTable;
        private WHControlConfig_Editor_VM _vm;
        public WHControlConfig_Editor_VM VM
        {
            get { return _vm; }
        }
        private UC_WHControlConfig_Smoke _UC_SmokeCfg;
        private UC_WHControlConfig_Tem _UC_TemCfg;
        public UC_WHControlConfig_VM_Base Strategy { get { return _vm.Strategy; } }
        public frm_WHControlConfig_Editor(string sn, UC_WHControlConfig_VM_Base strategy, List<UC_WHControlConfig_Tem_VM> temStrategyList, List<UC_WHControlConfig_Smoke_VM> smokeStrategyList)
        {
            InitializeComponent();
            UpdateLang(CommonUI.ControlConfigLangPath);
            _vm = new WHControlConfig_Editor_VM();
            _vm.SelectedScreenSN = sn;
            if (strategy == null)
            {
                comboBox_ControlType.Enabled = true;
                _vm.Strategy = new UC_WHControlConfig_Tem_VM(sn);
            }
            else
            {
                _vm.Strategy = strategy;
                comboBox_ControlType.Enabled = false;
            }
            _vm.TemStrategyList = temStrategyList;
            _vm.SmokeStrategyList = smokeStrategyList;
        }
        #region 私有函数
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, this);
            MultiLanguageUtils.ReadLanguageResource(langResFile, "frm_WHControlConfig_Editor_String", out _langTable);
        }
        private void UpdateUI(UC_WHControlConfig_VM_Base strategy)
        {
            if (strategy == null) return;
            UC_WHControlConfig_Smoke_VM smoke_VM = null;
            UC_WHControlConfig_Tem_VM tem_VM = null;
            if (strategy.StratlType == StrategyType.SmokeStrategy)
            {
                smoke_VM = (UC_WHControlConfig_Smoke_VM)strategy;
                if (smoke_VM == null) return;
                if (_UC_SmokeCfg != null) panel_ControlContent.Controls.Remove(_UC_SmokeCfg);
                _UC_SmokeCfg = new UC_WHControlConfig_Smoke(smoke_VM);
                panel_ControlContent.Controls.Add(_UC_SmokeCfg);
                _UC_SmokeCfg.Dock = DockStyle.Fill;
                _UC_SmokeCfg.BringToFront();
            }
            else if (strategy.StratlType == StrategyType.TemperatureStrategy)
            {
                tem_VM = (UC_WHControlConfig_Tem_VM)strategy;
                if (tem_VM == null) return;
                if (_UC_TemCfg != null) panel_ControlContent.Controls.Remove(_UC_TemCfg);
                _UC_TemCfg = new UC_WHControlConfig_Tem(tem_VM);
                panel_ControlContent.Controls.Add(_UC_TemCfg);
                _UC_TemCfg.Dock = DockStyle.Fill;
                _UC_TemCfg.BringToFront();
            }
        }
        #endregion
        private void comboBox_ControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_vm.CtrlTypeList[comboBox_ControlType.SelectedIndex].Value == StrategyType.TemperatureStrategy) label_Tip.Visible = true;
            else label_Tip.Visible = false;
            ControlType type = _vm.CtrlTypeList[comboBox_ControlType.SelectedIndex];
            if (_vm.SelectedCtrlType.Value == type.Value) return;
            _vm.SelectedCtrlType = type;
            if (!comboBox_ControlType.Enabled || comboBox_ControlType.Items.Count == 0) return;
            if (type.Value == StrategyType.SmokeStrategy)
            {
                UpdateUI(new UC_WHControlConfig_Smoke_VM(_vm.SelectedScreenSN));
            }
            else if (type.Value == StrategyType.TemperatureStrategy)
            {
                UpdateUI(new UC_WHControlConfig_Tem_VM(_vm.SelectedScreenSN));
            }
        }

        private void WHControlConfig_Editor_Load(object sender, EventArgs e)
        {
            comboBox_ControlType.DisplayMember = "Name";
            comboBox_ControlType.ValueMember = "Value";
            comboBox_ControlType.Items.AddRange(_vm.CtrlTypeList.ToArray());
            if (_vm.Strategy == null)
            {
                comboBox_ControlType.SelectedIndex = 0;
            }
            else
            {
                comboBox_ControlType.SelectedIndex = _vm.CtrlTypeList.FindIndex(a => a.Value == _vm.Strategy.StratlType);
            }
            this.comboBox_ControlType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ControlType_SelectedIndexChanged);
            UpdateUI(_vm.Strategy);
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (comboBox_ControlType.SelectedIndex < 0) return;
            StrategyType type = _vm.SelectedCtrlType.Value;
            UC_WHControlConfig_VM_Base baseVM = null;
            #region 页面输入合理性检测
            if (type == StrategyType.TemperatureStrategy)
            {
                ValueRes tempRes = _UC_TemCfg.IsOK();
                switch (tempRes)
                {
                    case ValueRes.ok:
                        break;
                    case ValueRes.MinBiggerThanMax:
                        CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_tem_minvaluebiggerthanmax", "最小温度不能大于最大温度"));
                        return;
                    case ValueRes.MinToMaxToSimilar:
                        CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_tem_valuetoosimilar", "最大温度与最小温度值相差不能小于5℃"));
                        return;
                    case ValueRes.invalidValue:
                        CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_action_illegal", "请设置有效的执行动作"));
                        return;
                }
            }
            else if (type == StrategyType.SmokeStrategy)
            {
                if (!_UC_SmokeCfg.IsOk())
                {
                    CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_power_notchoose", "请选择需要关闭的电源"));
                    return;
                }
                baseVM = _UC_SmokeCfg.SmokeVM;
            }
            #endregion
            if (comboBox_ControlType.Enabled)
            {
                ControlConfigSaveRes res;
                if (type == StrategyType.SmokeStrategy)
                {
                    res = _vm.IsSmokeOk(_UC_SmokeCfg.SmokeVM);
                    switch (res)
                    {
                        case ControlConfigSaveRes.ok:
                            _vm.Strategy = _UC_SmokeCfg.SmokeVM;
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            return;
                        case ControlConfigSaveRes.smoke_CtrlCfgIsExist:
                            CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_controlconfig_repeat", "策略重复，添加失败"));
                            return;
                        case ControlConfigSaveRes.smoke_CtrlCfgIsInvalid:
                            CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_action_illegal", "请设置有效的执行动作"));
                            return;
                        case ControlConfigSaveRes.smoke_objIsNull:
                            CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_controlconfig_null", "策略对象为空"));
                            return;
                        default:
                            break;
                    }
                }
                else if (type == StrategyType.TemperatureStrategy)
                {
                    res = _vm.IsTemOk(_UC_TemCfg.TemVM);
                    switch (res)
                    {
                        case ControlConfigSaveRes.ok:
                            _vm.Strategy = _UC_TemCfg.TemVM;
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            return;
                        case ControlConfigSaveRes.tem_CtrlCfgIsExist:
                            CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_controlconfig_repeat", "策略重复，添加失败"));
                            return;
                        case ControlConfigSaveRes.tem_CtrlCfgIsInvalid:
                            CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_action_illegal", "请设置有效的执行动作"));
                            return;
                        case ControlConfigSaveRes.tem_objIsNull:
                            CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_controlconfig_null", "策略对象为空"));
                            return;
                        case ControlConfigSaveRes.tem_ConditionError:
                            CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "msg_tem_valuetoosimilar", "最大温度与最小温度值相差不能小于5℃"));
                            return;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if (type == StrategyType.SmokeStrategy)
                {
                    _vm.Strategy = _UC_SmokeCfg.SmokeVM;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else if (type == StrategyType.TemperatureStrategy)
                {
                    _vm.Strategy = _UC_TemCfg.TemVM;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }

            }
        }
    }
}
