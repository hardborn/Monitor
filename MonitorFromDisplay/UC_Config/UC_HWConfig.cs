using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Xml.Serialization;
using Nova.Monitoring.MonitorDataManager;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.Common;
using Nova.Monitoring.UI.MonitorSetting;
using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_HWConfig : UC_ConfigBase
    {
        private UC_HWConfig_VM _vm = new UC_HWConfig_VM();
        private int MaxFanCount = 4;
        private int MaxPowerCount = 13;
        private string _sn10 = string.Empty;
        public UC_HWConfig()
        {
            InitializeComponent();
            uCHWConfigVMBindingSource.DataSource = _vm;
            UpdateLanguage();
        }
        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable = null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_HWConfig_String", out _hashTable);
            HsLangTable = _hashTable;
        }

        #region 消息注册
        protected override void Register()
        {
            Messenger.Default.Register<string>(this, MsgToken.MSG_HWConfig, OnMsgHWConfig);
        }
        protected override void UnRegister()
        {
            Messenger.Default.Unregister<string>(this, MsgToken.MSG_HWConfig, OnMsgHWConfig);
        }
        private void OnMsgHWConfig(string sn)
        {
            if (MonitorAllConfig.Instance().IsAllScreen(sn))
            {
                radioButton_DifferentFanCount.Enabled = false;
                radioButton_DifferentPowerCount.Enabled = false;
            }
            else
            {
                string[] str = sn.Split('-');
                _sn10 = str[0] + "-" + (Convert.ToInt32(str[1], 16) + 1).ToString("00");
                radioButton_DifferentFanCount.Enabled = true;
                radioButton_DifferentPowerCount.Enabled = true;
            }
            _vm.ReceiveMsgHWConfig(sn);         
        }
        #endregion

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            if (_vm == null) 
                return;

            SaveConfig((result) =>
            {
                if (result)
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "HwConfigSuccess", "硬件保存成功!"), 
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                else
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "HwConfigFailed", "硬件保存失败!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                CloseProcessForm();
             });
            ShowSending("SaveHWConfig", "正在保存硬件的配置,请稍候...", true);
        }

        private void SaveConfig(Action<bool> callBackAction)
        {
            Func<bool> func = () =>
            {
                return _vm.UpdateHWCfg();
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                this.BeginInvoke(callBackAction, new object[] { result });
            }, null);
        }

        #region 事件

        private void crystalButton_FanCountSetting_Click(object sender, EventArgs e)
        {
            SettingCommInfo commInfo = new SettingCommInfo();
            commInfo.SameCount = (byte)numericUpDown_MCSameFanCount.Value;
            commInfo.TypeStr = CommonUI.GetCustomMessage(HsLangTable,"HwFanName","风扇");
            commInfo.IconImage = Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Fan;
            commInfo.MaxCount = (byte)(MaxFanCount);
            if (_vm.FanInfo.AllFanCountDif == null || _vm.FanInfo.AllFanCountDif.Count == 0)
            {
                SerializableDictionary<string, byte> moinfos = new SerializableDictionary<string, byte>();
                SetCount(MonitorAllConfig.Instance().AllCommPortLedDisplayDic[_vm.SN],
                    _vm.SN.Replace("-",""), commInfo.SameCount, out moinfos);
                _vm.FanInfo.AllFanCountDif = moinfos;
            }
            
            Frm_FanPowerAdvanceSetting setFanCntFrm = new Frm_FanPowerAdvanceSetting(
                MonitorAllConfig.Instance().AllCommPortLedDisplayDic[_vm.SN],
                string.IsNullOrEmpty(MonitorAllConfig.Instance().CurrentScreenName) ? _sn10 : MonitorAllConfig.Instance().CurrentScreenName,
                _vm.SN.Replace("-", ""), _vm.FanInfo.AllFanCountDif, commInfo);
            setFanCntFrm.StartPosition = FormStartPosition.CenterParent;
            //setFanCntFrm.UpdateFont(Frm_MonitorStatusDisplay.CurrentFont);
            setFanCntFrm.UpdateLanguage(CommonUI.LanguageName);
            if (setFanCntFrm.ShowDialog() == DialogResult.OK)
            {
                _vm.FanInfo.AllFanCountDif = setFanCntFrm.CurAllSettingDic;
            }
        }

        private void crystalButton_PowerCountSetting_Click(object sender, EventArgs e)
        {
            SettingCommInfo commInfo = new SettingCommInfo();
            commInfo.SameCount = (byte)numericUpDown_MCSamePowerCount.Value;
            commInfo.TypeStr = CommonUI.GetCustomMessage(HsLangTable,"HwPowerName","电源");
            commInfo.IconImage = Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Power_Setting;
            commInfo.MaxCount = (byte)(MaxPowerCount);
            if (_vm.MCPower.AllPowerCountDif == null || _vm.MCPower.AllPowerCountDif.Count == 0)
            {
                SerializableDictionary<string, byte> moinfos = new SerializableDictionary<string, byte>();
                SetCount(MonitorAllConfig.Instance().AllCommPortLedDisplayDic[_vm.SN],
                    _vm.SN.Replace("-",""), commInfo.SameCount, out moinfos);
                _vm.MCPower.AllPowerCountDif = moinfos;
            }
            Frm_FanPowerAdvanceSetting setPowerCntFrm = new Frm_FanPowerAdvanceSetting(
                MonitorAllConfig.Instance().AllCommPortLedDisplayDic[_vm.SN],
                string.IsNullOrEmpty(MonitorAllConfig.Instance().CurrentScreenName) ? _sn10 : MonitorAllConfig.Instance().CurrentScreenName,
                _vm.SN.Replace("-", ""), _vm.MCPower.AllPowerCountDif, commInfo);
            setPowerCntFrm.StartPosition = FormStartPosition.CenterParent;
            //setFanCntFrm.UpdateFont(Frm_MonitorStatusDisplay.CurrentFont);
            setPowerCntFrm.UpdateLanguage(CommonUI.LanguageName);
            if (setPowerCntFrm.ShowDialog() == DialogResult.OK)
            {
                _vm.MCPower.AllPowerCountDif = setPowerCntFrm.CurAllSettingDic;
            }
        }

        private void radioButton_SameFanCount_Click(object sender, EventArgs e)
        {
            radioButton_SameFanCount.Checked = true;
        }

        private void radioButton_DifferentFanCount_Click(object sender, EventArgs e)
        {
            radioButton_DifferentFanCount.Checked = true;
            if (_vm.FanInfo.AllFanCountDif == null || _vm.FanInfo.AllFanCountDif.Count == 0)
            {
                SerializableDictionary<string, byte> moinfos = new SerializableDictionary<string, byte>();
                SetCount(MonitorAllConfig.Instance().AllCommPortLedDisplayDic[_vm.SN],
                    _vm.SN.Replace("-", ""), (byte)numericUpDown_MCSameFanCount.Value, out moinfos);
                _vm.FanInfo.AllFanCountDif = moinfos;
            }
        }

        private void radioButton_SamePowerCount_Click(object sender, EventArgs e)
        {
            radioButton_SamePowerCount.Checked = true;
        }

        private void radioButton_DifferentPowerCount_Click(object sender, EventArgs e)
        {
            radioButton_DifferentPowerCount.Checked = true;
            if (_vm.MCPower.AllPowerCountDif == null || _vm.MCPower.AllPowerCountDif.Count == 0)
            {
                SerializableDictionary<string, byte> moinfos = new SerializableDictionary<string, byte>();
                SetCount(MonitorAllConfig.Instance().AllCommPortLedDisplayDic[_vm.SN],
                    _vm.SN.Replace("-", ""), (byte)numericUpDown_MCSamePowerCount.Value, out moinfos);
                _vm.MCPower.AllPowerCountDif = moinfos;
            }
        }

        #endregion

        #region 内部方法

        private void SetCount(List<ILEDDisplayInfo> oneLedInfos, string sn, byte bcount,
            out SerializableDictionary<string, byte> mcInfo)
        {
            mcInfo = new SerializableDictionary<string, byte>();
            ScanBoardRegionInfo sbRegionInfo = null;
            string sbAddr = "";
            if (oneLedInfos == null || oneLedInfos.Count == 0)
            {
                return;
            }
            foreach (ILEDDisplayInfo led in oneLedInfos)
            {
                if (led == null || led.ScannerCount <= 0)
                {
                    continue;
                }
                for (int n = 0; n < led.ScannerCount; n++)
                {
                    sbRegionInfo = led[n];
                    if (sbRegionInfo == null || sbRegionInfo.SenderIndex==255)
                    {
                        continue;
                    }
                    sbAddr = GetSBAddr(sn, sbRegionInfo.SenderIndex, sbRegionInfo.PortIndex, sbRegionInfo.ConnectIndex);
                    mcInfo.Add(sbAddr, bcount);
                }
            }
        }
        private static string GetSBAddr(string commPort, byte senderAddr, byte portAddr, UInt16 sbAddr)
        {
            return commPort + '-' + senderAddr.ToString() + '-'
                    + portAddr.ToString() + '-' + sbAddr.ToString();
        }
        #endregion
    }
}
