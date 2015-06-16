using GalaSoft.MvvmLight.Threading;
using Log4NetLibrary;
using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.LCT.GigabitSystem.HWPointDetect;
using Nova.Monitoring.ColudSupport;
using Nova.Monitoring.Common;
using Nova.Monitoring.DAL;
using Nova.Monitoring.DataDispatcher;
using Nova.Monitoring.Engine;
using Nova.Monitoring.HardwareMonitorInterface;
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.Utilities;
using Nova.Xml.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    /// <summary>
    /// UI与Service间数据交互集合
    /// </summary>
    public class MonitorAllConfig : IDisposable
    {
        private static MonitorAllConfig _instance = null;
        private static object objLock = new object();
        private static object _lockMonitordata = new object();
        private static object _objLockReadData = new object();
        FileLogService _fLogService = new FileLogService(typeof(MonitorAllConfig));
        private EMailSendMonitorErrMsg _sendMonitorErrMsg = null;
        private bool _isTimeOut = false;
        private System.Threading.AutoResetEvent _autoEventIsTimeOut = new System.Threading.AutoResetEvent(false);
        public static MonitorAllConfig Instance()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MonitorAllConfig();
                    }
                }
            }
            return _instance;
        }
        private MonitorAllConfig()
        {
            ALLScreenName = "ALLScreen";
            _dataSourceInfos = new List<DataSourceInfo>();
            _dataDispatcherInfos = new List<DataDispatcherInfo>();
            _ledInfoList = new List<LedBasicInfo>();
            _ledRegistationUiList = new List<LedRegistationInfoResponse>();
            NovaCareServerAddress = "172.16.80.166";
            DataServicePort = 8888;
            //AppDataConfig.CurrentMAC = SystemHelper.GetMACAddress();//Nova.Monitoring.SystemMessageManager.SystemInfo.GetMACAddress();
            _sendMonitorErrMsg = new EMailSendMonitorErrMsg();
            _sendMonitorErrMsg.FLogService = _fLogService;
            _sendMonitorErrMsg.WriteSendEMailLogEvent += OnSendEMailLogComplete;
            _sendMonitorErrMsg.LoadLastRunStatusFile();
        }

        #region 界面超时机制
        /// <summary>
        /// 调用超时方法(默认超时时间4000ms)
        /// </summary>
        /// <param name="funcName">功能名称</param>
        private void UITimerMode(string funcName)
        {
            UITimerMode(4000, funcName);
        }
        /// <summary>
        /// 调用超时方法(带超时时间ms)
        /// </summary>
        /// <param name="timerOut"></param>
        /// <param name="funcName">功能名称</param>
        private void UITimerMode(int timerOut, string funcName)
        {
            _isTimeOut = true;
            Action action = new Action(() =>
            {
                UITimeOutExec(timerOut, funcName);
            });
            action.BeginInvoke(null, null);
        }
        /// <summary>
        /// 内部使用的，禁止调用(需在此增加方法超时后的处理方法)
        /// </summary>
        /// <param name="timerOut"></param>
        /// <param name="funcName"></param>
        private void UITimeOutExec(int timerOut, string funcName)
        {
            _autoEventIsTimeOut.Reset();
            _autoEventIsTimeOut.WaitOne(timerOut, false);
            if (_isTimeOut)
            {
                switch (funcName)
                {
                    case "RefreshAllFunctionCardInfos":
                        if (GetFunctionCardPowerEvent != null)
                        {
                            if (_peripheralCfgDICDic == null)
                            {
                                _peripheralCfgDICDic = new SerializableDictionary<string, string>();
                            }
                            else
                            {
                                _peripheralCfgDICDic.Clear();
                            }
                            GetFunctionCardPowerEvent(null, null);
                        }
                        _fLogService.Error("FunctionCard Read Timeout!");
                        break;
                    case "RefreshAllOpticalProbeInfos":
                        if (GetLightProbeEvent != null)
                        {
                            if (AllOpticalProbeInfo == null || AllOpticalProbeInfo.UseablePeripheralList == null)
                            {
                                AllOpticalProbeInfo = new PeripheralsLocateInfo();
                            }
                            else
                            {
                                AllOpticalProbeInfo.UseablePeripheralList.Clear();
                            }
                            GetLightProbeEvent(null, null);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void UITimerOutCancel()
        {
            _isTimeOut = false;
            _autoEventIsTimeOut.Set();
        }

        #endregion

        #region 对外事件
        public event Action<string, string> OpenUIHandlerEvent;
        /// <summary>
        /// 硬件配置变更通知UI
        /// </summary>
        public event EventHandler LedMonitoringConfigChangedEvent;
        /// <summary>
        /// 屏体变更通知UI
        /// </summary>
        public event EventHandler LedScreenChangedEvent;
        /// <summary>
        /// 周期变更通知UI
        /// </summary>
        public event EventHandler LedAcquisitionConfigEvent;
        /// <summary>
        /// 屏体变更引起注册变更通知UI
        /// </summary>
        public event Action<bool> LedRegistationInfoEvent;
        /// <summary>
        /// 光探头变更通知UI
        /// </summary>
        public event EventHandler GetLightProbeEvent;
        /// <summary>
        /// 监控数据变更通知UI
        /// </summary>
        public event EventHandler MonitorDataChangedEvent;
        /// <summary>
        /// 多功能卡变更通知UI
        /// </summary>
        public event EventHandler GetFunctionCardPowerEvent;
        /// <summary>
        /// 告警配置变更通知UI
        /// </summary>
        public event EventHandler AlarmConfigSynchronizedEvent;
        /// <summary>
        /// 策略配置变更通知UI
        /// </summary>
        public event EventHandler StrategySynchronizedEvent;
        /// <summary>
        /// 亮度配置变更通知UI
        /// </summary>
        public event Action<SmartLightConfigInfo> BrightnessChangedEvent;
        /// <summary>
        /// 亮度配置变更通知UI
        /// </summary>
        public event Action<string> MonitorUIStatusChangedEvent;
        /// <summary>
        /// Care连接状态
        /// </summary>
        public event Action<bool> CareServiceConnectionStatusChangedEvent;
        /// <summary>
        /// 通过串口号获取SN
        /// </summary>
        public event Action<string> FromCOMFindSNEvent;
        /// <summary>
        /// 亮度刷新通知
        /// </summary>
        public event EventHandler<BrightnessValueRefreshEventArgs> BrightnessValueRefreshed;


        #endregion

        #region 属性数据
        public System.Collections.Hashtable EMailLangHsTable
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件日志
        /// </summary>
        public List<NotifyContent> NotifyContentList
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件配置信息
        /// </summary>
        public EMailNotifyConfig NotifyConfig
        {
            get;
            set;
        }
        /// <summary>
        /// 全部屏统一配置时的名称
        /// </summary>
        public string ALLScreenName { get; set; }
        /// <summary>
        /// 屏统一配置时的别名称
        /// </summary>
        public string ScreenName
        {
            get
            {
                return _screenName;
            }
            set
            {
                _screenName = value;
            }
        }
        private string _screenName = string.Empty;
        /// <summary>
        /// 刷新周期
        /// </summary>
        public LedAcquisitionConfig AcquisitionConfig
        {
            get
            {
                return _acquisitionConfig;
            }
            set
            {
                _acquisitionConfig = value;
                if (LedAcquisitionConfigEvent != null)
                {
                    LedAcquisitionConfigEvent(null, null);
                }
            }
        }
        private LedAcquisitionConfig _acquisitionConfig = null;

        /// <summary>
        /// 硬件配置
        /// </summary>
        public List<LedMonitoringConfig> LedMonitorConfigs
        {
            get
            {
                if (_ledMonitorConfigs == null)
                {
                    return new List<LedMonitoringConfig>();
                }
                return _ledMonitorConfigs.ToList();
            }
            set
            {
                _ledMonitorConfigs = value;
            }
        }
        private List<LedMonitoringConfig> _ledMonitorConfigs = null;

        /// <summary>
        /// 告警配置
        /// </summary>
        public List<LedAlarmConfig> LedAlarmConfigs
        {
            get
            {
                if (_ledAlarmConfigs == null)
                {
                    return new List<LedAlarmConfig>();
                }
                return _ledAlarmConfigs.ToList();
            }
            set
            {
                _ledAlarmConfigs = value;
            }
        }
        private List<LedAlarmConfig> _ledAlarmConfigs = null;

        /// <summary>
        /// 策略集体
        /// </summary>
        public Dictionary<string, List<Strategy>> StrategyConfigDic
        {
            get
            {
                if (_strategyConfigDic == null)
                {
                    return new Dictionary<string, List<Strategy>>();
                }
                return _strategyConfigDic;
            }
            set
            {
                _strategyConfigDic = value;
            }
        }
        private Dictionary<string, List<Strategy>> _strategyConfigDic;

        /// <summary>
        /// 所有多功能卡信息
        /// </summary>
        public SerializableDictionary<string, string> PeripheralCfgDICDic
        {
            get
            {
                if (_peripheralCfgDICDic == null)
                {
                    return new SerializableDictionary<string, string>();
                }
                return _peripheralCfgDICDic;
            }
            set { _peripheralCfgDICDic = value; }
        }
        private SerializableDictionary<string, string> _peripheralCfgDICDic;

        /// <summary>
        /// 所有的光探头明细
        /// </summary>
        public PeripheralsLocateInfo AllOpticalProbeInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public List<DataSourceInfo> DataSourceInfos
        {
            get
            {
                if (_dataSourceInfos == null)
                {
                    return new List<DataSourceInfo>();
                }
                return _dataSourceInfos.ToList();
            }
            set
            {
                _dataSourceInfos = value;
            }
        }
        private List<DataSourceInfo> _dataSourceInfos = null;
        /// <summary>
        /// 数据分发器
        /// </summary>
        public List<DataDispatcherInfo> DataDispatcherInfos
        {
            get
            {
                if (_dataDispatcherInfos == null)
                {
                    return new List<DataDispatcherInfo>();
                }
                return _dataDispatcherInfos.ToList();
            }
            set
            {
                _dataDispatcherInfos = value;
            }
        }
        private List<DataDispatcherInfo> _dataDispatcherInfos = null;
        /// <summary>
        /// 屏信息
        /// </summary>
        public List<LedBasicInfo> LedInfoList
        {
            get
            {
                if (_ledInfoList == null)
                {
                    return new List<LedBasicInfo>();
                }
                return _ledInfoList.ToList();
            }
            set
            {
                _ledInfoList = value;
            }
        }
        private List<LedBasicInfo> _ledInfoList = null;
        /// <summary>
        /// 屏注册信息
        /// </summary>
        public List<LedRegistationInfoResponse> LedRegistationUiList
        {
            get
            {
                if (_ledRegistationUiList == null)
                {
                    return new List<LedRegistationInfoResponse>();
                }
                return _ledRegistationUiList.ToList();
            }
            set
            {
                _ledRegistationUiList = value;
            }
        }
        private List<LedRegistationInfoResponse> _ledRegistationUiList = null;
        /// <summary>
        /// 亮度调节以及光探头信息
        /// </summary>
        public List<SmartLightConfigInfo> BrightnessConfigList
        {
            get { return _brightnessConfigList; }
            set { _brightnessConfigList = value; }
        }
        private List<SmartLightConfigInfo> _brightnessConfigList = null;
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string NovaCareServerAddress { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int DataServicePort { get; set; }
        /// <summary>
        /// 串口的屏数据
        /// </summary>
        public SerializableDictionary<string, List<ILEDDisplayInfo>> AllCommPortLedDisplayDic
        {
            get
            {
                if (_allCommPortLedDisplayDic == null)
                {
                    return new SerializableDictionary<string, List<ILEDDisplayInfo>>();
                }
                SerializableDictionary<string, List<ILEDDisplayInfo>> allLedDic = new SerializableDictionary<string, List<ILEDDisplayInfo>>();
                foreach (KeyValuePair<string, List<ILEDDisplayInfo>> keyvalue in _allCommPortLedDisplayDic)
                {
                    allLedDic.Add(keyvalue.Key, keyvalue.Value.ToList());
                }
                return allLedDic;
            }
            set
            {
                _allCommPortLedDisplayDic = value;
            }
        }
        private SerializableDictionary<string, List<ILEDDisplayInfo>> _allCommPortLedDisplayDic = null;
        /// <summary>
        /// 用户全局配置
        /// </summary>
        public UserConfig UserConfigInfo
        {
            get
            {
                return _userConfigInfo;
            }
            set
            {
                _userConfigInfo = value;
            }
        }
        private UserConfig _userConfigInfo = null;
        public bool CareServiceConnectionStatus { get; set; }
        public AllMonitorData ScreenMonitorData { get; set; }
        public string CurrentScreenName { get; set; }
        public bool IsGetLedInfo { get; set; }
        public string MonitorDisplayVersion { get; set; }
        #endregion

        #region 字段
        private static object _lockResgiter = new object();
        private SerializableDictionary<FunctionCardRoadInfo, string> _peripheralCfgDICDicDb = new SerializableDictionary<FunctionCardRoadInfo, string>();
        private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();
        private LCTMainMonitorData _lctMainData = null;
        #endregion

        #region 对外接口
        /// <summary>
        /// 初次启动服务
        /// </summary>
        public void StartServices()
        {
            _fLogService.Debug("开始初始化监控...");
            DispatcherHelper.Initialize();
            DataEngine.Dispatcher = DispatcherHelper.UIDispatcher;
            RestFulClient.Instance.Initialize(NovaCareServerAddress);
            StartCoreService();
            LoadDispatchers();
            LoadDataSources();
            _fLogService.Debug("完成初始化监控...");
        }
        public void GetVersions(string version)
        {
            AppDataConfig.CurVersion = version;
        }
        public float GetTempValueChanged(TemperatureType tempDisplayType, float value)
        {
            if (tempDisplayType == TemperatureType.Fahrenheit)
            {
                return value * 1.8f + 32;
            }
            else if (tempDisplayType == TemperatureType.Celsius)
            {
                return (value - 32) / 1.8f;
            }
            return value;
        }
        public void RefreshAllOpticalProbeInfos()
        {
            _fLogService.Info("开始读取光探头信息...");
            ClientDispatcher.Instance.RefreshOpticalProbeInfo();
            UITimerMode(5000, "RefreshAllOpticalProbeInfos");
        }
        public void RefreshAllFunctionCardInfos()
        {
            _fLogService.Info("开始读取多功能卡信息...");
            ClientDispatcher.Instance.RefreshFunctionCardInfo();
            UITimerMode("RefreshAllFunctionCardInfos");
        }
        public void RefreshBrightnessConfigInfos()
        {
            _fLogService.Info("开始读取亮度配置信息...");
            ClientDispatcher.Instance.RefreshSmartLightConfigInfo();
        }
        public void RefreshMonitoringData()
        {
            _fLogService.Info("开始手动读取刷新数据...");
            _stopwatch.Reset();
            _stopwatch.Start();
            ClientDispatcher.Instance.RefreshMonitoringData();
            _fLogService.Info("完成手动读取刷新数据");
        }
        public void LedRegistationInfoEventMethod(bool isNotice)
        {
            _fLogService.Debug("注册信息变更，界面变更");
            if (LedRegistationInfoEvent != null)
            {
                LedRegistationInfoEvent(isNotice);
            }
        }
        public void FromCOMtoSN(string param)
        {
            _fLogService.Info("开始获取亮度的屏SN...");
            ClientDispatcher.Instance.FromCOMtoSN(param);
            _fLogService.Info("完成获取亮度的屏SN");
        }

        public void WriteLogToFile(string msg)
        {
            WriteLogToFile(msg, false);
        }
        public void WriteLogToFile(string msg, bool isError)
        {
            if (isError)
            {
                _fLogService.Error(msg);
            }
            else
            {
                _fLogService.Debug(msg);
            }
        }
        #endregion

        #region 对外数据接口
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="ledregister"></param>
        /// <returns></returns>
        public ServerResponseCode SaveResgiterTo(string account, List<LedRegistationInfo> ledregisters, bool isReregistering)
        {
            _fLogService.Info("开始注册...");
            List<LedRegistationInfo> ledregisterList = new List<LedRegistationInfo>();
            ServerResponseCode serverCode = DataDispatcher.ClientDispatcher.Instance.RegistTo
                (account, NovaCareServerAddress, ledregisters, isReregistering);
            _fLogService.Info("完成注册,返回结果..." + serverCode.ToString());
            return serverCode;
        }
        public bool UpdateLedAcquisitionConfig(LedAcquisitionConfig acquisitionConfig)
        {
            _fLogService.Info("更新周期...");
            bool res = DataDispatcher.ClientDispatcher.Instance.UpdateLedAcquisitionConfig(acquisitionConfig);
            if (res)
            {
                AcquisitionConfig.DataPeriod = acquisitionConfig.DataPeriod;
                AcquisitionConfig.IsAutoRefresh = acquisitionConfig.IsAutoRefresh;
                AcquisitionConfig.RetryCount = acquisitionConfig.RetryCount;

                _fLogService.Info("更新周期成功完成...");
                return true;
            }
            else
            {
                _fLogService.Info("更新周期失败完成...");
                return false;
            }
        }
        public bool UpdateLedMonitoringConfig(string sn, LedMonitoringConfig ledMonitoringConfig)
        {
            _fLogService.Info("开始更新硬件配置...");
            bool isResult = true;
            if (IsAllScreen(sn))
            {
                _ledMonitorConfigs.Clear();
                foreach (LedBasicInfo ledinfo in LedInfoList)
                {
                    if (IsAllScreen(ledinfo.Sn))
                    {
                        _ledMonitorConfigs.Add((LedMonitoringConfig)ledMonitoringConfig.Clone());
                        continue;
                    }
                    LedMonitoringConfig ledmonitor = (LedMonitoringConfig)ledMonitoringConfig.Clone();
                    ledmonitor.SN = ledinfo.Sn;
                    isResult = isResult && DataDispatcher.ClientDispatcher.Instance.UpdateLedMonitoringConfig(ledinfo.Sn, ledmonitor);
                    _ledMonitorConfigs.Add(ledmonitor);
                }
            }
            else
            {
                isResult = DataDispatcher.ClientDispatcher.Instance.UpdateLedMonitoringConfig(sn, (LedMonitoringConfig)ledMonitoringConfig.Clone());
                _ledMonitorConfigs.Remove(_ledMonitorConfigs.Find(a => a.SN == sn));
                _ledMonitorConfigs.Add((LedMonitoringConfig)ledMonitoringConfig.Clone());
            }
            _fLogService.Info("完成更新硬件配置...");
            if (LedMonitoringConfigChangedEvent != null)
                LedMonitoringConfigChangedEvent(null, null);
            return isResult;
        }
        public bool UpdateLedAlarmConfig(string sn, LedAlarmConfig alarmConfig)
        {
            _fLogService.Info("开始更新告警配置...");
            bool isResult = true;
            if (IsAllScreen(sn))
            {
                _ledAlarmConfigs.Clear();
                foreach (LedBasicInfo ledinfo in LedInfoList)
                {
                    if (IsAllScreen(ledinfo.Sn))
                    {
                        _ledAlarmConfigs.Add((LedAlarmConfig)alarmConfig.Clone());
                        continue;
                    }
                    LedAlarmConfig ledAlarm = (LedAlarmConfig)alarmConfig.Clone();
                    ledAlarm.SN = ledinfo.Sn;
                    isResult = isResult && DataDispatcher.ClientDispatcher.Instance.UpdateLedAlarmConfig(ledinfo.Sn, ledAlarm);
                    _ledAlarmConfigs.Add(ledAlarm);
                }
            }
            else
            {
                isResult = DataDispatcher.ClientDispatcher.Instance.UpdateLedAlarmConfig(sn, (LedAlarmConfig)alarmConfig.Clone());
                _ledAlarmConfigs.Remove(_ledAlarmConfigs.Find(a => a.SN == sn));
                _ledAlarmConfigs.Add((LedAlarmConfig)alarmConfig.Clone());
            }
            _fLogService.Info("完成更新告警配置...");
            return isResult;
        }
        public bool SaveFunctionCardPowerConfig(SerializableDictionary<FunctionCardRoadInfo, string> funcCardPower)
        {
            SerializableDictionary<string, string> funcCardPowerStr = new SerializableDictionary<string, string>();
            foreach (KeyValuePair<FunctionCardRoadInfo, string> keyvalue in funcCardPower)
            {
                funcCardPowerStr.Add(
                    CommandTextParser.GetJsonSerialization<FunctionCardRoadInfo>(keyvalue.Key),
                    keyvalue.Value);
            }
            _fLogService.Info("开始保存多功能卡配置...");
            if (DataDispatcher.ClientDispatcher.Instance.SaveFunctionCardPowerConfig(
                CommandTextParser.GetJsonSerialization<SerializableDictionary<string, string>>(funcCardPowerStr)))
            {
                _peripheralCfgDICDicDb = funcCardPower;
                _peripheralCfgDICDic = funcCardPowerStr;
                _fLogService.Info("成功保存多功能卡配置...");
                return true;
            }
            else
            {
                _fLogService.Info("失败保存多功能卡配置...");
                return false;
            }
        }
        public bool SaveStrategyCofig(StrategyTable straTable, string sn)
        {
            foreach (var item in _strategyConfigDic)
            {
                if (sn == item.Key) continue;
                straTable.StrategyList.AddRange(item.Value);
            }
            _fLogService.Info("开始保存策略配置...");
            bool res = DataDispatcher.ClientDispatcher.Instance.UpdateStrategy(straTable);
            _fLogService.Info("完成保存策略配置...");
            if (res)
            {
                _strategyConfigDic = new Dictionary<string, List<Strategy>>();// straTable.StrategyList;
                foreach (var item in straTable.StrategyList)
                {
                    if (!_strategyConfigDic.ContainsKey(item.SN))
                        _strategyConfigDic.Add(item.SN, new List<Strategy>());
                    _strategyConfigDic[item.SN].Add(item);
                }
            }
            return res;
        }
        public int SaveBrigthnessAutoConfig(int autoBrightPeriod, int readLuxCnt, bool isSmart, bool isBrightGradual)
        {
            if (_brightnessConfigList == null || _brightnessConfigList.Count == 0)
            {
                return 1;
            }
            bool isSucess = true;
            for (int i = 0; i < _brightnessConfigList.Count; i++)
            {
                SmartLightConfigInfo smartLight = (SmartLightConfigInfo)_brightnessConfigList[i].Clone();

                if (smartLight.DispaySoftWareConfig != null)
                {
                    smartLight.DispaySoftWareConfig.AutoAdjustPeriod = autoBrightPeriod;
                    smartLight.DispaySoftWareConfig.AutoBrightReadLuxCnt = (byte)readLuxCnt;
                    smartLight.DispaySoftWareConfig.IsSmartEnable = isSmart;
                    smartLight.DispaySoftWareConfig.IsBrightGradualEnable = isBrightGradual;
                }
                if (smartLight.DisplayHardcareConfig != null)
                {
                    smartLight.DisplayHardcareConfig.AutoAdjustPeriod = autoBrightPeriod;
                    smartLight.DisplayHardcareConfig.AutoBrightReadLuxCnt = (byte)readLuxCnt;
                    smartLight.DisplayHardcareConfig.IsSmartEnable = isSmart;
                    smartLight.DisplayHardcareConfig.IsBrightGradualEnable = isBrightGradual;
                }
                isSucess = isSucess && SaveBrightnessConfig(smartLight);
                CustomTransform.Delay(1000, 5);
            }
            if (isSucess)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
        public bool SaveBrightnessConfig(SmartLightConfigInfo config)
        {
            return SaveBrightnessConfig(config, true);
        }
        public bool SaveBrightnessConfig(SmartLightConfigInfo config, bool isSave)
        {
            bool res = false;
            if (isSave == false)
            {
                _fLogService.Info("开始测试保存亮度的配置...");
                DataDispatcher.ClientDispatcher.Instance.SetSmartLightConfigInfo(
                    CommandTextParser.GetJsonSerialization<SmartLightConfigInfo>(config));
                _fLogService.Info("完成测试保存亮度的配置...");
                return true;
            }

            List<SmartLightConfigInfo> configList = new List<SmartLightConfigInfo>();
            if (_brightnessConfigList != null)
            {
                foreach (var item in _brightnessConfigList)
                {
                    configList.Add((SmartLightConfigInfo)item.Clone());
                }
            }
            if (configList == null)
            {
                configList = new List<SmartLightConfigInfo>();
                configList.Add(config);
            }
            else
            {
                int index = configList.FindIndex(a => a.ScreenSN == config.ScreenSN);
                if (index < 0)
                    configList.Add(config);
                else configList[index] = config;
            }
            _fLogService.Info("开始保存亮度的配置...");
            res = DataDispatcher.ClientDispatcher.Instance.UpdateSmartBrightEasyConfig(config.ScreenSN, config);
            if (res)
            {
                _brightnessConfigList = configList;
            }
            _fLogService.Info("完成保存亮度的配置...");
            return res;
        }
        public bool SaveUserConfig()
        {
            _fLogService.Info("开始保存用户的配置...");
            bool res = DataDispatcher.ClientDispatcher.Instance.SaveUserConfig(_userConfigInfo);
            _fLogService.Info("完成保存用户的配置...");
            return res;
        }
        /// <summary>
        /// 保存邮件配置参数
        /// </summary>
        /// <returns></returns>
        public bool SaveEMailNotifyConfig(EMailNotifyConfig notifyConfig)
        {
            //_fLogService.Info("开始保存用户的配置...");
            bool res = MonitorDataAccessor.Instance().UpdateEmailCfg(
              CommandTextParser.GetJsonSerialization<EMailNotifyConfig>(notifyConfig),
              SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            //_fLogService.Info("完成保存用户的配置...");

            if (res && _sendMonitorErrMsg != null)
            {
                _sendMonitorErrMsg.IsNotifyConfigChanged = true;
            }
            if (res)
            {
                NotifyConfig = notifyConfig;
            }
            return res;
        }

        public void SendMonitorDataToLCT(LCTMainMonitorData lctData)
        {
            _lctMainData = lctData;
            string strText = string.Empty;
            if (lctData != null)
            {
                strText = CommandTextParser.GetJsonSerialization<LCTMainMonitorData>(lctData);
            }
            Action action = new Action(() =>
            {
                ClientDispatcher.Instance.SendCommand(new Command()
                {
                    Code = CommandCode.MonitorDataToLCT,
                    Target = TargetType.ToClient,
                    CommandText = strText
                });
            });
            action.BeginInvoke(null, null);
        }

        public void ReReadScreenConfig(int code, string msg)
        {
            Action action = new Action(() =>
            {
                ClientDispatcher.Instance.SendCommand(new Command()
                {
                    Code = CommandCode.RefreshLedScreenConfigInfo,
                    CommandText = msg
                });
            });
            action.BeginInvoke(null, null);
        }

        /// <summary>
        /// 获取一天的邮件日志
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public bool GetSendEMailLog(string times)
        {
            if (NotifyContentList == null)
            {
                NotifyContentList = new List<NotifyContent>();
            }
            NotifyContentList.Clear();
            List<string> temp = MonitorDataAccessor.Instance().GetSendEmailOprLog(times);
            if (temp.Count <= 0)
            {
                return false;
            }
            for (int i = 0; i < temp.Count; i++)
            {
                NotifyContentList.Add(CommandTextParser.GetDeJsonSerialization<NotifyContent>(temp[i]));
            }
            return true;
        }

        public bool DeleteOneSendEMailLog(string times)
        {
            return MonitorDataAccessor.Instance().DeleteOneSendEmailOprLog(times);
        }
        #endregion

        #region 收到的数据命令

        private void CommandReceivedBeginInvoke(Command commad)
        {
            try
            {
                switch (commad.Code)
                {
                    case CommandCode.GetBrightnessConfig:
                        _fLogService.Info("收到亮度读取命令：" + commad.Code);
                        DataDispatcher.ClientDispatcher.Instance.SetBrightnessConfigToLCT(CommandTextParser.GetJsonSerialization<List<SmartLightConfigInfo>>(_brightnessConfigList));
                        break;
                    #region 收到的界面命令
                    case CommandCode.ViewMonitoringUI:
                        _fLogService.Info("开始收到打开主界面的命令..." + commad.Code);
                        if (OpenUIHandlerEvent != null)
                        {
                            string[] str = commad.CommandText.Split('|');
                            if (str.Length < 2)
                            {
                                OpenUIHandlerEvent(str[0], string.Empty);
                            }
                            else
                            {
                                string[] para = str[1].Split('●');
                                string sn = null;
                                if (_ledInfoList == null)
                                {
                                    WriteLogToFile("Catch:在亮度打开时，监控的屏为空怎么可能?? Why???", true);
                                    break;
                                }
                                var ledBInfo = _ledInfoList.Find(a => a.Commport == para[0] && a.LedIndexOfCom == Convert.ToInt32(para[1]));
                                if (ledBInfo == null)
                                {
                                    break;
                                }
                                sn = ledBInfo.Sn;
                                if (str[0] == "OpenSmartBrightness")
                                {
                                    if (para[2] == "3")
                                    {
                                        if (para[3] == "0")
                                        {
                                            var cfg = _brightnessConfigList.Find(a => a.ScreenSN == sn);
                                            if (cfg != null)
                                            {
                                                cfg.AdjustType = BrightAdjustType.Smart;
                                                SaveBrightnessConfig(cfg);
                                            }
                                        }
                                        else
                                        {
                                            OpenUIHandlerEvent(str[0], para[0] + "●" + para[1] + "●" + sn);
                                        }
                                    }
                                    else if (para[2] == "0")
                                    {
                                        //手动
                                        var cfg = _brightnessConfigList.Find(a => a.ScreenSN == sn);
                                        if (cfg != null)
                                        {
                                            if (cfg.DispaySoftWareConfig != null && cfg.DispaySoftWareConfig.OneDayConfigList != null)
                                            {
                                                foreach (var smartCfg in cfg.DispaySoftWareConfig.OneDayConfigList)
                                                {
                                                    if (smartCfg != null) smartCfg.IsConfigEnable = false;
                                                }
                                            }
                                            if (cfg.DisplayHardcareConfig != null && cfg.DisplayHardcareConfig.OneDayConfigList != null)
                                            {
                                                foreach (var smartCfg in cfg.DisplayHardcareConfig.OneDayConfigList)
                                                {
                                                    if (smartCfg != null) smartCfg.IsConfigEnable = false;
                                                }
                                            }
                                            cfg.AdjustType = BrightAdjustType.Mannual;
                                            SaveBrightnessConfig(cfg);
                                        }
                                    }
                                    else if (para[2] == "1" || para[2] == "2")
                                    {
                                        if (para[3] != null)
                                        {
                                            var smartLightConfigInfo = CommandTextParser.GetDeJsonSerialization<SmartLightConfigInfo>(para[3]);
                                            var cfg = _brightnessConfigList.Find(a => a.ScreenSN == sn);
                                            if (cfg != null)
                                            {
                                                if (cfg.DisplayHardcareConfig != null)
                                                {
                                                    smartLightConfigInfo.DisplayHardcareConfig.AutoAdjustPeriod = cfg.DisplayHardcareConfig.AutoAdjustPeriod;
                                                    smartLightConfigInfo.DisplayHardcareConfig.AutoBrightReadLuxCnt = cfg.DisplayHardcareConfig.AutoBrightReadLuxCnt;
                                                }
                                                if (cfg.DispaySoftWareConfig != null)
                                                {
                                                    smartLightConfigInfo.DispaySoftWareConfig.AutoAdjustPeriod = cfg.DispaySoftWareConfig.AutoAdjustPeriod;
                                                    smartLightConfigInfo.DispaySoftWareConfig.AutoBrightReadLuxCnt = cfg.DispaySoftWareConfig.AutoBrightReadLuxCnt;
                                                }
                                            }
                                            smartLightConfigInfo.ScreenSN = sn;
                                            SaveBrightnessConfig(smartLightConfigInfo);
                                        }
                                    }
                                }
                            }
                        }
                        _fLogService.Info("完成收到打开主界面的命令...");
                        break;
                    #endregion
                    #region 收到的点检命令
                    case CommandCode.SetSpotInspectionConfigInfo:
                        _fLogService.Info("收到配置点检参数命令..." + commad.Code);
                        if (_ledInfoList == null || _ledInfoList.Count == 0)
                        {
                            _fLogService.Error("显示屏信息为空...");
                            _fLogService.Info(string.Format("@CommandLog@Command Execute finished(error)####Code={0},id={1},source={2},Target={3},commadText={4},Description={5}\n{Errorinfo={6}", commad.Code.ToString(), commad.Id, commad.Source, commad.Target.ToString(), commad.CommandText, commad.Description, "LedInfo is null"));
                            return;
                        }
                        List<string> detectParamStrList = CommandTextParser.GetDeJsonSerialization<List<string>>(commad.CommandText);
                        string[] ledInfoStrList;
                        int indexOfCom;
                        DetectConfigParams detectParam;
                        LedBasicInfo ledInfo;
                        foreach (var item in detectParamStrList)
                        {
                            ledInfoStrList = item.Split('|');
                            if (ledInfoStrList.Length < 3) continue;
                            if (!Int32.TryParse(ledInfoStrList[1], out indexOfCom)) continue;
                            if (ledInfoStrList[2] == "") continue;
                            detectParam = CommandTextParser.GetDeJsonSerialization<DetectConfigParams>(ledInfoStrList[2]);
                            if (detectParam == null) continue;
                            ledInfo = _ledInfoList.Find(a => a.Commport == ledInfoStrList[0] && a.LedIndexOfCom == indexOfCom);
                            if (ledInfo == null) continue;
                            bool res = DataDispatcher.ClientDispatcher.Instance.UpdatePointDeteConfig(ledInfo.Sn, detectParam);
                            _fLogService.Info("显示屏:" + ledInfo.Sn + ",点检参数保存完成，结果：" + res.ToString());
                        }
                        break;
                    #endregion
                    case CommandCode.LCTGetMonitorData:
                        SendMonitorDataToLCT(_lctMainData);
                        break;
                    case CommandCode.LCTGetScreenInfo:
                        ClientDispatcher.Instance.SendCommand(new Command()
                        {
                            Code = CommandCode.ScreenInfoToLCT,
                            CommandText = CommandTextParser.GetJsonSerialization<List<LedBasicInfo>>(_ledInfoList),
                            Target = TargetType.ToClient
                        });
                        break;
                    default:
                        _fLogService.Debug("收到未处理的命令..." + commad.Code);
                        break;
                }
                _fLogService.Info(string.Format("@CommandLog@Command Execute finished(normal)####Code={0},id={1},source={2},Target={3},commadText={4},Description={5}", commad.Code.ToString(), commad.Id, commad.Source, commad.Target.ToString(), commad.CommandText, commad.Description));
            }
            catch (Exception ex)
            {
                _fLogService.Info(string.Format("@CommandLog@Command Execute finished(error)####Code={0},id={1},source={2},Target={3},commadText={4},Description={5}\n{Errorinfo={6}", commad.Code.ToString(), commad.Id, commad.Source, commad.Target.ToString(), commad.CommandText, commad.Description, ex.ToString()));
                WriteLogToFile("ExistCatch：收到命令处理时出错：" + ex.ToString(), true);
            }
        }
        private void MonitoringDataReceivedBeginInvoke(string key, object data)
        {
            try
            {
                switch (key)
                {
                    case "UpdateOpticalProbeInfo":
                        _fLogService.Info("收到底层上来的数据：" + key);
                        UITimerOutCancel();
                        AllOpticalProbeInfo = CommandTextParser.GetDeJsonSerialization<PeripheralsLocateInfo>(data as string);
                        if (GetLightProbeEvent != null)
                        {
                            GetLightProbeEvent(null, null);
                        }
                        break;
                    case "M3_MonitoringData":
                        _fLogService.Debug("收到底层上来的数据：" + key);
                        SetMonitorData(data as string);
                        break;
                    case "UpdateFunctionCardInfo":
                        _fLogService.Info("收到底层上来的数据：" + key);
                        SetFunctionToDic(data as string);
                        break;
                    case "ReadSmartLightHWconfigInfo":
                        _fLogService.Info("收到底层上来的数据：" + key);
                        SetBrightnessConfigInfos(data as string);
                        break;
                    case "M3_StateData":
                        if (MonitorUIStatusChangedEvent != null)
                        {
                            MonitorUIStatusChangedEvent(data as string);
                        }
                        break;
                    case "FromCOMFindSN":
                        if (FromCOMFindSNEvent != null)
                        {
                            Delegate[] delegAry = FromCOMFindSNEvent.GetInvocationList();
                            foreach (Action<string> deleg in delegAry)
                            {
                                deleg.BeginInvoke(data as string, null, null);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLogToFile("ExistCatch：MonitoringDataReceived Error:" + ex.ToString(), true);
            }
        }
        #endregion

        #region 内部方法
        private string StartCoreService()
        {
            _fLogService.Info("开始启动Care服务...");
            DataEngine.StartService(DataServicePort);
            DataEngine.InitializeRuleEngine();
            DataEngine.InitializeCommandManagerQueue();
            DataEngine.DataSourceExceptionOccurred += DataEngine_DataSourceExceptionOccurred;
            _fLogService.Info("成功启动Care服务...");
            return string.Empty;
        }

        private void DataEngine_DataSourceExceptionOccurred(object sender, ExceptionOccurredEventArgs e)
        {
            if (e.Exception == null)
            {
                _fLogService.Error("来自底层空的异常...");
                return;
            }
            Action action = new Action(() =>
            {
                switch (e.Exception.Source)
                {
                    case "M3_MonitorData":
                        _fLogService.Error("获取数据底层报了异常..." + e.Exception.Message);
                        SetMonitorData(string.Empty);
                        break;
                    default:
                        _fLogService.Error(string.Format("其他未处理的异常:{0},{1}", e.Exception.Source, e.Exception.ToString()));
                        break;
                }
            });
            action.BeginInvoke(null, null);
        }

        #region 注册事件
        private void LoadDispatchers()
        {
            //Web数据分发器
            // WebDispatcher.Instance.Initialize("127.0.0.1", DataServicePort);
            //UI数据分发器
            ClientDispatcher.Instance.Initialize("127.0.0.1", DataServicePort);

            ClientDispatcher.Instance.CommandReceived -= MainWindow_CommandReceived;
            ClientDispatcher.Instance.LedBaseInfoUpdated -= Instance_LedBaseInfoUpdated;
            ClientDispatcher.Instance.SystemVersionUpdate -= Instance_SystemVersionUpdate;
            ClientDispatcher.Instance.PhysicalDisplayInfoUpdated -= Instance_PhysicalDisplayInfoUpdated;
            ClientDispatcher.Instance.CareServiceConnectionStatusChanged -= Instance_CareServiceConnectionStatusChanged;
            ClientDispatcher.Instance.MonitoringDataReceived -= Instance_MonitoringDataReceived;
            ClientDispatcher.Instance.AlarmConfigSynchronized -= Instance_AlarmConfigSynchronized;
            ClientDispatcher.Instance.StrategySynchronized -= Instance_StrategySynchronized;
            ClientDispatcher.Instance.BrightnessValueRefreshed -= Instance_BrightnessValueRefreshed;
            ClientDispatcher.Instance.BrightnessConfigChanged -= Instance_BrightnessConfigChanged;

            ClientDispatcher.Instance.CommandReceived += MainWindow_CommandReceived;
            ClientDispatcher.Instance.LedBaseInfoUpdated += Instance_LedBaseInfoUpdated;
            ClientDispatcher.Instance.SystemVersionUpdate += Instance_SystemVersionUpdate;
            ClientDispatcher.Instance.PhysicalDisplayInfoUpdated += Instance_PhysicalDisplayInfoUpdated;
            ClientDispatcher.Instance.CareServiceConnectionStatusChanged += Instance_CareServiceConnectionStatusChanged;
            ClientDispatcher.Instance.MonitoringDataReceived += Instance_MonitoringDataReceived;
            ClientDispatcher.Instance.AlarmConfigSynchronized += Instance_AlarmConfigSynchronized;
            ClientDispatcher.Instance.StrategySynchronized += Instance_StrategySynchronized;
            ClientDispatcher.Instance.BrightnessValueRefreshed += Instance_BrightnessValueRefreshed;
            ClientDispatcher.Instance.BrightnessConfigChanged += Instance_BrightnessConfigChanged;
        }

        private void Instance_BrightnessConfigChanged(object sender, BrightnessConfigChangedEventArgs e)
        {
            var currentIndex = _brightnessConfigList.FindIndex(c => c.ScreenSN == e.SN);
            if (currentIndex >= 0)
            {
                _brightnessConfigList[currentIndex] = e.SmartLightConfig;
            }
            else
            {
                _brightnessConfigList.Add(e.SmartLightConfig);
            }
            if (BrightnessChangedEvent != null)
            {
                BrightnessChangedEvent(e.SmartLightConfig);
            }
        }

        private Dictionary<string, string> _currentBrightnessTable = new Dictionary<string, string>();
        private void Instance_BrightnessValueRefreshed(object sender, BrightnessValueRefreshEventArgs e)
        {
            if (_currentBrightnessTable.Keys.Contains(e.SN))
            {
                _currentBrightnessTable[e.SN] = e.BrightnessValue;
            }
            else
            {
                _currentBrightnessTable.Add(e.SN, e.BrightnessValue);
            }

            if (BrightnessValueRefreshed != null)
            {
                BrightnessValueRefreshed(this, e);
            }
        }

        public string GetCurrentBrightnessValueBy(string sn)
        {
            if (sn == null) return string.Empty;
            if (_currentBrightnessTable.Keys.Contains(sn))
            {
                return _currentBrightnessTable[sn];
            }
            else
            {
                return string.Empty;
            }
        }

        void Instance_PhysicalDisplayInfoUpdated(LEDDisplayInfoCollection infoCollection)
        {
            _fLogService.Info("开始收到物理屏变更信息...");
            SetPhysicalDisplayInfoUpdated(infoCollection);
            _fLogService.Info("完成收到物理屏变更信息...");
        }

        void MainWindow_CommandReceived(Command commad)
        {
            //Action action = new Action(() =>
            //{
            CommandReceivedBeginInvoke(commad);
            //});
            //action.BeginInvoke(null, null);
        }
        void Instance_LedBaseInfoUpdated(List<LedBasicInfo> registationInfos)
        {
            _fLogService.Info("开始上层得到屏信息...");
            _ledInfoList.Clear();
            _ledRegistationUiList.Clear();
            foreach (LedBasicInfo ledBas in registationInfos)
            {
                _ledInfoList.Add(ledBas);
            }
            if (_ledInfoList != null && _ledInfoList.Count > 0)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ReadDataBaseData));
                thread.Name = "读取数据库";
                thread.Start();
            }
            else
            {
                SendMonitorDataToLCT(null);
                LedBaseInfoEvent();
            }
            _fLogService.Info("完成上层得到屏信息...");
        }
        void Instance_SystemVersionUpdate(VersionUpdateInfo versionInfo)
        {
            _fLogService.Debug("收到更新软件包...");
            if (AppDataConfig.CurVersion == versionInfo.Version
                || System.Diagnostics.Process.GetProcessesByName("NovaUpdate").Length > 0)
            {
                return;
            }
            string[] strParams = new string[3];
            strParams[0] = versionInfo.Url;
            strParams[1] = versionInfo.MD5;
            strParams[2] = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string param = "\"" + strParams[0] + "\" \"" + strParams[1] + "\" \"" + strParams[2] + "\"";
            string str = AppDomain.CurrentDomain.BaseDirectory + "..\\AutoUpdate\\NovaUpdate.exe";
            _fLogService.Info(string.Format("开始正式更新,参数:{0}，路径：{1}", param, str));
            System.Diagnostics.Process.Start(str, param);
            _fLogService.Debug("内部完成正式更新软件...");
        }
        void Instance_CareServiceConnectionStatusChanged(bool status)
        {
            _fLogService.Debug("收到Care的连接心跳：" + status.ToString());
            CareServiceConnectionStatus = status;
            if (status)
            {
                _ledRegistationUiList.Clear();
                LedBasicToUIScreen();
            }
            if (CareServiceConnectionStatusChangedEvent != null)
            {
                CareServiceConnectionStatusChangedEvent(status);
            }
        }
        void Instance_MonitoringDataReceived(string key, object data)
        {
            Action action = new Action(() =>
            {
                MonitoringDataReceivedBeginInvoke(key, data);
            });
            action.BeginInvoke(null, null);
        }

        private void Instance_AlarmConfigSynchronized(object sender, AlarmConfigSynchronizedEventArgs e)
        {
            _fLogService.Info("收到Care过来告警的同步");
            if (e.AlarmConfig != null)
            {
                _ledAlarmConfigs.Remove(_ledAlarmConfigs.FirstOrDefault(a => a.SN == e.AlarmConfig.SN));
                _ledAlarmConfigs.Add((LedAlarmConfig)e.AlarmConfig.Clone());

                if (AlarmConfigSynchronizedEvent != null)
                {
                    _fLogService.Info("收到Care过来告警的同步通知UI");
                    AlarmConfigSynchronizedEvent(e.AlarmConfig.SN, null);
                }
            }
        }
        void Instance_StrategySynchronized(object sender, StrategySynchronizedEventArgs e)
        {
            _fLogService.Info("收到Care过来策略的同步");
            if (e.Strategys != null)
            {
                _strategyConfigDic.Clear();
                foreach (var item in e.Strategys.StrategyList)
                {
                    if (!_strategyConfigDic.Keys.Contains(item.SN))
                        _strategyConfigDic.Add(item.SN, new List<Strategy>());
                    _strategyConfigDic[item.SN].Add(item);
                }
                if (StrategySynchronizedEvent != null)
                {
                    _fLogService.Info("收到Care过来策略的同步通知UI");
                    StrategySynchronizedEvent(sender, null);
                }
            }
        }
        #endregion

        private void OnSendEMailLogComplete(MailUserToken userToken, NotifyState notifyState)
        {
            if (userToken == null) return;
            NotifyContent ntfContext = userToken.NtfContext;
            ntfContext.NotifyState = notifyState;
            bool res = MonitorDataAccessor.Instance().InsertSendEmailOprLog(DateTime.Now.ToString("yyyy-MM-dd"),
              CommandTextParser.GetJsonSerialization<NotifyContent>(ntfContext),
              SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            //写日志
        }

        private void LoadDataSources()
        {
            InitializeDataSourceInfo();
            foreach (var info in DataSourceInfos)
            {
                Type type = Type.GetType(info.Type, false, true);

                if (type != null)
                {
                    DataEngine.LoadDataSource(type, true);
                }
                else
                {
                    if (!string.IsNullOrEmpty(info.Location))
                    {
                        string filePath = string.Empty;
                        if (Path.IsPathRooted(info.Location))
                        {
                            filePath = info.Location;
                        }
                        else
                        {
                            filePath = Path.GetFullPath(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, info.Location));
                        }
                        FileInfo assemblypath = new FileInfo(filePath);
                        if (!File.Exists(assemblypath.FullName))
                        {
                            _fLogService.Error("文件不存在:" + assemblypath.FullName);
                            return;
                        }
                        System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFile(assemblypath.FullName);

                        foreach (Type t in ass.GetExportedTypes())
                        {
                            if (t.AssemblyQualifiedName == info.Type || t.FullName == info.Type)
                            {
                                type = t;
                                break;
                            }
                        }
                        if (type != null)
                        {
                            Action action = new Action(() =>
                            {
                                DataEngine.LoadDataSource(type, true);
                            });
                            action.BeginInvoke(null, null);
                        }
                    }
                }
            }
        }
        private void LoadDataSourceCallback(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                _fLogService.Info("数据源装载完成");
                LoadDispatchers();
                ClientDispatcher.Instance.LedBaseInfoUpdated -= Instance_LedBaseInfoUpdated;
                ClientDispatcher.Instance.LedBaseInfoUpdated += Instance_LedBaseInfoUpdated;
                ClientDispatcher.Instance.RefreshLedBasicInfo();
            }
            else
            {
                _fLogService.Error("数据源装载失败");
            }
        }
        private void ReadDataBaseData()
        {
            _fLogService.Info("打开数据库的结果是:" + Nova.Monitoring.DAL.MonitorDataAccessor.Instance().IsOpenDbResult.ToString());
            ReadAcquisitionConfig();
            ReadHWConfig();
            ReadAlarmConfig();
            ReadSpotInspectionConfig();
            ReadStrategyConfig();
            ReadPeripheralConfig();
            _fLogService.Debug(string.Format("<-{0}->:ThreadID={1}", "ReadDataBaseData", System.Threading.Thread.GetDomainID().ToString()));
            ReadBrightnessConfig();
            RefreshAllFunctionCardInfos();
            RefreshBrightnessConfigInfos();
            ReadUserConfigInfo();
            ReadPointDeteConfig();
            ReadEmailConfig();
            _fLogService.Info("完成读取数据库...");
            LedBasicToUIScreen();
            LedBaseInfoEvent();
            RefreshMonitoringData();
        }

        private void ReadSpotInspectionConfig()
        {
            var cfgList = MonitorDataAccessor.Instance().GetPeriodicInspectionCfg(string.Empty);
            foreach (LedBasicInfo key in LedInfoList)
            {
                var config = cfgList.Find(a => a.SN == key.Sn);
                if (cfgList.FindIndex(a => a.SN == key.Sn) >= 0)
                {
                    ClientDispatcher.Instance.UpdateSyncData(
                        key.Sn,
                        new SyncData() { SyncType = SyncType.AlarmConfig, SyncParam = SyncFlag.Synchronized, SyncContent = config.Content, Datestamp = SystemHelper.GetUtcTicksByDateTime(config.Time).ToString() });
                    DataDispatcher.ClientDispatcher.Instance.SetPeriodicInspectionConfig(config.SN, config.Content);
                }
            }
        }

        private void LedBaseInfoEvent()
        {
            IsGetLedInfo = true;
            if (LedScreenChangedEvent != null)
            {
                Action action = new Action(() =>
                {
                    _fLogService.Info("屏体变更通知到UI");
                    LedScreenChangedEvent(null, null);
                });
                action.BeginInvoke(null, null);
            }
        }
        private void ReadAcquisitionConfig()
        {
            List<string> list;
            List<DateTime> timeList;
            MonitorDataAccessor.Instance().GetSoftWareCfg(out list, out timeList);
            if (list != null && list.Count > 0)
            {
                AcquisitionConfig = CommandTextParser.GetDeJsonSerialization<LedAcquisitionConfig>(list[0]);
                DataDispatcher.ClientDispatcher.Instance.SetLedAcquisitionConfig(list[0]);
            }
            if (AcquisitionConfig == null)
            {
                AcquisitionConfig = new LedAcquisitionConfig();
                AcquisitionConfig.DataPeriod = 60000;
                AcquisitionConfig.IsAutoRefresh = false;
                AcquisitionConfig.RetryCount = 0;
                DataDispatcher.ClientDispatcher.Instance.SetLedAcquisitionConfig(
                    CommandTextParser.GetJsonSerialization<LedAcquisitionConfig>(AcquisitionConfig));
            }
        }
        private void ReadHWConfig()
        {
            List<ConfigInfo> cfgList = MonitorDataAccessor.Instance().GetHardWareCfg(string.Empty);
            if (_ledMonitorConfigs == null)
            {
                _ledMonitorConfigs = new List<LedMonitoringConfig>();
            }
            else
            {
                _ledMonitorConfigs.Clear();
            }
            foreach (LedBasicInfo key in LedInfoList)
            {
                LedMonitoringConfig ledMonitor = null;
                if (cfgList.FindIndex(a => a.SN == key.Sn) >= 0)
                {
                    ledMonitor = CommandTextParser.GetDeJsonSerialization<LedMonitoringConfig>(cfgList.Find(a => a.SN == key.Sn).Content);
                    if (ledMonitor != null)
                    {
                        ledMonitor.SN = key.Sn;
                    }
                }
                if (ledMonitor == null)
                {
                    ledMonitor = new LedMonitoringConfig();
                    ledMonitor.MonitoringCardConfig = new MonitoringCardConfig();
                    ledMonitor.MonitoringCardConfig.MonitoringEnable = false;
                    ledMonitor.SN = key.Sn;
                }
                DataDispatcher.ClientDispatcher.Instance.SetLedMonitoringConfig(
                    CommandTextParser.GetJsonSerialization<LedMonitoringConfig>(ledMonitor));
                _ledMonitorConfigs.Add((LedMonitoringConfig)ledMonitor.Clone());
            }

            if (LedInfoList.Count > 1)
            {
                LedMonitoringConfig ledMonitor = (LedMonitoringConfig)_ledMonitorConfigs[0].Clone();
                ledMonitor.SN = ALLScreenName;
                _ledMonitorConfigs.Insert(0, ledMonitor);
            }
        }
        private void ReadPointDeteConfig()
        {
            List<ConfigInfo> cfgList = MonitorDataAccessor.Instance().GetPointDetectCfg(string.Empty);
            DetectConfigParams cfg;
            Dictionary<string, DetectConfigParams> detectConfigParamList = new Dictionary<string, DetectConfigParams>();
            foreach (LedBasicInfo key in LedInfoList)
            {
                if (cfgList.FindIndex(a => a.SN == key.Sn) < 0) continue;
                var config = cfgList.Find(a => a.SN == key.Sn);
                cfg = CommandTextParser.GetDeJsonSerialization<DetectConfigParams>(config.Content);
                if (cfg != null)
                {
                    detectConfigParamList.Clear();
                    detectConfigParamList.Add(key.Sn, cfg);
                    ClientDispatcher.Instance.UpdateSyncData(
                        key.Sn,
                        new SyncData() { SyncType = SyncType.PeriodicInspectionConfig, SyncParam = SyncFlag.Synchronized, SyncContent = config.Content, Datestamp = SystemHelper.GetUtcTicksByDateTime(config.Time).ToString() });
                    DataDispatcher.ClientDispatcher.Instance.SetPointDeteConfig(CommandTextParser.GetJsonSerialization<Dictionary<string, DetectConfigParams>>(detectConfigParamList));
                }
            }
        }
        private void ReadAlarmConfig()
        {
            List<ConfigInfo> cfgList = MonitorDataAccessor.Instance().GetAlarmCfg(string.Empty);
            if (_ledAlarmConfigs == null)
            {
                _ledAlarmConfigs = new List<LedAlarmConfig>();
            }
            else
            {
                _ledAlarmConfigs.Clear();
            }

            foreach (LedBasicInfo key in LedInfoList)
            {
                LedAlarmConfig ledAlarm = null;
                var config = cfgList.Find(a => a.SN == key.Sn);
                if (cfgList.FindIndex(a => a.SN == key.Sn) >= 0)
                {
                    ledAlarm = CommandTextParser.GetDeJsonSerialization<LedAlarmConfig>(config.Content);
                    ledAlarm.SN = key.Sn;
                    ClientDispatcher.Instance.UpdateSyncData(
                        key.Sn,
                        new SyncData() { SyncType = SyncType.AlarmConfig, SyncParam = SyncFlag.Synchronized, SyncContent = config.Content, Datestamp = SystemHelper.GetUtcTicksByDateTime(config.Time).ToString() });
                }
                if (ledAlarm == null)
                {
                    ledAlarm = GetInitialAlarmConfg(key.Sn);
                }
                DataDispatcher.ClientDispatcher.Instance.SetLedAlarmConfig(
                    CommandTextParser.GetJsonSerialization<LedAlarmConfig>(ledAlarm));
                _ledAlarmConfigs.Add((LedAlarmConfig)ledAlarm.Clone());

            }

            if (LedInfoList.Count > 1)
            {
                LedAlarmConfig ledAlarm = (LedAlarmConfig)_ledAlarmConfigs[0].Clone();
                ledAlarm.SN = ALLScreenName;
                _ledAlarmConfigs.Insert(0, ledAlarm);
            }
        }
        private void ReadStrategyConfig()
        {
            string stra;
            DateTime time;
            MonitorDataAccessor.Instance().GetStrategy(out stra, out time);
            if (_strategyConfigDic == null)
            {
                _strategyConfigDic = new Dictionary<string, List<Strategy>>();
            }
            else
            {
                _strategyConfigDic.Clear();
            }
            if (stra == string.Empty) return;
            StrategyTable strategyTable = CommandTextParser.GetDeJsonSerialization<StrategyTable>(stra);
            if (strategyTable == null)
            {
                return;
            }
            DataDispatcher.ClientDispatcher.Instance.SetStrategy(stra);
            foreach (var item in strategyTable.StrategyList)
            {
                if (!_strategyConfigDic.Keys.Contains(item.SN))
                    _strategyConfigDic.Add(item.SN, new List<Strategy>());
                _strategyConfigDic[item.SN].Add(item);
            }

        }
        private void ReadPeripheralConfig()
        {
            string per;
            DateTime time;
            MonitorDataAccessor.Instance().GetPeripheralCfg(out per, out time);
            if (_peripheralCfgDICDicDb == null)
            {
                _peripheralCfgDICDicDb = new SerializableDictionary<FunctionCardRoadInfo, string>();
            }
            else
            {
                _peripheralCfgDICDicDb.Clear();
            }
            if (string.IsNullOrEmpty(per))
            {
                _peripheralCfgDICDicDb.Clear();
            }
            else
            {
                SerializableDictionary<string, string> dicDb = CommandTextParser.GetDeJsonSerialization<
                    SerializableDictionary<string, string>>(per);
                foreach (KeyValuePair<string, string> keyvalue in dicDb)
                {
                    _peripheralCfgDICDicDb.Add(
                        CommandTextParser.GetDeJsonSerialization<FunctionCardRoadInfo>(keyvalue.Key), keyvalue.Value);
                }
            }
        }
        private void ReadBrightnessConfig()
        {
            lock (_objLockReadData)
            {
                List<ConfigInfo> configInfoList = MonitorDataAccessor.Instance().GetLightProbeCfg(string.Empty);
                if (_brightnessConfigList == null)
                {
                    _brightnessConfigList = new List<SmartLightConfigInfo>();
                }
                else
                {
                    _brightnessConfigList.Clear();
                }
                if (configInfoList.Count == 0)
                {
                    return;
                }
                foreach (LedBasicInfo item in _ledInfoList)
                {
                    ConfigInfo config = configInfoList.Find(a => a.SN == item.Sn);
                    if (!string.IsNullOrEmpty(config.SN))
                    {
                        var smartLightConfigInfo = CommandTextParser.GetDeJsonSerialization<SmartLightConfigInfo>(config.Content);
                        _fLogService.Debug(string.Format("<-{0}->:ThreadID={1}", "ReadBrightnessConfig", System.Threading.Thread.GetDomainID().ToString()));
                        UpdateBrightnessSyncData(config.SN, config);
                        _brightnessConfigList.Add(smartLightConfigInfo);
                    }
                }
            }
        }

        private void UpdateBrightnessSyncData(string id, ConfigInfo smartLightConfigInfo)
        {
            var smartLightConfig = CommandTextParser.GetDeJsonSerialization<SmartLightConfigInfo>(smartLightConfigInfo.Content);
            var brightnessConfig = new BrightnessConfig(smartLightConfig);
            var brightnessJson = CommandTextParser.GetJsonSerialization<BrightnessConfig>(brightnessConfig);

            _fLogService.Debug(string.Format("<-{0}->:ThreadID={1}", "UpdateBrightnessSyncData", System.Threading.Thread.GetDomainID().ToString()));
            ClientDispatcher.Instance.UpdateSyncData(
                        id,
                        new SyncData() { SyncType = SyncType.BrightnessRuleConfig, SyncParam = SyncFlag.Synchronized, SyncContent = brightnessJson, Datestamp = SystemHelper.GetUtcTicksByDateTime(smartLightConfigInfo.Time).ToString() });
        }
        private void ReadUserConfigInfo()
        {
            ConfigInfo cfg = MonitorDataAccessor.Instance().GetUserCfg();
            if (_userConfigInfo == null)
            {
                _userConfigInfo = new UserConfig();
            }
            if (string.IsNullOrEmpty(cfg.Content))
            {
                _userConfigInfo = new UserConfig();
                _userConfigInfo.TemperatureUnit = TemperatureType.Celsius;
            }
            else
            {
                _userConfigInfo = CommandTextParser.GetDeJsonSerialization<UserConfig>(cfg.Content);
            }
        }
        private void ReadEmailConfig()
        {
            ConfigInfo cfg = MonitorDataAccessor.Instance().GetEmailCfg();
            if (NotifyConfig == null)
            {
                NotifyConfig = new EMailNotifyConfig();
            }
            if (string.IsNullOrEmpty(cfg.Content))
            {
                NotifyConfig = new EMailNotifyConfig();
            }
            else
            {
                NotifyConfig = CommandTextParser.GetDeJsonSerialization<EMailNotifyConfig>(cfg.Content);
            }
        }
        private void InitializeDataSourceInfo()
        {
            _dataSourceInfos.Add(new DataSourceInfo()
            {
                Type = "Nova.Monitoring.MarsDataAcquisition.DataAcquisition",
                Location = "Nova.Monitoring.MarsDataAcquisition.dll",
                Name = "M3数据源"
            });
        }
        public void LedBasicToUIScreen()
        {
            DataDispatcher.ClientDispatcher.Instance.GetLedRegistationInfo(AppDataConfig.CurrentMAC, new Action<IRestResponse>((response) =>
                 {
                     lock (_lockResgiter)
                     {
                         _fLogService.Info("从Care得到注册的信息");
                         _ledRegistationUiList = GetRegistationInfoByString(response.Content);
                         //LedBaseInfoEvent();
                         LedRegistationInfoEventMethod(true);
                     }
                 }), string.Empty);
        }

        private List<LedRegistationInfoResponse> GetRegistationInfoByString(string response)
        {
            List<LedRegistationInfoResponse> registationInfos = new List<LedRegistationInfoResponse>();

            List<LedRegistationInfoResponse> registInfoResponseList =
                CommandTextParser.GetDeJsonSerialization<List<LedRegistationInfoResponse>>(response);
            if (registInfoResponseList == null)
            {
                registInfoResponseList = new List<LedRegistationInfoResponse>();
            }
            foreach (LedBasicInfo ledbasic in _ledInfoList)
            {
                LedRegistationInfoResponse registationInfo =
                    registInfoResponseList.Find(a => a.SN == ledbasic.Sn);
                if (registationInfo == null)
                {
                    registationInfo = new LedRegistationInfoResponse()
                    {
                        IsRegisted = false,
                        User = null,
                        Name = null,
                        Mac = AppDataConfig.CurrentMAC,
                        SN = ledbasic.Sn
                    };
                }
                ledbasic.AliaName = registationInfo.Name;
                registationInfos.Add(registationInfo);
            }
            _fLogService.Info("完成获取到的Care注册的信息处理");
            return registationInfos;
        }
        private void SetPhysicalDisplayInfoUpdated(LEDDisplayInfoCollection infoCollection)
        {
            _allCommPortLedDisplayDic = new SerializableDictionary<string, List<ILEDDisplayInfo>>();

            foreach (KeyValuePair<string, List<SimpleLEDDisplayInfo>> keyValue
                in infoCollection.LedSimples)
            {
                foreach (SimpleLEDDisplayInfo simple in keyValue.Value)
                {
                    if (!_allCommPortLedDisplayDic.ContainsKey(keyValue.Key))
                    {
                        _allCommPortLedDisplayDic.Add(keyValue.Key, new List<ILEDDisplayInfo>());
                    }
                    _allCommPortLedDisplayDic[keyValue.Key].Add(
                        new SimpleLEDDisplayInfo(simple.PixelColsInScanBd,
                            simple.PixelRowsInScanBd, simple.ScanBdCols, simple.ScanBdRows, simple.PortCols,
                            simple.PortRows, simple.SenderIndex, simple.X, simple.Y,
                            simple.VirtualMode, simple.PortScanBdInfoList));
                }
            }
            foreach (KeyValuePair<string, List<StandardLEDDisplayInfo>> keyValue
                in infoCollection.LedStandards)
            {
                foreach (StandardLEDDisplayInfo standard in keyValue.Value)
                {
                    if (!_allCommPortLedDisplayDic.ContainsKey(keyValue.Key))
                    {
                        _allCommPortLedDisplayDic.Add(keyValue.Key, new List<ILEDDisplayInfo>());
                    }
                    _allCommPortLedDisplayDic[keyValue.Key].Add((StandardLEDDisplayInfo)standard.Clone());
                }
            }
            foreach (KeyValuePair<string, List<ComplexLEDDisplayInfo>> keyValue
                in infoCollection.LedComplex)
            {
                foreach (ComplexLEDDisplayInfo complex in keyValue.Value)
                {
                    if (!_allCommPortLedDisplayDic.ContainsKey(keyValue.Key))
                    {
                        _allCommPortLedDisplayDic.Add(keyValue.Key, new List<ILEDDisplayInfo>());
                    }
                    _allCommPortLedDisplayDic[keyValue.Key].Add((ComplexLEDDisplayInfo)complex.Clone());
                }
            }
        }

        private void SetFunctionToDic(string data)
        {
            FunctionCardLocateInfo funInfo = CommandTextParser.GetDeJsonSerialization<FunctionCardLocateInfo>(data);
            if (_peripheralCfgDICDic == null)
            {
                _peripheralCfgDICDic = new SerializableDictionary<string, string>();
            }
            else
            {
                _peripheralCfgDICDic.Clear();
            }

            if (funInfo == null || funInfo.UseableFunctionCardList == null
                || funInfo.UseableFunctionCardList.Count == 0)
            {
            }
            else
            {
                bool isFind = false;
                foreach (FunctionCardLocation func in funInfo.UseableFunctionCardList)
                {
                    isFind = false;
                    foreach (KeyValuePair<FunctionCardRoadInfo, string> keyDb in _peripheralCfgDICDicDb)
                    {
                        if (func.Equals(keyDb.Key.FunCardLocation))
                        {
                            isFind = true;
                            _peripheralCfgDICDic.Add(CommandTextParser.GetJsonSerialization<FunctionCardRoadInfo>(keyDb.Key), keyDb.Value);
                            continue;
                        }
                    }
                    if (isFind == false)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            FunctionCardRoadInfo roadInfo = new FunctionCardRoadInfo() { FunCardLocation = func, PowerIndex = i };
                            roadInfo.PowerAliaName = roadInfo.ToString();
                            _peripheralCfgDICDic.Add(CommandTextParser.GetJsonSerialization<FunctionCardRoadInfo>(roadInfo)
                            , roadInfo.PowerAliaName);
                        }
                    }
                }
            }
            UITimerOutCancel();
            if (GetFunctionCardPowerEvent != null)
            {
                GetFunctionCardPowerEvent(null, null);
            }
        }
        private void SetBrightnessConfigInfos(string data)
        {
            SmartLightConfigInfo lightDataList = CommandTextParser.GetDeJsonSerialization<SmartLightConfigInfo>(data);
            if (lightDataList != null && lightDataList.DisplayHardcareConfig != null && lightDataList.DisplayHardcareConfig.OneDayConfigList != null)
            {
                foreach (var item in lightDataList.DisplayHardcareConfig.OneDayConfigList)
                {
                    item.StartTime = new DateTime(2014, 1, 1, item.StartTime.Hour, item.StartTime.Minute, item.StartTime.Second);
                }
            }
            lock (_objLockReadData)
            {
                int index;
                index = _brightnessConfigList.FindIndex(a => a.ScreenSN == lightDataList.ScreenSN);
                bool _isExist = true;
                if (index < 0)
                {
                    lightDataList.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
                    if (lightDataList.DisplayHardcareConfig != null && lightDataList.DisplayHardcareConfig.OneDayConfigList != null
                        && lightDataList.DisplayHardcareConfig.OneDayConfigList.Count > 0)
                    {
                        lightDataList.DispaySoftWareConfig = CommandTextParser.GetDeJsonSerialization<DisplaySmartBrightEasyConfig>
                            (CommandTextParser.GetJsonSerialization<DisplaySmartBrightEasyConfigBase>(lightDataList.DisplayHardcareConfig));
                        if (lightDataList.HwExecTypeValue == BrightnessHWExecType.MannualControl)
                        {
                            lightDataList.AdjustType = BrightAdjustType.Mannual;
                        }
                        else
                        {
                            lightDataList.AdjustType = BrightAdjustType.Smart;
                        }
                        if (lightDataList.DispaySoftWareConfig == null)
                        {
                            lightDataList.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
                        }
                        lightDataList.DispaySoftWareConfig.DisplayUDID = lightDataList.ScreenSN;
                    }
                    else
                    {
                        lightDataList.AdjustType = BrightAdjustType.Mannual;
                        _isExist = false;
                    }
                    _brightnessConfigList.Add(lightDataList);
                    index = _brightnessConfigList.Count - 1;
                }
                else
                {
                    if (lightDataList.DisplayHardcareConfig == null
                        || lightDataList.DisplayHardcareConfig.OneDayConfigList == null
                        || lightDataList.DisplayHardcareConfig.OneDayConfigList.Count == 0)
                    {
                        DisplaySmartBrightEasyConfigBase abc = _brightnessConfigList[index].DispaySoftWareConfig.Clone() as DisplaySmartBrightEasyConfigBase;
                        _brightnessConfigList[index].DisplayHardcareConfig =
                            CommandTextParser.GetDeJsonSerialization<DisplaySmartBrightEasyConfigBase>
                            (CommandTextParser.GetJsonSerialization<DisplaySmartBrightEasyConfigBase>(abc));
                        _isExist = false;
                    }
                    else
                    {
                        lightDataList.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
                        DisplaySmartBrightEasyConfigBase sb = (DisplaySmartBrightEasyConfigBase)lightDataList.DisplayHardcareConfig.Clone();
                        lightDataList.DispaySoftWareConfig = CommandTextParser.GetDeJsonSerialization<DisplaySmartBrightEasyConfig>
                            (CommandTextParser.GetJsonSerialization<DisplaySmartBrightEasyConfigBase>(sb));
                        lightDataList.DispaySoftWareConfig.DisplayUDID = lightDataList.ScreenSN;
                        string strDb = CommandTextParser.GetJsonSerialization<DisplaySmartBrightEasyConfigBase>(_brightnessConfigList[index].DisplayHardcareConfig, true);
                        string strHW = CommandTextParser.GetJsonSerialization<DisplaySmartBrightEasyConfigBase>(lightDataList.DisplayHardcareConfig, true);

                        if (strDb != strHW)
                        {
                            _fLogService.Debug("strDb:" + strDb);
                            _fLogService.Debug("strHW:" + strHW);
                            if (lightDataList.HwExecTypeValue == BrightnessHWExecType.MannualControl)
                            {
                                lightDataList.AdjustType = BrightAdjustType.Mannual;
                            }
                            else
                            {
                                lightDataList.AdjustType = BrightAdjustType.Smart;
                            }
                        }
                        else
                        {
                            lightDataList.AdjustType = _brightnessConfigList[index].AdjustType;
                        }
                        _brightnessConfigList[index] = lightDataList;
                    }
                }
                _brightnessConfigList[index].HwExecTypeValue = BrightnessHWExecType.DisHardWareControl;
                SmartLightConfigInfo tmp = (SmartLightConfigInfo)_brightnessConfigList[index].Clone();
                if (_isExist)
                {
                    DataDispatcher.ClientDispatcher.Instance.SaveSmartLightConfigInfo(tmp.ScreenSN, tmp);
                }
                DataDispatcher.ClientDispatcher.Instance.SetBrightnessConfigToLCT(
                    CommandTextParser.GetJsonSerialization<List<SmartLightConfigInfo>>(new List<SmartLightConfigInfo>() { tmp }));
                tmp.DisplayHardcareConfig = null;
                DataDispatcher.ClientDispatcher.Instance.SetSmartLightConfigInfo(CommandTextParser.GetJsonSerialization<SmartLightConfigInfo>(tmp));
                if (BrightnessChangedEvent != null)
                {
                    BrightnessChangedEvent(tmp);
                }
            }
        }

        private void SetMonitorData(string strData)
        {
            lock (_lockMonitordata)
            {
                _stopwatch.Stop();
                _fLogService.Debug("Monitor ReceiveData Timer:" + _stopwatch.ElapsedMilliseconds);
                ScreenMonitorData = CommandTextParser.GetDeJsonSerialization<AllMonitorData>(strData);
                if (MonitorDataChangedEvent != null)
                {
                    MonitorDataChangedEvent(null, null);
                }
                else
                {
                    Frm_MonitorDisplayMain_VM main_vm = new Frm_MonitorDisplayMain_VM();
                    main_vm.OnCmdInitialize();
                }
                if (_sendMonitorErrMsg != null && ScreenMonitorData != null)
                {
                    GetAllFaultAndAlarmInfo();
                }
            }
        }
        private void WriteLog(string msg)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("NovaMonitorManager：MonitorAll{0}：日志：{1}",
                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), msg));
        }

        private void GetAllFaultAndAlarmInfo()
        {
            _sendMonitorErrMsg.EMailLangHsTable = EMailLangHsTable;
            _sendMonitorErrMsg.NotifyConfig = NotifyConfig;
            _sendMonitorErrMsg.LedInfoList = LedInfoList;
            _sendMonitorErrMsg.LedAlarmConfigs = LedAlarmConfigs;
            _sendMonitorErrMsg.ScreenMonitorData = (AllMonitorData)ScreenMonitorData.Clone();
            _sendMonitorErrMsg.GetEMailMonitorErrMsg();
            DeleteTimeEMailLog();
        }
        private void DeleteTimeEMailLog()
        {
            string time = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            DeleteOneSendEMailLog(time);
        }
        #endregion

        #region 内部配置初始化
        private LedAlarmConfig GetInitialAlarmConfg(string sn)
        {
            LedAlarmConfig ledAlarm = new LedAlarmConfig();
            ledAlarm.SN = sn;
            ledAlarm.ParameterAlarmConfigList = new List<ParameterAlarmConfig>();

            ParameterAlarmConfig param = new ParameterAlarmConfig();
            param.ParameterType = StateQuantityType.Temperature;
            param.HighThreshold = 60;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.ParameterType = StateQuantityType.Humidity;
            param.HighThreshold = 60;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.ParameterType = StateQuantityType.Voltage;
            param.LowThreshold = 4;
            param.HighThreshold = 12;
            param.Level = AlarmLevel.Warning;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.ParameterType = StateQuantityType.Voltage;
            param.HighThreshold = 3.5;
            param.LowThreshold = 0;
            param.Level = AlarmLevel.Malfunction;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.ParameterType = StateQuantityType.FanSpeed;
            param.LowThreshold = 1000;
            param.LowThreshold = 0;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            return ledAlarm;
        }
        #endregion

        public bool IsAllScreen(string screenSN)
        {
            if (screenSN == ALLScreenName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Dispose()
        {
            if (_sendMonitorErrMsg != null)
            {
                _sendMonitorErrMsg.WriteSendEMailLogEvent -= OnSendEMailLogComplete;
                _sendMonitorErrMsg.Dispose();
            }
            DataDispatcher.ClientDispatcher.Instance.Dispose();
            if (_instance != null)
            {
                _instance = null;
            }
        }
    }
}