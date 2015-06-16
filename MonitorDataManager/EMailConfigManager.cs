using Nova.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    [Serializable]
    public class EMailNotifyConfig : ICloneable
    {
        public static string DEFAULT_EMAIL_ADDR = "NovaStarTech@126.com";
        public static string DEFAULT_EMAIL_PW = "novastar";
        public static string DEFAULT_SMTP_SERVER = "smtp.126.com";
        public static int DEFAULT_EMAIL_PORT = 25;
        public static string DEFAULT_EMAIL_SEND_SOURCE = "A-1";

        /// <summary>
        /// 是否使用加密连接
        /// </summary>
        public bool IsEnableSsl
        {
            get
            {
                return _bEnableSsl;
            }
            set
            {
                _bEnableSsl = value;
            }
        }
        private bool _bEnableSsl = true;
        /// <summary>
        /// 定期发送邮件模式
        /// </summary>
        public EMailSendModel SendMailModel
        {
            get { return _sendMailModel; }
            set { _sendMailModel = value; }
        }
        private EMailSendModel _sendMailModel = EMailSendModel.None;
        /// <summary>
        /// 每周发送情况下，星期几发送
        /// </summary>
        public DayOfWeek SendMailWeek
        {
            get { return _sendMailWeek; }
            set { _sendMailWeek = value; }
        }
        private DayOfWeek _sendMailWeek = DayOfWeek.Monday;
        /// <summary>
        /// 定期发送时间
        /// </summary>
        public DateTime SendTimer
        {
            get { return _sendTimer; }
            set { _sendTimer = value; }
        }
        private DateTime _sendTimer = DateTime.Now;

        /// <summary>
        /// 是否启用系统恢复日志通知
        /// </summary>
        public bool EnableRecoverNotify
        {
            get { return _enableRecoverNotify; }
            set { _enableRecoverNotify = value; }
        }
        private bool _enableRecoverNotify = false;
        /// <summary>
        /// 是否启定期邮件通知
        /// </summary>
        public bool TimeEMailNotify
        {
            get { return _timeEMailNotify; }
            set { _timeEMailNotify = value; }
        }
        private bool _timeEMailNotify = false;
        /// <summary>
        /// 是否启用日志通知
        /// </summary>
        public bool EnableNotify
        {
            get { return _enableNotify; }
            set { _enableNotify = value; }
        }
        private bool _enableNotify = false;

        /// <summary>
        /// 是否启通知用日志记录
        /// </summary>
        public bool EnableJournal
        {
            get { return _enableJournal; }
            set { _enableJournal = value; }
        }
        private bool _enableJournal = false;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddr
        {
            get { return _emailAddr; }
            set { _emailAddr = value; }
        }
        private string _emailAddr = string.Empty;

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _password = string.Empty;

        /// <summary>
        /// SMTP服务器地址
        /// </summary>
        public string SmtpServer
        {
            get { return _smtpServer; }
            set { _smtpServer = value; }
        }
        private string _smtpServer = string.Empty;

        /// <summary>
        /// 发送邮件的端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        private int _port = 25;

        /// <summary>
        /// 接收邮件人信息
        /// </summary>
        public BindingList<ReceiverInfo> ReceiveInfoList
        {
            get { return _receiveInfoList; }
            set { _receiveInfoList = value; }
        }
        private BindingList<ReceiverInfo> _receiveInfoList = new BindingList<ReceiverInfo>();

        /// <summary>
        /// 邮件从哪里发出
        /// </summary>
        public string EmailSendSource
        {
            get { return _emailSendSource; }
            set { _emailSendSource = value; }
        }
        private string _emailSendSource = string.Empty;
        /// <summary>
        /// 通知保存的时间
        /// </summary>
        public int LogSaveDays
        {
            get
            {
                return _logSaveDays;
            }
            set
            {
                _logSaveDays = value;
            }
        }
        private int _logSaveDays = 7;

        public EMailNotifyConfig()
        {
            _emailAddr = DEFAULT_EMAIL_ADDR;
            _emailSendSource = DEFAULT_EMAIL_SEND_SOURCE;
            _password = DEFAULT_EMAIL_PW;
            _port = DEFAULT_EMAIL_PORT;
            _smtpServer = DEFAULT_SMTP_SERVER;
        }

        #region ICloneable 成员

        public object Clone()
        {
            EMailNotifyConfig temp = new EMailNotifyConfig();
            temp._enableRecoverNotify = this._enableRecoverNotify;
            temp._sendTimer = this._sendTimer;
            temp._sendMailWeek = this._sendMailWeek;
            temp._sendMailModel = this._sendMailModel;
            temp._timeEMailNotify = this._timeEMailNotify;
            temp._emailAddr = this._emailAddr;
            temp._emailSendSource = this._emailSendSource;
            temp._enableJournal = this._enableJournal;
            temp._enableNotify = this._enableNotify;
            temp._password = this._password;
            temp._port = this._port;
            temp._smtpServer = this._smtpServer;
            temp._logSaveDays = this._logSaveDays;
            temp._bEnableSsl = this._bEnableSsl;
            for (int i = 0; i < this._receiveInfoList.Count; i++)
            {
                temp._receiveInfoList.Add((ReceiverInfo)_receiveInfoList[i].Clone());
            }
            return temp;
        }

        #endregion
    }

    [Serializable]
    public class ReceiverInfo : ICloneable
    {
        /// <summary>
        /// 接收者的姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name = string.Empty;

        /// <summary>
        /// 接收者的邮箱地址
        /// </summary>
        public string EmailAddr
        {
            get { return _emailAddr; }
            set { _emailAddr = value; }
        }
        private string _emailAddr = string.Empty;

        public ReceiverInfo(string name, string emailAddr)
        {
            _name = name;
            _emailAddr = emailAddr;
        }

        public ReceiverInfo()
        {

        }

        #region ICloneable 成员

        public object Clone()
        {
            ReceiverInfo temp = new ReceiverInfo(this._name, this._emailAddr);
            return temp;
        }

        #endregion
    }

    [Serializable]
    public class NotifyContent : ICloneable, IComparable
    {
        /// <summary>
        /// 用于唯一标识这条消息的标识码
        /// </summary>
        public string MsgID
        {
            get
            {
                return _msgID;
            }
            set
            {
                _msgID = value;
            }
        }
        private string _msgID;
        /// <summary>
        /// 通知的类型
        /// </summary>
        public NotifyType ErrNotifyType
        {
            get
            {
                return _errNotifyType;
            }
            set
            {
                _errNotifyType = value;
            }
        }
        private NotifyType _errNotifyType = NotifyType.Email;
        /// <summary>
        /// 显示屏的名称
        /// </summary>
        public string ScreenName
        {
            get
            {
                return _screenName;
            }
            set
            {
                _screenName = value;
            }
        }
        private string _screenName = "";
        /// <summary>
        /// 通知的日期
        /// </summary>
        public string MsgDate
        {
            get
            {
                return _msgDate;
            }
            set
            {
                _msgDate = value;
            }
        }
        private string _msgDate = "";
        /// <summary>
        /// 通知的内容
        /// </summary>
        public string MsgContent
        {
            get
            {
                return _msgContent;
            }
            set
            {
                _msgContent = value;
            }
        }
        private string _msgContent = "";
        /// <summary>
        /// 消息的主题
        /// </summary>
        public string MsgTitle
        {
            get
            {
                return _msgTitle;
            }
            set
            {
                _msgTitle = value;
            }
        }
        private string _msgTitle = "";

        /// <summary>
        /// 接收通知的人
        /// </summary>
        public string Receiver
        {
            get
            {
                return _receiver;
            }
            set
            {
                _receiver = value;
            }
        }
        private string _receiver = "";
        /// <summary>
        /// 通知的状态
        /// </summary>
        public NotifyState NotifyState
        {
            get
            {
                return _notifyState;
            }
            set
            {
                _notifyState = value;
            }
        }
        private NotifyState _notifyState = NotifyState.Sended;

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<string> AttachmentFileNameList
        {
            get
            {
                return _attachmentFileNameList;
            }
            set
            {
                _attachmentFileNameList = value;
            }
        }
        private List<string> _attachmentFileNameList = new List<string>();

        public NotifyContent()
        {

        }

        #region ICloneable 成员

        public object Clone()
        {
            NotifyContent temp = new NotifyContent();
            temp._errNotifyType = this._errNotifyType;
            temp._msgContent = this._msgContent;
            temp._msgDate = this._msgDate;
            temp._msgID = this._msgID;
            temp._msgTitle = this._msgTitle;
            temp._notifyState = this._notifyState;
            temp._receiver = this._receiver;
            temp._screenName = this._screenName;

            temp.AttachmentFileNameList = new List<string>();
            foreach (string att in _attachmentFileNameList)
            {
                temp.AttachmentFileNameList.Add(att);
            }
            return temp;
        }

        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            if (!(obj is NotifyContent))
            {
                return -1;
            }
            NotifyContent temp = (NotifyContent)obj;
            if (temp.NotifyState > temp.NotifyState)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }

    public class ErrorNotifyLog : XmlFile
    {
        private string[] NotifyLogName = new string[] { "ID", "NotifyType", "ScreenName", "Date", "Context", "Title",
                                               "Receiver", "NotifyState"};
        private enum NotifyLogInfoType
        {
            ID = 0, NotifyType, ScreenName, Date, Context, Title, Receiver, NotifyState, EndFlags
        }

        #region 字段
        private int[] _scaleIndex = new int[2];
        private string[] _propertyValue = new string[(int)NotifyLogInfoType.EndFlags];
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logPathName"></param>
        /// <param name="XmlFlag"></param>
        /// <param name="bSuccess"></param>
        public ErrorNotifyLog(string logPathName, XmlFileFlag XmlFlag, out bool bSuccess)
            : base(logPathName, XmlFlag, "ErrorNotify", out bSuccess)
        {
            _szScales[0] = "ErrorNotify";
            _szScales[1] = "ErrorNotifyItem";
        }

        #region 对外接口

        #region 设置
        /// <summary>
        /// 添加一日志项
        /// </summary>
        /// <param name="itemInf"></param>
        /// <returns></returns>
        public bool AddNotifyLog(NotifyContent itemInf)
        {
            if (itemInf == null)
            {
                return false;
            }
            UpdatePropertyValue(itemInf);

            _scaleIndex[0] = 0;
            _scaleIndex[1] = GetNotifylogItemCount();
            return AddXmlScaleElement(1, _scaleIndex, _propertyValue.Length, NotifyLogName, _propertyValue);
        }
        #endregion

        #region 获取
        /// <summary>
        /// 获取日志项个数
        /// </summary>
        /// <returns></returns>
        public int GetNotifylogItemCount()
        {
            _scaleIndex[0] = 0;
            return GetXmlScaleChildCount(0, _scaleIndex);
        }
        /// <summary>
        /// 读取指定序号的日志项
        /// </summary>
        /// <param name="index"></param>
        /// <param name="itemInf"></param>
        /// <returns></returns>
        public bool GetNotifylogItem(int index, ref NotifyContent itemInf)
        {
            _scaleIndex[0] = 0;
            _scaleIndex[1] = index;

            string[] valueArray;
            if (!GetXmlScaleSpecElements(1, _scaleIndex, NotifyLogName, out valueArray))
            {
                return false;
            }
            try
            {
                itemInf.NotifyState = (NotifyState)Enum.Parse(typeof(NotifyState),
                                       valueArray[(int)NotifyLogInfoType.NotifyState]);
                if (!Enum.IsDefined(typeof(NotifyState), itemInf.NotifyState))
                {
                    return false;
                }
                itemInf.ErrNotifyType = (NotifyType)Enum.Parse(typeof(NotifyType),
                                       valueArray[(int)NotifyLogInfoType.NotifyType]);
                if (!Enum.IsDefined(typeof(NotifyType), itemInf.ErrNotifyType))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            itemInf.MsgContent = valueArray[(int)NotifyLogInfoType.Context];
            itemInf.MsgDate = valueArray[(int)NotifyLogInfoType.Date];
            itemInf.MsgID = valueArray[(int)NotifyLogInfoType.ID];
            itemInf.Receiver = valueArray[(int)NotifyLogInfoType.Receiver];
            itemInf.ScreenName = valueArray[(int)NotifyLogInfoType.ScreenName];
            itemInf.MsgTitle = valueArray[(int)NotifyLogInfoType.Title];

            return true;
        }
        /// <summary>
        /// 获取全部的日志信息
        /// </summary>
        /// <param name="itemInf"></param>
        public void GetAllNotifylogItem(out List<NotifyContent> itemList)
        {
            int itemCnt = GetNotifylogItemCount();
            itemList = new List<NotifyContent>();

            for (int i = 0; i < itemCnt; i++)
            {
                NotifyContent newItemInf = new NotifyContent();
                if (GetNotifylogItem(i, ref newItemInf))
                {
                    itemList.Add(newItemInf);
                }
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新指定ID的日志项
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="itemInf"></param>
        /// <returns></returns>
        public bool ModifyNotifylogItemBySpecificID(string msgID, NotifyContent itemInf)
        {
            if (itemInf == null)
            {
                return false;
            }

            int itemCnt = GetNotifylogItemCount();

            for (int i = itemCnt - 1; i >= 0; i--)
            {
                NotifyContent newItemInf = new NotifyContent();
                if (GetNotifylogItem(i, ref newItemInf))
                {
                    if (newItemInf.MsgID == msgID)
                    {
                        UpdatePropertyValue(itemInf);

                        _scaleIndex[0] = 0;
                        _scaleIndex[1] = i;
                        return ModifyXmlScaleElement(1, _scaleIndex, _propertyValue.Length, NotifyLogName, _propertyValue);
                    }
                }
            }
            return false;
        }
        #endregion

        #endregion

        #region 私有函数
        private void UpdatePropertyValue(NotifyContent itemInf)
        {
            _propertyValue[(int)NotifyLogInfoType.Context] = itemInf.MsgContent;
            _propertyValue[(int)NotifyLogInfoType.Date] = itemInf.MsgDate;
            _propertyValue[(int)NotifyLogInfoType.ID] = itemInf.MsgID;
            _propertyValue[(int)NotifyLogInfoType.NotifyState] = itemInf.NotifyState.ToString();
            _propertyValue[(int)NotifyLogInfoType.NotifyType] = itemInf.ErrNotifyType.ToString();
            _propertyValue[(int)NotifyLogInfoType.Receiver] = itemInf.Receiver;
            _propertyValue[(int)NotifyLogInfoType.ScreenName] = itemInf.ScreenName;
            _propertyValue[(int)NotifyLogInfoType.Title] = itemInf.MsgTitle;
        }
        #endregion
    }
}