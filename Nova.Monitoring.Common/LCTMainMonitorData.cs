using Nova.LCT.GigabitSystem.Common;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    [Serializable]
    public class LCTMainMonitorData
    {
        public LCTMainMonitorData()
        {
            FaultMonitorInfos = new SerializableDictionary<string, MonitorErrData>();
            AlarmMonitorInfos = new SerializableDictionary<string, MonitorErrData>();
            RefreshTypeInfo = new MonitorAllTypeInfo();
            ValidInfo = new MonitorIsValidInfo();
            RedundantStateInfoDic = new SerializableDictionary<string, SerializableDictionary<int, RedundantStateInfo>>();
        }
        public SerializableDictionary<string, MonitorErrData> FaultMonitorInfos { get; set; }
        public SerializableDictionary<string, MonitorErrData> AlarmMonitorInfos { get; set; }
        public MonitorAllTypeInfo RefreshTypeInfo { get; set; }
        public MonitorIsValidInfo ValidInfo { get; set; }
        public SerializableDictionary<string, SerializableDictionary<int, RedundantStateInfo>> RedundantStateInfoDic { get; set; }
    }

    [Serializable]
    public class MonitorAllTypeInfo
    {
        public MonitorAllTypeInfo()
        {
            IsUpdateSenderDVI = true;
            IsUpdateSBStatus = true;
            IsUpdateTemperature = true;
            IsUpdateMCStatus = false;
            IsUpdateHumidity = false;
            IsUpdateSmoke = false;
            IsUpdateFan = false;
            IsUpdatePower = false;
            IsUpdateRowLine = false;
            IsUpdateGeneralStatus = false;
        }
        public bool IsUpdateSBStatus { get; set; }
        public bool IsUpdateMCStatus { get; set; }
        public bool IsUpdateTemperature { get; set; }
        public bool IsUpdateHumidity { get; set; }
        public bool IsUpdateSmoke { get; set; }
        public bool IsUpdateFan { get; set; }
        public bool IsUpdatePower { get; set; }
        public bool IsUpdateRowLine { get; set; }
        public bool IsUpdateGeneralStatus { get; set; }
        public bool IsUpdateSenderDVI { get; set; }
    }
    public class MonitorIsValidInfo
    {
        public MonitorIsValidInfo()
        {
            IsSBStatusValid = false;
            IsMCStatusValid = false;
            IsTemperatureValid = false;
            IsHumidityValid = false;
            IsSmokeValid = false;
            IsFanValid = false;
            IsPowerValid = false;
            IsRowLineValid = false;
            IsGeneralStatusValid = false;
            IsSenderDVIValid = false;
        }
        public bool IsSBStatusValid { get; set; }
        public bool IsMCStatusValid { get; set; }
        public bool IsTemperatureValid { get; set; }
        public bool IsHumidityValid { get; set; }
        public bool IsSmokeValid { get; set; }
        public bool IsFanValid { get; set; }
        public bool IsPowerValid { get; set; }
        public bool IsRowLineValid { get; set; }
        public bool IsGeneralStatusValid { get; set; }
        public bool IsSenderDVIValid { get; set; }
    }
}
