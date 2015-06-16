using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Collections.ObjectModel;
using System.Threading;
using Nova.Monitoring.DataSource;
using Nova.Monitoring.Common;
using Nova.Monitoring.SystemMessageManager;
using Nova.Monitoring.HardwareMonitorInterface;
using Nova.Monitoring.MarsMonitorDataReader;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using Nova.Monitoring.ColudSupport;
using Nova.Xml.Serialization;
using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Log4NetLibrary;
using Nova.LCT.GigabitSystem.HWPointDetect;
using Quartz;
using Quartz.Impl;

namespace Nova.Monitoring.MarsDataAcquisition
{
    public partial class DataAcquisition : DataSourceBase
    {
        #region 字段
        private SystemInfo _sysInfo;
        private System.Timers.Timer _dataReadTimer;
        private bool _timerIsEnable = true;

        private IMonitorDataReader _moniDatareader = null;
        private List<ScreenInfo> _screenInfos = null;
        /// <summary>
        /// 0表征目前线程正处于idle状态，1表征处于running状态
        /// </summary>
        private int _isRunningMetux = 0;
        private int _isReadingMonitorData = 0;
        private string _screenUDID;
        private string _cmdSource;
        private Dictionary<string, LedAlarmConfig> _ledAlarmConfigDic = new Dictionary<string, LedAlarmConfig>();
        private CompletedMonitorCallbackParams _cmpParams;
        private int _retryCount = 2;
        private List<RepetDataInfo> _senderLocList = new List<RepetDataInfo>();
        private List<RepetDataInfo> _scannerLocList = new List<RepetDataInfo>();
        private DataType _dataType = DataType.all;
        private bool _isNeedRetry = false;
        private AutoResetEvent _autoResetHWEvent = new AutoResetEvent(false);
        private Dictionary<string, DetectConfigParams> _detectConfigParamList = new Dictionary<string, DetectConfigParams>();
        private Dictionary<string, ReadSmartLightDataParams> _dicHwBrightConfig = new Dictionary<string, ReadSmartLightDataParams>();
        #endregion

        public DataAcquisition()
        {
            _sysInfo = new SystemInfo();
        }

        public List<ScreenInfo> ScreenInfos
        {
            get { return _screenInfos; }
            set
            {
                _screenInfos = value;
            }
        }

        public IMonitorDataReader MonitorDataReader
        {
            get { return _moniDatareader; }
        }

        public Dictionary<string, DetectConfigParams> DetectConfigParamList
        {
            get { return _detectConfigParamList; }
        }

        #region LedInfo
        private void GetScreenListInfo()
        {
            _fLogService.Info("需要获取屏信息...");
            WriteLog("需要获取屏信息...");
            if (_moniDatareader == null)
            {
                _fLogService.Error("获取屏失败，原因硬件为空...");
                WriteLog("获取屏失败，原因硬件为空...");
                return;
            }
            _screenInfos = _moniDatareader.GetAllScreenInfo();
            if (_screenInfos == null)
            {
                _fLogService.Error("获取屏对象为Null");
                return;
            }
            _fLogService.Debug("Mars给的屏个数为：" + _screenInfos.Count);
            List<LedBasicInfo> LedDeviceInfos = new List<LedBasicInfo>();
            LedBasicInfo lInfo = new LedBasicInfo();
            for (int i = 0; i < _screenInfos.Count; i++)
            {
                LedDeviceInfos.Add(DataConverter(_screenInfos[i]));
            }
            SendAllPhysicalDisplayInfo();
            SendData("ScreenBaseInfoList", CommandTextParser.GetJsonSerialization<List<LedBasicInfo>>(LedDeviceInfos));
            WriteLog("对外已发出屏信息...");
            _fLogService.Info("对外已发出屏信息...");
        }
        private void SendAllPhysicalDisplayInfo()
        {
            WriteLog("获取各屏的LCT信息...");
            Dictionary<string, List<ILEDDisplayInfo>> ledInfos = _moniDatareader.GetAllCommPortLedDisplay();
            LEDDisplayInfoCollection lct_list = new LEDDisplayInfoCollection();
            if (ledInfos == null)
            {
                _fLogService.Info("获取各屏的LCT信息为空...");
                return;
            }
            foreach (KeyValuePair<string, List<ILEDDisplayInfo>> keyValue in ledInfos)
            {
                foreach (ILEDDisplayInfo led in keyValue.Value)
                {
                    switch (led.Type)
                    {
                        case LEDDisplyType.SimpleSingleType:
                            if (!lct_list.LedSimples.ContainsKey(keyValue.Key))
                            {
                                lct_list.LedSimples.Add(keyValue.Key, new List<SimpleLEDDisplayInfo>());
                            }
                            SimpleLEDDisplayInfo simple = (led as SimpleLEDDisplayInfo);
                            lct_list.LedSimples[keyValue.Key].Add(simple);
                            break;
                        case LEDDisplyType.StandardType:
                            if (!lct_list.LedStandards.ContainsKey(keyValue.Key))
                            {
                                lct_list.LedStandards.Add(keyValue.Key, new List<StandardLEDDisplayInfo>());
                            }
                            StandardLEDDisplayInfo standard = (led as StandardLEDDisplayInfo);
                            lct_list.LedStandards[keyValue.Key].Add(standard);
                            break;
                        case LEDDisplyType.ComplexType:
                            if (!lct_list.LedComplex.ContainsKey(keyValue.Key))
                            {
                                lct_list.LedComplex.Add(keyValue.Key, new List<ComplexLEDDisplayInfo>());
                            }
                            ComplexLEDDisplayInfo complex = (led as ComplexLEDDisplayInfo);
                            lct_list.LedComplex[keyValue.Key].Add(complex);
                            break;
                    }
                }
            }
            SendData("AllPhysicalDisplayInfo", CommandTextParser.GetJsonSerialization<LEDDisplayInfoCollection>(lct_list));
            WriteLog("对外发送各屏的LCT信息...");
            _fLogService.Info("对外发送各屏的LCT信息...");
        }
        private LedBasicInfo DataConverter(ScreenInfo sInfo)
        {
            if (sInfo == null) return null;
            LedBasicInfo lInfo = new LedBasicInfo();
            lInfo.Width = sInfo.LedWidth;
            lInfo.Height = sInfo.LedHeight;
            lInfo.Sn = sInfo.LedSN;
            lInfo.PointCount = sInfo.PointCount;
            lInfo.LedIndexOfCom = sInfo.LedIndexOfCom;
            lInfo.Commport = sInfo.Commport;
            lInfo.IsSupportPointDetect = sInfo.IsSupportPointDetect;
            lInfo.PartInfos = new List<PartInfo>();
            lInfo.PartInfos.Add(new PartInfo() { Type = DeviceType.SendCard, Amount = sInfo.SenderCardCount });
            lInfo.PartInfos.Add(new PartInfo() { Type = DeviceType.ReceiverCard, Amount = sInfo.ScannerCardCount });
            lInfo.PartInfos.Add(new PartInfo() { Type = DeviceType.MonitoringCard, Amount = sInfo.MonitorCardCount });
            return lInfo;
        }

        #endregion
        #region Monitor data read
        private void ReadMonitorData_First()
        {
            if (Interlocked.Exchange(ref _isReadingMonitorData, 1) == 0)
            {
                _senderLocList.Clear();
                _scannerLocList.Clear();
                //_ledAlarmConfigDic.Clear();
                _isNeedRetry = false;
                _dataType = DataType.all;
                if (_screenInfos == null || _screenInfos.Count == 0)
                {
                    _fLogService.Error("DataReadTimer_Elapsed->:显示屏信息为空，重启数据源：");
                    //修复逻辑：取消重新读取，直接提示用户无屏通知
                    SendData("M3_StateData", "NoScreenInfo");
                    _fLogService.Debug("ReadMonitorData_First BusyWorking释放");
                    Interlocked.Exchange(ref _isReadingMonitorData, 0);
                    //AsyncRestartDataReader(false);
                }
                else
                {
                    Action action = new Action(() =>
                    {
                        ReadMonitorData(null);
                    });
                    action.BeginInvoke(null, null);
                    //action.Invoke();
                }
            }
            else
            {
                _fLogService.Debug("ReadMonitorData_First->:已经在查询数据了");
            }
        }
        FileLogService _fLogService = new FileLogService(typeof(DataAcquisition));
        private void DataReadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReadMonitorData_First();
        }
        private SerializableDictionary<string, List<byte>> _senderRetryInfoDic;
        private SerializableDictionary<string, List<ScanBoardRegionInfo>> _scanRetryInfoDic;
        private void ReadDataCallback(CompletedMonitorCallbackParams cmpParams, object userToken)
        {
            if (userToken != null)
            {
                SenderMonitorDataNeedRetry(cmpParams, _ledAlarmConfigDic, out _senderRetryInfoDic);
                if (_senderRetryInfoDic.Count != 0)
                {
                    _isNeedRetry = true;
                    _dataType = DataType.senderData;
                    return;
                }
                else
                {
                    ScannerMonitorDataNeedRetry(cmpParams, _ledAlarmConfigDic, out _scanRetryInfoDic);
                    if (_scanRetryInfoDic.Count != 0)
                    {
                        _isNeedRetry = true;
                        _dataType = DataType.scannerData;
                        return;
                    }
                }
            }
            _isNeedRetry = false;
            try
            {
                if (cmpParams != null && cmpParams.MonitorData != null
               && cmpParams.MonitorData.AllScreenMonitorCollection != null && cmpParams.MonitorData.AllScreenMonitorCollection.Count > 0)
                {
                    SendData("M3_MonitoringData",
                        CommandTextParser.GetJsonSerialization<AllMonitorData>(cmpParams.MonitorData));
                    SendData("M3_StateData", "ReadMonitoringSuccess");
                }
                else
                {
                    SendData("M3_MonitoringData",
                        CommandTextParser.GetJsonSerialization<AllMonitorData>(cmpParams.MonitorData));
                    SendData("M3_StateData", "ReadMonitoringFailed");
                }

                WriteLog("ReadDataCallback回调开始，首先获取计算机的相关参数...");
                #region pc hardware information
                string pcInfo = string.Empty;
                pcInfo += _sysInfo.CpuLoad;
                pcInfo += "+" + _sysInfo.PhysicalMemory;
                pcInfo += "+" + _sysInfo.MemoryAvailable;

                List<HDDInfo> hInfoList = _sysInfo.GetLogicalDrives();
                foreach (var hddInfo in hInfoList)
                {
                    pcInfo += "+" + hddInfo.DiskName + "-" + hddInfo.TotalSize + "-" + hddInfo.FreeSpace;
                }
                DataPoint pcDP = new DataPoint();
                pcDP.Key = ByteToHexStrX1(new byte[] { (byte)HWDeviceType.ComputerHWInfo })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.ComputerStatus })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)0 })
                                           + "|" + ByteToHexStrX2(new byte[] { (byte)1 });
                pcDP.Value = pcInfo;
                #endregion
                List<AutoReadResultData> brightList = _moniDatareader.AutoReadResultDatas();
                WriteLog("ReadDataCallback将数据对外发出...");
                Dictionary<string, object> allScreenMonitorData;
                List<DataPoint> dpList;
                if (MonitorDataConverter(cmpParams, out allScreenMonitorData))
                {
                    if (allScreenMonitorData.Count != 0)
                    {
                        dpList = new List<DataPoint>();
                        foreach (var item in allScreenMonitorData.Keys)
                        {
                            dpList = new List<DataPoint>();
                            foreach (var dataPoint in ((Dictionary<string, object>)allScreenMonitorData[item]).Keys)
                            {
                                DataPoint dp = new DataPoint();
                                dp.Key = dataPoint;
                                dp.Value = ((Dictionary<string, object>)allScreenMonitorData[item])[dataPoint].ToString();
                                dpList.Add(dp);
                            }

                            #region brightness
                            DataPoint pcDPBright = new DataPoint();
                            DataPoint pcDPSensorBright = new DataPoint();
                            if (brightList != null && brightList.Count != 0)
                            {
                                AutoReadResultData brightnessData = brightList.Find(a => a.SN == item);
                                if (brightnessData != null && brightnessData.AutoBrightInfo != null)
                                {
                                    if (brightnessData.AutoBrightInfo.IsSucess)
                                    {
                                        pcDPBright.Value = brightnessData.AutoBrightInfo.BrightValue.ToString();
                                    }
                                    else
                                    {
                                        pcDPBright.Value = "-1";
                                    }
                                }
                                if (brightnessData != null && brightnessData.AutoSensorInfo != null)
                                {
                                    if (brightnessData.AutoSensorInfo.IsSucess)
                                    {
                                        pcDPSensorBright.Value = brightnessData.AutoSensorInfo.SensorValue.ToString();
                                    }
                                    else
                                    {
                                        pcDPSensorBright.Value = "-1";
                                    }
                                }


                            }
                            else
                            {
                                pcDPBright.Value = "-1";
                                pcDPSensorBright.Value = "-1";
                            }
                            pcDPBright.Key = ByteToHexStrX1(new byte[] { (byte)HWDeviceType.Screen })
                                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.brightness })
                                                       + "|" + ByteToHexStrX1(new byte[] { (byte)0 })
                                                       + "|" + ByteToHexStrX2(new byte[] { (byte)1 });
                            pcDPSensorBright.Key = ByteToHexStrX1(new byte[] { (byte)HWDeviceType.Screen })
                                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.EnvironmentBrightness })
                                                       + "|" + ByteToHexStrX1(new byte[] { (byte)0 })
                                                       + "|" + ByteToHexStrX2(new byte[] { (byte)1 });
                            #endregion
                            dpList.Add(pcDP);
                            dpList.Add(pcDPBright);
                            dpList.Add(pcDPSensorBright);
                            SendData("MonitoringData", new DataPoint() { Key = item, Value = dpList });
                        }
                    }
                }
                WriteLog("ReadDataCallback将数据对外发出完成...");
            }
            catch (Exception ex)
            {
                WriteLog("ReadDataCallback将数据返回时出现异常：" + ex.ToString());
                _fLogService.Error("ExistCatch:ReadDataCallback将数据返回时出现异常：" + ex.ToString());
            }
            finally
            {
                _fLogService.Debug("ReadDataCallback BusyWorking释放");
                Interlocked.Exchange(ref _isReadingMonitorData, 0);
            }
        }
        private bool MonitorDataConverter(CompletedMonitorCallbackParams monitorData, out Dictionary<string, object> allScreenMonitorData)
        {
            allScreenMonitorData = new Dictionary<string, object>();
            if (monitorData == null) return false;
            StringBuilder pathSB = new StringBuilder();
            byte keyByte = 0;
            List<byte> pathByteArr = new List<byte>();
            List<byte> tmpArr;
            if (monitorData.MonitorData.AllScreenMonitorCollection.Count == 0) return false;
            string key;
            foreach (var sData in monitorData.MonitorData.AllScreenMonitorCollection)
            {
                Dictionary<string, object> terMonitorData = new Dictionary<string, object>();
                #region sender card
                foreach (var senderData in sData.SenderMonitorCollection)//sender monitor data list
                {
                    keyByte = (byte)HWDeviceType.Sender;//equip type
                    pathByteArr.Clear();
                    for (int i = 0; i < senderData.MappingList.Count; i++)
                    {
                        pathByteArr.Add((byte)(int)senderData.MappingList[i].Device);
                        pathByteArr.Add((byte)senderData.MappingList[i].DeviceIndex);
                    }
                    tmpArr = new List<byte>(pathByteArr);
                    tmpArr.Add((byte)(int)PhysicalType.workstatus);
                    tmpArr.Add((byte)0);
                    key = ByteToHexStrX1(new byte[] { keyByte })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.workstatus })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                       + "|" + ByteToHexStrX2(tmpArr.ToArray());
                    if (terMonitorData.ContainsKey(key)) continue;
                    terMonitorData.Add(key, (int)senderData.DeviceStatus);
                    if (senderData.DeviceStatus == DeviceWorkStatus.OK)
                    {
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.connectedstatus);
                        tmpArr.Add((byte)0);
                        key = ByteToHexStrX1(new byte[] { keyByte })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.connectedstatus })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                           + "|" + ByteToHexStrX2(tmpArr.ToArray());
                        if (terMonitorData.ContainsKey(key)) continue;
                        terMonitorData.Add(key, Convert.ToInt32(!senderData.IsDviConnected));

                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.dviRate);
                        tmpArr.Add((byte)0);
                        key = ByteToHexStrX1(new byte[] { keyByte })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.dviRate })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                               + "|" + ByteToHexStrX2(tmpArr.ToArray());
                        if (terMonitorData.ContainsKey(key)) continue;
                        terMonitorData.Add(key, (int)senderData.DviRate);
                        #region Sender Card Port
                        foreach (var portInfo in senderData.ReduPortIndexCollection)
                        {
                            pathByteArr.Clear();
                            for (int i = 0; i < portInfo.MappingList.Count; i++)
                            {
                                pathByteArr.Add((byte)(int)portInfo.MappingList[i].Device);
                                pathByteArr.Add((byte)portInfo.MappingList[i].DeviceIndex);
                            }
                            tmpArr = new List<byte>(pathByteArr);
                            tmpArr.Add((byte)(int)PhysicalType.PortOfSenderStatus);
                            tmpArr.Add((byte)0);
                            key = ByteToHexStrX1(new byte[] { keyByte })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.PortOfSenderStatus })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                                   + "|" + ByteToHexStrX2(tmpArr.ToArray());
                            if (terMonitorData.ContainsKey(key)) continue;
                            terMonitorData.Add(key, Convert.ToInt32(portInfo.IsReduState));
                        }
                        #endregion
                    }
                }
                #endregion
                #region Deconcentrator
                foreach (var deconcentratorData in sData.DeconcentratorCollection)
                {
                    pathByteArr.Clear();
                    for (int i = 0; i < deconcentratorData.MappingList.Count; i++)
                    {
                        pathByteArr.Add((byte)(int)deconcentratorData.MappingList[i].Device);
                        pathByteArr.Add((byte)deconcentratorData.MappingList[i].DeviceIndex);
                    }
                    tmpArr = new List<byte>(pathByteArr);
                    tmpArr.Add((byte)(int)PhysicalType.workstatus);
                    tmpArr.Add((byte)0);
                    key = ByteToHexStrX1(new byte[] { keyByte })
                       + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.workstatus })
                       + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                       + "|" + ByteToHexStrX2(tmpArr.ToArray());
                    if (terMonitorData.ContainsKey(key))
                        terMonitorData.Add(key, (int)deconcentratorData.DeviceStatus);
                }
                #endregion
                #region scanner card
                keyByte = (byte)HWDeviceType.Scanner;//equip type
                foreach (var scannerData in sData.ScannerMonitorCollection)
                {
                    pathByteArr.Clear();
                    for (int i = 0; i < scannerData.MappingList.Count; i++)
                    {
                        pathByteArr.Add((byte)(int)scannerData.MappingList[i].Device);
                        pathByteArr.Add((byte)scannerData.MappingList[i].DeviceIndex);
                    }
                    tmpArr = new List<byte>(pathByteArr);
                    tmpArr.Add((byte)(int)PhysicalType.workstatus);
                    tmpArr.Add((byte)0);

                    key = ByteToHexStrX1(new byte[] { keyByte })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.workstatus })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                       + "|" + ByteToHexStrX2(tmpArr.ToArray());

                    if (terMonitorData.ContainsKey(key)) continue;
                    terMonitorData.Add(key, (int)scannerData.DeviceStatus);
                    if (scannerData.DeviceStatus == DeviceWorkStatus.OK)
                    {
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.temperature);
                        tmpArr.Add((byte)0);
                        key = ByteToHexStrX1(new byte[] { keyByte })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.temperature })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                           + "|" + ByteToHexStrX2(tmpArr.ToArray());
                        if (terMonitorData.ContainsKey(key)) continue;
                        terMonitorData.Add(key, scannerData.Temperature);
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.voltage);
                        tmpArr.Add((byte)0);
                        key = ByteToHexStrX1(new byte[] { keyByte })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.voltage })
                                           + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                           + "|" + ByteToHexStrX2(tmpArr.ToArray());
                        if (terMonitorData.ContainsKey(key)) continue;
                        terMonitorData.Add(key, scannerData.Voltage);
                    }
                }
                #endregion
                #region function card
                keyByte = (byte)HWDeviceType.FunctionCard;//equip type
                foreach (var funCardData in sData.FunctionCardInfoCollection)
                {
                    pathByteArr.Clear();
                    for (int i = 0; i < funCardData.MappingList.Count; i++)
                    {
                        pathByteArr.Add((byte)(int)funCardData.MappingList[i].Device);
                        pathByteArr.Add((byte)funCardData.MappingList[i].DeviceIndex);
                    }
                    tmpArr = new List<byte>(pathByteArr);
                    tmpArr.Add((byte)(int)PhysicalType.workstatus);
                    tmpArr.Add((byte)0);
                    key = ByteToHexStrX1(new byte[] { keyByte })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.workstatus })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                       + "|" + ByteToHexStrX2(tmpArr.ToArray());
                    if (terMonitorData.ContainsKey(key)) continue;
                    terMonitorData.Add(key, (int)funCardData.DeviceStatus);
                    if (funCardData.DeviceStatus == DeviceWorkStatus.OK)
                    {
                        List<byte> funCardPathByteArr = new List<byte>();
                        //functionCard PeripheralInfo
                        foreach (var index in funCardData.PeripheralInfoDict.Keys)
                        {
                            PeripheralMonitorBaseInfo perInfo = funCardData.PeripheralInfoDict[index];
                            if (perInfo is LightSensorMonitorInfo)
                            {
                                if (perInfo.DeviceStatus == DeviceWorkStatus.OK)
                                {
                                    tmpArr = new List<byte>(pathByteArr);
                                    tmpArr.Add((byte)(int)PhysicalType.brightness);
                                    tmpArr.Add((byte)(byte)index);
                                    LightSensorMonitorInfo lightSensorInfo = perInfo as LightSensorMonitorInfo;
                                    key = ByteToHexStrX1(new byte[] { keyByte })
                                          + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.brightness })
                                          + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                          + "|" + ByteToHexStrX2(tmpArr.ToArray());
                                    if (terMonitorData.ContainsKey(key)) continue;
                                    terMonitorData.Add(key, lightSensorInfo.Lux);
                                }
                            }
                            else if (perInfo is TemperatureSensorMonitorInfo)
                            {
                                if (perInfo.DeviceStatus == DeviceWorkStatus.OK)
                                {
                                    tmpArr = new List<byte>(pathByteArr);
                                    tmpArr.Add((byte)(int)PhysicalType.temperature);
                                    tmpArr.Add((byte)(byte)index);
                                    TemperatureSensorMonitorInfo temSensorInfo = perInfo as TemperatureSensorMonitorInfo;
                                    key = ByteToHexStrX1(new byte[] { keyByte })
                                          + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.temperature })
                                          + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                          + "|" + ByteToHexStrX2(tmpArr.ToArray());
                                    if (terMonitorData.ContainsKey(key)) continue;
                                    terMonitorData.Add(key, temSensorInfo.Tempearture);
                                }
                            }
                            else if (perInfo is HumiditySensorMonitorInfo)
                            {
                                if (perInfo.DeviceStatus == DeviceWorkStatus.OK)
                                {
                                    tmpArr = new List<byte>(pathByteArr);
                                    tmpArr.Add((byte)(int)PhysicalType.humidity);
                                    tmpArr.Add((byte)(byte)index);
                                    HumiditySensorMonitorInfo humiditySensorInfo = perInfo as HumiditySensorMonitorInfo;
                                    key = ByteToHexStrX1(new byte[] { keyByte })
                                          + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.humidity })
                                          + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                          + "|" + ByteToHexStrX2(tmpArr.ToArray());
                                    if (terMonitorData.ContainsKey(key)) continue;
                                    terMonitorData.Add(key, humiditySensorInfo.Humidity);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region monitor card
                keyByte = (byte)(HWDeviceType.MonitorCard);//equip type
                foreach (var monitorCardData in sData.MonitorCardInfoCollection)
                {
                    if (monitorCardData == null) continue;
                    pathByteArr.Clear();
                    for (int i = 0; i < monitorCardData.MappingList.Count; i++)
                    {
                        pathByteArr.Add((byte)(int)monitorCardData.MappingList[i].Device);
                        pathByteArr.Add((byte)monitorCardData.MappingList[i].DeviceIndex);
                    }
                    #region common data
                    tmpArr = new List<byte>(pathByteArr);
                    tmpArr.Add((byte)(int)PhysicalType.workstatus);
                    tmpArr.Add((byte)0);
                    key = ByteToHexStrX1(new byte[] { keyByte })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.workstatus })
                                       + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                       + "|" + ByteToHexStrX2(tmpArr.ToArray());
                    if (terMonitorData.ContainsKey(key)) continue;
                    terMonitorData.Add(key, (int)monitorCardData.DeviceStatus);

                    if (monitorCardData.DeviceStatus == DeviceWorkStatus.OK)
                    {
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.humidity);
                        tmpArr.Add((byte)0);
                        if (monitorCardData.HumidityUInfo != null && monitorCardData.HumidityUInfo.IsUpdate)
                        {
                            key = ByteToHexStrX1(new byte[] { keyByte })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.humidity })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                               + "|" + ByteToHexStrX2(tmpArr.ToArray());
                            if (terMonitorData.ContainsKey(key)) continue;
                            terMonitorData.Add(key, monitorCardData.HumidityUInfo.Humidity);
                        }
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.doorOpenStatus);
                        tmpArr.Add((byte)0);
                        if (monitorCardData.CabinetDoorUInfo != null && monitorCardData.CabinetDoorUInfo.IsUpdate)
                        {
                            key = ByteToHexStrX1(new byte[] { keyByte })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.doorOpenStatus })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                               + "|" + ByteToHexStrX2(tmpArr.ToArray());
                            if (terMonitorData.ContainsKey(key)) continue;
                            terMonitorData.Add(key, Convert.ToInt32(monitorCardData.CabinetDoorUInfo.IsDoorOpen));
                        }
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.smoke);
                        tmpArr.Add((byte)0);
                        if (monitorCardData.SmokeUInfo != null && monitorCardData.SmokeUInfo.IsUpdate)
                        {
                            key = ByteToHexStrX1(new byte[] { keyByte })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.smoke })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                               + "|" + ByteToHexStrX2(tmpArr.ToArray());
                            if (terMonitorData.ContainsKey(key)) continue;
                            terMonitorData.Add(key, Convert.ToInt32(monitorCardData.SmokeUInfo.IsSmokeAlarm));
                        }
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.temperature);
                        tmpArr.Add((byte)0);
                        if (monitorCardData.TemperatureUInfo != null && monitorCardData.TemperatureUInfo.IsUpdate)
                        {
                            key = ByteToHexStrX1(new byte[] { keyByte })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.temperature })
                                               + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                               + "|" + ByteToHexStrX2(tmpArr.ToArray());
                            if (terMonitorData.ContainsKey(key)) continue;
                            terMonitorData.Add(key, monitorCardData.TemperatureUInfo.Temperature);
                        }
                        tmpArr = new List<byte>(pathByteArr);
                        tmpArr.Add((byte)(int)PhysicalType.SocketCableStatus);
                        tmpArr.Add((byte)0);
                        if (monitorCardData.SocketCableUInfo != null)
                        {
                            WorkStatusType socketCableStatus = IsSocketCableStatusOK(monitorCardData);
                            if (socketCableStatus != WorkStatusType.Unknown)
                            {
                                key = ByteToHexStrX1(new byte[] { keyByte })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.SocketCableStatus })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                                   + "|" + ByteToHexStrX2(tmpArr.ToArray());
                                if (terMonitorData.ContainsKey(key)) continue;
                                terMonitorData.Add(key, (int)socketCableStatus);
                            }
                        }
                    #endregion
                        #region list data
                        //monitorCard voltage
                        if (monitorCardData.PowerUInfo != null && monitorCardData.PowerUInfo.IsUpdate)
                        {
                            foreach (var monitorPower in monitorCardData.PowerUInfo.PowerMonitorInfoCollection)
                            {
                                tmpArr = new List<byte>(pathByteArr);
                                tmpArr.Add((byte)(int)PhysicalType.voltage);
                                tmpArr.Add((byte)monitorPower.Key);
                                key = ByteToHexStrX1(new byte[] { keyByte })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.voltage })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                                   + "|" + ByteToHexStrX2(tmpArr.ToArray());
                                if (terMonitorData.ContainsKey(key)) continue;
                                terMonitorData.Add(key, monitorPower.Value);
                            }
                        }
                        //monitorCard Fan
                        if (monitorCardData.FansUInfo != null && monitorCardData.FansUInfo.IsUpdate)
                        {
                            foreach (var monitorFan in monitorCardData.FansUInfo.FansMonitorInfoCollection)
                            {
                                tmpArr = new List<byte>(pathByteArr);
                                tmpArr.Add((byte)(int)PhysicalType.fanspeed);
                                tmpArr.Add((byte)monitorFan.Key);
                                key = ByteToHexStrX1(new byte[] { keyByte })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(int)PhysicalType.fanspeed })
                                                   + "|" + ByteToHexStrX1(new byte[] { (byte)(tmpArr.Count / 2) })
                                                   + "|" + ByteToHexStrX2(tmpArr.ToArray());
                                if (terMonitorData.ContainsKey(key)) continue;
                                terMonitorData.Add(key, monitorFan.Value);
                            }
                        }
                        //monitorCard SocketCable
                        //if (monitorCardData.SocketCableUInfo.IsUpdate)
                        //{
                        //    foreach (var socketcableInfo in monitorCardData.SocketCableUInfo.SocketCableInfoCollection)
                        //    {
                        //        if (socketcableInfo.DeviceStatus == DeviceWorkStatus.OK) continue;
                        //        socketcableInfo.SocketCableInfoDict
                        //    }
                        //}
                        #endregion
                    }
                }
                #endregion
                if (!allScreenMonitorData.ContainsKey(sData.ScreenUDID))
                    allScreenMonitorData.Add(sData.ScreenUDID, terMonitorData);
            }
            return true;
        }
        /// <summary>
        /// 解析监控卡排线状态
        /// </summary>
        /// <param name="monitorCardInfo"></param>
        /// <returns></returns>
        private WorkStatusType IsSocketCableStatusOK(MonitorCardMonitorInfo monitorCardInfo)
        {
            if (!monitorCardInfo.SocketCableUInfo.IsUpdate) return WorkStatusType.Unknown;
            if (monitorCardInfo.SocketCableUInfo != null
                                && monitorCardInfo.SocketCableUInfo.SocketCableInfoCollection != null)
            {
                foreach (SocketCableMonitorInfo socket in monitorCardInfo.SocketCableUInfo.SocketCableInfoCollection)
                {
                    if (socket.SocketCableInfoDict == null || socket.SocketCableInfoDict.Count == 0)
                        return WorkStatusType.Error;

                    foreach (List<SocketCableStatus> socketCables in socket.SocketCableInfoDict.Values)
                    {
                        foreach (SocketCableStatus socketcable in socketCables)
                        {
                            if (!socketcable.IsCableOK) return WorkStatusType.Error;
                        }
                    }
                }
            }
            return WorkStatusType.OK;
        }
        #region 数据合并
        private void SenderDataMerge(ref CompletedMonitorCallbackParams allData, CompletedMonitorCallbackParams senderData, SerializableDictionary<string, List<byte>> senderInfoDic)
        {//List<PhysicalType>
            if (senderData == null || senderData.MonitorData == null || senderData.MonitorData.AllScreenMonitorCollection == null || senderData.MonitorData.AllScreenMonitorCollection.Count == 0) return;
            ScreenModnitorData screenData;
            int senderIndex;
            Dictionary<int, SenderMonitorInfo> senderMonitoIndexList = new Dictionary<int, SenderMonitorInfo>();
            foreach (var item in senderData.MonitorData.AllScreenMonitorCollection)
            {
                screenData = allData.MonitorData.AllScreenMonitorCollection.Find(a => a.ScreenUDID == item.ScreenUDID);
                if (screenData == null)
                {
                    allData.MonitorData.AllScreenMonitorCollection.Add(item);
                    continue;
                }
                //合并发送卡数据
                foreach (var sender in item.SenderMonitorCollection)
                {
                    if (_senderLocList.Find(a => a.CommPort == sender.DeviceRegInfo.CommPort && a.SenderIndex == sender.DeviceRegInfo.SenderIndex) == null) continue;
                    senderIndex = screenData.SenderMonitorCollection.FindIndex(a => (a.DeviceRegInfo.CommPort == sender.DeviceRegInfo.CommPort && a.DeviceRegInfo.SenderIndex == sender.DeviceRegInfo.SenderIndex));
                    if (senderIndex < 0)
                        screenData.SenderMonitorCollection.Add(sender);
                    else
                    {
                        try
                        {
                            screenData.SenderMonitorCollection[senderIndex] = sender;
                        }
                        catch (Exception ex)
                        {
                            _fLogService.Error("ExistCatch:SenderDataMerge->:发送卡数据合并异常：" + ex.ToString());
                        }
                    }
                }
            }
            //合并监控终端本地UI展示所需数据
            if (allData.MonitorData.RedundantStateType != null)
            {
                foreach (var item in senderData.MonitorData.RedundantStateType)
                {
                    if (!allData.MonitorData.RedundantStateType.ContainsKey(item.Key))
                    {
                        allData.MonitorData.RedundantStateType.Add(item.Key, item.Value);
                        continue;
                    }
                    foreach (var redundantState in item.Value)
                    {
                        if (!allData.MonitorData.RedundantStateType[item.Key].ContainsKey(redundantState.Key))
                            allData.MonitorData.RedundantStateType[item.Key].Add(redundantState.Key, redundantState.Value);
                        else allData.MonitorData.RedundantStateType[item.Key][redundantState.Key] = redundantState.Value;
                    }
                }
            }
            else allData.MonitorData.RedundantStateType = senderData.MonitorData.RedundantStateType;
            if (allData.MonitorData.CurAllSenderDVIDic != null)
            {
                foreach (var item in senderData.MonitorData.CurAllSenderDVIDic)
                {
                    if (!allData.MonitorData.CurAllSenderDVIDic.ContainsKey(item.Key))
                    {
                        allData.MonitorData.CurAllSenderDVIDic.Add(item.Key, item.Value);
                        continue;
                    }
                    if (!senderInfoDic.ContainsKey(item.Key)) continue;
                    try
                    {
                        for (int i = 0; i < senderInfoDic[item.Key].Count && i < item.Value.Count; i++)
                        {
                            if (i < allData.MonitorData.CurAllSenderDVIDic[item.Key].Count)
                                allData.MonitorData.CurAllSenderDVIDic[item.Key][(int)senderInfoDic[item.Key][i]] = item.Value[i];
                        }
                    }
                    catch (Exception ex)
                    {
                        _fLogService.Error("ExistCatch:SenderDataMerge->:CurAllSenderDVIDic数据合并异常：" + ex.ToString());
                    }
                }
            }
            else allData.MonitorData.CurAllSenderDVIDic = senderData.MonitorData.CurAllSenderDVIDic;
            if (allData.MonitorData.CurAllSenderStatusDic != null)
            {
                foreach (var item in senderData.MonitorData.CurAllSenderStatusDic)
                {
                    if (!allData.MonitorData.CurAllSenderStatusDic.ContainsKey(item.Key))
                    {
                        allData.MonitorData.CurAllSenderStatusDic.Add(item.Key, item.Value);
                        continue;
                    }
                    if (!senderInfoDic.ContainsKey(item.Key)) continue;
                    try
                    {
                        for (int i = 0; i < senderInfoDic[item.Key].Count && i < item.Value.Count; i++)
                        {
                            allData.MonitorData.CurAllSenderStatusDic[item.Key][(int)senderInfoDic[item.Key][i]] = item.Value[i];
                        }
                    }
                    catch (Exception ex)
                    {
                        _fLogService.Error("ExistCatch:SenderDataMerge->:CurAllSenderStatusDic数据合并异常：" + ex.ToString());
                    }
                }
            }
            else allData.MonitorData.CurAllSenderStatusDic = senderData.MonitorData.CurAllSenderStatusDic;
        }
        private void ScannerDataMerge(ref CompletedMonitorCallbackParams allData, CompletedMonitorCallbackParams scannerData)
        {
            if (scannerData == null || scannerData.MonitorData == null || scannerData.MonitorData.AllScreenMonitorCollection == null || scannerData.MonitorData.AllScreenMonitorCollection.Count == 0) return;
            ScreenModnitorData screenData;
            int index;
            //ScannerMonitorInfo scanner;
            //MonitorCardMonitorInfo monitor;
            foreach (var item in scannerData.MonitorData.AllScreenMonitorCollection)
            {
                screenData = allData.MonitorData.AllScreenMonitorCollection.Find(a => a.ScreenUDID == item.ScreenUDID);
                if (screenData == null)
                {
                    allData.MonitorData.AllScreenMonitorCollection.Add(item);
                    continue;
                }
                foreach (var scannerInfo in item.ScannerMonitorCollection)
                {
                    index = screenData.ScannerMonitorCollection.FindIndex(a => a.DeviceRegInfo.CommPort == scannerInfo.DeviceRegInfo.CommPort && a.DeviceRegInfo.SenderIndex == scannerInfo.DeviceRegInfo.SenderIndex && a.DeviceRegInfo.PortIndex == scannerInfo.DeviceRegInfo.PortIndex && a.DeviceRegInfo.ConnectIndex == scannerInfo.DeviceRegInfo.ConnectIndex);
                    if (index < 0) screenData.ScannerMonitorCollection.Add(scannerInfo);
                    else screenData.ScannerMonitorCollection[index] = scannerInfo;
                }
                foreach (var monitorInfo in item.MonitorCardInfoCollection)
                {
                    index = screenData.MonitorCardInfoCollection.FindIndex(a => a.DeviceRegInfo.CommPort == monitorInfo.DeviceRegInfo.CommPort && a.DeviceRegInfo.SenderIndex == monitorInfo.DeviceRegInfo.SenderIndex && a.DeviceRegInfo.PortIndex == monitorInfo.DeviceRegInfo.PortIndex && a.DeviceRegInfo.ConnectIndex == monitorInfo.DeviceRegInfo.ConnectIndex);
                    if (index < 0) screenData.MonitorCardInfoCollection.Add(monitorInfo);
                    else //monitor = monitorInfo;
                        screenData.MonitorCardInfoCollection[index] = monitorInfo;
                }
            }
            foreach (var item in scannerData.MonitorData.MonitorResInfDic)
            {
                if (allData.MonitorData.MonitorResInfDic.ContainsKey(item.Key))
                    allData.MonitorData.MonitorResInfDic[item.Key] = item.Value;
                else allData.MonitorData.MonitorResInfDic.Add(item.Key, item.Value);
            }
            foreach (var item in scannerData.MonitorData.MonitorDataDic)
            {
                if (!allData.MonitorData.MonitorDataDic.ContainsKey(item.Key))
                    allData.MonitorData.MonitorDataDic.Add(item.Key, item.Value);
                else
                {
                    foreach (var scanMoniData in item.Value)
                    {
                        if (allData.MonitorData.MonitorDataDic[item.Key].ContainsKey(scanMoniData.Key))
                            allData.MonitorData.MonitorDataDic[item.Key][scanMoniData.Key] = scanMoniData.Value;
                        else allData.MonitorData.MonitorDataDic[item.Key].Add(scanMoniData.Key, scanMoniData.Value);
                    }
                }
            }
        }
        #endregion
        #region 重新读取监控数据
        private void BeginReadSenderCardData(SerializableDictionary<string, List<byte>> senderInfoDic)
        {
            if (_moniDatareader.BeginRetryReadSenderData(senderInfoDic, new CompletedMonitorCallback((cmpParams, userToken) =>
            {
                SenderDataMerge(ref _cmpParams, cmpParams, senderInfoDic);
                ReadDataCallback(_cmpParams, userToken);
            }
            ), DataType.senderData) != ReadMonitorDataErrType.OK)
                ReadDataCallback(_cmpParams, null);

        }
        private void BeginReadScanBoardData(SerializableDictionary<string, List<ScanBoardRegionInfo>> scanInfoDic)
        {
            if (_moniDatareader.BeginRetryReadScannerData(scanInfoDic, new CompletedMonitorCallback((cmpParams, userToken) =>
            {
                ScannerDataMerge(ref _cmpParams, cmpParams);
                ReadDataCallback(_cmpParams, userToken);
            }
            ), DataType.scannerData) != ReadMonitorDataErrType.OK)
                ReadDataCallback(_cmpParams, null);

        }
        private void SenderMonitorDataNeedRetry(CompletedMonitorCallbackParams cmpParams, Dictionary<string, LedAlarmConfig> ledAlarmConfigDic, out SerializableDictionary<string, List<byte>> senderInfoDic)
        {
            senderInfoDic = new SerializableDictionary<string, List<byte>>();
            List<RepetDataInfo> senderLocList = new List<RepetDataInfo>();
            if (_retryCount < 1) return;
            if (cmpParams == null || cmpParams.MonitorData == null || cmpParams.MonitorData.AllScreenMonitorCollection == null || cmpParams.MonitorData.AllScreenMonitorCollection.Count == 0) return;
            LedAlarmConfig ledAlarm;
            RepetDataInfo senderLocInfo;
            try
            {
                #region 监控数据告警判断
                foreach (var item in cmpParams.MonitorData.AllScreenMonitorCollection)
                {
                    if (!ledAlarmConfigDic.ContainsKey(item.ScreenUDID)) continue;
                    ledAlarm = ledAlarmConfigDic[item.ScreenUDID];
                    //判断发送卡监控数据是否需要重读
                    foreach (var sender in item.SenderMonitorCollection)
                    {
                        senderLocInfo = new RepetDataInfo();
                        senderLocInfo.CommPort = sender.DeviceRegInfo.CommPort;
                        senderLocInfo.SenderIndex = sender.DeviceRegInfo.SenderIndex;
                        if (senderLocList.Find(a => a.CommPort == senderLocInfo.CommPort && a.SenderIndex == senderLocInfo.SenderIndex) == null) continue;
                        if (sender.DeviceStatus != DeviceWorkStatus.OK)
                            senderLocList.Add(senderLocInfo);
                        else if (!sender.IsDviConnected)
                            senderLocList.Add(senderLocInfo);
                        else if (IsPortOfSenderReduState(sender.ReduPortIndexCollection))
                            senderLocList.Add(senderLocInfo);
                    }
                }
                #endregion
                if (senderLocList.Count == 0) return;
                int index;
                foreach (var item in senderLocList)
                {
                    senderLocInfo = _senderLocList.Find(a => a.CommPort == item.CommPort && a.SenderIndex == item.SenderIndex);
                    index = _senderLocList.FindIndex(a => a.CommPort == item.CommPort && a.SenderIndex == item.SenderIndex);

                    if (index < 0)
                    {
                        item.RetryCount++;
                        _senderLocList.Add(item);
                    }
                    else if (senderLocInfo.RetryCount < _retryCount)
                    {
                        _senderLocList[index].RetryCount++;
                    }
                    else continue;
                    if (!senderInfoDic.ContainsKey(item.CommPort))
                    {
                        senderInfoDic.Add(item.CommPort, new List<byte>());
                    }
                    senderInfoDic[item.CommPort].Add(item.SenderIndex);
                    //if (senderLocInfo == null || senderLocInfo.RetryCount < _retryCount)
                    //{
                    //    item.RetryCount++;
                    //    _senderLocList.Add(item);
                    //    if (!senderInfoDic.ContainsKey(item.CommPort))
                    //    {
                    //        senderInfoDic.Add(item.CommPort, new List<byte>());
                    //    }
                    //    senderInfoDic[item.CommPort].Add(item.SenderIndex);
                    //}
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SenderMonitorDataNeedRetry error：" + ex.ToString());
                _fLogService.Error("ExistCatch:SenderMonitorDataNeedRetry error：" + ex.ToString());
            }
        }
        private void ScannerMonitorDataNeedRetry(CompletedMonitorCallbackParams cmpParams, Dictionary<string, LedAlarmConfig> ledAlarmConfigDic, out SerializableDictionary<string, List<ScanBoardRegionInfo>> scanInfoDic)
        {
            scanInfoDic = new SerializableDictionary<string, List<ScanBoardRegionInfo>>();
            List<RepetDataInfo> scannerLocList = new List<RepetDataInfo>();
            if (_retryCount < 1) return;
            if (cmpParams == null || cmpParams.MonitorData == null || cmpParams.MonitorData.AllScreenMonitorCollection == null || cmpParams.MonitorData.AllScreenMonitorCollection.Count == 0) return;
            LedAlarmConfig ledAlarm;
            ParameterAlarmConfig alarmConfig;
            RepetDataInfo scannerLocInfo;
            #region 监控数据告警判断
            try
            {
                foreach (var item in cmpParams.MonitorData.AllScreenMonitorCollection)
                {
                    if (!ledAlarmConfigDic.ContainsKey(item.ScreenUDID)) continue;
                    ledAlarm = ledAlarmConfigDic[item.ScreenUDID];
                    //判断接收卡监控数据是否需要重读
                    foreach (var scanner in item.ScannerMonitorCollection)
                    {
                        scannerLocInfo = new RepetDataInfo();
                        scannerLocInfo.CommPort = scanner.DeviceRegInfo.CommPort;
                        scannerLocInfo.SenderIndex = scanner.DeviceRegInfo.SenderIndex;
                        scannerLocInfo.PortIndex = scanner.DeviceRegInfo.PortIndex;
                        scannerLocInfo.ScannerIndex = scanner.DeviceRegInfo.ConnectIndex;
                        if (scanner.DeviceStatus != DeviceWorkStatus.OK)
                        {
                            scannerLocList.Add(scannerLocInfo);
                            continue;
                        }
                        alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Voltage && a.Level == AlarmLevel.Malfunction);
                        if (alarmConfig != null)
                        {
                            if (scanner.Voltage < alarmConfig.HighThreshold)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                        alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Voltage && a.Level == AlarmLevel.Warning);
                        if (alarmConfig != null)
                        {
                            if (scanner.Voltage < alarmConfig.LowThreshold || scanner.Voltage > alarmConfig.HighThreshold)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                        alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Temperature);
                        if (alarmConfig != null)
                        {
                            if (scanner.Temperature < alarmConfig.HighThreshold)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                    }
                    //判断监控卡监控数据是否需要重读
                    foreach (var monitor in item.MonitorCardInfoCollection)
                    {
                        if (monitor == null || monitor.DeviceRegInfo == null)
                            continue;
                        if (scannerLocList.FindIndex(a => a.CommPort == monitor.DeviceRegInfo.CommPort && a.SenderIndex == monitor.DeviceRegInfo.SenderIndex && a.PortIndex == monitor.DeviceRegInfo.PortIndex && a.ScannerIndex == monitor.DeviceRegInfo.ConnectIndex) >= 0) continue;
                        scannerLocInfo = new RepetDataInfo();
                        scannerLocInfo.CommPort = monitor.DeviceRegInfo.CommPort;
                        scannerLocInfo.SenderIndex = monitor.DeviceRegInfo.SenderIndex;
                        scannerLocInfo.PortIndex = monitor.DeviceRegInfo.PortIndex;
                        scannerLocInfo.ScannerIndex = monitor.DeviceRegInfo.ConnectIndex;
                        if (monitor.DeviceStatus != DeviceWorkStatus.OK)
                        {
                            scannerLocList.Add(scannerLocInfo);
                            continue;
                        }
                        if (monitor.CabinetDoorUInfo != null && monitor.CabinetDoorUInfo.IsUpdate && monitor.CabinetDoorUInfo.IsDoorOpen)
                        {
                            scannerLocList.Add(scannerLocInfo);
                            continue;
                        }
                        if (monitor.SmokeUInfo != null && monitor.SmokeUInfo.IsUpdate && monitor.SmokeUInfo.IsSmokeAlarm)
                        {
                            scannerLocList.Add(scannerLocInfo);
                            continue;
                        }
                        if (monitor.TemperatureUInfo != null && monitor.TemperatureUInfo.IsUpdate)
                        {
                            alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Temperature);
                            if (alarmConfig != null && monitor.TemperatureUInfo.Temperature < alarmConfig.HighThreshold)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                        if (monitor.HumidityUInfo != null && monitor.HumidityUInfo.IsUpdate)
                        {
                            alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Humidity);
                            if (alarmConfig != null && monitor.HumidityUInfo.Humidity < alarmConfig.HighThreshold)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                        if (monitor.SocketCableUInfo != null)
                        {
                            if (IsSocketCableStatusOK(monitor) == WorkStatusType.Error)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                        if (monitor.FansUInfo != null && monitor.FansUInfo.IsUpdate)
                        {
                            alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.FanSpeed);
                            if (alarmConfig != null && monitor.FansUInfo.FansMonitorInfoCollection.Values.ToList().FindIndex(a => a < alarmConfig.LowThreshold) > 0)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                        if (monitor.PowerUInfo != null && monitor.PowerUInfo.IsUpdate)
                        {
                            alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Voltage && a.Level == AlarmLevel.Warning);
                            if (alarmConfig != null && monitor.PowerUInfo.PowerMonitorInfoCollection.Values.ToList().FindIndex(a => (a < alarmConfig.LowThreshold) || a > alarmConfig.HighThreshold) > 0)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }

                            alarmConfig = ledAlarm.ParameterAlarmConfigList.Find(a => a.ParameterType == StateQuantityType.Voltage && a.Level == AlarmLevel.Malfunction);
                            if (alarmConfig != null && monitor.PowerUInfo.PowerMonitorInfoCollection.Values.ToList().FindIndex(a => a < alarmConfig.HighThreshold) > 0)
                            {
                                scannerLocList.Add(scannerLocInfo);
                                continue;
                            }
                        }
                    }
                }
            #endregion
                if (scannerLocList.Count == 0) return;
                int index;
                ScanBoardRegionInfo reginInfo;
                foreach (var item in scannerLocList)
                {
                    scannerLocInfo = _scannerLocList.Find(a => a.CommPort == item.CommPort && a.SenderIndex == item.SenderIndex && a.PortIndex == item.PortIndex && a.ScannerIndex == item.ScannerIndex);
                    index = _scannerLocList.FindIndex(a => a.CommPort == item.CommPort && a.SenderIndex == item.SenderIndex && a.PortIndex == item.PortIndex && a.ScannerIndex == item.ScannerIndex);
                    if (index < 0)
                    {
                        item.RetryCount++;
                        _scannerLocList.Add(item);
                    }
                    else if (scannerLocInfo.RetryCount < _retryCount)
                    {
                        _scannerLocList[index].RetryCount++;
                    }
                    else continue;
                    if (!scanInfoDic.ContainsKey(item.CommPort))
                    {
                        scanInfoDic.Add(item.CommPort, new List<ScanBoardRegionInfo>());
                    }
                    reginInfo = new ScanBoardRegionInfo();
                    reginInfo.SenderIndex = item.SenderIndex;
                    reginInfo.PortIndex = item.PortIndex;
                    reginInfo.ConnectIndex = item.ScannerIndex;
                    scanInfoDic[item.CommPort].Add(reginInfo);
                    //if (scannerLocInfo == null || scannerLocInfo.RetryCount < _retryCount)
                    //{
                    //    item.RetryCount++;
                    //    _scannerLocList.Add(item);
                    //    if (!scanInfoDic.ContainsKey(item.CommPort))
                    //    {
                    //        scanInfoDic.Add(item.CommPort, new List<ScanBoardRegionInfo>());
                    //    }
                    //    reginInfo = new ScanBoardRegionInfo();
                    //    reginInfo.SenderIndex = item.SenderIndex;
                    //    reginInfo.PortIndex = item.PortIndex;
                    //    reginInfo.ConnectIndex = item.ScannerIndex;
                    //    scanInfoDic[item.CommPort].Add(reginInfo);
                    //}
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ScannerMonitorDataNeedRetry error：" + ex.ToString());
                _fLogService.Error("ExistCatch:ScannerMonitorDataNeedRetry error：" + ex.ToString());
            }
        }
        private bool IsPortOfSenderReduState(List<PortOfSenderMonitorInfo> portMonitorInfoList)
        {
            foreach (var item in portMonitorInfoList)
            {
                if (item.IsReduState) return true;
            }
            return false;
        }
        #endregion
        #endregion
        #region 父类重写
        protected override void OnInitialize()
        {
            if (_moniDatareader == null)
            {
                _moniDatareader = new MarsHWMonitorDataReader();
            }
            _fLogService.Info("开始初始化...");
            SendData("M3_StateData", "OnInitialize");
            //打开服务
            _moniDatareader.NotifyRegisterErrEvent -= MoniDatareader_NotifyRegisterErrEvent;
            _moniDatareader.NotifyRegisterErrEvent += MoniDatareader_NotifyRegisterErrEvent;
            //配屏信息
            _moniDatareader.NotifyScreenCfgChangedEvent -= MoniDatareader_NotifyScreenCfgChangedEvent;
            _moniDatareader.NotifyScreenCfgChangedEvent += MoniDatareader_NotifyScreenCfgChangedEvent;
            //监控配置文件
            _moniDatareader.NotifyUpdateCfgFileResEvent -= MoniDatareader_NotifyUpdateCfgFileResEvent;
            _moniDatareader.NotifyUpdateCfgFileResEvent += MoniDatareader_NotifyUpdateCfgFileResEvent;
            //参数更新
            _moniDatareader.NotifySettingCompletedEvent -= MoniDatareader_NotifySettingCompletedEvent;
            _moniDatareader.NotifySettingCompletedEvent += MoniDatareader_NotifySettingCompletedEvent;
            UpdateConfigMessage(TransferType.M3_FirstInitialize, string.Empty);

            //这个地方不再初始化，改为由底下事件通知来定：MarsScreenChangedInit
            //InitialErryType res = InitialErryType.OK;
            //_fLogService.Info("硬件开始初始化...");
            //SendData("M3_StateData", "HWOnInitialize");
            //res = _moniDatareader.Initialize();
            //_fLogService.Info("硬件完成初始化");
            //if (res == InitialErryType.OK)
            //{
            //    //GetScreenListInfo();
            //}
            //if (res != InitialErryType.OK)
            //{
            //    _fLogService.Error("硬件初始化异常：" + res.ToString());
            //    WriteLog("硬件初始化异常：" + res.ToString());
            //    SendData("M3_StateData", "HWOnInitializeFailed");
            //    MonitorException = new Exception(res.ToString()) { Source = "M3_MonitorData" };
            //}
            //else
            //{
            //    WriteLog("硬件初始化完成");
            //    _fLogService.Info("硬件初始化完成");
            //    SendData("M3_StateData", "OnInitializeSuccess");
            //}

            if (_dataReadTimer == null)
            {
                WriteLog("周期开始启动");
                _fLogService.Info("周期开始启动");
                _dataReadTimer = new System.Timers.Timer();
                _dataReadTimer.Interval = 60000;
                _dataReadTimer.Elapsed += DataReadTimer_Elapsed;
            }
        }
        protected override void OnStart()
        {
            if (_moniDatareader == null)
            {
                WriteLog("启动硬件时,硬件为空，无法启动!");
                _fLogService.Error("启动硬件时,硬件为空，无法启动!");
                return;
            }
            if (_timerIsEnable)
            {
                _dataReadTimer.Start();
            }
        }
        protected override void OnStop()
        {
            if (_moniDatareader == null)
            {
                WriteLog("停止硬件时,硬件为空，不需要停止!");
                _fLogService.Error("停止硬件时,硬件为空，不需要停止!");
                return;
            }
            _dataReadTimer.Stop();
        }
        protected override void OnPause()
        {
            _dataReadTimer.Enabled = false;
            if (_moniDatareader == null)
            {
                return;
            }
        }
        protected override void OnResume()
        {
            if (_timerIsEnable)
            {
                _dataReadTimer.Enabled = true;
            }
            else
            {
                _dataReadTimer.Enabled = false;
            }
            if (_moniDatareader == null)
            {
                return;
            }
        }
        protected override void OnDispose()
        {
            if (_moniDatareader != null)
            {
                //监控配置文件
                _moniDatareader.NotifyUpdateCfgFileResEvent -= MoniDatareader_NotifyUpdateCfgFileResEvent;
                //配屏信息
                _moniDatareader.NotifyScreenCfgChangedEvent -= MoniDatareader_NotifyScreenCfgChangedEvent;
                //参数更新
                _moniDatareader.NotifySettingCompletedEvent -= MoniDatareader_NotifySettingCompletedEvent;
                //错误
                _moniDatareader.NotifyRegisterErrEvent -= MoniDatareader_NotifyRegisterErrEvent;
                _moniDatareader.Unitialize();
                _moniDatareader = null;
            }
            if (_dataReadTimer != null)
            {
                _dataReadTimer.Elapsed -= DataReadTimer_Elapsed;
                _dataReadTimer.Dispose();
                _dataReadTimer = null;
            }
        }
        protected override void OnExecuteCommand(Command cmd)
        {
            //Action action = new Action(() =>
            //{
            ExecuteCommandBeginInvoke(cmd);
            //});
            //action.BeginInvoke(null, null);
            //action.Invoke();
        }

        private void ReadSmartLightHWconfigInfo(string sn)
        {
            if (string.IsNullOrEmpty(sn))
            {
                _dicHwBrightConfig.Clear();
                foreach (ScreenInfo screen in _screenInfos)
                {
                    if (_dicHwBrightConfig.ContainsKey(screen.Commport))
                    {
                        _fLogService.Debug("获取硬件亮度配置从字典中取了:" + screen.LedSN);
                        TransFerParamsDataHandlerCallBack(new TransferParams()
                        {
                            TranType = TransferType.M3_ReadSmartLightHWConfig,
                            Content = CommandTextParser.GetJsonSerialization<ReadSmartLightDataParams>(_dicHwBrightConfig[screen.Commport])
                        }, screen.Commport + "|" + screen.LedSN);
                    }
                    else
                    {
                        _moniDatareader.ExecuteCommandCallBack(
                            new TransferParams() { TranType = TransferType.M3_ReadSmartLightHWConfig, Content = screen.Commport },
                            TransFerParamsDataHandlerCallBack, screen.Commport + "|" + screen.LedSN);
                        _autoResetHWEvent.WaitOne(3000, false);
                    }
                }
            }
            else
            {
                _moniDatareader.ExecuteCommandCallBack(
                    new TransferParams() { TranType = TransferType.M3_ReadSmartLightHWConfig, Content = sn },
                    TransFerParamsDataHandlerCallBack, sn);
            }
        }

        private void ExecuteCommandBeginInvoke(Command cmd)
        {
            try
            {
                if (cmd.Code.Equals(CommandCode.SetLedAlarmConfigInfo))
                {
                    LedAlarmConfig ledAlarm = CommandTextParser.GetDeJsonSerialization<LedAlarmConfig>(cmd.CommandText);
                    if (ledAlarm != null && !string.IsNullOrEmpty(ledAlarm.SN))
                    {
                        if (_ledAlarmConfigDic.ContainsKey(ledAlarm.SN))
                            _ledAlarmConfigDic[ledAlarm.SN] = ledAlarm;
                        else
                            _ledAlarmConfigDic.Add(ledAlarm.SN, ledAlarm);
                    }
                }
                else if (cmd.Code == CommandCode.SetLedAcquisitionConfigInfo)
                {
                    LedAcquisitionConfig obj = CommandTextParser.GetDeJsonSerialization<LedAcquisitionConfig>(cmd.CommandText);
                    _dataReadTimer.Enabled = obj.IsAutoRefresh;
                    _timerIsEnable = obj.IsAutoRefresh;
                    _dataReadTimer.Interval = obj.DataPeriod;
                    _retryCount = obj.RetryCount;
                    WriteLog("命令执行：周期变更了:" + obj.DataPeriod);
                    _fLogService.Info("命令执行：周期变更了:" + obj.DataPeriod);
                    _fLogService.Info(string.Format("@CommandLog@Command Execute finished(normal)####Code={0},id={1},source={2},Target={3},cmdText={4},Description={5}", cmd.Code.ToString(), cmd.Id, cmd.Source, cmd.Target.ToString(), cmd.CommandText, cmd.Description));
                    return;
                }
                else if (cmd.Code == CommandCode.SetSpotInspectionConfigInfo)
                {
                    Dictionary<string, DetectConfigParams> detectConfigParamList = CommandTextParser.GetDeJsonSerialization<Dictionary<string, DetectConfigParams>>(cmd.CommandText);
                    if (detectConfigParamList != null)
                    {
                        foreach (var item in detectConfigParamList)
                        {
                            if (!_detectConfigParamList.ContainsKey(item.Key))
                            {
                                _detectConfigParamList.Add(item.Key, item.Value);
                            }
                            else _detectConfigParamList[item.Key] = item.Value;
                        }
                    }
                }
                else if (cmd.Code == CommandCode.SetPeriodicInspectionConfigInfo)
                {
                    var cycleConfig = CommandTextParser.GetDeJsonSerialization<ParameterInspectionCycleConfig>(cmd.CommandText);
                    if (_spotInspectionConfigTable.Keys.Contains(cmd.Description))
                    {
                        _spotInspectionConfigTable[cmd.Description] = cycleConfig;
                    }
                    else
                    {
                        _spotInspectionConfigTable.Add(cmd.Description, cycleConfig);
                    }

                    #region 点检周期解析及执行 - Modify by Lixc
                    //long timerTicks = 0;
                    //if (cycleConfig.Cycle == PeriodType.Daily)
                    //{
                    //    var difference = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, cycleConfig.Hour, cycleConfig.Minute, 0) - DateTime.Now).Ticks;
                    //    if (difference >= 0)
                    //    {
                    //        timerTicks = difference;
                    //    }
                    //    else
                    //    {
                    //        timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, cycleConfig.Hour, cycleConfig.Minute, 0).AddDays(1) - DateTime.Now).Ticks;
                    //    }
                    //}
                    //else if (cycleConfig.Cycle == PeriodType.Weekly)
                    //{
                    //    int differenceDay = cycleConfig.Sign - (int)DateTime.Now.DayOfWeek;
                    //    if (differenceDay > 0)
                    //    {
                    //        timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, cycleConfig.Hour, cycleConfig.Minute, 0).AddDays(differenceDay) - DateTime.Now).Ticks;
                    //    }
                    //    else if (differenceDay == 0)
                    //    {
                    //        timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, cycleConfig.Hour, cycleConfig.Minute, 0).AddDays(differenceDay) - DateTime.Now).Ticks;
                    //        if (timerTicks < 0)
                    //        {
                    //            timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, cycleConfig.Hour, cycleConfig.Minute, 0).AddDays(7) - DateTime.Now).Ticks;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, cycleConfig.Hour, cycleConfig.Minute, 0).AddDays(7+differenceDay) - DateTime.Now).Ticks;
                    //    }
                    //}
                    //else if (cycleConfig.Cycle == PeriodType.Monthly)
                    //{
                    //    int differenceDay = cycleConfig.Sign - (int)DateTime.Now.Day;
                    //    if (differenceDay > 0)
                    //    {
                    //        timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute, 0) - DateTime.Now).Ticks;
                    //    }
                    //    else if (differenceDay == 0)
                    //    {
                    //        timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute, 0) - DateTime.Now).Ticks;
                    //        if (timerTicks < 0)
                    //        {
                    //            timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute, 0).AddMonths(1) - DateTime.Now).Ticks;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        timerTicks = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute, 0).AddMonths(1) - DateTime.Now).Ticks;
                    //    }
                    //}
                    //var _spotInspectionTimer = new System.Threading.Timer(ProcessSpotInspection, cmd.Description, timerTicks / 10000, timerTicks / 10000);
                    #endregion

                    if (cycleConfig == null || cycleConfig.Cycle == PeriodType.Disable)
                    {
                        if (_spotInspectionScheduler != null)
                            _spotInspectionScheduler.DeleteJob(new JobKey(cmd.Description));
                        if (_spotInspectionJobTable.Keys.Contains(cmd.Description))
                            _spotInspectionJobTable.Remove(cmd.Description);
                    }
                    else
                    {
                        lock (_syncObj)
                        {
                            if (_spotInspectionScheduler == null)
                            {
                                ISchedulerFactory sf = new StdSchedulerFactory();
                                _spotInspectionScheduler = sf.GetScheduler();
                            }
                        }

                        ITrigger trigger = null;
                        if (cycleConfig.Cycle == PeriodType.Daily)
                        {
                            //trigger = TriggerBuilder.Create().StartAt(new DateTimeOffset(DateTime.Now, new TimeSpan(0,0,0,0,random.Next(1000)))).WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(cycleConfig.Hour, cycleConfig.Minute)).Build();
                            trigger = TriggerBuilder.Create().WithIdentity(cmd.Description).StartNow().WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(cycleConfig.Hour, cycleConfig.Minute)).Build();
                        }
                        else if (cycleConfig.Cycle == PeriodType.Weekly)
                        {
                            //trigger = TriggerBuilder.Create().StartAt(new DateTimeOffset(DateTime.Now, new TimeSpan(random.Next(1000)))).WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute((DayOfWeek)cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute)).Build();
                            trigger = TriggerBuilder.Create().WithIdentity(cmd.Description).StartNow().WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute((DayOfWeek)cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute)).Build();
                        }
                        else if (cycleConfig.Cycle == PeriodType.Monthly)
                        {
                            //trigger = TriggerBuilder.Create().StartAt(new DateTimeOffset(DateTime.Now, new TimeSpan(random.Next(1000)))).WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute)).Build();
                            trigger = TriggerBuilder.Create().WithIdentity(cmd.Description).StartNow().WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(cycleConfig.Sign, cycleConfig.Hour, cycleConfig.Minute)).Build();
                        }
                        IJobDetail job = JobBuilder.Create<SpotInspectionJob>().WithIdentity(cmd.Description).Build();

                        job.JobDataMap.Put("SpotInspectionJob_SN", cmd.Description);
                        job.JobDataMap.Put("Sender", this);

                        if (_spotInspectionJobTable.Keys.Contains(cmd.Description))
                        {
                            if (_spotInspectionScheduler.DeleteJob(new JobKey(cmd.Description)))
                            {
                                _spotInspectionJobTable[cmd.Description] = job;
                            }
                        }
                        else
                        {
                            _spotInspectionJobTable.Add(cmd.Description, job);
                        }
                        _spotInspectionScheduler.ScheduleJob(job, trigger);

                        _spotInspectionScheduler.Start();
                    }

                }

                //判断Command类型，如果是设置（亮度调节）命令，单独处理调用SetBrightness()
                if (_moniDatareader != null)
                {
                    switch (cmd.Code)
                    {
                        case CommandCode.SetBrightness:
                            WriteLog("命令执行：调节亮度...");
                            _fLogService.Info("命令执行：调节亮度...");
                            int brightness = 0;
                            string[] strBright = cmd.CommandText.Split('|');
                            if (strBright.Length < 2 || !Int32.TryParse(strBright[1], out brightness))
                            {
                                SendData(CmdResult.CMD_SetBrightness.ToString(), cmd.Source + "|" + Convert.ToInt32(false));
                                SendData("BrightnessLog", strBright[0] + "|" + "4" + "|" + "false" + "|" + "-1");
                                _fLogService.Info(string.Format("@CommandLog@Command Execute finished(error)####Code={0},id={1},source={2},Target={3},cmdText={4},Description={5}\n{Errorinfo={6}", cmd.Code.ToString(), cmd.Id, cmd.Source, cmd.Target.ToString(), cmd.CommandText, cmd.Description, "CommandText analyze error"));
                                return;
                            }
                            byte br = (byte)brightness;
                            byte brValue = (byte)(brightness * 255 / 100);
                            //System.Diagnostics.Trace.WriteLine("brightness:" + br);
                            //TODO:这里需要调节亮度的屏信息
                            if (!SetBrightness(strBright[0], cmd.Source, br))
                            {
                                SendData(CmdResult.CMD_SetBrightness.ToString(), cmd.Source + "|" + Convert.ToInt32(false));
                                SendData("BrightnessLog", strBright[0] + "|" + "4" + "|" + "false" + "|" + brValue);
                            }
                            else
                            {
                                SendData("BrightnessLog", strBright[0] + "|" + "4" + "|" + "true" + "|" + brValue);
                            }
                            break;
                        case CommandCode.OpenDevice:
                            WriteLog("命令执行：打开设备...");
                            _fLogService.Info("命令执行：打开设备...");
                            UpdateConfigMessage(TransferType.M3_OpenDevice, cmd.CommandText);
                            break;
                        case CommandCode.CloseDevice:
                            WriteLog("命令执行：关闭设备...");
                            _fLogService.Info("命令执行：关闭设备...");
                            UpdateConfigMessage(TransferType.M3_CloseDevice, cmd.CommandText);
                            break;
                        case CommandCode.RefreshLedScreenConfigInfo:
                            WriteLog("命令执行：需要获取屏信息!");
                            _fLogService.Info("命令执行：需要获取屏信息!");
                            if (cmd.CommandText == "Get")
                            {
                                GetScreenListInfo();
                            }
                            else
                            {
                                UpdateConfigMessage(TransferType.M3_UpdateLedScreenConfigInfo, cmd.CommandText);
                            }
                            break;
                        case CommandCode.SetLedMonitoringConfigInfo:
                            WriteLog("命令执行：硬件配置变更了!");
                            _fLogService.Info("命令执行：硬件配置变更了!");
                            MarsHWConfig marsconfig = MonitoringConfigToMarsHWConfig(cmd.CommandText);
                            UpdateConfigMessage(TransferType.UpdateLedMonitoringConfigInfo,
                                CommandTextParser.GetJsonSerialization<MarsHWConfig>(marsconfig));
                            break;
                        case CommandCode.RefreshOpticalProbeInfo:
                            WriteLog("命令执行：开始读取光探头信息!");
                            _fLogService.Info("命令执行：开始读取光探头信息!");
                            _moniDatareader.ExecuteCommandCallBack(
                                new TransferParams() { TranType = TransferType.M3_PeripheralsInfo },
                                TransFerParamsDataHandlerCallBack, null);
                            break;
                        case CommandCode.RefreshFunctionCardInfo:
                            WriteLog("命令执行：开始读取多功能卡信息!");
                            _fLogService.Info("命令执行：开始读取多功能卡信息!");
                            _moniDatareader.ExecuteCommandCallBack(
                                new TransferParams() { TranType = TransferType.M3_FunctionCardMonitor },
                                TransFerParamsDataHandlerCallBack, null);
                            break;
                        case CommandCode.RefreshMonitoringData:
                            _fLogService.Debug("命令执行：刷新监控数据!");
                            ReadMonitorData_First();
                            break;
                        case CommandCode.RefreshSmartBrightEasyConfigInfo:
                            //读取硬件亮度配置
                            ReadSmartLightHWconfigInfo(cmd.CommandText);
                            break;
                        case CommandCode.SetSmartBrightEasyConfigInfo:
                            //写入亮度的配置：软硬件
                            UpdateConfigMessage(TransferType.M3_WriteSmartLightHWConfig, cmd.CommandText);
                            break;
                        case CommandCode.StartSmartBrightness:
                        case CommandCode.StopSmartBrightness:
                            UpdateConfigMessage(TransferType.M3_EnableSmartBrightness, cmd.CommandText);
                            break;
                        case CommandCode.SetFromCOMToSN:
                            UpdateConfigMessage(TransferType.M3_COMFindSN, cmd.CommandText);
                            break;
                        case CommandCode.StopDataAcquisition:
                            _fLogService.Debug("Stop Monitor:" + cmd.CommandText);
                            var snList = new List<string>();
                            _screenInfos.ForEach(s => snList.Add(s.LedSN));
                            UpdateConfigMessage(TransferType.M3_MonitorStopNotify, cmd.CommandText + "|" + CommandTextParser.GetJsonSerialization<List<string>>(snList));
                            OnPause();
                            break;
                        case CommandCode.ResumeDataAcquisition:
                            _fLogService.Debug("Renume Monitor:" + cmd.CommandText);
                            var snLists = new List<string>();
                            _screenInfos.ForEach(s => snLists.Add(s.LedSN));
                            UpdateConfigMessage(TransferType.M3_MonitorRenumeNotify, cmd.CommandText + "|" + CommandTextParser.GetJsonSerialization<List<string>>(snLists));
                            OnResume();
                            break;
                    }
                    Thread.Sleep(10);
                }
                _fLogService.Info(string.Format("@CommandLog@Command Execute finished(normal)####Code={0},id={1},source={2},Target={3},cmdText={4},Description={5}", cmd.Code.ToString(), cmd.Id, cmd.Source, cmd.Target.ToString(), cmd.CommandText, cmd.Description));
            }
            catch (Exception ex)
            {
                _fLogService.Info(string.Format("@CommandLog@Command Execute finished(error)####Code={0},id={1},source={2},Target={3},cmdText={4},Description={5}\nErrorinfo={6}", cmd.Code.ToString(), cmd.Id, cmd.Source, cmd.Target.ToString(), cmd.CommandText, cmd.Description, ex.ToString()));
                _fLogService.Error("ExistCatch:ExecuteCommandBeginInvoke->Error:" + ex.ToString());
                MonitorException = ex;
            }
        }

        private void TransFerParamsDataHandlerCallBack(TransferParams param, object userToken)
        {
            switch (param.TranType)
            {
                case TransferType.M3_PeripheralsInfo:
                    WriteLog("返回光探头数据");
                    _fLogService.Info("返回光探头数据");
                    SendData("UpdateOpticalProbeInfo", param.Content);
                    break;
                case TransferType.M3_FunctionCardMonitor:
                    WriteLog("返回多功能卡信息");
                    _fLogService.Info("返回多功能卡信息");
                    SendData("UpdateFunctionCardInfo", param.Content);
                    break;
                case TransferType.M3_ReadSmartLightHWConfig:
                    WriteLog("返回硬件亮度配置信息");
                    _fLogService.Info("返回硬件亮度配置信息");
                    SmartLightConfigInfo smartLight = new SmartLightConfigInfo();
                    ReadSmartLightDataParams smartparam = CommandTextParser.GetDeJsonSerialization<ReadSmartLightDataParams>(param.Content);
                    smartLight.DisplayHardcareConfig = smartparam.DisplayConfigBase;
                    smartLight.HwExecTypeValue = smartparam.BrightHWExecType;
                    smartLight.ScreenSN = userToken.ToString().Split('|')[1];
                    if (!_dicHwBrightConfig.ContainsKey(userToken.ToString().Split('|')[0]))
                    {
                        _dicHwBrightConfig.Add(userToken.ToString().Split('|')[0], smartparam);
                    }

                    _autoResetHWEvent.Set();
                    SendData("ReadSmartLightHWconfigInfo", CommandTextParser.GetJsonSerialization<SmartLightConfigInfo>(smartLight));
                    break;
                default:
                    break;
            }
        }

        private void MarsScreenChangedInit()
        {
            InitialErryType res = InitialErryType.OK;
            _fLogService.Info("硬件开始初始化...");
            SendData("M3_StateData", "HWOnInitialize");
            res = _moniDatareader.Initialize();
            if (res != InitialErryType.OK)
            {
                _fLogService.Error("硬件初始化异常：" + res.ToString());
                WriteLog("硬件初始化异常：" + res.ToString());
                SendData("M3_StateData", "HWOnInitializeFailed");
                MonitorException = new Exception(res.ToString()) { Source = "M3_MonitorData" };
            }
            else
            {
                WriteLog("硬件初始化完成");
                _fLogService.Info("硬件初始化完成");
                SendData("M3_StateData", "OnInitializeSuccess");
            }
        }
        #endregion

        #region 事件通知
        void MoniDatareader_FindPeripheralsEvent(PeripheralsLocateInfo periInfo)
        {
            SendData("FindPeripherals", CommandTextParser.GetJsonSerialization<PeripheralsLocateInfo>(periInfo));
        }
        void MoniDatareader_NotifyUpdateCfgFileResEvent(object sender, UpdateCfgFileResEventArgs e)
        {
            if (e == null || e.UpdateParams == null)
            {
                WriteLog("硬件通知为空，是不可能的事情？");
                _fLogService.Info("硬件通知为空，是不可能的事情？");
                return;
            }
            switch (e.UpdateParams.TranType)
            {
                case TransferType.M3_RefreshDataFinish:
                    if (_isNeedRetry)
                    {
                        switch (_dataType)
                        {
                            case DataType.all:
                                break;
                            case DataType.senderData:
                                Action actionSender = new Action(() =>
                                {
                                    BeginReadSenderCardData(_senderRetryInfoDic);
                                });
                                actionSender.BeginInvoke(null, null);
                                break;
                            case DataType.scannerData:
                                Action actionScan = new Action(() =>
                                {
                                    BeginReadScanBoardData(_scanRetryInfoDic);
                                });
                                actionScan.BeginInvoke(null, null);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TransferType.M3_ExecStaus:
                    SendData("M3_StateData", e.UpdateParams.Content);
                    break;
                case TransferType.M3_COMFindSN:
                    SendData("FromCOMFindSN", e.UpdateParams.Content);
                    break;
                case TransferType.M3_UpdateLedScreenConfigInfo:
                    GetScreenListInfo();
                    break;
                case TransferType.M3_ExecBrightResultLog:
                    SendData("BrightnessLog", e.UpdateParams.Content);
                    break;
                case TransferType.M3_BrightConfigSaveResult:
                    SendData("BrightConfigSaveResult", e.UpdateParams.Content);
                    break;
                default:
                    break;
            }
        }
        void MoniDatareader_NotifyScreenCfgChangedEvent(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(AsyncRestartDataReader, false);
        }
        void MoniDatareader_NotifyRegisterErrEvent(object sender, EventArgs e)
        {
            //TODO:
            //Interlocked.Exchange(ref _isReadingMonitorData, 0);
            //StartServer();
            //AsyncRestartDataReader(false);
        }
        void MoniDatareader_NotifySettingCompletedEvent(object sender, NotifySettingCompletedEventArgs e)
        {//Value:，显示屏ID|设置结果
            if (e.SettingType == HWSettingType.GlobalBright)
            {
                SendData(CmdResult.CMD_SetBrightness.ToString(), _cmdSource + "|" + +Convert.ToInt32(e.Result));
            }
            Interlocked.Exchange(ref _isRunningMetux, 0);
        }

        #endregion

        #region 内部方法
        private void AsyncRestartDataReader(object state)
        {
            RestartDataReader(state);
        }

        private void ReadMonitorData()
        {
            throw new NotImplementedException();
        }

        private object _objLock = new object();
        private void ReadMonitorData(IAsyncResult result)
        {
            lock (_objLock)
            {
                ReadMonitorDataErrType res = _moniDatareader.BeginReadData(new CompletedMonitorCallback(
                        (cmpParams, userToken) =>
                        {
                            if (cmpParams != null)
                            {
                                string cmpStr = CommandTextParser.GetJsonSerialization<CompletedMonitorCallbackParams>(cmpParams);
                                _cmpParams = CommandTextParser.GetDeJsonSerialization<CompletedMonitorCallbackParams>(cmpStr);
                            }
                            ReadDataCallback(_cmpParams, userToken);
                        }), DataType.all);

                if (res == ReadMonitorDataErrType.NoServerObj)
                {
                    //取消重新读取，直接提示用户
                    //_fLogService.Error("服务没有注册，需重新注册...");
                    //AsyncRestartDataReader(false);
                    //UpdateConfigMessage(TransferType.M3_UpdateLedScreenConfigInfo, string.Empty);
                    Interlocked.Exchange(ref _isReadingMonitorData, 0);
                    return;
                }
                else if (res != ReadMonitorDataErrType.OK)
                {
                    State = DataSourceState.Error;
                    SendData("M3_StateData", res.ToString());
                    MonitorException = new Exception(res.ToString()) { Source = "M3_MonitorData" };
                    if (res != ReadMonitorDataErrType.BusyWorking)
                    {
                        _fLogService.Debug("ReadMonitorData BusyWorking释放");
                        Interlocked.Exchange(ref _isReadingMonitorData, 0);
                    }
                }
            }
        }

        private void RestartDataReader(object state)
        {
            //需要重新获取屏信息
            //GetScreenListInfo();
            WriteLog("重启硬件,先进行停止...");
            _fLogService.Info("重启硬件,先进行停止...");
            try
            {
                Stop();
                _moniDatareader.Unitialize();
            }
            catch (Exception ex)
            {
                _fLogService.Error("ExistCatch:RestartDataReader->:停止硬件出错:" + ex.ToString());
                WriteLog("停止硬件出错:" + ex.Message);
            }
            WriteLog("重启硬件,重新初始化...");
            _fLogService.Info("重启硬件,重新初始化...");
            //Initialize();
            State = DataSourceState.Initializing;
            MarsScreenChangedInit();
            State = DataSourceState.Initialized;
            //GetScreenListInfo();
            if (State != DataSourceState.Initialized)
            {
                SendData(CmdResult.CMD_UpdateLedInfo.ToString(), Convert.ToInt32(false));
                WriteLog("重启硬件,初始化失败");
                _fLogService.Info("RestartDataReader->:重启硬件,初始化失败");
            }
            else
            {
                Thread.Sleep(2000);
                Start();
                SendData(CmdResult.CMD_UpdateLedInfo.ToString(), Convert.ToInt32(true));
                WriteLog("重启硬件,重启成功");
                _fLogService.Info("RestartDataReader->:重启硬件,重启成功");
            }
            //if (state == null || string.IsNullOrEmpty(state.ToString()) || state.ToString().ToLower() != "true")
            //{
            _fLogService.Debug("RestartDataReader BusyWorking释放");
            Interlocked.Exchange(ref _isReadingMonitorData, 0);
            //}
        }

        /// <summary>
        /// 设置显示屏亮度
        /// </summary>
        /// <param name="SUID">显示屏ID</param>
        /// <param name="brightness">亮度值</param>
        /// <returns></returns>
        private bool SetBrightness(string screenUDID, string cmdSource, byte brightness)
        {
            bool isOk = true;
            if (Interlocked.Exchange(ref _isRunningMetux, 1) == 0)
            {
                _screenUDID = screenUDID;
                if (_moniDatareader.SetScreenBright(screenUDID, brightness) != HWSettingResult.OK)
                    isOk = false;
            }
            else isOk = false;
            return isOk;
        }

        private MarsHWConfig MonitoringConfigToMarsHWConfig(string content)
        {
            WriteLog("硬件配置变更成内部需要的对象...");
            LedMonitoringConfig ledConfig = CommandTextParser.GetDeJsonSerialization<LedMonitoringConfig>(content);

            MarsHWConfig marsconfig = new MarsHWConfig();
            marsconfig.SN = ledConfig.SN;
            if (ledConfig.MonitoringCardConfig != null)
            {
                marsconfig.IsUpdateMCStatus = ledConfig.MonitoringCardConfig.MonitoringEnable;
                if (ledConfig.MonitoringCardConfig.ParameterConfigTable != null)
                {
                    foreach (ParameterMonitoringConfig param in ledConfig.MonitoringCardConfig.ParameterConfigTable)
                    {
                        switch (param.Type)
                        {
                            case StateQuantityType.Humidity:
                                marsconfig.IsUpdateHumidity = param.MonitoringEnable;
                                break;
                            case StateQuantityType.DoorStatus:
                                marsconfig.IsUpdateGeneralStatus = param.MonitoringEnable;
                                break;
                            case StateQuantityType.Smoke:
                                marsconfig.IsUpdateSmoke = param.MonitoringEnable;
                                break;
                            case StateQuantityType.FlatCableStatus:
                                marsconfig.IsUpdateRowLine = param.MonitoringEnable;
                                break;
                            case StateQuantityType.FanSpeed:
                                marsconfig.IsUpdateFan = param.MonitoringEnable;
                                if (param.MonitoringEnable)
                                {
                                    marsconfig.FanInfoObj.FanPulseCount = param.ReservedConfig;
                                    marsconfig.FanInfoObj.isSame = true;
                                    if (param.ConfigMode == ParameterConfigMode.GeneralExtendedMode)
                                    {
                                        marsconfig.FanInfoObj.FanCount = param.GeneralExtendedConfig;
                                    }
                                    else if (param.ConfigMode == ParameterConfigMode.AdvanceExtendedMode)
                                    {
                                        marsconfig.FanInfoObj.isSame = false;
                                        if (param.ExtendedConfig != null)
                                        {
                                            foreach (ParameterExtendedConfig paramEx in param.ExtendedConfig)
                                            {
                                                int scanIndex = -1;
                                                string[] strIndexs = paramEx.ReceiveCardId.Split('-');
                                                if (strIndexs.Length == 4)
                                                {
                                                    int.TryParse(strIndexs[3], out scanIndex);
                                                }
                                                marsconfig.FanInfoObj.FanList.Add(new OneFanInfo()
                                                {
                                                    FanCount = paramEx.ParameterCount,
                                                    Sn = strIndexs[0],
                                                    SenderIndex = int.Parse(strIndexs[1]),
                                                    PortIndex = int.Parse(strIndexs[2]),
                                                    ScanIndex = int.Parse(strIndexs[3])
                                                });
                                            }
                                        }
                                    }
                                }
                                break;
                            case StateQuantityType.Voltage:
                                marsconfig.IsUpdateMCVoltage = param.MonitoringEnable;
                                if (param.MonitoringEnable)
                                {
                                    marsconfig.PowerInfoObj.isSame = true;
                                    if (param.ConfigMode == ParameterConfigMode.GeneralExtendedMode)
                                    {
                                        marsconfig.PowerInfoObj.PowerCount = param.GeneralExtendedConfig;
                                    }
                                    else if (param.ConfigMode == ParameterConfigMode.AdvanceExtendedMode)
                                    {
                                        marsconfig.PowerInfoObj.isSame = false;
                                        if (param.ExtendedConfig != null)
                                        {
                                            foreach (ParameterExtendedConfig paramEx in param.ExtendedConfig)
                                            {
                                                int scanIndex = -1;
                                                string[] strIndexs = paramEx.ReceiveCardId.Split('-');
                                                if (strIndexs.Length == 4)
                                                {
                                                    int.TryParse(strIndexs[3], out scanIndex);
                                                }
                                                marsconfig.PowerInfoObj.PowerList.Add(new OnePowerInfo()
                                                {
                                                    PowerCount = paramEx.ParameterCount,
                                                    Sn = strIndexs[0],
                                                    SenderIndex = int.Parse(strIndexs[1]),
                                                    PortIndex = int.Parse(strIndexs[2]),
                                                    ScanIndex = int.Parse(strIndexs[3])
                                                });
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            _fLogService.Info("硬件配置变更成内部需要的对象成功。。");
            return marsconfig;
        }

        private void UpdateConfigMessage(TransferType configType, string comText)
        {
            TransferParams param = new TransferParams()
            {
                TranType = configType,
                Content = comText
            };
            _moniDatareader.UpdateConfigMessage(CommandTextParser.SerialCmdTextParamTo(param));
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStrX2(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStrX1(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X1");
                }
            }
            return returnStr;
        }

        private void WriteLog(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("NovaMonitorManager：DataAcquistion>>>>>>>>>");
            sb.Append(msg);
            Debug.WriteLine(sb.ToString());
        }
        #endregion
    }
    public enum CmdResult
    {
        CMD_UpdateMonitorConfig,
        CMD_UpdateLedInfo,
        CMD_SetBrightness,
    }
    public enum DataType
    {
        all,
        senderData,
        scannerData
    }
    public class RepetDataInfo
    {
        public string CommPort { get; set; }
        public byte SenderIndex { get; set; }
        public byte PortIndex { get; set; }
        public ushort ScannerIndex { get; set; }
        public int RetryCount { get; set; }
    }
}