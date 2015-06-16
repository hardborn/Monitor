using Log4NetLibrary;
using Nova.LCT.GigabitSystem.Common;
using Nova.Monitoring.Common;
using Nova.Monitoring.HardwareMonitorInterface;
using Nova.Net.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{

    public delegate void WriteEMailLogEventHandler(MailUserToken userToken,NotifyState notifyState);
    public class EMailSendMonitorErrMsg
    {
        public EMailSendMonitorErrMsg()
        {

        }
        private const string FILENAME = "Record";
        private const string SENDFILE = "Statistics";
        #region 临时对象
        private class MonitorErrData
        {
            public string SN;
            public ErrType ErrorType;
            public MonitorInfoResult MonitorResult;
            public DeviceRegionInfo DeviceRegInfo;
        }

        private enum ErrType
        {
            DVI,
            SenderCard,
            SBStatus,
            MonitorCard,
            Fan,
            CabinetDoor,
            Humidity,
            Power,
            Smoke,
            Soket,
            Temperature,
            Redundancy,
            Unknown
        }

        private enum MonitorInfoResult
        {
            Fault = 0,
            Alarm
        }
        #endregion

        #region 属性
        public FileLogService FLogService { get; set; }
        public bool IsNotifyConfigChanged { get; set; }
        public AllMonitorData ScreenMonitorData { get; set; }
        public EMailNotifyConfig NotifyConfig
        {
            set 
            {
                if (value == null)
                {
                    _emailConfig = new EMailNotifyConfig();
                }else
                {
                    _emailConfig = value;
                }
                if (_emailSender == null)
                {
                    bool bSucceed;
                    _emailSender = new EMailSendError(_emailConfig, out bSucceed);
                    _emailSender.WriteEMailLogEvent += OnSendEMailLogComplete;
                }
            }
        }
        public System.Collections.Hashtable EMailLangHsTable
        {
            get;
            set;
        }
        /// <summary>
        /// 告警配置
        /// </summary>
        public List<LedAlarmConfig> LedAlarmConfigs
        {
            set
            {
                _ledAlarmConfigs = value;
            }
        }
        private List<LedAlarmConfig> _ledAlarmConfigs = null;
        /// <summary>
        /// 屏信息
        /// </summary>
        public List<LedBasicInfo> LedInfoList
        {
            set
            {
                _ledInfoList = value;
            }
        }
        private List<LedBasicInfo> _ledInfoList = new List<LedBasicInfo>();
        #endregion

        #region 字段
        private List<MonitorErrData> _LastAllScreenMonitorErrData = new List<MonitorErrData>();
        private List<MonitorErrData> _newAllScreenMonitorErrData = new List<MonitorErrData>();

        private List<MonitorErrData> _LastAllScreenMonitorAlarmData = new List<MonitorErrData>();
        private List<MonitorErrData> _newAllScreenMonitorAlarmData = new List<MonitorErrData>();
        private int _currentSendCount = 0;
        private EMailNotifyConfig _emailConfig = null;
        private EMailSendError _emailSender = null;
        private List<string> _snList = new List<string>();
        private EMailSendModel _emailSendModel = EMailSendModel.None;
        private SystemRunRecordData _systemRunRecordData = null;
        private int _syetemRefreshIndex = 0;
        private int _sendEMailFile = 0;
        private DateTime _dateTime = new DateTime(DateTime.MaxValue.Ticks);
        private bool _isSend = false;
        #endregion

        public event WriteEMailLogEventHandler WriteSendEMailLogEvent;
        private void OnSendEMailLogComplete(MailUserToken userToken,NotifyState notifyState)
        {
            if (this.WriteSendEMailLogEvent != null)
            {
                this.WriteSendEMailLogEvent.Invoke(userToken, notifyState);
            }
        }

        #region 接口
        public void GetEMailMonitorErrMsg()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(StartThread));
            thread.Start();
        }
        /// <summary>
        /// 载入系统运行记录数据
        /// </summary>
        public void LoadLastRunStatusFile()
        {
            string fileName = ConstValue.EMAIL_NOTIFY_LOG_PATH + FILENAME + ".xml";
            if (File.Exists(fileName))
            {
                SystemRunStatuslFileAccessor.ReadConfigurationFile(fileName, out _systemRunRecordData);
            }
            if (_systemRunRecordData == null)
            {
                _systemRunRecordData = new SystemRunRecordData();
            }
            _syetemRefreshIndex = (int)_systemRunRecordData.SystemRefeshIndex;
        }

        public void Dispose()
        {
            if (_emailConfig != null)
            {
                _emailSender.WriteEMailLogEvent -= OnSendEMailLogComplete;
            }
        }
        #endregion
        private void StartThread()
        {
            if (_emailConfig != null && _emailConfig.EnableNotify)
            {
                FLogService.Info("EMailSendMonitorErrMsg" + "数据解析开始");
                GetAllScreenErrInfo();
                FLogService.Info("EMailSendMonitorErrMsg" + "数据解析完成");
                NotifyFaultAndAlarmEmail();
                FLogService.Info("EMailSendMonitorErrMsg" + "邮件发送完成");
            }
        }
        #region 获取监控数据
        private void GetAllScreenErrInfo()
        {
            _newAllScreenMonitorErrData.Clear();
            _newAllScreenMonitorAlarmData.Clear();
            _snList.Clear();
            _syetemRefreshIndex++;
            if(ScreenMonitorData==null)
            {
                _LastAllScreenMonitorAlarmData.Clear();
                _LastAllScreenMonitorErrData.Clear();
                return;
            }
            List<ScreenModnitorData> AllScreenMonitor = ScreenMonitorData.AllScreenMonitorCollection;
            for (int i = 0; i < AllScreenMonitor.Count; i++)
            {
                LedBasicInfo info =_ledInfoList.Find(a=>a.Sn==AllScreenMonitor[i].ScreenUDID);
                if(info==null)
                {
                    continue;
                }
                LedAlarmConfig alarmConfig = _ledAlarmConfigs.Find(a => a.SN == AllScreenMonitor[i].ScreenUDID);
                GetSenderErrData(AllScreenMonitor[i].ScreenUDID,AllScreenMonitor[i].SenderMonitorCollection);
                GetScanBoardErrData(AllScreenMonitor[i].ScreenUDID,alarmConfig, AllScreenMonitor[i].ScannerMonitorCollection);
                GetMonitorErrData(AllScreenMonitor[i].ScreenUDID, alarmConfig, AllScreenMonitor[i].MonitorCardInfoCollection);
                if(!_snList.Contains(AllScreenMonitor[i].ScreenUDID))
               {
                   _snList.Add(AllScreenMonitor[i].ScreenUDID);
               }
            }
        }
        private void GetMonitorErrData(string sn,LedAlarmConfig alarmConfig,List<MonitorCardMonitorInfo> monitorCardInfo)
        {
            FLogService.Info("EMailSendMonitorErrMsg" + "监控数据解析开始");

            if (monitorCardInfo == null || alarmConfig == null)
            {
                return;
            }
            MonitorErrData temp = null;
            for (int i = 0; i < monitorCardInfo.Count; i++)
            {
                //监控卡
                if(monitorCardInfo[i].DeviceStatus==DeviceWorkStatus.Error)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.MonitorCard;
                    temp.MonitorResult = MonitorInfoResult.Fault;
                    temp.DeviceRegInfo = (DeviceRegionInfo)monitorCardInfo[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorErrData.Add(temp);
                    AddSystemRunData(ErrType.MonitorCard, 1);
                    continue;
                }
                else if (monitorCardInfo[i].DeviceStatus == DeviceWorkStatus.Unknown)
                {
                    continue;
                }
                //湿度
                ParameterAlarmConfig info = alarmConfig.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Humidity);// && a.Disable == false);
                if (monitorCardInfo[i].HumidityUInfo!=null && monitorCardInfo[i].HumidityUInfo.IsUpdate && info != null && info.HighThreshold < monitorCardInfo[i].HumidityUInfo.Humidity)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.Humidity;
                    temp.MonitorResult = MonitorInfoResult.Alarm;
                    temp.DeviceRegInfo = (DeviceRegionInfo)monitorCardInfo[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorAlarmData.Add(temp);
                    AddSystemRunData(ErrType.Humidity, 1);
                }//烟雾
                if (monitorCardInfo[i].SmokeUInfo!=null && monitorCardInfo[i].SmokeUInfo.IsUpdate && monitorCardInfo[i].SmokeUInfo.IsSmokeAlarm)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.Smoke;
                    temp.MonitorResult = MonitorInfoResult.Alarm;
                    temp.DeviceRegInfo = (DeviceRegionInfo)monitorCardInfo[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorAlarmData.Add(temp);
                    AddSystemRunData(ErrType.Smoke, 1);
                }//箱门
                if (monitorCardInfo[i].CabinetDoorUInfo!=null && monitorCardInfo[i].CabinetDoorUInfo.IsUpdate && monitorCardInfo[i].CabinetDoorUInfo.IsDoorOpen)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.CabinetDoor;
                    temp.MonitorResult = MonitorInfoResult.Alarm;
                    temp.DeviceRegInfo = (DeviceRegionInfo)monitorCardInfo[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorAlarmData.Add(temp);
                    AddSystemRunData(ErrType.CabinetDoor, 1);
                }//风扇
                info = alarmConfig.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.FanSpeed);// && a.Disable == false);
                if (monitorCardInfo[i].FansUInfo != null && monitorCardInfo[i].FansUInfo.IsUpdate && info != null)
                {
                    foreach (var item in monitorCardInfo[i].FansUInfo.FansMonitorInfoCollection.Keys)
                    {
                        if(monitorCardInfo[i].FansUInfo.FansMonitorInfoCollection[item] < info.LowThreshold)
                        {
                            temp = new MonitorErrData();
                            temp.SN = sn;
                            temp.ErrorType = ErrType.Fan;
                            temp.MonitorResult = MonitorInfoResult.Alarm;
                            temp.DeviceRegInfo = (DeviceRegionInfo)monitorCardInfo[i].DeviceRegInfo.Clone();
                            _newAllScreenMonitorAlarmData.Add(temp);
                            AddSystemRunData(ErrType.Fan, 1);
                            break;
                        }
                    }
                }
                //电源
                info = alarmConfig.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Voltage);// && a.Disable == false);
                if (monitorCardInfo[i].PowerUInfo!=null && monitorCardInfo[i].PowerUInfo.IsUpdate && info != null)
                {
                    foreach (var item in monitorCardInfo[i].PowerUInfo.PowerMonitorInfoCollection.Keys)
                    {
                        //告警
                        if (monitorCardInfo[i].PowerUInfo.PowerMonitorInfoCollection[item] > info.HighThreshold)
                        {
                            temp = new MonitorErrData();
                            temp.SN = sn;
                            temp.ErrorType = ErrType.Power;
                            temp.MonitorResult = MonitorInfoResult.Alarm;
                            temp.DeviceRegInfo = (DeviceRegionInfo)monitorCardInfo[i].DeviceRegInfo.Clone();
                            _newAllScreenMonitorAlarmData.Add(temp);
                            AddSystemRunData(ErrType.Power, 1);
                            break;
                        }
                    }
                    foreach (var item in monitorCardInfo[i].PowerUInfo.PowerMonitorInfoCollection.Keys)
                    {
                        //故障
                        if (monitorCardInfo[i].PowerUInfo.PowerMonitorInfoCollection[item] < info.LowThreshold)
                        {
                            temp = new MonitorErrData();
                            temp.SN = sn;
                            temp.ErrorType = ErrType.Power;
                            temp.MonitorResult = MonitorInfoResult.Fault;
                            temp.DeviceRegInfo = (DeviceRegionInfo)monitorCardInfo[i].DeviceRegInfo.Clone();
                            _newAllScreenMonitorErrData.Add(temp);
                            AddSystemRunData(ErrType.Power, 1);
                            break;
                        }
                    }
                }
                //排线
                if (monitorCardInfo[i].SocketCableUInfo != null && monitorCardInfo[i].SocketCableUInfo.IsUpdate)
                {
                    GetSocketErrData(sn,monitorCardInfo[i].SocketCableUInfo.SocketCableInfoCollection, monitorCardInfo[i].DeviceRegInfo);
                }
            }
            FLogService.Info("EMailSendMonitorErrMsg" + "监控数据解析完成");

        }
        private void GetSocketErrData(string sn,List<SocketCableMonitorInfo> SocketCableInfo, DeviceRegionInfo deviceRegInfo)
        {
            if (SocketCableInfo==null)
            { return; }
            for (int i = 0; i < SocketCableInfo.Count; i++)
            {
                foreach (var item in SocketCableInfo[i].SocketCableInfoDict.Keys)
                {
                    for (int j = 0; j < SocketCableInfo[i].SocketCableInfoDict[item].Count; j++)
                    {
                        if(!SocketCableInfo[i].SocketCableInfoDict[item][j].IsCableOK)
                        {
                            MonitorErrData temp = new MonitorErrData();
                            temp.SN = sn;
                            temp.ErrorType = ErrType.Soket;
                            temp.MonitorResult = MonitorInfoResult.Fault;
                            temp.DeviceRegInfo = (DeviceRegionInfo)deviceRegInfo.Clone();
                            _newAllScreenMonitorErrData.Add(temp);
                            AddSystemRunData(ErrType.Soket, 1);
                            return;
                        }
                    }
                }
            }
        }
        private void GetScanBoardErrData(string sn,LedAlarmConfig alarmConfig,List<ScannerMonitorInfo> scannerMonitor)
        {
            FLogService.Info("EMailSendMonitorErrMsg" + "接收卡数据解析开始");

            if (scannerMonitor == null || alarmConfig == null)
            {
                return;
            }
            MonitorErrData temp = null;
            for (int i = 0; i < scannerMonitor.Count; i++)
            {

                if(scannerMonitor[i].DeviceStatus==DeviceWorkStatus.Unknown)
                {
                    continue;
                }
                //接收卡
                if (scannerMonitor[i].DeviceStatus==DeviceWorkStatus.Error)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.SBStatus;
                    temp.MonitorResult = MonitorInfoResult.Fault;
                    temp.DeviceRegInfo = (DeviceRegionInfo)scannerMonitor[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorErrData.Add(temp);
                    AddSystemRunData(ErrType.SBStatus, 1);
                }
                //板载温度
                ParameterAlarmConfig info = alarmConfig.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Temperature);// && a.Disable == false);
                if (info != null && info.HighThreshold < scannerMonitor[i].Temperature)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.Temperature;
                    temp.MonitorResult = MonitorInfoResult.Alarm;
                    temp.DeviceRegInfo = (DeviceRegionInfo)scannerMonitor[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorAlarmData.Add(temp);
                    AddSystemRunData(ErrType.Temperature, 1);
                }
            }
            FLogService.Info("EMailSendMonitorErrMsg" + "接收卡数据解析完成");

        }
        private void GetSenderErrData(string sn,List<SenderMonitorInfo> senderMonitor)
        {
            FLogService.Info("EMailSendMonitorErrMsg" + "发送卡数据解析开始");

            if (senderMonitor==null)
            {
                return ;
            }
            MonitorErrData temp = null;
            for (int i = 0; i < senderMonitor.Count; i++)
            {
                temp = new MonitorErrData();
                if (senderMonitor[i].DeviceStatus == DeviceWorkStatus.Error)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.SenderCard;
                    temp.MonitorResult = MonitorInfoResult.Fault;
                    temp.DeviceRegInfo = (DeviceRegionInfo)senderMonitor[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorErrData.Add(temp);
                    AddSystemRunData(ErrType.SenderCard, 1);
                }
                //DVI
                temp = new MonitorErrData();
                if(!senderMonitor[i].IsDviConnected)
                {
                    temp = new MonitorErrData();
                    temp.SN = sn;
                    temp.ErrorType = ErrType.DVI;
                    temp.MonitorResult = MonitorInfoResult.Fault;
                    temp.DeviceRegInfo = (DeviceRegionInfo)senderMonitor[i].DeviceRegInfo.Clone();
                    _newAllScreenMonitorErrData.Add(temp);
                    AddSystemRunData(ErrType.DVI, 1);
                }
                for (int j = 0; j < senderMonitor[i].ReduPortIndexCollection.Count; j++)
                {
                    //冗余
                    if (senderMonitor[i].ReduPortIndexCollection[j].IsReduState)
                    {
                        temp = new MonitorErrData();
                        temp.SN = sn;
                        temp.ErrorType = ErrType.Redundancy;
                        temp.MonitorResult = MonitorInfoResult.Alarm;
                        temp.DeviceRegInfo = (DeviceRegionInfo)senderMonitor[i].DeviceRegInfo.Clone();
                        _newAllScreenMonitorAlarmData.Add(temp);
                        AddSystemRunData(ErrType.Redundancy, 1);
                        break;
                    }
                }
            }
            FLogService.Info("EMailSendMonitorErrMsg" + "发送卡数据解析完成");

        }
        #endregion

        #region 邮件发送相关
        private bool CheckMonitorDataChanged(out bool isNoErrorAlarm, out bool IsRecover)
        {
            bool isChanged = false;
            isNoErrorAlarm = true;
            IsRecover = false;
            if (_newAllScreenMonitorErrData.Count <= 0 && _newAllScreenMonitorAlarmData.Count <= 0)
            {
                if (_LastAllScreenMonitorAlarmData.Count > 0 || _LastAllScreenMonitorErrData.Count > 0)
                {
                    _LastAllScreenMonitorAlarmData.Clear();
                    _LastAllScreenMonitorErrData.Clear();
                    IsRecover = true;
                }
                return isChanged;
            }
            isNoErrorAlarm = false;
            #region 故障和告警是否改变
            if (_newAllScreenMonitorErrData.Count != _LastAllScreenMonitorErrData.Count)
            {
                isChanged = true;
            }
            for (int i = 0; i < _snList.Count; i++)
            {
                foreach (ErrType item in Enum.GetValues(typeof(ErrType)))
                {
                    List<MonitorErrData> temp = null;
                    List<MonitorErrData> last = null;
                    if (item == ErrType.DVI
                        || item == ErrType.MonitorCard
                        || item == ErrType.Power
                        || item == ErrType.SBStatus
                        || item == ErrType.Soket
                        || item == ErrType.SenderCard
                        )
                    {
                        temp = _newAllScreenMonitorErrData.FindAll(a => a.ErrorType == item && a.SN == _snList[i]);
                        last = _LastAllScreenMonitorErrData.FindAll(a => a.ErrorType == item && a.SN == _snList[i]);
                        if(CompareDataIsEqual(temp, last))
                        {
                            isChanged = true;
                        }
                    }
                    else if(item == ErrType.Smoke
                        || item == ErrType.Humidity
                        || item == ErrType.Power
                        || item == ErrType.CabinetDoor
                        || item == ErrType.Redundancy
                        || item == ErrType.Fan)
                    {
                        temp = _newAllScreenMonitorAlarmData.FindAll(a => a.ErrorType == item && a.SN == _snList[i]);
                        last = _LastAllScreenMonitorAlarmData.FindAll(a => a.ErrorType == item && a.SN == _snList[i]);
                        if (CompareDataIsEqual(temp, last))
                        {
                            isChanged = true;
                        }
                    }
                }
            }
            #endregion

            #region 保存当次的故障数据

            _LastAllScreenMonitorAlarmData.Clear();
            _LastAllScreenMonitorErrData.Clear();
            for (int i = 0; i < _newAllScreenMonitorAlarmData.Count; i++)
            {
                _LastAllScreenMonitorAlarmData.Add(_newAllScreenMonitorAlarmData[i]);
            }
            for (int i = 0; i < _newAllScreenMonitorErrData.Count; i++)
            {
                _LastAllScreenMonitorErrData.Add(_newAllScreenMonitorErrData[i]);
            }
            #endregion

            return isChanged;
        }
        private bool CompareDataIsEqual(List<MonitorErrData> temp,List<MonitorErrData> last)
        {
            if(temp==null && last==null)
            {
                return false;
            }
            else if ((temp == null && last != null) || (temp != null && last == null))
            {
                return true;
            }
            else if(temp.Count!=last.Count)
            {
                return true;
            }
            for (int i = 0; i < temp.Count; i++)
            {
                if(temp[i].DeviceRegInfo.SenderIndex!=last[i].DeviceRegInfo.SenderIndex
                  || temp[i].DeviceRegInfo.PortIndex!=last[i].DeviceRegInfo.PortIndex
                  || temp[i].DeviceRegInfo.CommPort != last[i].DeviceRegInfo.CommPort
                  || temp[i].DeviceRegInfo.ConnectIndex != last[i].DeviceRegInfo.ConnectIndex
                  || temp[i].DeviceRegInfo.CardIndex != last[i].DeviceRegInfo.CardIndex
                  || temp[i].MonitorResult != last[i].MonitorResult)
                {
                    return true;
                }
            }
            return false;
        }
        private string MakeNotifyString(string time,string titleMsg,
                                     int curNotifyIndex)
        {

             #region 故障
            string errorStr = "";

            for (int i = 0; i < _snList.Count; i++)
            {
                LedBasicInfo info = _ledInfoList.Find(a => a.Sn == _snList[i]);
                if(info!=null)
                {
                    if (string.IsNullOrEmpty(info.AliaName))
                    {
                        errorStr += "\t" + GetStr("ScreenName", "屏名称：") + _snList[i] + "\r\n";
                    }
                    else
                    {
                        errorStr += "\t" + info.AliaName + "\r\n";
                    }
                }
                else
                {
                    errorStr += "\t" + GetStr("ScreenName", "屏名称：") + _snList[i] + "\r\n";
                }
                string faultStr = "";
                string alarmStr = "";
                foreach (ErrType item in Enum.GetValues(typeof(ErrType)))
                {
                    List<MonitorErrData> temp = null;
                    if (item == ErrType.DVI
                        || item == ErrType.MonitorCard
                        || item == ErrType.Power
                        || item == ErrType.SenderCard
                        || item == ErrType.SBStatus
                        || item == ErrType.Soket
                        )
                    {
                        temp = _newAllScreenMonitorErrData.FindAll(a => a.ErrorType == item && a.SN == _snList[i]);
                        if (temp == null || temp.Count <= 0)
                        {
                            continue;
                        }
                        GetOneErrOrAlarmStr(item, temp, ref faultStr);
                        faultStr += "\r\n\t\t\t";
                    }
                    else if (item == ErrType.Smoke
                        || item == ErrType.Humidity
                        || item == ErrType.Power
                        || item == ErrType.Temperature
                        || item == ErrType.CabinetDoor
                        || item == ErrType.Fan)
                    {
                        temp = _newAllScreenMonitorAlarmData.FindAll(a => a.ErrorType == item && a.SN == _snList[i]);
                        if (temp == null || temp.Count <= 0)
                        {
                            continue;
                        }
                        GetOneErrOrAlarmStr(item, temp, ref alarmStr);
                        alarmStr += "\r\n\t\t\t";
                    }
                }
                if (_newAllScreenMonitorErrData.Count > 0)
                {
                    errorStr +="\t\t" +  GetStr("ErrName", "故障名称：") + faultStr + "\r\n";
                }
                if (_newAllScreenMonitorAlarmData.Count > 0)
                {
                    if (!string.IsNullOrEmpty(alarmStr))
                    {
                        errorStr += "\t\t" + GetStr("AlarmName", "告警名称：") + alarmStr + "\r\n";
                    }
                }
            }
            #region 冗余状态信息

            string redundantStateMsg = "";
            SenderRedundancyInfo senderInfo = null;
            foreach (string key in ScreenMonitorData.RedundantStateType.Keys)
            {
                foreach (int sIndex in ScreenMonitorData.RedundantStateType[key].Keys)
                {
                    foreach (int var in ScreenMonitorData.RedundantStateType[key][sIndex].RedundantStateTypeList.Keys)
                    {
                        if (ScreenMonitorData.RedundantStateType[key][sIndex].RedundantStateTypeList[var] != RedundantStateType.Error)
                        {
                            continue;
                        }
                        if (ScreenMonitorData.TempRedundancyDict.ContainsKey(key) && ScreenMonitorData.TempRedundancyDict[key] != null )
                        {
                            for (int j = 0; j < ScreenMonitorData.TempRedundancyDict[key].Count; j++)
                            {
                                senderInfo = ScreenMonitorData.TempRedundancyDict[key][j];
                                if (senderInfo.SlaveSenderIndex == sIndex && senderInfo.SlavePortIndex == var)
                                {
                                    break;
                                }
                                senderInfo = null;
                            }
                        }
                        else
                        {
                            continue;
                        }
                        if (senderInfo == null)
                        {
                            continue;
                        }
                        redundantStateMsg += "\t\t\t"+key + " - " + GetStr("SenderIndex", "发送卡") + (sIndex + 1) + "-";
                        redundantStateMsg += GetStr("portIndex", "网口") + (var + 1) + GetStr("RedundantState", "冗余") + ";";
                        redundantStateMsg += " - " + GetStr("MasterSenderPort", "主网口:发送卡") + (senderInfo.MasterSenderIndex + 1);
                        redundantStateMsg += GetStr("portIndex", "网口") + (senderInfo.MasterPortIndex + 1) + "\r\n";
                    }
                }
            }
            #endregion
             #endregion

            #region


            string body = "";
            if (_newAllScreenMonitorAlarmData.Count > 0 || _newAllScreenMonitorErrData.Count > 0)
            {
                body += errorStr + "\r\r";
            }

            if(!string.IsNullOrEmpty(redundantStateMsg))
            {
                body += "\t\t"+ GetStr("AlarmName", "告警名称：") + GetStr("RedundantState", "冗余")+"\r\n" + redundantStateMsg;
            }
            string msg = GetStr("EmailGreetings", "您好：");
            msg += "\r\n";
            msg = msg + "\t" + time + "   "  +titleMsg + "!\r\n";
            body = msg + body;
            //加上警示
            msg = "\r\n\r\n\t" + GetStr("EmailAlarmDealWith", "请及时到现场处理故障或告警!");
            body += msg;
            string timesNotify = "(" + GetStr("CurNotifyTimes", "当前故障通知次数:") + (curNotifyIndex + 1).ToString()
                                 + "," + GetStr("MaxNotifyTimes", "最多通知次数:") + "3" + ")";
            body += timesNotify;
            #endregion
            return body;
        }

        private void GetOneErrOrAlarmStr(ErrType item, List<MonitorErrData> temp, ref string errStr)
        {
            errStr += GetErrTypeStr(item, temp[0].MonitorResult) + "  " + GetStr("TotalErr", "总数：") + " " + temp.Count;
            if (item != ErrType.DVI && item != ErrType.SenderCard)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    errStr += "\r\n\t\t\t\t";
                    errStr += GetErrPositionStr(temp[i].DeviceRegInfo.SenderIndex,
                                            temp[i].DeviceRegInfo.PortIndex,
                                            temp[i].DeviceRegInfo.ConnectIndex,
                                            temp[i].DeviceRegInfo.ScanBoardRows,
                                            temp[i].DeviceRegInfo.ScanBoardCols,
                                            temp[i].DeviceRegInfo.IsComplex);
                }
            }
        }

        //private void AddOneScreen
        private string GetErrTypeStr(ErrType item, MonitorInfoResult monitorResult)
        {
            string msg = "";
            if (item == ErrType.DVI)
            {
                msg = GetStr("DVIFault", "DVI故障");
            }
            if (item == ErrType.SenderCard)
            {
                msg = GetStr("SenderCardFault", "发送卡故障");
            }
            else if(item==ErrType.CabinetDoor)
            {
                msg = GetStr("CabinetDoorAlarm", "箱门开启");
            }
            else if (item == ErrType.Fan)
            {
                msg = GetStr("FanAlarm", "风扇告警");
            }
            else if (item == ErrType.Humidity)
            {
                msg = GetStr("HumidityAlarm", "湿度告警");
            }
            else if (item == ErrType.MonitorCard)
            {
                msg = GetStr("MonitorCardFault", "监控卡故障");
            }
            else if (item == ErrType.Power)
            {
                if(monitorResult == MonitorInfoResult.Fault)
                {
                    msg = GetStr("PowerFault", "电源故障");
                }
                else
                {
                    msg = GetStr("PowerAlarm", "电源告警");
                }
            }
            else if (item == ErrType.Redundancy)
            {
                msg = GetStr("RedundancyAlarm", "冗余告警");
            }
            else if (item == ErrType.SBStatus)
            {
                msg = GetStr("SBStatusFault", "接收卡故障");
            }
            else if (item == ErrType.Smoke)
            {
                msg = GetStr("SmokeAlarm", "烟雾告警");
            }
            else if (item == ErrType.Soket)
            {
                msg = GetStr("SoketFault", "排线故障");
            }
            else if (item == ErrType.Temperature)
            {
                msg = GetStr("TemperatureAlarm", "温度告警");
            }
            return msg;
        }
        private string GetErrPositionStr(int senderIndex, int portIndex, int connectIndex, int rowIndex, int colIndex, bool isComplex)
        {
            string msg = GetStr("SenderIndex", "发送卡") + "-" + (senderIndex + 1).ToString() +
                GetStr("portIndex", "网口") + "-" + (portIndex + 1).ToString() +
                GetStr("connectIndex", "接收卡") + "-" + (connectIndex + 1).ToString();
            if (!isComplex)
            {
                msg += GetStr("rowIndex", "行号") + "-" + (rowIndex + 1).ToString() +
                GetStr("colIndex", "列号") + "-" + (colIndex + 1).ToString();
            }
            return msg;
        }
        private string GetStr(string strKey,string deufltStr)
        {
            string msg = "";
            if (!CustomTransform.GetLanguageString(strKey, EMailLangHsTable, out msg))
            {
                msg = deufltStr;
            }
            return msg;
        }
        private void NotifyFaultAndAlarmEmail()
        {
            if (_emailConfig.EnableNotify)
            {
                if (IsNotifyConfigChanged)
                {
                    _currentSendCount = 0;
                    IsNotifyConfigChanged = false;
                    if (_emailSender != null)
                    {
                        _emailSender.WriteEMailLogEvent -= OnSendEMailLogComplete;
                        _emailSender.Dispose();
                        bool bSucceed;
                        _emailSender = new EMailSendError(_emailConfig, out bSucceed);
                        _emailSender.WriteEMailLogEvent += OnSendEMailLogComplete;
                    }
                }
                bool isNoErrorAlarm = false;
                bool isRecover = false;
                bool isChanged = CheckMonitorDataChanged(out isNoErrorAlarm, out isRecover);
                if (_currentSendCount > 0 && isNoErrorAlarm && isRecover)
                {
                    if (_emailConfig.EnableRecoverNotify)
                    {
                        FLogService.Info("EMailSendMonitorErrMsg" + "发送系统恢复邮件");
                        NotifyFaultSytstemRecover();
                    }
                }
                if (isChanged && !isNoErrorAlarm) //如果监控信息改变同时不是变成没有故障或告警，那么重置发送次数
                {
                    _currentSendCount = 0;
                }

                 if ((isChanged && !isNoErrorAlarm) ||
                    (!isChanged && !isNoErrorAlarm && _currentSendCount < 3))
                {

                    string titleMsg = GetStr("MonitorErrorNotification", "监控故障通知");
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    titleMsg = _emailConfig.EmailSendSource + " " + titleMsg;

                    string msg = MakeNotifyString(time,titleMsg,
                                                  _currentSendCount);
                    GetSystemRefreshData();
                    DateTime curTime = DateTime.Now;
                    NotifyContent content = GetEmailNotifyContent(titleMsg, msg, time, curTime);

                    string fileName = curTime.Date.ToString("yyyy-MM-dd");
                    fileName = ConstValue.EMAIL_NOTIFY_LOG_PATH + fileName + ".xml";
                    if (_emailSender==null)
                    {
                        return;
                    }
                    _emailSender.SendErrorNotify(content, fileName);
                    _currentSendCount++;
                    FLogService.Info("EMailSendMonitorErrMsg" + "发送故障和告警邮件数据");
                     
                }
            }
            if (_emailConfig.TimeEMailNotify && _emailConfig.EnableNotify)
            {
                FLogService.Info("EMailSendMonitorErrMsg" + "发送系统运行数据报告");
                SetIsSendEMail();
            }
        }
        private void NotifyFaultSytstemRecover()
        {
            string titleMsg = GetStr("MonitorSytstemRecoverNotification", "监控系统恢复通知");
            titleMsg = _emailConfig.EmailSendSource + " " + titleMsg;

            string tiems = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string msg = GetStr("EmailGreetings", "您好:");
            msg += "\r\n\t" + titleMsg + "\r\n\t";
            msg += GetStr("MonitorMessageNotification", "系统已恢复正常!");
            DateTime curTime = DateTime.Now;
            NotifyContent content = GetEmailNotifyContent(titleMsg, msg, tiems, curTime);

            string fileName = curTime.Date.ToString("yyyy-MM-dd");
            fileName = ConstValue.EMAIL_NOTIFY_LOG_PATH + fileName + ".xml";
            _emailSender.SendErrorNotify(content, fileName);
        }
        private NotifyContent GetEmailNotifyContent(string title,string msg, string displayName,DateTime time)
        {
            NotifyContent content = new NotifyContent();
            Guid guid = Guid.NewGuid();

            content.MsgContent = msg;
            content.MsgDate = time.ToString();
            content.MsgID = guid.ToString();

            content.MsgTitle = title;
            content.NotifyState = NotifyState.Sended;
            string receiver = "";
            for (int i = 0; i < _emailConfig.ReceiveInfoList.Count; i++)
            {
                receiver += _emailConfig.ReceiveInfoList[i].Name + ":"
                    + _emailConfig.ReceiveInfoList[i].EmailAddr + "\r\n";
            }
            content.Receiver = receiver;
            content.SendEMailTime = displayName;
            return content;
        }
        #endregion

        #region 系统刷新信息记录

        /// <summary>
        /// 获得刷新后数据
        /// </summary>
        private void GetSystemRefreshData()
        {
            if (_emailConfig.TimeEMailNotify)
            {
                if (_systemRunRecordData == null)
                {
                    _systemRunRecordData = new SystemRunRecordData();
                }
                _systemRunRecordData.SystemRefeshIndex = (uint)_syetemRefreshIndex;

                string fileName = ConstValue.EMAIL_NOTIFY_LOG_PATH + FILENAME + ".xml";
                SystemRunStatuslFileAccessor.SaveAsConfigurationFile(_systemRunRecordData, fileName);
            }
            else
            {
                _syetemRefreshIndex = 0;
            }
        }

        /// <summary>
        /// 更新记录数据
        /// </summary>
        /// <param name="monitor"></param>
        private void AddSystemRunData(ErrType item,int count)
        {
            if (_systemRunRecordData==null)
            {
                _systemRunRecordData = new SystemRunRecordData();
            }

            if (item == ErrType.DVI)
            {
                _systemRunRecordData.SenderErrCount += (uint)count;
            }
            else if (item == ErrType.SenderCard)
            {
                _systemRunRecordData.SenderErrCount += (uint)count;
            }
            else if (item == ErrType.CabinetDoor)
            {
                _systemRunRecordData.CabinetDoorMonitorIndex += (uint)count;
            }
            else if (item == ErrType.Fan)
            {
                _systemRunRecordData.FanAlarmSwitchCount += (uint)count;
            }
            else if (item == ErrType.Humidity)
            {
                _systemRunRecordData.HumidityAlarmCount += (uint)count;
            }
            else if (item == ErrType.MonitorCard)
            {
                _systemRunRecordData.MCStatusErrCount += (uint)count;
            }
            else if (item == ErrType.Power)
            {
                _systemRunRecordData.PowerAlarmSwitchCount += (uint)count;
            }
            else if (item == ErrType.Redundancy)
            {
                _systemRunRecordData.RedundantStateCount += (uint)count;
            }
            else if (item == ErrType.SBStatus)
            {
                _systemRunRecordData.ReceiveCardErroeIndex += (uint)count;
            }
            else if (item == ErrType.Smoke)
            {
                _systemRunRecordData.SmokeMonitorErrorIndex += (uint)count;
            }
            else if (item == ErrType.Soket)
            {
                _systemRunRecordData.CabinetMonitorIndex += (uint)count;
            }
            else if (item == ErrType.Temperature)
            {
                _systemRunRecordData.TemperatureMonitorErrorIndex += (uint)count;
            }
        }
        /// <summary>
        /// 创建Excel文件
        /// </summary>
        /// <returns></returns>
        private bool CreateExcelFile()
        {
            #region 获取数据
            DataTable dateTable = new DataTable("Datas");
            dateTable.Columns.Add(GetStr("DataName","名称"), Type.GetType("System.String"));
            dateTable.Columns.Add(GetStr("DataTime", "次数"), Type.GetType("System.Int32"));
            dateTable.Rows.Add(new object[] { GetStr("SystemRefeshIndex", "系统刷新"), _systemRunRecordData.SystemRefeshIndex });
            //dateTable.Rows.Add(new object[] { GetStr("ScrenName", "屏名称"), _systemRunRecordData.ScreenName });
            dateTable.Rows.Add(new object[] { GetStr("SenderErrCount", "发送卡故障"), _systemRunRecordData.SenderErrCount });
            dateTable.Rows.Add(new object[] { GetStr("EmailScannerError", "接收卡故障"), _systemRunRecordData.ReceiveCardErroeIndex });
            dateTable.Rows.Add(new object[] { GetStr("EmailMCCardError", "监控卡故障"), _systemRunRecordData.MCStatusErrCount });
            dateTable.Rows.Add(new object[] { GetStr("EmailTemperatureError", "温度告警"), _systemRunRecordData.TemperatureMonitorErrorIndex });
            dateTable.Rows.Add(new object[] { GetStr("EmailSmokeError", "烟雾告警"), _systemRunRecordData.SmokeMonitorErrorIndex });
            dateTable.Rows.Add(new object[] { GetStr("EmailHumidityError", "湿度告警"), _systemRunRecordData.HumidityAlarmCount });
            dateTable.Rows.Add(new object[] { GetStr("EmailFlatCableError", "箱体告警"), _systemRunRecordData.CabinetMonitorIndex });
            dateTable.Rows.Add(new object[] { GetStr("EmailPowerError", "电源告警"), _systemRunRecordData.PowerAlarmSwitchCount });
            dateTable.Rows.Add(new object[] { GetStr("EmailCableDoorError", "箱门告警"), _systemRunRecordData.CabinetDoorMonitorIndex });
            dateTable.Rows.Add(new object[] { GetStr("EmailFanError", "风扇告警"), _systemRunRecordData.FanAlarmSwitchCount });
            dateTable.Rows.Add(new object[] { GetStr("RedundantStateCount", "冗余总数"), _systemRunRecordData.RedundantStateCount });

            #endregion
            string msg = ConstValue.EMAIL_NOTIFY_LOG_PATH + SENDFILE + ".xls";
            if (!SystemRunStatuslFileAccessor.CreateStatisticsExcel(dateTable, msg))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获得发送邮件模式
        /// </summary>
        private void SetIsSendEMail()
        {
            DateTime time = DateTime.Now;
            if (_emailConfig.SendMailModel == EMailSendModel.Everyday)
            {
                if (CompareIsOverTime(_emailConfig.SendTimer))
                {
                    SendEMailStatisticsFile();
                }
            }
            else if (_emailConfig.SendMailModel == EMailSendModel.Weekly)
            {
                if (time.DayOfWeek == _emailConfig.SendMailWeek)
                {
                    if (CompareIsOverTime(_emailConfig.SendTimer))
                    {
                        SendEMailStatisticsFile();
                    }
                }
            }
            else if (_emailConfig.SendMailModel == EMailSendModel.Mouthly)
            {
                DateTime dateTime = _emailConfig.SendTimer.Date;
                if (time.Day == dateTime.Day)
                {

                    if (CompareIsOverTime(dateTime))
                    {
                        SendEMailStatisticsFile();
                    }
                }
                else if (DayIsOverDay(dateTime.Day))
                {
                    if (CompareIsOverTime(dateTime))
                    {
                        SendEMailStatisticsFile();
                    }
                }
            }
            else
            {
                return;
            }
            _emailSendModel = _emailConfig.SendMailModel;
        }
        private bool DayIsOverDay(int day)
        {
            DateTime time = DateTime.Now;
            int dayIndex = DateTime.DaysInMonth(time.Year, time.Month);
            if (dayIndex < day)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 发送系统刷新数据邮件
        /// </summary>
        /// <returns></returns>
        private bool SendEMailStatisticsFile()
        {
            if (!_isSend)
            {
                string fileName = ConstValue.EMAIL_NOTIFY_LOG_PATH + SENDFILE + ".xls";

                if (!CreateExcelFile())
                {
                    return false;
                }
                if (!File.Exists(fileName))
                {
                    return false;
                }
                DateTime curTime = DateTime.Now;
                string titleMsg = GetStr("SystemRefreshRecord", "系统运行数据");
                titleMsg = _emailConfig.EmailSendSource + " " + titleMsg;
                NotifyContent content = GetEmailNotifyContent(titleMsg, "", curTime.ToString("yyyy-MM-dd HH:mm:ss"), curTime);
                content.AttachmentFileNameList.Add(fileName);
                if (!_emailSender.SendErrorNotify(content, fileName))
                {
                    return false;
                }
                _sendEMailFile++;
                _dateTime = DateTime.Now;
                ClearSystemRefreshData();
                _isSend = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 比较是否到时间点
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private bool CompareIsOverTime(DateTime time)
        {
            if (DateTime.Now.Hour > time.Hour)
            {
                return true;
            }
            else if (DateTime.Now.Hour < time.Hour)
            {
                return false;
            }
            else
            {
                if (DateTime.Now.Minute >= time.Minute)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 使能发送邮件
        /// </summary>
        private void SetEnableSendEMail()
        {
            if (_sendEMailFile < 0)
            {
                return;
            }
            if (_dateTime == DateTime.MaxValue)
            {
                return;
            }
            if (_dateTime.Date < DateTime.Now.Date
                || (int)_dateTime.Month < (int)DateTime.Now.Month)
            {
                //_sendEMailFile = 0;
                _isSend = false;
            }
        }
        /// <summary>
        ///清除系统记录数据
        /// </summary>
        private void ClearSystemRefreshData()
        {
            if (_systemRunRecordData != null)
            {
                _systemRunRecordData = null;
                _syetemRefreshIndex = 0;
                _systemRunRecordData = new SystemRunRecordData();
            }
        }
        /// <summary>
        /// 重置发送时间
        /// </summary>
        private void ResetSendTime()
        {
            if (_emailConfig.SendMailModel != _emailSendModel && _emailSendModel != EMailSendModel.None)
            {
                //_sendEMailFile = 0;
                _isSend = false;
            }
        }
        #endregion
    }

    public class EMailSendError :  IDisposable
    {
        private class NotifySendInfo
        {
            public NotifyContent NtfContent;
            public string NotifyFile;
        }

        private EMailNotifyConfig _emailConfig = null;
        private MailSender _mailSender = null;
        private object _sendObj = new object();
        private object _logFileMetux = new object();

        private string _errorTag = "";
        private string _normalTag = "";
        private bool _bSending = false;

        public event WriteEMailLogEventHandler WriteEMailLogEvent;


        private List<NotifySendInfo> _notifyContextList = new List<NotifySendInfo>();
        public EMailSendError(EMailNotifyConfig emailConfig, out bool bSucceed)
        {
            _emailConfig = (EMailNotifyConfig)emailConfig.Clone();
            _mailSender = new MailSender(emailConfig.SmtpServer,
                emailConfig.Port,
                emailConfig.EmailAddr,
                emailConfig.Password,
                out bSucceed);
            _mailSender.IsEnableSsl = emailConfig.IsEnableSsl;

            if (bSucceed)
            {
                _mailSender.EmailSendCompleteEvent += new SendCompletedEventHandler(OnMailComplete);
            }
        }

        #region INotifyMonitorError 成员

        public bool SendErrorNotify(NotifyContent notifyContent, string logFileName)
        {
            if (_bSending)
            {
                NotifySendInfo newSend = new NotifySendInfo();
                newSend.NtfContent = notifyContent;
                newSend.NotifyFile = logFileName;

                _notifyContextList.Add(newSend);
                return true;
            }
            else
            {
                return SendOneNotify(notifyContent, logFileName);
            }

        }

        private void OnMailComplete(object sender, AsyncCompletedEventArgs e)
        {
            //更改日志中的状态结果
            MailUserToken userToken = (MailUserToken)e.UserState;
            NotifyState notifyState=NotifyState.Failed;
            if(e.Error==null)
            {
                notifyState = NotifyState.Succeed;
            }
            if (WriteEMailLogEvent!=null)
            {
                WriteEMailLogEvent.Invoke(userToken, notifyState);
            }
           //修改日志
            _bSending = false;
            RemoveAttachment();
            SendOneNotifyInList();
        }

        private bool SendOneNotifyInList()
        {
            if (_notifyContextList.Count > 0)
            {
                NotifySendInfo notifySendInf = _notifyContextList[0];
                _notifyContextList.RemoveAt(0);
                NotifyContent notifyContent = notifySendInf.NtfContent;
                string logFileName = notifySendInf.NotifyFile;

                return SendErrorNotify(notifyContent, logFileName);
            }
            return false;
        }
        private bool SendOneNotify(NotifyContent notifyContent, string logFileName)
        {
            lock (_sendObj)
            {
                //发送邮件
                SmtpException smtpEx = new SmtpException();

                MailUserToken userToken = new MailUserToken(notifyContent.MsgID, logFileName, notifyContent);
                List<string> receiver = new List<string>();
                for (int i = 0; i < _emailConfig.ReceiveInfoList.Count; i++)
                {
                    receiver.Add(_emailConfig.ReceiveInfoList[i].EmailAddr);
                }
                if (receiver.Count <= 0)
                {
                    return false;
                }

                for (int i = 0; i < notifyContent.AttachmentFileNameList.Count; i++)
                {
                    _mailSender.Attachments(notifyContent.AttachmentFileNameList[i]);
                }
                bool isSendSucceed = true;
                if (!_mailSender.SetMailMessageContext(notifyContent.MsgContent, notifyContent.MsgTitle, false))
                {
                    isSendSucceed = false;
                }
                else
                {
                    if (!_mailSender.SendAsync(receiver, System.Net.Mail.MailPriority.Normal,
                                               ref smtpEx, userToken))
                    {
                        isSendSucceed = false;
                    }
                    else
                    {
                        _bSending = true;
                    }
                }
                if (_emailConfig.EnableJournal)
                {
                    #region 写入日志
                    
                    #endregion
                }
                return isSendSucceed;
            }
        }
        private bool RemoveAttachment()
        {
            if (!_mailSender.ClearAttachments())
            {
                return false;
            }
            return true;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (_mailSender != null)
            {
                _mailSender.EmailSendCompleteEvent -= new SendCompletedEventHandler(OnMailComplete);
                _mailSender.Dispose();
            }
        }

        #endregion
    }
}
