using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Nova.Control;
using Nova.LCT.GigabitSystem.Common;
using Nova.Windows.Forms;
using Nova.Xml.Serialization;

namespace Nova.Monitoring.UI.MonitorSetting
{
    public partial class UC_OneScreenLayout : UserControl
    {
        private CustomToolTip _customToolTip = null;
        private SettingMonitorCntEventHandler _setOneScanBoardInfoEvent = null;
        private Frm_SetInfo _setInfoFrm = null;
        private UC_StandarAndSimpleLayout _standarAndSimpleLayout = null;
        private UC_ComplexLayout _complexLayout = null;
        private ILEDDisplayInfo _curLedInf = null;
        // 当前显示屏类型
        private LEDDisplyType _ledType = LEDDisplyType.SimpleSingleType;
        private string _commPort = "";
        private Dictionary<string, byte> _curSettingDic = null;

        private ScanBoardRegionInfo _scanBoardTempInfo;
        private Rectangle _tempRect = Rectangle.Empty;
        private string _tempAddr = "";
        private byte _tempCnt;
        private SetCustomObjInfo _tempCustomInfo = null;

        private SettingCommInfo _commonInfo;
        #region 字体相关
        private Font _customToolTipFont = null;
        private Font _simpleOrStandardScreenFont = null;
        private Font _complexScreenFont = null;
        #endregion


        /// <summary>
        /// 格子的选中背景颜色
        /// </summary>
        public Color GridSelectedBackColor
        {
            get 
            {
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultSelectedStyle.BackColor;
                }
                return Color.Empty;
            }
            set 
            { 
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultSelectedStyle.BackColor = value;
                    if (_standarAndSimpleLayout.GridDic != null)
                    {
                        foreach (string addr in _standarAndSimpleLayout.GridDic.Keys)
                        {
                            _standarAndSimpleLayout.GridDic[addr].SelectStyle.BackColor = value;
                        }
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultSelectedStyle.BoardColor;
                }
                return Color.Empty;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultSelectedStyle.BoardColor = value;
                    if (_standarAndSimpleLayout.GridDic != null)
                    {
                        foreach (string addr in _standarAndSimpleLayout.GridDic.Keys)
                        {
                            _standarAndSimpleLayout.GridDic[addr].SelectStyle.BoardColor = value;
                        }
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultStyle.BoardColor;
                }
                return Color.Empty;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultStyle.BoardColor = value;
                    if (_standarAndSimpleLayout.GridDic != null)
                    {
                        foreach (string addr in _standarAndSimpleLayout.GridDic.Keys)
                        {
                            _standarAndSimpleLayout.GridDic[addr].Style.BoardColor = value;
                        }
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultStyle.BackColor;
                }
                return Color.Empty;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultStyle.BackColor = value;
                    if (_standarAndSimpleLayout.GridDic != null)
                    {
                        foreach (string addr in _standarAndSimpleLayout.GridDic.Keys)
                        {
                            _standarAndSimpleLayout.GridDic[addr].Style.BackColor = value;
                        }
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultFocusStyle.BackColor;
                }
                return Color.Empty;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultFocusStyle.BackColor = value;
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultFocusStyle.BoardColor;
                }
                return Color.Empty;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultFocusStyle.BoardColor = value;
                    _standarAndSimpleLayout.InvalidateDrawArea();
                }
            }
        }

        public Color SelectedZoonBorderColor
        {
            get
            {
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.SelectedZoneColorByMove;
                }
                return Color.Empty;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.SelectedZoneColorByMove = value;
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultFocusStyle.BoardWidth;
                }
                return 0;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultFocusStyle.BoardWidth = value;
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultSelectedStyle.BoardWidth;
                }
                return 0;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultSelectedStyle.BoardWidth = value; 
                    if (_standarAndSimpleLayout.GridDic != null)
                    {
                        foreach (string addr in _standarAndSimpleLayout.GridDic.Keys)
                        {
                            _standarAndSimpleLayout.GridDic[addr].SelectStyle.BoardWidth = value;
                        }
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
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
                if (_standarAndSimpleLayout != null)
                {
                    return _standarAndSimpleLayout.DefaultStyle.BoardWidth;
                }
                return 0;
            }
            set
            {
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultStyle.BoardWidth = value;
                    if (_standarAndSimpleLayout.GridDic != null)
                    {
                        foreach (string addr in _standarAndSimpleLayout.GridDic.Keys)
                        {
                            _standarAndSimpleLayout.GridDic[addr].Style.BoardWidth = value;
                        }
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
                }
            }
        }
        /// <summary>
        /// 简单屏或标准屏的显示字体
        /// </summary>
        public Font SimpleOrStandardScreenFont
        {
            get { return _simpleOrStandardScreenFont; }
            set
            {
                _simpleOrStandardScreenFont = value;
                if (_standarAndSimpleLayout != null)
                {
                    _standarAndSimpleLayout.DefaultFocusStyle.GridFont = value;
                    _standarAndSimpleLayout.DefaultSelectedStyle.GridFont = value;
                    _standarAndSimpleLayout.DefaultStyle.GridFont = value;

                    foreach (string key in this._standarAndSimpleLayout.GridDic.Keys)
                    {
                        _standarAndSimpleLayout.GridDic[key].Style.GridFont = value;
                        _standarAndSimpleLayout.GridDic[key].SelectStyle.GridFont = value;
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
                }
            }
        }
        /// <summary>
        /// 复杂屏的显示字体
        /// </summary>
        public Font ComplexScreenFont
        {
            get { return _complexScreenFont; }
            set
            {
                _complexScreenFont = value;
                if (_complexLayout != null)
                {
                    _complexLayout.MonitorInfoListFont = value;
                }
            }
        }
        /// <summary>
        /// 提示信息的字体
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


        public UC_OneScreenLayout(string commPort, ILEDDisplayInfo ledDisplayInfo,
                                  Dictionary<string, byte> curAllSettingDic,
                                  SettingCommInfo commInfo, 
                                  SettingMonitorCntEventHandler SetOneScanBoardInfoEvent)
        {
            InitializeComponent();
            this.Disposed += new EventHandler(UC_OneScreenLayout_Disposed);

            _commPort = commPort;
            _curLedInf = ledDisplayInfo;
            _curSettingDic = curAllSettingDic;

            _customToolTipFont = this.Font;
            _complexScreenFont = this.Font;
            _simpleOrStandardScreenFont = this.Font;
            try
            {
                _commonInfo = (SettingCommInfo)commInfo.Clone();
            }
            catch
            {
                _commonInfo = new SettingCommInfo();
            }
            _setOneScanBoardInfoEvent = SetOneScanBoardInfoEvent;

            _ledType = _curLedInf.Type;
            if (_ledType == LEDDisplyType.ComplexType)
            {
                _complexLayout = new UC_ComplexLayout(_commonInfo);
                _complexLayout.Parent = doubleBufferPanel_SettingZoon;
                _complexLayout.Dock = DockStyle.Fill;
                _complexLayout.SetComplexScreenInfEvent += new SettingMonitorCntEventHandler(ComplexScreenSetInfo); 
                groupBox_ScalingRate.Visible = false;
            }
            else
            {
                _standarAndSimpleLayout = new UC_StandarAndSimpleLayout(_commonInfo);
                _standarAndSimpleLayout.DefaultFocusStyle.BackColor = Color.Wheat;
                _standarAndSimpleLayout.IsCanSelect = true;
                _standarAndSimpleLayout.IsCanSelectMoreGrid = true;
                _standarAndSimpleLayout.Parent = doubleBufferPanel_SettingZoon;
                _standarAndSimpleLayout.Dock = DockStyle.Fill;
                _standarAndSimpleLayout.BorderStyle = BorderStyle.FixedSingle;
                _standarAndSimpleLayout.BackColor = Color.Transparent;
                _standarAndSimpleLayout.ClearAllRectangularGrid();
                _standarAndSimpleLayout.ContextMenuStrip = contextMenuStrip_Set;
                _standarAndSimpleLayout.GridMouseMove += new RectangularGridMouseEventHandler(StandarAndSimpleLayout_GridMouseMove);

                _standarAndSimpleLayout.GridMouseDoubleClick += new RectangularGridMouseEventHandler(StandarAndSimpleLayout_GridMouseDoubleClick);

                groupBox_ScalingRate.Visible = true;
            }
            UpdateDisplay();
            if (_standarAndSimpleLayout != null)
            {
                vScrollBar_PixelLength_Scroll(null, null);
            }
        }



        private void UpdateDisplay()
        {
            if (_ledType == LEDDisplyType.ComplexType)
            {
                for (int i = 0; i < _curLedInf.ScannerCount; i++)
                {
                    _scanBoardTempInfo = (ScanBoardRegionInfo)_curLedInf[i].Clone();
                    _tempAddr = StaticFunction.GetSBAddr(_commPort,
                                                         _scanBoardTempInfo.SenderIndex,
                                                         _scanBoardTempInfo.PortIndex,
                                                         _scanBoardTempInfo.ConnectIndex);

                    if (!_curSettingDic.ContainsKey(_tempAddr))
                    {
                        _curSettingDic.Add(_tempAddr, _commonInfo.SameCount);
                        if (_setOneScanBoardInfoEvent != null)
                        {
                            _setOneScanBoardInfoEvent.Invoke(_tempAddr, _commonInfo.SameCount);
                        }
                    }
                    _complexLayout.AddOneMonitorCardInf(_tempAddr, _curSettingDic[_tempAddr]);
                }
            }
            else
            {
                Point screenLocation = _curLedInf.GetScreenPosition();
                for (int i = 0; i < _curLedInf.ScannerCount; i++)
                {
                    if(_curLedInf[i] == null || _curLedInf[i].SenderIndex == 255)
                    {
                        continue;
                    }
                    _scanBoardTempInfo = (ScanBoardRegionInfo)_curLedInf[i].Clone();
                    _tempAddr = StaticFunction.GetSBAddr(_commPort,
                                                         _scanBoardTempInfo.SenderIndex,
                                                          _scanBoardTempInfo.PortIndex,
                                                          _scanBoardTempInfo.ConnectIndex);
                    _tempRect = new Rectangle(_scanBoardTempInfo.X - screenLocation.X,
                                              _scanBoardTempInfo.Y - screenLocation.Y,
                                              _scanBoardTempInfo.Width, 
                                              _scanBoardTempInfo.Height);

                    if (!_curSettingDic.ContainsKey(_tempAddr))
                    {
                        _curSettingDic.Add(_tempAddr, _commonInfo.SameCount);

                        if (_setOneScanBoardInfoEvent != null)
                        {
                            _setOneScanBoardInfoEvent.Invoke(_tempAddr, _commonInfo.SameCount);
                        }
                    }
                    _tempCustomInfo = new SetCustomObjInfo();
                    _tempCustomInfo.ScanBordInfo = _scanBoardTempInfo;
                    _tempCustomInfo.Count = _curSettingDic[_tempAddr];
                    _standarAndSimpleLayout.AddRectangularGrid(_tempRect, _tempAddr, _tempCustomInfo);
                }
                _standarAndSimpleLayout.InvalidateDrawArea();
            }
        }

        private void ComplexScreenSetInfo(string addr, byte data)
        {
            if (_setOneScanBoardInfoEvent != null)
            {
                _setOneScanBoardInfoEvent.Invoke(addr, data);
            }
        }

        private void UC_OneScreenLayout_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= new EventHandler(UC_OneScreenLayout_Disposed);

            if(_customToolTip != null)
            {
                _customToolTip.Close();
                _customToolTip.Dispose();
                _customToolTip = null;
            }

            if (_standarAndSimpleLayout != null)
            {
                _standarAndSimpleLayout.GridMouseMove -= new RectangularGridMouseEventHandler(StandarAndSimpleLayout_GridMouseMove);
                _standarAndSimpleLayout.GridMouseDoubleClick -= new RectangularGridMouseEventHandler(StandarAndSimpleLayout_GridMouseDoubleClick);
            }
            if (_complexLayout != null)
            {
                _complexLayout.SetComplexScreenInfEvent -= new SettingMonitorCntEventHandler(ComplexScreenSetInfo); 
            }
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StandarAndSimpleLayout_GridMouseMove(object sender, RectangularGridMouseEventArgs e)
        {
            if (_customToolTip == null)
            {
                _customToolTip = new CustomToolTip();
                _customToolTip.Owner = this.ParentForm;
                _customToolTip.TipContentFont = _customToolTipFont;
            }
            if (e.GridInfo == null)
            {
                //当前鼠标移动到的区域无矩形格子
                _customToolTip.SetTipInfo(_standarAndSimpleLayout.DrawPanel, null);
                Debug.WriteLine("进入格子的GridMouseMove事件，并获取的格子信息为空");
                return;
            }

            RectangularGridInfo scanBordGridInfo = _standarAndSimpleLayout[e.GridInfo.Key];
            List<string> noticeStrList = null;
            if (scanBordGridInfo != null)
            {
                _tempCustomInfo = (SetCustomObjInfo)scanBordGridInfo.CustomObj;
                if (_tempCustomInfo == null)
                {
                    _customToolTip.SetTipInfo(_standarAndSimpleLayout.DrawPanel, null);
                    return;
                }
                noticeStrList = new List<string>();
                _tempCnt = _tempCustomInfo.Count;

                noticeStrList.Add(StaticValue.SenderName + ":" + (_tempCustomInfo.ScanBordInfo.SenderIndex + 1));
                noticeStrList.Add(StaticValue.PortName + ":" + (_tempCustomInfo.ScanBordInfo.PortIndex + 1));
                noticeStrList.Add(StaticValue.ScanBoardName + ":" + (_tempCustomInfo.ScanBordInfo.ConnectIndex + 1));
                noticeStrList.Add("(X, Y):(" + e.GridInfo.Region.X + "," + e.GridInfo.Region.Y + ")");
                noticeStrList.Add("(W, H):(" + e.GridInfo.Region.Width + "," + e.GridInfo.Region.Height + ")");
                noticeStrList.Add(StaticValue.CountStr + ":" + _tempCustomInfo.Count);
            }

            _customToolTip.SetTipInfo(_standarAndSimpleLayout.DrawPanel, noticeStrList);
            if (!_customToolTip.TopLevel)
            {
                _customToolTip.TopLevel = true;
            }
        }

        private void StandarAndSimpleLayout_GridMouseDoubleClick(object sender, RectangularGridMouseEventArgs e)
        {
            if (e.GridInfo == null)
            {
                return;
            }
            _tempCustomInfo = (SetCustomObjInfo)e.GridInfo.CustomObj;
            if (_tempCustomInfo == null)
            {
                return;
            }
            _scanBoardTempInfo = (ScanBoardRegionInfo)_tempCustomInfo.ScanBordInfo.Clone();
            _tempCnt = _tempCustomInfo.Count;

            _setInfoFrm = new Frm_SetInfo(_commonInfo.TypeStr, _tempCnt, _commonInfo.MaxCount);

            if (_setInfoFrm.ShowDialog() == DialogResult.OK)
            {
                _tempCustomInfo.Count = _setInfoFrm.Count;


                _tempAddr = StaticFunction.GetSBAddr(_commPort,
                                                     _scanBoardTempInfo.SenderIndex,
                                                      _scanBoardTempInfo.PortIndex,
                                                      _scanBoardTempInfo.ConnectIndex);

                _standarAndSimpleLayout.GridDic[_tempAddr].CustomObj = _tempCustomInfo;
                _curSettingDic[_tempAddr] = _setInfoFrm.Count;

                if (_setOneScanBoardInfoEvent != null)
                {
                    _setOneScanBoardInfoEvent.Invoke(_tempAddr, _setInfoFrm.Count);
                }
                _standarAndSimpleLayout.InvalidateDrawArea();
            }
        }

        private void vScrollBar_PixelLength_Scroll(object sender, ScrollEventArgs e)
        {
            _standarAndSimpleLayout.LengthForOnePixel = vScrollBar_PixelLength.Value / 100.0f;
            label_Value.Text = (vScrollBar_PixelLength.Value / 100.0f).ToString("f2");
        }

        private void crystalButton_SetSelect_Click(object sender, EventArgs e)
        {
            if (_ledType == LEDDisplyType.ComplexType)
            {
                if (_complexLayout.SelectedItemRowsItems.Count <= 0)
                {
                    string info = Frm_FanPowerAdvanceSetting.GetLangControlText("PleaseSelectScanBoard", "请选中需要设置的接收卡!");
                    CustomMessageBox.ShowTopMostCustomMessageBox(this.ParentForm, info, "",
                                                          MessageBoxButtons.OK, MessageBoxIconType.Alert);
                    return;
                }
                if (_complexLayout.SelectedItemRowsItems.Count == 1)
                {
                    bool bGetRes = _complexLayout.GetCountByRowIndex(_complexLayout.SelectedItemRowsItems[0].Index, 
                                                                     out _tempCnt);
                }
                else
                {
                    _tempCnt = _commonInfo.SameCount;
                }

                _setInfoFrm = new Frm_SetInfo(_commonInfo.TypeStr, _tempCnt, _commonInfo.MaxCount);

                if (_setInfoFrm.ShowDialog() == DialogResult.OK)
                {
                    _complexLayout.SetCountForSelectedItems(_setInfoFrm.Count);
                }
            }
            else
            {
                if (_standarAndSimpleLayout.SelectedGrid == null
                   || _standarAndSimpleLayout.SelectedGrid.Count <= 0)
                {
                    string info = Frm_FanPowerAdvanceSetting.GetLangControlText("PleaseSelectScanBoard", "请选中需要设置的接收卡!");
                    CustomMessageBox.ShowTopMostCustomMessageBox(this.ParentForm, info, "",
                                                          MessageBoxButtons.OK, MessageBoxIconType.Alert);
                    return;
                }
                if (_standarAndSimpleLayout.SelectedGrid.Count == 1)
                {
                    _tempCustomInfo = (SetCustomObjInfo)_standarAndSimpleLayout.SelectedGrid[0].CustomObj;
                    if (_tempCustomInfo == null)
                    {
                        return;
                    }
                    _tempCnt = _tempCustomInfo.Count;
                }
                else
                {
                    _tempCnt = _commonInfo.SameCount;
                }

                _setInfoFrm = new Frm_SetInfo(_commonInfo.TypeStr, _tempCnt, _commonInfo.MaxCount);

                if (_setInfoFrm.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < _standarAndSimpleLayout.SelectedGrid.Count; i++)
                    {
                        _tempCustomInfo = (SetCustomObjInfo)_standarAndSimpleLayout.SelectedGrid[i].CustomObj;
                        if (_tempCustomInfo == null)
                        {
                            continue;
                        }
                        _scanBoardTempInfo = (ScanBoardRegionInfo)_tempCustomInfo.ScanBordInfo.Clone();
                        _tempCustomInfo.Count = _setInfoFrm.Count;
                        _standarAndSimpleLayout.SelectedGrid[i].CustomObj = _tempCustomInfo;

                        _tempAddr = StaticFunction.GetSBAddr(_commPort,
                                                              _scanBoardTempInfo.SenderIndex,
                                                            _scanBoardTempInfo.PortIndex,
                                                            _scanBoardTempInfo.ConnectIndex);

                        _curSettingDic[_tempAddr] = _setInfoFrm.Count;

                        if (_setOneScanBoardInfoEvent != null)
                        {
                            _setOneScanBoardInfoEvent.Invoke(_tempAddr, _setInfoFrm.Count);
                        }
                    }
                    _standarAndSimpleLayout.InvalidateDrawArea();
                }
            }
        }

        private void crystalButton_Resume_Click(object sender, EventArgs e)
        {
            string info = Frm_FanPowerAdvanceSetting.GetLangControlText("WhetherSetDefaultValue",
                                                                "确认需要将当前显示屏的监控信息设置为默认值?");
            if (CustomMessageBox.ShowTopMostCustomMessageBox(this.ParentForm, info, "",
                                                  MessageBoxButtons.YesNo, MessageBoxIconType.Question) == DialogResult.No)
            {
                return;
            }

            if (_ledType == LEDDisplyType.ComplexType)
            {
                _complexLayout.ResumeDefaultCount(_commonInfo.SameCount);
            }
            else
            {
                for (int i = 0; i < _curLedInf.ScannerCount; i++)
                {
                    _scanBoardTempInfo = (ScanBoardRegionInfo)_curLedInf[i].Clone();
                    _tempAddr = StaticFunction.GetSBAddr(_commPort,
                                                         _scanBoardTempInfo.SenderIndex,
                                                          _scanBoardTempInfo.PortIndex,
                                                          _scanBoardTempInfo.ConnectIndex);

                    _tempCustomInfo = new SetCustomObjInfo();
                    _tempCustomInfo.Count = _commonInfo.SameCount;
                    _tempCustomInfo.ScanBordInfo = _scanBoardTempInfo;

                    _standarAndSimpleLayout.GridDic[_tempAddr].CustomObj = _tempCustomInfo;

                    _curSettingDic[_tempAddr] = _commonInfo.SameCount;

                    if (_setOneScanBoardInfoEvent != null)
                    {
                        _setOneScanBoardInfoEvent.Invoke(_tempAddr, _commonInfo.SameCount);
                    }
                }
                _standarAndSimpleLayout.InvalidateDrawArea();
            }
        }

        private void ToolStripMenuItem_Set_Click(object sender, EventArgs e)
        {
            crystalButton_SetSelect_Click(null, null);
        }
    }
}
