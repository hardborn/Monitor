using System;
using System.Collections.Generic;
using System.Text;
using Nova.LCT.GigabitSystem.Common;
using System.Drawing;
using Nova.Convert;
using Nova.Xml.Serialization;
using Nova.LCT.GigabitSystem.Monitor;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    [Serializable]
    public enum ScanBdMonitoredParamCountType
    {
        SameForEachScanBd = 0,
        DifferentForEachScanBd
    }
    [Serializable]
    /// <summary>
    /// 温度显示类型
    /// </summary>
    public enum TemperatureType
    {
        /// <summary>
        /// 摄氏
        /// </summary>
        Celsius = 0,
        /// <summary>
        /// 华氏
        /// </summary>
        Fahrenheit
    }
    public enum CtrlSytemMode
    {
        HasSenderMode = 0,
        NoSenderMode
    }
    [Serializable]
    public class ScanBdMonitoredParamUpdateInfo
    {
        public ScanBdMonitoredParamCountType CountType = ScanBdMonitoredParamCountType.SameForEachScanBd;
        public int AlarmThreshold = 0;
        public byte SameCount = 4;

        public SerializableDictionary<string, byte> CountDicOfScanBd = new SerializableDictionary<string, byte>();

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is ScanBdMonitoredParamUpdateInfo))
            {
                return false;
            }
            ScanBdMonitoredParamUpdateInfo temp = (ScanBdMonitoredParamUpdateInfo)obj;

            temp.CountType = this.CountType;
            temp.AlarmThreshold = this.AlarmThreshold;
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
        public object Clone()
        {
            ScanBdMonitoredParamUpdateInfo newObj = new ScanBdMonitoredParamUpdateInfo();
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
    public class ScanBdMonitoredPowerInfo
    {
        public ScanBdMonitoredParamCountType CountType = ScanBdMonitoredParamCountType.SameForEachScanBd;
        public float AlarmThreshold = 0;
        public float FaultThreshold = 0;
        public byte SameCount = 4;

        public SerializableDictionary<string, byte> CountDicOfScanBd = new SerializableDictionary<string, byte>();

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is ScanBdMonitoredPowerInfo))
            {
                return false;
            }
            ScanBdMonitoredPowerInfo temp = (ScanBdMonitoredPowerInfo)obj;

            temp.CountType = this.CountType;
            temp.AlarmThreshold = this.AlarmThreshold;
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
        public object Clone()
        {
            ScanBdMonitoredPowerInfo newObj = new ScanBdMonitoredPowerInfo();
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
    public class MonitorConfigData
    {
        public MonitorConfigData()
        {
            TempDisplayType = TemperatureType.Celsius;
            TempAlarmThreshold = 60;
            HumiAlarmThreshold = 60;
            FanSpeed = 1000;
            PowerAlarmValue = 4;
            PowerFaultValue = 3.5f;
            IsDisplayScanBoardVolt = true;
            MCFanInfo = new ScanBdMonitoredParamUpdateInfo();
            MCPowerInfo = new ScanBdMonitoredPowerInfo();
        }
        public TemperatureType TempDisplayType { get; set; }
        public float TempAlarmThreshold { get; set; }
        public float HumiAlarmThreshold { get; set; }
        public float FanSpeed { get; set; }
        public float PowerAlarmValue { get; set; }
        public float PowerFaultValue { get; set; }

        public bool IsDisplayScanBoardVolt { get; set; }

        public ScanBdMonitoredParamUpdateInfo MCFanInfo { get; set; }
        public ScanBdMonitoredPowerInfo MCPowerInfo { get; set; }
    }

    public enum ClickLinkLabelType
    {
        MaxValue = 0,
        MinValue,
        None
    }
    public class ClickMaxMinValueEventArgs : EventArgs
    {
        public ClickLinkLabelType Type
        {
            get { return _type; }
        }
        private ClickLinkLabelType _type = ClickLinkLabelType.None;
        public ClickMaxMinValueEventArgs(ClickLinkLabelType type)
        {
            _type = type;
        }
    }
    public delegate void ClickMaxMinValueEventHandler(object sender, ClickMaxMinValueEventArgs e);
    /// <summary>
    /// 控件库应用到的字符串及对应的枚举
    /// </summary>
    public class CommonStaticValue
    {
        /// <summary>
        /// 摄氏温度的单位
        /// </summary>
        public static string CelsiusUnit = "℃";
        /// <summary>
        /// 华氏温度的单位
        /// </summary>
        public static string FahrenheitUnit = "℉";
        /// <summary>
        /// 湿度的单位
        /// </summary>
        public readonly static string HUMIDITY_UNIT = "%";

        public enum ColsHeaderType
        {
            Info = 0,
            SenderIndex,
            PortIndex,
            ScanBordIndex,
            StartX,
            StartY,
            SBWidth,
            SBHeight
        }
        //DataGridView的列头
        public static string[] ColsHeaderText = new string[] { "信息", "发送卡", "网口", "接收卡",
                                                               "起始X", "起始Y", "宽度", "高度"};
        //DataGridView第一列的列头
        public static string[] DisplayTypeStr = new string[] { "接收卡状态", "温度", "监控卡状态", "湿度", "烟雾", "风扇", "电源", "箱体状态", "箱门状态", "全部" };

        public enum NoticeType
        {
            OK = 0,
            Error,
            Alarm,
            Unkown,
            Invalid,
            VoltageException
        }
        public enum CabinetDoorStatusType
        {
            Close = 0,
            Open
        }
        public static string[] StatusNoticeStr = new string[] { "正常", "故障", "告警", "未知", "无效", "电压异常" };

        public static string ScanBordNameStr = "接收卡";
        public static string RowColNameStr = "行,列";

        public static string[] SBStatusDisplayStr = new string[] { "接收卡总个数", "故障的接收卡个数", "电压异常的接收卡个数" };
        public static string[] MCStatusDisplayStr = new string[] { "监控卡总个数", "故障的监控卡个数", "电压异常的监控卡个数" };
        public static string[] SmokeDisplayStr = new string[] { "接收卡总个数", "烟雾告警的接收卡个数" };
        public static string[] FanDisplayStr = new string[] { "风扇总个数", "告警的风扇个数" };
        public static string[] PowerDisplayStr = new string[] { "电源总个数", "告警的电源个数" };
        public static string[] TemperatureDisplayStr = new string[] {"最低温度", "最高温度", "平均温度", 
                                                                      "过平均温度的接收卡数量",
                                                                      "温度告警的接收卡数量"};
        public static string[] HumidityDisplayStr = new string[] {"最低湿度", "最高湿度", "平均湿度", 
                                                                  "过平均湿度的接收卡数量",
                                                                  "湿度告警的接收卡数量"};
        public static string[] RowLineStr = new string[] { "箱体总个数", "故障的箱体数量"};
        public static string[] GeneralStatusStr = new string[] { "箱体总个数", "箱门开启的箱体个数" };
        public static string[] CabinetDoorStatusStr = new string[] { "关闭", "开启" };
        public static string PreviewComplexMonitorInfo = "查看复杂屏监控数据";

        public static string SupplyVoltage = "供电电压";
        public static string FaultInformation = "故障信息";
        public static string NotConnectMC = "未连接监控卡";
        public static string SoketName = "插座";
        public static string GroupName = "数据组数";
        public static string ScanSignalName = "扫描";
        public static string SignalName = "信号";
        public static string[] RGBSignalStr = new string[] { "红信号", "绿信号", "蓝信号", "虚拟红信号" };
        public enum SwitchSignType
        {
            Fan,
            Power,
            SBPower,
            MCSelefPower,
            MCOtherPower
        }
        public static string[] SwitchSignStr = new string[] {  "风扇", "电源", "接收卡板载电压", "监控卡板载电压", "监控卡电压"};

        public static string SwitchName = "路数";
        public static string CommPortScreen = "屏";
        public static string DoubleClickToViewInfo = "详细信息:请双击查看!";
        public static string ScreenNotExist = "不存在!";
    }

    public enum PicType
    {
        Fan,
        Power
    }

    public enum MonitorInfoResult
    {
        Ok = 0,
        Fault = 1,
        Alarm = 2,
        Unknown,

    }
    public class FanAndPowerRepaintInfo
    {
        public static byte[] DecimalCount = new byte[] { 0, 2 };


        public static Image[] AlarmImage = new Image[] 
                                    { 
                                        global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.Fan_alarm,
                                        //null
                                        global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.Yellowpoint
                                    };

        public static Image[] OKImage = new Image[] 
                                    { 
                                        global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.Fan_OK,
                                        //null
                                        global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.greenpoint
                                    };
        public static Image[] InvalidImage = new Image[] 
                                    { 
                                        global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.Fan_invalid,
                                        //null
                                        global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.graypoint
                                    };
    }

    public enum GridType
    {
        ScanBoardGird = 0,
        ComplexScreenGrid,
        ScreenGrid
    }
    public interface IGridBiningObject
    {
        GridType Type
        {
            get;
        }
    }

    public class ScanBoardGridBindObj : IGridBiningObject
    {
        public GridType Type
        {
            get
            {
                return GridType.ScanBoardGird;
            }
        }
        public SBInfoAndMonitorInfo ScanBoardAndMonitorInfo = new SBInfoAndMonitorInfo();

    }
    public class ComplexScreenGridBindObj : IGridBiningObject
    {
        public GridType Type
        {
            get
            {
                return GridType.ComplexScreenGrid;
            }
        }
        public string CommPortName = "";
        public byte ScreenIndex = 0;
        public UC_ComplexScreenLayout ComplexScreenLayout = null;
    }
    public class ScreenGridBindObj : IGridBiningObject
    {
        public GridType Type
        {
            get
            {
                return GridType.ScreenGrid;
            }
        }
        public string CommPortName = "";
        public byte ScreenIndex;
        public Rectangle ScreenRect = Rectangle.Empty;
        public bool ScreenIsValid = true;
    }
    /// <summary>
    /// 每个接收卡监控信息及自身信息
    /// </summary>
    public class SBInfoAndMonitorInfo
    {
        public ScannerMonitorData MonitorData = null;
        public ScanBoardRegionInfo ScanBordInfo = null;
        public ScanBoardRowLineStatus RowLineStatus = null;
        public List<bool> GeneralSwitchList = null;
        public VirtualModeType virModeType = VirtualModeType.Unknown;
        public int SBColIndex = 0;
        public int SBRowIndex = 0;
        public string CommPortName = "";
        public byte ScreenIndex = 0;
        public string SBAddress = ""; //发送卡 + 网口号 + 接收卡号
        public string SBRectKey = ""; //串口号 + 发送卡 + 网口号 + 接收卡号


        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is SBInfoAndMonitorInfo))
            {
                return false;
            }
            SBInfoAndMonitorInfo temp = (SBInfoAndMonitorInfo)obj;

            temp.SBColIndex = this.SBColIndex;
            temp.SBRowIndex = this.SBRowIndex;
            temp.CommPortName = this.CommPortName;
            temp.SBAddress = this.SBAddress;
            temp.SBRectKey = this.SBRectKey;
            temp.ScreenIndex = this.ScreenIndex;
            temp.virModeType = this.virModeType;
            if (this.MonitorData != null)
            {
                temp.MonitorData = (ScannerMonitorData)this.MonitorData.Clone();
            }
            else
            {
                temp.MonitorData = null;
            }

            if (this.ScanBordInfo != null)
            {
                temp.ScanBordInfo = (ScanBoardRegionInfo)this.ScanBordInfo.Clone();
            }
            else
            {
                temp.ScanBordInfo = null;
            }
            if (this.RowLineStatus != null)
            {
                temp.RowLineStatus = (ScanBoardRowLineStatus)this.RowLineStatus.Clone();
            }
            else
            {
                temp.RowLineStatus = null;
            }
            if (this.GeneralSwitchList != null)
            {
                temp.GeneralSwitchList = new List<bool>();
                for (int i = 0; i < this.GeneralSwitchList.Count; i++)
                {
                    temp.GeneralSwitchList.Add(this.GeneralSwitchList[i]);
                }
            }
            else
            {
                temp.GeneralSwitchList = null;
            }
            return true;
        }

        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            SBInfoAndMonitorInfo newObj = new SBInfoAndMonitorInfo();
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

    public class TempAndHumiStatisticsInfo
    {
        public int ValidScanBoardCnt = 0;
        public float MaxValue = 0;
        public float MinValue = 0;
        public List<float> AllValueList = new List<float>();
        public float AverageValue = 0;
        public int BeyondAverageCnt = 0;
        public float TotalValue = 0;

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is TempAndHumiStatisticsInfo))
            {
                return false;
            }
            TempAndHumiStatisticsInfo temp = (TempAndHumiStatisticsInfo)obj;

            temp.ValidScanBoardCnt = this.ValidScanBoardCnt;
            temp.MaxValue = this.MaxValue;
            temp.MinValue = this.MinValue;
            temp.AverageValue = this.AverageValue;
            temp.BeyondAverageCnt = this.BeyondAverageCnt;
            temp.TotalValue = this.TotalValue;
            temp.AllValueList = new List<float>();
            if (this.AllValueList != null)
            {
                for (int i = 0; i < this.AllValueList.Count; i++)
                {
                    temp.AllValueList.Add(this.AllValueList[i]);
                }
            }
            return true;
        }

        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            TempAndHumiStatisticsInfo newObj = new TempAndHumiStatisticsInfo();
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
    public enum ValueCompareResult
    {
        EqualsMaxValue = 0,
        EqualsMinValue,
        AboveMaxValue,
        AboveMinValue,
        BelowMaxValue,
        BelowMinValue,
        IsFirstValidValue,
        EqualsBothValue,
        Unknown
    }

    /// <summary>
    /// 从监控信息中获取不同类型的颜色和值
    /// </summary>
    public class GetMonitorColorAndValue
    {
        /// <summary>
        /// 获取设置的需要监控的监控卡的风扇和电源个数
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="sbCommKey"></param>
        /// <returns></returns>
        public static uint GetMonitorFanCnt(ScanBdMonitoredParamUpdateInfo configInfo, string sbCommKey)
        {
            if (configInfo.CountType == ScanBdMonitoredParamCountType.SameForEachScanBd)
            {
                return configInfo.SameCount;
            }
            else
            {
                if (configInfo.CountDicOfScanBd != null
                    && configInfo.CountDicOfScanBd.ContainsKey(sbCommKey))
                {
                    return configInfo.CountDicOfScanBd[sbCommKey];
                }
                else
                {
                    return configInfo.SameCount;
                }
            }
        }

        /// <summary>
        /// 获取设置的需要监控的监控卡的风扇和电源个数
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="sbCommKey"></param>
        /// <returns></returns>
        public static uint GetMonitorPowerCnt(ScanBdMonitoredPowerInfo configInfo, string sbCommKey)
        {
            if (configInfo.CountType == ScanBdMonitoredParamCountType.SameForEachScanBd)
            {
                return configInfo.SameCount;
            }
            else
            {
                if (configInfo.CountDicOfScanBd != null
                    && configInfo.CountDicOfScanBd.ContainsKey(sbCommKey))
                {
                    return configInfo.CountDicOfScanBd[sbCommKey];
                }
                else
                {
                    return configInfo.SameCount;
                }
            }
        }

        public static float GetDisplayTempValueByCelsius(TemperatureType tempDisplayType, float fvalue)
        {
            if (tempDisplayType == TemperatureType.Celsius)
            {
                return fvalue;
            }
            else
            {
                return 32 + fvalue * 1.8f;
            }
        }

        #region 获取颜色显示信息
        /// <summary>
        /// 获取不同温度的颜色
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="curScanBordIndex"></param>
        /// <param name="tempObject"></param>
        /// <param name="clr"></param>
        public static bool DetectTempIsValidAndGetInfo(ColorGradePartition clrGradePartition,
                                                       TemperatureType tempDisplayType,
                                                       ScannerMonitorData monitorData, 
                                                       ref int value, ref Color clr)
        {
            if (monitorData != null)
            {
                //ErrorCode  -- Error
                //if (monitorData.TemperatureOfMonitorCard.IsValid)
                //{
                //    //监控卡温度有效
                //    value = monitorData.TemperatureOfMonitorCard.Value;
                //}
                //else if (monitorData.TemperatureOfScanCard.IsValid)
                //{
                //    //监控卡温度无效
                //    value = monitorData.TemperatureOfScanCard.Value;
                //}
                if (monitorData.TemperatureOfScanCard.IsValid)
                {
                    //接收卡温度
                    value = (int)GetDisplayTempValueByCelsius(tempDisplayType,
                                                              monitorData.TemperatureOfScanCard.Value);
                }
                else
                {
                    clr = Color.Gray;
                    return false;
                }
                clrGradePartition.GetGradeColor(value, ref clr);
                return true;

            }
            else
            {
                clr = Color.Gray;
                return false;
            }
        }
        /// <summary>
        /// 获取湿度字符串和湿度颜色
        /// </summary>
        /// <param name="clrGradePartition"></param>
        /// <param name="monitorData"></param>
        /// <param name="value"></param>
        /// <param name="clr"></param>
        /// <returns></returns>
        public static bool DetectHumiValidAndGetInfo(ColorGradePartition clrGradePartition, 
                                                     ScannerMonitorData monitorData, 
                                                     ref int value, ref Color clr)
        {
            if (monitorData != null)
            {
                if (monitorData.IsConnectMC 
                    && monitorData.HumidityOfMonitorCard.IsValid)
                {
                    //监控卡湿度有效
                    value = (int)monitorData.HumidityOfMonitorCard.Value;
                }
                else if (monitorData.HumidityOfScanCard.IsValid)
                {
                    //监控卡湿度无效
                    value = (int)monitorData.HumidityOfScanCard.Value;
                }
                else
                {
                    clr = Color.Gray;
                    return false;
                }
                clrGradePartition.GetGradeColor(value, ref clr);
                return true;
            }
            else
            {
                clr = Color.Gray;
                return false;
            }
        }
        /// <summary>
        /// 获取不同状态的颜色
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetSBStatusColorAndStr(ScannerMonitorData monitorData, ref Color clr)
        {
            clr = Color.Gray;
            if (monitorData != null)
            {
                if (monitorData.WorkStatus == WorkStatusType.Error)
                {
                    clr = Color.Red;
                }
                else if (monitorData.WorkStatus == WorkStatusType.OK)
                {
                    if (monitorData.VoltageOfScanCard.IsValid)
                    {
                        if (monitorData.VoltageOfScanCard.Value > StatisticsMonitorInfo.MAXSUPPLYVOLTAGE
                            || monitorData.VoltageOfScanCard.Value < StatisticsMonitorInfo.MINXSUPPLYVOLTAGE)
                        {
                            clr = Color.Yellow;
                        }
                        else
                        {
                            clr = Color.Green;
                        }
                    }
                    else
                    {
                        //电压无效时认为正常
                        clr = Color.Green;
                    }
                }
            }
        }
        /// <summary>
        /// 获取不同烟雾报警状态的颜色
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetSmokeColorAndStr(ScannerMonitorData monitorData, ref Color clr)
        {
            clr = Color.Gray;
            if (monitorData != null && monitorData.IsConnectMC)
            {
                if (monitorData.SmokeWarn.IsValid)
                {
                    if (monitorData.SmokeWarn.IsSmokeAlarm)
                    {
                        clr = Color.Yellow;
                    }
                    else
                    {
                        clr = Color.Green;
                    }
                }
            }
        }
        /// <summary>
        /// 获取不同状态的颜色
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetMCStatusColorAndStr(ScannerMonitorData monitorData, ref Color clr)
        {
            clr = Color.Gray;
            if (monitorData != null)
            {
                if (monitorData.IsConnectMC)
                {
                    if (monitorData.VoltageOfMonitorCardCollection != null
                        && monitorData.VoltageOfMonitorCardCollection.Count > 0
                        && monitorData.VoltageOfMonitorCardCollection[0].IsValid)
                    {
                        if (monitorData.VoltageOfMonitorCardCollection[0].Value > StatisticsMonitorInfo.MAXSUPPLYVOLTAGE
                            || monitorData.VoltageOfMonitorCardCollection[0].Value < StatisticsMonitorInfo.MINXSUPPLYVOLTAGE)
                        {
                            clr = Color.Yellow;
                        }
                        else
                        {
                            clr = Color.Green;
                        }
                    }
                    else
                    {
                        //电压无效时认为正常
                        clr = Color.Green;
                    }
                }
                else
                {
                    clr = Color.Red;
                }
            }
        }
        /// <summary>
        /// 获取排线故障和告警颜色信息
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetRowLineCorAndStr(ScannerMonitorData monitorData,
                                               ScanBoardRowLineStatus rowLineStatus,
                                               ref Color clr)
        {
            clr = Color.Gray;
            if (monitorData == null || !monitorData.IsConnectMC)
            {
                return;
            }

            if (rowLineStatus != null
                && rowLineStatus.SoketRowLineStatusList != null
                && rowLineStatus.SoketRowLineStatusList.Count > 0)
            {
                clr = Color.Green;
                SoketRowLineInfo soketInfo = null;
                for (int i = 0; i < rowLineStatus.SoketRowLineStatusList.Count; i++)
                {
                    soketInfo = rowLineStatus.SoketRowLineStatusList[i];
                    if (!soketInfo.IsABCDOk
                        || !soketInfo.IsCtrlOk
                        || !soketInfo.IsDCLKOk
                        || !soketInfo.IsLatchOk
                        || !soketInfo.IsOEOk)
                    {
                        clr = Color.Red;
                        return;
                    }
                    if (soketInfo.RGBStatusList != null
                        || soketInfo.RGBStatusList.Count > 0)
                    {
                        for (int j = 0; j < soketInfo.RGBStatusList.Count; j++)
                        {
                            if (!soketInfo.RGBStatusList[j].IsBlueOk
                                || !soketInfo.RGBStatusList[j].IsGreenOk
                                || !soketInfo.RGBStatusList[j].IsRedOk
                                || !soketInfo.RGBStatusList[j].IsVRedOk)
                            {
                                clr = Color.Red;
                                return;
                            }
                        }
                    }
                }
            }
        }
        public static void GetGeneralSwitchClr(ScannerMonitorData monitorData,
                                               List<bool> generalSwitchCloseList,
                                               ref Color clr)
        {
            clr = Color.Gray;
            if (monitorData == null || !monitorData.IsConnectMC)
            {
                return;
            }

            if (generalSwitchCloseList != null && generalSwitchCloseList.Count > 0)
            {
                clr = Color.Green;
                for (int i = 0; i < StatisticsMonitorInfo.CurCabintDoorCount; i++)
                {
                    if (!generalSwitchCloseList[i])
                    {
                        clr = Color.Yellow;
                        return;
                    }
                }
            }
        }
        #endregion

        #region 鼠标移动提示信息
        public static List<string> GetSBStatusNoticeStr(ScannerMonitorData monitorData)
        {
            List<string> valueList = new List<string>();
            string valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.SBStatus] + ":";
            if (monitorData == null)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                valueList.Add(valueStr);
                return valueList;
            }

            if (monitorData.WorkStatus == WorkStatusType.Error)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Error];
                valueList.Add(valueStr);
            }
            else if (monitorData.WorkStatus == WorkStatusType.OK)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.OK];
                valueList.Add(valueStr);

                valueStr = CommonStaticValue.SupplyVoltage + ": ";
                if (monitorData.VoltageOfScanCard.IsValid)
                {
                    valueStr += monitorData.VoltageOfScanCard.Value.ToString("f2");
                }
                else
                {
                    valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                }
                valueList.Add(valueStr);
            }
            else
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                valueList.Add(valueStr);
            }
            return valueList;
        }
        public static List<string> GetMCStatusNoticeStr(ScannerMonitorData monitorData)
        {
            List<string> valueList = new List<string>();
            string valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.MCStatus] + ":";
            if (monitorData == null)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                valueList.Add(valueStr);
                return valueList;
            }

            if (monitorData.IsConnectMC)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.OK];
                valueList.Add(valueStr);

                valueStr = CommonStaticValue.SupplyVoltage + ": ";
                if (monitorData.VoltageOfMonitorCardCollection == null
                    || monitorData.VoltageOfMonitorCardCollection.Count < 0)
                {
                    valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                }
                else
                {
                    if (monitorData.VoltageOfMonitorCardCollection[0].IsValid)
                    {
                        valueStr += monitorData.VoltageOfMonitorCardCollection[0].Value.ToString("f2");
                    }
                    else
                    {
                        valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                    }
                }
                valueList.Add(valueStr);
            }
            else
            {
                valueStr += CommonStaticValue.NotConnectMC;
                valueList.Add(valueStr);
            }
            return valueList;
        }
        public static string GetSmokeNoticeStr(ScannerMonitorData monitorData)
        {
            string valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Smoke] + ":";
            if (monitorData == null)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                return valueStr;
            }

            if (monitorData.IsConnectMC)
            {
                if (monitorData.SmokeWarn.IsValid)
                {
                    if (monitorData.SmokeWarn.IsSmokeAlarm)
                    {
                        valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Alarm];
                    }
                    else
                    {
                        valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.OK];
                    }
                }
                else
                {
                    valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                }
            }
            else
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown] + "(" + CommonStaticValue.NotConnectMC + ")";
            }
            return valueStr;
        }
        public static string GetTemperatureNoticeStr(ScannerMonitorData monitorData, 
                                                     TemperatureType tempDisplayType)
        {
            string valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Temperature] + ":";
            if (monitorData == null)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                return valueStr;
            }

            if (monitorData.TemperatureOfScanCard.IsValid)
            {
                //监控卡温度
                int nValue = (int)GetDisplayTempValueByCelsius(tempDisplayType,
                                                               monitorData.TemperatureOfScanCard.Value);
                valueStr += nValue.ToString();
            }
            else
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
            }
            return valueStr;
        }
        public static string GetHumidityNoticeStr(ScannerMonitorData monitorData)
        {
            string valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Humidity] + ":";
            if (monitorData == null)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                return valueStr;
            }

            if (monitorData.IsConnectMC)
            {
                if (monitorData.HumidityOfMonitorCard.IsValid)
                {
                    //监控卡湿度有效
                    valueStr += ((int)monitorData.HumidityOfMonitorCard.Value).ToString();
                }
                else if (monitorData.HumidityOfScanCard.IsValid)
                {
                    //监控卡湿度有效
                    valueStr += ((int)monitorData.HumidityOfScanCard.Value).ToString();
                }
                else
                {
                    valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                }
            }
            else
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown] + "(" + CommonStaticValue.NotConnectMC + ")";
            }
            return valueStr;
        }
        public static List<string> GetFanNoticeStr(ScannerMonitorData monitorData,
                                                   ScanBdMonitoredParamUpdateInfo mcFanInfo,
                                                   string sbCommAddr)
        {
            List<string> noticeStrList = new List<string>();
            string valueStr = "";
            if (monitorData == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Fan] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            uint fanCnt = GetMonitorFanCnt(mcFanInfo, sbCommAddr);
            if (fanCnt == 0)
            {
                return noticeStrList;
            }

            if (!monitorData.IsConnectMC)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Fan] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown]
                           + "(" + CommonStaticValue.NotConnectMC + ")";
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            #region 获取每个风扇的信息
            if (monitorData.FanSpeedOfMonitorCardCollection == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Fan] + ":"
                             + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }
            for (int i = 0; i < fanCnt; i++)
            {

                valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.Fan]
                           + "(" + CommonStaticValue.SwitchName + " " + (i + 1).ToString() + "): ";
                if (i >= monitorData.FanSpeedOfMonitorCardCollection.Count)
                {
                    valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                }
                else
                {
                    if (monitorData.FanSpeedOfMonitorCardCollection[i].IsValid)
                    {
                        valueStr += monitorData.FanSpeedOfMonitorCardCollection[i].Value;
                    }
                    else
                    {
                        valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                    }
                }
                noticeStrList.Add(valueStr);
            }
            #endregion

            return noticeStrList;
        }
        public static List<string> GetFanNoticeStr(ScannerMonitorData monitorData,
                                           ScanBdMonitoredParamUpdateInfo mcFanInfo,
                                           int fanIndex,
                                           string sbCommAddr)
        {
            List<string> noticeStrList = new List<string>();
            string valueStr = "";
            if (monitorData == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Fan] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            uint fanCnt = GetMonitorFanCnt(mcFanInfo, sbCommAddr);
            if (fanCnt == 0)
            {
                return noticeStrList;
            }

            if (!monitorData.IsConnectMC)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Fan] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown]
                           + "(" + CommonStaticValue.NotConnectMC + ")";
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            #region 获取每个风扇的信息
            if (monitorData.FanSpeedOfMonitorCardCollection == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Fan] + ":"
                             + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }
            for (int i = 0; i < fanCnt; i++)
            {
                if (i == fanIndex)
                {
                    valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.Fan]
                              + "(" + CommonStaticValue.SwitchName + " " + (i + 1).ToString() + "): ";
                    if (i >= monitorData.FanSpeedOfMonitorCardCollection.Count)
                    {
                        valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                    }
                    else
                    {
                        if (monitorData.FanSpeedOfMonitorCardCollection[i].IsValid)
                        {
                            valueStr += monitorData.FanSpeedOfMonitorCardCollection[i].Value;
                        }
                        else
                        {
                            valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                        }
                    }
                    noticeStrList.Add(valueStr);
                }
            }
            #endregion

            return noticeStrList;
        }
        public static List<string> GetPowerNoticeStr(ScannerMonitorData monitorData,
                                                     ScanBdMonitoredPowerInfo mcPowerInfo,
                                                     bool isUpdateScanBoadVolt,
                                                     string sbCommAddr)
        {
            List<string> noticeStrList = new List<string>();
            string valueStr = "";
            if (monitorData == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Power] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            uint powerCnt = GetMonitorPowerCnt(mcPowerInfo, sbCommAddr);
            if (powerCnt == 0)
            {
                return noticeStrList;
            }

            valueStr = "";
            if (!monitorData.IsConnectMC)
            {
                valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.MCOtherPower] + ":"
                            + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown]
                            + "(" + CommonStaticValue.NotConnectMC + ")";
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            #region 获取监控卡每路的电压
            if (monitorData.VoltageOfMonitorCardCollection == null)
            {
                valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.MCOtherPower] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);

                return noticeStrList;
            }

            int powerIndexInList = 0;
            for (int i = 0; i < powerCnt; i++)
            {
                powerIndexInList = i + 1;
                valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.MCOtherPower]
                           + "(" + CommonStaticValue.SwitchName + " " + powerIndexInList.ToString() + "): ";
                if (powerIndexInList >= monitorData.VoltageOfMonitorCardCollection.Count)
                {
                    valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                }
                else
                {
                    if (monitorData.VoltageOfMonitorCardCollection[powerIndexInList].IsValid)
                    {
                        valueStr += monitorData.VoltageOfMonitorCardCollection[powerIndexInList].Value.ToString("f2");
                    }
                    else
                    {
                        valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                    }
                }
                noticeStrList.Add(valueStr);
            }
            #endregion

            return noticeStrList;
        }
        public static List<string> GetPowerNoticeStr(ScannerMonitorData monitorData,
                                                     ScanBdMonitoredPowerInfo mcPowerInfo,
                                                     int powerIndex,
                                                     string sbCommAddr)
        {
            List<string> noticeStrList = new List<string>();
            string valueStr = "";
            if (monitorData == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.Power] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }
            uint powerCnt = GetMonitorPowerCnt(mcPowerInfo, sbCommAddr);
            if (powerCnt == 0)
            {
                return noticeStrList;
            }

            valueStr = "";
            if (!monitorData.IsConnectMC)
            {
                valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.MCOtherPower] + ":"
                            + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown]
                            + "(" + CommonStaticValue.NotConnectMC + ")";
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            #region 获取监控卡每路的电压
            if (monitorData.VoltageOfMonitorCardCollection == null)
            {
                valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.MCOtherPower] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);

                return noticeStrList;
            }
            int powerIndexInList = 0;
            for (int i = 0; i < powerCnt; i++)
            {
                powerIndexInList = i + 1;
                if (i == powerIndex)
                {
                    valueStr = CommonStaticValue.SwitchSignStr[(int)CommonStaticValue.SwitchSignType.MCOtherPower]
                               + "(" + CommonStaticValue.SwitchName + " " + powerIndexInList.ToString() + "): ";
                    if (powerIndexInList >= monitorData.VoltageOfMonitorCardCollection.Count)
                    {
                        valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                    }
                    else
                    {
                        if (monitorData.VoltageOfMonitorCardCollection[powerIndexInList].IsValid)
                        {
                            valueStr += monitorData.VoltageOfMonitorCardCollection[powerIndexInList].Value.ToString("f2");
                        }
                        else
                        {
                            valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                        }
                    }
                    noticeStrList.Add(valueStr);
                    break;
                }
            }
            #endregion
            return noticeStrList;
        }
        public static List<string> GetRowLineNoticeStr(ScannerMonitorData monitorData,
                                                       ScanBoardRowLineStatus rowLineStatus)
        {
            List<string> noticeStrList = new List<string>();
            string valueStr = "";
            if (monitorData == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.RowLine] + ":"
                           + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            if (!monitorData.IsConnectMC)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.RowLine] + ":"
                            + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown]
                            + "(" + CommonStaticValue.NotConnectMC + ")";
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }

            if (rowLineStatus == null)
            {
                valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.RowLine] + ":"
                            + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                noticeStrList.Add(valueStr);
                return noticeStrList;
            }
            bool bRGBHasFault = false;
            bool bAllHasFault = false;
            bool bIsOneGroupHasFault = false;
            bool bCtrlIsHasFault = false;
            List<string> groupAlarmListStr = null;
            List<string> soketAlarmListStr = null;
            if (rowLineStatus != null && rowLineStatus.SoketRowLineStatusList != null)
            {
                SoketRowLineInfo soketInfo = null;
                soketAlarmListStr = new List<string>();
                for (int i = 0; i < rowLineStatus.SoketRowLineStatusList.Count; i++)
                {
                    valueStr = "";
                    bRGBHasFault = false;
                    bCtrlIsHasFault = false;
                    groupAlarmListStr = new List<string>();
                    soketInfo = rowLineStatus.SoketRowLineStatusList[i];

                    #region 获取控制信号字符串
                    if (!soketInfo.IsABCDOk)
                    {
                        bCtrlIsHasFault = true;
                        valueStr = CommonStaticValue.ScanSignalName;
                    }
                    if (!soketInfo.IsCtrlOk)
                    {
                        if (bCtrlIsHasFault)
                        {
                            valueStr += ",";
                        }
                        valueStr += "Ctrl";
                        bCtrlIsHasFault = true;
                    }
                    if (!soketInfo.IsDCLKOk)
                    {
                        if (bCtrlIsHasFault)
                        {
                            valueStr += ",";
                        }
                        valueStr += "DCLK";
                        bCtrlIsHasFault = true;
                    }
                    if (!soketInfo.IsLatchOk)
                    {
                        if (bCtrlIsHasFault)
                        {
                            valueStr += ",";
                        }
                        bCtrlIsHasFault = true;
                        valueStr += "Latch";
                    }
                    if (!soketInfo.IsOEOk)
                    {
                        if (bCtrlIsHasFault)
                        {
                            valueStr += ",";
                        }
                        valueStr += "OE";
                        bCtrlIsHasFault = true;
                    }
                    #endregion

                    if (bCtrlIsHasFault)
                    {
                        bAllHasFault = true;
                        soketAlarmListStr.Add(CommonStaticValue.SoketName + (i + 1).ToString() + ":");
                        soketAlarmListStr.Add("  " + valueStr + CommonStaticValue.SignalName);
                    }

                    #region 获取RGB信号的字符串
                    if (soketInfo.RGBStatusList != null
                        || soketInfo.RGBStatusList.Count > 0)
                    {
                        groupAlarmListStr = new List<string>();
                        for (int j = 0; j < soketInfo.RGBStatusList.Count; j++)
                        {
                            valueStr = "";
                            bIsOneGroupHasFault = false;
                            if (!soketInfo.RGBStatusList[j].IsRedOk)
                            {
                                bIsOneGroupHasFault = true;
                                valueStr = "R";
                            }
                            if (!soketInfo.RGBStatusList[j].IsGreenOk)
                            {
                                if (bIsOneGroupHasFault)
                                {
                                    valueStr += ",";
                                }
                                valueStr += "G";
                                bIsOneGroupHasFault = true;
                            }
                            if (!soketInfo.RGBStatusList[j].IsBlueOk)
                            {
                                if (bIsOneGroupHasFault)
                                {
                                    valueStr += ",";
                                }
                                valueStr += "B";
                                bIsOneGroupHasFault = true;
                            }
                            if (!soketInfo.RGBStatusList[j].IsVRedOk)
                            {
                                if (bIsOneGroupHasFault)
                                {
                                    valueStr += ",";
                                }
                                valueStr += "VR";
                                bIsOneGroupHasFault = true;
                            }
                            if (bIsOneGroupHasFault)
                            {
                                bRGBHasFault = true;
                                groupAlarmListStr.Add("  " + CommonStaticValue.GroupName + (j + 1).ToString()
                                                      + ":" + valueStr + CommonStaticValue.SignalName);
                            }
                        }

                        if (bRGBHasFault)
                        {
                            bAllHasFault = true;
                            if (!bCtrlIsHasFault)
                            {
                                soketAlarmListStr.Add(CommonStaticValue.SoketName + (i + 1).ToString() + ":");
                            }
                            soketAlarmListStr.AddRange(groupAlarmListStr);
                        }
                    }
                    #endregion
                }
            }
            if (bAllHasFault)
            {
                noticeStrList.Add(CommonStaticValue.FaultInformation + ":");
                noticeStrList.AddRange(soketAlarmListStr);
            }
            else
            {
                noticeStrList.Add(CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.RowLine] + ":"
                                  + CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.OK]);
            }
            return noticeStrList;
        }
        public static string GetGeneralSwitchNoticeStr(ScannerMonitorData monitorData,
                                                       List<bool> generalSwitchCloseList)
        {
            string valueStr = CommonStaticValue.DisplayTypeStr[(int)MonitorDisplayType.GeneralSwitch] + ":";
            if (monitorData == null)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                return valueStr;
            }

            if (!monitorData.IsConnectMC)
            {
                valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown] + "(" + CommonStaticValue.NotConnectMC + ")";
            }
            else
            {
                if (generalSwitchCloseList != null && generalSwitchCloseList.Count > 0)
                {
                    bool isOpen = false;
                    for (int i = 0; i < StatisticsMonitorInfo.CurCabintDoorCount; i++)
                    {
                        if (!generalSwitchCloseList[i])
                        {
                            isOpen = true;
                            break;
                        }
                    }
                    if (isOpen)
                    {
                        valueStr += CommonStaticValue.CabinetDoorStatusStr[(int)CommonStaticValue.CabinetDoorStatusType.Open];
                    }
                    else
                    {
                        valueStr += CommonStaticValue.CabinetDoorStatusStr[(int)CommonStaticValue.CabinetDoorStatusType.Close];
                    }
                }
                else
                {
                    valueStr += CommonStaticValue.StatusNoticeStr[(int)CommonStaticValue.NoticeType.Unkown];
                }
            }
            return valueStr;
        }
        #endregion
    }

    public class StatisticsMonitorInfo
    {
        /// <summary>
        /// 需要检测的箱门个数
        /// </summary>
        public static byte CurCabintDoorCount = 1;
        public readonly static float MAXSUPPLYVOLTAGE = 5.5f;
        public readonly static float MINXSUPPLYVOLTAGE = 3.3f;



        /// <summary>
        /// 获取故障和告警信息
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetSBStatusFaultInfo(ScannerMonitorData monitorData, out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (monitorData != null)
            {
                if (monitorData.WorkStatus == WorkStatusType.OK)
                {
                    if (monitorData.VoltageOfScanCard.IsValid)
                    {
                        if (monitorData.VoltageOfScanCard.Value > MAXSUPPLYVOLTAGE
                            || monitorData.VoltageOfScanCard.Value < MINXSUPPLYVOLTAGE)
                        {
                            resType = MonitorInfoResult.Alarm;
                        }
                        else
                        {
                            resType = MonitorInfoResult.Ok;
                        }
                    }
                    else
                    {
                        //电压无效时认为正常
                        resType = MonitorInfoResult.Ok;
                    }
                }
                else
                {
                    resType = MonitorInfoResult.Fault;
                }
            }
        }
        /// <summary>
        /// 获取故障和告警信息
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetMCStatusFaultInfo(ScannerMonitorData monitorData, out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (monitorData != null)
            {
                if (monitorData.IsConnectMC)
                {
                    if (monitorData.VoltageOfMonitorCardCollection != null
                      && monitorData.VoltageOfMonitorCardCollection.Count > 0
                      && monitorData.VoltageOfMonitorCardCollection[0].IsValid)
                    {
                        if (monitorData.VoltageOfMonitorCardCollection[0].Value > MAXSUPPLYVOLTAGE
                            || monitorData.VoltageOfMonitorCardCollection[0].Value < MINXSUPPLYVOLTAGE)
                        {
                            resType = MonitorInfoResult.Alarm;
                        }
                        else
                        {
                            resType = MonitorInfoResult.Ok;
                        }
                    }
                    else
                    {
                        //电压无效时认为正常
                        resType = MonitorInfoResult.Ok;
                    }
                }
                else
                {
                    resType = MonitorInfoResult.Fault;
                }
            }
        }
        /// <summary>
        /// 获取故障和告警信息
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetSmokeAlarmInfo(ScannerMonitorData monitorData, out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (monitorData != null && monitorData.IsConnectMC)
            {
                if (monitorData.SmokeWarn.IsValid)
                {
                    if (monitorData.SmokeWarn.IsSmokeAlarm)
                    {
                        resType = MonitorInfoResult.Alarm;
                    }
                    else
                    {
                        resType = MonitorInfoResult.Ok;
                    }
                }
            }
        }
        /// <summary>
        /// 获取温度告警信息
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetTempAlarmInfo(ScannerMonitorData monitorData,
                                            float threshold,
                                            out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (monitorData != null)
            {
                if (monitorData.TemperatureOfScanCard.IsValid)
                {
                    int nValue = (int)monitorData.TemperatureOfScanCard.Value;
                    if (nValue > threshold)
                    {
                        //温度超过阈值时认为为告警信息
                        resType = MonitorInfoResult.Alarm;
                    }
                    else
                    {
                        resType = MonitorInfoResult.Ok;
                    }
                }
            }
        }
        /// <summary>
        /// 获取湿度告警信息
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetHumidityAlarmInfo(ScannerMonitorData monitorData,
                                                float threshold,
                                                out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (monitorData != null)
            {
                ValueInfo valueInf;
                if (monitorData.IsConnectMC
                    && monitorData.HumidityOfMonitorCard.IsValid)
                {
                    valueInf = monitorData.HumidityOfMonitorCard;
                }
                else if (monitorData.HumidityOfScanCard.IsValid)
                {
                    valueInf = monitorData.HumidityOfScanCard;
                }
                else
                {
                    return;
                }
                if ((int)valueInf.Value > threshold)
                {
                    //湿度超过阈值时认为为告警信息
                    resType = MonitorInfoResult.Alarm;
                }
                else
                {
                    resType = MonitorInfoResult.Ok;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="monitorData"></param>
        /// <param name="fanCnt"></param>
        /// <param name="alarmCnt"></param>
        /// <param name="hasAlarm"></param>
        public static void GetFanAlarmInfo(ScannerMonitorData monitorData, uint fanCount, int threshold,
                                           out bool hasValidFan, out uint alarmCnt)
        {
            alarmCnt = 0;
            hasValidFan = false;
            if (monitorData == null || !monitorData.IsConnectMC)
            {
                return;
            }
            if (fanCount == 0)
            {
                hasValidFan = true;
                return;
            }
            if (monitorData.FanSpeedOfMonitorCardCollection != null
                && monitorData.FanSpeedOfMonitorCardCollection.Count != 0)
            {
                MonitorInfoResult oneSwitchRes;
                ValueInfo valueInfo;
                for (int n = 0; n < fanCount; n++)
                {
                    if (n < monitorData.FanSpeedOfMonitorCardCollection.Count)
                    {
                        valueInfo = monitorData.FanSpeedOfMonitorCardCollection[n];
                    }
                    else
                    {
                        valueInfo = new ValueInfo();
                        valueInfo.IsValid = false;
                    }
                    GetOneFanOrPowerAlarmInfo(valueInfo, threshold, out oneSwitchRes);
                    if (oneSwitchRes != MonitorInfoResult.Unknown)
                    {
                        hasValidFan = true;
                        if (oneSwitchRes == MonitorInfoResult.Alarm)
                        {
                            alarmCnt++;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取排线告警数量
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetMCPowerAlarmInfo(ScannerMonitorData monitorData, uint powerCount, float alarmThreshold,float faultThreshold,
                                               out bool hasValidPower, out uint alarmCnt)
        {
            alarmCnt = 0;
            hasValidPower = false;
            if (monitorData == null || !monitorData.IsConnectMC)
            {
                return;
            }
            if (powerCount == 0)
            {
                hasValidPower = true;
                return;
            }
            if (monitorData.VoltageOfMonitorCardCollection != null
                 && monitorData.VoltageOfMonitorCardCollection.Count != 0)
            {
                MonitorInfoResult oneSwitchRes;
                ValueInfo valueInfo;
                int powerIndexInList = 0;
                for (int n = 0; n < powerCount; n++)
                {
                    powerIndexInList = n + 1;
                    if (powerIndexInList < monitorData.VoltageOfMonitorCardCollection.Count)
                    {
                        valueInfo = monitorData.VoltageOfMonitorCardCollection[powerIndexInList];
                    }
                    else
                    {
                        valueInfo = new ValueInfo();
                        valueInfo.IsValid = false;
                    }
                    GetOneFanOrPowerAlarmInfo(valueInfo, alarmThreshold,faultThreshold, out oneSwitchRes);
                    if (oneSwitchRes != MonitorInfoResult.Unknown)
                    {
                        hasValidPower = true;
                        if (oneSwitchRes == MonitorInfoResult.Alarm)
                        {
                            alarmCnt++;
                        }
                        else if (oneSwitchRes == MonitorInfoResult.Fault)
                        {
                            alarmCnt++;
                        }
                    }
                }
            }
        }
        public static void GetOneFanOrPowerAlarmInfo(ValueInfo valueInfo, float threshold,
                                                     out MonitorInfoResult resType)
        {
            //-50 代表不可能达到的故障阀值
            GetOneFanOrPowerAlarmInfo(valueInfo, threshold, -50, out resType);
        }
        public static void GetOneFanOrPowerAlarmInfo(ValueInfo valueInfo, float alarmThreshold, float faultThreshold,
                                                    out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (valueInfo.IsValid)
            {
                if (valueInfo.Value < faultThreshold)
                {
                    resType = MonitorInfoResult.Fault;
                }
                else if (valueInfo.Value < alarmThreshold)
                {
                    //风扇转速或电压值小于阈值时认为为告警信息
                    resType = MonitorInfoResult.Alarm;
                }
                else
                {
                    resType = MonitorInfoResult.Ok;
                }
            }
        }
        /// <summary>
        /// 获取排线告警数量
        /// </summary>
        /// <param name="scanBordAddr"></param>
        /// <param name="clr"></param>
        public static void GetRowLineFaultInfo(ScannerMonitorData monitorData,
                                               ScanBoardRowLineStatus rowLineStatus,
                                               out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (monitorData == null || !monitorData.IsConnectMC)
            {
                return;
            }
            if (rowLineStatus != null && rowLineStatus.SoketRowLineStatusList != null)
            {
                SoketRowLineInfo soketInfo = null;
                resType = MonitorInfoResult.Ok;
                for (int i = 0; i < rowLineStatus.SoketRowLineStatusList.Count; i++)
                {
                    soketInfo = rowLineStatus.SoketRowLineStatusList[i];
                    if (!soketInfo.IsABCDOk
                        || !soketInfo.IsCtrlOk
                        || !soketInfo.IsDCLKOk
                        || !soketInfo.IsLatchOk
                        || !soketInfo.IsOEOk)
                    {
                        resType = MonitorInfoResult.Fault;
                        return;
                    }
                    if (soketInfo.RGBStatusList != null
                        || soketInfo.RGBStatusList.Count > 0)
                    {
                        for (int j = 0; j < soketInfo.RGBStatusList.Count; j++)
                        {
                            if (!soketInfo.RGBStatusList[j].IsBlueOk
                                || !soketInfo.RGBStatusList[j].IsGreenOk
                                || !soketInfo.RGBStatusList[j].IsRedOk
                                || !soketInfo.RGBStatusList[j].IsVRedOk)
                            {
                                resType = MonitorInfoResult.Fault;
                                return;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取箱门状态告警的信息
        /// </summary>
        /// <param name="generalSwitchCloseList"></param>
        /// <param name="alarmCnt"></param>
        /// <returns></returns>
        public static void GetGeneralSwitcAlarmInfo(ScannerMonitorData monitorData,
                                                    List<bool> generalSwitchCloseList,
                                                    out MonitorInfoResult resType)
        {
            resType = MonitorInfoResult.Unknown;
            if (monitorData == null || !monitorData.IsConnectMC)
            {
                return;
            }
            if (generalSwitchCloseList != null && generalSwitchCloseList.Count > 0)
            {
                resType = MonitorInfoResult.Ok;
                for (int i = 0; i < CurCabintDoorCount; i++)
                {
                    if (!generalSwitchCloseList[i])
                    {
                        resType = MonitorInfoResult.Alarm;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取温度对应的信息
        /// </summary>
        /// <param name="monitorData"></param>
        /// <param name="curIndex"></param>
        /// <param name="backClr"></param>
        /// <param name="displayStr"></param>
        public static void CaculateTempStatisticsInfo(ScannerMonitorData monitorData,
                                                      float threshold,
                                                      out ValueCompareResult valueCompareRes,
                                                      out MonitorInfoResult resType,
                                                      ref TempAndHumiStatisticsInfo statisticsInfo)
        {
            valueCompareRes = ValueCompareResult.Unknown;
            resType = MonitorInfoResult.Unknown;
            if (monitorData != null)
            {
                if (monitorData.TemperatureOfScanCard.IsValid)
                {
                    float fValue = monitorData.TemperatureOfScanCard.Value;
                    int nValue = (int)fValue;
                    if (nValue > threshold)
                    {
                        resType = MonitorInfoResult.Alarm;
                    }
                    else
                    {
                        resType = MonitorInfoResult.Ok;
                    }
                    if (statisticsInfo.ValidScanBoardCnt <= 0)
                    {
                        //当前扫描卡索引为0时，获取当前温度
                        statisticsInfo.MinValue = nValue;
                        statisticsInfo.MaxValue = nValue;

                        valueCompareRes = ValueCompareResult.IsFirstValidValue;
                    }
                    else
                    {
                        //获取当前最小温度和最大温度
                        if (statisticsInfo.MinValue > nValue)
                        {
                            valueCompareRes = ValueCompareResult.BelowMinValue;
                            statisticsInfo.MinValue = nValue;
                        }
                        else if (statisticsInfo.MinValue == nValue)
                        {
                            valueCompareRes = ValueCompareResult.EqualsMinValue;
                        }

                        if (statisticsInfo.MaxValue < nValue)
                        {
                            valueCompareRes = ValueCompareResult.AboveMaxValue;
                            statisticsInfo.MaxValue = nValue;
                        }
                        else if (statisticsInfo.MaxValue == nValue)
                        {
                            if (valueCompareRes == ValueCompareResult.EqualsMinValue)
                            {
                                valueCompareRes = ValueCompareResult.EqualsBothValue;
                            }
                            else
                            {
                                valueCompareRes = ValueCompareResult.EqualsMaxValue;
                            }
                        }
                    }
                    statisticsInfo.ValidScanBoardCnt++;
                    statisticsInfo.TotalValue += nValue;
                    statisticsInfo.AllValueList.Add(nValue);
                }
            }
        }
        /// <summary>
        /// 获取湿度对应的信息
        /// </summary>
        /// <param name="monitorData"></param>
        /// <param name="curIndex"></param>
        /// <param name="backClr"></param>
        /// <param name="displayStr"></param>
        public static void CaculateHumiStatisticsInfo(ScannerMonitorData monitorData,
                                                      float threshold,
                                                      out ValueCompareResult valueCompareRes,
                                                      out MonitorInfoResult resType,
                                                      ref TempAndHumiStatisticsInfo statisticsInfo)
        {
            valueCompareRes = ValueCompareResult.Unknown;
            resType = MonitorInfoResult.Unknown;
            if (monitorData != null)
            {
                float fValue = 0;
                if (!monitorData.IsConnectMC || !monitorData.HumidityOfMonitorCard.IsValid)
                {
                    if (!monitorData.HumidityOfScanCard.IsValid)
                    {
                        return;
                    }
                    else
                    {
                        statisticsInfo.ValidScanBoardCnt++;
                        fValue = monitorData.HumidityOfScanCard.Value;
                    }
                }
                else
                {
                    statisticsInfo.ValidScanBoardCnt++;
                    fValue = monitorData.HumidityOfMonitorCard.Value;
                }
                int nValue = (int)fValue;
                if (nValue > threshold)
                {
                    resType = MonitorInfoResult.Alarm;
                }
                else
                {
                    resType = MonitorInfoResult.Ok;
                }
                if (statisticsInfo.ValidScanBoardCnt <= 1)
                {
                    //当前扫描卡索引为0时，获取当前湿度
                    statisticsInfo.MinValue = nValue;
                    statisticsInfo.MaxValue = nValue;

                    valueCompareRes = ValueCompareResult.IsFirstValidValue;
                }
                else
                {
                    //获取当前最小湿度和最大湿度
                    if (statisticsInfo.MinValue > nValue)
                    {
                        valueCompareRes = ValueCompareResult.BelowMinValue;
                        statisticsInfo.MinValue = nValue;
                    }
                    else if (statisticsInfo.MinValue == nValue)
                    {
                        valueCompareRes = ValueCompareResult.EqualsMinValue;
                    }

                    if (statisticsInfo.MaxValue < nValue)
                    {
                        valueCompareRes = ValueCompareResult.AboveMaxValue;
                        statisticsInfo.MaxValue = nValue;
                    }
                    else if (statisticsInfo.MaxValue == nValue)
                    {
                        if (valueCompareRes == ValueCompareResult.EqualsMinValue)
                        {
                            valueCompareRes = ValueCompareResult.EqualsBothValue;
                        }
                        else
                        {
                            valueCompareRes = ValueCompareResult.EqualsMaxValue;
                        }
                    }
                }
                statisticsInfo.TotalValue += nValue;
                statisticsInfo.AllValueList.Add(nValue);
            }
        }
        /// <summary>
        /// 获取平均值
        /// </summary>
        /// <param name="averageTemp"></param>
        /// <returns></returns>
        public static void GetAverageValueAndBeyondCnt(ref TempAndHumiStatisticsInfo statisticsInfo)
        {
            statisticsInfo.MaxValue = (int)statisticsInfo.MaxValue;
            statisticsInfo.MinValue = (int)statisticsInfo.MinValue;
            if (statisticsInfo.ValidScanBoardCnt > 0)
            {
                statisticsInfo.AverageValue = (int)(statisticsInfo.TotalValue / statisticsInfo.ValidScanBoardCnt);
            }
            for (int i = 0; i < statisticsInfo.AllValueList.Count; i++)
            {
                if (statisticsInfo.AllValueList[i] > statisticsInfo.AverageValue)
                {
                    statisticsInfo.BeyondAverageCnt++;
                }
            }
        }
    }

    public class HightLightScanPaintInfo
    {
        public static Color BoardColor = Color.Red;
        public static int BoardWidth = 3;
    }
}
