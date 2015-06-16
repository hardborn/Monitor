using Log4NetLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace Nova.Monitoring.SystemMessageManager
{
    public class SystemInfo
    {
        #region AIP声明
        [DllImport("IpHlpApi.dll")]
        extern static public uint GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder);

        [DllImport("User32")]
        private extern static int GetWindow(int hWnd, int wCmd);

        [DllImport("User32")]
        private extern static int GetWindowLongA(int hWnd, int wIndx);

        [DllImport("user32.dll")]
        private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static int GetWindowTextLength(IntPtr hWnd);
        #endregion

        FileLogService _logService = new FileLogService(typeof(SystemInfo));

        #region 构造函数
        /// <summary>
        /// 构造函数，初始化计数器等
        /// </summary>
        public SystemInfo()
        {
            try
            {
                //初始化CPU计数器
                pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                pcCpuLoad.MachineName = ".";
                pcCpuLoad.NextValue();

                //CPU个数
                _processorCount = Environment.ProcessorCount;

                //获得物理内存
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo["TotalPhysicalMemory"] != null)
                    {
                        _physicalMemory = long.Parse(mo["TotalPhysicalMemory"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:error：<-{0}->:{1} \r\n error detail：{2}", "SystemInfo", ex.Message, ex.ToString()));
            }
        }
        #endregion
        #region CPU个数
        /// <summary>
        /// 获取CPU个数
        /// </summary>
        private int _processorCount = 0;   //CPU个数
        public int ProcessorCount
        {
            get
            {
                return _processorCount;
            }
        }
        #endregion
        #region CPU占用率
        /// <summary>
        /// 获取CPU占用率
        /// </summary>
        private PerformanceCounter pcCpuLoad;   //CPU计数器
        public int CpuLoad
        {
            get
            {
                try
                {
                    var percentage = this.pcCpuLoad.NextValue();
                    return (int)Math.Round(percentage, 2, MidpointRounding.AwayFromZero);
                }
                catch (Exception ex)
                {
                    _logService.Error(string.Format("ExistCatch:error：<-{0}->:{1} \r\n error detail：{2}", "SystemInfo", ex.Message, ex.ToString()));
                    return 0;
                }
                //int cpu = 0;
                //try
                //{
                //    Math.Truncate(Convert.ToDecimal(pcCpuLoad.NextValue()));
                //    string f = Math.Truncate(Convert.ToDecimal(pcCpuLoad.NextValue())).ToString().Replace(',', '.');
                //    cpu = Convert.ToInt32(f);
                //}
                //catch
                //{
                //}
                ////return Convert.ToInt32(pcCpuLoad.NextValue().ToString().Replace(',', '.'));
                //return cpu;
            }
        }
        #endregion
        #region 可用内存
        /// <summary>
        /// 获取可用内存
        /// </summary>
        public long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
                    }
                }
                return availablebytes;
            }
        }
        #endregion
        #region 物理内存
        /// <summary>
        /// 获取物理内存
        /// </summary>
        private long _physicalMemory = 0;   //物理内存
        public long PhysicalMemory
        {
            get
            {
                return _physicalMemory;
            }
        }
        #endregion
        #region 获得分区信息
        /// <summary>
        /// 获取分区信息
        /// </summary>
        public List<HDDInfo> GetLogicalDrives()
        {
            List<HDDInfo> drives = new List<HDDInfo>();
            ManagementClass diskClass = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection disks = diskClass.GetInstances();
            foreach (ManagementObject disk in disks)
            {
                // DriveType.Fixed 为固定磁盘(硬盘)
                if (int.Parse(disk["DriveType"].ToString()) == (int)DriveType.Fixed)
                {
                    drives.Add(new HDDInfo(disk["Name"].ToString(), long.Parse(disk["Size"].ToString()), long.Parse(disk["FreeSpace"].ToString())));
                }
            }
            return drives;
        }
        /// <summary>
        /// 获取特定分区信息
        /// </summary>
        /// <param name="DriverID">盘符</param>
        public List<HDDInfo> GetLogicalDrives(char DriverID)
        {
            List<HDDInfo> drives = new List<HDDInfo>();
            WqlObjectQuery wmiquery = new WqlObjectQuery("SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '" + DriverID + ":'");
            ManagementObjectSearcher wmifind = new ManagementObjectSearcher(wmiquery);
            foreach (ManagementObject disk in wmifind.Get())
            {
                if (int.Parse(disk["DriveType"].ToString()) == (int)DriveType.Fixed)
                {
                    drives.Add(new HDDInfo(disk["Name"].ToString(), long.Parse(disk["Size"].ToString()), long.Parse(disk["FreeSpace"].ToString())));
                }
            }
            return drives;
        }
        #endregion
        #region 获取本机的Mac地址
        public static string GetMACAddress()
        {
            foreach (System.Net.NetworkInformation.NetworkInterface nic
                in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    return AddressBytesToString(nic.GetPhysicalAddress().GetAddressBytes());
            }

            return string.Empty;
        }

        private static string AddressBytesToString(byte[] addressBytes)
        {
            return string.Join("", (from b in addressBytes
                                    select b.ToString("X2")).ToArray());
        }
        #endregion

    }
}
