using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    [Serializable]
    public class MonitorSysData
    {
        public bool IsConfigInfoSame = true;
        public bool IsCycleMonitor = false;
        public int RetryReadTimes = 1;
        public int MonitorPeriod = 60 * 1000;

        public OneDisplayMonitorSysData SameMonitorSysData = null;
        public List<OneDisplayMonitorSysData> AllDisplayMonitorSysDataList = null;

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is MonitorSysData))
            {
                return false;
            }
            MonitorSysData temp = (MonitorSysData)obj;

            temp.IsConfigInfoSame = this.IsConfigInfoSame;
            temp.IsCycleMonitor = this.IsCycleMonitor;
            temp.RetryReadTimes = this.RetryReadTimes;
            temp.MonitorPeriod = this.MonitorPeriod;
            temp.SameMonitorSysData = (OneDisplayMonitorSysData)this.SameMonitorSysData.Clone();
            if (this.AllDisplayMonitorSysDataList == null)
            {
                temp.AllDisplayMonitorSysDataList = null;
            }
            else
            {
                temp.AllDisplayMonitorSysDataList = new List<OneDisplayMonitorSysData>();
                for (int i = 0; i < this.AllDisplayMonitorSysDataList.Count; i++)
                {
                    temp.AllDisplayMonitorSysDataList.Add((OneDisplayMonitorSysData)this.AllDisplayMonitorSysDataList[i].Clone());
                }
            }
            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            MonitorSysData newObj = new MonitorSysData();
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

        public MonitorSysData()
        {
            SameMonitorSysData = new OneDisplayMonitorSysData();
            AllDisplayMonitorSysDataList = new List<OneDisplayMonitorSysData>();
        }
    }
}