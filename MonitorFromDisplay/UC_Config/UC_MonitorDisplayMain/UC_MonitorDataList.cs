using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;
using Nova.Monitoring.HardwareMonitorInterface;
using Nova.Monitoring.MonitorDataManager;
using Nova.Control;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_MonitorDataList : UserControl
    {
        private readonly int DefaultMinWidth = 50;
        private readonly int ColsCount = 12;
        private CustomToolTip _customToolTip = null;
        private bool _isFirst = true;

        public delegate void DataGridClickHandler(string sn, int colIndex);
        public event DataGridClickHandler DataGridClickEvent;

        public UC_MonitorDataList()
        {
            InitializeComponent();
            Initialize();
            dbDataGridView_MonitorInfo.MultiSelect = false;
        }
        public object DataSource
        {
            get
            {
                return dbDataGridView_MonitorInfo.DataSource;
            }
            set
            {
                SetDataSource(value);
            }
        }

        delegate void SetDataSourceDel(object value);
        private void SetDataSource(object value)
        {
            if (this.InvokeRequired)
            {
                SetDataSourceDel cs = new SetDataSourceDel(SetDataSource);
                this.Invoke(cs, new object[] { value });
            }
            else
            {
                dbDataGridView_MonitorInfo.DataSource = value;
            }
        }

        public void UpdateLanguage(string screenName)
        {
            dbDataGridView_MonitorInfo[0, 0].Value = screenName;
        }

        private void Initialize()
        {
            DataGridViewTextBoxColumn column0 = new DataGridViewTextBoxColumn();
            column0.DataPropertyName = "SNName";
            column0.HeaderText = StaticValue.DisplayTypeStr[0];
            column0.Name = "SNName";
            column0.ReadOnly = true;
            column0.DisplayIndex = 0;
            column0.Width = 215;
            column0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dbDataGridView_MonitorInfo.Columns.Add(column0);

            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("SenderDVI", StaticValue.DisplayTypeStr[1], true, 1, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("Status", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.SBStatus + 2],
                true, (int)MonitorDisplayType.SBStatus + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("Temperature", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.Temperature + 2],
                true, (int)MonitorDisplayType.Temperature + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("MCStatus", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.MCStatus + 2],
                true, (int)MonitorDisplayType.MCStatus + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("Humidity", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.Humidity + 2],
                true, (int)MonitorDisplayType.Humidity + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("SmokeWarn", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.Smoke + 2],
                true, (int)MonitorDisplayType.Smoke + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("FanSpeed", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.Fan + 2],
                true, (int)MonitorDisplayType.Fan + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("ValtageValue", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.Power + 2],
                true, (int)MonitorDisplayType.Power + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("RowLineStatus", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.RowLine + 2],
                true, (int)MonitorDisplayType.RowLine + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("GeneralStatus", StaticValue.DisplayTypeStr[(int)MonitorDisplayType.GeneralSwitch + 2],
                true, (int)MonitorDisplayType.GeneralSwitch + 2, 50));
            dbDataGridView_MonitorInfo.Columns.Add(
                GetDataGridViewImageColumn("CareStatus", StaticValue.DisplayTypeStr[11], true, 11, 50));

            DataGridViewTextBoxColumn column12 = new DataGridViewTextBoxColumn();
            column12.DataPropertyName = "SN";
            column12.HeaderText = StaticValue.DisplayTypeStr[0];
            column12.Name = "SN";
            column12.ReadOnly = true;
            column12.DisplayIndex = 0;
            column12.Width = 130;
            column12.Visible = false;
            dbDataGridView_MonitorInfo.Columns.Add(column12);

            dbDataGridView_MonitorInfo.AutoGenerateColumns = false;
            dbDataGridView_MonitorInfo.RowHeadersVisible = false;
            dbDataGridView_MonitorInfo.ColumnHeadersVisible = false;
            dbDataGridView_MonitorInfo.RowCount = 1;

            dbDataGridView_MonitorInfo.Rows[0].ReadOnly = true;
            dbDataGridView_MonitorInfo.Rows[0].Height = 35;
            dbDataGridView_MonitorInfo.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(236, 233, 216);
            dbDataGridView_MonitorInfo.Rows[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 233, 216);
            dbDataGridView_MonitorInfo.Columns[0].DefaultCellStyle.SelectionForeColor = Color.Black;
            dbDataGridView_MonitorInfo.Rows[0].SetValues(new object[]
                                                             { 
                                                                 StaticValue.DisplayTypeStr[0],
                                                                 Properties.Resources.SenderStatus,
                                                                 Properties.Resources.SBStatus,
                                                                 Properties.Resources.Temperature,
                                                                 Properties.Resources.MonitorCard,
                                                                 Properties.Resources.Humidity,
                                                                 Properties.Resources.Smoke,
                                                                 Properties.Resources.Fan,
                                                                 Properties.Resources.Power,
                                                                 Properties.Resources.Cabinet,
                                                                 Properties.Resources.Cabinet_Door,
                                                                 Properties.Resources.CareServer,
                                                                 StaticValue.DisplayTypeStr[0]
                                                             });
            dbDataGridView_MonitorInfo.Rows[0].Frozen = true;

            if (dbDataGridView_MonitorInfo.ColumnCount < ColsCount)
            {
                return;
            }
            int nWidth = (dbDataGridView_MonitorInfo.Width - dbDataGridView_MonitorInfo.RowHeadersWidth) / ColsCount;
            if (nWidth < DefaultMinWidth)
            {
                return;
            }
            for (int i = 0; i < ColsCount; i++)
            {
                dbDataGridView_MonitorInfo.Columns[i].Width = nWidth;
                dbDataGridView_MonitorInfo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private DataGridViewImageColumn GetDataGridViewImageColumn(string colName, string colText, bool colReadOnly,
            int disIndex, int colWidth)
        {
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            column.DataPropertyName = colName;
            column.Name = colName;
            column.HeaderText = colText;
            column.ReadOnly = colReadOnly;
            column.Width = colWidth;
            column.DisplayIndex = disIndex;
            //column.ImageLayout= DataGridViewImageCellLayout.Zoom;
            return column;
        }

        delegate void UpdateCurErrInfoHandler(List<MonitorDataFlag> monitorDataFlags);
        public void UpdateCurErrInfo(List<MonitorDataFlag> monitorDataFlags)
        {
            if (this.InvokeRequired)
            {
                UpdateCurErrInfoHandler cs = new UpdateCurErrInfoHandler(UpdateCurErrInfo);
                this.Invoke(cs, new object[] { monitorDataFlags });
                return;
            }

            if (monitorDataFlags == null || monitorDataFlags.Count == 0)
            {
                dbDataGridView_MonitorInfo.RowCount = 1;
                _isFirst = true;
                return;
            }
            dbDataGridView_MonitorInfo.RowCount = monitorDataFlags.Count + 1;

            for (int i = 0; i < monitorDataFlags.Count; i++)
            {
                dbDataGridView_MonitorInfo.Rows[i + 1].ReadOnly = true;
                dbDataGridView_MonitorInfo.Rows[i + 1].Height = 30;
                dbDataGridView_MonitorInfo.Rows[i + 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                dbDataGridView_MonitorInfo.Columns[0].DefaultCellStyle.SelectionBackColor = Color.AliceBlue;
                //dbDataGridView_MonitorInfo.Rows[i + 1].DefaultCellStyle.SelectionBackColor = Color.Blue;
                dbDataGridView_MonitorInfo.Rows[i + 1].SetValues(new object[]
                                                             { 
                                                                 monitorDataFlags[i].SNName,
                                                                 monitorDataFlags[i].IsSenderDVIValid,
                                                                 monitorDataFlags[i].IsSBStatusValid,
                                                                 monitorDataFlags[i].IsTemperatureValid,
                                                                monitorDataFlags[i].IsMCStatusValid,
                                                                monitorDataFlags[i].IsHumidityValid,
                                                                monitorDataFlags[i].IsSmokeValid,
                                                                monitorDataFlags[i].IsFanValid,
                                                                monitorDataFlags[i].IsPowerValid,
                                                                monitorDataFlags[i].IsRowLineValid,
                                                                monitorDataFlags[i].IsGeneralStatusValid,
                                                                monitorDataFlags[i].IsOnCareValid,
                                                                 monitorDataFlags[i].SN
                                                             });
            }
            if (DataGridClickEvent != null && _isFirst)
            {
                dbDataGridView_MonitorInfo[2, 1].Selected = true;
                DataGridClickEvent(dbDataGridView_MonitorInfo[12, 1].Value.ToString(), 2);
                _isFirst = false;
            }
        }
        public void SetSelectedCell(int rowIndex, int columnIndex)
        {
            if (rowIndex <= 0 || rowIndex >= dbDataGridView_MonitorInfo.Rows.Count || columnIndex <= 0 || columnIndex >= dbDataGridView_MonitorInfo.ColumnCount)
                return;
            dbDataGridView_MonitorInfo.Rows[rowIndex].Cells[columnIndex].Selected = true;
        }
        private void dbDataGridView_MonitorInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex <= 0 || e.ColumnIndex < 0 || e.Value == null)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                if (e.Value == dbDataGridView_MonitorInfo[12, e.RowIndex])
                {
                    string[] str = e.Value.ToString().Split('-');
                    e.Value = str[0] + "-" + (Convert.ToInt32(str[1], 16) + 1).ToString("00");
                }
                return;
            }
            DeviceWorkStatus deviceStatus = (DeviceWorkStatus)Enum.Parse(typeof(DeviceWorkStatus), e.Value.ToString());
            switch (e.ColumnIndex)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    switch (deviceStatus)
                    {
                        case DeviceWorkStatus.OK:
                            e.Value = Properties.Resources.OK;
                            break;
                        case DeviceWorkStatus.Error:
                            e.Value = Properties.Resources.Alarm;
                            break;
                        case DeviceWorkStatus.Unknown:
                            e.Value = Properties.Resources.Unknown;
                            break;
                        default:
                            e.Value = Properties.Resources.UnAvailable;
                            break;
                    }
                    break;
                case 11:
                    switch (deviceStatus)
                    {
                        case DeviceWorkStatus.OK:
                            e.Value = Properties.Resources.OnLine;
                            break;
                        case DeviceWorkStatus.Error:
                            e.Value = Properties.Resources.OffLine;
                            break;
                        default:
                            e.Value = Properties.Resources.NoRegister;
                            break;
                    }
                    break;
            }
        }

        private void dbDataGridView_MonitorInfo_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (_customToolTip != null)
            {
                _customToolTip.SetTipInfo(this.dbDataGridView_MonitorInfo, null);
            }
        }

        private void dbDataGridView_MonitorInfo_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            List<string> noticeStrList = new List<string>();
            if (_customToolTip == null)
            {
                _customToolTip = new CustomToolTip();
                _customToolTip.Owner = this.ParentForm;
                _customToolTip.TipContentFont = this.Font;
            }
            if (e.RowIndex == 0)
            {
                noticeStrList.Add(StaticValue.DisplayTypeStr[e.ColumnIndex]);
                _customToolTip.SetTipInfo(this.dbDataGridView_MonitorInfo, noticeStrList);
            }
            else if (e.RowIndex > 0 && e.ColumnIndex > 0
                && dbDataGridView_MonitorInfo[e.ColumnIndex, e.RowIndex].Value!=null 
                && dbDataGridView_MonitorInfo[e.ColumnIndex, e.RowIndex].Value.ToString() != "UnAvailable")
            {
                noticeStrList.Add(StaticValue.ColConTextTip);
                _customToolTip.SetTipInfo(this.dbDataGridView_MonitorInfo, noticeStrList);
            }
            else
            {
                _customToolTip.SetTipInfo(this.dbDataGridView_MonitorInfo, null);
            }
        }

        private void dbDataGridView_MonitorInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dbDataGridView_MonitorInfo.ColumnCount
                || e.RowIndex <= 0 || e.RowIndex >= dbDataGridView_MonitorInfo.RowCount)
            {
                return;
            }

            if (e.ColumnIndex < 11 && dbDataGridView_MonitorInfo[e.ColumnIndex, e.RowIndex].Value==null ||
                e.ColumnIndex < 11 && dbDataGridView_MonitorInfo[e.ColumnIndex, e.RowIndex].Value.ToString() == "UnAvailable")
            {
                return;
            }
            if (DataGridClickEvent != null)
            {
                DataGridClickEvent(dbDataGridView_MonitorInfo[12, e.RowIndex].Value.ToString(), e.ColumnIndex);
            }
        }

        private void UC_Closing()
        {
            if (_customToolTip != null)
            {
                _customToolTip.Close();
                _customToolTip.Dispose();
            }
        }
    }
}