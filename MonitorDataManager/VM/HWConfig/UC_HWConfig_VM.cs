using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.Common;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_HWConfig_VM : ViewModelBase
    {
        #region 属性
        private string _sn;
        public string SN
        {
            get { return _sn; }
            set
            {
                _sn = value;
                RaisePropertyChanged("SN");
            }
        }
        private bool _isMonitorCardConnected = false;
        public bool IsMonitorCardConnected
        {
            get
            {
                return _isMonitorCardConnected;
            }
            set
            {
                _isMonitorCardConnected = value;
                RaisePropertyChanged("IsMonitorCardConnected");
            }
        }
        private bool _isRefreshHumidity = false;
        public bool IsRefreshHumidity
        {
            get { return _isRefreshHumidity; }
            set
            {
                _isRefreshHumidity = value;
                RaisePropertyChanged("IsRefreshHumidity");
            }
        }
        private bool _isRefreshSmoke = false;
        public bool IsRefreshSmoke
        {
            get { return _isRefreshSmoke; }
            set
            {
                _isRefreshSmoke = value;
                RaisePropertyChanged("IsRefreshSmoke");
            }
        }
        private bool _isRefreshCabinet = false;
        public bool IsRefreshCabinet
        {
            get { return _isRefreshCabinet; }
            set
            {
                _isRefreshCabinet = value;
                RaisePropertyChanged("IsRefreshCabinet");
            }
        }
        private bool _isRefreshCabinetDoor = false;
        public bool IsRefreshCabinetDoor
        {
            get { return _isRefreshCabinetDoor; }
            set
            {
                _isRefreshCabinetDoor = value;
                RaisePropertyChanged("IsRefreshCabinetDoor");
            }
        }
        private UC_HWConfig_CabinetFanInfo_VM _fanInfo = new UC_HWConfig_CabinetFanInfo_VM();
        public UC_HWConfig_CabinetFanInfo_VM FanInfo
        {
            get { return _fanInfo; }
            set
            {
                _fanInfo = value;
                RaisePropertyChanged("FanInfo");
            }
        }
        private UC_HWConfig_MonitorCardPower_VM _mcPower = new UC_HWConfig_MonitorCardPower_VM();
        public UC_HWConfig_MonitorCardPower_VM MCPower
        {
            get { return _mcPower; }
            set
            {
                _mcPower = value;
                RaisePropertyChanged("MCPower");
            }
        }

        #endregion
        public UC_HWConfig_VM()
        {
            //Register();
        }
        #region 消息注册
        //private void Register()
        //{
        //    Messenger.Default.Register<string>(this, MsgToken.MSG_HWConfig, OnMsgHWConfig);
        //}
        public void ReceiveMsgHWConfig(string sn)
        {
            Initialize(sn);
        }
        //private void UnRegister()
        //{
        //    Messenger.Default.Unregister<string>(this, MsgToken.MSG_HWConfig, OnMsgHWConfig);
        //}
        #endregion
        #region 数据初始化
        private void Initialize(string sn)
        {
            _sn = sn;
            LedMonitoringConfig ledMonitorCfg = null;

            List<LedMonitoringConfig> listHw = MonitorAllConfig.Instance().LedMonitorConfigs;

            if (listHw == null)
            {
                return;
            }

            foreach (LedMonitoringConfig item in listHw)
            {
                if (_sn == MonitorAllConfig.Instance().ALLScreenName)
                {
                    ledMonitorCfg = item;
                    break;
                }
                else if (_sn == item.SN)
                {
                    ledMonitorCfg = item;
                    break;
                }
            }
            if (ledMonitorCfg != null)
            {
                InitialHwConfg(ledMonitorCfg);
            }
        }
        private void InitialHwConfg(LedMonitoringConfig ledMonitorCfg)
        {
            if (ledMonitorCfg == null || ledMonitorCfg.MonitoringCardConfig == null || ledMonitorCfg.SN == null)
            {
                return;
            }
            IsMonitorCardConnected = ledMonitorCfg.MonitoringCardConfig.MonitoringEnable;
            if (ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable == null || ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Count == 0)
            {
                return;
            }

            ParameterMonitoringConfig cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.FlatCableStatus));
            if (cfg != null)
            {
                IsRefreshCabinet = cfg.MonitoringEnable;
            }
            cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.DoorStatus));
            if (cfg != null)
            {
                IsRefreshCabinetDoor = cfg.MonitoringEnable;
            }
            cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.Humidity));
            if (cfg != null)
            {
                IsRefreshHumidity = cfg.MonitoringEnable;
            }
            cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.Smoke));
            if (cfg != null)
            {
                IsRefreshSmoke = cfg.MonitoringEnable;
            }
            cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.FanSpeed));
            FanInfo = new UC_HWConfig_CabinetFanInfo_VM();
            if (cfg != null)
            {
                FanInfo.IsRefreshFan = cfg.MonitoringEnable;
                if (FanInfo.IsRefreshFan)
                {
                    FanInfo.FanSpeed = cfg.ReservedConfig;
                    if (_sn == MonitorAllConfig.Instance().ALLScreenName)
                    {
                        cfg.ConfigMode = ParameterConfigMode.GeneralExtendedMode;
                    }
                    if (cfg.ConfigMode == ParameterConfigMode.GeneralExtendedMode)
                    {
                        FanInfo.AllFanOfCabinetSame = true;
                        FanInfo.AllFanOfCabinetDif = false;
                        FanInfo.FanCount = cfg.GeneralExtendedConfig;
                    }
                    else if (cfg.ConfigMode == ParameterConfigMode.AdvanceExtendedMode)
                    {
                        FanInfo.AllFanOfCabinetSame = false;
                        FanInfo.AllFanOfCabinetDif = true;
                        FanInfo.AllFanCountDif = new SerializableDictionary<string, byte>();
                        foreach (ParameterExtendedConfig param in cfg.ExtendedConfig)
                        {
                            FanInfo.AllFanCountDif.Add(param.ReceiveCardId, (byte)param.ParameterCount);
                        }
                    }
                }
            }
            cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.Voltage));
            MCPower = new UC_HWConfig_MonitorCardPower_VM();
            if (cfg != null)
            {
                MCPower.IsRefreshPower = cfg.MonitoringEnable;
                if (MCPower.IsRefreshPower)
                {
                    if (_sn == MonitorAllConfig.Instance().ALLScreenName)
                    {
                        cfg.ConfigMode = ParameterConfigMode.GeneralExtendedMode;
                    }
                    if (cfg.ConfigMode == ParameterConfigMode.GeneralExtendedMode)
                    {
                        MCPower.AllPowerOfCabinetSame = true;
                        MCPower.AllPowerOfCabinetDif = false;
                        MCPower.PowerCount = cfg.GeneralExtendedConfig;
                    }
                    else if (cfg.ConfigMode == ParameterConfigMode.AdvanceExtendedMode)
                    {
                        MCPower.AllPowerOfCabinetSame = false;
                        MCPower.AllPowerOfCabinetDif = true;
                        MCPower.AllPowerCountDif = new SerializableDictionary<string, byte>();
                        foreach (ParameterExtendedConfig param in cfg.ExtendedConfig)
                        {
                            MCPower.AllPowerCountDif.Add(param.ReceiveCardId, (byte)param.ParameterCount);
                        }
                    }
                }
            }
        }
        #endregion
        #region 数据提交命令
        private RelayCommand _saveWHConfig;
        public RelayCommand SaveWHConfig
        {
            get
            {
                if (_saveWHConfig == null)
                {
                    _saveWHConfig = new RelayCommand(() => { UpdateHWCfg(); }, null);
                }
                return _saveWHConfig;
            }
        }
        public bool UpdateHWCfg()
        {
            LedMonitoringConfig ledHWCfg = SetMonitorCfg();
            return MonitorAllConfig.Instance().UpdateLedMonitoringConfig(_sn, ledHWCfg);
        }
        private LedMonitoringConfig SetMonitorCfg()
        {
            LedMonitoringConfig ledHWCfg = new LedMonitoringConfig();
            ledHWCfg.SN = _sn;
            ledHWCfg.MonitoringCardConfig = new MonitoringCardConfig();
            ledHWCfg.MonitoringCardConfig.MonitoringEnable = IsMonitorCardConnected;
            if (!IsMonitorCardConnected)
            {
                return ledHWCfg;
            }
            ledHWCfg.MonitoringCardConfig.ParameterConfigTable = new List<ParameterMonitoringConfig>();
            ParameterMonitoringConfig pCfg = new ParameterMonitoringConfig();
            //Humidity
            pCfg.ConfigMode = ParameterConfigMode.NoneExtendedMode;
            pCfg.MonitoringEnable = IsRefreshHumidity;
            pCfg.Type = StateQuantityType.Humidity;
            ledHWCfg.MonitoringCardConfig.ParameterConfigTable.Add(pCfg);
            //smoke
            ParameterMonitoringConfig pCfg1 = new ParameterMonitoringConfig();
            pCfg1.ConfigMode = ParameterConfigMode.NoneExtendedMode;
            pCfg1.Type = StateQuantityType.Smoke;
            pCfg1.MonitoringEnable = IsRefreshSmoke;
            ledHWCfg.MonitoringCardConfig.ParameterConfigTable.Add(pCfg1);
            //cabinet
            ParameterMonitoringConfig pCfg2 = new ParameterMonitoringConfig();
            pCfg2.Type = StateQuantityType.FlatCableStatus;
            pCfg2.ConfigMode = ParameterConfigMode.NoneExtendedMode;
            pCfg2.MonitoringEnable = IsRefreshCabinet;
            ledHWCfg.MonitoringCardConfig.ParameterConfigTable.Add(pCfg2);
            //cabinetdoor
            ParameterMonitoringConfig pCfg3 = new ParameterMonitoringConfig();
            pCfg3.ConfigMode = ParameterConfigMode.NoneExtendedMode;
            pCfg3.Type = StateQuantityType.DoorStatus;
            pCfg3.MonitoringEnable = IsRefreshCabinetDoor;
            ledHWCfg.MonitoringCardConfig.ParameterConfigTable.Add(pCfg3);
            //fan
            ParameterMonitoringConfig pCfg4 = new ParameterMonitoringConfig();
            if (FanInfo != null)
            {
                pCfg4.ReservedConfig = FanInfo.FanSpeed;
                if (FanInfo.IsRefreshFan)
                {
                    if (FanInfo.AllFanOfCabinetSame)
                    {
                        pCfg4.ConfigMode = ParameterConfigMode.GeneralExtendedMode;
                        pCfg4.MonitoringEnable = FanInfo.IsRefreshFan;
                        pCfg4.GeneralExtendedConfig = FanInfo.FanCount;
                    }
                    else if (FanInfo.AllFanOfCabinetDif)
                    {
                        pCfg4.ConfigMode = ParameterConfigMode.AdvanceExtendedMode;
                        pCfg4.ExtendedConfig = new List<ParameterExtendedConfig>();
                        if (FanInfo.AllFanCountDif != null)
                        {
                            foreach (KeyValuePair<string, byte> keyValue in FanInfo.AllFanCountDif)
                            {
                                ParameterExtendedConfig param = new ParameterExtendedConfig();
                                param.ReceiveCardId = keyValue.Key;
                                param.ParameterCount = keyValue.Value;
                                pCfg4.ExtendedConfig.Add(param);
                            }
                        }
                    }
                }
                pCfg4.MonitoringEnable = FanInfo.IsRefreshFan;
                pCfg4.Type = StateQuantityType.FanSpeed;
                ledHWCfg.MonitoringCardConfig.ParameterConfigTable.Add(pCfg4);
            }
            //Power

            ParameterMonitoringConfig pCfg5 = new ParameterMonitoringConfig();
            if (MCPower != null)
            {
                if (MCPower.IsRefreshPower)
                {
                    if (MCPower.AllPowerOfCabinetSame)
                    {
                        pCfg5.ConfigMode = ParameterConfigMode.GeneralExtendedMode;
                        pCfg5.MonitoringEnable = MCPower.IsRefreshPower;
                        pCfg5.GeneralExtendedConfig = MCPower.PowerCount;
                    }
                    else if (MCPower.AllPowerOfCabinetDif)
                    {
                        pCfg5.ConfigMode = ParameterConfigMode.AdvanceExtendedMode;
                        pCfg5.ExtendedConfig = new List<ParameterExtendedConfig>();
                        if (MCPower.AllPowerCountDif != null)
                        {
                            foreach (KeyValuePair<string, byte> keyValue in MCPower.AllPowerCountDif)
                            {
                                ParameterExtendedConfig param = new ParameterExtendedConfig();
                                param.ReceiveCardId = keyValue.Key;
                                param.ParameterCount = keyValue.Value;
                                pCfg5.ExtendedConfig.Add(param);
                            }
                        }
                    }
                }
                pCfg5.MonitoringEnable = MCPower.IsRefreshPower;
                pCfg5.Type = StateQuantityType.Voltage;
                ledHWCfg.MonitoringCardConfig.ParameterConfigTable.Add(pCfg5);
            }
            return ledHWCfg;
        }
        #endregion
        //public void Dispose()
        //{
        //    UnRegister();
        //}
    }
}
