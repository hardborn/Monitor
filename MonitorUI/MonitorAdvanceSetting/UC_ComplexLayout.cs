using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Nova.Monitoring.UI.MonitorSetting
{
    public partial class UC_ComplexLayout : UserControl
    {
        private enum ColType
        {
            IsConnect = 0,
            CommPort,
            SenderIndex,
            PortIndex,
            ScanBoardIndex,
            Count
        }
        private static string[] CountArray = null;
        private static byte ColumnWidth = 60;
        private static string[] ColHeaderText = new string[] { "连接", "串口", "发送卡", "网口", "接收卡", "数量"};
        private SettingCommInfo _commonInfo;
        private List<byte> _countList = null;
        private List<string> _addrList = null;

        public DataGridViewSelectedRowCollection SelectedItemRowsItems
        {
            get { return dbfDataGridView_ComplexLayout.SelectedRows; }
        }

        public Font MonitorInfoListFont
        {
            get { return dbfDataGridView_ComplexLayout.DefaultCellStyle.Font; }
            set
            {
                dbfDataGridView_ComplexLayout.DefaultCellStyle.Font = value;
                dbfDataGridView_ComplexLayout.ColumnHeadersDefaultCellStyle.Font = value;
                dbfDataGridView_ComplexLayout.Refresh();
            }
        }
        /// <summary>
        /// 设置复杂显示屏的监控信息
        /// </summary>
        public SettingMonitorCntEventHandler SetComplexScreenInfEvent = null;

        /// <summary>
        /// 构造复杂显示屏监控信息设置页面
        /// </summary>
        public UC_ComplexLayout(SettingCommInfo commInfo)
        {
            InitializeComponent();
            _countList = new List<byte>();
            _addrList = new List<string>();

            try
            {
                _commonInfo = (SettingCommInfo)commInfo.Clone();
            }
            catch
            {
                _commonInfo = new SettingCommInfo();
            }
            CountArray = new string[_commonInfo.MaxCount];  //最大个数加上4，然后再加未选中时的个数0
            for (int i = 0; i < _commonInfo.MaxCount; i++)
            {
                CountArray[i] = (i + 1).ToString();
            }
            InitDataGridView();
        }
        /// <summary>
        /// 载入窗口语言资源
        /// </summary>
        public void UpdateLanguage()
        {
            ColHeaderText[0] = Frm_FanPowerAdvanceSetting.GetLangControlText("ConnectStr", ColHeaderText[0]);
            ColHeaderText[1] = Frm_FanPowerAdvanceSetting.GetLangControlText("CommPortName", ColHeaderText[1]);
            ColHeaderText[2] = Frm_FanPowerAdvanceSetting.GetLangControlText("SenderName", ColHeaderText[2]);

            ColHeaderText[3] = Frm_FanPowerAdvanceSetting.GetLangControlText("PortName", ColHeaderText[3]);
            ColHeaderText[4] = Frm_FanPowerAdvanceSetting.GetLangControlText("ScanBoardName", ColHeaderText[4]);
            ColHeaderText[5] = Frm_FanPowerAdvanceSetting.GetLangControlText("Count", ColHeaderText[5]);

            dbfDataGridView_ComplexLayout.Columns[0].HeaderText = ColHeaderText[0] + _commonInfo.TypeStr;
            dbfDataGridView_ComplexLayout.Columns[1].HeaderText = ColHeaderText[1];

            dbfDataGridView_ComplexLayout.Columns[2].HeaderText = ColHeaderText[2];
            dbfDataGridView_ComplexLayout.Columns[3].HeaderText = ColHeaderText[3];
            dbfDataGridView_ComplexLayout.Columns[4].HeaderText = ColHeaderText[4];
            dbfDataGridView_ComplexLayout.Columns[5].HeaderText = ColHeaderText[5];
        }
        /// <summary>
        /// 添加一个接收卡的监控信息
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="info"></param>
        public void AddOneMonitorCardInf(string addr, byte count)
        {
            _countList.Add(count);
            _addrList.Add(addr);
            dbfDataGridView_ComplexLayout.RowCount++;
            int nRowIndex = dbfDataGridView_ComplexLayout.RowCount - 1;
            bool isConnect = false;
            string countStr = CountArray[0];
            if (count > 0)
            {
                isConnect = true;
                countStr = count.ToString();
            }

            string commPort = "";
            byte senderIndex = 0;
            byte portIndex = 0;
            UInt16 scanBoardIndex = 0;
            StaticFunction.GetPerAddr(addr, out commPort, out senderIndex, out portIndex, out scanBoardIndex);
            dbfDataGridView_ComplexLayout.Rows[nRowIndex].SetValues(new object[] 
                                                                            { 
                                                                            isConnect,
                                                                            commPort,
                                                                            (senderIndex + 1).ToString(),
                                                                            (portIndex + 1).ToString(), 
                                                                            (scanBoardIndex + 1).ToString(),
                                                                            countStr
                                                                            });

            DataGridViewDisableComboBoxCell comBoxCell = dbfDataGridView_ComplexLayout[(int)ColType.Count, nRowIndex] as DataGridViewDisableComboBoxCell; ;
            if (isConnect)
            {
                comBoxCell.Enabled = true;
            }
            else
            {
                comBoxCell.Enabled = false;
            }
            this.Refresh();
        }
        /// <summary>
        /// 恢复所有默认个数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool ResumeDefaultCount(byte count)
        {
            if (count > CountArray.Length)
            {
                return false;
            }
            DataGridViewDisableComboBoxCell comBoxCell = null;
            bool isConnect = false;
            int nIndex = 0;
            if (count > 0)
            {
                isConnect = true;
            }
            else
            {
                isConnect = false;
            }

            for (int i = 0; i < dbfDataGridView_ComplexLayout.Rows.Count; i++)
            {
                comBoxCell = dbfDataGridView_ComplexLayout[(int)ColType.Count, i] as DataGridViewDisableComboBoxCell;
                _countList[i] = count;
                if (isConnect)
                {
                    comBoxCell.Value = count.ToString();
                }
                comBoxCell.Enabled = isConnect;
                dbfDataGridView_ComplexLayout[(int)ColType.IsConnect, i].Value = isConnect;
                if (SetComplexScreenInfEvent != null)
                {
                    SetComplexScreenInfEvent.Invoke(_addrList[nIndex], _countList[nIndex]);
                }
            }
            dbfDataGridView_ComplexLayout.Refresh();
            return true;
        }
        /// <summary>
        /// 设置选中项的个数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool SetCountForSelectedItems(byte count)
        {
            if (count > CountArray.Length)
            {
                return false;
            }
            DataGridViewDisableComboBoxCell comBoxCell = null;
            bool isConnect = false;
            int nIndex = 0;
            if (count > 0)
            {
                isConnect = true;
            }
            else
            {
                isConnect = false;
            }
            for (int i = 0; i < dbfDataGridView_ComplexLayout.SelectedRows.Count; i++)
            {
                nIndex = dbfDataGridView_ComplexLayout.SelectedRows[i].Index;
                comBoxCell = dbfDataGridView_ComplexLayout[(int)ColType.Count, nIndex] as DataGridViewDisableComboBoxCell;
                _countList[nIndex] = count;
                if (isConnect)
                {
                    comBoxCell.Value = count.ToString();
                }
                comBoxCell.Enabled = isConnect;
                dbfDataGridView_ComplexLayout[(int)ColType.IsConnect, nIndex].Value = isConnect;
                if (SetComplexScreenInfEvent != null)
                {
                    SetComplexScreenInfEvent.Invoke(_addrList[nIndex], _countList[nIndex]);
                }
            }
            dbfDataGridView_ComplexLayout.Refresh();
            return true;
        }
        /// <summary>
        /// 获取指定行的个数
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool GetCountByRowIndex(int rowIndex, out byte count)
        {
            count = 0;
            if (rowIndex >= 0 && rowIndex < dbfDataGridView_ComplexLayout.RowCount)
            {
                count = _countList[rowIndex];
                return true;
            }
            return false;
        }

        private void InitDataGridView()
        {
            DataGridViewCheckBoxColumn Column0 = new DataGridViewCheckBoxColumn();
            Column0.DataPropertyName = ColType.IsConnect.ToString();
            Column0.Name = ColType.IsConnect.ToString();
            Column0.HeaderText = ColHeaderText[0] + _commonInfo.TypeStr;
            Column0.Width = ColumnWidth;
            Column0.SortMode = DataGridViewColumnSortMode.NotSortable;
            dbfDataGridView_ComplexLayout.Columns.Add(Column0);

            DataGridViewTextBoxColumn Column1 = new DataGridViewTextBoxColumn();
            Column1.DataPropertyName = ColType.CommPort.ToString();
            Column1.Name = ColType.CommPort.ToString();
            Column1.HeaderText = ColHeaderText[1];
            Column1.Width = ColumnWidth;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.ReadOnly = true;
            dbfDataGridView_ComplexLayout.Columns.Add(Column1);

            DataGridViewTextBoxColumn Column2 = new DataGridViewTextBoxColumn();
            Column2.DataPropertyName = ColType.SenderIndex.ToString();
            Column2.Name = ColType.SenderIndex.ToString();
            Column2.HeaderText = ColHeaderText[2];
            Column2.Width = ColumnWidth;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.ReadOnly = true;
            dbfDataGridView_ComplexLayout.Columns.Add(Column2);


            DataGridViewTextBoxColumn Column3 = new DataGridViewTextBoxColumn();
            Column3.DataPropertyName = ColType.PortIndex.ToString();
            Column3.Name = ColType.PortIndex.ToString();
            Column3.HeaderText = ColHeaderText[3];
            Column3.Width = ColumnWidth;
            Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column3.ReadOnly = true;
            dbfDataGridView_ComplexLayout.Columns.Add(Column3);


            DataGridViewTextBoxColumn Column4 = new DataGridViewTextBoxColumn();
            Column4.DataPropertyName = ColType.ScanBoardIndex.ToString();
            Column4.Name = ColType.ScanBoardIndex.ToString();
            Column4.HeaderText = ColHeaderText[4];
            Column4.Width = ColumnWidth;
            Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column4.ReadOnly = true;
            dbfDataGridView_ComplexLayout.Columns.Add(Column4);

            DataGridViewDisableComboBoxColumn Column5 = new DataGridViewDisableComboBoxColumn();
            Column5.DataPropertyName = ColType.Count.ToString();
            Column5.Name = ColType.Count.ToString();
            Column5.HeaderText = ColHeaderText[5];
            Column5.DataSource = CountArray;
            Column5.Width = ColumnWidth;
            Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column5.ReadOnly = false;
            Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dbfDataGridView_ComplexLayout.Columns.Add(Column5);

            dbfDataGridView_ComplexLayout.AutoGenerateColumns = false;
        }
        private void DealWithModifyCellValue(int curRowIndex, int curColIndex)
        {
            DataGridViewDisableComboBoxCell comBoxCell = null;
            if (curColIndex == (int)ColType.IsConnect)
            {
                comBoxCell = dbfDataGridView_ComplexLayout[(int)ColType.Count, curRowIndex] as DataGridViewDisableComboBoxCell;
                if ((bool)dbfDataGridView_ComplexLayout[(int)ColType.IsConnect, curRowIndex].Value)
                {
                    dbfDataGridView_ComplexLayout[(int)ColType.IsConnect, curRowIndex].Value = false;
                    comBoxCell.Enabled = false;
                    _countList[curRowIndex] = 0;
                }
                else
                {
                    dbfDataGridView_ComplexLayout[(int)ColType.IsConnect, curRowIndex].Value = true;
                    comBoxCell.Enabled = true;
                    _countList[curRowIndex] = Convert.ToByte(comBoxCell.Value);
                }
                if (SetComplexScreenInfEvent != null)
                {
                    SetComplexScreenInfEvent.Invoke(_addrList[curRowIndex], _countList[curRowIndex]);
                }
            }
            else if (curColIndex == (int)ColType.Count)
            {
                comBoxCell = dbfDataGridView_ComplexLayout[(int)ColType.Count, curRowIndex] as DataGridViewDisableComboBoxCell; ;
                _countList[curRowIndex] = Convert.ToByte(comBoxCell.Value);
                if (SetComplexScreenInfEvent != null)
                {
                    SetComplexScreenInfEvent.Invoke(_addrList[curRowIndex], _countList[curRowIndex]);
                }
            }
        }

        private void UC_ComplexLayout_Load(object sender, EventArgs e)
        {
            UpdateLanguage();
        }

        private void dbfDataGridView_ComplexLayout_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dbfDataGridView_ComplexLayout.ColumnCount)
            {
                return;
            }
            if (e.RowIndex < 0 || e.RowIndex >= dbfDataGridView_ComplexLayout.RowCount)
            {
                return;
            }

            DealWithModifyCellValue(e.RowIndex, e.ColumnIndex);
            this.Refresh();
        }

        private void dbfDataGridView_ComplexLayout_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dbfDataGridView_ComplexLayout.ColumnCount)
            {
                return;
            }
            if (e.RowIndex < 0 || e.RowIndex >= dbfDataGridView_ComplexLayout.RowCount)
            {
                return;
            }
            DealWithModifyCellValue(e.RowIndex, e.ColumnIndex);
            this.Refresh();
        }

        private void dbfDataGridView_ComplexLayout_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dbfDataGridView_ComplexLayout.ColumnCount)
            {
                return;
            }
            if (e.RowIndex < 0 || e.RowIndex >= dbfDataGridView_ComplexLayout.RowCount)
            {
                return;
            }
            if (e.ColumnIndex == (int)ColType.Count)
            {
                DataGridViewDisableComboBoxCell comBoxCell = null;
                comBoxCell = dbfDataGridView_ComplexLayout[(int)ColType.Count, e.RowIndex] as DataGridViewDisableComboBoxCell; ;
                _countList[e.RowIndex] = Convert.ToByte(comBoxCell.Value);
                if (SetComplexScreenInfEvent != null)
                {
                    SetComplexScreenInfEvent.Invoke(_addrList[e.RowIndex], _countList[e.RowIndex]);
                }
            }
        }
    }
    public class DataGridViewDisableComboBoxColumn : DataGridViewComboBoxColumn
    {
        public DataGridViewDisableComboBoxColumn()
        {
            this.CellTemplate = new DataGridViewDisableComboBoxCell();
        }
    }

    public class DataGridViewDisableComboBoxCell : DataGridViewComboBoxCell
    {
        private bool enabledValue;
        public bool Enabled
        {
            get
            {
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        public override object Clone()
        {
            DataGridViewDisableComboBoxCell cell =
                (DataGridViewDisableComboBoxCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }


        public DataGridViewDisableComboBoxCell()
        {
            this.enabledValue = true;
        }

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // The comBox cell is disabled, so paint the border,  
            // background, and disabled comBox for the cell.
            if (!this.enabledValue)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground =
                        new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) ==
                    DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                                advancedBorderStyle);
                }

                // Calculate the area in which to draw the comBox.
                Rectangle comBoxArea = cellBounds;
                Rectangle comBoxAdjustment =
                    this.BorderWidths(advancedBorderStyle);
                comBoxArea.X += comBoxAdjustment.X;
                comBoxArea.Y += comBoxAdjustment.Y;
                comBoxArea.Height -= comBoxAdjustment.Height;
                comBoxArea.Width -= comBoxAdjustment.Width;

                // Draw the disabled comBox.                
                ComboBoxRenderer.DrawDropDownButton(graphics, comBoxArea, ComboBoxState.Disabled);
            }
            else
            {
                // The comBox cell is enabled, so let the base class 
                // handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, "", errorText,
                    cellStyle, advancedBorderStyle, paintParts);


                // Draw the disabled comBox text. 如果"Text"属性为空字符串则显示默认的Formatvalue,否则显示text
                if (this.Value == null)
                {
                    if (this.FormattedValue is String)
                    {
                        TextRenderer.DrawText(graphics,
                            (string)this.FormattedValue,
                            this.DataGridView.Font,
                            cellBounds, SystemColors.GrayText);
                    }
                }
                else
                {
                    if (this.Value is String)
                    {
                        TextRenderer.DrawText(graphics,
                            (string)this.Value,
                            this.DataGridView.Font,
                            cellBounds, SystemColors.ControlText);
                    }
                }
            }
        }
    }
}
