using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nova.LCT.Message.Client;
using System.Diagnostics;
using Nova.LCT.GigabitSystem.CommonInfoAccessor;
using Nova.LCT.GigabitSystem.Monitor;
using Nova.LCT.GigabitSystem.Common;
using System.IO;
using Nova.LCT.GigabitSystem.UI;
using Nova.Runtime.Serialization;
using System.Drawing;
using Nova.Equipment.Protocol.TGProtocol;
using Nova.Message.Common;
using Nova.IO.Port;
using System.Windows;
using Nova.Monitoring.HardwareMonitorInterface;
using Nova.Monitoring.ColudSupport;
using GalaSoft.MvvmLight.Threading;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Xml.Serialization;
using Log4NetLibrary;
using Nova.Monitoring.Common;
using Nova.LCT.GigabitSystem.HWPointDetect;

namespace Nova.Monitoring.MarsMonitorDataReader
{
    public partial class MarsHWMonitorDataReader : IMonitorDataReader
    {
        FileLogService _fLogService = new FileLogService(typeof(MarsHWMonitorDataReader));
        #region 属性
        public HWSystemType SysType
        {
            get { return _sysType; }
        }
        private HWSystemType _sysType = HWSystemType.M3;
        #endregion

        public List<string> GetDeviceSysNumber()
        {
            return null;
        }

        public bool IsDeviceStatusOk(ref string statusInfo)
        {
            return true;
        }
        #region 自定义类型
        private class ReadHWDataParams
        {
            public CompletedMonitorCallback CallBack
            {
                get
                {
                    return _callBak;
                }
            }
            private CompletedMonitorCallback _callBak = null;

            public object UserToken
            {
                get
                {
                    return _userToken;
                }
            }
            private object _userToken = null;

            public ReadHWDataParams(CompletedMonitorCallback callBack, object userToken)
            {
                _callBak = callBack;
                _userToken = userToken;
            }
        }

        private struct OpreateCallBack
        {
            public object UserToKen { get; set; }
            public TransFerParamsDataHandler TransFerParamsCallBack { get; set; }
        }

        #endregion

        #region 字段
        private readonly string SERVER_NAME = "MarsServerProvider";
        private readonly string SERVER_FORM_NAME = "A7F89E4D-04F4-46a6-9754-A334B3E8FEE5";
        private readonly string SERVER_PATH = AppDomain.CurrentDomain.BaseDirectory + "..\\MarsServerProvider\\MarsServerProvider.exe";
        private ILCTServerBaseProxy _serverProxy = null;
        private AllCOMHWBaseInfo _allComBaseInfo = null;
        private Dictionary<string, bool> _screenPointDetectIdentify = new Dictionary<string, bool>();
        private AutoResetEvent _waitForInitCompletedMetux = new AutoResetEvent(false);
        private AutoResetEvent _waitForGetChipTypeCompletedMetux = new AutoResetEvent(false);
        private ReadSenderDviInfo _readDviInfo = null;
        private HWStatusMonitor _hwStatusMonitor = null;
        private SmartLightDataAccessor _smartLightAccessor = null;
        private FunctionCardOutDeviceMonitor _funcMonitor = null;
        private ReadHWDataParams _readHWParams = null;
        private AllMonitorData _allMonitorData = null;
        private SmartBright _smartBright = null;
        //private string CONFIG_FOLDER_NAME = @"MonitorConfig\";
        private string CONFIG_SCREEN_NAME = @"ScreenInfo\";
        private bool _isNeedUpdateCfgFile = false;
        private bool _isNeedNotifyScreenCfgChanged = false;
        private object _notifyLocker = new object();
        //private MonitorSysData _monitorSysData = null;
        private Dictionary<string, MarsHWConfig> _hwConfigs = null;
        private bool _isWrightDataOK = false;
        private int _isHwRunningMetux = 0;
        private Dictionary<string, int> _comSenderList = new Dictionary<string, int>();
        private Dictionary<string, OpreateCallBack> _dicCallBack = new Dictionary<string, OpreateCallBack>();
        private object _lockCallBack = new object();
        private System.Threading.Timer _reRegisterTimer = null;
        private System.Threading.Timer _reReadScreenTimer = null;
        private System.Diagnostics.Stopwatch _stopwatch = new Stopwatch();
        private int _readHWCount = 0;
        private object _readHWObjLock = new object();

        private bool _isStopExec = false;
        /// <summary>
        /// 0表征目前线程正处于idle状态，1表征处于running状态
        /// </summary>
        private int _isRunningMetux = 0;
        private int _interlocked = 1;
        #region 发送数据Tag
        private object _setOnePowerStatusObj = new object();
        private static string ClassName = "MonitorCtrlOperate";
        private static char SendInfoTagSeperate = '#';
        private static string SetPowerStatusTagStart = ClassName + SendInfoTagSeperate + "FuncCard_SetPowerPortCtrl_Start";
        private static string SetPowerStatusTagClose = ClassName + SendInfoTagSeperate + "FuncCard_SetPowerPortCtrl_Close";
        private bool _isInitialize = false;

        #region 对外使用数据
        private SerializableDictionary<string, List<SenderRedundancyInfo>> _reduInfoList = new SerializableDictionary<string, List<SenderRedundancyInfo>>();
        private List<SupperDisplay> _supportList = new List<SupperDisplay>();
        #endregion

        #region 重新读屏后的备份：为了初始化时赋值使用
        private AllCOMHWBaseInfo _allComBaseInfo_Bak = null;
        private SerializableDictionary<string, List<SenderRedundancyInfo>> _reduInfoList_Bak = new SerializableDictionary<string, List<SenderRedundancyInfo>>();
        #endregion
        #endregion
        #endregion

        #region 事件
        public event NotifyUpdateCfgFileResEventHandler NotifyUpdateCfgFileResEvent;
        private void OnNotifyUpdateCfgFileResEvent(object sender, UpdateCfgFileResEventArgs e)
        {
            if (NotifyUpdateCfgFileResEvent != null)
            {
                NotifyUpdateCfgFileResEvent(sender, e);
            }
        }

        public event EventHandler NotifyScreenCfgChangedEvent;
        private void OnNotifyScreenCfgChangedEvent(object sender, EventArgs e)
        {
            if (NotifyScreenCfgChangedEvent != null)
            {
                NotifyScreenCfgChangedEvent(sender, e);
            }
        }

        public event NotifySettingCompletedEventHandler NotifySettingCompletedEvent;
        private void OnNotifySettingCompletedEvent(object sender, NotifySettingCompletedEventArgs e)
        {
            if (NotifySettingCompletedEvent != null)
            {
                NotifySettingCompletedEvent(sender, e);
            }
        }
        public event NotifyRegisterErrEventHandler NotifyRegisterErrEvent;
        /// <summary>
        /// 响应服务退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNotifyRegisterErrEvent(object sender, NotifyRegisterErrorEventArgs e)
        {
            SendStatusMsg("NoScreenInfo");
            _reRegisterTimer.Change(5000, 5000);
            if (_allComBaseInfo_Bak != null && _allComBaseInfo_Bak.AllInfoDict != null)
            {
                _allComBaseInfo_Bak.AllInfoDict.Clear();
            }
            if (_allComBaseInfo != null && _allComBaseInfo.AllInfoDict != null)
            {
                _allComBaseInfo.AllInfoDict.Clear();
            }
            Interlocked.Exchange(ref _isRunningMetux, 0);
            if (NotifyRegisterErrEvent != null)
            {
                NotifyRegisterErrEvent(sender, e);
            }
        }
        public event DetectPointCompletedEventHandler DetectPointCompletedEvent;
        #endregion

        public MarsHWMonitorDataReader()
        {
            StartServer();
            _reRegisterTimer = new System.Threading.Timer(TimerRegisterToServerCallBack,
                    null, Timeout.Infinite, Timeout.Infinite);
            InitalizeServerProxy();
            RegisterToServer();
        }

        public void FirstInitialized()
        {
            _hwConfigs = new Dictionary<string, MarsHWConfig>();
            _sn_ScannerInfos = new List<SN_ScannerPropertyReader>();
            _sn_BrightSensors = new Dictionary<string, List<PeripheralsLocation>>();
            _autoReadResultDatas = new List<AutoReadResultData>();
            _autoTimer = new System.Threading.Timer(AutoTimerCallBack, null,
                System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            _reReadScreenTimer = new System.Threading.Timer(TimerReadScreenCallBack,
                    null, Timeout.Infinite, Timeout.Infinite);
            _fLogService.Debug("FirstInitialized OK，Start OnEquipmentChangeEvent");
            OnEquipmentChangeEvent(null, null);
        }

#if TestMode
        private void GetallComBaseInfos()
        {
            Action action = new Action(() =>
            {
                Thread.Sleep(10000);
                ReadallComBaseInfos();
            });
            action.BeginInvoke(null, null);
        }

        private void ReadallComBaseInfos()
        {
            Thread.Sleep(3000);
            AllCOMHWBaseInfo allInfo = new AllCOMHWBaseInfo();
            allInfo.AllInfoDict = new SerializableDictionary<string, OneCOMHWBaseInfo>();
            int createScreenCount = 3;
            for (int scrIndex = 0; scrIndex < createScreenCount; scrIndex++)
            {
                string scrUDID = "1306280000015920-" + scrIndex.ToString("X2");
                allInfo.AllInfoDict.Add(scrUDID, new OneCOMHWBaseInfo()
                {
                    DisplayResult = CommonInfoCompeleteResult.OK,
                    FirstSenderSN = "1306280000015920",
                    GraphicsDviInfo = new GraphicsDVIPortInfo() { DviPortCols = 2, DviPortRows = 2, GraphicsHeight = 128, GraphicsWidth = 128 },
                    LEDDisplayInfoList = new List<ILEDDisplayInfo>()
                    { new StandardLEDDisplayInfo()
                    { ScanBoardCols=2, ScanBoardRows=3, ScannerRegionList=new List<ScanBoardRegionInfo>()
                    { new ScanBoardRegionInfo(){ ConnectIndex=0, Height=128, PortIndex=0, SenderIndex=0, Width=128, X=0, Y=0, XInPort=12, YInPort=12},
                        }}},
                    ReduInfoList = new List<SenderRedundancyInfo>(),
                    ReduResult = CommonInfoCompeleteResult.OK
                });
            }
            _allComBaseInfo_Bak = allInfo;
            _waitForInitCompletedMetux.Set();
        }

        private void MonitorDataCallBack(CompletedMonitorCallback callBack, object userToken)
        {
            CompletedMonitorCallbackParams comParams = GetCompletedMonitorCallbackParams();
            _readHWParams = new ReadHWDataParams(callBack, userToken);
            _readHWParams.CallBack.BeginInvoke(comParams, _readHWParams.UserToken, null, null);
            Thread.Sleep(7000);
            SendStatusMsg("Finish数据Data");
        }
        private CompletedMonitorCallbackParams GetCompletedMonitorCallbackParams()
        {
            CompletedMonitorCallbackParams param = new CompletedMonitorCallbackParams();

            AllMonitorData monitorData = new AllMonitorData();
            param.MonitorData = monitorData;

            int createScreenCount = 3;
            int senderCount = 2;
            int scannerCount = 2;
            int fansCount = 2;
            int powerCount = 2;
            int cableCount = 1;
            int functionCount = 2;
            int peripheralCount = 2;
            int comFunctionCount = 2;
            const int deconcentratorCount = 1;
            for (int scrIndex = 0; scrIndex < createScreenCount; scrIndex++)
            {
                string scrUDID = "1306280000015920-" + scrIndex.ToString("X2");
                ScreenModnitorData scrMonitorData = new ScreenModnitorData(scrUDID);

                for (int senderIndex = 0; senderIndex < senderCount; senderIndex++)
                {
        #region 添加发送卡数据
                    SendStatusMsg("StartReadDVIData" + senderIndex);
                    SenderMonitorInfo sendInfo = new SenderMonitorInfo();
                    sendInfo.DeviceStatus = DeviceWorkStatus.OK;
                    sendInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Sender, senderIndex));

                    sendInfo.IsDviConnected = true;
                    sendInfo.DviRate = 60;
                    sendInfo.ReduPortIndexCollection.Add(new PortOfSenderMonitorInfo());
                    sendInfo.ReduPortIndexCollection.Add(new PortOfSenderMonitorInfo());
                    scrMonitorData.SenderMonitorCollection.Add(sendInfo);
                    SendStatusMsg("FinishReadDVIData" + senderIndex);
        #endregion

        #region 分线器数据
                    scrMonitorData.DeconcentratorCollection = new List<DeconcentratorMonitorInfo>();
                    for (int deconcentratoIndex = 0; deconcentratoIndex < deconcentratorCount; deconcentratoIndex++)
                    {
                        SendStatusMsg("Start分线器数据Data" + deconcentratoIndex);
                        DeconcentratorMonitorInfo deconcentratorInfo = new DeconcentratorMonitorInfo();
                        deconcentratorInfo.DeviceStatus = DeviceWorkStatus.OK;
                        deconcentratorInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Sender, senderIndex));
                        if (deconcentratoIndex < 1)
                            deconcentratorInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 0));
                        else
                            deconcentratorInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 1));
                        scrMonitorData.DeconcentratorCollection.Add(deconcentratorInfo);
                        SendStatusMsg("End分线器数据Data" + deconcentratoIndex);
                    }
        #endregion
                    for (int scannerIndex = 0; scannerIndex < scannerCount; scannerIndex++)
                    {
        #region 添加接收卡数据
                        SendStatusMsg("Start接收卡数据Data" + scannerIndex);
                        ScannerMonitorInfo scannerInfo = new ScannerMonitorInfo();
                        scannerInfo.DeviceStatus = DeviceWorkStatus.OK;

                        scannerInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Sender, senderIndex));
                        if (scannerIndex < 50)
                        {
                            scannerInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 0));
                        }
                        else
                        {
                            scannerInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 1));
                        }
                        scannerInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Scanner, scannerIndex));

                        scannerInfo.Temperature = 25.5f;
                        scannerInfo.Voltage = 5.5f;
                        scrMonitorData.ScannerMonitorCollection.Add(scannerInfo);
                        SendStatusMsg("End接收卡数据Data" + scannerIndex);
        #endregion

        #region 添加监控卡数据
                        //MonitorCardMonitorInfo
                        SendStatusMsg("Start监控卡数据Data" + scannerIndex);
                        MonitorCardMonitorInfo monitorCardInfo = new MonitorCardMonitorInfo();
                        monitorCardInfo.DeviceStatus = DeviceWorkStatus.OK;

                        monitorCardInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Sender, senderIndex));
                        if (scannerIndex < 50)
                        {
                            monitorCardInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 0));
                        }
                        else
                        {
                            monitorCardInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 1));
                        }
                        monitorCardInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Scanner, scannerIndex));
                        monitorCardInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.MonitorCard, 0));

                        monitorCardInfo.TemperatureUInfo = new MCTemperatureUpdateInfo() { IsUpdate = true, Temperature = 20.5f };
                        monitorCardInfo.HumidityUInfo = new MCHumidityUpdateInfo() { IsUpdate = true, Humidity = 80.5f };
                        monitorCardInfo.SmokeUInfo = new MCSmokeUpdateInfo() { IsUpdate = true, IsSmokeAlarm = false };
                        monitorCardInfo.CabinetDoorUInfo = new MCDoorUpdateInfo() { IsUpdate = true, IsDoorOpen = true };

                        SendStatusMsg("End监控卡数据Data" + scannerIndex);
        #region 风扇数据
                        SendStatusMsg("Start风扇数据Data" + scannerIndex);
                        MCFansUpdateInfo mcFansUpdateInfo = new MCFansUpdateInfo();
                        mcFansUpdateInfo.IsUpdate = true;
                        mcFansUpdateInfo.FansMonitorInfoCollection = new SerializableDictionary<int, int>();
                        for (int fansIndex = 0; fansIndex < fansCount; fansIndex++)
                        {
                            int speed = 3000;
                            if (scannerIndex < 50)
                            {
                                speed = 3500;
                            }
                            mcFansUpdateInfo.FansMonitorInfoCollection.Add(fansIndex, speed);
                        }
                        monitorCardInfo.FansUInfo = mcFansUpdateInfo;
                        SendStatusMsg("End风扇数据Data" + scannerIndex);
        #endregion

        #region 电源数据
                        SendStatusMsg("Start电源数据Data" + scannerIndex);
                        MCPowerUpdateInfo mcPowerUInfo = new MCPowerUpdateInfo();
                        mcPowerUInfo.IsUpdate = true;
                        mcPowerUInfo.PowerMonitorInfoCollection = new SerializableDictionary<int,float>();
                        for (int pwoerIndex = 0; pwoerIndex < powerCount; pwoerIndex++)
                        {
                            float power = 5.0f;
                            if (scannerIndex < 50)
                            {
                                power = 4.5f;
                            }

                            mcPowerUInfo.PowerMonitorInfoCollection.Add(pwoerIndex, power);
                        }
                        monitorCardInfo.PowerUInfo = mcPowerUInfo;
                        SendStatusMsg("End电源数据Data" + scannerIndex);
        #endregion

        #region 排线数据
                        SendStatusMsg("Start排线数据Data" + scannerIndex);
                        List<SocketCableMonitorInfo> socketCableInfoCollection = new List<SocketCableMonitorInfo>();
                        for (int cableIndex = 0; cableIndex < cableCount; cableIndex++)
                        {
                            SocketCableMonitorInfo cableInfo = new SocketCableMonitorInfo();
                            cableInfo.DeviceStatus = DeviceWorkStatus.OK;

                            cableInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Sender, senderIndex));
                            if (scannerIndex < 50)
                            {
                                cableInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 0));
                            }
                            else
                            {
                                cableInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 1));
                            }
                            cableInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Scanner, scannerIndex));
                            cableInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.MonitorCard, 0));
                            cableInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.SocketOfMonitorCard, cableIndex));
                            for (int groupIndex = 0; groupIndex < 8; groupIndex++)
                            {
                                List<SocketCableStatus> oneGroupStatusList = new List<SocketCableStatus>();
                                SocketCableStatus cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.ABCD_Signal;
                                cableStatus.IsCableOK = true;
                                oneGroupStatusList.Add(cableStatus);

                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.CTRL_Signal;
                                cableStatus.IsCableOK = true;
                                oneGroupStatusList.Add(cableStatus);



                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.DCLK_Signal;
                                cableStatus.IsCableOK = true;
                                oneGroupStatusList.Add(cableStatus);

                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.LAT_Signal;
                                cableStatus.IsCableOK = true;
                                oneGroupStatusList.Add(cableStatus);

                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.OE_Signal;
                                cableStatus.IsCableOK = false;
                                oneGroupStatusList.Add(cableStatus);

                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.Red_Signal;
                                cableStatus.IsCableOK = false;
                                oneGroupStatusList.Add(cableStatus);

                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.Green_Signal;
                                cableStatus.IsCableOK = false;
                                oneGroupStatusList.Add(cableStatus);

                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.Blue_Signal;
                                cableStatus.IsCableOK = false;
                                oneGroupStatusList.Add(cableStatus);

                                
                                cableStatus = new SocketCableStatus();
                                cableStatus.CableType = SocketCableType.VRed_Signal;
                                cableStatus.IsCableOK = false;
                                oneGroupStatusList.Add(cableStatus);

                                cableInfo.SocketCableInfoDict.Add(groupIndex, oneGroupStatusList);
                                
                            }
                            socketCableInfoCollection.Add(cableInfo);
                            monitorCardInfo.SocketCableUInfo = new MCSocketCableUpdateInfo() { IsUpdate = true, SocketCableInfoCollection = socketCableInfoCollection };
                        }
                        SendStatusMsg("End排线数据Data" + scannerIndex);
        #endregion

                        scrMonitorData.MonitorCardInfoCollection.Add(monitorCardInfo);
        #endregion
                    }

                    for (int functionIndex = 0; functionIndex < functionCount; functionIndex++)
                    {
                        SendStatusMsg("Start多功能卡数据Data" + functionIndex);
        #region 添加网口上的多功能卡
                        FunctionCardMonitorInfo functionInfo = new FunctionCardMonitorInfo();
                        functionInfo.PeripheralInfoDict = new SerializableDictionary<int, PeripheralMonitorBaseInfo>();
                        functionInfo.DeviceStatus = DeviceWorkStatus.OK;

                        functionInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.Sender, senderIndex));
                        functionInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.PortOfSender, 1));
                        functionInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.FunctionCard, functionIndex));

                        for (int sensorIndex = 0; sensorIndex < peripheralCount; sensorIndex++)
                        {
                            PeripheralMonitorBaseInfo sensorInfo;
                            if (sensorIndex <= 2)
                            {
                                sensorInfo = new LightSensorMonitorInfo() { DeviceStatus = DeviceWorkStatus.OK, Lux = 280 };
                            }
                            else
                            {
                                sensorInfo = new TemperatureSensorMonitorInfo() { DeviceStatus = DeviceWorkStatus.OK, Tempearture = 18.5f };
                            }
                            functionInfo.PeripheralInfoDict.Add(sensorIndex, sensorInfo);
                        }
                        SendStatusMsg("End多功能卡数据Data" + functionIndex);
                        scrMonitorData.FunctionCardInfoCollection.Add(functionInfo);
        #endregion
                    }

                }
                monitorData.AllScreenMonitorCollection.Add(scrMonitorData);
            }

            //for (int comFuncIndex = 0; comFuncIndex < comFunctionCount; comFuncIndex++ )
            //{
            //    #region 添加串口上的多功能卡
            //    FunctionCardMonitorInfo functionInfo = new FunctionCardMonitorInfo();
            //    functionInfo.DeviceStatus = DeviceWorkStatus.OK;

            //    functionInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.FunctionCard, comFuncIndex));


            //    for (int sensorIndex = 0; sensorIndex < sensorCount; sensorIndex++)
            //    {
            //        LightSensorMonitorInfo sensorInfo = new LightSensorMonitorInfo();
            //        sensorInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.FunctionCard, comFuncIndex));
            //        sensorInfo.MappingList.Add(new DeviceSearchMapping(HWDeviceType.LightSensor, 0));

            //        sensorInfo.Birghtness = 2600;
            //        functionInfo.LightSensorInfoDict.Add(sensorIndex, sensorInfo);
            //    }
            //    #endregion
            //}

            return param;
        }
#endif

        #region IMonotorDataReader 成员
        private int _isRunning = 0;
        public InitialErryType Initialize()
        {
            _fLogService.Info("Mars开始硬件初始化...");
            lock (_lockCallBack)
            {
                _dicCallBack.Clear();
            }
            if (_allComBaseInfo_Bak == null)
            {
                _allComBaseInfo = new AllCOMHWBaseInfo();
                _allComBaseInfo.AllInfoDict = new SerializableDictionary<string, OneCOMHWBaseInfo>();
                OnNotifyExecResEvent(
                    TransferType.M3_UpdateLedScreenConfigInfo, string.Empty,
                    UpdateCfgFileResType.OK);
                return InitialErryType.NoServer;
            }
            else
            {
                if (_allComBaseInfo_Bak.AllInfoDict == null || _allComBaseInfo_Bak.AllInfoDict.Count == 0)
                {
                    _allComBaseInfo = new AllCOMHWBaseInfo();
                    _allComBaseInfo.AllInfoDict = new SerializableDictionary<string, OneCOMHWBaseInfo>();
                    OnNotifyExecResEvent(
                        TransferType.M3_UpdateLedScreenConfigInfo, string.Empty,
                        UpdateCfgFileResType.OK);
                    return InitialErryType.GetBaseInfoErr;
                }
            }
            _hwConfigs = new Dictionary<string, MarsHWConfig>();
            #region 初始化屏体等相关信息
            InitializeScreen(_allComBaseInfo_Bak);
            #endregion
            #region 初始化发送卡数据读取对象
            _readDviInfo = new ReadSenderDviInfo(_serverProxy);
            _readDviInfo.CompleteRefreshDviInfoEvent += new CompleteRefreshSenderDviEventHandler(ReadDviInfo_CompleteRefreshDviInfoEvent);
            _readDviInfo.CompleteRetryRefreshDviInfoEvent += _readDviInfo_CompleteRetryRefreshDviInfoEvent;
            #endregion

            #region 初始化常规监控数据读取对象
            Dictionary<string, List<ILEDDisplayInfo>> displayInfoList = new Dictionary<string, List<ILEDDisplayInfo>>();
            foreach (string key in _allComBaseInfo.AllInfoDict.Keys)
            {
                displayInfoList.Add(key, _allComBaseInfo.AllInfoDict[key].LEDDisplayInfoList);
            }
            _hwStatusMonitor = new HWStatusMonitor(_serverProxy, displayInfoList, _reduInfoList);
            _hwStatusMonitor.Initialize();
            _hwStatusMonitor.IsCycleMonitor = false;
            _hwStatusMonitor.CompleteRefreshAllCommPortEvent += new CompleteRefreshAllCommPortEventHandler(HWStatusMonitor_CompleteRefreshAllCommPortEvent);
            _hwStatusMonitor.BeginReadSBMonitorInfoEvent += _hwStatusMonitor_BeginReadSBMonitorInfoEvent;
            _hwStatusMonitor.CompleteReadSBMonitorInfoEvent += _hwStatusMonitor_CompleteReadSBMonitorInfoEvent;
            _hwStatusMonitor.CompleteRetryReadSBMonitorInfoEvent += new CompleteRefreshAllCommPortEventHandler(HWStatusMonitor_CompleteRetryReadSBMonitorInfoEvent);
            #endregion

            #region 硬件亮度配置
            _smartLightAccessor = new SmartLightDataAccessor(_serverProxy, string.Empty);
            #endregion

            #region 初始化多功能卡外设监控数据读取对象
            _funcMonitor = new FunctionCardOutDeviceMonitor(_serverProxy);
            #endregion
            _smartBright = new SmartBright(_serverProxy, _allComBaseInfo, _supportList);
            _smartBright.OutputLogEvent += _smartBright_OutputLogEvent;
            #region 初始化配置文件路径
            //InitConfigFileName();
            #endregion
            lock (_readHWObjLock)
            {
                _readHWCount = 0;
            }
            OnNotifyExecResEvent(
                        TransferType.M3_UpdateLedScreenConfigInfo, string.Empty,
                        UpdateCfgFileResType.OK);
            return InitialErryType.OK;
        }
        #region 点检芯片型号
        /// <summary>
        /// 读取芯片类型
        /// </summary>
        private ChipType ReadChipType(ILEDDisplayInfo ledDisplayInfo, string portName)
        {
            ChipType cType = ChipType.Unknown;
            if (_serverProxy == null || !_serverProxy.IsRegisted)
            {
                return cType;
            }
            ScanBoardRegionInfo scanBdInfo;
            ledDisplayInfo.GetFirstScanBoardInList(out scanBdInfo);
            if (scanBdInfo == null)
            {
                return cType;
            }
            PackageRequestReadData readPack = TGProtocolParser.ReadDriverType(portName,
                scanBdInfo.SenderIndex,
                scanBdInfo.PortIndex,
                scanBdInfo.ConnectIndex,
                CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                "ReadDriverType",
                null,
                (object sender, CompletePackageRequestEventArgs e) =>
                {
                    PackageBase readRequest = e.Request;
                    if (readRequest != null)
                    {
                        string strTag = readRequest.Tag;
                        if ((AckResults)readRequest.AckCode != AckResults.ok
                            || readRequest.CommResult != CommResults.ok
                            || readRequest.PackResult != PackageResults.ok)
                        {
                            if (strTag == "ReadDriverType")
                            {
                                cType = ChipType.Unknown;
                            }
                        }
                        else
                        {
                            if (strTag == "ReadDriverType")
                            {
                                PackageRequestReadData readRquest = (PackageRequestReadData)readRequest;
                                int driverType;
                                bool res = TGProtocolParser.ParsDeviceType(readRquest, out driverType);
                                object chip = Enum.Parse(typeof(ChipType), driverType.ToString());
                                cType = (ChipType)chip;
                            }
                        }
                    }
                    _waitForGetChipTypeCompletedMetux.Set();
                });
            _serverProxy.SendRequestReadData(readPack);
            _waitForGetChipTypeCompletedMetux.WaitOne();
            return cType;
        }

        #endregion
        private void InitializeScreen(AllCOMHWBaseInfo allComBaseInfo)
        {
            _allComBaseInfo = allComBaseInfo;
            ClearScannerDic();
            if (allComBaseInfo == null || allComBaseInfo.AllInfoDict == null)
            {
                return;
            }
            string[] keys = new string[allComBaseInfo.AllInfoDict.Count];
            allComBaseInfo.AllInfoDict.Keys.CopyTo(keys, 0);
            for (int i = 0; i < keys.Length; i++)
            {
                if (allComBaseInfo.AllInfoDict[keys[i]].DisplayResult != CommonInfoCompeleteResult.OK)
                {
                    allComBaseInfo.AllInfoDict.Remove(keys[i]);
                }
            }
            _fLogService.Debug("InitializeScreen Finish Remove");
            _allMonitorData = new AllMonitorData();
            _comSenderList.Clear();
            _supportList.Clear();
            _reduInfoList.Clear();
            foreach (KeyValuePair<string, OneCOMHWBaseInfo> pair in allComBaseInfo.AllInfoDict)
            {
                string firstSenderSN = pair.Value.FirstSenderSN;
                string udid = "";
                int senderCount = -1;
                int tempCount = 0;
                for (int i = 0; i < pair.Value.LEDDisplayInfoList.Count; i++)
                {
                    List<OneScreenInSupperDisplay> list = new List<OneScreenInSupperDisplay>();
                    udid = GetScreenUdid(firstSenderSN, i);
                    ScreenModnitorData monitorData = new ScreenModnitorData(udid);
                    _allMonitorData.AllScreenMonitorCollection.Add(monitorData);
                    OneScreenInSupperDisplay oneDisplay = new OneScreenInSupperDisplay()
                    {
                        ScreenUDID = udid
                    };
                    list.Add(oneDisplay);

                    SupperDisplay supper = new SupperDisplay()
                    {
                        DisplayUDID = udid,
                        ScreenList = list
                    };
                    _supportList.Add(supper);

                    tempCount = GetCommportSenderCount(pair.Value.LEDDisplayInfoList[i]);
                    if (tempCount > senderCount)
                    {
                        senderCount = tempCount;
                    }
                    SetAutoReader(udid, pair.Key, pair.Value.LEDDisplayInfoList[i], pair.Value.ReduInfoList);
                }
                if (senderCount >= 0)
                {
                    _reduInfoList.Add(pair.Key, pair.Value.ReduInfoList);
                    _comSenderList.Add(pair.Key, senderCount + 1);
                }
            }
            _fLogService.Debug("InitializeScreen Finish _comSenderList");
            #region 获取点检标识
            if (Interlocked.Exchange(ref _isRunning, 1) == 0)
            {
                _screenPointDetectIdentify.Clear();
                string sn;
                ChipType cType;
                ChipInherentProperty chipInherentPro = new ChipInherentProperty();
                foreach (var item in allComBaseInfo.AllInfoDict)
                {
                    for (int i = 0; i < item.Value.LEDDisplayInfoList.Count; i++)
                    {
                        sn = GetScreenUdid(item.Value.FirstSenderSN, i);
                        if (_screenPointDetectIdentify.ContainsKey(sn))
                        {
                            continue;
                        }
                        cType = ReadChipType(item.Value.LEDDisplayInfoList[i], item.Key);
                        if (!chipInherentPro.ChipInherentPropertyDict.ContainsKey(cType))
                        {
                            _screenPointDetectIdentify.Add(sn, false);
                        }
                        else
                        {
                            _screenPointDetectIdentify.Add(sn, chipInherentPro.ChipInherentPropertyDict[cType].IsSupportPointDetect);
                        }
                    }
                }
                _isRunning = 0;
            }
            _fLogService.Debug("InitializeScreen Finish 点检");
            #endregion
        }

        void _hwStatusMonitor_CompleteReadSBMonitorInfoEvent(object sender, CompletePackageRequestEventArgs e)
        {
            SendStatusMsg(string.Format("CompleteReadSBData|{0}-{1}-{2}-{3}!",
                e.Request.PortName, e.Request.DesAddr + 1, e.Request.PortAddr + 1, e.Request.ScanBoardAddr + 1));
        }

        void _hwStatusMonitor_BeginReadSBMonitorInfoEvent(object sender, BeginPackageRequestEventArgs e)
        {
            SendStatusMsg(string.Format("BeginReadSBData|{0}-{1}-{2}-{3}!",
                e.Request.PortName, e.Request.DesAddr + 1,
                e.Request.PortAddr + 1, e.Request.ScanBoardAddr + 1));
        }

        public ReadMonitorDataErrType BeginReadData(CompletedMonitorCallback callBack, object userToken)
        {
            _fLogService.Info("start BeginReadData");
#if TestMode
            Action action = new Action(() =>
            {
                MonitorDataCallBack(callBack, userToken);
            });
            action.BeginInvoke(null, null);
            return ReadMonitorDataErrType.OK;
#endif
            SendStatusMsg("StartReadMonitorData");
            ReadMonitorDataErrType readErrType = IsExcute();
            if (readErrType != ReadMonitorDataErrType.OK)
            {
                return readErrType;
            }
            if (Interlocked.Exchange(ref _isRunningMetux, 1) == 0)
            {
                _readHWParams = new ReadHWDataParams(callBack, userToken);
                ClearMonitorData();
                _fLogService.Info("Start DVI Data");
                SendStatusMsg("StartReadDVIData");
                if (_readDviInfo != null)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _readDviInfo.RefreshSenderDviInfo();
                }
                else
                {
                    Interlocked.Exchange(ref _isRunningMetux, 0);
                    return ReadMonitorDataErrType.NoServerObj;
                }
                //#if TestMode
                //                ThreadPool.QueueUserWorkItem(StartReadHWMonitorData, parm);
                //#else
                //                throw new Exception("The method or operation is not implemented.");
                //#endif
                return ReadMonitorDataErrType.OK;
            }
            else
            {
                _fLogService.Info("ReadAll: Hardware Monitor is busy...");
                return ReadMonitorDataErrType.BusyWorking;
            }
        }
        public ReadMonitorDataErrType BeginRetryReadSenderData(SerializableDictionary<string, List<byte>> senderInfos, CompletedMonitorCallback callBack, object userToken)
        {
            _fLogService.Info("start BeginRetryReadSenderData");
            SendStatusMsg("StartRetryReadSenderData");
            ReadMonitorDataErrType readErrType = IsExcute();
            if (readErrType != ReadMonitorDataErrType.OK)
            {
                return readErrType;
            }
            if (Interlocked.Exchange(ref _isRunningMetux, 1) == 0)
            {
                _readHWParams = new ReadHWDataParams(callBack, userToken);
                ClearMonitorData();
                if (_readDviInfo != null)
                {
                    _stopwatch.Restart();
                    _readDviInfo.RefreshSenderDviInfo(senderInfos);
                }
                else
                {
                    Interlocked.Exchange(ref _isRunningMetux, 0);
                    return ReadMonitorDataErrType.NoServerObj;
                }
                //#if TestMode
                //                ThreadPool.QueueUserWorkItem(StartReadHWMonitorData, parm);
                //#else
                //                throw new Exception("The method or operation is not implemented.");
                //#endif
                return ReadMonitorDataErrType.OK;
            }
            else
            {
                _fLogService.Info("RetrySender: Hardware Monitor is busy...");
                return ReadMonitorDataErrType.BusyWorking;
            }
        }
        public ReadMonitorDataErrType BeginRetryReadScannerData(Dictionary<string, List<ScanBoardRegionInfo>> scanBordInfos, CompletedMonitorCallback callBack, object userToken)
        {
            _fLogService.Info("start BeginReadOneScannerData");
            SendStatusMsg("StartRetryReadScannerData");
            ReadMonitorDataErrType readErrType = IsExcute();
            if (readErrType != ReadMonitorDataErrType.OK)
            {
                return readErrType;
            }
            if (Interlocked.Exchange(ref _isRunningMetux, 1) == 0)
            {
                _readHWParams = new ReadHWDataParams(callBack, userToken);
                ClearMonitorData();
                if (_hwStatusMonitor != null)
                {
                    _stopwatch.Restart();
                    if (!_hwStatusMonitor.ManualRefreshStatus(scanBordInfos))
                    {
                        Interlocked.Exchange(ref _isRunningMetux, 0);
                        _fLogService.Debug("NoWait：Read Scanner, there is no place to wait");
                        return ReadMonitorDataErrType.BusyWorking;
                    }
                }
                else
                {
                    Interlocked.Exchange(ref _isRunningMetux, 0);
                    return ReadMonitorDataErrType.NoServerObj;
                }
                return ReadMonitorDataErrType.OK;
            }
            else
            {
                _fLogService.Info("RetryScanner：Read Hardware Monitor is busy...");
                return ReadMonitorDataErrType.BusyWorking;
            }
        }
        public void Unitialize()
        {
            //_waitForInitCompletedMetux.Set();
            _fLogService.Info("Release hardware object...");
            #region 释放发送卡数据读取对象
            if (_readDviInfo != null)
            {
                _readDviInfo.CompleteRefreshDviInfoEvent -= new CompleteRefreshSenderDviEventHandler(ReadDviInfo_CompleteRefreshDviInfoEvent);
                _readDviInfo.CompleteRetryRefreshDviInfoEvent -= _readDviInfo_CompleteRetryRefreshDviInfoEvent;
                _readDviInfo.Dispose();
                _readDviInfo = null;
            }
            #endregion

            #region 释放常规监控数据读取对象
            if (_hwStatusMonitor != null)
            {
                _hwStatusMonitor.CompleteRefreshAllCommPortEvent -= new CompleteRefreshAllCommPortEventHandler(HWStatusMonitor_CompleteRefreshAllCommPortEvent);
                _hwStatusMonitor.BeginReadSBMonitorInfoEvent -= _hwStatusMonitor_BeginReadSBMonitorInfoEvent;
                _hwStatusMonitor.CompleteReadSBMonitorInfoEvent -= _hwStatusMonitor_CompleteReadSBMonitorInfoEvent;
                _hwStatusMonitor.CompleteRetryReadSBMonitorInfoEvent -= HWStatusMonitor_CompleteRetryReadSBMonitorInfoEvent;
                _hwStatusMonitor.Dispose();
                _hwStatusMonitor = null;
            }
            #endregion

            #region 硬件亮度配置
            if (_smartBright != null)
            {
                _smartBright.OutputLogEvent -= _smartBright_OutputLogEvent;
                _smartBright.Dispose();
                _smartBright = null;
            }
            _smartLightAccessor = null;
            #endregion

            #region 释放多功能卡外设监控数据读取对象
            //多功能卡对象不包含要释放的资源，这里不需要处理
            _funcMonitor = null;
            #endregion

            Interlocked.Exchange(ref _isRunningMetux, 0);
        }
        public void UpdateConfigMessage(string commandText)
        {
            ThreadPool.QueueUserWorkItem(UpdateConfigFileAsy, commandText);
        }
        public List<ScreenInfo> GetAllScreenInfo()
        {
            _fLogService.Info("Mars Start Read Screen...");
            if (_serverProxy == null || !_serverProxy.IsRegisted)
            {
                _fLogService.Error("Server is null...");
                SendStatusMsg("NoScreenInfo");
                _reReadScreenTimer.Change(10000, 10000);
                return null;
            }

            List<ScreenInfo> scrInfoList = new List<ScreenInfo>();
            if (_allComBaseInfo == null || _allComBaseInfo.AllInfoDict == null)
            {
                _fLogService.Error("Get Screen null or Get Screen List null...");
                _reReadScreenTimer.Change(10000, 10000);
                return null;
            }
            else if (_allComBaseInfo.AllInfoDict.Count == 0)
            {
                _fLogService.Error("Get Screen Count 0");
                SendStatusMsg("NoScreenInfo");
                _reReadScreenTimer.Change(10000, 10000);
                return scrInfoList;
            }
            foreach (string comName in _allComBaseInfo.AllInfoDict.Keys)
            {
                OneCOMHWBaseInfo baseInfo = _allComBaseInfo.AllInfoDict[comName];
                int count = baseInfo.LEDDisplayInfoList.Count;
                _fLogService.Debug("得到的屏列表数据的个数：" + count);
                for (int i = 0; i < count; i++)
                {
                    ScreenInfo scrInfo = new ScreenInfo();
                    scrInfo.LedSN = GetScreenUdid(baseInfo.FirstSenderSN, i);
                    scrInfo.LedIndexOfCom = i;
                    scrInfo.Commport = comName;
                    ILEDDisplayInfo displayInfo = baseInfo.LEDDisplayInfoList[i];
                    System.Drawing.Size scrSize = displayInfo.GetScreenSize();
                    scrInfo.LedWidth = scrSize.Width;
                    scrInfo.LedHeight = scrSize.Height;
                    scrInfo.LedInfo = displayInfo;
                    scrInfo.DeconcentratorCount = 0;
                    scrInfo.FunctionCardCount = 0;
                    if (_screenPointDetectIdentify.ContainsKey(scrInfo.LedSN))
                    {
                        scrInfo.IsSupportPointDetect = _screenPointDetectIdentify[scrInfo.LedSN];
                    }
                    List<ScreenSenderAddrInfo> senderAddrList = null;
                    List<ScreenPortAddrInfo> senderPortAddrList = null;
                    List<ScanBoardRegionInfo> regionInfoList;
                    displayInfo.GetScreenPortAddrInfo(out senderPortAddrList);
                    if (senderPortAddrList != null)
                    {
                        foreach (var portAddr in senderPortAddrList)
                        {
                            regionInfoList = displayInfo.GetScannerOfSpecifyPort(portAddr.SenderIndex, portAddr.PortIndex);
                            if (regionInfoList != null)
                            {
                                foreach (var scanInfo in regionInfoList)
                                {
                                    if (scanInfo.ConnectIndex == 255)
                                    {
                                        continue;
                                    }
                                    scrInfo.PointCount += scanInfo.Width * scanInfo.Height;
                                }
                            }
                        }
                        if (displayInfo.VirtualMode == VirtualModeType.Led4Mode1 || displayInfo.VirtualMode == VirtualModeType.Led4Mode2)
                        {
                            scrInfo.PointCount *= 4;
                        }
                        else scrInfo.PointCount *= 3;
                    }
                    displayInfo.GetScreenSenderAddrInfo(out senderAddrList);
                    if (senderAddrList != null)
                    {
                        scrInfo.SenderCardCount = senderAddrList.Count;
                        int totalLoadCount = 0;
                        for (int scanIndex = 0; scanIndex < senderAddrList.Count; scanIndex++)
                        {
                            ScreenSenderAddrInfo addrInfo = senderAddrList[scanIndex];
                            totalLoadCount += addrInfo.LoadScannerCount;
                        }
                        scrInfo.ScannerCardCount = totalLoadCount;

                        scrInfo.MonitorCardCount = totalLoadCount;
                        //TODO:第二阶段加入监控卡的个数：if (IsHWScreenConfigExist(scrInfo.LedSN) && _hwConfigs[scrInfo.LedSN].IsUpdateMCStatus)
                        //{
                        //    scrInfo.MonitorCardCount = totalLoadCount;
                        //}
                        //else
                        //{
                        //    scrInfo.MonitorCardCount = 0;
                        //}
                    }
                    scrInfoList.Add(scrInfo);
                }
            }
            if (scrInfoList.Count == 0)
            {
                SendStatusMsg("NoScreenInfo");
                _reReadScreenTimer.Change(10000, 10000);
            }
            return scrInfoList;
        }
        public Dictionary<string, List<ILEDDisplayInfo>> GetAllCommPortLedDisplay()
        {
            if (_serverProxy == null || !_serverProxy.IsRegisted
                || _allComBaseInfo == null || _allComBaseInfo.AllInfoDict == null)
            {
                return null;
            }
            Dictionary<string, List<ILEDDisplayInfo>> allCommPortLedDisplayDic = new Dictionary<string, List<ILEDDisplayInfo>>();
            foreach (OneCOMHWBaseInfo baseInfo in _allComBaseInfo.AllInfoDict.Values)
            {
                int count = baseInfo.LEDDisplayInfoList.Count;
                for (int i = 0; i < count; i++)
                {
                    string sn = GetScreenUdid(baseInfo.FirstSenderSN, i);
                    if (!allCommPortLedDisplayDic.ContainsKey(sn))
                    {
                        allCommPortLedDisplayDic.Add(sn, new List<ILEDDisplayInfo>());
                    }
                    allCommPortLedDisplayDic[sn].Add(baseInfo.LEDDisplayInfoList[i]);
                }
            }

            return allCommPortLedDisplayDic;
        }
        public HWSettingResult SetScreenBright(string screenUDID, byte brightness)
        {
            if (_serverProxy == null || !_serverProxy.IsRegisted)
            {
                return HWSettingResult.NoServerObj;
            }

            if (!CheckHaveValidScreenInfo(_allComBaseInfo))
            {
                return HWSettingResult.NoScreenInfo;
            }

            ILEDDisplayInfo scrInfo;
            string comName = "";
            bool res = GetScreenInfoByUDID(screenUDID, out comName, out scrInfo);
            if (!res || scrInfo == null)
            {
                return HWSettingResult.NoScreenInfo;
            }
            SendBrightToHW(comName, scrInfo, (byte)(brightness * 255 / 100));
            SetIsEnableSmartBright(screenUDID, false);

            return HWSettingResult.OK;
        }

        #region 读取硬件带回调
        public void ExecuteCommandCallBack(TransferParams param, TransFerParamsDataHandler callback, object userToken)
        {
            string userParam = System.Guid.NewGuid().ToString();
            try
            {
                if (_serverProxy == null || !_serverProxy.IsRegisted)
                {
                    return;
                }
                lock (_lockCallBack)
                {
                    _dicCallBack.Add(userParam, new OpreateCallBack() { UserToKen = userToken, TransFerParamsCallBack = callback });
                }
                switch (param.TranType)
                {
                    case TransferType.M3_PeripheralsInfo:
                        _fLogService.Info("开始提供光探头。。。");
                        PeripheralsFinderAccessor peripherals = new PeripheralsFinderAccessor(_serverProxy);
                        peripherals.ReadAllPeripheralsOnSenderOrPortFuncCard(ReadPeripheralsResultCallback, userParam);
                        break;
                    case TransferType.M3_FunctionCardMonitor:
                        _fLogService.Info("开始提供多功能卡信息。。。");
                        FunctionCardFinderAccessor funCard = new FunctionCardFinderAccessor(_serverProxy);
                        funCard.ReadAllFunctionCardOnPort(ReadFunctionCardCallBack, userParam);
                        break;
                    case TransferType.M3_ReadSmartLightHWConfig:
                        _fLogService.Info("Read HW Bright Exec...");
                        if (!_smartLightAccessor.ReadHWSmartLightData(param.Content, 0, ReadSmartLightHWConfigCallBack, userParam))
                        {
                            ReadSmartLightHWConfigCallBack(new ReadSmartLightDataParams(), userParam);
                        }
                        _fLogService.Info("Read HW Bright Finish...");
                        break;
                    case TransferType.M3_BlackScreen:
                        //TODO:黑屏
                        string commPort1 = string.Empty;
                        ILEDDisplayInfo leds1 = null;
                        GetScreenInfoByUDID(param.Content, out commPort1, out leds1);
                        if (string.IsNullOrEmpty(commPort1) || leds1 == null)
                        {
                            TransFerDataHandlerCallback(TransferType.M3_BlackScreen, "false", userParam);
                            return;
                        }
                        SetBlackScreen(TransferType.M3_BlackScreen, commPort1, leds1, 0, userParam);
                        break;
                    case TransferType.M3_NormalScreen:
                        //TODO:正常显示
                        string commPort2 = string.Empty;
                        ILEDDisplayInfo leds2 = null;
                        GetScreenInfoByUDID(param.Content, out commPort1, out leds1);
                        if (string.IsNullOrEmpty(commPort1) || leds1 == null)
                        {
                            TransFerDataHandlerCallback(TransferType.M3_NormalScreen, "false", userParam);
                            return;
                        }
                        SetNormalScreen(TransferType.M3_NormalScreen, commPort2, leds2, 0, userParam);
                        break;
                    case TransferType.M3_ReadBrightness:
                        _fLogService.Info("读取亮度值。。。");
                        break;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    lock (_lockCallBack)
                    {
                        _dicCallBack.Remove(userParam);
                    }
                }
                catch (Exception exc)
                {
                    _fLogService.Error("ExistCatch:去除字典中的ID时出现异常:" + exc.ToString());
                }
                _fLogService.Error("ExistCatch:CommandBack Exception:" + ex.ToString());
            }
        }
        #endregion
        private object _detectPointUserToken;
        private static object detectPointObj = new object();
        private Dictionary<string, SpotInspectionResult> _spotInspectionReusltTable = new Dictionary<string, SpotInspectionResult>();
        public void DetectPoint(string comName, ILEDDisplayInfo ledDisplayInfo, DetectConfigParams detectParams, object usertoken)
        {
            lock (detectPointObj)
            {
                if (_serverProxy == null)
                {
                    _detectPointRes = new SpotInspectionResult();
                    _detectPointRes.Result = false;
                    if (DetectPointCompletedEvent != null)
                        DetectPointCompletedEvent(_detectPointRes, usertoken);
                    return;
                }
                var _detectPointPerformer = new DetectPointPerformer(AppDataConfig.RAMTable_PATH, usertoken as string);
                _detectPointPerformer.CompletedDetectOneScanBdEvent += detectPointPerformer_CompletedDetectOneScanBdEvent;
                _detectPointPerformer.CompletedDetectAllScanBdEvent += detectPointPerformer_CompletedDetectAllScanBdEvent;
                _detectPointPerformer.ServerProxy = _serverProxy;
                _detectPointPerformer.SelectedPortName = comName;

                _detectPointUserToken = usertoken;
                _detectPointRes = new SpotInspectionResult();
                _detectPointRes.Result = true;
                if (_spotInspectionReusltTable.ContainsKey(usertoken as string))
                {
                    _spotInspectionReusltTable[usertoken as string] = _detectPointRes;
                }
                else
                {
                    _spotInspectionReusltTable.Add(usertoken as string, _detectPointRes);
                }
                DetectConfigParams tmpDetect = null;
                if (ledDisplayInfo.VirtualMode == VirtualModeType.Led4Mode1 || ledDisplayInfo.VirtualMode == VirtualModeType.Led4Mode2) tmpDetect = new DetectConfigParams(detectParams.DriverChipType, true, detectParams.ThresholdGrade, detectParams.DetectType, detectParams.IsUseCurrentGain, detectParams.RedGain, detectParams.GreenGain, detectParams.BlueGain, detectParams.VRedGain);
                else tmpDetect = new DetectConfigParams(detectParams.DriverChipType, false, detectParams.ThresholdGrade, detectParams.DetectType, detectParams.IsUseCurrentGain, detectParams.RedGain, detectParams.GreenGain, detectParams.BlueGain, detectParams.VRedGain);
                DetectPointError res = _detectPointPerformer.DetectPoint(ledDisplayInfo, tmpDetect);
                if (res != DetectPointError.OK)
                {
                    _detectPointRes = new SpotInspectionResult();
                    _detectPointRes.Id = usertoken as string;
                    _detectPointRes.Result = false;
                    if (DetectPointCompletedEvent != null)
                        DetectPointCompletedEvent(_detectPointRes, usertoken);
                }
            }
        }
        #endregion

        #region 可供外部使用的
        public bool GetScreenInfoByUDID(string scrUDID, out string scrComName, out ILEDDisplayInfo scrInfo)
        {
            scrComName = "";
            scrInfo = null;
            if (_allComBaseInfo == null || _allComBaseInfo.AllInfoDict == null)
            {
                return false;
            }
            try
            {
                foreach (string comName in _allComBaseInfo.AllInfoDict.Keys)
                {
                    OneCOMHWBaseInfo baseInfo = _allComBaseInfo.AllInfoDict[comName];
                    //枚举每个显示屏
                    for (int i = 0; i < baseInfo.LEDDisplayInfoList.Count; i++)
                    {
                        ILEDDisplayInfo displayInfo = baseInfo.LEDDisplayInfoList[i];
                        string curID = GetScreenUdid(baseInfo.FirstSenderSN, i);
                        if (scrUDID == curID)
                        {
                            scrComName = comName;
                            scrInfo = displayInfo;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _fLogService.Error("ExistCatch：Get Screen ID Exception:" + ex.ToString());
                return false;
            }
        }
        #endregion

        #region 私有读取硬件带回调的回调函数
        private void ReadSmartLightHWConfigCallBack(ReadSmartLightDataParams smartParam, object userToken)
        {
            _fLogService.Info("Read HW Bright Callback Start...");
            lock (_lockCallBack)
            {
                if (smartParam != null && smartParam.DisplayConfigBase != null
                    && smartParam.DisplayConfigBase.AutoBrightSetting != null
                    && smartParam.DisplayConfigBase.AutoBrightSetting.UseLightSensorList != null
                    && (userToken != null && _dicCallBack.ContainsKey(userToken.ToString())))
                {
                    string[] strSN = _dicCallBack[userToken.ToString()].UserToKen.ToString().Split('|');
                    if (strSN.Length == 2)
                    {
                        foreach (PeripheralsLocation perip in smartParam.DisplayConfigBase.AutoBrightSetting.UseLightSensorList)
                        {
                            perip.CommPort = strSN[0];
                            perip.FirstSenderSN = strSN[1];
                        }
                    }
                }
            }
            _fLogService.Info("Read HW Bright Callback For Finish...");
            TransFerDataHandlerCallback(TransferType.M3_ReadSmartLightHWConfig,
                CommandTextParser.GetJsonSerialization<ReadSmartLightDataParams>(smartParam), userToken);
        }
        private void ReadPeripheralsResultCallback(PeripheralsLocateInfo periInfo, object userToken)
        {
            TransFerDataHandlerCallback(TransferType.M3_PeripheralsInfo,
                CommandTextParser.GetJsonSerialization<PeripheralsLocateInfo>(periInfo), userToken);
        }

        private void ReadFunctionCardCallBack(FunctionCardLocateInfo funInfo, object userToken)
        {
            TransFerDataHandlerCallback(TransferType.M3_FunctionCardMonitor,
                CommandTextParser.GetJsonSerialization<FunctionCardLocateInfo>(funInfo), userToken);
        }

        private void TransFerDataHandlerCallback(TransferType type, string content, object userToken)
        {
            _fLogService.Info("return type..." + type.ToString());
            TransferParams param = new TransferParams()
            {
                TranType = type,
                Content = content
            };
            lock (_lockCallBack)
            {
                if (userToken != null && _dicCallBack.ContainsKey(userToken.ToString()))
                {
                    _dicCallBack[userToken.ToString()].TransFerParamsCallBack(param, _dicCallBack[userToken.ToString()].UserToKen);
                    _dicCallBack.Remove(userToken.ToString());
                }
            }
        }
        #endregion

        #region 私有函数
        private void TimerRegisterToServerCallBack(object state)
        {
            if (Interlocked.Exchange(ref _interlocked, 0) == 1)
            {
                StartServer();
                InitalizeServerProxy();
                bool res = RegisterToServer();
                OnEquipmentChangeEvent(null, null);
                Interlocked.Exchange(ref _interlocked, 1);
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        private void StartServer()
        {
            if (File.Exists(SERVER_PATH))
            {
                Process[] processList = Process.GetProcessesByName(SERVER_NAME);
                if (processList == null || processList.Length == 0)
                {
                    _fLogService.Debug("Server Start...");
                    Process.Start(SERVER_PATH);
                    Thread.Sleep(3000);
                    _fLogService.Debug("Server Finish...");
                }
            }
        }

        #region 初始化服务对象
        /// <summary>
        /// 初始化服务对象
        /// </summary>
        private void InitalizeServerProxy()
        {
            //Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            DispatcherHelper.UIDispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (_serverProxy != null)
                    {
                        TerminateServerProxy();
                    }
                    #region 初始化服务
                    _serverProxy = new LCTServerMessageProxy();
                    _fLogService.Info("Mars start Initalize Server...");
                    _serverProxy.Initalize();
                    _fLogService.Info("Mars Initalize Server finish!");
                    _serverProxy.HandshakeServerProviderInterval = 10000;
                    _serverProxy.NotifyRegisterErrEvent -= OnNotifyRegisterErrEvent;
                    _serverProxy.NotifyRegisterErrEvent += OnNotifyRegisterErrEvent;
                    _serverProxy.CompleteConnectAllController -= OnCompleteConnectAllController;
                    _serverProxy.CompleteConnectAllController += OnCompleteConnectAllController;
                    _serverProxy.CompleteConnectEquipment -= OnCompleteConnectEquipment;
                    _serverProxy.CompleteConnectEquipment += OnCompleteConnectEquipment;
                    _serverProxy.EquipmentChangeEvent -= OnEquipmentChangeEvent;
                    _serverProxy.EquipmentChangeEvent += OnEquipmentChangeEvent;
                    #endregion
                }
                catch (Exception ex)
                {
                    _fLogService.Error("ExistCatch：Mars Initalize Server exception：" + ex.ToString());
                }
            }));
        }
        /// <summary>
        /// 销毁服务对象
        /// </summary>
        private void TerminateServerProxy()
        {
            //Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            DispatcherHelper.UIDispatcher.Invoke(new Action(() =>
            {
                if (_serverProxy == null)
                {
                    return;
                }
                try
                {
                    _serverProxy.NotifyRegisterErrEvent -= new NotifyRegisterErrEventHandler(OnNotifyRegisterErrEvent);
                    _serverProxy.CompleteConnectAllController -= new EventHandler(OnCompleteConnectAllController);
                    _serverProxy.CompleteConnectEquipment -= new ConnectEquipmentEventHandler(OnCompleteConnectEquipment);
                    _serverProxy.EquipmentChangeEvent -= new EventHandler(OnEquipmentChangeEvent);
                    _serverProxy.Terminate();
                    _serverProxy = null;
                }
                catch (Exception ex)
                {
                    _fLogService.Error("ExistCatch：TerminateServerProxy exception：" + ex.ToString());
                    _serverProxy = null;
                }
            }));
        }
        /// <summary>
        /// 向服务注册
        /// </summary>
        private bool RegisterToServer()
        {
            string serverVer = string.Empty;
            bool res = false;
            try
            {
                res = ((LCTServerMessageProxy)_serverProxy).Register(SERVER_FORM_NAME, out serverVer);
                if (res == false)// || string.IsNullOrEmpty(serverVer)
                {
                    _fLogService.Debug("连接服务结果首次失败了。");
                    res = ((LCTServerMessageProxy)_serverProxy).Register(SERVER_FORM_NAME, out serverVer);
                }
                string msg = "连接服务结果：" + res.ToString();
                _fLogService.Info(msg);
                if (res)
                {
                    msg = "连接到底层服务,服务版本:" + serverVer;
                    _fLogService.Debug(msg);
                    _reRegisterTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                }
                else
                {
                    _reRegisterTimer.Change(7000, 7000);
                }
                return res;
            }
            catch (Exception ex)
            {
                _fLogService.Error("ExistCatch：Register Server Exception:" + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 所有设备完成重新连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCompleteConnectAllController(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 设备变更事件的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEquipmentChangeEvent(object sender, EventArgs e)
        {
            if (_isStopExec)
            {
                _fLogService.Debug("Equipment has changed, but LCT donot Changed");
                return;
            }
            _fLogService.Debug("Equipment has changed, need to determine screen changes");

#if TestMode
            _isInitialize = true;
            GetallComBaseInfos();
            return;
#endif
            _isInitialize = true;
            LoadAllComBaseInfoFromHW();
        }
        /// <summary>
        /// 连接单个设备完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCompleteConnectEquipment(object sender, ConnectEquipmentEventArgs e)
        {

        }
        /// <summary>
        /// 从硬件获取基础配置信息
        /// </summary>
        /// <returns></returns>
        #endregion

        #region 点检
        private DetectPointPerformer _detectPointPerformer;
        private void InitialDetectPointObj()
        {
            _detectPointPerformer = new DetectPointPerformer(AppDataConfig.RAMTable_PATH);
            _detectPointPerformer.CompletedDetectOneScanBdEvent -= detectPointPerformer_CompletedDetectOneScanBdEvent;
            _detectPointPerformer.CompletedDetectAllScanBdEvent -= detectPointPerformer_CompletedDetectAllScanBdEvent;
            _detectPointPerformer.CompletedDetectOneScanBdEvent += detectPointPerformer_CompletedDetectOneScanBdEvent;
            _detectPointPerformer.CompletedDetectAllScanBdEvent += detectPointPerformer_CompletedDetectAllScanBdEvent;

        }
        private SpotInspectionResult _detectPointRes = new SpotInspectionResult();
        private void detectPointPerformer_CompletedDetectAllScanBdEvent(object sender, CompletedDetectAllScanBdEventArgs e)
        {
            DetectPointPerformer performer = sender as DetectPointPerformer;
            if (performer != null)
            {
                performer.CompletedDetectOneScanBdEvent -= detectPointPerformer_CompletedDetectOneScanBdEvent;
                performer.CompletedDetectAllScanBdEvent -= detectPointPerformer_CompletedDetectAllScanBdEvent;
                string sn = performer.Id;
                if (DetectPointCompletedEvent != null)
                    DetectPointCompletedEvent(_spotInspectionReusltTable[sn], sn);
            }
        }

        private void detectPointPerformer_CompletedDetectOneScanBdEvent(object sender, CompletedDetectOneScanBdEventArgs e)
        {
            DetectPointPerformer performer = sender as DetectPointPerformer;
            string sn = performer.Id;
# if test
                _spotInspectionReusltTable[sn].Result = true;
                _spotInspectionReusltTable[sn].IsSupportVirtualRed = false;
                SpotInspectionBox spotInspectionBox = new SpotInspectionBox()
                {
                    BoxTotalPoint = e.ScanBdInfo.Width * e.ScanBdInfo.Height * 3,
                    Position = e.ScanBdInfo.SenderIndex + "-" + e.ScanBdInfo.PortIndex + "-" + e.ScanBdInfo.ConnectIndex,
                    SpotInspectionUnitList = new List<SpotInspectionUnit>()// GetErrPointList(e)
                };
                _spotInspectionReusltTable[sn].TotalPoint += spotInspectionBox.BoxTotalPoint;
                _spotInspectionReusltTable[sn].UnitCount += (e.ScanBdProp.Width / e.ScanBdProp.StandardLedModuleProp.ModulePixelCols) *
                (e.ScanBdProp.Height / e.ScanBdProp.StandardLedModuleProp.ModulePixelRows);
                if (spotInspectionBox.SpotInspectionUnitList.Count != 0 && _spotInspectionReusltTable[sn].SpotInspectionBoxList.FindIndex(a => a.Position == spotInspectionBox.Position) < 0)
                    _spotInspectionReusltTable[sn].SpotInspectionBoxList.Add(spotInspectionBox);

#else
            if (e.IsDetectedSuccessful)
            {
                _spotInspectionReusltTable[sn].IsSupportVirtualRed = e.ErrorPointInfo.IsSupportRedB;
                SpotInspectionBox spotInspectionBox = new SpotInspectionBox()
                {
                    BoxTotalPoint = e.ErrorPointInfo.IsSupportRedB ? e.ScanBdInfo.Width * e.ScanBdInfo.Height * 4 : e.ScanBdInfo.Width * e.ScanBdInfo.Height * 3,
                    Position = e.ScanBdInfo.SenderIndex + "-" + e.ScanBdInfo.PortIndex + "-" + e.ScanBdInfo.ConnectIndex,
                    SpotInspectionUnitList = GetErrPointList(e)
                };
                _spotInspectionReusltTable[sn].TotalPoint += spotInspectionBox.BoxTotalPoint;
                _spotInspectionReusltTable[sn].UnitCount += (e.ScanBdProp.Width / e.ScanBdProp.StandardLedModuleProp.ModulePixelCols) *
                (e.ScanBdProp.Height / e.ScanBdProp.StandardLedModuleProp.ModulePixelRows);
                if (spotInspectionBox.SpotInspectionUnitList.Count != 0 && _spotInspectionReusltTable[sn].SpotInspectionBoxList.FindIndex(a => a.Position == spotInspectionBox.Position) < 0)
                    _spotInspectionReusltTable[sn].SpotInspectionBoxList.Add(spotInspectionBox);
            }
            else _spotInspectionReusltTable[sn].Result = false;
#endif
        }

        #region 计算单元板坏点数量
        private void GetErrPointInfo(CompletedDetectOneScanBdEventArgs e, out Dictionary<System.Drawing.Point, int> redACount, out Dictionary<System.Drawing.Point, int> greenCount, out Dictionary<System.Drawing.Point, int> blueCount, out Dictionary<System.Drawing.Point, int> redBCount, out Dictionary<System.Drawing.Point, int> allErrCount)
        {
            redACount = new Dictionary<System.Drawing.Point, int>();
            greenCount = new Dictionary<System.Drawing.Point, int>();
            blueCount = new Dictionary<System.Drawing.Point, int>();
            redBCount = new Dictionary<System.Drawing.Point, int>();
            allErrCount = new Dictionary<System.Drawing.Point, int>();
            if (e.ErrorPointInfo == null)
            {
                return;
            }
            int width = e.ScanBdProp.StandardLedModuleProp.ModulePixelCols, height = e.ScanBdProp.StandardLedModuleProp.ModulePixelRows;
            System.Drawing.Point p;
            if (e.ErrorPointInfo.ErrorRedAPointList != null)
            {
                foreach (var item in e.ErrorPointInfo.ErrorRedAPointList)
                {
                    p = new System.Drawing.Point(item.X / width, item.Y / height);
                    if (redACount.ContainsKey(p))
                    {
                        redACount[p] += 1;
                    }
                    else redACount.Add(p, 1);

                    if (allErrCount.ContainsKey(p))
                    {
                        allErrCount[p] += 1;
                    }
                    else allErrCount.Add(p, 1);
                }
            }
            if (e.ErrorPointInfo.ErrorGreenPointList != null)
            {
                foreach (var item in e.ErrorPointInfo.ErrorGreenPointList)
                {
                    p = new System.Drawing.Point(item.X / width, item.Y / height);
                    if (greenCount.ContainsKey(p))
                    {
                        greenCount[p] += 1;
                    }
                    else greenCount.Add(p, 1);
                    if (allErrCount.ContainsKey(p))
                    {
                        allErrCount[p] += 1;
                    }
                    else allErrCount.Add(p, 1);
                }
            }
            if (e.ErrorPointInfo.ErrorBluePointList != null)
            {
                foreach (var item in e.ErrorPointInfo.ErrorBluePointList)
                {
                    p = new System.Drawing.Point(item.X / width, item.Y / height);
                    if (blueCount.ContainsKey(p))
                    {
                        blueCount[p] += 1;
                    }
                    else blueCount.Add(p, 1);
                    if (allErrCount.ContainsKey(p))
                    {
                        allErrCount[p] += 1;
                    }
                    else allErrCount.Add(p, 1);
                }
            }
            if (e.ErrorPointInfo.IsSupportRedB)
            {
                foreach (var item in e.ErrorPointInfo.ErrorRedBPointList)
                {
                    p = new System.Drawing.Point(item.X / width, item.Y / height);
                    if (redBCount.ContainsKey(p))
                    {
                        redBCount[p] += 1;
                    }
                    else redBCount.Add(p, 1);
                    if (allErrCount.ContainsKey(p))
                    {
                        allErrCount[p] += 1;
                    }
                    else allErrCount.Add(p, 1);
                }
            }
        }
        private List<SpotInspectionUnit> GetErrPointList(CompletedDetectOneScanBdEventArgs e)
        {
            Dictionary<System.Drawing.Point, int> redACount;
            Dictionary<System.Drawing.Point, int> greenCount;
            Dictionary<System.Drawing.Point, int> blueCount;
            Dictionary<System.Drawing.Point, int> redBCount;
            Dictionary<System.Drawing.Point, int> allErrCount;
            GetErrPointInfo(e, out redACount, out greenCount, out blueCount, out redBCount, out allErrCount);
            List<SpotInspectionUnit> spotInspectionUnitList = new List<SpotInspectionUnit>();
            SpotInspectionUnit unit;
            int pointOfModule = e.ScanBdProp.StandardLedModuleProp.ModulePixelRows * e.ScanBdProp.StandardLedModuleProp.ModulePixelCols * (e.ErrorPointInfo.IsSupportRedB ? 4 : 3);
            foreach (var item in allErrCount)
            {
                unit = new SpotInspectionUnit();
                unit.UnitTotalPoint = pointOfModule;
                unit.AllErrorPointNumner = item.Value;
                if (redACount.ContainsKey(item.Key))
                    unit.RedPointErrorNumber = redACount[item.Key];
                if (greenCount.ContainsKey(item.Key))
                    unit.GreenPointErrorNumber = greenCount[item.Key];
                if (blueCount.ContainsKey(item.Key))
                    unit.BluePointErrorNumber = blueCount[item.Key];
                if (e.ErrorPointInfo.IsSupportRedB)
                {
                    if (redBCount.ContainsKey(item.Key))
                        unit.VirtualRedPointErrorNumber = redBCount[item.Key];
                }
                spotInspectionUnitList.Add(unit);
            }
            return spotInspectionUnitList;
        }
        #endregion
        #endregion

        #region 从硬件读取基本信息
        private HWSoftwareSpaceRes LoadAllComBaseInfoFromHW()
        {
            _reReadScreenTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            if (_serverProxy != null && !_serverProxy.IsRegisted)
            {
                RegisterToServer();
            }

            if (_serverProxy == null || _serverProxy.EquipmentTable == null || _serverProxy.EquipmentTable.Count == 0)
            {
                _fLogService.Error("服务的相关信息为空，导致无法读取软件空间");
                if (_allComBaseInfo_Bak != null && _allComBaseInfo_Bak.AllInfoDict != null)
                {
                    _allComBaseInfo_Bak.AllInfoDict.Clear();
                }
                OnNotifyScreenCfgChangedEvent(this, EventArgs.Empty);
                _reReadScreenTimer.Change(10000, 10000);
                return HWSoftwareSpaceRes.NoServerObject;
            }

            lock (_readHWObjLock)
            {
                _readHWCount++;
            }
            AllCOMHWBaseInfoAccessor accessor = new AllCOMHWBaseInfoAccessor(_serverProxy);
            CompleteReadAllComHWBaseInfoCallback callBack = new CompleteReadAllComHWBaseInfoCallback(ReadAllComBaseInfoCompleted);
            HWSoftwareSpaceRes res = accessor.ReadAllComHWBaseInfo(callBack, null);

            if (res != HWSoftwareSpaceRes.OK)
            {
                lock (_readHWObjLock)
                {
                    _readHWCount--;
                }
                if (_allComBaseInfo_Bak != null && _allComBaseInfo_Bak.AllInfoDict != null)
                {
                    _allComBaseInfo_Bak.AllInfoDict.Clear();
                }
                OnNotifyScreenCfgChangedEvent(this, EventArgs.Empty);
                _reReadScreenTimer.Change(10000, 10000);
                _fLogService.Debug("读取软件空间失败,因此无屏信息:" + res.ToString());
            }
            return res;
        }

        private void TimerReadScreenCallBack(object state)
        {
            _fLogService.Debug("没有读取到屏，因此Timer来读取...");
            LoadAllComBaseInfoFromHW();
        }
        /// <summary>
        /// 获取基础配置信息完成
        /// </summary>
        /// <param name="allBaseInfo"></param>
        /// <param name="userToken"></param>
        private void ReadAllComBaseInfoCompleted(CompleteReadAllComHWBaseInfoParams allBaseInfo, object userToken)
        {
            _allComBaseInfo_Bak = allBaseInfo.AllInfo;
            if (_allComBaseInfo_Bak == null || _allComBaseInfo_Bak.AllInfoDict == null || _allComBaseInfo_Bak.AllInfoDict.Count == 0)
            {
                _fLogService.Debug("读取软件空间回调结果为空,因此无屏信息");
                OnNotifyScreenCfgChangedEvent(this, EventArgs.Empty);
                _reReadScreenTimer.Change(10000, 10000);
                lock (_readHWObjLock)
                {
                    if (_readHWCount > 0)
                    {
                        _readHWCount--;
                    }
                }
                return;
            }
            else
            {
                _reReadScreenTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                _fLogService.Debug("读取软件空间回调结果的个数：" + _allComBaseInfo_Bak.AllInfoDict.Count);
            }
            lock (_readHWObjLock)
            {
                if (_readHWCount > 0)
                {
                    _readHWCount--;
                }
                if (_readHWCount == 0)
                {
                    CheckAndSetScreenChanged(allBaseInfo);
                }
                else
                {
                    _fLogService.Debug("由于后面又存在读取，因此本次的判断通知做废处理");
                }
            }
            //_waitForInitCompletedMetux.Set();
        }

        private int GetCommportSenderCount(ILEDDisplayInfo leds)
        {
            int count = 0;
            List<ScreenSenderAddrInfo> senders = null;
            leds.GetScreenSenderAddrInfo(out senders);
            if (senders == null)
            {
                return count;
            }
            foreach (ScreenSenderAddrInfo sender in senders)
            {
                if (count < sender.SenderIndex)
                {
                    count = sender.SenderIndex;
                }
            }
            return count;
        }
        #endregion

        #region 获取设备搜索路径
        private List<DeviceSearchMapping> GetPortOfSenderMapping(int screenIndex, int senderIndex, int portIndex)
        {
            List<DeviceSearchMapping> mapList = new List<DeviceSearchMapping>();
            DeviceSearchMapping map = null;
            map = new DeviceSearchMapping(HWDeviceType.Screen, screenIndex);
            mapList.Add(map);
            map = new DeviceSearchMapping(HWDeviceType.Sender, senderIndex);
            mapList.Add(map);
            map = new DeviceSearchMapping(HWDeviceType.PortOfSender, portIndex);
            mapList.Add(map);
            return mapList;
        }

        private List<DeviceSearchMapping> GetSenderSearchMapping(int screenIndex, int senderIndex)
        {
            List<DeviceSearchMapping> mapList = new List<DeviceSearchMapping>();
            DeviceSearchMapping map = new DeviceSearchMapping(HWDeviceType.Screen, screenIndex);
            mapList.Add(map);
            map = new DeviceSearchMapping(HWDeviceType.Sender, senderIndex);
            mapList.Add(map);
            return mapList;
        }

        private List<DeviceSearchMapping> GetScannerSearchMapping(int screenIndex, int senderIndex, int portIndex, int scannerIndex)
        {
            List<DeviceSearchMapping> mapList = new List<DeviceSearchMapping>();
            DeviceSearchMapping map = new DeviceSearchMapping(HWDeviceType.Screen, screenIndex);
            mapList.Add(map);
            map = new DeviceSearchMapping(HWDeviceType.Sender, senderIndex);
            mapList.Add(map);
            map = new DeviceSearchMapping(HWDeviceType.PortOfSender, portIndex);
            mapList.Add(map);
            map = new DeviceSearchMapping(HWDeviceType.Scanner, scannerIndex);
            mapList.Add(map);
            return mapList;
        }

        private List<DeviceSearchMapping> GetMonitorCardSearchMapping(int screenIndex, int senderIndex, int portIndex, int scannerIndex, int monitorCardIndex)
        {
            List<DeviceSearchMapping> mapList = GetScannerSearchMapping(screenIndex, senderIndex, portIndex, scannerIndex);

            DeviceSearchMapping map = new DeviceSearchMapping(HWDeviceType.MonitorCard, monitorCardIndex);

            mapList.Add(map);
            return mapList;
        }

        private List<DeviceSearchMapping> GetSocketCableStatus(int screenIndex, int senderIndex, int portIndex, int scannerIndex, int monitorCardIndex, int socketIndex)
        {
            List<DeviceSearchMapping> mapList = GetMonitorCardSearchMapping(screenIndex, senderIndex, portIndex, scannerIndex, monitorCardIndex);
            DeviceSearchMapping map = new DeviceSearchMapping(HWDeviceType.SocketOfMonitorCard, socketIndex);
            mapList.Add(map);

            return mapList;
        }
        #endregion

        #region 读取发送卡数据
        private SerializableDictionary<string, List<WorkStatusType>> GetWorkStatusList(SerializableDictionary<string, List<WorkStatusType>> readWorkStatus)
        {
            SerializableDictionary<string, List<WorkStatusType>> result = new SerializableDictionary<string, List<WorkStatusType>>();
            foreach (KeyValuePair<string, int> comSendCount in _comSenderList)
            {
                if (!result.ContainsKey(comSendCount.Key))
                {
                    result.Add(comSendCount.Key, new List<WorkStatusType>());
                }
                for (int i = 0; i < comSendCount.Value; i++)
                {
                    if (readWorkStatus.ContainsKey(comSendCount.Key))
                    {
                        if (i < readWorkStatus[comSendCount.Key].Count)
                        {
                            result[comSendCount.Key].Add(readWorkStatus[comSendCount.Key][i]);
                            continue;
                        }
                    }
                    result[comSendCount.Key].Add(WorkStatusType.SenderCardError);
                }
            }
            return result;
        }
        private void ReadDviInfo_CompleteRefreshDviInfoEvent(object sender, CompleteRefreshSenderDviEventArgs e)
        {
            _stopwatch.Stop();
            _fLogService.Info("读取DVI数据完成，开始解析..." + _stopwatch.ElapsedMilliseconds);
            _stopwatch.Start();
            SendStatusMsg("GetDVIDataFinish");
            #region 解析发送卡数据
            if (e.IsRefreshSuccess)
            {
                _allMonitorData.CurAllSenderStatusDic = GetWorkStatusList(e.StatusDic);
                _allMonitorData.CurAllSenderDVIDic = e.RefreshRateDic;
                _allMonitorData.RedundantStateType = e.RedundantState;
                _allMonitorData.TempRedundancyDict = new SerializableDictionary<string, List<SenderRedundancyInfo>>();
                foreach (KeyValuePair<string, List<SenderRedundancyInfo>> keyvalue in _reduInfoList)
                {
                    _allMonitorData.TempRedundancyDict.Add(keyvalue.Key, keyvalue.Value);
                }
                _allMonitorData.CommPortData = new SerializableDictionary<string, int>();
                EquipmentInfo equipInfo = null;
                int count = 0;
                foreach (string commPort in _serverProxy.EquipmentTable.Keys)
                {
                    equipInfo = _serverProxy.EquipmentTable[commPort];
                    if (equipInfo == null
                        || equipInfo.EquipTypeCount <= 0)
                    {
                        continue;
                    }
                    if (CustomTransform.IsSystemController(equipInfo.ModuleID))
                    {
                        count = CustomTransform.GetPortNumber(equipInfo.ModuleID);
                        _allMonitorData.CommPortData.Add(commPort, count);
                    }
                }

                //枚举每个使用到的串口
                foreach (string comName in _allComBaseInfo.AllInfoDict.Keys)
                {
                    OneCOMHWBaseInfo baseInfo = _allComBaseInfo.AllInfoDict[comName];
                    //枚举每个显示屏
                    for (int i = 0; i < baseInfo.LEDDisplayInfoList.Count; i++)
                    {
                        ILEDDisplayInfo displayInfo = baseInfo.LEDDisplayInfoList[i];
                        //获取显示屏用到的发送卡
                        List<ScreenSenderAddrInfo> scrSenderAddrList;
                        displayInfo.GetScreenSenderAddrInfo(out scrSenderAddrList);
                        //从所有硬件数据中找出当前显示屏的数据
                        ScreenModnitorData monitorData = FindScreenMonitorDataFromList(_allMonitorData.AllScreenMonitorCollection, GetScreenUdid(baseInfo.FirstSenderSN, i));

                        if (monitorData != null)
                        {
                            for (int j = 0; j < scrSenderAddrList.Count; j++)
                            {
                                #region 获取每个显示屏的每张发送卡的数据

                                int senderIndex = scrSenderAddrList[j].SenderIndex;
                                SenderMonitorInfo senderMInfo = new SenderMonitorInfo();
                                senderMInfo.DeviceStatus = DeviceWorkStatus.OK;

                                senderMInfo.DeviceRegInfo = new DeviceRegionInfo();
                                senderMInfo.DeviceRegInfo.CommPort = comName;
                                senderMInfo.DeviceRegInfo.SenderIndex = (byte)senderIndex;

                                #region 设置发送卡的路径
                                senderMInfo.MappingList = GetSenderSearchMapping(i, senderIndex);
                                #endregion

                                #region 设置发送卡DVI信息
                                if (e.RefreshRateDic.ContainsKey(comName))
                                {
                                    List<ValueInfo> senderRefInfo = e.RefreshRateDic[comName];
                                    if (senderIndex < senderRefInfo.Count)
                                    {
                                        senderMInfo.IsDviConnected = senderRefInfo[senderIndex].IsValid;
                                        senderMInfo.DviRate = (int)senderRefInfo[senderIndex].Value;
                                    }
                                    else
                                    {
                                        senderMInfo.DeviceStatus = DeviceWorkStatus.Error;
                                    }
                                }
                                else
                                {
                                    senderMInfo.DeviceStatus = DeviceWorkStatus.Error;
                                }
                                #endregion

                                #region 设置发送卡网口的状态（冗余）
                                if (e.RedundantState.ContainsKey(comName))
                                {
                                    Dictionary<int, SenderRedundantStateInfo> senderReduStateDict = e.RedundantState[comName];
                                    if (senderReduStateDict.ContainsKey(senderIndex))
                                    {
                                        SenderRedundantStateInfo senderReduInfo = senderReduStateDict[senderIndex];
                                        foreach (int portIndex in senderReduInfo.RedundantStateTypeList.Keys)
                                        {
                                            PortOfSenderMonitorInfo portOfSenderInfo = new PortOfSenderMonitorInfo();
                                            portOfSenderInfo.DeviceStatus = DeviceWorkStatus.OK;
                                            #region 设置网口的路径
                                            portOfSenderInfo.MappingList = GetPortOfSenderMapping(i, senderIndex, portIndex);

                                            #endregion
                                            if (senderReduInfo.RedundantStateTypeList[portIndex] == RedundantStateType.Error)
                                            {
                                                portOfSenderInfo.IsReduState = true;
                                            }
                                            else
                                            {
                                                portOfSenderInfo.IsReduState = false;
                                            }

                                            senderMInfo.ReduPortIndexCollection.Add(portOfSenderInfo);
                                        }
                                    }
                                }
                                #endregion
                                #endregion

                                monitorData.SenderMonitorCollection.Add(senderMInfo);
                            }
                        }
                    }
                }
            }
            #endregion
            _stopwatch.Stop();
            _fLogService.Info("解析DVI数据完成，开始读取监控常规数据..." + _stopwatch.ElapsedMilliseconds);
            SendStatusMsg("ParsingDVIDataFinish");
            _stopwatch.Reset();
            _stopwatch.Start();
            if (_hwStatusMonitor == null || !_hwStatusMonitor.ManualRefreshStatus())
            {
                _fLogService.Debug("ReadScannerError:没有读取数据，直接返回结果无接收卡数据");
                NotifyCompleteAllMonitor();
            }
            else
            {
                SendStatusMsg("StartReadScannerData");
            }
        }
        /// <summary>
        /// 指定发送卡数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _readDviInfo_CompleteRetryRefreshDviInfoEvent(object sender, CompleteRefreshSenderDviEventArgs e)
        {
            _fLogService.Info("读取DVI数据完成，开始解析...");
            SendStatusMsg("GetDVIDataFinish");
            #region 解析发送卡数据
            if (e.IsRefreshSuccess)
            {
                _allMonitorData.CurAllSenderStatusDic = GetWorkStatusList(e.StatusDic);
                _allMonitorData.CurAllSenderDVIDic = e.RefreshRateDic;
                _allMonitorData.RedundantStateType = e.RedundantState;
                _allMonitorData.TempRedundancyDict = new SerializableDictionary<string, List<SenderRedundancyInfo>>();
                foreach (KeyValuePair<string, List<SenderRedundancyInfo>> keyvalue in _reduInfoList)
                {
                    _allMonitorData.TempRedundancyDict.Add(keyvalue.Key, keyvalue.Value);
                }
                _allMonitorData.CommPortData = new SerializableDictionary<string, int>();
                EquipmentInfo equipInfo = null;
                int count = 0;
                foreach (string commPort in _serverProxy.EquipmentTable.Keys)
                {
                    equipInfo = _serverProxy.EquipmentTable[commPort];
                    if (equipInfo == null
                        || equipInfo.EquipTypeCount <= 0)
                    {
                        continue;
                    }
                    if (CustomTransform.IsSystemController(equipInfo.ModuleID))
                    {
                        count = CustomTransform.GetPortNumber(equipInfo.ModuleID);
                        _allMonitorData.CommPortData.Add(commPort, count);
                    }
                }

                //枚举每个使用到的串口
                foreach (string comName in _allComBaseInfo.AllInfoDict.Keys)
                {
                    OneCOMHWBaseInfo baseInfo = _allComBaseInfo.AllInfoDict[comName];
                    //枚举每个显示屏
                    for (int i = 0; i < baseInfo.LEDDisplayInfoList.Count; i++)
                    {
                        ILEDDisplayInfo displayInfo = baseInfo.LEDDisplayInfoList[i];
                        //获取显示屏用到的发送卡
                        List<ScreenSenderAddrInfo> scrSenderAddrList;
                        displayInfo.GetScreenSenderAddrInfo(out scrSenderAddrList);
                        //从所有硬件数据中找出当前显示屏的数据
                        ScreenModnitorData monitorData = FindScreenMonitorDataFromList(_allMonitorData.AllScreenMonitorCollection, GetScreenUdid(baseInfo.FirstSenderSN, i));

                        if (monitorData != null)
                        {
                            for (int j = 0; j < scrSenderAddrList.Count; j++)
                            {
                                #region 获取每个显示屏的每张发送卡的数据

                                int senderIndex = scrSenderAddrList[j].SenderIndex;
                                SenderMonitorInfo senderMInfo = new SenderMonitorInfo();
                                senderMInfo.DeviceStatus = DeviceWorkStatus.OK;

                                senderMInfo.DeviceRegInfo = new DeviceRegionInfo();
                                senderMInfo.DeviceRegInfo.CommPort = comName;
                                senderMInfo.DeviceRegInfo.SenderIndex = (byte)senderIndex;

                                #region 设置发送卡的路径
                                senderMInfo.MappingList = GetSenderSearchMapping(i, senderIndex);
                                #endregion

                                #region 设置发送卡DVI信息
                                if (e.RefreshRateDic.ContainsKey(comName))
                                {
                                    List<ValueInfo> senderRefInfo = e.RefreshRateDic[comName];
                                    if (senderIndex < senderRefInfo.Count)
                                    {
                                        senderMInfo.IsDviConnected = senderRefInfo[senderIndex].IsValid;
                                        senderMInfo.DviRate = (int)senderRefInfo[senderIndex].Value;
                                    }
                                    else
                                    {
                                        senderMInfo.DeviceStatus = DeviceWorkStatus.Error;
                                    }
                                }
                                else
                                {
                                    senderMInfo.DeviceStatus = DeviceWorkStatus.Error;
                                }
                                #endregion

                                #region 设置发送卡网口的状态（冗余）
                                if (e.RedundantState.ContainsKey(comName))
                                {
                                    Dictionary<int, SenderRedundantStateInfo> senderReduStateDict = e.RedundantState[comName];
                                    if (senderReduStateDict.ContainsKey(senderIndex))
                                    {
                                        SenderRedundantStateInfo senderReduInfo = senderReduStateDict[senderIndex];
                                        foreach (int portIndex in senderReduInfo.RedundantStateTypeList.Keys)
                                        {
                                            PortOfSenderMonitorInfo portOfSenderInfo = new PortOfSenderMonitorInfo();
                                            portOfSenderInfo.DeviceStatus = DeviceWorkStatus.OK;
                                            #region 设置网口的路径
                                            portOfSenderInfo.MappingList = GetPortOfSenderMapping(i, senderIndex, portIndex);

                                            #endregion
                                            if (senderReduInfo.RedundantStateTypeList[portIndex] == RedundantStateType.Error)
                                            {
                                                portOfSenderInfo.IsReduState = true;
                                            }
                                            else
                                            {
                                                portOfSenderInfo.IsReduState = false;
                                            }

                                            senderMInfo.ReduPortIndexCollection.Add(portOfSenderInfo);
                                        }
                                    }
                                }
                                #endregion
                                #endregion

                                monitorData.SenderMonitorCollection.Add(senderMInfo);
                            }
                        }
                    }
                }
            }
            SendStatusMsg("ParsingDVIDataFinish");
            NotifyCompleteAllMonitor();
            #endregion
        }
        #endregion

        #region 读取常规监控数据
        private void HWStatusMonitor_CompleteRefreshAllCommPortEvent(object sender, CompleteRefreshAllCommPortEventArgs e)
        {
            Action action = new Action(() =>
            {
                HWStatusMonitor_Exec_CompleteRefreshCommPortEvent(sender, e, false);
            });
            action.BeginInvoke(null, null);
        }

        #region 重读自定义常规监控数据机制
        private void HWStatusMonitor_CompleteRetryReadSBMonitorInfoEvent(object sender, CompleteRefreshAllCommPortEventArgs e)
        {
            Action action = new Action(() =>
            {
                HWStatusMonitor_Exec_CompleteRefreshCommPortEvent(sender, e, true);
            });
            action.BeginInvoke(null, null);
        }

        private void HWStatusMonitor_Exec_CompleteRefreshCommPortEvent(object sender, CompleteRefreshAllCommPortEventArgs e,bool isRetry)
        {
            _stopwatch.Stop();
            _fLogService.Info("读取监控常规数据完成，开始解析..." + _stopwatch.ElapsedMilliseconds);
            _stopwatch.Start();
            SendStatusMsg("GetScannerDataFinish");
            #region 监控数据处理
            if (_allComBaseInfo == null || _allComBaseInfo.AllInfoDict == null)
            {
                SendStatusMsg("ParsingScannerDataFinish");
                if (isRetry)
                {
                    _fLogService.Info("读取监控常规数据完成，重读通知...");
                    NotifyCompleteAllMonitor();
                }
                else
                {
                    _fLogService.Info("读取监控常规数据完成，开始读取多功能卡外设数据...");
                    _funcMonitor.ReadAllOutDevice(ReadAllOutDeviceCallback);
                }
                return;
            }
            foreach (string comName in _allComBaseInfo.AllInfoDict.Keys)
            {
                OneCOMHWBaseInfo baseInfo = _allComBaseInfo.AllInfoDict[comName];
                if (baseInfo == null || baseInfo.LEDDisplayInfoList == null)
                {
                    _fLogService.Debug("回调接收卡：得到串口的数据为什么会没有屏：" + comName);
                    continue;
                }
                //枚举每个显示屏
                for (int i = 0; i < baseInfo.LEDDisplayInfoList.Count; i++)
                {
                    string sn = GetScreenUdid(baseInfo.FirstSenderSN, i);
                    if (!_hwConfigs.ContainsKey(sn))
                    {
                        _hwConfigs.Add(sn, new MarsHWConfig());
                    }
                    //从所有硬件数据中找出当前显示屏的数据
                    ScreenModnitorData monitorData = FindScreenMonitorDataFromList(_allMonitorData.AllScreenMonitorCollection, sn);

                    ILEDDisplayInfo displayInfo = baseInfo.LEDDisplayInfoList[i];
                    int scnanerCount = displayInfo.ScannerCount;
                    for (int scannerOffset = 0; scannerOffset < scnanerCount; scannerOffset++)
                    {
                        ScanBoardRegionInfo regionInfo = displayInfo[scannerOffset];
                        if (regionInfo.SenderIndex == ConstValue.BLANK_SCANNER)
                        {
                            if (isRetry)
                            {
                                _allMonitorData.AllScreenMonitorCollection.Remove(monitorData);
                            }
                            continue;
                        }
                        int scanBoardCols = 0;
                        int scanBoardRows = 0;
                        bool isisComplex = false;
                        GetScanBoardInPosition(displayInfo, regionInfo, ref scanBoardCols, ref scanBoardRows, ref isisComplex);
                        SendStatusMsg("ParsingScannerData");
                        #region 接收卡监控数据
                        ScannerMonitorInfo scannerMInfo = new ScannerMonitorInfo();
                        scannerMInfo.DeviceRegInfo = new DeviceRegionInfo();
                        scannerMInfo.DeviceRegInfo.CommPort = comName;
                        scannerMInfo.DeviceRegInfo.SenderIndex = regionInfo.SenderIndex;
                        scannerMInfo.DeviceRegInfo.PortIndex = regionInfo.PortIndex;
                        scannerMInfo.DeviceRegInfo.ConnectIndex = regionInfo.ConnectIndex;

                        scannerMInfo.DeviceRegInfo.IsComplex = isisComplex;
                        scannerMInfo.DeviceRegInfo.ScanBoardCols = scanBoardCols;
                        scannerMInfo.DeviceRegInfo.ScanBoardRows = scanBoardRows;

                        scannerMInfo.DeviceStatus = DeviceWorkStatus.OK;
                        scannerMInfo.MappingList = GetScannerSearchMapping(i, regionInfo.SenderIndex, regionInfo.PortIndex, regionInfo.ConnectIndex);
                        ScannerMonitorData scannerMData = new ScannerMonitorData();
                        if (!e.ScannerMonitorDataDicDic.ContainsKey(comName))
                        {
                            scannerMInfo.DeviceStatus = DeviceWorkStatus.Unknown;
                        }
                        else
                        {
                            scannerMData = GetScannerMonitorDataFromScannerMDict(e.ScannerMonitorDataDicDic[comName], regionInfo);
                            if (scannerMData != null)
                            {
                                if (!_allMonitorData.MonitorDataDic.ContainsKey(sn))
                                {
                                    _allMonitorData.MonitorDataDic.Add(sn, new SerializableDictionary<string, ScannerMonitorData>());
                                }
                                if (!_allMonitorData.MonitorDataDic[sn].ContainsKey(GetScannerNameFlag(regionInfo)))
                                {
                                    _allMonitorData.MonitorDataDic[sn].Add(GetScannerNameFlag(regionInfo), scannerMData);
                                }
                                if (e.ReadStatusResultDic.ContainsKey(comName))
                                {
                                    if (!_allMonitorData.MonitorResInfDic.ContainsKey(sn))
                                    {
                                        _allMonitorData.MonitorResInfDic.Add(sn, e.ReadStatusResultDic[comName].CompleteTime);
                                    }
                                    else
                                    {
                                        _allMonitorData.MonitorResInfDic[sn] = e.ReadStatusResultDic[comName].CompleteTime;
                                    }
                                }

                                if (scannerMData.WorkStatus == WorkStatusType.OK)
                                {
                                    scannerMInfo.TemperatureIsVaild = scannerMData.TemperatureOfScanCard.IsValid;
                                    scannerMInfo.Temperature = scannerMData.TemperatureOfScanCard.Value;
                                    scannerMInfo.Voltage = scannerMData.VoltageOfScanCard.Value;
                                }
                                else
                                {
                                    scannerMInfo.TemperatureIsVaild = false;
                                    scannerMInfo.DeviceStatus = DeviceWorkStatus.Error;
                                }
                            }
                            else
                            {
                                scannerMInfo.TemperatureIsVaild = false;
                                scannerMInfo.DeviceStatus = DeviceWorkStatus.Error;
                            }
                        }
                        monitorData.ScannerMonitorCollection.Add(scannerMInfo);
                        #endregion

                        SendStatusMsg("ParsingMonitorData");
                        #region 监控卡监控数据
                        MonitorCardMonitorInfo monitorCardMInfo = new MonitorCardMonitorInfo();
                        monitorCardMInfo.DeviceStatus = DeviceWorkStatus.OK;
                        monitorCardMInfo.MappingList = GetMonitorCardSearchMapping(i, regionInfo.SenderIndex, regionInfo.PortIndex, regionInfo.ConnectIndex, 0);
                        monitorCardMInfo.DeviceRegInfo = (DeviceRegionInfo)scannerMInfo.DeviceRegInfo.Clone();
                        if (!IsHWScreenConfigExist(sn) || _hwConfigs[sn].IsUpdateMCStatus == false)
                        {
                            continue;
                        }
                        else if (scannerMInfo.DeviceStatus != DeviceWorkStatus.OK ||
                            !IsHWScreenConfigExist(sn))
                        {
                            if (scannerMInfo.DeviceStatus == DeviceWorkStatus.Error)
                            {
                                monitorCardMInfo.DeviceStatus = DeviceWorkStatus.Error;
                            }
                            else
                            {
                                monitorCardMInfo.DeviceStatus = DeviceWorkStatus.Unknown;
                            }
                        }
                        else if (scannerMData.IsConnectMC)
                        {
                            #region 湿度状态
                            if (IsHWScreenConfigExist(sn) && _hwConfigs[sn].IsUpdateHumidity)
                            {
                                MCHumidityUpdateInfo humidityUInfo = new MCHumidityUpdateInfo()
                                {
                                    IsUpdate = true,
                                    Humidity = scannerMData.HumidityOfMonitorCard.Value
                                };
                                monitorCardMInfo.HumidityUInfo = humidityUInfo;
                            }
                            #endregion

                            #region 烟雾状态
                            if (IsHWScreenConfigExist(sn) && _hwConfigs[sn].IsUpdateSmoke)
                            {
                                MCSmokeUpdateInfo smokeUInfo = new MCSmokeUpdateInfo()
                                {
                                    IsUpdate = true,
                                    IsSmokeAlarm = scannerMData.SmokeWarn.IsSmokeAlarm
                                };
                                monitorCardMInfo.SmokeUInfo = smokeUInfo;
                            }
                            #endregion

                            #region 监控卡本板温度状态
                            MCTemperatureUpdateInfo tempUInfo = new MCTemperatureUpdateInfo()
                            {
                                IsUpdate = true, //_monitorSysData.SameMonitorSysData.IsUpdateTemperature,
                                Temperature = scannerMData.TemperatureOfMonitorCard.Value
                            };
                            monitorCardMInfo.TemperatureUInfo = tempUInfo;
                            #endregion

                            #region 风扇状态
                            if (IsHWScreenConfigExist(sn) && _hwConfigs[sn].IsUpdateFan && _hwConfigs[sn].FanInfoObj != null)
                            {
                                SerializableDictionary<int, int> fansMonitorInfoCollection = new SerializableDictionary<int, int>();
                                for (int fansIndex = 0; fansIndex < scannerMData.FanSpeedOfMonitorCardCollection.Count; fansIndex++)
                                {
                                    ValueInfo valInfo = scannerMData.FanSpeedOfMonitorCardCollection[fansIndex];
                                    if (scannerMData != null)
                                    {
                                        scannerMData.FanSpeedOfMonitorCardCollection[fansIndex] = new ValueInfo()
                                        {
                                            IsValid = valInfo.IsValid,
                                            Value = valInfo.Value / _hwConfigs[sn].FanInfoObj.FanPulseCount
                                        };
                                    }
                                    if (_hwConfigs[sn].FanInfoObj.isSame)
                                    {
                                        if (fansIndex < _hwConfigs[sn].FanInfoObj.FanCount && valInfo.IsValid)
                                        {
                                            fansMonitorInfoCollection.Add(fansIndex, (int)valInfo.Value / _hwConfigs[sn].FanInfoObj.FanPulseCount);
                                        }
                                    }
                                    else
                                    {
                                        OneFanInfo oneInfo = _hwConfigs[sn].FanInfoObj.FanList.Find(a =>
                                            a.ScanIndex == regionInfo.ConnectIndex && a.SenderIndex == regionInfo.SenderIndex
                                            && a.PortIndex == regionInfo.PortIndex);
                                        if (oneInfo != null && fansIndex < oneInfo.FanCount && valInfo.IsValid)
                                        {
                                            fansMonitorInfoCollection.Add(fansIndex, (int)valInfo.Value / _hwConfigs[sn].FanInfoObj.FanPulseCount);
                                        }
                                    }
                                }
                                monitorCardMInfo.FansUInfo = new MCFansUpdateInfo()
                                {
                                    IsUpdate = true,
                                    FansMonitorInfoCollection = fansMonitorInfoCollection
                                };
                            }
                            #endregion

                            #region 电源状态
                            if (IsHWScreenConfigExist(sn) && _hwConfigs[sn].IsUpdateMCVoltage && _hwConfigs[sn].PowerInfoObj != null)
                            {
                                SerializableDictionary<int, float> powerMonitorInfoCollection = new SerializableDictionary<int, float>();
                                for (int powerIndex = 0; powerIndex < scannerMData.VoltageOfMonitorCardCollection.Count; powerIndex++)
                                {
                                    ValueInfo valInfo = scannerMData.VoltageOfMonitorCardCollection[powerIndex];

                                    if (_hwConfigs[sn].PowerInfoObj.isSame)
                                    {
                                        if (powerIndex <= _hwConfigs[sn].PowerInfoObj.PowerCount)
                                        {
                                            powerMonitorInfoCollection.Add(powerIndex, valInfo.Value);
                                        }
                                    }
                                    else
                                    {
                                        OnePowerInfo oneInfo = _hwConfigs[sn].PowerInfoObj.PowerList.Find(a =>
                                            a.ScanIndex == regionInfo.ConnectIndex && a.SenderIndex == regionInfo.SenderIndex
                                            && a.PortIndex == regionInfo.PortIndex);
                                        if (oneInfo != null && powerIndex <= oneInfo.PowerCount)
                                        {
                                            powerMonitorInfoCollection.Add(powerIndex, valInfo.Value);
                                        }
                                    }
                                }
                                monitorCardMInfo.PowerUInfo = new MCPowerUpdateInfo()
                                {
                                    IsUpdate = true,
                                    PowerMonitorInfoCollection = powerMonitorInfoCollection
                                };
                            }
                            #endregion

                            #region 箱门状态
                            if (IsHWScreenConfigExist(sn) && _hwConfigs[sn].IsUpdateGeneralStatus)
                            {
                                byte generalStatus = scannerMData.GeneralStatusData;
                                List<bool> swithStatus = null;
                                ParseGeneralSwitch.ParseMCGeneralSwitchStatus(generalStatus, scannerMData.IsConnectMC, out swithStatus);

                                monitorCardMInfo.CabinetDoorUInfo = new MCDoorUpdateInfo()
                                {
                                    IsUpdate = true,
                                    IsDoorOpen = (!swithStatus[0])
                                };
                            }
                            #endregion

                            #region 排线状态
                            if (IsHWScreenConfigExist(sn) && _hwConfigs[sn].IsUpdateRowLine)
                            {
                                byte[] moduleBytes = scannerMData.ModuleStatusBytes;
                                ScanBoardRowLineStatus rowLineStatus = null;
                                List<SocketCableMonitorInfo> socketCableInfoCollection = new List<SocketCableMonitorInfo>();
                                if (ParseRowLineData.ParseScanBoardRowLineData(moduleBytes, scannerMData.IsConnectMC, displayInfo.VirtualMode, out rowLineStatus))
                                {
                                    //插座的列表
                                    for (int socketIndex = 0; socketIndex < rowLineStatus.SoketRowLineStatusList.Count; socketIndex++)
                                    {
                                        SocketCableMonitorInfo socketCableInfo = new SocketCableMonitorInfo();
                                        socketCableInfo.DeviceStatus = DeviceWorkStatus.OK;
                                        socketCableInfo.MappingList = GetSocketCableStatus(i, regionInfo.SenderIndex, regionInfo.PortIndex, regionInfo.ConnectIndex, 0, socketIndex);

                                        SoketRowLineInfo socketOriginalInfo = rowLineStatus.SoketRowLineStatusList[socketIndex];
                                        for (int rgbGroupIndex = 0; rgbGroupIndex < socketOriginalInfo.RGBStatusList.Count; rgbGroupIndex++)
                                        {
                                            OneGroupRGBStatus orStatus = socketOriginalInfo.RGBStatusList[rgbGroupIndex];
                                            List<SocketCableStatus> allGroupStatusList = new List<SocketCableStatus>();

                                            SocketCableStatus cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.ABCD_Signal;
                                            cableStatus.IsCableOK = socketOriginalInfo.IsABCDOk;
                                            allGroupStatusList.Add(cableStatus);

                                            cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.CTRL_Signal;
                                            cableStatus.IsCableOK = socketOriginalInfo.IsCtrlOk;
                                            allGroupStatusList.Add(cableStatus);


                                            cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.DCLK_Signal;
                                            cableStatus.IsCableOK = socketOriginalInfo.IsDCLKOk;
                                            allGroupStatusList.Add(cableStatus);

                                            cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.LAT_Signal;
                                            cableStatus.IsCableOK = socketOriginalInfo.IsLatchOk;
                                            allGroupStatusList.Add(cableStatus);

                                            cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.OE_Signal;
                                            cableStatus.IsCableOK = socketOriginalInfo.IsOEOk;
                                            allGroupStatusList.Add(cableStatus);

                                            cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.Red_Signal;
                                            cableStatus.IsCableOK = orStatus.IsRedOk;
                                            allGroupStatusList.Add(cableStatus);

                                            cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.Green_Signal;
                                            cableStatus.IsCableOK = orStatus.IsGreenOk;
                                            allGroupStatusList.Add(cableStatus);

                                            cableStatus = new SocketCableStatus();
                                            cableStatus.CableType = SocketCableType.Blue_Signal;
                                            cableStatus.IsCableOK = orStatus.IsBlueOk;
                                            allGroupStatusList.Add(cableStatus);

                                            if (displayInfo.VirtualMode == VirtualModeType.Led4Mode1 ||
                                                displayInfo.VirtualMode == VirtualModeType.Led4Mode2)
                                            {
                                                cableStatus = new SocketCableStatus();
                                                cableStatus.CableType = SocketCableType.VRed_Signal;
                                                cableStatus.IsCableOK = orStatus.IsVRedOk;
                                                allGroupStatusList.Add(cableStatus);
                                            }
                                            socketCableInfo.SocketCableInfoDict.Add(rgbGroupIndex, allGroupStatusList);
                                        }
                                        socketCableInfoCollection.Add(socketCableInfo);
                                    }
                                }

                                monitorCardMInfo.SocketCableUInfo = new MCSocketCableUpdateInfo()
                                {
                                    IsUpdate = true,
                                    SocketCableInfoCollection = socketCableInfoCollection
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            monitorCardMInfo.DeviceStatus = DeviceWorkStatus.Error;
                        }
                        monitorData.MonitorCardInfoCollection.Add(monitorCardMInfo);
                        #endregion
                    }
                }
            }
            _stopwatch.Stop();
            _fLogService.Info("读取监控常规数据完成，开始读取多功能卡外设数据..." + _stopwatch.ElapsedMilliseconds);
            _stopwatch.Restart();
            SendStatusMsg("ParsingScannerDataFinish");
            if (_funcMonitor == null || isRetry)
            {
                NotifyCompleteAllMonitor();
            }
            else
            {
                _funcMonitor.ReadAllOutDevice(ReadAllOutDeviceCallback);
            }
            #endregion
        }

        private void GetScanBoardInPosition(ILEDDisplayInfo displayInfo, ScanBoardRegionInfo info, ref int cols, ref int rows, ref bool isComplex)
        {
            isComplex = false;
            if (displayInfo is SimpleLEDDisplayInfo)
            {
                SimpleLEDDisplayInfo simpleLED = (SimpleLEDDisplayInfo)displayInfo;
                for (int m = 0; m < simpleLED.ScanBdCols; m++)
                {
                    for (int n = 0; n < simpleLED.ScanBdRows; n++)
                    {
                        ScanBoardRegionInfo data = simpleLED[m, n];
                        if (data.CompareTo(info) == 0)
                        {
                            cols = m;
                            rows = n;
                            return;
                        }
                    }
                }
            }
            else if (displayInfo is StandardLEDDisplayInfo)
            {
                StandardLEDDisplayInfo StandardLED = (StandardLEDDisplayInfo)displayInfo;
                for (int m = 0; m < StandardLED.ScanBoardCols; m++)
                {
                    for (int n = 0; n < StandardLED.ScanBoardRows; n++)
                    {
                        ScanBoardRegionInfo data = StandardLED[m, n];
                        if (data.CompareTo(info) == 0)
                        {
                            cols = m;
                            rows = n;
                            return;
                        }
                    }
                }
            }
            else
            {
                isComplex = true;
            }
        }

        #endregion
        #endregion

        #region 读取多功能卡监控数据
        private void ReadAllOutDeviceCallback(List<CompletedAddOutDeviceArgs> argsList)
        {
            _fLogService.Info("读取多功能卡外设完成，开始通知监控数据采集完成...");
            NotifyCompleteAllMonitor();
        }

        private void NotifyCompleteAllMonitor()
        {
            _fLogService.Info("开始通知数据采集完成");
            if (_readHWParams != null && _readHWParams.CallBack != null)
            {
                _stopwatch.Stop();
                CompletedMonitorCallbackParams param = new CompletedMonitorCallbackParams()
                {
                    MonitorData = _allMonitorData
                };
                Interlocked.Exchange(ref _isRunningMetux, 0);
                _readHWParams.CallBack.Invoke(param, _readHWParams.UserToken);
            }
            _fLogService.Info("完成通知数据采集完成，开始检查配置文件是否更新..." + _stopwatch.ElapsedMilliseconds);
            lock (_notifyLocker)
            {
                if (_isNeedNotifyScreenCfgChanged)
                {
                    _fLogService.Info("通知外部显示屏配置更新");
                    _isNeedNotifyScreenCfgChanged = false;
                    OnNotifyScreenCfgChangedEvent(this, EventArgs.Empty);
                    _fLogService.Info("完成通知外部显示屏配置更新");
                }
            }
            OnNotifyExecResEvent(TransferType.M3_RefreshDataFinish, string.Empty, UpdateCfgFileResType.OK);
        }
        #endregion

        #region 硬件操作
        private void SendBrightToHW(string comName, ILEDDisplayInfo scrInfo, byte bright)
        {
            List<ScreenPortAddrInfo> portList;
            scrInfo.GetScreenPortAddrInfo(out portList);
            _isWrightDataOK = true;
            for (int i = 0; i < portList.Count; i++)
            {
                string tag = "SendBrightToHW";
                if (i == portList.Count - 1)
                {
                    tag = "SendBrightToHW_Complete";
                }
                ScreenPortAddrInfo addrInfo = portList[i];

                _fLogService.Info(string.Format("设置串口:{0},发送卡:{1},网口:{2},亮度:{3}",
                    comName, addrInfo.SenderIndex, addrInfo.PortIndex, bright));

                PackageRequestWriteData dataPack = TGProtocolParser.SetGlobalBrightness(comName, addrInfo.SenderIndex,
                    addrInfo.PortIndex, SystemAddress.SCANBOARD_BROADCAST_ADDRESS,
                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT, tag, false,
                    bright, null, CompletedSetBrightness);
                _serverProxy.SendRequestWriteData(dataPack);
            }
        }
        private void CompletedSetBrightness(object sender, CompletePackageRequestEventArgs e)
        {
            if (e.Request.PackResult != PackageResults.ok
                || e.Request.CommResult != Nova.IO.Port.CommResults.ok
                || e.Request.AckCode != (int)AckResults.ok)
            {
                if (e.Request.Tag == "SendBrightToHW_Complete")
                {
                    NotifySettingCompletedEventArgs args = new NotifySettingCompletedEventArgs()
                    {
                        SettingType = HWSettingType.GlobalBright,
                        Result = false
                    };
                    OnNotifySettingCompletedEvent(this, args);
                }
                else
                {
                    _isWrightDataOK = false;
                }
            }
            else
            {
                if (e.Request.Tag == "SendBrightToHW_Complete")
                {
                    NotifySettingCompletedEventArgs args = new NotifySettingCompletedEventArgs()
                    {
                        SettingType = HWSettingType.GlobalBright,
                        Result = false
                    };

                    if (_isWrightDataOK)
                    {
                        args.Result = true;
                    }
                    OnNotifySettingCompletedEvent(this, args);
                }
            }
        }
        private bool SetPowerExecuteStatus(string func, PowerOperateType operateType)
        {
            bool isOk = true;
            Thread.Sleep(20);
            _fLogService.Info(string.Format("设置电源路径:{0},方式:{1}", func, operateType.ToString()));
            if (Interlocked.Exchange(ref _isHwRunningMetux, 1) == 0)
            {
                FunctionCardRoadInfo roadInfo = CommandTextParser.GetDeJsonSerialization<FunctionCardRoadInfo>(func);
                if (roadInfo == null || roadInfo.FunCardLocation == null)
                {
                    _fLogService.Error("设置电源状态为:" + operateType.ToString() + "时，发现电源地址为空!");
                    return false;
                }
                PackageRequestWriteData writeData = null;
                if (roadInfo.FunCardLocation.IsConnectCommPort)
                {
                    writeData = TGProtocolParser.FuncCard_SetPowerPortCtrl(roadInfo.FunCardLocation.CommPort,//COM名称
                                                                           (byte)roadInfo.FunCardLocation.FuncCardIndex,
                                                                           CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                           operateType == PowerOperateType.Start ? SetPowerStatusTagStart : SetPowerStatusTagClose,
                                                                           false,
                                                                           roadInfo.PowerIndex,
                                                                           operateType,
                                                                           null,
                                                                           WriteDataCompleteRequest);
                }
                else
                {
                    writeData = TGProtocolParser.FuncCard_SetPowerPortCtrl(roadInfo.FunCardLocation.CommPort,
                                                                           (byte)roadInfo.FunCardLocation.SenderIndex,
                                                                           (byte)roadInfo.FunCardLocation.PortIndex,
                                                                           (byte)roadInfo.FunCardLocation.FuncCardIndex,
                                                                           CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                           operateType == PowerOperateType.Start ? SetPowerStatusTagStart : SetPowerStatusTagClose,
                                                                           false,
                                                                           roadInfo.PowerIndex,
                                                                           operateType,
                                                                           null,
                                                                           WriteDataCompleteRequest);
                }

                _fLogService.Debug("设置电源时完成");
                return _serverProxy.SendRequestWriteData(writeData);
            }
            else
            {
                isOk = false;
            }
            return isOk;
        }
        private void WriteDataCompleteRequest(object sender, CompletePackageRequestEventArgs e)
        {
            if (GetClassNameFromTag(e.Request.Tag) != ClassName)
            {
                NotifySettingCompletedEventArgs args = null;
                if (e.Request.Tag == SetPowerStatusTagStart)
                {
                    args = new NotifySettingCompletedEventArgs()
                    {
                        SettingType = HWSettingType.OpenDevice,
                        Result = false
                    };
                }
                else
                {
                    args = new NotifySettingCompletedEventArgs()
                    {
                        SettingType = HWSettingType.CloseDevice,
                        Result = false
                    };
                }
                OnNotifySettingCompletedEvent(this, args);
                Interlocked.Exchange(ref _isHwRunningMetux, 0);
                return;
            }
            if (e.Request.PackResult == PackageResults.ok
               && e.Request.CommResult == Nova.IO.Port.CommResults.ok
               && e.Request.AckCode == (byte)AckResults.ok)
            {
                if (e.Request.Tag == SetPowerStatusTagStart)
                {
                    lock (_setOnePowerStatusObj)
                    {
                        NotifySettingCompletedEventArgs args = new NotifySettingCompletedEventArgs()
                        {
                            SettingType = HWSettingType.OpenDevice,
                            Result = true
                        };
                        OnNotifySettingCompletedEvent(this, args);
                        _fLogService.Info(e.Request.CurPartIndex + " 某一路电源的打开状态成功，Pulse控制一路电源的锁");
                        System.Threading.Monitor.Pulse(_setOnePowerStatusObj);
                    }
                }
                else if (e.Request.Tag == SetPowerStatusTagClose)
                {
                    lock (_setOnePowerStatusObj)
                    {
                        NotifySettingCompletedEventArgs args = new NotifySettingCompletedEventArgs()
                        {
                            SettingType = HWSettingType.CloseDevice,
                            Result = true
                        };
                        OnNotifySettingCompletedEvent(this, args);
                        _fLogService.Info(e.Request.CurPartIndex + " 某一路电源的关闭状态成功，Pulse控制一路电源的锁");
                        System.Threading.Monitor.Pulse(_setOnePowerStatusObj);
                    }
                }
            }
            else
            {
                if (e.Request.Tag == SetPowerStatusTagStart)
                {
                    lock (_setOnePowerStatusObj)
                    {
                        NotifySettingCompletedEventArgs args = new NotifySettingCompletedEventArgs()
                        {
                            SettingType = HWSettingType.OpenDevice,
                            Result = false
                        };
                        OnNotifySettingCompletedEvent(this, args);
                        _fLogService.Error(e.Request.CurPartIndex + " 某一路电源的打开状态失败，Pulse控制一路电源的锁");
                        System.Threading.Monitor.Pulse(_setOnePowerStatusObj);
                    }
                }
                else if (e.Request.Tag == SetPowerStatusTagClose)
                {
                    lock (_setOnePowerStatusObj)
                    {
                        NotifySettingCompletedEventArgs args = new NotifySettingCompletedEventArgs()
                        {
                            SettingType = HWSettingType.CloseDevice,
                            Result = false
                        };
                        OnNotifySettingCompletedEvent(this, args);
                        _fLogService.Error(e.Request.CurPartIndex + " 某一路电源的关闭状态失败，Pulse控制一路电源的锁");
                        System.Threading.Monitor.Pulse(_setOnePowerStatusObj);
                    }
                }
            }
            Interlocked.Exchange(ref _isHwRunningMetux, 0);
        }
        private void CompleteControlRequst(object sender, CompletePackageRequestEventArgs e)
        {
            if (e.Request.PackResult == PackageResults.ok
                        && e.Request.CommResult == CommResults.ok
                        && e.Request.AckCode == (int)AckResults.ok)
            {
                if (e.Request.Tag == "KillBlackScreen")
                {
                    TransFerDataHandlerCallback(TransferType.M3_BlackScreen, "true", e.Request.UserToken);
                }
            }
            else
            {
                if (e.Request.Tag == "KillBlackScreen")
                {
                    TransFerDataHandlerCallback(TransferType.M3_BlackScreen, "false", e.Request.UserToken);
                }
            }
        }
        private string GetClassNameFromTag(string tag)
        {
            string[] tagPart = tag.Split(SendInfoTagSeperate);
            if (tagPart.Length >= 2)
            {
                return tagPart[0];
            }
            return tag;
        }
        private void SetBlackScreen(TransferType transType, string commPort, ILEDDisplayInfo led,
            byte value, object userToken)
        {
            ReadMonitorDataErrType res = IsExcute();
            if (res != ReadMonitorDataErrType.OK)
            {
                TransFerDataHandlerCallback(transType, "false", userToken);
                return;
            }
            List<ScreenPortAddrInfo> list = null;
            led.GetScreenPortAddrInfo(out list);
            if (list == null || list.Count == 0)
            {
                TransFerDataHandlerCallback(transType, "false", userToken);
                return;
            }
            TransFerDataHandlerCallback(transType, "true", userToken);
            //PackageRequestWriteData writeDataPack = null;
            //foreach (ScreenPortAddrInfo screen in list)
            //{
            //    writeDataPack = TGProtocolParser.SetKillMode(commPort,
            //                                                   screen.SenderIndex,
            //                                                   screen.PortIndex,
            //                                                   SystemAddress.SCANBOARD_BROADCAST_ADDRESS,
            //                                                   CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
            //                                                   "KillBlackScreen",
            //                                                   false,
            //                                                   value,
            //                                                   null, CompleteControlRequst);
            //    //new CompletePackageRequestEventHandler(CompleteScreenControlRequst));
            //    writeDataPack.UserToken=userToken;
            //    _serverProxy.SendRequestWriteData(writeDataPack);
            //}
        }
        private void SetNormalScreen(TransferType transType, string commPort, ILEDDisplayInfo led,
            byte value, object userToken)
        {
            ReadMonitorDataErrType res = IsExcute();
            if (res != ReadMonitorDataErrType.OK)
            {
                TransFerDataHandlerCallback(transType, "false", userToken);
                return;
            }
            List<ScreenPortAddrInfo> list = null;
            led.GetScreenPortAddrInfo(out list);
            if (list == null || list.Count == 0)
            {
                TransFerDataHandlerCallback(transType, "false", userToken);
                return;
            }
            TransFerDataHandlerCallback(transType, "true", userToken);
        }

        #endregion

        private void SendStatusMsg(string msg)
        {
            OnNotifyExecResEvent(TransferType.M3_ExecStaus, msg, UpdateCfgFileResType.OK);
        }
        #endregion

        /*
        #region 初始化配置文件路径
        private void InitConfigFileName()
        {
            string saveFilePath = ConstValue.PROGRAM_FATH + CONFIG_FOLDER_NAME;
            if (!Directory.Exists(saveFilePath))
            {
                Directory.CreateDirectory(saveFilePath);
            }
            string oldMonitorCfgFileName = Path.GetFileName(ConstValue.MONITOR_CONFIG_FILE);
            _monitorCfgFileName = saveFilePath + oldMonitorCfgFileName;

            string oldFuncCardCfgFileName = Path.GetFileName(ConstValue.MUL_FUNC_CARD_CONFIG_FOLDER);
            _funcCardCfgFileName = saveFilePath + oldFuncCardCfgFileName;

            UpdateConfigFileToCurrent();
        }
        #endregion
        */
        #region 配置文件更新,不带回调的处理
        private void UpdateConfigFileAsy(object state)
        {
            string commandText = (string)state;
            TransferParams param = CommandTextParser.DeserialCmdTextToParam(commandText);
            _fLogService.Debug("Receive Config:" + param.TranType);
            switch (param.TranType)
            {
                case TransferType.M3_UpdateLedScreenConfigInfo:
                    _isStopExec = false;
                    UpdateScreenConfig(param);
                    break;
                case TransferType.UpdateLedMonitoringConfigInfo:
                    UpdateMonitoringConfigInfo(param.Content);
                    break;
                case TransferType.M3_OpenDevice:
                    SetPowerExecuteStatus(param.Content, PowerOperateType.Start);
                    break;
                case TransferType.M3_CloseDevice:
                    SetPowerExecuteStatus(param.Content, PowerOperateType.Stop);
                    break;
                case TransferType.M3_WriteSmartLightHWConfig:
                    SetSmartLightConfig(param.Content);
                    break;
                case TransferType.M3_MonitorStopNotify:
                    string[] strList = param.Content.Split('|');
                    if (strList[0].Equals("Correction", StringComparison.CurrentCultureIgnoreCase))
                    {
                        SetIsEnableSmartBrights(strList[1], false);
                    }
                    else if (strList[0].Equals("ProgramUpdate", StringComparison.CurrentCultureIgnoreCase))
                    {
                        _isStopExec = true;
                    }
                    break;
                case TransferType.M3_MonitorRenumeNotify:
                    string[] strList1 = param.Content.Split('|');
                    if (strList1[0].Equals("Correction", StringComparison.CurrentCultureIgnoreCase))
                    {
                        SetIsEnableSmartBrights(strList1[1], true);
                    }
                    else if (strList1[0].Equals("ProgramUpdate", StringComparison.CurrentCultureIgnoreCase))
                    {
                        _isStopExec = false;
                    }
                    break;
                case TransferType.M3_EnableSmartBrightness:
                    _fLogService.Info("上端主动要求使能智能亮度." + param.Content);
                    if (string.IsNullOrEmpty(param.Content))
                    {
                        _fLogService.Error("是否启用智能亮度参数为空，无法设置");
                    }
                    else
                    {
                        string[] str = param.Content.Split('|');
                        if (str.Length < 2)
                        {
                            _fLogService.Error("是否启用智能亮度参数少于两个，无法设置");
                        }
                        else
                        {
                            if (str[1] == "Enable")
                            {
                                SetIsEnableSmartBright(str[0], true);
                            }
                            else
                            {
                                SetIsEnableSmartBright(str[0], false);
                            }
                        }
                    }
                    break;
                case TransferType.M3_COMFindSN:
                    if (string.IsNullOrEmpty(param.Content))
                    {
                        OnNotifyExecResEvent(TransferType.M3_COMFindSN, string.Empty, UpdateCfgFileResType.FileError);
                    }
                    else
                    {
                        string[] str = param.Content.Split('|');
                        int screenIndex = 0;
                        if (str.Length < 2 || !int.TryParse(str[1], out screenIndex))
                        {
                            OnNotifyExecResEvent(TransferType.M3_COMFindSN, string.Empty, UpdateCfgFileResType.FileError);
                        }
                        else
                        {
                            string strResult = FromComFindSN(str[0], screenIndex);
                            if (string.IsNullOrEmpty(strResult))
                            {
                                OnNotifyExecResEvent(TransferType.M3_COMFindSN, string.Empty, UpdateCfgFileResType.FileError);
                            }
                            else
                            {
                                OnNotifyExecResEvent(TransferType.M3_COMFindSN, strResult, UpdateCfgFileResType.OK);
                            }
                        }
                    }
                    break;
                case TransferType.M3_FirstInitialize:
                    FirstInitialized();
                    break;
            }
        }

        private void UpdateMonitoringConfigInfo(string content)
        {
            lock (_notifyLocker)
            {
                MarsHWConfig marsconfig = CommandTextParser.GetDeJsonSerialization<MarsHWConfig>(content);
                if (_hwConfigs.ContainsKey(marsconfig.SN))
                {
                    _hwConfigs.Remove(marsconfig.SN);
                }
                _hwConfigs.Add(marsconfig.SN, marsconfig);
                OnNotifyExecResEvent(TransferType.M3_HWMonitor, string.Empty, UpdateCfgFileResType.OK);
            }
        }
        private void UpdateScreenConfig(TransferParams param)
        {
            OnEquipmentChangeEvent(null, EventArgs.Empty);
        }
        private void CheckAndSetScreenChanged(CompleteReadAllComHWBaseInfoParams allBaseInfo)
        {
            _fLogService.Debug("进入：CheckAndSetScreenChanged");
            string saveFilePath = System.IO.Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"NovaLCT 2012\Config\Monitoring\"), CONFIG_SCREEN_NAME);
            if (!Directory.Exists(saveFilePath))
            {
                _fLogService.Debug("CheckAndSetScreenChanged：无目录，需要创建");
                Directory.CreateDirectory(saveFilePath);
            }

            SerializableDictionary<string, List<SenderRedundancyInfo>> reduInfoList = new SerializableDictionary<string, List<SenderRedundancyInfo>>();
            if (allBaseInfo == null || allBaseInfo.AllInfo == null || allBaseInfo.AllInfo.AllInfoDict == null)
            {
                _fLogService.Debug("CheckAndSetScreenChanged：读硬件数据为空，所以直接退出");
                return;
            }
            if (_allComBaseInfo != null && (_isNeedNotifyScreenCfgChanged == true ||
                _allComBaseInfo.AllInfoDict.Count != allBaseInfo.AllInfo.AllInfoDict.Count))
            {
                WriteScreenFiles(saveFilePath, allBaseInfo.AllInfo.AllInfoDict);
                ScreenChangedNotify();
                return;
            }

            try
            {
                string[] strFiles = System.IO.Directory.GetFiles(saveFilePath);
                foreach (string file in strFiles)
                {
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                _fLogService.Error("ExistCatch：Delete Old Screen File Error:" + ex.ToString());
            }

            string saveFileName = string.Empty;
            string saveFileName_tmp = string.Empty;
            foreach (KeyValuePair<string, OneCOMHWBaseInfo> pair in allBaseInfo.AllInfo.AllInfoDict)
            {
                if (pair.Value.DisplayResult == CommonInfoCompeleteResult.OK)
                {
                    bool isReduDif = false;
                    _fLogService.Debug("CheckAndSetScreenChanged：冗余判断");
                    if (!_reduInfoList.ContainsKey(pair.Key))
                    {
                        if (pair.Value.ReduInfoList != null && pair.Value.ReduInfoList.Count > 0)
                        {
                            isReduDif = true;
                        }
                    }
                    else
                    {
                        isReduDif = IsRedundancyChanged(pair.Value.ReduInfoList, _reduInfoList[pair.Key]);
                    }

                    saveFileName = saveFilePath + pair.Value.FirstSenderSN;
                    if (_allComBaseInfo != null && _allComBaseInfo.AllInfoDict != null)
                    {
                        if (!_allComBaseInfo.AllInfoDict.ContainsKey(pair.Key))
                        {
                            isReduDif = true;
                        }
                        else
                        {
                            WriteScreenFile(saveFileName, pair.Key, _allComBaseInfo.AllInfoDict[pair.Key].FirstSenderSN, _allComBaseInfo.AllInfoDict[pair.Key].LEDDisplayInfoList);
                        }
                    }
                    saveFileName_tmp = saveFileName + "_tmp";
                    WriteScreenFile(saveFileName_tmp, pair.Key, pair.Value.FirstSenderSN, pair.Value.LEDDisplayInfoList);
                    if (isReduDif || !MonitorFuncHelper.IsMD5Equal(saveFileName, saveFileName_tmp))
                    {
                        WriteScreenFiles(saveFilePath, allBaseInfo.AllInfo.AllInfoDict);
                        ScreenChangedNotify();
                        break;
                    }
                    else
                    {
                        _fLogService.Info("屏体无变更，不需要重新读取");
                    }
                }
                else
                {
                    OnNotifyScreenCfgChangedEvent(this, EventArgs.Empty);
                }
            }
        }

        private void WriteScreenFiles(string saveFilePath, SerializableDictionary<string, OneCOMHWBaseInfo> allInfoDict)
        {
            lock (_lockObj)
            {
                string saveFileName = string.Empty;
                foreach (KeyValuePair<string, OneCOMHWBaseInfo> pair in allInfoDict)
                {
                    saveFileName = saveFilePath + pair.Value.FirstSenderSN;
                    WriteScreenFile(saveFileName, pair.Key, pair.Value.FirstSenderSN, pair.Value.LEDDisplayInfoList);
                }
            }
        }

        private bool WriteScreenFile(string saveFileName, string commPort, string sn, List<ILEDDisplayInfo> ledInfos)
        {
            _fLogService.Debug("CheckAndSetScreenChanged：初始化ScreenInfoAccessor：" + sn);
            ScreenInfoAccessor screenInfoAccessor = new ScreenInfoAccessor(_serverProxy, commPort);
            return screenInfoAccessor.SaveDviScreenInfoToFile(saveFileName, new GraphicsDVIPortInfo(), ledInfos);
        }

        private void ScreenChangedNotify()
        {
            lock (_notifyLocker)
            {
                _fLogService.Info("屏体有变更，需要重新读取");
                if (_isRunningMetux == 0)
                {
                    OnNotifyScreenCfgChangedEvent(this, EventArgs.Empty);
                }
                else
                {
                    _isNeedNotifyScreenCfgChanged = true;
                }
            }
        }

        private bool IsRedundancyChanged(List<SenderRedundancyInfo> a, List<SenderRedundancyInfo> b)
        {
            if (System.Object.ReferenceEquals(a, b) || ((object)a == null && (object)b == null))
            {
                return false;
            }

            if (a == null && b != null)
            {
                if (b.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (a != null && b == null)
            {
                if (a.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (a.Count == b.Count)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (!a[i].Equals(b[i]))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        private void SetSmartLightConfig(string content)
        {
            _fLogService.Info("开始设置亮度到软硬件...");
            SmartLightConfigInfo smartInfo =
                    CommandTextParser.GetDeJsonSerialization<SmartLightConfigInfo>(content);
            string commPort = string.Empty;
            ILEDDisplayInfo leds = null;
            GetScreenInfoByUDID(smartInfo.ScreenSN, out commPort, out leds);
            if (smartInfo.DisplayHardcareConfig != null)
            {
                ReadSmartLightDataParams readparam = new ReadSmartLightDataParams();
                readparam.BrightHWExecType = smartInfo.HwExecTypeValue;
                readparam.DisplayConfigBase = new DisplaySmartBrightEasyConfigBase();
                smartInfo.DisplayHardcareConfig.CopyTo(readparam.DisplayConfigBase);
                SetLightSensorDeleteSNIndex(readparam.DisplayConfigBase.AutoBrightSetting);
                _smartLightAccessor.WriteHWSmartLightData(commPort, 0,
                    readparam, WriteSmartLightDataCallBack, smartInfo);
                _fLogService.Debug("HW:" + CommandTextParser.GetJsonSerialization<ReadSmartLightDataParams>(readparam));
            }
            else
            {
                if (smartInfo.DispaySoftWareConfig != null)
                {
                    if (smartInfo.HwExecTypeValue == BrightnessHWExecType.HardWareControl
                        || smartInfo.AdjustType == BrightAdjustType.Mannual)
                    {
                        _smartBright.SetSmartEnableStatus(smartInfo.ScreenSN, false);
                        _smartBright.DetachSmartBright(smartInfo.ScreenSN);
                        SetSensorData(smartInfo.ScreenSN, null, false);
                    }
                    else
                    {
                        _smartBright.SetSmartEnableStatus(smartInfo.ScreenSN, true);
                        SetLightSensorDeleteSNIndex(smartInfo.DispaySoftWareConfig.AutoBrightSetting);
                        _smartBright.AttachSmartBright(smartInfo.ScreenSN, smartInfo.DispaySoftWareConfig);
                        _fLogService.Debug("Soft:" + CommandTextParser.GetJsonSerialization<DisplaySmartBrightEasyConfig>(smartInfo.DispaySoftWareConfig));
                        SetSensorData(smartInfo.ScreenSN, smartInfo.DispaySoftWareConfig, true);
                    }
                }
                OnNotifyExecResEvent(
                        TransferType.M3_BrightConfigSaveResult,
                        string.Format("{0}|{1}", smartInfo.ScreenSN, true),
                        UpdateCfgFileResType.OK);

                _fLogService.Info("SetSmartLightConfig：Only SoftWare Success!");
            }
            _fLogService.Info("完成设置亮度到软硬件...");
        }

        private void WriteSmartLightDataCallBack(WriteSmartLightDataParams param, object userToken)
        {
            SmartLightConfigInfo smartInfo = (SmartLightConfigInfo)userToken;
            if (param.IsResult)
            {
                if (smartInfo.DispaySoftWareConfig != null)
                {
                    if (smartInfo.HwExecTypeValue == BrightnessHWExecType.HardWareControl
                        || smartInfo.AdjustType == BrightAdjustType.Mannual)
                    {
                        if (_smartBright != null)
                        {
                            _smartBright.SetSmartEnableStatus(smartInfo.ScreenSN, false);
                            _smartBright.DetachSmartBright(smartInfo.ScreenSN);
                        }
                        SetSensorData(smartInfo.ScreenSN, null, false);
                    }
                    else
                    {
                        if (_smartBright != null)
                        {
                            _smartBright.SetSmartEnableStatus(smartInfo.ScreenSN, true);
                            SetLightSensorDeleteSNIndex(smartInfo.DispaySoftWareConfig.AutoBrightSetting);
                            _smartBright.AttachSmartBright(smartInfo.ScreenSN, smartInfo.DispaySoftWareConfig);
                        }
                        _fLogService.Debug("Soft:" + CommandTextParser.GetJsonSerialization<DisplaySmartBrightEasyConfig>(smartInfo.DispaySoftWareConfig));
                        SetSensorData(smartInfo.ScreenSN, smartInfo.DispaySoftWareConfig, true);
                    }
                }
                OnNotifyExecResEvent(
                        TransferType.M3_BrightConfigSaveResult,
                        string.Format("{0}|{1}", smartInfo.ScreenSN, true),
                        UpdateCfgFileResType.OK);
                _fLogService.Info("SetSmartLightConfig：Success!");
            }
            else
            {
                OnNotifyExecResEvent(
                        TransferType.M3_BrightConfigSaveResult,
                        string.Format("{0}|{1}", smartInfo.ScreenSN, false),
                        UpdateCfgFileResType.FileError);
                _fLogService.Info("SetSmartLightConfig：Faild!");
            }
        }

        private void SetLightSensorDeleteSNIndex(AutoBrightExtendData autoSensorData)
        {
            if (autoSensorData != null && autoSensorData.UseLightSensorList != null)
            {
                foreach (PeripheralsLocation peripheral in autoSensorData.UseLightSensorList)
                {
                    if (!string.IsNullOrEmpty(peripheral.FirstSenderSN))
                    {
                        peripheral.FirstSenderSN = peripheral.FirstSenderSN.Split('-')[0];
                    }
                }
            }
        }

        private void SetIsEnableSmartBrights(string snListsString, bool isEnable)
        {
            Action action = new Action(() =>
            {
                List<string> snLists = CommandTextParser.GetDeJsonSerialization<List<string>>(snListsString);
                foreach (string sn in snLists)
                {
                    SetIsEnableSmartBright(sn, true);
                    Thread.Sleep(20);
                }
            });
            action.BeginInvoke(null, null);
        }
        private void SetIsEnableSmartBright(string screenSN, bool isEnable)
        {
            string commPort = string.Empty;
            ILEDDisplayInfo leds = null;
            if (string.IsNullOrEmpty(screenSN))
            {
                _fLogService.Error(string.Format("屏{0}找不到，无法设置智能亮度是否调节：{1}", screenSN, isEnable));
                return;
            }
            GetScreenInfoByUDID(screenSN, out commPort, out leds);
            if (string.IsNullOrEmpty(commPort))
            {
                _fLogService.Error(string.Format("屏{0}找不到串口，无法设置智能亮度是否调节：{1}", screenSN, isEnable));
                return;
            }
            _fLogService.Info(string.Format("屏{0}设置智能亮度是否调节：{1}", screenSN, isEnable));
            //TODO:智能亮度是否启用：软硬件配置
            if (_smartLightAccessor != null)
            {
                _smartLightAccessor.SetIsEnableSmartBright(commPort, 0, isEnable, null);
            }
            if (_smartBright != null)
            {
                _smartBright.SetSmartEnableStatus(screenSN, isEnable);
            }
        }
        #endregion

        #region 内部查找判断等私有函数
        private ReadMonitorDataErrType IsExcute()
        {
            if (_serverProxy == null || !_serverProxy.IsRegisted)
            {
                RegisterToServer();
                return ReadMonitorDataErrType.NoServerObj;
            }
            if (!CheckHaveValidScreenInfo(_allComBaseInfo))
            {
                return ReadMonitorDataErrType.NoScreenInfo;
            }
            //if (_hwConfigs == null || _hwConfigs.Count == 0)
            //{
            //    return ReadMonitorDataErrType.NoMonitorConfig;
            //}
            return ReadMonitorDataErrType.OK;
        }
        private void ClearMonitorData()
        {
            _allMonitorData.AllScreenMonitorCollection.Clear();

            _allMonitorData.CommPortData.Clear();
            _allMonitorData.CurAllSenderDVIDic.Clear();
            _allMonitorData.CurAllSenderStatusDic.Clear();
            _allMonitorData.RedundantStateType.Clear();
            _allMonitorData.TempRedundancyDict.Clear();
            _allMonitorData.MonitorDataDic.Clear();
            _allMonitorData.MonitorResInfDic.Clear();
        }
        private bool IsHWScreenConfigExist(string sn)
        {
            if (_hwConfigs == null || !_hwConfigs.ContainsKey(sn))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckHaveValidScreenInfo(AllCOMHWBaseInfo allInfo)
        {
            bool isHave = false;
            if (allInfo == null || allInfo.AllInfoDict == null)
            {
                return isHave;
            }
            foreach (string key in allInfo.AllInfoDict.Keys)
            {
                OneCOMHWBaseInfo oneComInfo = allInfo.AllInfoDict[key];
                if (oneComInfo.LEDDisplayInfoList != null &&
                    oneComInfo.LEDDisplayInfoList.Count != 0)
                {
                    isHave = true;
                    break;
                }
            }
            return isHave;
        }
        private void OutputDebugInfo(string msg)
        {
#if DEBUG
            string domain = AppDomain.CurrentDomain.FriendlyName + "--" + this.GetType().ToString() + ": >>>>";
            StringBuilder sb = new StringBuilder();
            sb.Append(domain);
            sb.Append(msg);
            Debug.WriteLine(sb.ToString());
#endif
        }
        private string GetScreenUdid(string firstSenderSN, int scrIndexInCom)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(firstSenderSN);
            sb.Append("-");
            sb.Append(scrIndexInCom.ToString("x2"));
            return sb.ToString();
        }
        private ScreenModnitorData FindScreenMonitorDataFromList(List<ScreenModnitorData> monitorDataList, string scrUdid)
        {
            ScreenModnitorData data = monitorDataList.Find(a => a.ScreenUDID == scrUdid);
            if (data == null)
            {
                data = new ScreenModnitorData(scrUdid);
                _allMonitorData.AllScreenMonitorCollection.Add(data);
            }
            return data;
        }
        private ScannerMonitorData GetScannerMonitorDataFromScannerMDict(Dictionary<string, ScannerMonitorData> monitorDict, ScanBoardRegionInfo regionInfo)
        {
            string scannerFlag = GetScannerNameFlag(regionInfo);

            if (monitorDict != null && monitorDict.ContainsKey(scannerFlag))
            {
                return monitorDict[scannerFlag];
            }
            else
            {
                return null;
            }
        }
        private string GetScannerNameFlag(ScanBoardRegionInfo regionInfo)
        {
            return regionInfo.SenderIndex + "-" + regionInfo.PortIndex + "-" + regionInfo.ConnectIndex;
        }
        private string FromComFindSN(string comName, int screenIndex)
        {
            if (_allComBaseInfo == null || _allComBaseInfo.AllInfoDict == null)
            {
                return string.Empty;
            }
            if (_allComBaseInfo.AllInfoDict.ContainsKey(comName) && _allComBaseInfo.AllInfoDict[comName] != null)
            {
                return GetScreenUdid(_allComBaseInfo.AllInfoDict[comName].FirstSenderSN, screenIndex);
            }
            return string.Empty;
        }
        private void OnNotifyExecResEvent(TransferType transType, string resultData, UpdateCfgFileResType updateResType)
        {
            OnNotifyUpdateCfgFileResEvent(this, new UpdateCfgFileResEventArgs()
            {
                UpdateParams = new TransferParams() { TranType = transType, Content = resultData },
                Result = updateResType
            });
        }
        private void WriteLog(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("HWMonitorDataReader>>>>>>>>>");
            sb.Append(msg);
            Debug.WriteLine(sb.ToString());
        }
        void _smartBright_OutputLogEvent(object sender, EventArgs e)
        {
            if (sender != null)
            {
                _fLogService.Debug(sender.ToString());
                string[] str = sender.ToString().Split('|');
                if (str[0].IndexOf("SmartBright=>>>Auto Bright Result:") > 0)
                {
                    OnNotifyExecResEvent(
                        TransferType.M3_ExecBrightResultLog,
                        string.Format("{0}|{1}|{2}|{3}", str[1], str[2], str[3] == "true" ? "true" : "false", str[4]),
                        UpdateCfgFileResType.OK);
                }
            }
        }
        private void StartReadHWMonitorData(object state)
        {
            //            ReadHWDataParams parm = (ReadHWDataParams)state;
            //#if TestMode
            //            Thread.Sleep(3000);
            //            CompletedMonitorCallbackParams callBackP = GetCompletedMonitorCallbackParams();
            //            parm.CallBack.BeginInvoke(callBackP, parm.UserToken, null, null);

            //#else
            //            throw new Exception("The method or operation is not implemented.");
            //#endif

            //            Interlocked.Exchange(ref _isRunningMetux, 0);
        }
        #endregion

    }
}