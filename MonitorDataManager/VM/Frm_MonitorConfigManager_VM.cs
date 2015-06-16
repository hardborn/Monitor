using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class Frm_MonitorConfigManager_VM : ViewModelBase
    {
        private List<ComboBoxData_VM> _ledInfos;
        public List<ComboBoxData_VM> LedInfos
        {
            get
            {
                return _ledInfos;
            }
            set
            {
                _ledInfos = value;
                RaisePropertyChanged("LedInfos");
            }
        }

        public LedBasicInfo CurSelectedSN
        {
            get
            {
                return _curSelectedSN;
            }
            set
            {
                _curSelectedSN = value;
                RaisePropertyChanged("CurSelectedSN");
            }
        }
        private LedBasicInfo _curSelectedSN = null;
        public string CurrentName
        {
            get
            {
                return MonitorAllConfig.Instance().CurrentScreenName;
            }
            set
            {
                MonitorAllConfig.Instance().CurrentScreenName = value;
            }
        }

        #region 构造函数
        public Frm_MonitorConfigManager_VM()
        {
            if (!this.IsInDesignMode)
            {
                CmdInitialize = new RelayCommand(Initialize);
                CmdOpenFunc = new RelayCommand<string>(OnCmdOpenFunc);
            }
        }
        #endregion

        #region 命令
        public RelayCommand CmdInitialize
        {
            get;
            private set;
        }

        public RelayCommand<string> CmdOpenFunc
        {
            get;
            private set;
        }
        #endregion

        private void Initialize()
        {
            if (MonitorAllConfig.Instance().LedInfoList == null || MonitorAllConfig.Instance().LedInfoList.Count == 0)
            {
                if (_ledInfos != null)
                {
                    _ledInfos.Clear();
                }
                return;
            }
            List<ComboBoxData_VM> ledInfos = new List<ComboBoxData_VM>();
            foreach (LedBasicInfo led in MonitorAllConfig.Instance().LedInfoList)
            {
                LedRegistationInfoResponse ledResponse = MonitorAllConfig.Instance().LedRegistationUiList.Find(a=>a.SN==led.Sn);
                if (ledResponse == null || string.IsNullOrEmpty(ledResponse.Name))
                {
                    ledInfos.Add(new ComboBoxData_VM() { Name = led.Commport + "-" +
                        MonitorAllConfig.Instance().ScreenName+
                        (led.LedIndexOfCom + 1), Data = led.Clone() });
                }
                else
                {
                    ledInfos.Add(new ComboBoxData_VM() { Name = ledResponse.Name, Data = led.Clone() });
                }
            }
            if (ledInfos.Count > 1)
            {
                ledInfos.Insert(0, new ComboBoxData_VM()
                {
                    Name = MonitorAllConfig.Instance().ALLScreenName,
                    Data = ((LedBasicInfo)ledInfos[0].Data).Clone()
                });
                ((LedBasicInfo)ledInfos[0].Data).Sn = MonitorAllConfig.Instance().ALLScreenName;
            }

            LedInfos = ledInfos;

            CurSelectedSN = (LedBasicInfo)(ledInfos[0].Data);
            CurrentName = ledInfos[0].Name;
        }

        private void OnCmdOpenFunc(string funcName)
        {
            if (CurSelectedSN == null || string.IsNullOrEmpty(funcName))
            {
                return;
            }
            string strMsg = string.Empty;
            switch (funcName)
            {
                case "crystalButton_RefreshConfig":
                    strMsg = MsgToken.MSG_RefreshConfig;
                    break;
                case "crystalButton_HWConfig":
                    strMsg = MsgToken.MSG_HWConfig;
                    break;
                //case "crystalButton_CareRegisterConfig":
                //    strMsg = MsgToken.MSG_CareServerConfig;
                //    break;
                case "crystalButton_MonitorCardPowerConfig":
                    strMsg = MsgToken.MSG_MonitorCardPowerConfig;
                    break;
                case "crystalButton_DataAltarmConfig":
                    strMsg = MsgToken.MSG_DataAltarmConfig;
                    break;
                case "crystalButton_ControlConfig":
                    strMsg = MsgToken.MSG_ControlConfig;
                    break;
                case "crystalButton_ControlLog":
                    strMsg = MsgToken.MSG_ControlLog;
                    break;
                case "crystalButton_BrightnessConfig":
                    strMsg = MsgToken.MSG_BrightnessConfig;
                    break;
                case "crystalButton_NotifySetting":
                    strMsg = MsgToken.MSG_EmailNotifyConfig;
                    break;
                case "crystalButton_EMailLog":
                    strMsg = MsgToken.MSG_EMailLog;
                    break;
                default:
                    break;
            }
            Messenger.Default.Send<string>(CurSelectedSN.Sn, strMsg);
        }
    }
}