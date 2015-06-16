using Nova.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Nova.Monitoring.MonitorDataManager
{
    /// <summary>
    /// 控制策略集合
    /// </summary>
    public class MonitorCtrlConfigInfo
    {
        public bool IsEnableMonitorCtrl = false;
        public int CtrlLogValidDays = 30;
        public List<IMonitorCtrlInfo> MonitorCtrlInfoList = new List<IMonitorCtrlInfo>();

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            MonitorCtrlConfigInfo newObj = new MonitorCtrlConfigInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            MonitorCtrlConfigInfo info = (MonitorCtrlConfigInfo)obj;
            if (info == null)
            {
                return false;
            }
            info.IsEnableMonitorCtrl = IsEnableMonitorCtrl;
            info.CtrlLogValidDays = CtrlLogValidDays;
            if (MonitorCtrlInfoList == null)
            {
                info.MonitorCtrlInfoList = null;
            }
            else
            {
                info.MonitorCtrlInfoList = new List<IMonitorCtrlInfo>();
                for (int i = 0; i < MonitorCtrlInfoList.Count; i++)
                {
                    info.MonitorCtrlInfoList.Add(MonitorCtrlInfoList[i]);
                }
            }
            return true;
        }
    }

    #region 内部小对象
    public class CtrlFCAddr
    {
        public bool FCIsConnectCommPort { get; set; }
        public byte SenderAddr { get; set; }
        public byte PortAddr { get; set; }
        public UInt16 FuncCardAddr { get; set; }
        public string FCName { get; set; }
        public string CommPort { get; set; }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlFCAddr newObj = new CtrlFCAddr();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlFCAddr info = (CtrlFCAddr)obj;
            if (info == null)
            {
                return false;
            }
            info.FCIsConnectCommPort = FCIsConnectCommPort;
            info.SenderAddr = SenderAddr;
            info.PortAddr = PortAddr;
            info.FuncCardAddr = FuncCardAddr;
            info.FCName = FCName;
            info.CommPort = CommPort;
            return true;
        }
    }

    public class CtrlPowerAddrInfo
    {
        public CtrlPowerAddrInfo()
        {
            FCAddrInfo = new CtrlFCAddr();
        }
        public CtrlFCAddr FCAddrInfo { get; set; }
        public byte PowerIndex { get; set; }
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlPowerAddrInfo newObj = new CtrlPowerAddrInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlPowerAddrInfo info = (CtrlPowerAddrInfo)obj;
            if (info == null)
            {
                return false;
            }
            info.FCAddrInfo = (CtrlFCAddr)FCAddrInfo.Clone();
            info.PowerIndex = PowerIndex;
            return true;
        }
    }

    public class OnePowerCtrlRes
    {
        public OnePowerCtrlRes()
        {
            CtrlResult = SendCtrlInfoResult.Unknown;
            CurCtrlPowerAddr = new CtrlPowerAddrInfo();
        }

        public SendCtrlInfoResult CtrlResult { get; set; }
        public CtrlPowerAddrInfo CurCtrlPowerAddr { get; set; }

        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            OnePowerCtrlRes newObj = new OnePowerCtrlRes();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            OnePowerCtrlRes info = (OnePowerCtrlRes)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlResult = CtrlResult;
            info.CurCtrlPowerAddr = (CtrlPowerAddrInfo)CurCtrlPowerAddr.Clone();
            return true;
        }
    }
    #endregion

    #region 显示网格类
    public class DataGridViewCtrlInfo
    {
        public DataGridViewCtrlInfo() { }
        public DataGridViewCtrlInfo(IMonitorCtrlInfo ctrlInfo, string ctrlContent, string viewPowerDetail, string screenNumber)
        {
            CtrlInfo = ctrlInfo;
            CtrlContent = ctrlContent;
            ViewPowerDetail = viewPowerDetail;
            ScreenNumber = screenNumber;
        }
        public IMonitorCtrlInfo CtrlInfo { get; set; }

        public string CtrlContent { get; set; }

        public string ViewPowerDetail { get; set; }

        public string ScreenNumber { get; set; }
    }
    #endregion

    #region 实现策略接口类

    public interface IMonitorCtrlInfo
    {
        /// <summary>
        /// 屏序号
        /// </summary>
        int DisplayIndex { get; set; }
        /// <summary>
        /// 策略类型
        /// </summary>
        ControlOperateType OperateType { get; }
        /// <summary>
        /// 控制类型
        /// </summary>
        ControlReasonType CtrlMonitorReason { get; set; }
        /// <summary>
        /// 温度分类(最高，平均)
        /// </summary>
        DescreaseTempType DesTempType { get; set; }
        /// <summary>
        /// 第一阀值
        /// </summary>
        int CtrlThreshold { get; set; }
        /// <summary>
        /// 第二阀值
        /// </summary>
        int SecondCtrlThreshold { get; set; }
    }

    /// <summary>
    /// 亮度策略
    /// </summary>
    public class DecreaseBrightConfigInfo : IMonitorCtrlInfo
    {
        public DecreaseBrightConfigInfo()
        {
            CtrlMonitorReason = ControlReasonType.TemperatureHigh;
            DesTempType = DescreaseTempType.HighTemperature;
            IsRestoreBright = true;
            CtrlThreshold = 60;
            SecondCtrlThreshold = 70;
            DecreaseBright = 50;
            RestoreTempThreshold = 0;
        }

        public int DisplayIndex { get; set; }
        public ControlOperateType OperateType
        {
            get { return ControlOperateType.DecreaseBright; }
        }
        public ControlReasonType CtrlMonitorReason { get; set; }
        public DescreaseTempType DesTempType { get; set; }
        public int CtrlThreshold { get; set; }
        public int SecondCtrlThreshold { get; set; }
        /// <summary>
        /// 降低亮度到
        /// </summary>
        public byte DecreaseBright { get; set; }
        /// <summary>
        /// 恢复亮度
        /// </summary>
        public int RestoreTempThreshold { get; set; }
        /// <summary>
        /// 是否恢复
        /// </summary>
        public bool IsRestoreBright { get; set; }

        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            DecreaseBrightConfigInfo newObj = new DecreaseBrightConfigInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            DecreaseBrightConfigInfo info = (DecreaseBrightConfigInfo)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlMonitorReason = CtrlMonitorReason;
            info.DecreaseBright = DecreaseBright;
            info.CtrlThreshold = CtrlThreshold;
            info.DisplayIndex = DisplayIndex;
            info.RestoreTempThreshold = RestoreTempThreshold;
            info.IsRestoreBright = IsRestoreBright;
            info.SecondCtrlThreshold = SecondCtrlThreshold;
            info.DesTempType = DesTempType;
            return true;
        }
    }
    /// <summary>
    /// 打开设备
    /// </summary>
    public class CtrlPowerOnConfigInfo : IMonitorCtrlInfo
    {
        public CtrlPowerOnConfigInfo()
        {
            CtrlMonitorReason = ControlReasonType.TemperatureHigh;
            DesTempType = DescreaseTempType.HighTemperature;
            SecondCtrlThreshold = 70;
            CtrlThreshold = 60;
            PowerOffTempThreshold = 50;
            CtrlPowerList = new List<CtrlPowerAddrInfo>();
            IsPowerOff=true;
        }

        public int DisplayIndex { get; set; }
        public ControlOperateType OperateType
        {
            get { return ControlOperateType.PowerOn; }
        }
        public ControlReasonType CtrlMonitorReason { get; set; }
        public DescreaseTempType DesTempType { get; set; }
        public int SecondCtrlThreshold { get; set; }
        public int CtrlThreshold { get; set; }
        /// <summary>
        /// 控制设备列表
        /// </summary>
        public List<CtrlPowerAddrInfo> CtrlPowerList { get; set; }
        /// <summary>
        /// 关闭设备温度
        /// </summary>
        public int PowerOffTempThreshold { get; set; }
        /// <summary>
        /// 是否需要关闭
        /// </summary>
        public bool IsPowerOff { get; set; }

        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlPowerOnConfigInfo newObj = new CtrlPowerOnConfigInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlPowerOnConfigInfo info = (CtrlPowerOnConfigInfo)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlMonitorReason = CtrlMonitorReason;
            info.CtrlThreshold = CtrlThreshold;
            info.DisplayIndex = DisplayIndex;
            info.SecondCtrlThreshold = SecondCtrlThreshold;
            info.DesTempType = DesTempType;
            if (CtrlPowerList == null)
            {
                info.CtrlPowerList = null;
            }
            else
            {
                info.CtrlPowerList = new List<CtrlPowerAddrInfo>();
                for (int i = 0; i < CtrlPowerList.Count; i++)
                {
                    info.CtrlPowerList.Add((CtrlPowerAddrInfo)CtrlPowerList[i].Clone());
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 关闭设备
    /// </summary>
    public class CtrlPowerOffConfigInfo : IMonitorCtrlInfo
    {
        public CtrlPowerOffConfigInfo()
        {
            CtrlMonitorReason = ControlReasonType.TemperatureHigh;
            DesTempType = DescreaseTempType.HighTemperature;
            CtrlThreshold = 1;
            SecondCtrlThreshold = 70;
            IsSendNotifyEmail = false;
            CtrlPowerList = new List<CtrlPowerAddrInfo>();
        }

        public int DisplayIndex { get; set; }
        public ControlOperateType OperateType
        {
            get { return ControlOperateType.PowerOff; }
        }
        public ControlReasonType CtrlMonitorReason { get; set; }
        public DescreaseTempType DesTempType { get; set; }
        public int SecondCtrlThreshold { get; set; }
        public int CtrlThreshold { get; set; }
        /// <summary>
        /// 是否发邮件
        /// </summary>
        public bool IsSendNotifyEmail { get; set; }
        /// <summary>
        /// 设备列表
        /// </summary>
        public List<CtrlPowerAddrInfo> CtrlPowerList { get; set; }

        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlPowerOnConfigInfo newObj = new CtrlPowerOnConfigInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlPowerOnConfigInfo info = (CtrlPowerOnConfigInfo)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlMonitorReason = CtrlMonitorReason;
            info.CtrlThreshold = CtrlThreshold;
            info.DisplayIndex = DisplayIndex;
            info.SecondCtrlThreshold = SecondCtrlThreshold;
            info.DesTempType = DesTempType;
            if (CtrlPowerList == null)
            {
                info.CtrlPowerList = null;
            }
            else
            {
                info.CtrlPowerList = new List<CtrlPowerAddrInfo>();
                for (int i = 0; i < CtrlPowerList.Count; i++)
                {
                    info.CtrlPowerList.Add((CtrlPowerAddrInfo)CtrlPowerList[i].Clone());
                }
            }
            return true;
        }
    }

    #endregion

    #region CtrlLog
    public interface IMonitorCtrlRes
    {
        ControlOperateType OperateType { get; }
        ControlReasonType CtrlMonitorReason { get; set; }
        DescreaseTempType DesTempType { get; set; }
        DateTime CtrlTime { get; set; }
        string DisplayName { get; set; }
        int CtrlThreshold { get; set; }
        int SecondCtrlThreshold { get; set; }
    }

    public class CtrlDecreaseBrightRes : IMonitorCtrlRes
    {
        public CtrlDecreaseBrightRes()
        {
            CtrlMonitorReason = ControlReasonType.TemperatureHigh;
            DesTempType = DescreaseTempType.HighTemperature;
            CtrlResult = SendCtrlInfoResult.Unknown;
            DecreaseBright = 100;
            CtrlThreshold = 50;
            SecondCtrlThreshold = 70;
        }
        public ControlOperateType OperateType
        {
            get { return ControlOperateType.DecreaseBright; }
        }
        public ControlReasonType CtrlMonitorReason { get; set; }
        public DescreaseTempType DesTempType { get; set; }
        public DateTime CtrlTime { get; set; }
        public string DisplayName { get; set; }
        public SendCtrlInfoResult CtrlResult { get; set; }
        public byte DecreaseBright { get; set; }
        public int CtrlThreshold { get; set; }
        public int SecondCtrlThreshold { get; set; }
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            DecreaseBrightConfigInfo newObj = new DecreaseBrightConfigInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlDecreaseBrightRes info = (CtrlDecreaseBrightRes)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlMonitorReason = CtrlMonitorReason;
            info.DecreaseBright = DecreaseBright;
            info.CtrlTime = CtrlTime;
            info.DisplayName = DisplayName;
            info.CtrlResult = CtrlResult;
            info.CtrlThreshold = CtrlThreshold;
            info.SecondCtrlThreshold = SecondCtrlThreshold;
            info.DesTempType = DesTempType;
            return true;
        }
    }
    public class CtrlRestoreBrightRes : IMonitorCtrlRes
    {
        public CtrlRestoreBrightRes()
        {
            CtrlMonitorReason = ControlReasonType.TemperatureDecrease;
            DesTempType = DescreaseTempType.HighTemperature;
            CtrlResult = SendCtrlInfoResult.Unknown;
            RestoreBrightValue = 100;
            CtrlThreshold = 60;
            SecondCtrlThreshold = 70;
        }

        public ControlOperateType OperateType
        {
            get { return ControlOperateType.RestoreBright; }
        }
        public ControlReasonType CtrlMonitorReason { get; set; }
        public DateTime CtrlTime { get; set; }
        public string DisplayName { get; set; }
        public SendCtrlInfoResult CtrlResult { get; set; }
        public byte RestoreBrightValue { get; set; }
        public int CtrlThreshold { get; set; }
        public int SecondCtrlThreshold { get; set; }
        public DescreaseTempType DesTempType { get; set; }

        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            DecreaseBrightConfigInfo newObj = new DecreaseBrightConfigInfo();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlRestoreBrightRes info = (CtrlRestoreBrightRes)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlMonitorReason = CtrlMonitorReason;
            info.RestoreBrightValue = RestoreBrightValue;
            info.CtrlTime = CtrlTime;
            info.DisplayName = DisplayName;
            info.CtrlResult = CtrlResult;
            info.CtrlThreshold = CtrlThreshold;
            info.SecondCtrlThreshold = SecondCtrlThreshold;
            info.DesTempType = DesTempType;
            return true;
        }
    }
    public class CtrlPowerOffRes : IMonitorCtrlRes
    {
        public CtrlPowerOffRes()
        {
            CtrlMonitorReason = ControlReasonType.TemperatureHigh;
            DesTempType = DescreaseTempType.HighTemperature;
            PowerCtrlResList = new List<OnePowerCtrlRes>();
            CtrlThreshold = 60;
            SecondCtrlThreshold = 70;
            IsSendNotifyEmail = false;
        }
        public ControlOperateType OperateType
        {
            get { return ControlOperateType.PowerOff; }
        }
        public ControlReasonType CtrlMonitorReason { get; set; }
        public DateTime CtrlTime { get; set; }
        public string DisplayName { get; set; }
        public List<OnePowerCtrlRes> PowerCtrlResList { get; set; }
        public int CtrlThreshold { get; set; }
        public bool IsSendNotifyEmail { get; set; }
        public int SecondCtrlThreshold { get; set; }
        public DescreaseTempType DesTempType { get; set; }
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlPowerOffRes newObj = new CtrlPowerOffRes();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlPowerOffRes info = (CtrlPowerOffRes)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlMonitorReason = CtrlMonitorReason;
            info.CtrlThreshold = CtrlThreshold;
            info.CtrlTime = CtrlTime;
            info.DisplayName = DisplayName;
            info.CtrlThreshold = CtrlThreshold;
            info.IsSendNotifyEmail = IsSendNotifyEmail;
            info.SecondCtrlThreshold = SecondCtrlThreshold;
            info.DesTempType = DesTempType;
            if (PowerCtrlResList == null)
            {
                info.PowerCtrlResList = null;
            }
            else
            {
                info.PowerCtrlResList = new List<OnePowerCtrlRes>();
                for (int i = 0; i < PowerCtrlResList.Count; i++)
                {
                    info.PowerCtrlResList.Add((OnePowerCtrlRes)PowerCtrlResList[i].Clone());
                }
            }
            return true;
        }
    }
    public class CtrlPowerOnRes : IMonitorCtrlRes
    {
        public CtrlPowerOnRes()
        {
            CtrlMonitorReason = ControlReasonType.TemperatureHigh;
            DesTempType = DescreaseTempType.HighTemperature;
            PowerCtrlResList = new List<OnePowerCtrlRes>();
            CtrlThreshold = 50;
            SecondCtrlThreshold = 70;
        }

        public ControlOperateType OperateType
        {
            get { return ControlOperateType.PowerOn; }
        }
        public ControlReasonType CtrlMonitorReason { get; set; }
        public DateTime CtrlTime { get; set; }
        public string DisplayName { get; set; }
        public List<OnePowerCtrlRes> PowerCtrlResList { get; set; }
        public int CtrlThreshold { get; set; }
        public int SecondCtrlThreshold { get; set; }
        public DescreaseTempType DesTempType { get; set; }

        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CtrlPowerOnRes newObj = new CtrlPowerOnRes();
            CopyTo(newObj);
            return newObj;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CopyTo(object obj)
        {
            CtrlPowerOnRes info = (CtrlPowerOnRes)obj;
            if (info == null)
            {
                return false;
            }
            info.CtrlMonitorReason = CtrlMonitorReason;
            info.CtrlThreshold = CtrlThreshold;
            info.CtrlTime = CtrlTime;
            info.DisplayName = DisplayName;
            info.CtrlThreshold = CtrlThreshold;
            info.SecondCtrlThreshold = SecondCtrlThreshold;
            info.DesTempType = DesTempType;
            if (PowerCtrlResList == null)
            {
                info.PowerCtrlResList = null;
            }
            else
            {
                info.PowerCtrlResList = new List<OnePowerCtrlRes>();
                for (int i = 0; i < PowerCtrlResList.Count; i++)
                {
                    info.PowerCtrlResList.Add((OnePowerCtrlRes)PowerCtrlResList[i].Clone());
                }
            }
            return true;
        }
    }
    #endregion

    #region 写日志类
    public class MonitorControlLogFile : XmlFile
    {
        #region 字段
        private static readonly string ROOT_NAME = "MonitorControlInfoList";
        private static readonly string FIRST_NAME = "OneControlInfo";
        private static readonly string CTRLINFO_TYPE = "Type";

        private int[] _scaleIndex = new int[2];
        #endregion

        /// <summary>
        /// 初始化文件操作类
        /// </summary>
        /// <param name="szXmlPathName"></param>
        /// <param name="XmlFlag"></param>
        /// <param name="bSuccess"></param>
        public MonitorControlLogFile(string szXmlPathName, XmlFile.XmlFileFlag XmlFlag, out bool bSuccess)
            : base(szXmlPathName, XmlFlag, ROOT_NAME, out bSuccess)
        {
            _nTotalScale = 2;
        }
        /// <summary>
        /// 添加一个日志信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddCtrlLogInfoInfo(IMonitorCtrlRes info)
        {
            lock (this)
            {
                _scaleIndex[0] = 0;
                string infoStr = SerializeLogInfo(info);
                string indentInfoString = DoIndentation(infoStr, 2 * 2);
                if (!AddXmlScaleElement(0, _scaleIndex, 1, new string[] { FIRST_NAME }, new string[] { indentInfoString }))
                {
                    return false;
                }

                int infoCnt = GetXmlScaleChildCount(0, _scaleIndex);
                _scaleIndex[1] = infoCnt - 1;
                if (!SetCommonNote(1, _scaleIndex, CTRLINFO_TYPE, info.OperateType.ToString()))
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 载入所有日志信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool LoadAllLogInfo(out List<IMonitorCtrlRes> infoList)
        {
            lock (this)
            {
                infoList = new List<IMonitorCtrlRes>();

                _scaleIndex[0] = 0;

                int infoCnt = GetXmlScaleChildCount(0, _scaleIndex);

                IMonitorCtrlRes ctrlInfo = null;
                string infoValue;
                string ctrlInfoTypeStr = "";
                ControlOperateType ctrlType;
                Object ctrlTypeObj = null;
                for (int i = 0; i < infoCnt; i++)
                {
                    _scaleIndex[1] = i;
                    if (!GetCommonNote(1, _scaleIndex, CTRLINFO_TYPE, out ctrlInfoTypeStr))
                    {
                        return false;
                    }
                    ctrlTypeObj = Enum.Parse(typeof(ControlOperateType), ctrlInfoTypeStr);
                    if (Enum.IsDefined(typeof(ControlOperateType), ctrlTypeObj))
                    {
                        ctrlType = (ControlOperateType)ctrlTypeObj;
                    }
                    else
                    {
                        return false;
                    }

                    if (!GetXmlScaleSpecElementByIndex(0, _scaleIndex, i, out infoValue))
                    {
                        return false;
                    }
                    if (!DeSerializeLogInfo(infoValue, ctrlType, out ctrlInfo))
                    {
                        return false;
                    }
                    infoList.Add(ctrlInfo);
                }
            }
            return true;
        }

        /// <summary>
        /// 序列化一个对象
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string SerializeLogInfo(IMonitorCtrlRes info)
        {
            Type type = info.GetType();
            XmlSerializer serializer = new XmlSerializer(type);

            TextWriter textWriter = new StringWriter();
            serializer.Serialize(textWriter, info);
            textWriter.Flush();


            string xml = textWriter.ToString();
            textWriter.Close();

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xml);
            doc = Clean(doc);

            //若字符串开头为回车符，则去掉
            xml = doc.OuterXml;
            if (xml[0] == '\r' && xml[1] == '\n')
            {
                xml = xml.Substring(2, xml.Length - 2);
            }
            return xml;
        }
        /// <summary>
        /// 反序列化xml字符串为指定对象
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="info"></param>
        private bool DeSerializeLogInfo(string xml,
                                         ControlOperateType ctrlType,
                                         out IMonitorCtrlRes info)
        {
            try
            {
                Type type;
                if (ctrlType == ControlOperateType.DecreaseBright)
                {
                    type = typeof(CtrlDecreaseBrightRes);
                }
                else if (ctrlType == ControlOperateType.RestoreBright)
                {
                    type = typeof(CtrlRestoreBrightRes);
                }
                else if (ctrlType == ControlOperateType.PowerOff)
                {
                    type = typeof(CtrlPowerOffRes);
                }
                else if (ctrlType == ControlOperateType.PowerOn)
                {
                    type = typeof(CtrlPowerOnRes);
                }
                else
                {
                    info = null;
                    return false;
                }
                XmlSerializer serializer = new XmlSerializer(type);

                TextReader reader = new StringReader(xml);
                if (ctrlType == ControlOperateType.DecreaseBright)
                {
                    info = (CtrlDecreaseBrightRes)serializer.Deserialize(reader);
                }
                else if (ctrlType == ControlOperateType.RestoreBright)
                {
                    info = (CtrlRestoreBrightRes)serializer.Deserialize(reader);
                }
                else if (ctrlType == ControlOperateType.PowerOff)
                {
                    info = (CtrlPowerOffRes)serializer.Deserialize(reader);
                }
                else
                {
                    info = (CtrlPowerOnRes)serializer.Deserialize(reader);
                }
                reader.Close();
                return true;
            }
            catch
            {
                info = null;
                return false;
            }
        }
        /// <summary>
        /// 移除xml命名空间（序列化时自动添加的)
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private XmlDocument Clean(XmlDocument doc)
        {
            doc.RemoveChild(doc.FirstChild);
            XmlNode first = doc.FirstChild;
            foreach (XmlNode n in doc.ChildNodes)
            {
                if (n.NodeType == XmlNodeType.Element)
                {
                    first = n;
                    break;
                }
            }
            if (first.Attributes != null)
            {
                XmlAttribute a = null;
                a = first.Attributes["xmlns:xsd"];
                if (a != null) { first.Attributes.Remove(a); }
                a = first.Attributes["xmlns:xsi"];
                if (a != null) { first.Attributes.Remove(a); }
            }
            return doc;
        }
        /// <summary>
        /// 缩进
        /// </summary>
        /// <param name="sorXml"></param>
        /// <param name="indentationLength"></param>
        /// <returns></returns>
        private string DoIndentation(string sorXml, int indentationLength)
        {
            string indentString = "";
            for (int i = 0; i < indentationLength; i++)
            {
                indentString += " ";
            }

            int startIndex = 0;
            string desXml = sorXml;
            while (true)
            {
                int index = desXml.IndexOf("\r\n", startIndex);
                if (index == -1)
                {
                    break;
                }
                else
                {
                    startIndex = index + "\r\n".Length;
                    desXml = desXml.Insert(startIndex, indentString);
                    startIndex += indentationLength;
                }
            }

            return desXml;
        }
    }

    #endregion
}