using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Nova.LCT.GigabitSystem.Common;
using Nova.Monitoring.Common;
using Nova.Monitoring.HardwareMonitorInterface;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class Frm_MonitorDisplayMain_VM : ViewModelBase
    {
        public List<MonitorDataFlag> MonitorDataFlags { get; set; }

        public string SN
        {
            get
            {
                return _sn;
            }
            set
            {
                if (_sn == value)
                {
                    IsRefreshDetails = false;
                }
                else
                {
                    IsRefreshDetails = true;
                }
                _sn = value;
                //ScreenData = MonitorAllConfig.Instance().ScreenMonitorData.AllScreenMonitorCollection.Find(a => a.ScreenUDID == SN);
            }
        }
        public string SN10 { get; set; }

        private string _sn = string.Empty;
        public int SelectColIndex { get; set; }
        public AllMonitorData ScreenMonitorData
        {
            get;
            private set;
        }
        //public SerializableDictionary<string, List<Nova.LCT.GigabitSystem.Common.ILEDDisplayInfo>> AllCommPortLeds
        //{
        //    get;
        //    private set;
        //}
        public bool IsGetData
        {
            get;
            private set;
        }
        public bool IsRefreshDetails { get; set; }

        public Frm_MonitorDisplayMain_VM()
        {
            MonitorDataFlags = new List<MonitorDataFlag>();
            //AllCommPortLeds = new SerializableDictionary<string, List<LCT.GigabitSystem.Common.ILEDDisplayInfo>>();
        }

        private Log4NetLibrary.ILogService _fLogService = new Log4NetLibrary.FileLogService(typeof(Frm_MonitorDisplayMain_VM));
        private LCTMainMonitorData _lctData = null;
        public bool OnCmdInitialize()
        {
            IsGetData = false;
            if (MonitorAllConfig.Instance().ScreenMonitorData == null || MonitorAllConfig.Instance().ScreenMonitorData.AllScreenMonitorCollection == null
                || MonitorAllConfig.Instance().ScreenMonitorData.AllScreenMonitorCollection.Count == 0)
            {
                ScreenMonitorData = null;
                _fLogService.Error("获取数据结果为空，导致界面失败");
                return false;
            }
            foreach (ScreenModnitorData oneScreenData in MonitorAllConfig.Instance().ScreenMonitorData.AllScreenMonitorCollection)
            {
                if (oneScreenData.SenderMonitorCollection == null || oneScreenData.SenderMonitorCollection.Count == 0
                    || oneScreenData.ScannerMonitorCollection == null || oneScreenData.ScannerMonitorCollection.Count == 0)
                {
                    _fLogService.Error("获取数据结果不完整，因此不能推送到主界面");
                    return false;
                }
            }
            IsGetData = true;

            ScreenMonitorData = (AllMonitorData)MonitorAllConfig.Instance().ScreenMonitorData.Clone();
            List<string> screenList = new List<string>();

            _lctData = new LCTMainMonitorData();
            foreach (LedMonitoringConfig monitorConfig in MonitorAllConfig.Instance().LedMonitorConfigs)
            {
                _lctData.RefreshTypeInfo.IsUpdateMCStatus = true;
                LedBasicInfo led = MonitorAllConfig.Instance().LedInfoList.FirstOrDefault(a => a.Sn == monitorConfig.SN);
                if (led == null)
                {
                    continue;
                }
                if (monitorConfig.MonitoringCardConfig != null && monitorConfig.MonitoringCardConfig.MonitoringEnable == true
                    && monitorConfig.MonitoringCardConfig.ParameterConfigTable != null)
                {
                    foreach (ParameterMonitoringConfig param in monitorConfig.MonitoringCardConfig.ParameterConfigTable)
                    {
                        if (param.Type == StateQuantityType.FlatCableStatus
                            && _lctData.RefreshTypeInfo.IsUpdateRowLine == false && param.MonitoringEnable)
                        {
                            _lctData.RefreshTypeInfo.IsUpdateRowLine = true;
                        }
                        if (param.Type == StateQuantityType.DoorStatus
                            && _lctData.RefreshTypeInfo.IsUpdateGeneralStatus == false && param.MonitoringEnable)
                        {
                            _lctData.RefreshTypeInfo.IsUpdateGeneralStatus = true;
                        }
                        if (param.Type == StateQuantityType.Humidity
                            && _lctData.RefreshTypeInfo.IsUpdateHumidity == false && param.MonitoringEnable)
                        {
                            _lctData.RefreshTypeInfo.IsUpdateHumidity = true;
                        }
                        if (param.Type == StateQuantityType.Smoke
                            && _lctData.RefreshTypeInfo.IsUpdateSmoke == false && param.MonitoringEnable)
                        {
                            _lctData.RefreshTypeInfo.IsUpdateSmoke = true;
                        }
                        if (param.Type == StateQuantityType.FanSpeed
                            && _lctData.RefreshTypeInfo.IsUpdateFan == false && param.MonitoringEnable)
                        {
                            _lctData.RefreshTypeInfo.IsUpdateFan = true;
                        }
                        if (param.Type == StateQuantityType.Voltage
                            && _lctData.RefreshTypeInfo.IsUpdatePower == false && param.MonitoringEnable)
                        {
                            _lctData.RefreshTypeInfo.IsUpdatePower = true;
                        }
                    }
                }
            }

            foreach (ScreenModnitorData oneScreenData in ScreenMonitorData.AllScreenMonitorCollection)
            {
                screenList.Add(oneScreenData.ScreenUDID);
                MonitorDataFlag monitorFlag = MonitorDataFlags.Find(a => a.SN == oneScreenData.ScreenUDID);
                if (monitorFlag == null)
                {
                    monitorFlag = new MonitorDataFlag();
                    monitorFlag.SN = oneScreenData.ScreenUDID;
                    monitorFlag.SNName = GetSnAlia(oneScreenData.ScreenUDID);
                    MonitorDataFlags.Add(monitorFlag);
                }
                else
                {
                    monitorFlag.SNName = GetSnAlia(oneScreenData.ScreenUDID);
                }
                LedAlarmConfig ledAlarmConfig = MonitorAllConfig.Instance().LedAlarmConfigs.Find(a =>
                    a.SN == oneScreenData.ScreenUDID);

                LedMonitoringConfig ledMonitorConfig = MonitorAllConfig.Instance().LedMonitorConfigs.Find(a =>
                    a.SN == oneScreenData.ScreenUDID);
                if (ledMonitorConfig == null)
                {
                    if (oneScreenData == null || string.IsNullOrEmpty(oneScreenData.ScreenUDID))
                    {
                        _fLogService.Error("获取数据结果:没有硬件配置导致异常");
                    }
                    else
                    {
                        _fLogService.Error("获取数据结果:没有硬件配置导致异常:" + oneScreenData.ScreenUDID);
                    }
                    ledMonitorConfig = new LedMonitoringConfig();
                    ledMonitorConfig.SN = oneScreenData.ScreenUDID;
                    ledMonitorConfig.MonitoringCardConfig = new MonitoringCardConfig();
                    ledMonitorConfig.MonitoringCardConfig.MonitoringEnable = false;
                }

                LedBasicInfo ledInfo = MonitorAllConfig.Instance().LedInfoList.Find(a => a.Sn == oneScreenData.ScreenUDID);
                if (ledInfo == null)
                {
                    continue;
                }
                SetSenderMonitor(ledInfo, oneScreenData.SenderMonitorCollection, monitorFlag);
                SetScannerMonitor(ledInfo, oneScreenData.ScannerMonitorCollection, ledAlarmConfig, monitorFlag);
                SetMonitorCardMonitor(ledInfo, oneScreenData.MonitorCardInfoCollection, ledAlarmConfig, ledMonitorConfig, monitorFlag);
                SetRegisterMonitor(monitorFlag);
            }
            for (int i = 0; i < MonitorDataFlags.Count; )
            {
                if (screenList.Contains(MonitorDataFlags[i].SN))
                {
                    i++;
                }
                else
                {
                    MonitorDataFlags.RemoveAt(i);
                }
            }
            SetRedundantStateInfos();
            MonitorAllConfig.Instance().SendMonitorDataToLCT(_lctData);
            return true;
        }

        private void SetRedundantStateInfos()
        {
            if (ScreenMonitorData.RedundantStateType != null && ScreenMonitorData.RedundantStateType.Count > 0)
            {
                if (ScreenMonitorData.TempRedundancyDict.Count > 0)
                {
                    SerializableDictionary<int, RedundantStateInfo> redundantStatDataList = null;
                    RedundantStateInfo redundantStatData = null;
                    foreach (string commPort in ScreenMonitorData.RedundantStateType.Keys)
                    {
                        if (ScreenMonitorData.RedundantStateType[commPort] == null
                            || ScreenMonitorData.RedundantStateType[commPort].Count <= 0)
                        {
                            continue;
                        }
                        redundantStatDataList = new SerializableDictionary<int, RedundantStateInfo>();
                        foreach (int var in ScreenMonitorData.RedundantStateType[commPort].Keys)
                        {
                            redundantStatData = new RedundantStateInfo();
                            if (ScreenMonitorData.CommPortData == null)
                            {
                                break;
                            }
                            SenderRedundantStateInfo state = ScreenMonitorData.RedundantStateType[commPort][var];
                            redundantStatData.CommPort = commPort;
                            redundantStatData.SenderIndex = (byte)var;
                            SenderRedundancyInfo senderInfo = null;

                            if (ScreenMonitorData.CommPortData.ContainsKey(commPort))
                            {
                                redundantStatData.PortCount = ScreenMonitorData.CommPortData[commPort];
                                for (int i = 0; i < ScreenMonitorData.CommPortData[commPort]; i++)
                                {
                                    if (state.RedundantStateTypeList[i] == RedundantStateType.Error)
                                    {
                                        if (ScreenMonitorData.TempRedundancyDict.ContainsKey(commPort) && ScreenMonitorData.TempRedundancyDict[commPort] != null)
                                        {
                                            for (int j = 0; j < ScreenMonitorData.TempRedundancyDict[commPort].Count; j++)
                                            {
                                                senderInfo = (SenderRedundancyInfo)ScreenMonitorData.TempRedundancyDict[commPort][j].Clone();
                                                if (senderInfo.SlavePortIndex == i && senderInfo.SlaveSenderIndex == var)
                                                {
                                                    redundantStatData.RedundantStateTypeList.Add(i, state.RedundantStateTypeList[i]);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else if (state.RedundantStateTypeList[i] == RedundantStateType.Unknown)
                                    {
                                        continue;
                                    }
                                }
                            }
                            if (redundantStatData.RedundantStateTypeList.Count > 0)
                            {
                                redundantStatDataList.Add(var, redundantStatData);
                            }
                        }
                        if (redundantStatDataList.Count > 0)
                        {
                            _lctData.RedundantStateInfoDic.Add(commPort, redundantStatDataList);
                        }
                    }
                }
            }
        }

        #region 获取数据

        private string GetSnAlia(string sn)
        {
            LedRegistationInfoResponse ledReg = MonitorAllConfig.Instance().LedRegistationUiList.Find(a => a.SN == sn);
            LedBasicInfo ledInfo = MonitorAllConfig.Instance().LedInfoList.Find(a => a.Sn == sn);
            if (ledReg == null || string.IsNullOrEmpty(ledReg.Name))
            {
                if (ledInfo == null)
                {
                    string[] str = sn.Split('-');
                    SN10 = str[0] + "-" + (Convert.ToInt32(str[1], 16) + 1).ToString("00");
                }
                else
                {
                    SN10 = ledInfo.Commport + "-" + MonitorAllConfig.Instance().ScreenName + (ledInfo.LedIndexOfCom + 1);
                }
            }
            else
            {
                SN10 = ledReg.Name;
            }
            return SN10;
        }
        public void SetSenderMonitor(LedBasicInfo ledInfo, List<SenderMonitorInfo> senderInfos, MonitorDataFlag monitorFlag)
        {
            monitorFlag.IsSenderDVIValid = DeviceWorkStatus.Unknown;
            foreach (SenderMonitorInfo senderInfo in senderInfos)
            {
                if (senderInfo.DeviceStatus == DeviceWorkStatus.Error || senderInfo.IsDviConnected == false)
                {
                    monitorFlag.IsSenderDVIValid = DeviceWorkStatus.Error;
                    if(!_lctData.FaultMonitorInfos.ContainsKey(ledInfo.Commport))
                    {
                        _lctData.FaultMonitorInfos.Add(ledInfo.Commport,new MonitorErrData());
                    }
                    _lctData.FaultMonitorInfos[ledInfo.Commport].SenderDviExceptionCnt++;
                }
                else if (senderInfo.DeviceStatus == DeviceWorkStatus.OK &&
                    monitorFlag.IsSenderDVIValid == DeviceWorkStatus.Unknown)
                {
                    monitorFlag.IsSenderDVIValid = DeviceWorkStatus.OK;
                }
            }
            _lctData.ValidInfo.IsSenderDVIValid = true;
        }
        private void SetScannerMonitor(LedBasicInfo ledInfo, List<ScannerMonitorInfo> scannerInfos,
            LedAlarmConfig ledAlarmConfig, MonitorDataFlag monitorFlag)
        {
            monitorFlag.IsSBStatusValid = DeviceWorkStatus.Unknown;
            monitorFlag.IsTemperatureValid = DeviceWorkStatus.Unknown;
            float tempValue = 79;
            if (ledAlarmConfig.ParameterAlarmConfigList != null)
            {
                ParameterAlarmConfig param = ledAlarmConfig.ParameterAlarmConfigList.Find(a =>
                    a.ParameterType == StateQuantityType.Temperature);
                if (param != null)
                {
                    tempValue = (float)param.HighThreshold;
                }
            }

            foreach (ScannerMonitorInfo scannerInfo in scannerInfos)
            {
                if (scannerInfo.DeviceStatus == DeviceWorkStatus.Error)
                {
                    monitorFlag.IsSBStatusValid = DeviceWorkStatus.Error;
                    if (!_lctData.FaultMonitorInfos.ContainsKey(ledInfo.Commport))
                    {
                        _lctData.FaultMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                    }
                    _lctData.FaultMonitorInfos[ledInfo.Commport].SBStatusErrCount++;
                }
                else if (scannerInfo.DeviceStatus == DeviceWorkStatus.OK &&
                    monitorFlag.IsSBStatusValid == DeviceWorkStatus.Unknown)
                {
                    monitorFlag.IsSBStatusValid = DeviceWorkStatus.OK;
                }
                if (scannerInfo.TemperatureIsVaild == true)
                {
                    if (scannerInfo.Temperature > tempValue)
                    {
                        monitorFlag.IsTemperatureValid = DeviceWorkStatus.Error;
                        if (!_lctData.AlarmMonitorInfos.ContainsKey(ledInfo.Commport))
                        {
                            _lctData.AlarmMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                        }
                        _lctData.AlarmMonitorInfos[ledInfo.Commport].TemperatureAlarmCount++;
                        _lctData.ValidInfo.IsTemperatureValid = true;
                    }
                    else if (monitorFlag.IsTemperatureValid != DeviceWorkStatus.Error)
                    {
                        monitorFlag.IsTemperatureValid = DeviceWorkStatus.OK;
                        _lctData.ValidInfo.IsTemperatureValid = true;
                    }
                }
            }
            _lctData.ValidInfo.IsSBStatusValid = true;
        }
        private void SetMonitorCardMonitor(LedBasicInfo ledInfo, List<MonitorCardMonitorInfo> monitorCardInfos,
            LedAlarmConfig ledAlarmConfig, LedMonitoringConfig ledMonitorConfig, MonitorDataFlag monitorFlag)
        {
            if (ledMonitorConfig.MonitoringCardConfig == null || ledMonitorConfig.MonitoringCardConfig.MonitoringEnable == false)
            {
                monitorFlag.IsMCStatusValid = DeviceWorkStatus.UnAvailable;
                monitorFlag.IsFanValid = DeviceWorkStatus.UnAvailable;
                monitorFlag.IsHumidityValid = DeviceWorkStatus.UnAvailable;
                monitorFlag.IsPowerValid = DeviceWorkStatus.UnAvailable;
                monitorFlag.IsRowLineValid = DeviceWorkStatus.UnAvailable;
                monitorFlag.IsSmokeValid = DeviceWorkStatus.UnAvailable;
                monitorFlag.IsGeneralStatusValid = DeviceWorkStatus.UnAvailable;
                return;
            }

            monitorFlag.IsMCStatusValid = DeviceWorkStatus.Unknown;
            monitorFlag.IsFanValid = DeviceWorkStatus.Unknown;
            monitorFlag.IsHumidityValid = DeviceWorkStatus.Unknown;
            monitorFlag.IsPowerValid = DeviceWorkStatus.Unknown;
            monitorFlag.IsRowLineValid = DeviceWorkStatus.Unknown;
            monitorFlag.IsSmokeValid = DeviceWorkStatus.Unknown;
            monitorFlag.IsGeneralStatusValid = DeviceWorkStatus.Unknown;
            float fanValue = 1000;
            float humValue = 100;
            float valValue = 4;
            if (ledAlarmConfig.ParameterAlarmConfigList != null)
            {
                ParameterAlarmConfig param = ledAlarmConfig.ParameterAlarmConfigList.Find(a =>
                    a.ParameterType == StateQuantityType.FanSpeed);
                if (param != null)
                {
                    fanValue = (float)param.LowThreshold;
                }
                param = ledAlarmConfig.ParameterAlarmConfigList.Find(a =>
                    a.ParameterType == StateQuantityType.Humidity);
                if (param != null)
                {
                    humValue = (float)param.HighThreshold;
                }
            }
            ParameterMonitoringConfig monitorConfig = ledMonitorConfig.MonitoringCardConfig.ParameterConfigTable.Find
                (a => a.Type == StateQuantityType.FanSpeed);
            bool isFan = monitorConfig == null ? false : monitorConfig.MonitoringEnable;
            monitorConfig = ledMonitorConfig.MonitoringCardConfig.ParameterConfigTable.Find
                (a => a.Type == StateQuantityType.Humidity);
            bool isHumidity = monitorConfig == null ? false : monitorConfig.MonitoringEnable;
            monitorConfig = ledMonitorConfig.MonitoringCardConfig.ParameterConfigTable.Find
                (a => a.Type == StateQuantityType.Smoke);
            bool isSmoke = monitorConfig == null ? false : monitorConfig.MonitoringEnable;
            monitorConfig = ledMonitorConfig.MonitoringCardConfig.ParameterConfigTable.Find
                (a => a.Type == StateQuantityType.Voltage);
            bool isVoltage = monitorConfig == null ? false : monitorConfig.MonitoringEnable;
            monitorConfig = ledMonitorConfig.MonitoringCardConfig.ParameterConfigTable.Find
                (a => a.Type == StateQuantityType.DoorStatus);
            bool isDoorStatus = monitorConfig == null ? false : monitorConfig.MonitoringEnable;
            monitorConfig = ledMonitorConfig.MonitoringCardConfig.ParameterConfigTable.Find
                (a => a.Type == StateQuantityType.FlatCableStatus);
            bool isFlatCableStatus = monitorConfig == null ? false : monitorConfig.MonitoringEnable;

            _lctData.ValidInfo.IsMCStatusValid = true;
            foreach (MonitorCardMonitorInfo monitorCardInfo in monitorCardInfos)
            {
                if (monitorCardInfo.DeviceStatus == DeviceWorkStatus.Error)
                {
                    monitorFlag.IsMCStatusValid = DeviceWorkStatus.Error;
                    if (!_lctData.FaultMonitorInfos.ContainsKey(ledInfo.Commport))
                    {
                        _lctData.FaultMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                    }
                    _lctData.FaultMonitorInfos[ledInfo.Commport].MCStatusErrCount++;
                }
                else if (monitorCardInfo.DeviceStatus == DeviceWorkStatus.OK &&
                    monitorFlag.IsMCStatusValid == DeviceWorkStatus.Unknown)
                {
                    monitorFlag.IsMCStatusValid = DeviceWorkStatus.OK;
                }

                if (isFan == false)
                {
                    monitorFlag.IsFanValid = DeviceWorkStatus.UnAvailable;
                }
                else if (monitorCardInfo.FansUInfo == null)
                {
                    //if (monitorFlag.IsFanValid == DeviceWorkStatus.OK)
                    //{
                    //    monitorFlag.IsFanValid = DeviceWorkStatus.Error;
                    //}
                }
                else if (monitorCardInfo.FansUInfo.IsUpdate
                    && monitorCardInfo.FansUInfo.FansMonitorInfoCollection != null
                    && monitorCardInfo.FansUInfo.FansMonitorInfoCollection.Count > 0)
                {
                    foreach (int tmpValue in monitorCardInfo.FansUInfo.FansMonitorInfoCollection.Values)
                    {
                        if (tmpValue < fanValue)
                        {
                            monitorFlag.IsFanValid = DeviceWorkStatus.Error;
                            if (!_lctData.AlarmMonitorInfos.ContainsKey(ledInfo.Commport))
                            {
                                _lctData.AlarmMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                            }
                            _lctData.AlarmMonitorInfos[ledInfo.Commport].FanAlarmSwitchCount++;
                        }
                        else if (monitorCardInfo.DeviceStatus == DeviceWorkStatus.OK &&
                            monitorFlag.IsFanValid == DeviceWorkStatus.Unknown)
                        {
                            monitorFlag.IsFanValid = DeviceWorkStatus.OK;
                        }
                    }
                    _lctData.ValidInfo.IsFanValid = true;
                }

                if (isHumidity == false)
                {
                    monitorFlag.IsHumidityValid = DeviceWorkStatus.UnAvailable;
                }
                else if (monitorCardInfo.HumidityUInfo == null)
                {
                    //if (monitorFlag.IsHumidityValid == DeviceWorkStatus.OK)
                    //{
                    //    monitorFlag.IsHumidityValid = DeviceWorkStatus.Error;
                    //}
                }
                else if (monitorCardInfo.HumidityUInfo.IsUpdate)
                {
                    if (monitorCardInfo.HumidityUInfo.Humidity > humValue)
                    {
                        monitorFlag.IsHumidityValid = DeviceWorkStatus.Error;
                        if (!_lctData.AlarmMonitorInfos.ContainsKey(ledInfo.Commport))
                        {
                            _lctData.AlarmMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                        }
                        _lctData.AlarmMonitorInfos[ledInfo.Commport].HumidityAlarmCount++;
                    }
                    else if (monitorFlag.IsHumidityValid != DeviceWorkStatus.Error)
                    {
                        monitorFlag.IsHumidityValid = DeviceWorkStatus.OK;
                    }
                    _lctData.ValidInfo.IsHumidityValid = true;
                }

                if (isDoorStatus == false)
                {
                    monitorFlag.IsGeneralStatusValid = DeviceWorkStatus.UnAvailable;
                }
                else if (monitorCardInfo.CabinetDoorUInfo == null)
                {
                    //if (monitorFlag.IsGeneralStatusValid == DeviceWorkStatus.OK)
                    //{
                    //    monitorFlag.IsGeneralStatusValid = DeviceWorkStatus.Error;
                    //}
                }
                else if (monitorCardInfo.CabinetDoorUInfo.IsUpdate)
                {
                    if (monitorCardInfo.CabinetDoorUInfo.IsDoorOpen)
                    {
                        monitorFlag.IsGeneralStatusValid = DeviceWorkStatus.Error;
                        if (!_lctData.FaultMonitorInfos.ContainsKey(ledInfo.Commport))
                        {
                            _lctData.FaultMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                        }
                        _lctData.FaultMonitorInfos[ledInfo.Commport].GeneralSwitchErrCount++;
                    }
                    else if (monitorFlag.IsGeneralStatusValid != DeviceWorkStatus.Error)
                    {
                        monitorFlag.IsGeneralStatusValid = DeviceWorkStatus.OK;
                    }
                    _lctData.ValidInfo.IsGeneralStatusValid = true;
                }
                if (isVoltage == false)
                {
                    monitorFlag.IsPowerValid = DeviceWorkStatus.UnAvailable;
                }
                else if (monitorCardInfo.PowerUInfo == null)
                {
                    //if (monitorFlag.IsPowerValid == DeviceWorkStatus.OK)
                    //{
                    //    monitorFlag.IsPowerValid = DeviceWorkStatus.Error;
                    //}
                }
                else if (monitorCardInfo.PowerUInfo.IsUpdate
                    && monitorCardInfo.PowerUInfo.PowerMonitorInfoCollection != null)
                {
                    foreach (float tmpValue in monitorCardInfo.PowerUInfo.PowerMonitorInfoCollection.Values)
                    {
                        if (tmpValue < valValue)
                        {
                            monitorFlag.IsPowerValid = DeviceWorkStatus.Error;
                            if (!_lctData.FaultMonitorInfos.ContainsKey(ledInfo.Commport))
                            {
                                _lctData.FaultMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                            }
                            _lctData.FaultMonitorInfos[ledInfo.Commport].PowerAlarmSwitchCount++;
                        }
                        else if (monitorCardInfo.DeviceStatus == DeviceWorkStatus.OK &&
                            monitorFlag.IsPowerValid == DeviceWorkStatus.Unknown)
                        {
                            monitorFlag.IsPowerValid = DeviceWorkStatus.OK;
                        }
                    }
                    _lctData.ValidInfo.IsPowerValid = true;
                }
                if (isSmoke == false)
                {
                    monitorFlag.IsSmokeValid = DeviceWorkStatus.UnAvailable;
                }
                else if (monitorCardInfo.SmokeUInfo == null)
                {
                    //if (monitorFlag.IsSmokeValid == DeviceWorkStatus.OK)
                    //{
                    //    monitorFlag.IsSmokeValid = DeviceWorkStatus.Error;
                    //}
                }
                else if (monitorCardInfo.SmokeUInfo.IsUpdate)
                {
                    if (monitorCardInfo.SmokeUInfo.IsSmokeAlarm)
                    {
                        monitorFlag.IsSmokeValid = DeviceWorkStatus.Error;
                        if (!_lctData.AlarmMonitorInfos.ContainsKey(ledInfo.Commport))
                        {
                            _lctData.AlarmMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                        }
                        _lctData.AlarmMonitorInfos[ledInfo.Commport].SmokeAlarmCount++;
                    }
                    else if (monitorFlag.IsSmokeValid != DeviceWorkStatus.Error)
                    {
                        monitorFlag.IsSmokeValid = DeviceWorkStatus.OK;
                    }
                    _lctData.ValidInfo.IsSmokeValid = true;
                }

                if (isFlatCableStatus == false)
                {
                    monitorFlag.IsRowLineValid = DeviceWorkStatus.UnAvailable;
                }
                else if (monitorCardInfo.SocketCableUInfo == null)
                {
                    //    if (monitorFlag.IsRowLineValid == DeviceWorkStatus.OK)
                    //    {
                    //        monitorFlag.IsRowLineValid = DeviceWorkStatus.Error;
                    //    }
                }
                else if (monitorCardInfo.SocketCableUInfo.IsUpdate
                    && monitorCardInfo.SocketCableUInfo.SocketCableInfoCollection != null
                    && monitorCardInfo.SocketCableUInfo.SocketCableInfoCollection.Count > 0)
                {
                    foreach (SocketCableMonitorInfo socket in monitorCardInfo.SocketCableUInfo.SocketCableInfoCollection)
                    {
                        if (socket.SocketCableInfoDict == null || socket.SocketCableInfoDict.Count == 0)
                        {
                            monitorFlag.IsRowLineValid = DeviceWorkStatus.Error;
                            break;
                        }

                        foreach (List<SocketCableStatus> socketCables in socket.SocketCableInfoDict.Values)
                        {
                            foreach (SocketCableStatus socketcable in socketCables)
                            {
                                if (socketcable.IsCableOK == false)
                                {
                                    monitorFlag.IsRowLineValid = DeviceWorkStatus.Error;
                                    if (!_lctData.AlarmMonitorInfos.ContainsKey(ledInfo.Commport))
                                    {
                                        _lctData.AlarmMonitorInfos.Add(ledInfo.Commport, new MonitorErrData());
                                    }
                                    _lctData.AlarmMonitorInfos[ledInfo.Commport].SoketAlarmCount++;
                                }
                                else if (socketcable.IsCableOK == true &&
                                    monitorFlag.IsRowLineValid == DeviceWorkStatus.Unknown)
                                {
                                    monitorFlag.IsRowLineValid = DeviceWorkStatus.OK;
                                }
                            }
                        }
                        if (monitorFlag.IsRowLineValid == DeviceWorkStatus.Error)
                        {
                            break;
                        }
                    }
                    _lctData.ValidInfo.IsRowLineValid = true;
                }
            }
        }
        private void SetRegisterMonitor(MonitorDataFlag monitorFlag)
        {
            if (MonitorAllConfig.Instance().CareServiceConnectionStatus)
            {
                LedRegistationInfoResponse ledRegister = MonitorAllConfig.Instance().LedRegistationUiList.Find(a => a.SN == monitorFlag.SN);
                if (ledRegister == null || ledRegister.IsRegisted == false)
                {
                    monitorFlag.IsOnCareValid = DeviceWorkStatus.Unknown;
                }
                else
                {
                    monitorFlag.IsOnCareValid = DeviceWorkStatus.OK;
                }
            }
            else
            {
                monitorFlag.IsOnCareValid = DeviceWorkStatus.Error;
            }
        }

        #endregion
    }

    public class MonitorDataFlag
    {
        public MonitorDataFlag()
        {
            IsSenderDVIValid = DeviceWorkStatus.Unknown;
            IsSBStatusValid = DeviceWorkStatus.Unknown;
            IsTemperatureValid = DeviceWorkStatus.Unknown;
            IsMCStatusValid = DeviceWorkStatus.UnAvailable;
            IsHumidityValid = DeviceWorkStatus.UnAvailable;
            IsSmokeValid = DeviceWorkStatus.UnAvailable;
            IsFanValid = DeviceWorkStatus.UnAvailable;
            IsPowerValid = DeviceWorkStatus.UnAvailable;
            IsRowLineValid = DeviceWorkStatus.UnAvailable;
            IsGeneralStatusValid = DeviceWorkStatus.UnAvailable;
            IsOnCareValid = DeviceWorkStatus.UnAvailable;
        }

        public string SNName { get; set; }
        public DeviceWorkStatus IsSenderDVIValid { get; set; }
        public DeviceWorkStatus IsSBStatusValid { get; set; }
        public DeviceWorkStatus IsTemperatureValid { get; set; }
        public DeviceWorkStatus IsMCStatusValid { get; set; }
        public DeviceWorkStatus IsHumidityValid { get; set; }
        public DeviceWorkStatus IsSmokeValid { get; set; }
        public DeviceWorkStatus IsFanValid { get; set; }
        public DeviceWorkStatus IsPowerValid { get; set; }
        public DeviceWorkStatus IsRowLineValid { get; set; }
        public DeviceWorkStatus IsGeneralStatusValid { get; set; }
        public DeviceWorkStatus IsOnCareValid { get; set; }
        public string SN { get; set; }
    }
}
