using Nova.Monitoring.MonitorDataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class MonitorDataConfig : IDisposable
    {
        private static MonitorDataConfig _instance = null;
        private static object objLock = new object();
        public static MonitorDataConfig Instance()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MonitorDataConfig();
                    }
                }
            }
            return _instance;
        }
        private MonitorDataConfig() { }

        public MonitorConfigData MonitorConfig { get; private set; }

        public void LoadMonitorConfig()
        {
            //1.判断是否有原始配置
            //{
                 //读取原始配置给以上对象
                 //保存一次
            //}
            //else
            //{
            //    读库；
            //}
            //判断是否为空
            //{
            //    空的对象new一次。
            //    也需要保存一次
            //}
            MonitorConfig = new MonitorConfigData();
        }

        public void SaveMonitorUIDisplayConfig(MonitorUIDisplayConfig monitorUiConfig)
        {
            MonitorConfig.MonitorUIConfig = (MonitorUIDisplayConfig)monitorUiConfig.Clone();
            //写库
        }

        public void SaveMonitorCycleData(MonitorCycleData monitorCycle)
        {
            MonitorConfig.MonitorCycle = monitorCycle;
            //写库
        }
        public void SaveDisplayMonitorData(string screenSN, OneDisplayMonitorData oneDisplayData)
        {
            if (IsAllScreen(screenSN))
            {
                foreach (string key in MonitorConfig.AllDisplayMonitorSysDataDic.Keys)
                {
                    MonitorConfig.AllDisplayMonitorSysDataDic[key] = (OneDisplayMonitorData)oneDisplayData.Clone();
                    //写入库
                }
                if (MonitorConfig.MonitorUIConfig.IsDisplayMonitorInfoSame == false)
                {
                    MonitorConfig.MonitorUIConfig.IsDisplayMonitorInfoSame = true;
                    SaveMonitorUIDisplayConfig(MonitorConfig.MonitorUIConfig);
                }
            }
            else
            {
                if (MonitorConfig.MonitorUIConfig.IsDisplayMonitorInfoSame == true)
                {
                    MonitorConfig.MonitorUIConfig.IsDisplayMonitorInfoSame = false;
                    SaveMonitorUIDisplayConfig(MonitorConfig.MonitorUIConfig);
                }

                MonitorConfig.AllDisplayMonitorSysDataDic[screenSN] = (OneDisplayMonitorData)oneDisplayData.Clone();
                //写入库
            }
        }

        public void SaveDataThreshold(string screenSN, DataThresholdInfo dataThreshold)
        {
            if (IsAllScreen(screenSN))
            {
                foreach (string key in MonitorConfig.AllDataThresholdDic.Keys)
                {
                    MonitorConfig.AllDataThresholdDic[key] = (DataThresholdInfo)dataThreshold.Clone();
                    //写入库
                }
                if (MonitorConfig.MonitorUIConfig.IsDataThresholdSame == false)
                {
                    MonitorConfig.MonitorUIConfig.IsDataThresholdSame = true;
                    SaveMonitorUIDisplayConfig(MonitorConfig.MonitorUIConfig);
                }
            }
            else
            {
                if (MonitorConfig.MonitorUIConfig.IsDataThresholdSame == true)
                {
                    MonitorConfig.MonitorUIConfig.IsDataThresholdSame = false;
                    SaveMonitorUIDisplayConfig(MonitorConfig.MonitorUIConfig);
                }

                MonitorConfig.AllDataThresholdDic[screenSN] = (DataThresholdInfo)dataThreshold.Clone();
                //写入库
            }
        }

        public void SaveCtrlFuncCardPower(List<CtrlFuncCardPowerInfo> controlFCInfos)
        {
            MonitorConfig.ControlFCInfos.Clear();
            foreach (CtrlFuncCardPowerInfo ctrlInfo in controlFCInfos)
            {
                MonitorConfig.ControlFCInfos.Add((CtrlFuncCardPowerInfo)ctrlInfo.Clone());
            }

            //入库：MonitorConfig.ControlFCInfos  ControlAliaNamesDic
        }

        //public void Save光探头()
        //{

        //}

        private bool IsAllScreen(string screenSN)
        {
            if (screenSN == "ALLScreen")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (MonitorConfig != null)
            {
                MonitorConfig = null;
            }

            if (_instance != null)
            {
                _instance = null;
            }
        }
    }
}
