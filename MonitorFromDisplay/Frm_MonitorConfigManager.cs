using Nova.Monitoring.Common;
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig;
using Nova.Resource.Language;
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
    public partial class Frm_MonitorConfigManager : Frm_CommonBase
    {
        private Frm_MonitorConfigManager_VM _vm = null;
        private Dictionary<string, UC_ConfigBase> _dicConfigControls;
        private string _currentConfig = string.Empty;
        private Hashtable _hashTable = null;
        public Frm_MonitorConfigManager()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            Initialize();
        }

        internal Frm_MonitorConfigManager(string strFuncName, string strSelectSn)
        {
            InitializeComponent();
            Initialize();
            System.Windows.Forms.Control[] controls = panel_FuncList.Controls.Find(strFuncName, false);
            if (controls != null || controls.Length > 0)
            {
                buttonConfig_Click(controls[0], null);
            }
            else
            {

            }
        }

        public void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "Frm_MonitorConfigManager_String", out _hashTable);
        }

        private void Initialize()
        {
            _dicConfigControls = new Dictionary<string, UC_ConfigBase>();
            _vm = new Frm_MonitorConfigManager_VM();
            UpdateLanguage();
            tableLayoutPanel_Config.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(tableLayoutPanel_Config, true, null);
            Frm_MonitorConfigManager_LedScreenChangedEvent(null,null);
            buttonConfig_Click(crystalButton_RefreshConfig, null);
            MonitorAllConfig.Instance().LedScreenChangedEvent += Frm_MonitorConfigManager_LedScreenChangedEvent;
            MonitorAllConfig.Instance().LedRegistationInfoEvent += Frm_MonitorConfigManager_LedRegistationInfoEvent;
            comboBox_Screen_DataSourceChanged(null, null);
        }

        void Frm_MonitorConfigManager_LedRegistationInfoEvent(bool e)
        {
            Frm_MonitorConfigManager_LedScreenChangedEvent(null, null);
        }

        private delegate void Frm_LedScreenChangedEvent(object sender, EventArgs e);
        void Frm_MonitorConfigManager_LedScreenChangedEvent(object sender, EventArgs e)
        {
            if (!this.InvokeRequired)
            {
                comboBox_Screen.DataSource = null;
                _vm.CmdInitialize.Execute(null);
                comboBox_Screen.DataSource = _vm.LedInfos;
                comboBox_Screen.DisplayMember = "Name";
                comboBox_Screen.ValueMember = "Data";

                if (_vm.LedInfos == null || _vm.LedInfos.Count == 0)
                {
                    ControlEnabled(false);
                }
                else
                {
                    ControlEnabled(true);
                }
            }
            else
            {
                while (!this.IsHandleCreated)
                {
                    if (this == null || this.IsDisposed)
                    {
                        return;
                    }
                }
                Frm_LedScreenChangedEvent frm_ledChangedEvent = new Frm_LedScreenChangedEvent(Frm_MonitorConfigManager_LedScreenChangedEvent);
                this.Invoke(frm_ledChangedEvent, new object[] { sender, e });
            }
        }

        #region 事件
        private void buttonConfig_Click(object sender, EventArgs e)
        {
            _currentConfig = (sender as Nova.Control.CrystalButton).Name;
            SwitchFunctionView(_currentConfig);
        }

        private void SwitchFunctionView(string currentConfig)
        {
            if (string.IsNullOrEmpty(currentConfig))
            {
                return;
            }
            if (!_dicConfigControls.ContainsKey(currentConfig))
            {
                CreateUC_Config(currentConfig);
                panel_Config.Controls.Add(_dicConfigControls[currentConfig]);
            }
            FunctionChanged(currentConfig);
            panel_Config_Clear();
            ButtonSelected(currentConfig);
            _dicConfigControls[currentConfig].Visible = true;
            _vm.CmdOpenFunc.Execute(currentConfig);
        }

        private void ButtonSelected(string cbuttonName)
        {
            foreach (Nova.Control.CrystalButton control in panel_FuncList.Controls)
            {
                if (control.Name == cbuttonName)
                {
                    control.BackColor = Color.LimeGreen;
                    control.ButtonSelectedColor = Color.LimeGreen;
                }
                else
                {
                    control.BackColor = Color.DodgerBlue;
                    control.ButtonSelectedColor = Color.DodgerBlue;
                }
            }
        }

        private void comboBox_Screen_SelectedIndexChanged(object sender, EventArgs e)
        {
            _vm.CurSelectedSN = comboBox_Screen.SelectedValue as LedBasicInfo;
            if (comboBox_Screen.SelectedItem == null)
            {
                _vm.CurrentName = string.Empty;
            }
            else
            {
                _vm.CurrentName = (comboBox_Screen.SelectedItem as ComboBoxData_VM).Name;
            }
            SwitchFunctionView(_currentConfig);
            _vm.CmdOpenFunc.Execute(_currentConfig);
        }

        private void panel_Config_Clear()
        {
            foreach (System.Windows.Forms.Control con in panel_Config.Controls)
            {
                con.Visible = false;
            }
        }

        void Frm_MonitorConfigManager_NoSupportOneConfigEventHandler(object sender, EventArgs e)
        {
            comboBox_Screen.Enabled = false;
        }

        void Frm_MonitorConfigManager_SupportOneConfigEventHandler(object sender, EventArgs e)
        {
            comboBox_Screen.Enabled = true;
        }
        #endregion

        #region 内部方法
        private void CreateUC_Config(string btnName)
        {
            UC_ConfigBase uc_config = null;
            switch (btnName)
            {
                case "crystalButton_RefreshConfig":
                    uc_config = new UC_CycleConfig();
                    break;
                case "crystalButton_HWConfig":
                    uc_config = new UC_HWConfig();
                    break;
                //case "crystalButton_CareRegisterConfig":
                //    uc_config = new UC_CareServerConfig();
                //    break;
                case "crystalButton_MonitorCardPowerConfig":
                    uc_config = new UC_PowerControlConfig();
                    break;
                case "crystalButton_DataAltarmConfig":
                    uc_config = new UC_DataAlarmConfig();
                    break;
                case "crystalButton_ControlConfig":
                    uc_config = new UC_WHControlConfig();
                    break;
                case "crystalButton_ControlLog":
                    break;
                case "crystalButton_BrightnessConfig":
                    uc_config = new UC_BrightnessAllConfig();
                    break;
                case "crystalButton_NotifySetting":
                    uc_config = new UC_EMailNotify();
                    break;
                case "crystalButton_EMailLog":
                    uc_config = new UC_EMailNotifyLog();
                    break;
                default:
                    break;
            }
            uc_config.Dock = DockStyle.Fill;
            uc_config.ScreenSN = comboBox_Screen.SelectedText;
            if (!_dicConfigControls.ContainsKey(btnName))
            {
                _dicConfigControls.Add(btnName, uc_config);
            }
        }
        private void FunctionChanged(string currentConfig)
        {
            switch (currentConfig)
            {
                case "crystalButton_RefreshConfig":
                    ScreenDisplay(false);
                    break;
                case "crystalButton_HWConfig":
                    ScreenDisplay(true);
                    break;
                //case "crystalButton_CareRegisterConfig":
                //    ScreenDisplay(false);
                //    break;
                case "crystalButton_MonitorCardPowerConfig":
                    ScreenDisplay(false);
                    break;
                case "crystalButton_DataAltarmConfig":
                    ScreenDisplay(true);
                    break;
                case "crystalButton_ControlConfig":
                    ScreenDisplay(true);
                    break;
                case "crystalButton_ControlLog":
                    ScreenDisplay(true);
                    break;
                case "crystalButton_BrightnessConfig":
                    ScreenDisplay(true);
                    break;
                case "crystalButton_NotifySetting":
                    ScreenDisplay(false);
                    break;
                case "crystalButton_EMailLog":
                    ScreenDisplay(false);
                    break;
                default:
                    break;
            }
        }

        private void ScreenDisplay(bool isDisplay)
        {
            panel_ScreenMessage.Visible = isDisplay;
            if (isDisplay)
            {
                tableLayoutPanel_Config.SetRow(panel_Config, 1);
                tableLayoutPanel_Config.SetRowSpan(panel_Config, 1);
            }
            else
            {
                tableLayoutPanel_Config.SetRow(panel_Config, 0);
                tableLayoutPanel_Config.SetRowSpan(panel_Config, 2);
            }
        }

        private void ControlEnabled(bool isTrue)
        {
            comboBox_Screen.Enabled = isTrue;
            crystalButton_RefreshConfig.Enabled = isTrue;
            crystalButton_HWConfig.Enabled = isTrue;
            crystalButton_DataAltarmConfig.Enabled = isTrue;
            crystalButton_ControlConfig.Enabled = isTrue;
            panel_Config.Enabled = isTrue;
            crystalButton_MonitorCardPowerConfig.Enabled = isTrue;
            //crystalButton_BrightnessConfig.Enabled = isTrue;
            crystalButton_NotifySetting.Enabled = isTrue;
            crystalButton_EMailLog.Enabled = isTrue;
        }

        #endregion

        private void comboBox_Screen_DataSourceChanged(object sender, EventArgs e)
        {
            Frm_MonitorConfigManager_LedScreenChangedEvent(null, null);
        }

        private void Frm_MonitorConfigManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            MonitorAllConfig.Instance().LedScreenChangedEvent -= Frm_MonitorConfigManager_LedScreenChangedEvent;
            MonitorAllConfig.Instance().LedRegistationInfoEvent -= Frm_MonitorConfigManager_LedRegistationInfoEvent;
            foreach (UC_ConfigBase uc in _dicConfigControls.Values)
            {
                uc.Dispose();
            }
        }
    }
}