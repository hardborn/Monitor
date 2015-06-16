using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Monitoring.ColudSupport;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Monitoring.HardwareMonitorInterface
{
    public delegate void CompletedMonitorCallback(CompletedMonitorCallbackParams cmpParams, object userToken);
    public delegate void TransFerParamsDataHandler(TransferParams param, object userToken);
    public delegate void DetectPointCompletedEventHandler(SpotInspectionResult res,object userToken);
    public class CompletedMonitorCallbackParams
    {
        public AllMonitorData MonitorData
        {
            get
            {
                return _monitorData;
            }
            set
            {
                _monitorData = value;
            }
        }
        private AllMonitorData _monitorData = null;
    }
    //public delegate void ScreenCfgChangedEventHandler(object sender, )

    //public class ScreenCfgChangedEventArgs : EventArgs
    //{
    //    public Dictionary<string, ILEDDisplayInfo
    //}
    public enum UpdateCfgFileResType
    {
        OK,
        FileNoExist,
        FileError,
        Unknown
    }

    public enum HWSettingType
    {
        GlobalBright,
        OpenDevice,
        CloseDevice
    }

    public delegate void NotifyUpdateCfgFileResEventHandler(object sender, UpdateCfgFileResEventArgs e);
    public class UpdateCfgFileResEventArgs : EventArgs
    {
        public TransferParams UpdateParams
        {
            get;
            set;
        }

        public UpdateCfgFileResType Result
        {
            get;
            set;
        }
    }

    public delegate void NotifySettingCompletedEventHandler(object sender, NotifySettingCompletedEventArgs e);
    public class NotifySettingCompletedEventArgs : EventArgs
    {
        public HWSettingType SettingType
        {
            get;
            set;
        }
        public bool Result
        {
            get;
            set;
        }
    }

    public class ScreenInfo
    {
        private string _ledSN;
        public string LedSN
        {
            get { return _ledSN; }
            set { _ledSN = value; }
        }

        private int _ledWidth;
        public int LedWidth
        {
            get { return _ledWidth; }
            set
            {
                _ledWidth = value;
            }
        }

        private int _ledHeight;
        public int LedHeight
        {
            get { return _ledHeight; }
            set { _ledHeight = value; }
        }

        private bool _bSupportPointDetect;
        public bool IsSupportPointDetect
        {
            get { return _bSupportPointDetect; }
            set { _bSupportPointDetect = value; }
        }

        private int _pointCount;
        public int PointCount
        {
            get { return _pointCount; }
            set { _pointCount = value; }
        }

        private int _ledIndexOfCom;
        public int LedIndexOfCom
        {
            get { return _ledIndexOfCom; }
            set { _ledIndexOfCom = value; }
        }

        private string _commport;
        public string Commport
        {
            get { return _commport; }
            set { _commport = value; }
        }
        private int _senderCardCount;
        public int SenderCardCount
        {
            get { return _senderCardCount; }
            set { _senderCardCount = value; }
        }

        private int _scannerCardCount;
        public int ScannerCardCount
        {
            get { return _scannerCardCount; }
            set { _scannerCardCount = value; }
        }

        private int _monitorCardCount;
        public int MonitorCardCount
        {
            get { return _monitorCardCount; }
            set { _monitorCardCount = value; }
        }

        private int _deconcentratorCount;
        public int DeconcentratorCount
        {
            get { return _deconcentratorCount; }
            set { _deconcentratorCount = value; }
        }

        private int _functionCardCount;
        public int FunctionCardCount
        {
            get { return _functionCardCount; }
            set { _functionCardCount = value; }
        }
        private object _ledInfo;
        public object LedInfo
        {
            get { return _ledInfo; }
            set { _ledInfo = value; }
        }
    }

}
