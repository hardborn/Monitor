using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    [Serializable]
    public class MonitorConfigData
    {
        public MonitorConfigData()
        {
            MonitorCycle = new MonitorCycleData();
            MonitorUIConfig = new MonitorUIDisplayConfig();
            AllDisplayMonitorSysDataDic = new SerializableDictionary<string, OneDisplayMonitorData>();
            AllDataThresholdDic = new SerializableDictionary<string, DataThresholdInfo>();
            ControlFCInfos = new List<CtrlFuncCardPowerInfo>();
            ControlAliaNamesDic = new SerializableDictionary<string, string>();
            ScreenInfos = new SerializableDictionary<string, ScreenInfo>();
            //AllLightProbesDic
        }
        /// <summary>
        /// 刷新周期
        /// </summary>
        public MonitorCycleData MonitorCycle { get; set; }
        /// <summary>
        /// UI使用到的一些配置
        /// </summary>
        public MonitorUIDisplayConfig MonitorUIConfig { get; set; }
        /// <summary>
        /// 显示屏硬件配置
        /// </summary>
        public SerializableDictionary<string, OneDisplayMonitorData> AllDisplayMonitorSysDataDic { get; set; }
        /// <summary>
        /// 数据告警集体
        /// </summary>
        public SerializableDictionary<string, DataThresholdInfo> AllDataThresholdDic { get; set; }

        /// <summary>
        /// 供控制电源选择使用
        /// </summary>
        public List<CtrlFuncCardPowerInfo> ControlFCInfos { get; set; }

        public SerializableDictionary<string, string> ControlAliaNamesDic { get; set; }
        /// <summary>
        /// 光探头对象
        /// </summary>
        //public SerializableDictionary<string, List<光探头>> AllLightProbesDic { get; set; }

        /// <summary>
        /// 注册用户
        /// </summary>
        public string CareRegisterUser { get; set; }
        /// <summary>
        /// 屏体信息
        /// </summary>
        public SerializableDictionary<string, ScreenInfo> ScreenInfos { get; set; }

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is MonitorConfigData))
            {
                return false;
            }
            MonitorConfigData temp = (MonitorConfigData)obj;
            temp.MonitorCycle = (MonitorCycleData)this.MonitorCycle.Clone();
            temp.MonitorUIConfig = (MonitorUIDisplayConfig)this.MonitorUIConfig.Clone();
            if (this.AllDisplayMonitorSysDataDic == null)
            {
                temp.AllDisplayMonitorSysDataDic = null;
            }
            else
            {
                temp.AllDisplayMonitorSysDataDic = new SerializableDictionary<string, OneDisplayMonitorData>();
                foreach (KeyValuePair<string, OneDisplayMonitorData> keyvalue in this.AllDisplayMonitorSysDataDic)
                {
                    temp.AllDisplayMonitorSysDataDic.Add(keyvalue.Key, (OneDisplayMonitorData)keyvalue.Value.Clone());
                }
            }

            if (this.AllDataThresholdDic == null)
            {
                temp.AllDataThresholdDic = null;
            }
            else
            {
                temp.AllDataThresholdDic = new SerializableDictionary<string, DataThresholdInfo>();
                foreach (KeyValuePair<string, DataThresholdInfo> keyvalue in this.AllDataThresholdDic)
                {
                    temp.AllDataThresholdDic.Add(keyvalue.Key, (DataThresholdInfo)keyvalue.Value.Clone());
                }
            }

            if (this.ControlFCInfos == null)
            {
                temp.ControlFCInfos = null;
            }
            else
            {
                temp.ControlFCInfos = new List<CtrlFuncCardPowerInfo>();
                foreach (CtrlFuncCardPowerInfo ctrl in this.ControlFCInfos)
                {
                    temp.ControlFCInfos.Add(ctrl);
                }
            }

            if (this.ControlAliaNamesDic == null)
            {
                temp.ControlAliaNamesDic = null;
            }
            else
            {
                temp.ControlAliaNamesDic = new SerializableDictionary<string, string>();
                foreach (KeyValuePair<string, string> keyvalue in this.ControlAliaNamesDic)
                {
                    temp.ControlAliaNamesDic.Add(keyvalue.Key, keyvalue.Value);
                }
            }

            temp.CareRegisterUser = this.CareRegisterUser;
            if (this.ScreenInfos == null)
            {
                temp.ScreenInfos = null;
            }
            else
            {
                temp.ScreenInfos = new SerializableDictionary<string,ScreenInfo>();
                foreach (KeyValuePair<string, ScreenInfo> keyvalue in this.ScreenInfos)
                {
                    temp.ScreenInfos.Add(keyvalue.Key, (ScreenInfo)keyvalue.Value.Clone());
                }
            }

            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            MonitorConfigData newObj = new MonitorConfigData();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
        #endregion
    }
    [Serializable]
    public class ScreenInfo
    {
        public ScreenInfo()
        {
            IsScreenRegister = false;
            ScreenLongitude = 0;
            ScreenLatitude = 0;
            ScreenWidth = 0;
            ScreenHeight = 0;
            ScreenSenderCount = 0;
            ScreenScanerCount = 0;
            ScreenMonitorCount = 0;
        }
        /// <summary>
        /// 屏编号
        /// </summary>
        public string ScreenSN { get; set; }
        /// <summary>
        /// 屏别名
        /// </summary>
        public string ScreenAliaName { get; set; }
        /// <summary>
        /// 屏宽度
        /// </summary>
        public int ScreenWidth { get; set; }
        /// <summary>
        /// 屏高度
        /// </summary>
        public int ScreenHeight { get; set; }
        /// <summary>
        /// 是否注册
        /// </summary>
        public bool IsScreenRegister { get; set; }
        /// <summary>
        /// 屏经度
        /// </summary>
        public float ScreenLongitude{get;set;}
        /// <summary>
        /// 屏纬度
        /// </summary>
        public float ScreenLatitude { get; set; }
        /// <summary>
        /// 屏发送卡个数
        /// </summary>
        public int ScreenSenderCount { get; set; }
        /// <summary>
        /// 屏接收卡个数
        /// </summary>
        public int ScreenScanerCount { get; set; }
        /// <summary>
        /// 屏监控卡个数
        /// </summary>
        public int ScreenMonitorCount { get; set; }

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is ScreenInfo))
            {
                return false;
            }
            ScreenInfo temp = (ScreenInfo)obj;
            temp.ScreenSN = this.ScreenSN;
            temp.ScreenAliaName = this.ScreenAliaName;
            temp.ScreenWidth = this.ScreenWidth;
            temp.ScreenHeight = this.ScreenHeight;
            temp.IsScreenRegister = this.IsScreenRegister;
            temp.ScreenLongitude = this.ScreenLongitude;
            temp.ScreenLatitude = this.ScreenLatitude;
            temp.ScreenSenderCount = this.ScreenSenderCount;
            temp.ScreenScanerCount = this.ScreenScanerCount;
            temp.ScreenMonitorCount = this.ScreenMonitorCount;
            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            ScreenInfo newObj = new ScreenInfo();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
        #endregion
    }

    /// <summary>
    /// UI辅助内容
    /// </summary>
    public class MonitorUIDisplayConfig
    {
        public MonitorUIDisplayConfig()
        {
            IsDisplayMonitorInfoSame = true;
            IsDataThresholdSame = true;
            TempDisplayType = TemperatureType.Celsius;
        }
        /// <summary>
        /// 温度单位
        /// </summary>
        public TemperatureType TempDisplayType { get; set; }

        /// <summary>
        /// 显示屏是否一样配置
        /// </summary>
        public bool IsDisplayMonitorInfoSame { get; set; }

        /// <summary>
        /// 数据告警是否统一配置
        /// </summary>
        public bool IsDataThresholdSame { get; set; }
        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is MonitorUIDisplayConfig))
            {
                return false;
            }
            MonitorUIDisplayConfig temp = (MonitorUIDisplayConfig)obj;
            temp.IsDataThresholdSame = this.IsDataThresholdSame;
            temp.IsDisplayMonitorInfoSame = this.IsDisplayMonitorInfoSame;
            temp.TempDisplayType = this.TempDisplayType;
            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            MonitorUIDisplayConfig newObj = new MonitorUIDisplayConfig();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
        #endregion
    }

    [Serializable]
    public class MonitorCycleData
    {
        public MonitorCycleData()
        {
            IsCycleMonitor = false;
            RetryReadTimes = 1;
            MonitorPeriod = 60000;
        }
        /// <summary>
        /// 是否周期刷新
        /// </summary>
        public bool IsCycleMonitor { get; set; }
        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int RetryReadTimes { get; set; }
        /// <summary>
        /// 周期的时长
        /// </summary>
        public int MonitorPeriod { get; set; }

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is MonitorCycleData))
            {
                return false;
            }
            MonitorCycleData temp = (MonitorCycleData)obj;
            temp.IsCycleMonitor = this.IsCycleMonitor;
            temp.RetryReadTimes = this.RetryReadTimes;
            temp.MonitorPeriod = this.MonitorPeriod;
            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            MonitorCycleData newObj = new MonitorCycleData();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
        #endregion
    }

    #region 监测及告警配置信息

    [Serializable]
    public class OneDisplayMonitorData
    {
        public OneDisplayMonitorData()
        {
            IsUpdateSBStatus = true;
            IsDisplaySBValtage = true;
            IsUpdateMCStatus = false;
            IsUpdateHumidity = false;
            IsUpdateSmoke = false;
            IsUpdateRowLine = false;
            IsUpdateGeneralStatus = false;
            IsUpdateFan = false;
            IsUpdateMCVoltage = false;

            MCFanInfo = new ScanBdMonitoredParamUpdateData();
            MCPowerInfo = new ScanBdMonitoredPowerData();
        }

        public string FirstSenderSN { get; set; }

        /// <summary>
        /// 刷新状态
        /// </summary>
        public bool IsUpdateSBStatus { get; set; }
        /// <summary>
        /// 刷新电压
        /// </summary>
        public bool IsDisplaySBValtage { get; set; }
        /// <summary>
        /// 连接监控卡
        /// </summary>
        public bool IsUpdateMCStatus { get; set; }
        /// <summary>
        /// 刷新湿度
        /// </summary>
        public bool IsUpdateHumidity { get; set; }
        /// <summary>
        /// 刷新烟雾
        /// </summary>
        public bool IsUpdateSmoke { get; set; }
        /// <summary>
        /// 刷新箱体状态
        /// </summary>
        public bool IsUpdateRowLine { get; set; }
        /// <summary>
        /// 刷新箱门状态
        /// </summary>
        public bool IsUpdateGeneralStatus { get; set; }
        /// <summary>
        /// 刷新风扇
        /// </summary>
        public bool IsUpdateFan { get; set; }
        /// <summary>
        /// 刷新监控卡电源
        /// </summary>
        public bool IsUpdateMCVoltage { get; set; }
        /// <summary>
        /// 刷新风扇信息
        /// </summary>
        public ScanBdMonitoredParamUpdateData MCFanInfo { get; set; }
        /// <summary>
        /// 刷新监控卡信息
        /// </summary>
        public ScanBdMonitoredPowerData MCPowerInfo { get; set; }

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is OneDisplayMonitorData))
            {
                return false;
            }
            OneDisplayMonitorData temp = (OneDisplayMonitorData)obj;

            temp.FirstSenderSN = this.FirstSenderSN;
            temp.IsUpdateSBStatus = this.IsUpdateSBStatus;
            temp.IsUpdateMCStatus = this.IsUpdateMCStatus;
            temp.IsUpdateHumidity = this.IsUpdateHumidity;
            temp.IsDisplaySBValtage = this.IsDisplaySBValtage;
            temp.IsUpdateSmoke = this.IsUpdateSmoke;
            temp.IsUpdateFan = this.IsUpdateFan;
            temp.IsUpdateRowLine = this.IsUpdateRowLine;
            temp.IsUpdateMCVoltage = this.IsUpdateMCVoltage;
            temp.IsUpdateGeneralStatus = this.IsUpdateGeneralStatus;
            temp.MCFanInfo = (ScanBdMonitoredParamUpdateData)this.MCFanInfo.Clone();
            temp.MCPowerInfo = (ScanBdMonitoredPowerData)this.MCPowerInfo.Clone();
            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            OneDisplayMonitorData newObj = new OneDisplayMonitorData();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
        #endregion
    }

    #region 风扇电源对象
    [Serializable]
    public class ScanBdMonitorInfo
    {
        public ScanBdMonitorInfo()
        {
            CountType = ScanBdMonitoredParamCountType.SameForEachScanBd;
            SameCount = 4;
            CountDicOfScanBd = new SerializableDictionary<string, byte>();
        }
        /// <summary>
        /// 数据是否相同
        /// </summary>
        public ScanBdMonitoredParamCountType CountType { get; set; }
        /// <summary>
        /// 相同时的个数
        /// </summary>
        public byte SameCount { get; set; }
        /// <summary>
        /// 不同时的列表
        /// </summary>
        public SerializableDictionary<string, byte> CountDicOfScanBd { get; set; }

        #region ICopy 成员
        public virtual bool CopyTo(object obj)
        {
            if (!(obj is ScanBdMonitorInfo))
            {
                return false;
            }
            ScanBdMonitorInfo temp = (ScanBdMonitorInfo)obj;

            temp.CountType = this.CountType;
            temp.SameCount = this.SameCount;
            if (this.CountDicOfScanBd == null)
            {
                temp.CountDicOfScanBd = null;
            }
            else
            {
                temp.CountDicOfScanBd = new SerializableDictionary<string, byte>();
                foreach (string key in this.CountDicOfScanBd.Keys)
                {
                    temp.CountDicOfScanBd.Add(key, this.CountDicOfScanBd[key]);
                }
            }
            return true;
        }
        #endregion

        #region ICloneable 成员
        public virtual object Clone()
        {
            ScanBdMonitorInfo newObj = new ScanBdMonitorInfo();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
        #endregion
    }

    [Serializable]
    public class ScanBdMonitoredParamUpdateData:ScanBdMonitorInfo
    {
        public ScanBdMonitoredParamUpdateData()
        {
            CountDicOfScanBd = null;
            SameCount = 4;
            FanPulseCount = 1;
        }
        /// <summary>
        /// 风扇脉冲
        /// </summary>
        public int FanPulseCount { get; set; }

        public override bool CopyTo(object obj)
        {
            if(!base.CopyTo(obj))
            {
                return false;
            }
            (obj as ScanBdMonitoredParamUpdateData).FanPulseCount = this.FanPulseCount;
            return true;
        }

        public override object Clone()
        {
            ScanBdMonitoredParamUpdateData newObj = new ScanBdMonitoredParamUpdateData();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
    }
    [Serializable]
    public class ScanBdMonitoredPowerData : ScanBdMonitorInfo
    {
        public ScanBdMonitoredPowerData()
        {
            CountDicOfScanBd = null;
            SameCount = 3;
            VoltageThreshold = 4;
        }

        /// <summary>
        /// 电压告警
        /// </summary>
        public int VoltageThreshold { get; set; }

        public override bool CopyTo(object obj)
        {
            if (!base.CopyTo(obj))
            {
                return false;
            }
            (obj as ScanBdMonitoredPowerData).VoltageThreshold = this.VoltageThreshold;
            return true;
        }

        public override object Clone()
        {
            ScanBdMonitoredPowerData newObj = new ScanBdMonitoredPowerData();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
    }
    #endregion

    /// <summary>
    /// 数据阀值配置
    /// </summary>
    [Serializable]
    public class DataThresholdInfo
    {
        public DataThresholdInfo()
        {
            TemperatureThreshold = 60;
            HumidityThreshold = 60;
            FanSpeedThreshold = 1000;
            VoltageThreshold = 4;
        }

        /// <summary>
        /// 屏名称
        /// </summary>
        public string FirstSenderSN { get; set; }
        /// <summary>
        /// 温度阀值
        /// </summary>
        public int TemperatureThreshold { get; set; }
        /// <summary>
        /// 电压阀值
        /// </summary>
        public int VoltageThreshold { get; set; }
        /// <summary>
        /// 湿度阀值
        /// </summary>
        public int HumidityThreshold { get; set; }
        /// <summary>
        /// 转速阀值
        /// </summary>
        public int FanSpeedThreshold { get; set; }
        
        public bool CopyTo(object obj)
        {
            if (!(obj is DataThresholdInfo))
            {
                return false;
            }
            DataThresholdInfo temp = (DataThresholdInfo)obj;

            temp.FirstSenderSN = this.FirstSenderSN;
            temp.TemperatureThreshold = this.TemperatureThreshold;
            temp.VoltageThreshold = this.VoltageThreshold;
            temp.HumidityThreshold = this.HumidityThreshold;
            temp.FanSpeedThreshold = this.FanSpeedThreshold;

            return true;
        }

        public object Clone()
        {
            DataThresholdInfo newObj = new DataThresholdInfo();
            bool res = this.CopyTo(newObj);
            if (!res)
            {
                return null;
            }
            else
            {
                return newObj;
            }
        }
    }
    /// <summary>
    /// 多功能电源信息
    /// </summary>
    [Serializable]
    public class CtrlFuncCardInfo
    {
        public bool FCIsConnectCommPort { get; set; }
        public byte SenderAddr { get; set; }
        public byte PortAddr { get; set; }
        public UInt16 FuncCardAddr { get; set; }
        public string FCName { get; set; }
        public string CommPort { get; set; }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlFCAddr newObj = new CtrlFCAddr();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlFCAddr info = (CtrlFCAddr)obj;
            if (info == null)
            {
                return false;
            }
            info.FCIsConnectCommPort = FCIsConnectCommPort;
            info.SenderAddr = SenderAddr;
            info.PortAddr = PortAddr;
            info.FuncCardAddr = FuncCardAddr;
            info.FCName = FCName;
            info.CommPort = CommPort;
            return true;
        }
    }
    /// <summary>
    /// 电源控制具体内容，供策略选择使用
    /// </summary>
    [Serializable]
    public class CtrlFuncCardPowerInfo
    {
        public CtrlFuncCardPowerInfo()
        {
            FCAddrInfo = new CtrlFuncCardInfo();
        }
        public CtrlFuncCardInfo FCAddrInfo { get; set; }
        public byte PowerIndex { get; set; }
        public string PowerName { get; set; }
        public string AliaPowerName { get; set; }
        /// </summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlFuncCardPowerInfo newObj = new CtrlFuncCardPowerInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlFuncCardPowerInfo info = (CtrlFuncCardPowerInfo)obj;
            if (info == null)
            {
                return false;
            }
            info.FCAddrInfo = (CtrlFuncCardInfo)FCAddrInfo.Clone();
            info.PowerIndex = PowerIndex;
            info.PowerName = PowerName;
            info.AliaPowerName = AliaPowerName;
            return true;
        }
    }
    #endregion
}