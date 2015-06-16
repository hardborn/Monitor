using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;
using Nova.Control.DoubleBufferControl;
using Nova.Xml.Serialization;

namespace Nova.Monitoring.UI.SenderStatusDisplay
{
    public partial class UC_SenderStatusLayout : UserControl
    {
        #region 静态变量
        private int CommPortLineLength = 70;
        private Pen CommPortLinePen = null;
        private Brush CommPortStrBrush = null;
        private Pen CommPortLine = null;

        private int SenderConnectLineLength = 20;
        private Pen SenderBoarderAndLinePen = null;
        private Brush SenderBrush = null;
        private Brush SenderIndexBrush = null;

        private int BoundOffsetPixel = 10;
        private int SenderSizeOffset = 10;
        private int OneCommPortInfoImagePixel = 50;
        #endregion

        private Font _senderStrFont = null;
        private Font _commPortFont = null;
        private Size _drawSize = Size.Empty;
        private int _lastMouseMoveX = 0;
        private int _lastMouseMoveY = 0;
        private SerializableDictionary<string, List<WorkStatusType>> _curAllSenderStatusDic = null;
        private SerializableDictionary<string, List<SenderInfo>> _curAllSenderGridDic = null;
        private SerializableDictionary<string, List<ValueInfo>> _curAllSenderRefreshDic = null;
        private SerializableDictionary<string, SerializableDictionary<int, SenderRedundantStateInfo>> _redundantStateType = null;
        private SerializableDictionary<string, List<SenderRedundancyInfo>> _tempRedundancyDict = null;
        private SerializableDictionary<string, int> _commPortData = null;
        /// <summary>
        /// 鼠标在格子上移动时触发的事件
        /// </summary>
        public event MouseOperateInGridEventHandler MouseMoveInGridEvent = null;
        /// <summary>
        /// 格子绘制Panel
        /// </summary>
        public DoubleBufferPanel DrawPanel
        {
            get { return doubleBufferPanel_Draw; }
        }
        /// <summary>
        /// 串口名称字体
        /// </summary>
        public Font CommPortFont
        {
            get { return _commPortFont; }
            set 
            { 
                _commPortFont = value;
                this.doubleBufferPanel_Draw.Refresh();
            }
        }
        /// <summary>
        /// 发送卡序号的字体
        /// </summary>
        public Font SenderNumFont
        {
            get { return _senderStrFont; }
            set
            {
                _senderStrFont = value;
                this.doubleBufferPanel_Draw.Refresh();
            }
        }
        /// <summary>
        /// 构造发送卡状态拓扑图绘制UC
        /// </summary>
        public UC_SenderStatusLayout()
        {
            InitializeComponent();

            CommPortStrBrush = new SolidBrush(Color.Black);
            SenderIndexBrush = new SolidBrush(Color.Black);
            CommPortLinePen = new Pen(Color.Blue, 3.0f);
            CommPortLine = new Pen(Color.Black, 1.0f);
            SenderBoarderAndLinePen = new Pen(Color.Black, 1.0f);

            _senderStrFont = new Font("宋体", 12, FontStyle.Regular);
            _commPortFont = new Font("宋体", 14, FontStyle.Regular);

            _curAllSenderGridDic = new SerializableDictionary<string, List<SenderInfo>>();
        }

        /// <summary>
        /// 刷新串口连接的发送卡
        /// </summary>
        /// <param name="curAllSenderStatusDic">串口连接的发送卡DVI状态，key值为串口名称，value值为串口连接的发送卡数量</param>
        /// <param name="curAllSenderRefreshDic">串口连接的发送卡刷新率，key值为串口名称，value值为串口连接的发送卡数量</param>
        public void UpdateSenderStatus(SerializableDictionary<string, List<WorkStatusType>> curAllSenderStatusDic,
                                       SerializableDictionary<string, List<ValueInfo>> curAllSenderRefreshDic,
                                      SerializableDictionary<string, SerializableDictionary<int, SenderRedundantStateInfo>> redundantStateType,
                                       SerializableDictionary<string, int> commPortData,
            SerializableDictionary<string, List<SenderRedundancyInfo>> tempRedundancyDict)
        {
            _curAllSenderStatusDic = curAllSenderStatusDic;
            _curAllSenderRefreshDic = curAllSenderRefreshDic;
            _redundantStateType = redundantStateType;
            _commPortData = commPortData;
            _tempRedundancyDict = tempRedundancyDict;

            this.doubleBufferPanel_Draw.Refresh();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        private void ResourceDispose()
        {
            if (_curAllSenderStatusDic != null)
            {
                _curAllSenderStatusDic.Clear();
                _curAllSenderStatusDic = null;
            }
            if (_curAllSenderRefreshDic != null)
            {
                _curAllSenderRefreshDic.Clear();
                _curAllSenderRefreshDic = null;
            }
            if (_curAllSenderGridDic != null)
            {
                _curAllSenderGridDic.Clear();
                _curAllSenderGridDic = null;
            }
            if(_redundantStateType!=null)
            {
                _redundantStateType.Clear();
                _redundantStateType = null;
            }
            if(_tempRedundancyDict!=null)
            {
                _tempRedundancyDict.Clear();
                _tempRedundancyDict = null;
            }
            if (_commPortData != null)
            {
                _commPortData.Clear();
                _commPortData = null;
            }

            if (CommPortLinePen != null)
            {
                CommPortLinePen.Dispose();
            }

            if (CommPortStrBrush != null)
            {
                CommPortStrBrush.Dispose();
            }
            if (CommPortLine != null)
            {
                CommPortLine.Dispose();
            }
            if (SenderBoarderAndLinePen != null)
            {
                SenderBoarderAndLinePen.Dispose();
            }
            if (SenderBrush != null)
            {
                SenderBrush.Dispose();
            }
            if (SenderIndexBrush != null)
            {
                SenderIndexBrush.Dispose();
            }
        }

        /// <summary>
        /// 获取需要绘制的区域的大小
        /// </summary>
        private void GetDrawMaxSize()
        {
            Size maxSize = Size.Empty;

            int curWidth = 0;
            int nCurHeight = 0;
            int nSenderCnt = 0;
            if (_curAllSenderStatusDic == null)
            {
                return;
            }
            foreach (string commPort in _curAllSenderStatusDic.Keys)
            {
                if (_curAllSenderStatusDic[commPort] == null
                    && _curAllSenderStatusDic[commPort].Count <= 0)
                {
                    continue;
                }
                nSenderCnt = _curAllSenderStatusDic[commPort].Count;
                curWidth = nSenderCnt * (OneCommPortInfoImagePixel - SenderSizeOffset)
                          + nSenderCnt * SenderConnectLineLength
                          + CommPortLineLength + OneCommPortInfoImagePixel + BoundOffsetPixel;

                nCurHeight = OneCommPortInfoImagePixel + BoundOffsetPixel;

                maxSize.Width = Math.Max(maxSize.Width, curWidth) + BoundOffsetPixel;
                maxSize.Height += nCurHeight;
            }
            maxSize.Height += nCurHeight + BoundOffsetPixel;
            _drawSize = maxSize;
            if (_drawSize.Width <= 0)
            {
                _drawSize.Width = 5;
            }
            if (_drawSize.Height <= 0)
            {
                _drawSize.Width = 5;
            }
            this.doubleBufferPanel_Draw.Size = _drawSize;
        }
        /// <summary>
        /// 获取指定未知所在的格子信息
        /// </summary>
        /// <param name="pixelX"></param>
        /// <param name="pixelY"></param>
        /// <param name="gridInfo"></param>
        private void GetGridInfoByPos(int pixelX, int pixelY, out SenderInfo gridInfo)
        {
            gridInfo = null;
            if (_curAllSenderGridDic == null)
            {
                return;
            }
            foreach(string commPort in _curAllSenderGridDic.Keys)
            {
                if (_curAllSenderGridDic[commPort] == null)
                {
                    continue;
                }
                for (int i = 0; i < _curAllSenderGridDic[commPort].Count; i++)
                {
                    if (_curAllSenderGridDic[commPort][i] == null)
                    {
                        continue;
                    }
                    if (_curAllSenderGridDic[commPort][i].SenderRect.Contains(new Point(pixelX, pixelY)))
                    {
                        gridInfo = (SenderInfo)(_curAllSenderGridDic[commPort][i].Clone());
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 绘制一个串口的发送卡
        /// </summary>
        /// <param name="drawX">绘制的X</param>
        /// <param name="drawY">绘制的Y</param>
        /// <param name="commPort">串口名称</param>
        /// <param name="drawGraphics">绘制Graphics</param>
        private void DrawOneCommPortSender(int drawX, int drawY, string commPort, Graphics drawGraphics)
        {
            if (_curAllSenderStatusDic == null
               || !_curAllSenderStatusDic.ContainsKey(commPort)
               || _curAllSenderStatusDic[commPort] == null
               || _curAllSenderStatusDic[commPort].Count <= 0)
            {
                return;
            }
            Point lineStart = Point.Empty;
            Point lineEnd = Point.Empty;

            drawGraphics.DrawImage(Properties.Resources.CommPort, drawX, drawY, OneCommPortInfoImagePixel, OneCommPortInfoImagePixel);
            
            lineStart.X = drawX + OneCommPortInfoImagePixel;
            lineStart.Y = drawY + OneCommPortInfoImagePixel / 2;
            lineEnd.X = lineStart.X + CommPortLineLength;
            lineEnd.Y = lineStart.Y;
            drawGraphics.DrawLine(CommPortLinePen, lineStart, lineEnd);

            SizeF stringSizeF = drawGraphics.MeasureString(commPort, _commPortFont);
            drawGraphics.DrawString(commPort, _commPortFont, CommPortStrBrush, new PointF(lineStart.X, lineStart.Y - stringSizeF.Height));

            Rectangle senderRect = new Rectangle(lineEnd.X, drawY + SenderSizeOffset / 2,
                                                 OneCommPortInfoImagePixel - SenderSizeOffset,
                                                 OneCommPortInfoImagePixel - SenderSizeOffset);

            int senderCnt = _curAllSenderStatusDic[commPort].Count;
            string indexStr = "";
            PointF strPointF;
            SenderInfo senderGridInfo = null;
            List<SenderInfo> senderGridInfoList = new List<SenderInfo>();
            WorkStatusType status = WorkStatusType.Unknown;
            ValueInfo tempRefreshReate;
            for (byte i = 0; i < senderCnt; i++)
            {
                if (i > 0)
                {
                    senderRect.X += SenderConnectLineLength + OneCommPortInfoImagePixel - SenderSizeOffset;
                }
                status = _curAllSenderStatusDic[commPort][i];
                if (status == WorkStatusType.OK)
                {
                    SenderBrush = new SolidBrush(Color.Green);
                }
                else if (status == WorkStatusType.Error)
                {
                    SenderBrush = new SolidBrush(Color.Yellow);
                }
                else if (status == WorkStatusType.SenderCardError)
                {
                    SenderBrush = new SolidBrush(Color.DarkOrange);
                }
                else
                {
                    SenderBrush = new SolidBrush(Color.Gray);
                }
                tempRefreshReate = new ValueInfo();
                if (_curAllSenderRefreshDic == null
                    || !_curAllSenderRefreshDic.ContainsKey(commPort)
                    || _curAllSenderRefreshDic[commPort] == null
                    || i >= _curAllSenderRefreshDic[commPort].Count)
                {
                    tempRefreshReate.IsValid = false;
                }
                else
                {
                    tempRefreshReate.IsValid = _curAllSenderRefreshDic[commPort][i].IsValid;
                    tempRefreshReate.Value = _curAllSenderRefreshDic[commPort][i].Value;
                }
                drawGraphics.FillRectangle(SenderBrush, senderRect);
                drawGraphics.DrawRectangle(SenderBoarderAndLinePen, senderRect);

                indexStr = (i + 1).ToString();
                stringSizeF = drawGraphics.MeasureString(indexStr, _senderStrFont);

                strPointF = new PointF(senderRect.X + (senderRect.Width - stringSizeF.Width) / 2, 
                                       senderRect.Y +(senderRect.Height - stringSizeF.Height) / 2);
                drawGraphics.DrawString(indexStr, _senderStrFont, SenderIndexBrush, strPointF);
                RedundantStateType rState = RedundantStateType.Unknown;
                float weightCount = senderRect.Width;
                if (_commPortData.ContainsKey(commPort) && _commPortData[commPort] > 0)
                {
                    weightCount = senderRect.Width / (float)_commPortData[commPort];
                }
                float heightCount = (float)senderRect.Width / 4 * 3;
                PointF newPoint = senderRect.Location;
                PointF startPoint=new PointF(0,0);
                PointF endPoint = new PointF(0, 0);
                PointF[] pointList=new PointF[4];
                int step = 0;

                int masterSender = SenderConnectLineLength + OneCommPortInfoImagePixel - SenderSizeOffset;
                if (_tempRedundancyDict != null && _tempRedundancyDict.ContainsKey(commPort) &&
                    _tempRedundancyDict[commPort] != null && i<_tempRedundancyDict[commPort].Count
                    && _commPortData.ContainsKey(commPort) && _redundantStateType.ContainsKey(commPort)
                    && _redundantStateType[commPort] != null && _redundantStateType[commPort].Count>0)
                {
                    for (int j = 0; j < _commPortData[commPort]; j++)
                    {
                        if (j >= _redundantStateType[commPort][i].RedundantStateTypeList.Count)
                        {
                            rState = RedundantStateType.Unknown;
                        }
                        else
                        {
                            rState = _redundantStateType[commPort][i].RedundantStateTypeList[j];
                        }
                        if (rState == RedundantStateType.OK)
                        {
                            SenderBrush = new SolidBrush(Color.Green);
                            CommPortLine.Color = Color.Green;
                            step += 4;
                        }
                        else if (rState == RedundantStateType.Error)
                        {
                            CommPortLine.Color = Color.Red;
                            if (_tempRedundancyDict.ContainsKey(commPort))
                            {
                                for (int k = 0; k < _tempRedundancyDict[commPort].Count; k++)
                                {
                                    SenderRedundancyInfo senderInfo = _tempRedundancyDict[commPort][k];
                                    if (senderInfo.SlaveSenderIndex == i && senderInfo.SlavePortIndex == j)
                                    {
                                        SenderBrush = new SolidBrush(Color.Yellow);
                                        break;

                                    }
                                    else
                                    {
                                        SenderBrush = new SolidBrush(Color.Green);
                                    }
                                }
                            }
                            step += 4;
                        }
                        else
                        {
                            CommPortLine.Color = Color.Gray;
                            SenderBrush = new SolidBrush(Color.Gray);
                            step += 4;
                        }
                        RectangleF portRect = new RectangleF(newPoint.X + weightCount * j, newPoint.Y + heightCount, weightCount, senderRect.Height / 4);

                        drawGraphics.FillRectangle(SenderBrush, portRect);
                        if (_tempRedundancyDict.ContainsKey(commPort))
                        {
                            for (int k = 0; k < _tempRedundancyDict[commPort].Count; k++)
                            {
                                SenderRedundancyInfo senderInfo = _tempRedundancyDict[commPort][k];
                                if (senderInfo.SlaveSenderIndex == i && senderInfo.SlavePortIndex == j)
                                {
                                    pointList[0] = new PointF(newPoint.X + weightCount * j + weightCount / 2, newPoint.Y + senderRect.Height);
                                    pointList[1] = new PointF(newPoint.X + weightCount * j + weightCount / 2, newPoint.Y + senderRect.Height + step);
                                    if (senderInfo.MasterSenderIndex > senderInfo.SlaveSenderIndex)
                                    {
                                        pointList[2] = new PointF(newPoint.X + masterSender + weightCount * senderInfo.MasterPortIndex + weightCount / 2, newPoint.Y + senderRect.Height + step);
                                        pointList[3] = new PointF(newPoint.X + masterSender + weightCount * senderInfo.MasterPortIndex + weightCount / 2, newPoint.Y + senderRect.Height);
                                    }
                                    else if (senderInfo.MasterSenderIndex < senderInfo.SlaveSenderIndex)
                                    {
                                        pointList[2] = new PointF(newPoint.X - masterSender + weightCount * senderInfo.MasterPortIndex + weightCount / 2, newPoint.Y + senderRect.Height + step);
                                        pointList[3] = new PointF(newPoint.X - masterSender + weightCount * senderInfo.MasterPortIndex + weightCount / 2, newPoint.Y + senderRect.Height);
                                    }
                                    else
                                    {
                                        pointList[2] = new PointF(newPoint.X + weightCount * senderInfo.MasterPortIndex + weightCount / 2, newPoint.Y + senderRect.Height + step);
                                        pointList[3] = new PointF(newPoint.X + weightCount * senderInfo.MasterPortIndex + weightCount / 2, newPoint.Y + senderRect.Height);
                                    }
                                    drawGraphics.DrawLines(CommPortLine, pointList);

                                    if (rState == RedundantStateType.Error)
                                    {
                                        PointF centerPoint = new PointF(pointList[2].X + (pointList[1].X - pointList[2].X) / 2, pointList[1].Y);
                                        startPoint = new PointF(centerPoint.X - 4, centerPoint.Y - 4);
                                        endPoint = new PointF(centerPoint.X + 4, centerPoint.Y + 4);
                                        drawGraphics.DrawLine(CommPortLine, startPoint, endPoint);

                                        startPoint = new PointF(centerPoint.X - 4, centerPoint.Y + 4);
                                        endPoint = new PointF(centerPoint.X + 4, centerPoint.Y - 4);
                                        drawGraphics.DrawLine(CommPortLine, startPoint, endPoint);
                                    }

                                    break;
                                }
                            }
                        }
                    }
                    CommPortLine.Color = Color.Black;
                    for (int j = 1; j < _commPortData[commPort]; j++)
                    {
                        startPoint = new PointF(newPoint.X + weightCount * j, newPoint.Y + heightCount);
                        endPoint = new PointF(newPoint.X + weightCount * j, newPoint.Y + senderRect.Height);
                        drawGraphics.DrawLine(CommPortLine, startPoint, endPoint);

                    }

                    startPoint = new PointF(newPoint.X, newPoint.Y + heightCount);
                    endPoint = new PointF(newPoint.X + senderRect.Width, newPoint.Y + heightCount);
                    drawGraphics.DrawLine(CommPortLine, startPoint, endPoint);
                }
                if (i < senderCnt - 1)
                {
                    lineStart.X = senderRect.Right;
                    lineStart.Y = drawY + OneCommPortInfoImagePixel / 2;

                    lineEnd.X = senderRect.Right + SenderConnectLineLength;
                    lineEnd.Y = lineStart.Y;

                    drawGraphics.DrawLine(SenderBoarderAndLinePen, lineStart, lineEnd);
                }
                senderGridInfo = new SenderInfo();
                senderGridInfo.CommPort = commPort;
                senderGridInfo.SenderIndex = i;
                senderGridInfo.SenderRect = senderRect;
                senderGridInfo.Status = status;
                senderGridInfo.RefreshRateInfo = tempRefreshReate;
                if (_commPortData.ContainsKey(commPort) && _redundantStateType.ContainsKey(commPort) &&
                    i < _redundantStateType[commPort].Count)
                {
                    for (int j = 0; j < _commPortData[commPort]; j++)
                    {
                        if (j < _redundantStateType[commPort][i].RedundantStateTypeList.Count)
                        {
                            senderGridInfo.RedundantStateTypeList.Add(j, _redundantStateType[commPort][i].RedundantStateTypeList[j]);
                        }
                    }
                }
                senderGridInfoList.Add(senderGridInfo);
            }
            _curAllSenderGridDic.Add(commPort, senderGridInfoList);
        }

        private void doubleBufferPanel_Draw_Paint(object sender, PaintEventArgs e)
        {
            _curAllSenderGridDic.Clear();
            if (_curAllSenderStatusDic == null)
            {
                return;
            }
            GetDrawMaxSize();
            int nIndex = 0;
            foreach (string commPort in _curAllSenderStatusDic.Keys)
            {
                DrawOneCommPortSender(BoundOffsetPixel, (BoundOffsetPixel + OneCommPortInfoImagePixel) * nIndex + BoundOffsetPixel,
                                      commPort, e.Graphics);
                nIndex++;
            }
        }

        private void doubleBufferPanel_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if ((this._lastMouseMoveX != e.X) || (this._lastMouseMoveY != e.Y))
                {
                    this._lastMouseMoveX = e.X;
                    this._lastMouseMoveY = e.Y;
                    if (MouseMoveInGridEvent != null)
                    {
                        SenderInfo gridInfo = null;
                        GetGridInfoByPos(e.X, e.Y, out gridInfo);
                        MouseMoveInGridEvent(sender, new MouseOperateInGridEventArgs(e, gridInfo));
                    }
                }
            }
            catch
            {
            }
        }
    }
}