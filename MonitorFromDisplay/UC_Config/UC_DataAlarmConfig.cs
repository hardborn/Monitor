using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.MonitorDataManager;
using GalaSoft.MvvmLight.Messaging;
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_DataAlarmConfig : UC_ConfigBase
    {
        UC_DataAlarmConfig_VM _vm = null;
        public UC_DataAlarmConfig()
        {
            InitializeComponent();
            _vm = new UC_DataAlarmConfig_VM();
            uCDataAlarmConfigVMBindingSource.DataSource = _vm;
            _vm.TempType = MonitorAllConfig.Instance().UserConfigInfo.TemperatureUnit;
            UpdateLanguage();
            this.Invalidate();
        }

        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable=null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_DataAlarmConfig_String", out _hashTable);
            HsLangTable = _hashTable;
            label_TempUnit_TextChanged(null,null);
        }
        delegate void AlarmConfigSynchronizedHandler(object sender, EventArgs e);
        void UC_DataAlarmConfig_AlarmConfigSynchronizedEvent(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                AlarmConfigSynchronizedHandler cs = new AlarmConfigSynchronizedHandler(UC_DataAlarmConfig_AlarmConfigSynchronizedEvent);
                this.Invoke(cs, new object[] { sender, e });
            }
            else
            {
                if (sender.ToString() == _vm.SN)
                {
                    OnMSG_DataAltarmConfig(_vm.SN);
                    this.Invalidate();
                }
            }
        }

        protected override void Register()
        {
            MonitorAllConfig.Instance().AlarmConfigSynchronizedEvent += UC_DataAlarmConfig_AlarmConfigSynchronizedEvent;
            Messenger.Default.Register<string>(this, MsgToken.MSG_DataAltarmConfig, OnMSG_DataAltarmConfig);
        }

        protected override void UnRegister()
        {
            MonitorAllConfig.Instance().AlarmConfigSynchronizedEvent -= UC_DataAlarmConfig_AlarmConfigSynchronizedEvent;
            Messenger.Default.Unregister<string>(this, MsgToken.MSG_DataAltarmConfig, OnMSG_DataAltarmConfig);
        }

        private void OnMSG_DataAltarmConfig(string obj)
        {
            _vm = new UC_DataAlarmConfig_VM();
            uCDataAlarmConfigVMBindingSource.DataSource = _vm;
            _vm.TempType = MonitorAllConfig.Instance().UserConfigInfo.TemperatureUnit;
            _vm.CmdInitialize.Execute(obj);
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            SaveConfig((result) =>
            {
                if (result)
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "AlarmSaveSuccess", "告警配置保存成功!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                else
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "AlarmSaveFailed", "告警配置保存失败!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                CloseProcessForm();
            });
            ShowSending("SaveDataAlarmConfig", "正在保存告警的配置,请稍候...", true);
        }
        private void SaveConfig(Action<bool> callBackAction)
        {
            Func<bool> func = () =>
            {
                return _vm.OnCmdSaveAlarmConfig();
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                this.BeginInvoke(callBackAction, new object[] { result });
            }, null);
        }

        private void linkLabel_TempType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label_TempUnit.Text == "℃")
            {
                label_TempUnit.Text = "℉";
            }
            else
            {
                label_TempUnit.Text = "℃";
            }
        }

        private void numericUpDown_Power_ValueChanged(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Name == "numericUpDown_Power")
            {
                if (numericUpDown_Power.Value <= numericUpDown_PowerError.Value)
                {
                    numericUpDown_Power.Value = numericUpDown_PowerError.Value + (decimal)0.1;
                }
                else if(numericUpDown_Power.Value >= 12)
                {
                    numericUpDown_Power.Value = (decimal)11.9;
                }
            }
            else if ((sender as NumericUpDown).Name == "numericUpDown_PowerError" &&
                numericUpDown_Power.Value <= numericUpDown_PowerError.Value)
            {
                numericUpDown_PowerError.Value = numericUpDown_Power.Value - (decimal)0.1;
            }
        }

        private void label_TempUnit_TextChanged(object sender, EventArgs e)
        {
            if (label_TempUnit.Text == "℃")
            {
                linkLabel_TempType.Text = CommonUI.GetCustomMessage(HsLangTable, "FahrenheitTemp", "华氏温度");
            }
            else
            {
                linkLabel_TempType.Text = CommonUI.GetCustomMessage(HsLangTable, "CelsiusTemp", "摄氏温度");
            }
        }
    }
}