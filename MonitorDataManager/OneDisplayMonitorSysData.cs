using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    /// <summary>
    /// 一个物理屏对应的监控配置信息
    /// </summary>
    [Serializable]
    public class OneDisplayMonitorSysData
    {
        public TemperatureType TempDisplayType = TemperatureType.Celsius;
        public bool IsUpdateSBStatus = true;
        public bool IsDisplaySBValtage = true;
        public bool IsUpdateTemperature = true;
        public bool IsUpdateMCStatus = false;
        public bool IsUpdateHumidity = false;
        public bool IsUpdateSmoke = false;
        public bool IsUpdateFan = false;
        public bool IsUpdateRowLine = false;
        public bool IsUpdateMCVoltage = false;
        public bool IsUpdateGeneralStatus = false;
        public float TemperatureThreshold = 60;
        public float HumidityThreshold = 60;

        public ScanBdMonitoredParamUpdateInfo MCFanInfo = null;
        public ScanBdMonitoredPowerInfo MCPowerInfo = null;

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is OneDisplayMonitorSysData))
            {
                return false;
            }
            OneDisplayMonitorSysData temp = (OneDisplayMonitorSysData)obj;

            temp.TempDisplayType = this.TempDisplayType;
            temp.IsUpdateSBStatus = this.IsUpdateSBStatus;
            temp.IsUpdateMCStatus = this.IsUpdateMCStatus;
            temp.IsUpdateTemperature = this.IsUpdateTemperature;
            temp.IsUpdateHumidity = this.IsUpdateHumidity;
            temp.IsDisplaySBValtage = this.IsDisplaySBValtage;
            temp.IsUpdateSmoke = this.IsUpdateSmoke;
            temp.IsUpdateFan = this.IsUpdateFan;
            temp.IsUpdateRowLine = this.IsUpdateRowLine;
            temp.IsUpdateMCVoltage = this.IsUpdateMCVoltage;
            temp.IsUpdateGeneralStatus = this.IsUpdateGeneralStatus;
            temp.TemperatureThreshold = this.TemperatureThreshold;
            temp.HumidityThreshold = this.HumidityThreshold;
            temp.MCFanInfo = (ScanBdMonitoredParamUpdateInfo)this.MCFanInfo.Clone();
            temp.MCPowerInfo = (ScanBdMonitoredPowerInfo)this.MCPowerInfo.Clone();
            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            OneDisplayMonitorSysData newObj = new OneDisplayMonitorSysData();
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

        public OneDisplayMonitorSysData()
        {
            MCFanInfo = new ScanBdMonitoredParamUpdateInfo();
            MCFanInfo.AlarmThreshold = 1000;
            MCFanInfo.CountDicOfScanBd = null;
            MCFanInfo.CountType = ScanBdMonitoredParamCountType.SameForEachScanBd;
            MCFanInfo.SameCount = 4;

            MCPowerInfo = new ScanBdMonitoredPowerInfo();
            MCPowerInfo.AlarmThreshold = 4;
            MCPowerInfo.CountDicOfScanBd = null;
            MCPowerInfo.CountType = ScanBdMonitoredParamCountType.SameForEachScanBd;
            MCPowerInfo.SameCount = 3;
        }
    }
}
