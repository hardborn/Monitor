using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
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
}
