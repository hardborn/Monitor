using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nova.Algorithms.CheckCodes;
using Nova.Monitoring.HardwareMonitorInterface;

namespace Nova.Monitoring.MarsMonitorDataReader
{
    public class MonitorFuncHelper
    {
        public static bool IsMD5Equal(string firstFileName, string secondFileName)
        {
            string firstMD5 = "";
            MD5Helper.FileMD5ErrorMode errorMode = MD5Helper.CreateMD5(firstFileName, out firstMD5);
            if (errorMode != MD5Helper.FileMD5ErrorMode.OK)
            {
                return false;
            }
            string secondMD5 = "";
            errorMode = MD5Helper.CreateMD5(secondFileName, out secondMD5);
            if (errorMode != MD5Helper.FileMD5ErrorMode.OK)
            {
                return false;
            }

            if (firstMD5 != secondMD5)
            {
                return false;
            }

            return true;
        }
    }

    public class MarsHWConfig:HWConfigBase
    {
        public MarsHWConfig()
        {
            IsUpdateMCStatus = true;
            IsUpdateHumidity = false;
            IsUpdateSmoke = false;
            IsUpdateFan = false;
            IsUpdateMCVoltage = false;
            IsUpdateGeneralStatus = false;
            IsUpdateRowLine = false;
            FanInfoObj = new FanInfos();
            PowerInfoObj = new PowerInfos();
        }

        public bool IsUpdateMCStatus { get; set; }

        public bool IsUpdateHumidity { get; set; }

        public bool IsUpdateSmoke { get; set; }

        public bool IsUpdateFan { get; set; }
        public FanInfos FanInfoObj { get; set; }
        public bool IsUpdateMCVoltage { get; set; }
        public PowerInfos PowerInfoObj { get; set; }

        public bool IsUpdateGeneralStatus { get; set; }

        public bool IsUpdateRowLine { get; set; }
    }

    public class FanInfos
    {
        public FanInfos()
        {
            FanPulseCount = 1;
            FanCount = 4;
            isSame = true;
            FanList = new List<OneFanInfo>();
        }

        public int FanPulseCount { get; set; }
        public int FanCount { get; set; }
        public bool isSame { get; set; }
        public List<OneFanInfo> FanList {get;set;}
    }
    public class OneFanInfo
    {
        public OneFanInfo()
        {
            FanCount = 4;
            SenderIndex = 0;
            PortIndex = 0;
            ScanIndex = 0;
        }
        public int FanCount { get; set; }
        public string Sn { get; set; }
        public int SenderIndex { get; set; }
        public int PortIndex { get; set; }
        public int ScanIndex { get; set; }
    }

    public class PowerInfos
    {
        public PowerInfos()
        {
            PowerCount = 3;
            isSame = true;
            PowerList = new List<OnePowerInfo>();
        }
        public int PowerCount { get; set; }
        public bool isSame { get; set; }
        public List<OnePowerInfo> PowerList { get; set; }
    }
    public class OnePowerInfo
    {
        public OnePowerInfo()
        {
            PowerCount = 3;
            SenderIndex = 0;
            PortIndex = 0;
            ScanIndex = 0;
        }
        public int PowerCount { get; set; }
        public string Sn { get; set; }
        public int SenderIndex { get; set; }
        public int PortIndex { get; set; }
        public int ScanIndex { get; set; }
    }
}
