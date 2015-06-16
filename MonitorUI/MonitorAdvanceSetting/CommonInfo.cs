using System;
using System.Collections.Generic;
using System.Text;
using Nova.LCT.GigabitSystem.Common;
using System.Drawing;

namespace Nova.Monitoring.UI.MonitorSetting
{
    public delegate void SettingMonitorCntEventHandler(string addr, byte count);
    /// <summary>
    /// 每个接收卡监控信息及自身信息
    /// </summary>
    internal class SetCustomObjInfo
    {
        public byte Count = 0;
        public ScanBoardRegionInfo ScanBordInfo = null;
    }
    public class SettingCommInfo
    {
        public Image IconImage;
        public byte SameCount;
        public string TypeStr;
        public byte MaxCount;

        #region ICopy 成员
        public bool CopyTo(object obj)
        {
            if (!(obj is SettingCommInfo))
            {
                return false;
            }
            SettingCommInfo temp = (SettingCommInfo)obj;

            temp.IconImage = this.IconImage;
            temp.SameCount = this.SameCount;
            temp.TypeStr = this.TypeStr;
            temp.MaxCount = this.MaxCount;
            return true;
        }
        #endregion

        #region ICloneable 成员
        public object Clone()
        {
            SettingCommInfo newObj = new SettingCommInfo();
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
    internal class StaticFunction
    {
        public static char AddrSeperate = '-';
        public static string GetSBAddr(string commPort, byte senderAddr, byte portAddr, UInt16 sbAddr)
        {
            return commPort + AddrSeperate + senderAddr.ToString() + AddrSeperate 
                    + portAddr.ToString() + AddrSeperate + sbAddr.ToString();
        }
        public static bool GetPerAddr(string addr, out string commPort, out byte senderAddr, out byte portAddr, out UInt16 sbAddr)
        {
            string[] perAddr = addr.Split(AddrSeperate);
            commPort = "";
            senderAddr = 0;
            portAddr = 0;
            sbAddr = 0;
            if (perAddr.Length < 4)
            {
                return false;
            }
            commPort = perAddr[0];
            if (!Byte.TryParse(perAddr[1], out senderAddr))
            {
                return false;
            }
            if (!Byte.TryParse(perAddr[2], out portAddr))
            {
                return false;
            }
            if (!UInt16.TryParse(perAddr[3], out sbAddr))
            {
                return false;
            }
            return true;
        }
    }

    internal class StaticValue
    {
        public static string CountStr = "数量";

        public static string SenderName = "发送卡";
        public static string PortName = "网口";
        public static string ScanBoardName = "接收卡";
    }
}
