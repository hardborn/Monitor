using Nova.LCT.GigabitSystem.Common;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nova.Monitoring.HardwareMonitorInterface
{
    public delegate void ReadMonitorDataCallbcak();
    [Serializable]
    public class AllMonitorData : ICopy, ICloneable
    {
        public AllMonitorData()
        {
            CurAllSenderStatusDic = new SerializableDictionary<string, List<WorkStatusType>>();
            CurAllSenderDVIDic = new SerializableDictionary<string, List<ValueInfo>>();
            RedundantStateType = new SerializableDictionary<string, SerializableDictionary<int, SenderRedundantStateInfo>>();
            CommPortData = new SerializableDictionary<string, int>();
            TempRedundancyDict = new SerializableDictionary<string, List<SenderRedundancyInfo>>();
            MonitorDataDic = new SerializableDictionary<string, SerializableDictionary<string, ScannerMonitorData>>();
            MonitorResInfDic = new SerializableDictionary<string, DateTime>();
        }

        public List<ScreenModnitorData> AllScreenMonitorCollection
        {
            get
            {
                return _allScreenMonitorCollection;
            }
            set
            {
                _allScreenMonitorCollection = value;
            }
        }
        private List<ScreenModnitorData> _allScreenMonitorCollection = new List<ScreenModnitorData>();
        #region 发送卡数据
        /// <summary>
        /// 发送卡状态
        /// </summary>
        public SerializableDictionary<string, List<WorkStatusType>> CurAllSenderStatusDic
        {
            get;
            set;
        }
        /// <summary>
        /// DVI状态
        /// </summary>
        public SerializableDictionary<string, List<ValueInfo>> CurAllSenderDVIDic
        {
            get;
            set;
        }
        /// <summary>
        /// 冗余
        /// </summary>
        public SerializableDictionary<string, SerializableDictionary<int, SenderRedundantStateInfo>> RedundantStateType
        {
            get;
            set;
        }
        /// <summary>
        /// 串口下数据
        /// </summary>
        public SerializableDictionary<string, int> CommPortData
        {
            get;
            set;
        }
        /// <summary>
        /// 冗余数据
        /// </summary>
        public SerializableDictionary<string, List<SenderRedundancyInfo>> TempRedundancyDict
        {
            get;
            set;
        }
        #endregion
        #region 接收卡数据
        public SerializableDictionary<string, SerializableDictionary<string, ScannerMonitorData>> MonitorDataDic
        {
            get;
            set;
        }
        public SerializableDictionary<string, System.DateTime> MonitorResInfDic
        {
            get;
            set;
        }
        #endregion

        public object Clone()
        {
            AllMonitorData data = new AllMonitorData();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is AllMonitorData))
            {
                return false;
            }
            AllMonitorData config = obj as AllMonitorData;
            if (this.AllScreenMonitorCollection == null)
            {
                config.AllScreenMonitorCollection = null;
            }
            else
            {
                config.AllScreenMonitorCollection = new List<ScreenModnitorData>();
                foreach (ScreenModnitorData sender in this.AllScreenMonitorCollection)
                {
                    config.AllScreenMonitorCollection.Add((ScreenModnitorData)sender.Clone());
                }
            }
            if (this.CurAllSenderStatusDic == null)
            {
                config.CurAllSenderStatusDic = null;
            }
            else
            {
                config.CurAllSenderStatusDic = new SerializableDictionary<string,List<WorkStatusType>>();
                foreach (KeyValuePair<string, List<WorkStatusType>> keyvalue in this.CurAllSenderStatusDic)
                {
                    if (!config.CurAllSenderStatusDic.ContainsKey(keyvalue.Key))
                    {
                        config.CurAllSenderStatusDic.Add(keyvalue.Key, new List<WorkStatusType>());
                    }
                    foreach (WorkStatusType worktype in keyvalue.Value)
                    {
                        config.CurAllSenderStatusDic[keyvalue.Key].Add(worktype);
                    }
                }
            }
            if (this.CurAllSenderDVIDic == null)
            {
                config.CurAllSenderDVIDic = null;
            }
            else
            {
                config.CurAllSenderDVIDic = new SerializableDictionary<string,List<ValueInfo>>();
                foreach (KeyValuePair<string, List<ValueInfo>> keyvalue in this.CurAllSenderDVIDic)
                {
                    if (!config.CurAllSenderDVIDic.ContainsKey(keyvalue.Key))
                    {
                        config.CurAllSenderDVIDic.Add(keyvalue.Key, new List<ValueInfo>());
                    }
                    foreach (ValueInfo info in keyvalue.Value)
                    {
                        config.CurAllSenderDVIDic[keyvalue.Key].Add(info);
                    }
                }
            }
            if (this.RedundantStateType == null)
            {
                config.RedundantStateType = null;
            }
            else
            {
                config.RedundantStateType = new SerializableDictionary<string,SerializableDictionary<int,SenderRedundantStateInfo>>();
                foreach (KeyValuePair<string, SerializableDictionary<int, SenderRedundantStateInfo>> keyvalue in this.RedundantStateType)
                {
                    if (!config.RedundantStateType.ContainsKey(keyvalue.Key))
                    {
                        config.RedundantStateType.Add(keyvalue.Key,new SerializableDictionary<int,SenderRedundantStateInfo>());
                    }
                    foreach (KeyValuePair<int,SenderRedundantStateInfo> sender in keyvalue.Value)
                    {
                        config.RedundantStateType[keyvalue.Key].Add(sender.Key, (SenderRedundantStateInfo)sender.Value.Clone()); 
                    }
                }
            }
            if (this.CommPortData == null)
            {
                config.CommPortData = null;
            }
            else
            {
                config.CommPortData=new SerializableDictionary<string,int>();
                foreach (KeyValuePair<string, int> keyvalue in this.CommPortData)
                {
                    config.CommPortData.Add(keyvalue.Key, keyvalue.Value);
                }
            }
            if (this.TempRedundancyDict == null)
            {
                config.TempRedundancyDict = null;
            }
            else
            {
                config.TempRedundancyDict = new SerializableDictionary<string,List<SenderRedundancyInfo>>();
                foreach (KeyValuePair<string, List<SenderRedundancyInfo>> keyvalue in this.TempRedundancyDict)
                {
                    if (!config.TempRedundancyDict.ContainsKey(keyvalue.Key))
                    {
                        if(keyvalue.Value==null)
                        {
                            config.TempRedundancyDict.Add(keyvalue.Key, null);
                            continue;
                        }
                        else
                        {
                        config.TempRedundancyDict.Add(keyvalue.Key, new List<SenderRedundancyInfo>());
                        }
                    }
                    foreach (SenderRedundancyInfo info in keyvalue.Value)
                    {
                        config.TempRedundancyDict[keyvalue.Key].Add((SenderRedundancyInfo)info.Clone());
                    }
                }
            }
            if (this.MonitorDataDic == null)
            {
                config.MonitorDataDic = null;
            }
            else
            {
                config.MonitorDataDic = new SerializableDictionary<string,SerializableDictionary<string,ScannerMonitorData>>();
                foreach (KeyValuePair<string, SerializableDictionary<string, ScannerMonitorData>> keyvalue in this.MonitorDataDic)
                {
                    if (!config.MonitorDataDic.ContainsKey(keyvalue.Key))
                    {
                        config.MonitorDataDic.Add(keyvalue.Key, new SerializableDictionary<string, ScannerMonitorData>());
                    }
                    foreach (KeyValuePair<string, ScannerMonitorData> sender in keyvalue.Value)
                    {
                        config.MonitorDataDic[keyvalue.Key].Add(sender.Key, (ScannerMonitorData)sender.Value.Clone()); 
                    }
                }
            }

            if (this.MonitorResInfDic == null)
            {
                config.MonitorResInfDic = null;
            }
            else
            {
                config.MonitorResInfDic = new SerializableDictionary<string,DateTime>();
                foreach (KeyValuePair<string, DateTime> keyvalue in this.MonitorResInfDic)
                {
                    config.MonitorResInfDic.Add(keyvalue.Key, keyvalue.Value);
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 显示屏的监控数据
    /// </summary>
    [Serializable]
    public class ScreenModnitorData : ICopy, ICloneable
    {
        public ScreenModnitorData()
        {
        }

        public string ScreenUDID
        {
            get
            {
                return _screenUDID;
            }
            set
            {
                _screenUDID = value;
            }
        }
        private string _screenUDID = "";
        /// <summary>
        /// 发送卡监控
        /// </summary>
        public List<SenderMonitorInfo> SenderMonitorCollection
        {
            get
            {
                return _senderMonitorCollection;
            }
            set
            {
                _senderMonitorCollection = value;
            }
        }
        private List<SenderMonitorInfo> _senderMonitorCollection = new List<SenderMonitorInfo>();

        /// <summary>
        /// 接收卡状态
        /// </summary>
        public List<ScannerMonitorInfo> ScannerMonitorCollection
        {
            get
            {
                return _scannerMonitorCollection;
            }
            set
            {
                _scannerMonitorCollection = value;
            }
        }
        private List<ScannerMonitorInfo> _scannerMonitorCollection = new List<ScannerMonitorInfo>();

        /// <summary>
        /// 监控卡状态
        /// </summary>
        public List<MonitorCardMonitorInfo> MonitorCardInfoCollection
        {
            get
            {
                return _monitorCardInfoCollection;
            }
            set
            {
                _monitorCardInfoCollection = value;
            }
        }
        private List<MonitorCardMonitorInfo> _monitorCardInfoCollection = new List<MonitorCardMonitorInfo>();

        /// <summary>
        /// 多功能卡状态
        /// </summary>
        public List<FunctionCardMonitorInfo> FunctionCardInfoCollection
        {
            get
            {
                return _functionCardInfoCollection;
            }
            set
            {
                _functionCardInfoCollection = value;
            }
        }
        private List<FunctionCardMonitorInfo> _functionCardInfoCollection = new List<FunctionCardMonitorInfo>();

        public List<DeconcentratorMonitorInfo> DeconcentratorCollection
        {
            get { return _deconcentratorCollection; }
            set { _deconcentratorCollection = value; }
        }
        private List<DeconcentratorMonitorInfo> _deconcentratorCollection = new List<DeconcentratorMonitorInfo>();
        public ScreenModnitorData(string scrUDID)
        {
            _screenUDID = scrUDID;
        }

        public object Clone()
        {
            ScreenModnitorData data = new ScreenModnitorData();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is ScreenModnitorData))
            {
                return false;
            }
            ScreenModnitorData config = obj as ScreenModnitorData;
            config.ScreenUDID = this.ScreenUDID;
            if (this.SenderMonitorCollection == null)
            {
                config.SenderMonitorCollection = null;
            }
            else
            {
                config.SenderMonitorCollection=new List<SenderMonitorInfo>();
                foreach (SenderMonitorInfo sender in this.SenderMonitorCollection)
                {
                    config.SenderMonitorCollection.Add((SenderMonitorInfo)sender.Clone());
                }
            }
            if (this.ScannerMonitorCollection == null)
            {
                config.ScannerMonitorCollection = null;
            }
            else
            {
                config.ScannerMonitorCollection = new List<ScannerMonitorInfo>();
                foreach (ScannerMonitorInfo sender in this.ScannerMonitorCollection)
                {
                    config.ScannerMonitorCollection.Add((ScannerMonitorInfo)sender.Clone());
                }
            }
            if (this.MonitorCardInfoCollection == null)
            {
                config.MonitorCardInfoCollection = null;
            }
            else
            {
                config.MonitorCardInfoCollection = new List<MonitorCardMonitorInfo>();
                foreach (MonitorCardMonitorInfo sender in this.MonitorCardInfoCollection)
                {
                    config.MonitorCardInfoCollection.Add((MonitorCardMonitorInfo)sender.Clone());
                }
            }
            if (this.FunctionCardInfoCollection == null)
            {
                config.FunctionCardInfoCollection = null;
            }
            else
            {
                config.FunctionCardInfoCollection = new List<FunctionCardMonitorInfo>();
                foreach (FunctionCardMonitorInfo sender in this.FunctionCardInfoCollection)
                {
                    config.FunctionCardInfoCollection.Add((FunctionCardMonitorInfo)sender.Clone());
                }
            }
            if (this.DeconcentratorCollection == null)
            {
                config.DeconcentratorCollection = null;
            }
            else
            {
                config.DeconcentratorCollection = new List<DeconcentratorMonitorInfo>();
                foreach (DeconcentratorMonitorInfo sender in this.DeconcentratorCollection)
                {
                    config.DeconcentratorCollection.Add((DeconcentratorMonitorInfo)sender.Clone());
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 设备监控信息基类
    /// </summary>
    [Serializable]
    public class DeviceMonitorBaseInfo:ICopy,ICloneable
    {
        /// <summary>
        /// 该发送卡的
        /// </summary>
        public List<DeviceSearchMapping> MappingList
        {
            get
            {
                return _mappingList;
            }
            set
            {
                _mappingList = value;
            }
        }
        private List<DeviceSearchMapping> _mappingList = new List<DeviceSearchMapping>();

        /// <summary>
        /// 设备工作状态
        /// </summary>
        public DeviceWorkStatus DeviceStatus
        {
            get
            {
                return _deviceStatus;
            }
            set
            {
                _deviceStatus = value;
            }
        }
        private DeviceWorkStatus _deviceStatus = DeviceWorkStatus.Unknown;
        /// <summary>
        /// 卡所在位置
        /// </summary>
        public DeviceRegionInfo DeviceRegInfo { get; set; }

        public virtual object Clone()
        {
            DeviceMonitorBaseInfo data = new DeviceMonitorBaseInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public virtual bool CopyTo(object obj)
        {
            if (!(obj is DeviceMonitorBaseInfo))
            {
                return false;
            }
            DeviceMonitorBaseInfo config = obj as DeviceMonitorBaseInfo;
            config.DeviceStatus = this.DeviceStatus;
            if (this.DeviceRegInfo == null)
            {
                config.DeviceRegInfo = null;
            }
            else
            {
                config.DeviceRegInfo = new DeviceRegionInfo();
                this.DeviceRegInfo.CopyTo(config.DeviceRegInfo);
            }
            if (this.MappingList == null)
            {
                config.MappingList = null;
            }
            else
            {
                config.MappingList = new List<DeviceSearchMapping>();
                foreach (DeviceSearchMapping device in this.MappingList)
                {
                    config.MappingList.Add((DeviceSearchMapping)device.Clone());
                }
            }
            return true;
        }
    }
    [Serializable]
    public class DeviceRegionInfo : ICopy, ICloneable
    {
        public DeviceRegionInfo()
        {

        }
        public string CommPort{get;set;}
        public byte SenderIndex { get; set; }
        public byte PortIndex { get; set; }
        public ushort ConnectIndex{get;set;}
        public ushort CardIndex{get;set;}

        #region 邮件使用
        public bool IsComplex { get; set; }
        public int ScanBoardCols { get; set; }
        public int ScanBoardRows { get; set; }
        #endregion

        public object Clone()
        {
            DeviceRegionInfo data = new DeviceRegionInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is DeviceRegionInfo))
            {
                return false;
            }
            DeviceRegionInfo config = obj as DeviceRegionInfo;
            config.CommPort = this.CommPort;
            config.SenderIndex = this.SenderIndex;
            config.PortIndex = this.PortIndex;
            config.ConnectIndex = this.ConnectIndex;
            config.CardIndex = this.CardIndex;
            return true;
        }
    }

    /// <summary>
    /// 发送卡监控信息
    /// </summary>
    [Serializable]
    public class SenderMonitorInfo : DeviceMonitorBaseInfo
    {
        /// <summary>
        /// DVI源是否连接
        /// </summary>
        public bool IsDviConnected
        {
            get
            {
                return _isDviConnected;
            }
            set
            {
                _isDviConnected = value;
            }
        }
        private bool _isDviConnected = false;
        /// <summary>
        /// DVI的刷新率，DVI连接的时候才有效
        /// </summary>
        public int DviRate
        {
            get
            {
                return _dviRate;
            }
            set
            {
                _dviRate = value;
            }
        }
        private int _dviRate = 0;
        /// <summary>
        /// 进入冗余状态的网口的列表
        /// </summary>
        public List<PortOfSenderMonitorInfo> ReduPortIndexCollection
        {
            get
            {
                return _reduPortIndexCollection;
            }
            set
            {
                _reduPortIndexCollection = value;
            }
        }
        private List<PortOfSenderMonitorInfo> _reduPortIndexCollection = new List<PortOfSenderMonitorInfo>();

        public object Clone()
        {
            SenderMonitorInfo data = new SenderMonitorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is SenderMonitorInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            SenderMonitorInfo config = obj as SenderMonitorInfo;
            config.IsDviConnected = this.IsDviConnected;
            config.DviRate = this.DviRate;
            if (this.ReduPortIndexCollection == null)
            {
                config.ReduPortIndexCollection = null;
            }
            else
            {
                config.ReduPortIndexCollection = new List<PortOfSenderMonitorInfo>();
                foreach (PortOfSenderMonitorInfo port in this.ReduPortIndexCollection)
                {
                    config.ReduPortIndexCollection.Add((PortOfSenderMonitorInfo)(port.Clone()));
                }
            }
            return true;
        }
    }
    [Serializable]
    public class PortOfSenderMonitorInfo : DeviceMonitorBaseInfo
    {
        public bool IsReduState
        {
            get { return _isReduState; }
            set
            {
                _isReduState = value;
            }
        }
        private bool _isReduState = false;
        public object Clone()
        {
            PortOfSenderMonitorInfo data = new PortOfSenderMonitorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is PortOfSenderMonitorInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            PortOfSenderMonitorInfo config = obj as PortOfSenderMonitorInfo;
            config.IsReduState = this.IsReduState;
            return true;
        }
    }
    /// <summary>
    /// 接收卡的监控信息
    /// </summary>
    [Serializable]
    public class ScannerMonitorInfo : DeviceMonitorBaseInfo
    {
        public bool TemperatureIsVaild { get; set; }
        /// <summary>
        /// 接收卡本板温度
        /// </summary>
        public float Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
            }
        }
        private float _temperature = 0.0f;

        /// <summary>
        /// 接收卡本板电压
        /// </summary>
        public float Voltage
        {
            get
            {
                return _voltage;
            }
            set
            {
                _voltage = value;
            }
        }
        private float _voltage;
        public object Clone()
        {
            ScannerMonitorInfo data = new ScannerMonitorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is ScannerMonitorInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            ScannerMonitorInfo config = obj as ScannerMonitorInfo;
            config.Temperature = this.Temperature;
            config.Voltage = this.Voltage;
            config.TemperatureIsVaild = this.TemperatureIsVaild;
            return true;
        }
    }
    /// <summary>
    /// 监控卡的状态
    /// </summary>
    [Serializable]
    public class MonitorCardMonitorInfo : DeviceMonitorBaseInfo
    {
        /// <summary>
        /// 温度状态
        /// </summary>
        public MCTemperatureUpdateInfo TemperatureUInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 湿度状态
        /// </summary>
        public MCHumidityUpdateInfo HumidityUInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 烟雾状态
        /// </summary>
        public MCSmokeUpdateInfo SmokeUInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 箱门状态
        /// </summary>
        public MCDoorUpdateInfo CabinetDoorUInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 风扇状态
        /// </summary>
        public MCFansUpdateInfo FansUInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 电源状态
        /// </summary>
        public MCPowerUpdateInfo PowerUInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 排线状态
        /// </summary>
        public MCSocketCableUpdateInfo SocketCableUInfo
        {
            get;
            set;
        }

        public object Clone()
        {
            MonitorCardMonitorInfo data = new MonitorCardMonitorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is MonitorCardMonitorInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            MonitorCardMonitorInfo config = obj as MonitorCardMonitorInfo;
            if (this.TemperatureUInfo == null)
            {
                config.TemperatureUInfo = null;
            }
            else
            {
                config.TemperatureUInfo = new MCTemperatureUpdateInfo();
                this.TemperatureUInfo.CopyTo(config.TemperatureUInfo);
            }
            if (this.HumidityUInfo == null)
            {
                config.HumidityUInfo = null;
            }
            else
            {
                config.HumidityUInfo = new MCHumidityUpdateInfo();
                this.HumidityUInfo.CopyTo(config.HumidityUInfo);
            }
            if (this.SmokeUInfo == null)
            {
                config.SmokeUInfo = null;
            }
            else
            {
                config.SmokeUInfo = new MCSmokeUpdateInfo();
                this.SmokeUInfo.CopyTo(config.SmokeUInfo);
            }
            if (this.CabinetDoorUInfo == null)
            {
                config.CabinetDoorUInfo = null;
            }
            else
            {
                config.CabinetDoorUInfo = new MCDoorUpdateInfo();
                this.CabinetDoorUInfo.CopyTo(config.CabinetDoorUInfo);
            }
            if (this.FansUInfo == null)
            {
                config.FansUInfo = null;
            }
            else
            {
                config.FansUInfo = new MCFansUpdateInfo();
                this.FansUInfo.CopyTo(config.FansUInfo);
            }
            if (this.PowerUInfo == null)
            {
                config.PowerUInfo = null;
            }
            else
            {
                config.PowerUInfo = new MCPowerUpdateInfo();
                this.PowerUInfo.CopyTo(config.PowerUInfo);
            }
            if (this.SocketCableUInfo == null)
            {
                config.SocketCableUInfo = null;
            }
            else
            {
                config.SocketCableUInfo = new MCSocketCableUpdateInfo();
                this.SocketCableUInfo.CopyTo(config.SocketCableUInfo);
            }
            return true;
        }
    }

    #region 监控的子状态
    /// <summary>
    /// 监控卡子状态更新基类
    /// </summary>
    [Serializable]
    public class MonitorCardElementUpdateInfo:ICopy,ICloneable
    {
        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsUpdate
        {
            get;
            set;
        }

        public virtual object Clone()
        {
            MonitorCardElementUpdateInfo data = new MonitorCardElementUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public virtual bool CopyTo(object obj)
        {
            if (!(obj is MonitorCardElementUpdateInfo))
            {
                return false;
            }
            MonitorCardElementUpdateInfo config = obj as MonitorCardElementUpdateInfo;
            config.IsUpdate = this.IsUpdate;
            return true;
        }
}
    /// <summary>
    /// 监控卡温度更新
    /// </summary>
    [Serializable]
    public class MCTemperatureUpdateInfo : MonitorCardElementUpdateInfo,ICopy,ICloneable
    {
        /// <summary>
        /// 温度
        /// </summary>
        public float Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
            }
        }
        private float _temperature = 0.0f;

        public object Clone()
        {
            MCTemperatureUpdateInfo data = new MCTemperatureUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is MCTemperatureUpdateInfo))
            {
                return false;
            }
            MCTemperatureUpdateInfo config = obj as MCTemperatureUpdateInfo;
            config.Temperature = this.Temperature;
            config.IsUpdate = this.IsUpdate;
            return true;
        }
    }
    /// <summary>
    /// 监控卡湿度更新
    /// </summary>
    [Serializable]
    public class MCHumidityUpdateInfo : MonitorCardElementUpdateInfo
    {
        /// <summary>
        /// 湿度
        /// </summary>
        public float Humidity
        {
            get
            {
                return _humidity;
            }
            set
            {
                _humidity = value;
            }
        }
        private float _humidity = 0.0f;

        public override object Clone()
        {
            MCHumidityUpdateInfo data = new MCHumidityUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is MCHumidityUpdateInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            MCHumidityUpdateInfo config = obj as MCHumidityUpdateInfo;
            config.Humidity = this.Humidity;
            return true;
        }
    }
    /// <summary>
    /// 监控卡烟雾更新
    /// </summary>
    [Serializable]
    public class MCSmokeUpdateInfo : MonitorCardElementUpdateInfo
    {
        /// <summary>
        /// 烟雾
        /// </summary>
        public bool IsSmokeAlarm
        {
            get
            {
                return _smoke;
            }
            set
            {
                _smoke = value;
            }
        }
        private bool _smoke = false;
        public override object Clone()
        {
            MCSmokeUpdateInfo data = new MCSmokeUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is MCSmokeUpdateInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            MCSmokeUpdateInfo config = obj as MCSmokeUpdateInfo;
            config.IsSmokeAlarm = this.IsSmokeAlarm;
            return true;
        }
    }
    /// <summary>
    /// 箱门更新
    /// </summary>
    [Serializable]
    public class MCDoorUpdateInfo : MonitorCardElementUpdateInfo
    {
        /// <summary>
        /// 箱门状态
        /// </summary>
        public bool IsDoorOpen
        {
            get { return _isDoorOpen; }
            set
            {
                _isDoorOpen = value;
            }
        }
        private bool _isDoorOpen = false;
        public override object Clone()
        {
            MCDoorUpdateInfo data = new MCDoorUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is MCDoorUpdateInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            MCDoorUpdateInfo config = obj as MCDoorUpdateInfo;
            config.IsDoorOpen = this.IsDoorOpen;
            return true;
        }
    }
    /// <summary>
    /// 风扇更新信息
    /// </summary>
    [Serializable]
    public class MCFansUpdateInfo : MonitorCardElementUpdateInfo
    {
        /// <summary>
        /// 风扇转速,key为第几路风扇，value为风扇转速
        /// </summary>
        public SerializableDictionary<int, int> FansMonitorInfoCollection
        {
            get
            {
                return _fansMonitorInfoCollection;
            }
            set
            {
                _fansMonitorInfoCollection = value;
            }
        }
        private SerializableDictionary<int, int> _fansMonitorInfoCollection = new SerializableDictionary<int, int>();

        public override object Clone()
        {
            MCFansUpdateInfo data = new MCFansUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is MCFansUpdateInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            MCFansUpdateInfo config = obj as MCFansUpdateInfo;
            if (this.FansMonitorInfoCollection == null)
            {
                config.FansMonitorInfoCollection = null;
            }
            else
            {
                config.FansMonitorInfoCollection = new SerializableDictionary<int, int>();
                foreach (KeyValuePair<int, int> keyvalue in this.FansMonitorInfoCollection)
                {
                    config.FansMonitorInfoCollection.Add(keyvalue.Key, keyvalue.Value);
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 电源更新
    /// </summary>
    [Serializable]
    public class MCPowerUpdateInfo : MonitorCardElementUpdateInfo
    {
        /// <summary>
        /// 电压监控key为第几路电源，value为电源电压
        /// </summary>
        public SerializableDictionary<int, float> PowerMonitorInfoCollection
        {
            get
            {
                return _powerMonitorInfo;
            }
            set
            {
                _powerMonitorInfo = value;
            }
        }
        private SerializableDictionary<int, float> _powerMonitorInfo = new SerializableDictionary<int, float>();

        public override object Clone()
        {
            MCPowerUpdateInfo data = new MCPowerUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is MCPowerUpdateInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            MCPowerUpdateInfo config = obj as MCPowerUpdateInfo;
            if (this.PowerMonitorInfoCollection == null)
            {
                config.PowerMonitorInfoCollection = null;
            }
            else
            {
                config.PowerMonitorInfoCollection = new SerializableDictionary<int, float>();
                foreach (KeyValuePair<int, float> keyvalue in this.PowerMonitorInfoCollection)
                {
                    config.PowerMonitorInfoCollection.Add(keyvalue.Key, keyvalue.Value);
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 箱体排线更新
    /// </summary>
    [Serializable]
    public class MCSocketCableUpdateInfo : MonitorCardElementUpdateInfo
    {
        /// <summary>
        /// 排线状态
        /// </summary>
        public List<SocketCableMonitorInfo> SocketCableInfoCollection
        {
            get
            {
                return _socketCableInfoCollection;
            }
            set
            {
                _socketCableInfoCollection = value;
            }
        }
        private List<SocketCableMonitorInfo> _socketCableInfoCollection = new List<SocketCableMonitorInfo>();

        public override object Clone()
        {
            MCSocketCableUpdateInfo data = new MCSocketCableUpdateInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is MCSocketCableUpdateInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            MCSocketCableUpdateInfo config = obj as MCSocketCableUpdateInfo;
            if (this.SocketCableInfoCollection == null)
            {
                config.SocketCableInfoCollection = null;
            }
            else
            {
                config.SocketCableInfoCollection = new List<SocketCableMonitorInfo>();
                foreach (SocketCableMonitorInfo socket in this.SocketCableInfoCollection)
                {
                    config.SocketCableInfoCollection.Add((SocketCableMonitorInfo)socket.Clone());
                }
            }
            return true;
        }
    }
    #endregion

    /// <summary>
    /// 排线状态监控
    /// </summary>
    [Serializable]
    public class SocketCableMonitorInfo : DeviceMonitorBaseInfo
    {
        public SerializableDictionary<int, List<SocketCableStatus>> SocketCableInfoDict
        {
            get
            {
                return _scoketCableInfoDict;
            }
            set
            {
                _scoketCableInfoDict = value;
            }
        }
        private SerializableDictionary<int, List<SocketCableStatus>> _scoketCableInfoDict = new SerializableDictionary<int, List<SocketCableStatus>>();

        public override object Clone()
        {
            SocketCableMonitorInfo data = new SocketCableMonitorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is SocketCableMonitorInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            SocketCableMonitorInfo config = obj as SocketCableMonitorInfo;
            if (this.SocketCableInfoDict == null)
            {
                config.SocketCableInfoDict = null;
            }
            else
            {
                config.SocketCableInfoDict = new SerializableDictionary<int,List<SocketCableStatus>>();
                foreach (KeyValuePair<int, List<SocketCableStatus>> keyvalue in this.SocketCableInfoDict)
                {
                    foreach (SocketCableStatus sock in keyvalue.Value)
                    {
                        if (!config.SocketCableInfoDict.ContainsKey(keyvalue.Key))
                        {
                            config.SocketCableInfoDict.Add(keyvalue.Key, new List<SocketCableStatus>());
                        }
                        config.SocketCableInfoDict[keyvalue.Key].Add((SocketCableStatus)sock.Clone());
                    }
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 多功能开状态
    /// </summary>
    [Serializable]
    public class FunctionCardMonitorInfo : DeviceMonitorBaseInfo
    {
        private SerializableDictionary<int, PeripheralMonitorBaseInfo> _peripheralInfoDict = new SerializableDictionary<int, PeripheralMonitorBaseInfo>();
        public SerializableDictionary<int, PeripheralMonitorBaseInfo> PeripheralInfoDict
        {
            get { return _peripheralInfoDict; }
            set { _peripheralInfoDict = value; }
        }

        public override object Clone()
        {
            FunctionCardMonitorInfo data = new FunctionCardMonitorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is FunctionCardMonitorInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            FunctionCardMonitorInfo config = obj as FunctionCardMonitorInfo;
            if (this.PeripheralInfoDict == null)
            {
                config.PeripheralInfoDict = null;
            }
            else
            {
                config.PeripheralInfoDict = new SerializableDictionary<int, PeripheralMonitorBaseInfo>();
                foreach (KeyValuePair<int, PeripheralMonitorBaseInfo> keyvalue in this.PeripheralInfoDict)
                {
                    config.PeripheralInfoDict.Add(keyvalue.Key, (PeripheralMonitorBaseInfo)keyvalue.Value.Clone());
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 外设状态
    /// </summary>
    [Serializable]
    public class PeripheralMonitorBaseInfo:ICopy,ICloneable
    {
        public DeviceWorkStatus DeviceStatus
        {
            get
            {
                return _deviceStatus;
            }
            set
            {
                _deviceStatus = value;
            }
        }
        private DeviceWorkStatus _deviceStatus = DeviceWorkStatus.Unknown;

        public object Clone()
        {
            PeripheralMonitorBaseInfo data = new PeripheralMonitorBaseInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is PeripheralMonitorBaseInfo))
            {
                return false;
            }
            PeripheralMonitorBaseInfo config = obj as PeripheralMonitorBaseInfo;
            config.DeviceStatus = this.DeviceStatus;
            return true;
        }
    }
    /// <summary>
    /// 光探头的监控数据
    /// </summary>
    [Serializable]
    public class LightSensorMonitorInfo : PeripheralMonitorBaseInfo
    {
        public int Lux
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 温度探头的监控数据
    /// </summary>
    [Serializable]
    public class TemperatureSensorMonitorInfo : PeripheralMonitorBaseInfo
    {
        public float Tempearture
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 湿度探头的监控数据
    /// </summary>
    [Serializable]
    public class HumiditySensorMonitorInfo : PeripheralMonitorBaseInfo
    {
        public float Humidity
        {
            get;
            set;
        }
    }

    [Serializable]
    public class DeconcentratorMonitorInfo : DeviceMonitorBaseInfo
    {
        public override object Clone()
        {
            DeconcentratorMonitorInfo data = new DeconcentratorMonitorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public override bool CopyTo(object obj)
        {
            if (!(obj is DeconcentratorMonitorInfo))
            {
                return false;
            }
            base.CopyTo(obj);
            DeconcentratorMonitorInfo config = obj as DeconcentratorMonitorInfo;
            return true;
        }
    }
    /// <summary>
    /// 设备搜索映射
    /// </summary>
    [Serializable]
    public class DeviceSearchMapping:ICopy,ICloneable
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public HWDeviceType Device
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
            }
        }
        private HWDeviceType _device = HWDeviceType.UnDefine;
        /// <summary>
        /// 该种设备串联上的第几个
        /// </summary>
        public int DeviceIndex
        {
            get
            {
                return _deviceIndex;
            }
            set
            {
                _deviceIndex = value;
            }
        }
        private int _deviceIndex = -1;
        private DeviceSearchMapping()
        {

        }
        public DeviceSearchMapping(HWDeviceType device, int deviceIndex)
        {
            _device = device;
            _deviceIndex = deviceIndex;
        }

        public object Clone()
        {
            DeviceSearchMapping data = new DeviceSearchMapping();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is DeviceSearchMapping))
            {
                return false;
            }
            DeviceSearchMapping config = obj as DeviceSearchMapping;
            config.Device = this.Device;
            config.DeviceIndex = this.DeviceIndex;
            return true;
        }
    }
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum HWDeviceType
    {
        UnDefine,
        Screen,
        Sender,
        PortOfSender,
        Deconcentrator,
        Scanner,
        MonitorCard,
        FansOfMonitorCard,
        PowerOfMonitorCard,
        SocketOfMonitorCard,
        FunctionCard,
        ComputerHWInfo,
    }
    /// <summary>
    /// 物理量
    /// </summary>
    public enum PhysicalType
    {
        voltage = 0,
        temperature = 1,
        humidity = 2,
        brightness = 3,
        fanspeed = 4,
        smoke = 5,
        workstatus = 6,
        connectedstatus = 7,
        dviRate = 8,
        doorOpenStatus = 9,
        ComputerStatus = 10,
        SocketCableStatus = 11,
        PortOfSenderStatus = 12,
        EnvironmentBrightness = 13
    }
    /// <summary>
    /// 设备工作状态
    /// </summary>
    public enum DeviceWorkStatus
    {
        OK,
        Error,
        Unknown,
        UnAvailable
    }
    /// <summary>
    /// 排线类型定义
    /// </summary>
    public enum SocketCableType
    {
        Red_Signal,
        Green_Signal,
        Blue_Signal,
        VRed_Signal,
        ABCD_Signal,
        LAT_Signal,
        OE_Signal,
        DCLK_Signal,
        CTRL_Signal
    }
    /// <summary>
    /// 排线的状态
    /// </summary>
    [Serializable]
    public class SocketCableStatus:ICopy,ICloneable
    {
        public SocketCableType CableType
        {
            get;
            set;
        }

        public bool IsCableOK
        {
            get;
            set;
        }

        public object Clone()
        {
            SocketCableStatus data = new SocketCableStatus();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is SocketCableStatus))
            {
                return false;
            }
            SocketCableStatus config = obj as SocketCableStatus;

            config.IsCableOK = this.IsCableOK;
            config.CableType = this.CableType;
            return true;
        }
    }
    /// <summary>
    /// 读监控数据错误类型
    /// </summary>
    public enum ReadMonitorDataErrType
    {
        OK,
        Unknown,
        BusyWorking,
        NoServerObj,
        NoScreenInfo,
        NoMonitorConfig,
        NoSupportConfig
    }

    public enum HWSettingResult
    {
        OK,
        NoServerObj,
        NoScreenInfo,
        Error
    }

    public enum InitialErryType
    {
        OK,
        NoServer,
        GetBaseInfoErr
    }

    public enum HWSystemType
    {
        M3,
        Unknow
    }

    public class AutoReadResultData : ICloneable, ICopy
    {
        public AutoReadResultData()
        {
            AutoBrightInfo = new AutoGetBrightInfo();
            AutoSensorInfo = new AutoGetSensorInfo();
        }

        public string SN { get; set; }
        public AutoGetBrightInfo AutoBrightInfo { get; set; }
        public AutoGetSensorInfo AutoSensorInfo { get; set; }
        public object Clone()
        {
            AutoReadResultData data = new AutoReadResultData();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is AutoReadResultData))
            {
                return false;
            }
            AutoReadResultData config = obj as AutoReadResultData;
            config.SN = this.SN;
            config.AutoBrightInfo = (AutoGetBrightInfo)this.AutoBrightInfo.Clone();
            config.AutoSensorInfo = (AutoGetSensorInfo)this.AutoSensorInfo.Clone();
            return true;
        }
    }

    public class AutoGetBrightInfo:ICloneable,ICopy
    {
        public AutoGetBrightInfo()
        {
            IsSucess = false;
            BrightValue = -1;
        }
        public bool IsSucess{get;set;}
        public int BrightValue { get; set; }

        public object Clone()
        {
            AutoGetBrightInfo data = new AutoGetBrightInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is AutoGetBrightInfo))
            {
                return false;
            }
            AutoGetBrightInfo config = obj as AutoGetBrightInfo;
            config.IsSucess = this.IsSucess;
            config.BrightValue = this.BrightValue;
            return true;
        }
    }
    public class AutoGetSensorInfo : ICloneable, ICopy
    {
        public AutoGetSensorInfo()
        {
            IsSucess = false;
            SensorValue = -1;
        }
        public bool IsSucess { get; set; }
        public int SensorValue { get; set; }

        public object Clone()
        {
            AutoGetSensorInfo data = new AutoGetSensorInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is AutoGetSensorInfo))
            {
                return false;
            }
            AutoGetSensorInfo config = obj as AutoGetSensorInfo;
            config.IsSucess = this.IsSucess;
            config.SensorValue = this.SensorValue;
            return true;
        }
    }
}
