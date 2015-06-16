using Log4NetLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.LCT.GigabitSystem.HWPointDetect;
using Nova.Monitoring.Common;
using Nova.Monitoring.DAL;
using Nova.Monitoring.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Nova.Monitoring.DataDispatcher
{
    public partial class ClientDispatcher : IDataDispatcher, IDisposable
    {
        private static readonly object _syncObj = new object();
        private static readonly object _syncScreenList = new object();
        private static ClientDispatcher _instance;
        private UIClient _dataClient;
        private string _host;
        private int _port;
        private Timer _getScreenMacTimer;
        private Timer _heartbeatTimer;
        private Timer _checkSystemUpdateTimer;
        private Queue<Command> _commandQueue = new Queue<Command>();
        private object _obj = new object();
        private Command _currentCommand;
        private bool _commandReturn = false;
        private bool _serviceConnectionStatus = false;
        private bool _isSynchronizing = false;
        private static SemaphoreSlim _sem = new SemaphoreSlim(1);

        private List<Led> _ledList = new List<Led>();

        private Dictionary<string, string> _macTable;

        private Dictionary<string, Handler> _handlers;
        //private Dictionary<string, SyncInformation> _syncInfoTable = new Dictionary<string, SyncInformation>();

        private ILogService _logService;


        public delegate void MonitoringDataReceivedHandle(string key, object monitoringData);
        public event MonitoringDataReceivedHandle MonitoringDataReceived;

        public delegate void CommandReceiveHandle(Command commad);
        public event CommandReceiveHandle CommandReceived;

        public delegate void LedBaseInfoUpdatedHandle(List<LedBasicInfo> ledBasicInfos);
        public event LedBaseInfoUpdatedHandle LedBaseInfoUpdated;

        public delegate void PhysicalDisplayInfoUpdatedHandle(LEDDisplayInfoCollection infoCollection);
        public event PhysicalDisplayInfoUpdatedHandle PhysicalDisplayInfoUpdated;

        public delegate void SystemVersionUpdateHandle(VersionUpdateInfo versionInfo);
        public event SystemVersionUpdateHandle SystemVersionUpdate;

        public delegate void CareServiceConnectionStatusChangedHandle(bool status);
        public event CareServiceConnectionStatusChangedHandle CareServiceConnectionStatusChanged;

        public event EventHandler<StrategySynchronizedEventArgs> StrategySynchronized;

        public event EventHandler<AlarmConfigSynchronizedEventArgs> AlarmConfigSynchronized;

        public event EventHandler<BrightnessConfigChangedEventArgs> BrightnessConfigChanged;

        public event EventHandler<BrightnessValueRefreshEventArgs> BrightnessValueRefreshed;

        public static ClientDispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClientDispatcher();
                        }
                    }
                }
                return _instance;
            }
        }

        public ClientDispatcher()
        {
            CreateHandlers();
            _syncInformationManager = new SyncInformationManager();
            _logService = new FileLogService(typeof(ClientDispatcher));

        }

        private void GetScreenMacInfo(object state)
        {
            var request = new List<ScreenMacInfo>();
            LedList.ForEach(l => request.Add(new ScreenMacInfo() { Sn = l.SerialNumber }));
            RestFulClient.Instance.Post("api/index/getmac", request, response =>
            {
                ProcessScreenMacInfo(response);
            });
        }

        private void ProcessScreenMacInfo(IRestResponse response)
        {
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                ScreenMacResponse resultScreenMacInfo = null;
                var screenMacResponse = JsonConvert.DeserializeObject<List<ScreenMacResponse>>(response.Content);
                if (screenMacResponse == null)
                {
                    resultScreenMacInfo = null;
                }
                else
                {
                    resultScreenMacInfo = screenMacResponse.Find(r => !string.IsNullOrEmpty(r.Mac));
                }

                if (resultScreenMacInfo == null)
                {
                    AppDataConfig.CurrentMAC = SystemHelper.GetMACAddress(string.Empty);
                }
                else
                {
                    AppDataConfig.CurrentMAC = SystemHelper.GetMACAddress(resultScreenMacInfo.Mac);
                }
                if (_heartbeatTimer == null)
                {
                    _heartbeatTimer = new Timer(HeartbeatWithCareServer, null, 500, 15000);
                }
                if (_getScreenMacTimer != null)
                {
                    _getScreenMacTimer.Dispose();
                }
            }
        }





        public List<Led> LedList
        {
            get
            {
                lock (_syncScreenList)
                {
                    return _ledList;
                }
            }
            set
            {
                lock (_syncScreenList)
                {
                    _ledList = value;
                }
            }
        }


        public void Initialize(string host, int port)
        {
            if (AppDataConfig.CurrentMAC == null || AppDataConfig.CurrentMAC == string.Empty)
            {
                AppDataConfig.CurrentMAC = SystemHelper.GetMACAddress(string.Empty);
            }
            _host = host;
            _port = port;
            _dataClient = new UIClient();
            _dataClient.Connect(_host, _port);
            if (_dataClient.Hello())
            {

            }
            else
            {
                _dataClient.Disconnect();
            }
            _dataClient.Login("UIDispatcher");
            _dataClient.Register(new string[] { "*" });
            _dataClient.DataReceived += DataClient_DataReceived;
            _dataClient.CommandReceived += DataClient_CommandReceived;
        }

        private void CreateHandlers()
        {
            _handlers = new Dictionary<string, Handler>();
            _handlers.Add("ScreenBaseInfoList", new ScreenBaseInfoListHandler(this));
            _handlers.Add("AllPhysicalDisplayInfo", new AllPhysicalDisplayInfoHandler(this));
            _handlers.Add("M3_MonitoringData", new CommonHandler(this));
            _handlers.Add("MonitoringData", new MonitoringDataHandler(this));
            _handlers.Add("UpdateOpticalProbeInfo", new CommonHandler(this));
            _handlers.Add("UpdateFunctionCardInfo", new CommonHandler(this));
            _handlers.Add("ReadSmartLightHWconfigInfo", new CommonHandler(this));
            _handlers.Add("M3_StateData", new CommonHandler(this));
            _handlers.Add("FromCOMFindSN", new CommonHandler(this));
            _handlers.Add("SpotInspectionResult", new SpotInspectionResultHandler(this));
            _handlers.Add("BrightnessLog", new BrightnessLogHandler(this));
        }


        private Handler LookupHandlerBy(string handlerName)
        {
            if (_handlers.Keys.Contains(handlerName))
                return _handlers[handlerName];
            else
                return null;
        }

        public void NotifyLedBaseInfoUpdated(List<LedBasicInfo> screenBaseInfos)
        {
            if (LedBaseInfoUpdated != null)
            {
                //Action<List<LedBasicInfo>> action = new Action<List<LedBasicInfo>>((list) =>
                //{
                LedBaseInfoUpdated(screenBaseInfos);
                //});
                //action.BeginInvoke(screenBaseInfos, null, null);
            }
            //UpdateLedConfigSyncCollection();
            UpdateLedConfigSyncData();

            if (LedList != null && LedList.Count > 0 && _getScreenMacTimer == null)
            {
                _getScreenMacTimer = new Timer(GetScreenMacInfo, null, 200, 50000);
            }

        }
        public void NotifyPhysicalDisplayInfoUpdated(LEDDisplayInfoCollection infoCollection)
        {
            if (PhysicalDisplayInfoUpdated != null)
            {
                //Action<LEDDisplayInfoCollection> action = new Action<LEDDisplayInfoCollection>((info) =>
                //{
                PhysicalDisplayInfoUpdated(infoCollection);
                //});
                //action.BeginInvoke(infoCollection, null, null);
            }
        }

        public void NotifyBrightnessValueRefreshed(string sn, string value)
        {
            if (BrightnessValueRefreshed != null)
            {
                Action<string, string> action = new Action<string, string>((s, v) =>
                {
                    BrightnessValueRefreshed(this, new BrightnessValueRefreshEventArgs(s, v));
                });
                action.BeginInvoke(sn, value, null, null);
            }
        }

        public void NotifyDataReceived(string key, object data)
        {
            if (MonitoringDataReceived != null)
            {
                //Action<string, object> action = new Action<string, object>((k, d) =>
                //{
                MonitoringDataReceived(key, data);
                //});
                //action.BeginInvoke(key, data, null, null);
            }
        }
        public void NotifyCommandReceived(Command command)
        {
            if (CommandReceived != null)
            {
                //Action<Command> action = new Action<Command>((c) =>
                //{
                CommandReceived(command);
                //});
                //action.BeginInvoke(command, null, null);
            }
        }
        private void DataClient_CommandReceived(Command command)
        {
            //Action action = new Action(() =>
            //{
            ProcessReceivedCommand(command);
            //});
            //action.BeginInvoke(null, null);
        }

        private void ProcessReceivedCommand(Command command)
        {
            try
            {
                if (command.Code == CommandCode.UpdateSystem)
                {
                    var systemVersionInfo = JsonConvert.DeserializeObject<VersionUpdateInfo>(command.CommandText);
                    if (systemVersionInfo == null)
                        return;
                    if (SystemVersionUpdate != null)
                    {
                        SystemVersionUpdate(systemVersionInfo);
                    }
                }
                NotifyCommandReceived(command);
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "DataClient_CommandReceived", ex.ToString()));
                Debug.WriteLine(string.Format("当前Command信息：{0}-{1} \r\n异常信息：{2}", command.Code, command.CommandText, ex.Message));
            }
        }

        private void DataClient_DataReceived(string identity, object data)
        {
            Action action = new Action(() =>
            {
                ProcessReceivedData(identity, data);
            });
            action.BeginInvoke(null, null);
        }

        private void ProcessReceivedData(string identity, object data)
        {
            Handler handler = LookupHandlerBy(identity);
            if (handler == null)
            {
                return;
            }
            handler.Execute(identity, data);

            #region MonitoringData Parse

            /* 解析监控数据*/

            //List<DataPoint> dataPointCollection = new List<DataPoint>();
            //if (typeof(List<DataPoint>) == data.GetType())
            //{
            //    dataPointCollection = data as List<DataPoint>;
            //}
            //else if (typeof(DataPoint) == data.GetType())
            //{
            //    dataPointCollection = new List<DataPoint>() { data as DataPoint };
            //}
            //List<ComponentBase> componetBaseList = new List<ComponentBase>();
            //foreach (var dataPoint in dataPointCollection)
            //{
            //    string value = dataPoint.Value as string;
            //    string[] splitKeys = dataPoint.Key.Split(new Char[] { '|' });
            //    var deviceType = (DeviceType)int.Parse(splitKeys[0], System.Globalization.NumberStyles.AllowHexSpecifier);
            //    var parameterType = (StateQuantityType)int.Parse(splitKeys[1], System.Globalization.NumberStyles.AllowHexSpecifier);
            //    var infoOffset = 4;// int.Parse(splitKeys[2]);
            //    var positionInfo = splitKeys[3];
            //    string markToken = string.Empty;
            //    if (positionInfo.Length > infoOffset)
            //        markToken = positionInfo.Substring(0, positionInfo.Length - infoOffset);
            //    else
            //        markToken = positionInfo;
            //    var resultComponet = componetBaseList.FirstOrDefault(t => t.PositionInfo == markToken);
            //    if (componetBaseList.Count == 0 || (componetBaseList.FirstOrDefault(t => t.PositionInfo == markToken) == null))
            //    {
            //        ComponentBase component = new ComponentBase();
            //        component.Type = deviceType;
            //        component.PositionInfo = markToken;
            //        component.Parameters = new List<MonitoringParameter>() { new MonitoringParameter() { Type = parameterType, Value = value, PositionInfo = positionInfo } };
            //        componetBaseList.Add(component);
            //    }
            //    else
            //    {
            //        resultComponet.Parameters.Add(new MonitoringParameter() { Type = parameterType, Value = value, PositionInfo = positionInfo });
            //    }
            //}
            #endregion



            if (_checkSystemUpdateTimer == null)
            {
                _checkSystemUpdateTimer = new Timer(CheckSystemUpdate, null, 700, 10 * 60 * 1000);
            }
        }

        private void UpdateLedConfigSyncCollection()
        {
            //if (string.IsNullOrEmpty(AppDataConfig.CurrentMAC))
            //{
            //    return;
            //}
            for (int screenIndex = 0; screenIndex < LedList.Count; screenIndex++)
            {
                string id = GetScreenId(AppDataConfig.CurrentMAC, LedList[screenIndex].SerialNumber);
                if (_syncInformationManager.ContainsScreen(id))
                {
                    continue;
                }
                else
                {
                    foreach (int type in Enum.GetValues(typeof(SyncType)))
                    {
                        var summary = new SyncSummary();
                        summary.SyncMark = "-1";
                        summary.Type = (SyncType)Enum.Parse(typeof(SyncType), type.ToString());
                        var syncData = new SyncData()
                        {
                            SyncType = (SyncType)Enum.Parse(typeof(SyncType), type.ToString()),
                            SyncParam = SyncFlag.Synchronized,
                            SyncContent = string.Empty,
                            Datestamp = "-1"
                        };
                        _syncInformationManager.AddSyncData(id, syncData);
                    }
                }
            }
        }

        private void UpdateLedConfigSyncData()
        {
            List<ConfigInfo> brightnessConfigInfoList = MonitorDataAccessor.Instance().GetLightProbeCfg(string.Empty);
            List<ConfigInfo> alarmConfigInfoList = MonitorDataAccessor.Instance().GetAlarmCfg(string.Empty);
            List<ConfigInfo> pointDetectConfigInfoList = MonitorDataAccessor.Instance().GetPeriodicInspectionCfg(string.Empty);

            for (int screenIndex = 0; screenIndex < LedList.Count; screenIndex++)
            {
                string id = GetScreenId(AppDataConfig.CurrentMAC, LedList[screenIndex].SerialNumber);

                if (!_syncInformationManager.ContainsScreen(id))
                {
                    _syncInformationManager.InitializeSyncDataBy(id);
                }

                if (brightnessConfigInfoList.Any(a => a.SN == LedList[screenIndex].SerialNumber))
                {
                    var cfg = brightnessConfigInfoList.Find(a => a.SN == LedList[screenIndex].SerialNumber);
                    var smartLightConfig = JsonConvert.DeserializeObject<SmartLightConfigInfo>(cfg.Content);
                    if (smartLightConfig != null)
                    {
                        IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();//这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
                        timeConverter.DateTimeFormat = "HH':'mm':'ss";
                        var brightnessConfig = new BrightnessConfig(smartLightConfig);
                        var brightnessJson = JsonConvert.SerializeObject(brightnessConfig, Newtonsoft.Json.Formatting.Indented, timeConverter);

                        _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "UpdateLedConfigSyncData", System.Threading.Thread.GetDomainID().ToString()));
                        _syncInformationManager.UpdateSyncData(
                           id,
                           new SyncData() { SyncType = SyncType.BrightnessRuleConfig, SyncParam = SyncFlag.Synchronized, SyncContent = brightnessJson, Datestamp = SystemHelper.GetUtcTicksByDateTime(cfg.Time).ToString() });
                    }
                }

                if (alarmConfigInfoList.Any(a => a.SN == LedList[screenIndex].SerialNumber))
                {
                    var cfg = alarmConfigInfoList.Find(a => a.SN == LedList[screenIndex].SerialNumber);
                    var alarmConfig = JsonConvert.DeserializeObject<LedAlarmConfig>(cfg.Content);
                    if (alarmConfig != null)
                    {
                        _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "UpdateLedConfigSyncData", System.Threading.Thread.GetDomainID().ToString()));
                        _syncInformationManager.UpdateSyncData(
                            id,
                            new SyncData() { SyncType = SyncType.AlarmConfig, SyncParam = SyncFlag.Synchronized, SyncContent = cfg.Content, Datestamp = SystemHelper.GetUtcTicksByDateTime(cfg.Time).ToString() });


                    }
                }

                if (pointDetectConfigInfoList.Any(a => a.SN == LedList[screenIndex].SerialNumber))
                {
                    var cfg = pointDetectConfigInfoList.Find(a => a.SN == LedList[screenIndex].SerialNumber);
                    var pointDetect = JsonConvert.DeserializeObject<DetectConfigParams>(cfg.Content);
                    if (pointDetect != null)
                    {
                        _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "UpdateLedConfigSyncData", System.Threading.Thread.GetDomainID().ToString()));
                        _syncInformationManager.UpdateSyncData(
                            id,
                            new SyncData() { SyncType = SyncType.PeriodicInspectionConfig, SyncParam = SyncFlag.Synchronized, SyncContent = cfg.Content, Datestamp = SystemHelper.GetUtcTicksByDateTime(cfg.Time).ToString() });

                    }
                }
            }
        }

        public string GetScreenId(string mac, string sn)
        {
            return mac + "+" + sn;
        }

        private void CheckSystemUpdate(object state)
        {
            try
            {
                CheckSystemUpdateRequest request = new CheckSystemUpdateRequest()
                {
                    mac = AppDataConfig.CurrentMAC,
                    SystemVersion = AppDataConfig.CurVersion
                };

                RestFulClient.Instance.Post("api/cmd/version_update", request, response =>
                {
                    ProcessCheckSystemUpdateResponse(response);
                });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "CheckSystemUpdate", ex.ToString()));
            }
        }

        private void ProcessCheckSystemUpdateResponse(IRestResponse response)
        {
            try
            {
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    //TODO:处理无更新命令的情况
                    if (response.Content == "[]") return;

                    var checkUpdateResponse = JsonConvert.DeserializeObject<Command>(response.Content);
                    if (checkUpdateResponse == null)
                        return;

                    Action action = new Action(() =>
                    {
                        Interlocked.Increment(ref isBlocked);
                        if (isBlocked == 1)
                        {
                            StartCommandProcess(new List<Command>() { checkUpdateResponse });
                        }
                    });
                    action.BeginInvoke(null, null);
                }
                else
                {
                    _logService.Error(string.Format("NoExistCatch:<-{0}->:{1}", "ProcessCheckSystemUpdateResponse", response.ResponseStatus));
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "ProcessCheckSystemUpdateResponse", ex.ToString()));
            }

        }

        public void SendCommand(Command command)
        {
            _dataClient.SendCommand(command);
        }

        public void RefreshLedBasicInfo()
        {
            Command command = new Command()
            {
                Code = CommandCode.RefreshLedScreenConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = "Get"
            };
            _dataClient.SendCommand(command);
        }

        public void RefreshOpticalProbeInfo()
        {
            Command command = new Command()
            {
                Code = CommandCode.RefreshOpticalProbeInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = string.Empty
            };
            _dataClient.SendCommand(command);
        }

        public void RefreshSmartLightConfigInfo()
        {
            Command command = new Command()
            {
                Code = CommandCode.RefreshSmartBrightEasyConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = string.Empty
            };
            _dataClient.SendCommand(command);
        }

        public void RefreshFunctionCardInfo()
        {
            Command command = new Command()
            {
                Code = CommandCode.RefreshFunctionCardInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = string.Empty
            };
            _dataClient.SendCommand(command);
        }

        public void RefreshMonitoringData()
        {
            Command command = new Command()
            {
                Code = CommandCode.RefreshMonitoringData,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = string.Empty
            };
            _dataClient.SendCommand(command);
        }

        public LedRegistationInfoResponse GetLedRegistationInfo(string mac, string sn = "")
        {
            LedRegistationInfoResponse registationInfo = null;
            LedRegistationInfoRequest infoRequest = new LedRegistationInfoRequest() { mac = mac, sn = sn };
            RestFulClient.Instance.Post("api/index/getScreen", infoRequest, response =>
            {
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    try
                    {
                        var registInfoResponse = JsonConvert.DeserializeObject<LedRegistationInfoResponse>(response.Content);
                        if (registInfoResponse == null)
                            return;

                        registationInfo = new LedRegistationInfoResponse()
                        {
                            IsRegisted = registInfoResponse.IsRegisted,
                            User = registInfoResponse.User,
                            Name = registInfoResponse.Name,
                            Mac = registInfoResponse.Mac,
                            SN = registInfoResponse.SN
                        };

                    }
                    catch (JsonSerializationException serializatinException)
                    {
                        _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "GetLedRegistationInfo", serializatinException.ToString()));
                    }
                    catch (Exception ex)
                    {
                        _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "GetLedRegistationInfo", ex.ToString()));
                    }
                }
                else
                {
                    _logService.Info(string.Format("ExistCatch:<-{0}->:{1}", "GetLedRegistationInfo", "请求未完成！"));
                }
            });
            return registationInfo;
        }



        public void GetLedRegistationInfo(string mac, Action<IRestResponse> resultAction, string sn = "")
        {
            // LedRegistationInfoResponse registationInfo = null;
            List<LedRegistationInfoRequest> infoRequest = new List<LedRegistationInfoRequest>() { new LedRegistationInfoRequest() { mac = mac, sn = sn } };
            Action action = new Action(() =>
            {
                RestFulClient.Instance.Post("api/index/getScreen", infoRequest, resultAction);
            });
            action.BeginInvoke(null, null);
        }

        private void RegistationInfoHandler(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                var obj = result.AsyncState;
            }
        }

        public ServerResponseCode RegistTo(string account, string server, List<LedRegistationInfo> registationInfos, bool isReregistering)
        {
            RestFulClient.Instance.Initialize(server);

            var registationRequest = new LedRegistationRequest()
            {
                Mac = AppDataConfig.CurrentMAC,
                User = account,
                IsReregistering = isReregistering,
                RegistationInfos = registationInfos
            };

            string response = RestFulClient.Instance.Post("api/index/register", registationRequest);
            try
            {
                var registationResponse = JsonConvert.DeserializeObject<LedRegistationResponse>(response);
                if (registationResponse.IsReregistering == 1)
                {
                    return ServerResponseCode.ScreenReregister;
                }
                else
                {
                    return (ServerResponseCode)registationResponse.Result;
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "RegistTo", ex.ToString()));
                return ServerResponseCode.ExceptionResult;
            }
        }

        /// <summary>
        /// 更新Led采集配置
        /// </summary>
        /// <param name="sn">Led标识号</param>
        /// <param name="acquisitionConfig">采集配置对象</param>
        /// <returns></returns>
        public bool UpdateLedAcquisitionConfig(string sn, LedAcquisitionConfig acquisitionConfig)
        {
            string serializeStr = JsonConvert.SerializeObject(acquisitionConfig);
            Command command = new Command()
            {
                Code = CommandCode.SetLedAcquisitionConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = serializeStr
            };

            _dataClient.SendCommand(command);

            return MonitorDataAccessor.Instance().UpdateSoftWareCfg(serializeStr, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
        }

        /// <summary>
        /// 更新Led采集配置
        /// </summary>
        /// <param name="acquisitionConfig">采集配置对象</param>
        /// <returns></returns>
        public bool UpdateLedAcquisitionConfig(LedAcquisitionConfig acquisitionConfig)
        {
            string serializeStr = JsonConvert.SerializeObject(acquisitionConfig);
            SetLedAcquisitionConfig(serializeStr);
            return SaveLedAcquisitionConfig(serializeStr);
        }

        /// <summary>
        /// 更新Led监控配置
        /// </summary>
        /// <param name="sn">Led标识号</param>
        /// <param name="monitoringConfig">监控配置对象</param>
        /// <returns></returns>
        public bool UpdateLedMonitoringConfig(string sn, LedMonitoringConfig monitoringConfig)
        {
            string serializeStr = JsonConvert.SerializeObject(monitoringConfig);
            SetLedMonitoringConfig(serializeStr);
            return SaveLedMonitoringConfig(sn, serializeStr);
        }

        public bool UpdateLedMonitoringConfig(string sn, string monitoringConfigXml)
        {
            SetLedMonitoringConfig(monitoringConfigXml);
            return SaveLedMonitoringConfig(sn, monitoringConfigXml);
        }

        public bool UpdateSmartBrightEasyConfig(string sn, SmartLightConfigInfo smartLightConfig)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "HH':'mm':'ss";
            //jsonObject是准备转换的对象
            string serializeStr = JsonConvert.SerializeObject(smartLightConfig, Newtonsoft.Json.Formatting.Indented, timeConverter);
            SetSmartLightConfigInfo(serializeStr);
            return SaveSmartLightConfigInfo(sn, serializeStr);
        }

        /// <summary>
        /// 更新Led告警配置
        /// </summary>
        /// <param name="sn">Led标识号</param>
        /// <param name="alarmConfig">Led告警配置对象</param>
        /// <returns></returns>
        public bool UpdateLedAlarmConfig(string sn, LedAlarmConfig alarmConfig)
        {
            string serializeStr = JsonConvert.SerializeObject(alarmConfig);
            SetLedAlarmConfig(serializeStr);
            return SaveLedAlarmConfig(alarmConfig.SN, serializeStr);
        }

        /// <summary>
        /// 设置点检周期
        /// </summary>
        /// <param name="configStr"></param>
        public void SetPeriodicInspectionConfig(string sn, string configStr)
        {
            Command command = new Command()
            {
                Code = CommandCode.SetPeriodicInspectionConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = configStr,
                Description = sn
            };
            _dataClient.SendCommand(command);
        }


        public bool UpdateStrategy(StrategyTable strategy)
        {
            string serializeStr = JsonConvert.SerializeObject(strategy);

            SetStrategy(serializeStr);

            return SaveStrategy(serializeStr);
        }

        public bool UpdatePointDeteConfig(string sn, DetectConfigParams detectParams)
        {
            Dictionary<string, DetectConfigParams> detectParamList = new Dictionary<string, DetectConfigParams>();
            detectParamList.Add(sn, detectParams);
            string serializeStr = JsonConvert.SerializeObject(detectParamList);
            SetPointDeteConfig(serializeStr);
            return SavePointDeteConfig(sn, JsonConvert.SerializeObject(detectParams));
        }
        public bool SaveSmartLightConfigInfo(string sn, SmartLightConfigInfo smartBrightEasyConfig)
        {
            string serializeStr = JsonConvert.SerializeObject(smartBrightEasyConfig);
            return SaveSmartLightConfigInfo(sn, serializeStr);
        }

        public bool SaveSmartLightConfigInfo(string sn, string smartBrightEasyConfigStr)
        {
            long timestamp = SystemHelper.GetUtcTicksByDateTime(DateTime.Now);
            bool result = MonitorDataAccessor.Instance().UpdateLightProbeCfg(sn, smartBrightEasyConfigStr, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            if (result)
            {
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();//这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
                timeConverter.DateTimeFormat = "HH':'mm':'ss";
                var smartLightConfigInfo = JsonConvert.DeserializeObject<SmartLightConfigInfo>(smartBrightEasyConfigStr);
                var brightnessConfig = new BrightnessConfig(smartLightConfigInfo);
                var brightnessJson = JsonConvert.SerializeObject(brightnessConfig, Newtonsoft.Json.Formatting.Indented, timeConverter);
                _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "SaveSmartLightConfigInfo", System.Threading.Thread.GetDomainID().ToString()));
                _syncInformationManager.UpdateSyncData(
                    GetScreenId(AppDataConfig.CurrentMAC, sn),
                    new SyncData() { SyncType = SyncType.BrightnessRuleConfig, SyncParam = SyncFlag.Synchronized, SyncContent = brightnessJson, Datestamp = timestamp.ToString() });
            }
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveSmartLightConfigInfo", result));
            return result;
        }

        public bool SaveSmartLightConfigInfo(string sn, string smartBrightEasyConfigStr, long timestamp)
        {

            bool result = MonitorDataAccessor.Instance().UpdateLightProbeCfg(sn, smartBrightEasyConfigStr, timestamp);
            //if (result)
            //{
            //    _syncInformationManager.UpdateSyncData(
            //        GetScreenId(AppDataConfig.CurrentMAC, sn),
            //        new SyncData() { SyncType = SyncType.BrightnessRuleConfig, SyncParam = SyncFlag.Synchronized, SyncContent = smartBrightEasyConfigStr, Datestamp = timestamp.ToString() });
            //}
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveSmartLightConfigInfo", result));
            return result;
        }

        public bool SaveFunctionCardPowerConfig(string configjson)
        {
            bool result = MonitorDataAccessor.Instance().UpdatePeripheralCfg(configjson, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveFunctionCardPowerConfig", result));
            return result;
        }

        public void SetLedMonitoringConfig(string configStr)
        {
            Command command = new Command()
            {
                Code = CommandCode.SetLedMonitoringConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = configStr
            };
            _dataClient.SendCommand(command);
        }

        public void SetSmartLightConfigInfo(string smartBrightEasyConfigStr)
        {
            Command command = new Command()
            {
                Code = CommandCode.SetSmartBrightEasyConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = smartBrightEasyConfigStr
            };
            _dataClient.SendCommand(command);
        }

        public void SetStrategy(string strategyTableStr)
        {
            Command command = new Command()
            {
                Code = CommandCode.UpdateStrategy,
                Source = "MonitorUIClient",
                Target = TargetType.ToRuleEngine,
                CommandText = strategyTableStr
            };
            _dataClient.SendCommand(command);
        }

        public void SetLedAlarmConfig(string alarmConfig)
        {
            Command command = new Command()
            {
                Code = CommandCode.SetLedAlarmConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToAll,
                CommandText = alarmConfig
            };
            _dataClient.SendCommand(command);
        }

        public void SetLedAcquisitionConfig(string config)
        {
            Command command = new Command()
            {
                Code = CommandCode.SetLedAcquisitionConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = config
            };

            _dataClient.SendCommand(command);
        }

        public void SetPointDeteConfig(string configStr)
        {
            Command command = new Command()
            {
                Code = CommandCode.SetSpotInspectionConfigInfo,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = configStr
            };
            _dataClient.SendCommand(command);
        }

        public void SetBrightnessConfigToLCT(string configStr)
        {
            Command command = new Command()
            {
                Code = CommandCode.BrightnessConfigToLCT,
                Source = "MonitorUIClient",
                Target = TargetType.ToClient,
                CommandText = configStr
            };
            _dataClient.SendCommand(command);
            _logService.Info("亮度配置命令已发送给LCT：" + CommandCode.BrightnessConfigToLCT);
        }

        public bool SaveStrategy(string strategyTableStr)
        {
            bool result = MonitorDataAccessor.Instance().UpdateStrategy(strategyTableStr, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveStrategy", result));
            return result;
        }
        public bool SavePointDeteConfig(string sn, string pointDetectCfgStr)
        {
            long timeTicks = SystemHelper.GetUtcTicksByDateTime(DateTime.Now);
            bool result = MonitorDataAccessor.Instance().UpdatePointDetectCfg(sn, pointDetectCfgStr, timeTicks);
            _logService.Debug(string.Format("<-{0}->:{1}", "SavePointDeteConfig", result));
            return result;
        }
        public bool SaveLedAlarmConfig(string sn, string alarmConfigStr)
        {

            return SaveLedAlarmConfig(sn, alarmConfigStr, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
        }

        public bool SaveLedAlarmConfig(string sn, string alarmConfigStr, long timeTicks)
        {
            bool result = MonitorDataAccessor.Instance().UpdateAlarmCfg(sn, alarmConfigStr, timeTicks);
            if (result)
            {
                _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "SaveLedAlarmConfig", System.Threading.Thread.GetDomainID().ToString()));
                _syncInformationManager.UpdateSyncData(
                    GetScreenId(AppDataConfig.CurrentMAC, sn),
                    new SyncData() { SyncType = SyncType.AlarmConfig, SyncParam = SyncFlag.Synchronized, SyncContent = alarmConfigStr, Datestamp = timeTicks.ToString() });
            }
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveLedAlarmConfig", result));
            return result;
        }

        public bool SaveUserConfig(UserConfig userConfig)
        {
            string serializeStr = JsonConvert.SerializeObject(userConfig);

            return SaveUserConfig(serializeStr);
        }

        public bool SaveUserConfig(string userConfigStr)
        {
            bool result = MonitorDataAccessor.Instance().UpdateUserCfg(userConfigStr, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveUserConfig", result));
            return result;
        }

        //public void UpdateSyncInfoTable(string sn, SyncType type, string timeTicks, string ConfigStr)
        //{
        //    if (_syncInfoTable.Keys.Contains(sn))
        //    {
        //        var ConfigSyncData = _syncInfoTable[sn].SyncDatas.FirstOrDefault(d => d.SyncType == type);
        //        if (ConfigSyncData != null)
        //        {
        //            _syncInfoTable[sn].SyncDatas.Remove(ConfigSyncData);
        //        }
        //        _syncInfoTable[sn].SyncDatas.Add(new SyncData()
        //        {
        //            SyncType = type,
        //            SyncParam = SyncFlag.Synchronized,
        //            SyncContent = ConfigStr,
        //            Datestamp = string.IsNullOrEmpty(ConfigStr) ? "-1" : timeTicks
        //        });
        //    }
        //    else
        //    {
        //        var ConfigSyncInfo = new SyncInformation()
        //        {
        //            Id = sn,
        //            SyncDatas = new List<SyncData>()
        //        {
        //            new SyncData()
        //            {
        //                 SyncType = type,
        //                  SyncParam =  SyncFlag.Synchronized,
        //                   SyncContent = ConfigStr,
        //                  Datestamp = timeTicks
        //            }
        //        }
        //        };
        //        _syncInfoTable.Add(sn, ConfigSyncInfo);
        //    }

        //}

        public bool SaveLedAcquisitionConfig(string acquisitionConfig)
        {
            bool result = MonitorDataAccessor.Instance().UpdateSoftWareCfg(acquisitionConfig, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveLedAcquisitionConfig", result));
            return result;
        }

        public bool SaveLedMonitoringConfig(string sn, string monitoringConfig)
        {
            bool result = MonitorDataAccessor.Instance().UpdateHardWareCfg(sn, monitoringConfig, SystemHelper.GetUtcTicksByDateTime(DateTime.Now));
            _logService.Debug(string.Format("<-{0}->:{1}", "SaveLedMonitoringConfig", result));
            return result;
        }

        public void FromCOMtoSN(string param)
        {
            Command command = new Command()
            {
                Code = CommandCode.SetFromCOMToSN,
                Source = "MonitorUIClient",
                Target = TargetType.ToDataSource,
                CommandText = param
            };
            _dataClient.SendCommand(command);
        }

        public void HeartbeatWithCareServer(object obj)
        {
            if (LedList == null)
                return;
            List<Led> ledList = new List<Led>();
            lock (LedList)
            {
                ledList = LedList.Select(t => (Led)t.Clone()).ToList();
            }
            isBlocked = 0;

            UpdateLedConfigSyncData();

            foreach (var led in ledList)
            {
                HeartbeatRequest request;
                string id = GetScreenId(AppDataConfig.CurrentMAC, led.SerialNumber);

                if (_syncInformationManager.ContainsScreen(id))
                {
                    var syncInfos = new List<SyncSummary>();
                    var currentSyncInformation = _syncInformationManager.GetSyncInformation(id);
                    foreach (var syncData in currentSyncInformation.SyncDatas)
                    {
                        var info = new SyncSummary();
                        info.SyncMark = syncData.Datestamp;
                        info.Type = syncData.SyncType;
                        syncInfos.Add(info);
                    }
                    request = new HeartbeatRequest()
                    {
                        Identifier = id,
                        SystemVersion = AppDataConfig.CurVersion,
                        Timestamp = SystemHelper.GetUtcTicksByDateTime(DateTime.Now).ToString(),
                        SyncInfos = syncInfos
                    };
                }
                else
                {
                    var syncInfos = new List<SyncSummary>();
                    foreach (int type in Enum.GetValues(typeof(SyncType)))
                    {
                        var info = new SyncSummary();
                        info.SyncMark = "-1";
                        info.Type = (SyncType)Enum.Parse(typeof(SyncType), type.ToString());
                        syncInfos.Add(info);
                        _syncInformationManager.AddSyncData(id, new SyncData() { SyncType = info.Type, SyncParam = SyncFlag.Synchronized, SyncContent = string.Empty, Datestamp = "-1" });
                    }
                    request = new HeartbeatRequest()
                    {
                        Identifier = id,
                        SystemVersion = AppDataConfig.CurVersion,
                        Timestamp = SystemHelper.GetUtcTicksByDateTime(DateTime.Now).ToString(),
                        SyncInfos = syncInfos
                    };
                }

                RestFulClient.Instance.Post("api/index/heartbeat", request, response =>
                {
                    HeartbeatResponse(response);
                });
            }

        }

        private static object _syncResponse = new object();
        int isBlocked = 0;
        private void HeartbeatResponse(IRestResponse response)
        {
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                try
                {
                    //lock (_syncResponse)
                    //{
                    var hearbeatResponse = JsonConvert.DeserializeObject<HeartbeatResponse>(response.Content);
                    if (hearbeatResponse == null)
                        return;

                    if (hearbeatResponse.SyncInfos != null
                        && hearbeatResponse.SyncInfos.Any(i => i.IsSync != SyncFlag.Synchronized))
                    {
                        Action action = new Action(() =>
                         {
                             StartSyncProcess(hearbeatResponse.sn, hearbeatResponse.SyncInfos);
                         });
                        action.BeginInvoke(null, null);
                    }

                    if (hearbeatResponse.CommandList != null && hearbeatResponse.CommandList.Count > 0)
                    {

                        Action action = new Action(() =>
                        {
                            Interlocked.Increment(ref isBlocked);
                            if (isBlocked == 1)
                            {
                                StartCommandProcess(hearbeatResponse.CommandList);
                            }
                        });
                        action.BeginInvoke(null, null);

                    }
                    //}

                }
                catch (System.Net.WebException ex)
                {
                    _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "HeartbeatResponse.Net", ex.ToString()));

                }
                catch (Exception ex)
                {
                    _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "HeartbeatResponse", ex.ToString()));
                }


                if (_serviceConnectionStatus == false)
                {
                    _serviceConnectionStatus = true;
                    if (CareServiceConnectionStatusChanged != null)
                    {
                        CareServiceConnectionStatusChanged(true);
                    }
                }
            }
            else
            {
                if (_serviceConnectionStatus == true)
                {
                    _serviceConnectionStatus = false;
                    if (CareServiceConnectionStatusChanged != null)
                    {
                        CareServiceConnectionStatusChanged(false);
                    }
                }
            }

        }

        private void StartCommandProcess(List<Command> list)
        {
            lock (_commandQueue)
            {
                foreach (var command in list)
                {
                    if (_commandQueue.FirstOrDefault(c => c.Code == command.Code && c.CommandText == command.CommandText) == null)
                    {
                        _commandQueue.Enqueue(command);
                    }
                }
                ProcessCommand();
            }

        }

        private void StartSyncProcess(string id, List<SyncSummaryResponse> list)
        {
            //lock (_syncObj)
            //{
            //    _isSynchronizing = true;
            //}
            _sem.Wait();
            var syncInfoRequest = new SyncInformation();
            syncInfoRequest.Sn = id;
            syncInfoRequest.SyncDatas = new List<SyncData>();
            var syncInfo = _syncInformationManager.GetSyncInformation(id);

            foreach (var item in list)
            {
                var syncData = syncInfo.SyncDatas.FirstOrDefault(d => d.SyncType == item.Type);
                if (syncData == null)
                    continue;

                if (item.IsSync == SyncFlag.CareNotSynchronized)
                {
                    syncInfoRequest.SyncDatas.Add(new SyncData()
                    {
                        SyncType = item.Type,
                        SyncParam = item.IsSync,
                        Datestamp = syncData.Datestamp,
                        SyncContent = syncData.SyncContent
                    });
                }
                else// if(item.IsSync == SyncFlag.TerminalNotSynchronized)
                {
                    syncInfoRequest.SyncDatas.Add(new SyncData()
                    {
                        SyncType = item.Type,
                        SyncParam = item.IsSync,
                        Datestamp = syncData.Datestamp,
                        SyncContent = string.Empty
                    });
                }
                //else
                //{
                //    continue;
                //}
            }

            RestFulClient.Instance.Post("api/SyncInfo/index", syncInfoRequest, response =>
            {
                SyncInfoResponse(response);
            });
        }

        private void SyncInfoResponse(IRestResponse response)
        {
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                try
                {
                    var syncInfoResponse = JsonConvert.DeserializeObject<SyncInformation>(response.Content);
                    if (syncInfoResponse == null || syncInfoResponse.SyncDatas == null)
                        return;
                    var currentSyncInfo = _syncInformationManager.GetSyncInformation(syncInfoResponse.Sn);

                    foreach (var syncInfo in syncInfoResponse.SyncDatas)
                    {
                        if (syncInfo.SyncParam == SyncFlag.TerminalNotSynchronized)
                        {
                            _logService.Debug(string.Format("<-{0}|{1}->:{2}", "SyncInfoResponse", SyncFlag.TerminalNotSynchronized, syncInfo.SyncContent));
                            var currentSyncData = currentSyncInfo.SyncDatas.FirstOrDefault(d => d.SyncType == syncInfo.SyncType);
                            if (currentSyncData != null)
                            {
                                currentSyncInfo.SyncDatas.Remove(currentSyncData);
                            }
                            currentSyncInfo.SyncDatas.Add(syncInfo);
                            if (syncInfo.SyncType == SyncType.AlarmConfig)
                            {
                                SaveLedAlarmConfig(syncInfoResponse.Sn.Split(new char[] { '+' })[1], syncInfo.SyncContent, long.Parse(syncInfo.Datestamp));
                                var alarmConfig = JsonConvert.DeserializeObject<LedAlarmConfig>(syncInfo.SyncContent);
                                alarmConfig.SN = syncInfoResponse.Sn.Split(new char[] { '+' })[1];
                                if (AlarmConfigSynchronized != null)
                                {
                                    AlarmConfigSynchronized(this, new AlarmConfigSynchronizedEventArgs(alarmConfig));
                                }
                            }
                            else if (syncInfo.SyncType == SyncType.PeriodicInspectionConfig)
                            {
                                if (SavePeriodicInspectionConfig(syncInfoResponse.Sn.Split(new char[] { '+' })[1], syncInfo.SyncContent, long.Parse(syncInfo.Datestamp)))
                                {
                                    SetPeriodicInspectionConfig(syncInfoResponse.Sn.Split(new char[] { '+' })[1], syncInfo.SyncContent);
                                }
                            }
                            else if (syncInfo.SyncType == SyncType.BrightnessRuleConfig)
                            {
                                SmartLightConfigInfo smartLightConfigInfo = new SmartLightConfigInfo();
                                var brightnessRuleConfig = JsonConvert.DeserializeObject<BrightnessConfig>(syncInfo.SyncContent);
                                string sn = syncInfoResponse.Sn.Split(new char[] { '+' })[1];
                                List<ConfigInfo> configInfoList = MonitorDataAccessor.Instance().GetLightProbeCfg(sn);//读取当前库中的屏亮度配置

                                if (configInfoList.Count == 0)
                                {
                                    smartLightConfigInfo.ScreenSN = sn;
                                    smartLightConfigInfo.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
                                    smartLightConfigInfo.DispaySoftWareConfig.DisplayUDID = sn;
                                    smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting = new AutoBrightExtendData();

                                    smartLightConfigInfo.DisplayHardcareConfig = new DisplaySmartBrightEasyConfigBase();
                                    smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting = new AutoBrightExtendData();

                                    if (brightnessRuleConfig.AutoBrightSetting == null)
                                    {
                                        brightnessRuleConfig.AutoBrightSetting = new AutoBrightnessConfig();
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                    }
                                    else
                                    {
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        if (smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList != null)
                                        {
                                            smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList.Sort(CompareBrightnessByValue);
                                        }
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        if (smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList != null)
                                        {
                                            smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList.Sort(CompareBrightnessByValue);
                                        }
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                    }

                                    if (brightnessRuleConfig.OneDayConfigList == null)
                                    {
                                        brightnessRuleConfig.OneDayConfigList = new List<OneSmartBrightEasyConfig>();
                                        smartLightConfigInfo.DispaySoftWareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                        smartLightConfigInfo.DisplayHardcareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                    }
                                    else
                                    {
                                        brightnessRuleConfig.OneDayConfigList.Sort(CompareBrightnessByTime);
                                        smartLightConfigInfo.DispaySoftWareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                        smartLightConfigInfo.DisplayHardcareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                    }

                                }
                                else
                                {
                                    smartLightConfigInfo = JsonConvert.DeserializeObject<SmartLightConfigInfo>(configInfoList[0].Content);

                                    if (smartLightConfigInfo.DispaySoftWareConfig == null)
                                    {
                                        smartLightConfigInfo.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
                                    }
                                    if (smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting == null)
                                    {
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting = new AutoBrightExtendData();
                                    }
                                    if (smartLightConfigInfo.DisplayHardcareConfig == null)
                                    {
                                        smartLightConfigInfo.DisplayHardcareConfig = new DisplaySmartBrightEasyConfigBase();
                                    }
                                    if (smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting == null)
                                    {
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting = new AutoBrightExtendData();
                                    }

                                    if (brightnessRuleConfig.AutoBrightSetting == null)
                                    {
                                        brightnessRuleConfig.AutoBrightSetting = new AutoBrightnessConfig();
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                    }
                                    else
                                    {
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        if (smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList != null)
                                        {
                                            smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList.Sort(CompareBrightnessByValue);
                                        }
                                        smartLightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList = brightnessRuleConfig.AutoBrightSetting.AutoBrightMappingList;
                                        if (smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList != null)
                                        {
                                            smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList.Sort(CompareBrightnessByValue);
                                        }
                                        smartLightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo = brightnessRuleConfig.AutoBrightSetting.OpticalFailureInfo;
                                    }

                                    if (brightnessRuleConfig.OneDayConfigList == null)
                                    {
                                        brightnessRuleConfig.OneDayConfigList = new List<OneSmartBrightEasyConfig>();
                                        smartLightConfigInfo.DispaySoftWareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                        smartLightConfigInfo.DisplayHardcareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                    }
                                    else
                                    {
                                        brightnessRuleConfig.OneDayConfigList.Sort(CompareBrightnessByTime);
                                        smartLightConfigInfo.DispaySoftWareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                        smartLightConfigInfo.DisplayHardcareConfig.OneDayConfigList = brightnessRuleConfig.OneDayConfigList;
                                    }
                                    smartLightConfigInfo.DispaySoftWareConfig.DisplayUDID = sn;

                                }
                                smartLightConfigInfo.AdjustType = BrightAdjustType.Smart;
                                string configJson = JsonConvert.SerializeObject(smartLightConfigInfo);
                                if (SaveSmartLightConfigInfo(syncInfoResponse.Sn.Split(new char[] { '+' })[1], configJson, long.Parse(syncInfo.Datestamp)))
                                {
                                    _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "SyncInfoResponse", System.Threading.Thread.GetDomainID().ToString()));
                                    _syncInformationManager.UpdateSyncData(syncInfoResponse.Sn, syncInfo);
                                    SetSmartLightConfigInfo(configJson);
                                    var brightnessConfigList = new List<SmartLightConfigInfo>() { smartLightConfigInfo };
                                    string configListJson = JsonConvert.SerializeObject(brightnessConfigList);
                                    SetBrightnessConfigToLCT(configListJson);
                                    if (BrightnessConfigChanged != null)
                                    {
                                        BrightnessConfigChanged(this, new BrightnessConfigChangedEventArgs(sn, smartLightConfigInfo));
                                    }
                                }
                            }
                        }
                        else if (syncInfo.SyncParam == SyncFlag.CareNotSynchronized)
                        {
                            _logService.Debug(string.Format("<-{0}|{1}->:{2}", "SyncInfoResponse", SyncFlag.CareNotSynchronized, syncInfo.SyncContent));
                            var currentSyncData = currentSyncInfo.SyncDatas.FirstOrDefault(d => d.SyncType == syncInfo.SyncType);
                            RestFulClient.Instance.Post("api/SyncInfo/index", currentSyncData.SyncContent, null);
                        }
                    }

                }
                catch (System.Net.WebException ex)
                {
                    _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "SyncInfoResponse.Net", ex.ToString()));
                }
                catch (Exception ex)
                {
                    _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "SyncInfoResponse", ex.ToString()));
                }
                finally
                {
                    _sem.Release();
                }
                _logService.Debug(string.Format("ExistCatch:<-{0}->:{1}", "SyncInfoResponse", "自动同步已完成"));
            }
            else
            {
                _logService.Error(string.Format("<-{0}->:{1}", "SyncInfoResponse.else", response.Content));
                _sem.Release();
            }
        }

        private int CompareBrightnessByTime(OneSmartBrightEasyConfig x, OneSmartBrightEasyConfig y)
        {
            if (x.StartTime == null)
            {
                if (y.StartTime == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y.StartTime == null)
                {
                    return 1;
                }
                else
                {
                    //int retval = x.Time.CompareTo(y.Time);
                    if (x.StartTime.Hour - y.StartTime.Hour > 0)
                    {
                        return 1;
                    }
                    else if (x.StartTime.Hour - y.StartTime.Hour < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        if (x.StartTime.Minute - y.StartTime.Minute > 0)
                        {
                            return 1;
                        }
                        else if (x.StartTime.Minute - y.StartTime.Minute < 0)
                        {
                            return -1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        private int CompareBrightnessByValue(DisplayAutoBrightMapping x, DisplayAutoBrightMapping y)
        {
            if (x.EnvironmentBright == null)
            {
                if (y.EnvironmentBright == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y.EnvironmentBright == null)
                {
                    return 1;
                }
                else
                {
                    int retval = x.EnvironmentBright.CompareTo(y.EnvironmentBright);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return x.EnvironmentBright.CompareTo(y.EnvironmentBright);
                    }
                }
            }
        }

        private bool SavePeriodicInspectionConfig(string sn, string periodicInspectionConfigStr, long timeTicks)
        {
            bool result = MonitorDataAccessor.Instance().UpdatePeriodicInspectionCfg(sn, periodicInspectionConfigStr, timeTicks);
            if (result)
            {
                _logService.Debug(string.Format("<-{0}->:ThreadID={1}", "SavePeriodicInspectionConfig", System.Threading.Thread.GetDomainID().ToString()));
                _syncInformationManager.UpdateSyncData(
                   GetScreenId(AppDataConfig.CurrentMAC, sn),
                   new SyncData() { SyncType = SyncType.PeriodicInspectionConfig, SyncParam = SyncFlag.Synchronized, SyncContent = periodicInspectionConfigStr, Datestamp = timeTicks.ToString() });
            }
            _logService.Debug(string.Format("<-{0}->:{1}", "SavePeriodicInspectionConfig", result));
            return result;
        }

        private void ProcessCommand()
        {
            if (_commandQueue.Count > 1)
            {

            }
            while (_commandQueue.Count > 0)
            {
                _currentCommand = _commandQueue.Dequeue();
                _logService.Debug(string.Format("<-{0}->:{1}", "ProcessCommand", _currentCommand.Code + "|" + _currentCommand.CommandText));
                _dataClient.SendCommand(_currentCommand);
            }
        }


        public void Dispose()
        {
            if (_dataClient != null)
            {
                _dataClient.CommandReceived -= DataClient_CommandReceived;
                _dataClient.DataReceived -= DataClient_DataReceived;
            }
            // throw new NotImplementedException();
        }



        private static string GetCardNum(List<PartInfo> partsInfoList)
        {
            int senderCardNum = 0;
            int reciverCardNum = 0;
            int monitingCardNum = 0;
            foreach (var item in partsInfoList)
            {
                if (item.Type == DeviceType.ReceiverCard)
                {
                    reciverCardNum = item.Amount;
                }
                if (item.Type == DeviceType.SendCard)
                {
                    senderCardNum = item.Amount;
                }
                if (item.Type == DeviceType.MonitoringCard)
                {
                    monitingCardNum = item.Amount;
                }
            }
            return senderCardNum + "+" + reciverCardNum + "+" + monitingCardNum;
        }
    }

    public class StrategySynchronizedEventArgs : EventArgs
    {
        public StrategyTable Strategys { get; set; }
    }

    public class AlarmConfigSynchronizedEventArgs : EventArgs
    {
        public LedAlarmConfig AlarmConfig { get; set; }

        public AlarmConfigSynchronizedEventArgs(LedAlarmConfig config)
        {
            AlarmConfig = config;
        }
    }



    public class SyncInformation
    {
        public string Sn { get; set; }
        public List<SyncData> SyncDatas { get; set; }
        public SyncInformation()
        {
            SyncDatas = new List<SyncData>();
        }

        public SyncInformation(string id)
            : this()
        {
            Sn = id;
        }
    }

    public class SyncData
    {
        public SyncType SyncType { get; set; }
        public SyncFlag SyncParam { get; set; }
        public string SyncContent { get; set; }
        public string Datestamp { get; set; }
    }

    public class ScreenMacInfo
    {
        public string Sn { get; set; }
    }

    public class ScreenMacResponse
    {
        public string Sn { get; set; }
        public string Mac { get; set; }
    }
}
