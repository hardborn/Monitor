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
using Nova.Windows.Forms;
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_CycleConfig:UC_ConfigBase
    {
        private UC_CycleConfig_VM _vm;
        private System.Diagnostics.Stopwatch _stopWatch = null;
        private bool _isRefresh = false;
        public UC_CycleConfig()
        {
            InitializeComponent();
            _vm = new UC_CycleConfig_VM();
            uCCycleConfigVMBindingSource.DataSource = _vm;
            UpdateLanguage();
            _stopWatch = new System.Diagnostics.Stopwatch(); 
            OnMSG_RefreshConfig();
        }

        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable = null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_CycleConfig_String", out _hashTable);
            HsLangTable = _hashTable;
        }

        void UC_CycleConfig_LedAcquisitionConfigEvent(object sender,EventArgs e)
        {
            OnMSG_RefreshConfig();
        }
        delegate void MonitorDataChangedEventDel(object sender, EventArgs e);
        private void UC_CycleConfig_MonitorDataChangedEvent(object sender, EventArgs e)
        {
            if(this.InvokeRequired)
            {
                MonitorDataChangedEventDel cs = new MonitorDataChangedEventDel(UC_CycleConfig_MonitorDataChangedEvent);
                this.Invoke(cs, new object[] { sender, e });
                return;
            }
            if (_isRefresh)
            {
                _stopWatch.Stop();
                int minute=(int)Math.Ceiling(_stopWatch.ElapsedMilliseconds/1000.0);
                if (minute < 20)
                {
                    minute = 30;
                }
                else if(minute<40)
                {
                    minute += 5;
                }
                else if (minute < 60)
                {
                    minute += 8;
                }
                else if (minute < 120)
                {
                    minute += 20;
                }
                else
                {
                    minute += 30;
                }
                numericUpDown_MonitorPeriodValue.Minimum = minute - 3;
                numericUpDown_MonitorPeriodValue.Value = minute;
                CloseProcessForm();
            }
        }

        protected override void Register()
        {
            MonitorAllConfig.Instance().LedAcquisitionConfigEvent += UC_CycleConfig_LedAcquisitionConfigEvent;
            MonitorAllConfig.Instance().MonitorDataChangedEvent += UC_CycleConfig_MonitorDataChangedEvent;
        }

        protected override void UnRegister()
        {
            MonitorAllConfig.Instance().LedAcquisitionConfigEvent -= UC_CycleConfig_LedAcquisitionConfigEvent;
            MonitorAllConfig.Instance().MonitorDataChangedEvent -= UC_CycleConfig_MonitorDataChangedEvent;
        }

        private void OnMSG_RefreshConfig()
        {
            _vm.CmdInitialize.Execute(null);
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            SaveConfig((result) =>
            {
                if (result)
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "CycleConfigSuccess", "周期保存成功!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                else
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "CycleConfigFailed", "周期保存失败!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                CloseProcessForm();
            });
            ShowSending("SaveCycleConfig", "正在保存周期配置,请稍候...", true);
        }
        private void SaveConfig(Action<bool> callBackAction)
        {
            Func<bool> func = () =>
            {
                return _vm.OnCmdSaveTo();
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                this.BeginInvoke(callBackAction, new object[] { result });
            }, null);
        }

        private void crystalButton_RecommendTime_Click(object sender, EventArgs e)
        {
            if (MonitorAllConfig.Instance().AcquisitionConfig.IsAutoRefresh)
            {
                ShowCustomMessageBox(
                    CommonUI.GetCustomMessage(HsLangTable, "CycleRefreshTimeTitle", "为了获取时间准确性，请停止自动执行,并保存后，再试..."),
                    "", MessageBoxButtons.OK, MessageBoxIconType.Alert);
                return;
            }
            _stopWatch.Restart();
            _vm.CmdRefreshTime.Execute(null);
            _isRefresh = true;
            ShowSending("CycleRefreshTime", "正在获取推荐周期配置,请稍候...", true);
        }
    }
}
