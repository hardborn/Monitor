using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    [Serializable]
    public enum ScanBdMonitoredParamCountType
    {
        SameForEachScanBd = 0,
        DifferentForEachScanBd
    }

    #region 控制
    #region New控制
    /// <summary>
    /// 控制类型
    /// </summary>
    public enum CtrlReasonType
    {
        /// <summary>
        /// 温度控制
        /// </summary>
        TemperatureCtrl = 0,
        /// <summary>
        /// 烟雾控制
        /// </summary>
        SmokeAlarmCtrl,
        /// <summary>
        /// 智能亮度控制
        /// </summary>
        IntelBrightCtrl
    }
    public enum CtrlOperateType
    {
        /// <summary>
        /// 温度引起亮度变化
        /// </summary>
        TemperatureBright = 0,
        /// <summary>
        /// 设备关闭
        /// </summary>
        PowerOff,
        /// <summary>
        /// 智能亮度
        /// </summary>
        IntelBright,
        /// <summary>
        /// 设备开启
        /// </summary>
        PowerOn
    }

    #endregion
    #region Old控制
    /// <summary>
    /// 控制类型
    /// </summary>
    public enum ControlReasonType
    {
        /// <summary>
        /// 温度升高控制
        /// </summary>
        TemperatureHigh = 0,
        /// <summary>
        /// 烟雾控制
        /// </summary>
        SmokeAlarm,
        /// <summary>
        /// 温度降低控制
        /// </summary>
        TemperatureDecrease
    }
    /// <summary>
    /// 策略类型
    /// </summary>
    public enum ControlOperateType
    {
        DecreaseBright = 0,
        PowerOff,
        RestoreBright,
        PowerOn
    }
    public enum DectCtrlInfo
    {
        Successful,
        CtrlInfoOverLaping,
        CtrlInfoOver
    }
    #endregion
    /// <summary>
    /// 温度类型
    /// </summary>
    public enum DescreaseTempType
    {
        /// <summary>
        /// 最高温度
        /// </summary>
        HighTemperature,
        /// <summary>
        /// 平均温度
        /// </summary>
        AverageTemperature
    }

    #endregion

    #region Ctrl
    public enum SendCtrlInfoResult
    {
        Succeed = 0,
        Failed,
        Unknown
    }
    #endregion
}