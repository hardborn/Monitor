using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;
using Nova.Control;
using Nova.Xml.Serialization;
using Nova.Resource.Language;
using Nova.Windows.Forms;
using Nova.Drawing;

namespace Nova.Monitoring.UI.MonitorSetting
{
    public partial class Frm_FanPowerAdvanceSetting : Form
    {
        #region 静态字段
        public static Hashtable LangHashTable = null;
        public static string LanFileName = "";

        public static FontAdjuster AdjusterFontObj = new FontAdjuster();
        public static Font CurrentFont = null;
        #endregion

        #region 私有字
        private List<ILEDDisplayInfo> _oneLedInfos = null;
        private string _sn = string.Empty;
        private string _commPort = string.Empty;
        private SerializableDictionary<string, byte> _curConfigDic = null;
        private UC_OneScreenLayout _oneScreenInfo = null;
        private List<UC_OneScreenLayout> _scanBordLayoutInfoList = new List<UC_OneScreenLayout>();
        private SettingCommInfo _commonInfo;


        private Color _normalBackColor = Color.LightBlue;
        private Color _normalBorderColor = Color.Black;
        private Color _selectedBackColor = Color.White;
        private Color _selectedBorderColor = Color.Yellow;
        private Color _focusBackColor = Color.Wheat;
        private Color _focusBorderColor = Color.Green;
        private Color _selectedZoonBorderColor = Color.Red;
        private int _normalBorderWidth = 1;
        private int _selectedBorderWidth = 1;
        private int _focusBorderWidth = 1;

        #region 文件序列化和反序列化相关
        private static XmlSerializer XmlSerialize = null;
        private static Stream FileStream = null;
        private static XmlWriter XmlWriter = null;
        private static XmlReader XmlRead = null;
        #endregion
        #endregion

        
        /// <summary>
        /// 监控高级设置的数据存储变量属性, key值为串口号 + 发送卡序号 + 网口序号 + 接收卡序号
        /// </summary>
        public SerializableDictionary<string, byte> CurAllSettingDic
        {
            get { return _curConfigDic; }
        }
        /// <summary>
        /// 格子的选中背景颜色
        /// </summary>
        public Color GridSelectedBackColor
        {
            get
            {
                return _selectedBackColor;
            }
            set
            {
                _selectedBackColor = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridSelectedBackColor = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子的选中边界颜色
        /// </summary>
        public Color GridSelectedBorderColor
        {
            get
            {
                return _selectedBorderColor;
            }
            set
            {
                _selectedBorderColor = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridSelectedBorderColor = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子的正常显示边界颜色
        /// </summary>
        public Color GridNormalBorderColor
        {
            get
            {
                return _normalBorderColor;
            }
            set
            {
                _normalBorderColor = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridNormalBorderColor = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子的正常显示背景颜色
        /// </summary>
        public Color GridNormalBackColor
        {
            get
            {
                return _normalBackColor;
            }
            set
            {
                _normalBackColor = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridNormalBackColor = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子的聚焦背景颜色
        /// </summary>
        public Color GridFocusBackColor
        {
            get
            {
                return _focusBackColor;
            }
            set
            {
                _focusBackColor = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridFocusBackColor = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子的聚焦边界颜色
        /// </summary>
        public Color GridFocusBorderColor
        {
            get
            {
                return _focusBorderColor;
            }
            set
            {
                _focusBorderColor = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridFocusBorderColor = value;
                    }
                }
            }
        }
        /// <summary>
        /// 框选的边界颜色
        /// </summary>
        public Color SelectedZoonBorderColor
        {
            get
            {
                return _selectedZoonBorderColor;
            }
            set
            {
                _selectedZoonBorderColor = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].SelectedZoonBorderColor = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子聚焦边界线宽
        /// </summary>
        public int GridFocusBorderWidth
        {
            get
            {
                return _focusBorderWidth;
            }
            set
            {
                _focusBorderWidth = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridFocusBorderWidth = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子选中边界线宽
        /// </summary>
        public int GridSelectedBorderWidth
        {
            get
            {
                return _selectedBorderWidth;
            }
            set
            {
                _selectedBorderWidth = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridSelectedBorderWidth = value;
                    }
                }
            }
        }
        /// <summary>
        /// 格子正常显示的边界线宽
        /// </summary>
        public int GridNormalBorderWidth
        {
            get
            {
                return _normalBorderWidth;
            }
            set
            {
                _normalBorderWidth = value;
                if (_scanBordLayoutInfoList != null)
                {
                    for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                    {
                        _scanBordLayoutInfoList[i].GridNormalBorderWidth = value;
                    }
                }
            }
        }

        public Frm_FanPowerAdvanceSetting(List<ILEDDisplayInfo> oneLedInfos,string sn,string commPort,
                                          SerializableDictionary<string, byte> curAllSettingDic, 
                                          SettingCommInfo commInfo)
        {
            InitializeComponent(); 
            _oneLedInfos = oneLedInfos;
            _sn = sn;
            _commPort = commPort;
            _curConfigDic = new SerializableDictionary<string, byte>();
            if (curAllSettingDic != null)
            {
                foreach(string addr in curAllSettingDic.Keys)
                {
                    _curConfigDic.Add(addr, curAllSettingDic[addr]);
                }
            }
            try
            {
                _commonInfo = (SettingCommInfo)commInfo.Clone();
            }
            catch
            {
                _commonInfo = new SettingCommInfo();
            }
        }

        /// <summary>
        /// 载入语言资源
        /// </summary>
        /// <param name="langFileName"></param>
        /// <param name="proxyLangName"></param>
        /// <returns></returns>
        public bool UpdateLanguage(string langFileName)
        {
            LanFileName = langFileName;
            MultiLanguageUtils.UpdateLanguage(LanFileName, this);
            MultiLanguageUtils.ReadLanguageResource(LanFileName, "Frm_AdvanceSettingInfo", out LangHashTable);

            StaticValue.SenderName = Frm_FanPowerAdvanceSetting.GetLangControlText("SenderName", StaticValue.SenderName);
            StaticValue.PortName = Frm_FanPowerAdvanceSetting.GetLangControlText("PortName", StaticValue.PortName);

            StaticValue.ScanBoardName = Frm_FanPowerAdvanceSetting.GetLangControlText("ScanBoardName", StaticValue.ScanBoardName);
            StaticValue.CountStr = Frm_FanPowerAdvanceSetting.GetLangControlText("Count", StaticValue.CountStr);
            return true;
        }
        /// <summary>
        /// 更新窗口字体
        /// </summary>
        /// <param name="windowFont"></param>
        /// <returns></returns>
        public bool UpdateFont(Font windowFont)
        {
            CurrentFont = windowFont;
            AdjusterFontObj.Attach(this);
            AdjusterFontObj.UpdateFont(CurrentFont);
            if (_scanBordLayoutInfoList != null)
            {
                for (int i = 0; i < _scanBordLayoutInfoList.Count; i++)
                {
                    _scanBordLayoutInfoList[i].CustomToolTipFont = windowFont;
                    _scanBordLayoutInfoList[i].SimpleOrStandardScreenFont = windowFont;
                    _scanBordLayoutInfoList[i].ComplexScreenFont = windowFont;
                }
            }
            return true;
        }


        public static bool SerializeDictionary(string fileName, SerializableDictionary<string, byte> curSettingDic)
        {
            bool bRes = false;
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }
                XmlSerialize = new XmlSerializer(typeof(SerializableDictionary<string, byte>));

                FileStream = new FileStream(fileName, FileMode.Create);
                XmlWriter = new XmlTextWriter(FileStream, new UTF8Encoding());
                XmlSerialize.Serialize(XmlWriter, curSettingDic);

                bRes = true;
            }
            catch
            {

            }
            finally
            {
                if (XmlWriter != null)
                {
                    XmlWriter.Close();
                }
            }
            return bRes;
        }
        public static bool DeserializeDictionary(string fileName, out SerializableDictionary<string, byte> curSettingDic)
        {
            bool bRes = false;
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }
                XmlSerialize = new XmlSerializer(typeof(SerializableDictionary<string, byte>));

                FileStream = new FileStream(fileName, FileMode.Open);
                XmlRead = new XmlTextReader(FileStream);
                curSettingDic = (SerializableDictionary<string, byte>)XmlSerialize.Deserialize(XmlRead);

                bRes = true;
            }
            catch
            {
                curSettingDic = new SerializableDictionary<string, byte>();
            }
            finally
            {
                if (XmlRead != null)
                {
                    XmlRead.Close();
                }
            }
            return bRes;
        }
        /// <summary>
        /// 获取控件的Text字符串
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public static string GetLangControlText(string controlName, string defaultText)
        {
            if (LangHashTable != null
             && LangHashTable.Contains(controlName.ToLower()))
            {
                return (string)LangHashTable[controlName.ToLower()];
            }
            return defaultText;
        }

        /// <summary>
        /// 添加一个显示屏信息
        /// </summary>
        /// <param name="screenIndex"></param>
        private void AddOneScreenInf(string commPort, int pageIndex, string pageText, ILEDDisplayInfo ledInfo)
        {
            tabControl_ScreenLayout.TabPages.Add(pageText);
            if (ledInfo == null)
            {
                Label label = new Label();
                label.AutoSize = false;
                label.Text = label_Notice.Text;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Font = new Font("System", 14, FontStyle.Bold);
                label.BackColor = Color.AliceBlue;
                label.ForeColor = Color.Green;
                label.Parent = tabControl_ScreenLayout.TabPages[pageIndex];
                label.Dock = DockStyle.Fill;
            }
            else
            {
                _oneScreenInfo = new UC_OneScreenLayout(commPort, ledInfo, _curConfigDic, _commonInfo,
                                                       new SettingMonitorCntEventHandler(SetOneScanBoardInfoEvent));
                _oneScreenInfo.GridNormalBackColor = _normalBackColor;
                _oneScreenInfo.GridNormalBorderColor = _normalBorderColor;
                _oneScreenInfo.GridNormalBorderWidth = _normalBorderWidth;
                _oneScreenInfo.GridSelectedBackColor = _selectedBackColor;
                _oneScreenInfo.GridSelectedBorderColor = _selectedBorderColor;
                _oneScreenInfo.GridSelectedBorderWidth = _selectedBorderWidth;
                _oneScreenInfo.GridFocusBackColor = _focusBackColor;
                _oneScreenInfo.GridFocusBorderColor = _focusBorderColor;
                _oneScreenInfo.GridFocusBorderWidth = _focusBorderWidth;
                _oneScreenInfo.SelectedZoonBorderColor = _selectedZoonBorderColor;

                _oneScreenInfo.CustomToolTipFont = CurrentFont;
                _oneScreenInfo.SimpleOrStandardScreenFont = CurrentFont;
                _oneScreenInfo.ComplexScreenFont = CurrentFont;


                _scanBordLayoutInfoList.Add(_oneScreenInfo);
                int nCurScreenIndex = _scanBordLayoutInfoList.Count - 1;
                _scanBordLayoutInfoList[nCurScreenIndex].Parent = tabControl_ScreenLayout.TabPages[pageIndex];
                _scanBordLayoutInfoList[nCurScreenIndex].Dock = DockStyle.Fill;
            }
        }
        /// <summary>
        /// 设置一个接收卡的监控信息
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="data"></param>
        private void SetOneScanBoardInfoEvent(string addr, byte count)
        {
            _curConfigDic[addr] = count;
        }

        private void RemoveCountInfoForOther()
        {
            if (_oneLedInfos != null && _oneLedInfos.Count>0)
            {
                List<string> addrTempList = new List<string>();
                string addr = "";
                ScanBoardRegionInfo scanBoardTempInfo = null;
                foreach (ILEDDisplayInfo led in _oneLedInfos)
                {
                    if (led == null)
                    {
                        continue;
                    } 
                    for (int j = 0; j < led.ScannerCount; j++)
                    {
                        if (led[j] == null || led[j].SenderIndex == 255)
                        {
                            continue;
                        }
                        scanBoardTempInfo = (ScanBoardRegionInfo)led[j].Clone();
                        addr = StaticFunction.GetSBAddr(_commPort,
                                                        scanBoardTempInfo.SenderIndex,
                                                        scanBoardTempInfo.PortIndex,
                                                        scanBoardTempInfo.ConnectIndex);
                        addrTempList.Add(addr);
                    }
                }

                List<string> otherScanBoardAddrList = new List<string>();

                foreach (string key in _curConfigDic.Keys)
                {
                    if (!addrTempList.Contains(key))
                    {
                        otherScanBoardAddrList.Add(key);
                    }
                }

                for (int i = 0; i < otherScanBoardAddrList.Count; i++)
                {
                    _curConfigDic.Remove(otherScanBoardAddrList[i]);
                }
            }
        }
        private void Frm_AdvanceSetting_Load(object sender, EventArgs e)
        {
            if (_oneLedInfos != null && _oneLedInfos.Count > 0)
            {
                label_Notice.Visible = false;
                tabControl_ScreenLayout.TabPages.Clear();
                string pageName = "";
                int pageIndex = 0;
                foreach(ILEDDisplayInfo led in _oneLedInfos)
                {
                    if (led == null)
                    {
                        continue;
                    }
                    pageName = _sn;//GetLangControlText("ScreenName", "屏") + 
                    AddOneScreenInf(_commPort, pageIndex, pageName, led);
                    pageIndex++;
                }
            }
            else
            {
                label_Notice.Visible = true;

            }
            MultiLanguageUtils.UpdateLanguage(LanFileName, this);
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            RemoveCountInfoForOther();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void crystalButton_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}