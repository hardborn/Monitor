using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.ColudSupport
{
    public enum TransferType
    {
        M3_HWMonitor,
        M3_FunctionCardMonitor,
        UpdateLedMonitoringConfigInfo,
        M3_UpdateLedScreenConfigInfo,
        M3_PeripheralsInfo,
        M3_OpenDevice,
        M3_CloseDevice,
        M3_RefreshDataFinish,
        M3_ReadSmartLightHWConfig,
        M3_WriteSmartLightHWConfig,
        M3_ExecStaus,
        M3_EnableSmartBrightness,
        M3_COMFindSN,
        M3_BlackScreen,
        M3_NormalScreen,
        M3_ReadBrightness,
        M3_ExecBrightResultLog,
        M3_MonitorStopNotify,
        M3_MonitorRenumeNotify,
        M3_BrightConfigSaveResult,
        M3_FirstInitialize
    }

    public class TransferParams
    {
        public TransferType TranType
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }
    }
}
