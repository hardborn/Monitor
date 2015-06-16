using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nova.Control;
using Nova.LCT.GigabitSystem.Common;
using System.Collections;
using Nova.Xml.Serialization;

namespace Nova.Monitoring.UI.SenderStatusDisplay
{
    public partial class UC_SenderStatus : UserControl
    {
        private Hashtable LangHashTable = null;
        private CustomToolTip _customToolTip = null;
        private UC_SenderStatusLayout _senderStatusUC = null;
        private Font _customToolTipFont = null;
        private SerializableDictionary<string, List<SenderRedundancyInfo>> _tempRedundancyDict = null;
        public UC_SenderStatus()
        {
            InitializeComponent();
            _senderStatusUC = new UC_SenderStatusLayout();
            _senderStatusUC.Parent = this.doubleBufferPanel_SenderStatus;
            _senderStatusUC.Dock = DockStyle.Fill;
            _senderStatusUC.MouseMoveInGridEvent += new MouseOperateInGridEventHandler(SenderStatusUC_MouseMoveInGridEvent);
            _customToolTipFont = this.Font;
        }

        /// <summary>
        /// 串口名称字体
        /// </summary>
        public Font CommPortFont
        {
            get
            {
                if (_senderStatusUC != null)
                {
                    return _senderStatusUC.CommPortFont;
                }
                return this.Font;
            }
            set
            {
                if (_senderStatusUC != null)
                {
                    _senderStatusUC.CommPortFont = value;
                }
            }
        }
        /// <summary>
        /// 发送卡序号的字体
        /// </summary>
        public Font SenderNumFont
        {
            get
            {
                if (_senderStatusUC != null)
                {
                    return _senderStatusUC.SenderNumFont;
                }
                return this.Font;
            }
            set
            {
                if (_senderStatusUC != null)
                {
                    _senderStatusUC.SenderNumFont = value;
                }
            }
        }
        /// <summary>
        /// CustomToolTip显示的字体
        /// </summary>
        public Font CustomToolTipFont
        {
            get
            {
                if (_customToolTip != null)
                {
                    return _customToolTip.TipContentFont;
                }
                return _customToolTipFont;
            }
            set
            {
                _customToolTipFont = value;
                if (_customToolTip != null)
                {
                    _customToolTip.TipContentFont = value;
                }
            }
        }
        /// <summary>
        /// 刷新串口连接的发送卡信息
        /// </summary>
        /// <param name="curAllSenderStatusDic">串口连接的发送卡DVI状态，key值为串口名称，value值为串口连接的发送卡数量</param>
        /// <param name="curAllSenderRefreshDic">串口连接的发送卡刷新率，key值为串口名称，value值为串口连接的发送卡数量</param>
        public void UpdateSenderStatus(SerializableDictionary<string, List<WorkStatusType>> curAllSenderStatusDic,
                                       SerializableDictionary<string, List<ValueInfo>> curAllSenderRefreshDic,
                                       SerializableDictionary<string, SerializableDictionary<int, SenderRedundantStateInfo>> redundantStateType,
                                       SerializableDictionary<string, int> commPortData,
            SerializableDictionary<string, List<SenderRedundancyInfo>> tempRedundancyDict)
        {
            _tempRedundancyDict = tempRedundancyDict;
            _senderStatusUC.UpdateSenderStatus(curAllSenderStatusDic, curAllSenderRefreshDic, redundantStateType, commPortData, tempRedundancyDict);
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void DisposeAllInfo()
        {
            if (_customToolTip != null)
            {
                _customToolTip.Close();
                _customToolTip.Dispose();
                _customToolTip = null;
            }

            if (_tempRedundancyDict != null)
            {
                _tempRedundancyDict.Clear();
                _tempRedundancyDict = null;
            }

            _senderStatusUC.MouseMoveInGridEvent -= new MouseOperateInGridEventHandler(SenderStatusUC_MouseMoveInGridEvent);
        }
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="hashTable"></param>
        public void UpdateLanguage(Hashtable hashTable)
        {
            LangHashTable = hashTable;
            label_OK.Text = GetLangText("label_OK", "正常");
            label_SecderError.Text = GetLangText("label_SecderError", "发送卡故障");
            label_DVIException.Text = GetLangText("label_DVIException", "DVI信号异常");
            label_Unknown.Text = GetLangText("label_Unknown", "未知");
        }

        /// <summary>
        /// 获取控件的Text字符串
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        private string GetLangText(string langName, string defaultText)
        {
            if (LangHashTable != null
             && LangHashTable.Contains(langName.ToLower()))
            {
                return (string)LangHashTable[langName.ToLower()];
            }
            return defaultText;
        }
        private void SenderStatusUC_MouseMoveInGridEvent(object sender, MouseOperateInGridEventArgs e)
        {
            if (_customToolTip == null)
            {
                _customToolTip = new CustomToolTip();
                _customToolTip.Owner = this.ParentForm;
                _customToolTip.TipContentFont = _customToolTipFont;
            }
            if (e.SenderGridInfo == null)
            {
                _customToolTip.SetTipInfo(_senderStatusUC.DrawPanel, null);
                return;
            }
            List<string> noticeStrList = new List<string>();
            noticeStrList.Add(GetLangText("CommPortName", "串口") + ":" + e.SenderGridInfo.CommPort);
            noticeStrList.Add(GetLangText("SenderName", "发送卡") + ":" + (e.SenderGridInfo.SenderIndex + 1).ToString());
            if (e.SenderGridInfo.Status == WorkStatusType.OK)
            {
                noticeStrList.Add(GetLangText("DVISignalOK", "DVI信号:正常"));
                //DVI信号正常时显示发送卡的刷新率
                string refreshReteStr = GetLangText("RefreshRate", "刷新率:");
                if (!e.SenderGridInfo.RefreshRateInfo.IsValid)
                {
                    refreshReteStr += GetLangText("label_Unknown", "未知");
                }
                else
                {
                    refreshReteStr += e.SenderGridInfo.RefreshRateInfo.Value.ToString() + " HZ";
                }
                noticeStrList.Add(refreshReteStr);
            }
            else if (e.SenderGridInfo.Status == WorkStatusType.Error)
            {
                noticeStrList.Add(GetLangText("DVISignalException", "DVI信号:异常"));
            }
            else if (e.SenderGridInfo.Status == WorkStatusType.Unknown)
            {
                noticeStrList.Add(GetLangText("DVISignalUnkonwn", "DVI信号:未知"));
            }
			else if (e.SenderGridInfo.Status == WorkStatusType.SenderCardError)
            {
                noticeStrList.Add(GetLangText("SenderCardUnkonwn", "发送卡:发送卡故障"));
			}
            string msg = "";
            string portMsg = "";
            if (_tempRedundancyDict != null && _tempRedundancyDict.Count > 0
                && _tempRedundancyDict.ContainsKey(e.SenderGridInfo.CommPort))
            {
                if (_tempRedundancyDict[e.SenderGridInfo.CommPort] != null &&
                    _tempRedundancyDict[e.SenderGridInfo.CommPort].Count > 0)
                {
                    foreach (int i in e.SenderGridInfo.RedundantStateTypeList.Keys)
                    {
                        RedundantStateType state = e.SenderGridInfo.RedundantStateTypeList[i];
                        if (state == RedundantStateType.OK)
                        {
                            portMsg = "";
                            msg = GetLangText("PortNormal", "网口:正常");
                        }
                        else if (state == RedundantStateType.Error)
                        {
                            if (_tempRedundancyDict.ContainsKey(e.SenderGridInfo.CommPort))
                            {
                                for (int j = 0; j < _tempRedundancyDict[e.SenderGridInfo.CommPort].Count; j++)
                                {
                                    SenderRedundancyInfo senderInfo = _tempRedundancyDict[e.SenderGridInfo.CommPort][j];
                                    if (senderInfo.SlaveSenderIndex == e.SenderGridInfo.SenderIndex && senderInfo.SlavePortIndex == i)
                                    {
                                        portMsg = "-" + GetLangText("MasterSenderPort", "主网口:发送卡") + (senderInfo.MasterSenderIndex + 1) + "-" + GetLangText("Port", "网口") + (senderInfo.MasterPortIndex + 1);
                                        msg = GetLangText("PortRedundant", "网口:冗余");
                                        break;
                                    }
                                    else
                                    {
                                        msg = GetLangText("PortNormal", "网口:正常");
                                    }
                                }
                            }
                        }
                        else if (state == RedundantStateType.Unknown)
                        {
                            portMsg = "";
                            msg = GetLangText("PortUnknown", "网口:未知");
                        }
                        msg = (i + 1) + msg + portMsg;
                        noticeStrList.Add(msg);
                    }
                }
            }
            _customToolTip.SetTipInfo(_senderStatusUC.DrawPanel, noticeStrList);
            if (!_customToolTip.TopLevel)
            {
                _customToolTip.TopLevel = true;
            }
        }

        private void UC_SenderStatus_Load(object sender, EventArgs e)
        {

        }

        private void UC_SenderStatus_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
