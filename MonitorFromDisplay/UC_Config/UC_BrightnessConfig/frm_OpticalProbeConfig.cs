using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Monitoring.Common;
using Nova.Monitoring.MonitorDataManager;
using Nova.Resource.Language;
using Nova.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    public partial class frm_OpticalProbeConfig : Frm_CommonBase
    {
        private Hashtable _languageTable;
        private SmartLightConfigInfo _lightConfigInfo;
        private bool _isTestStart = false;

        public event Action<bool, List<PeripheralsLocation>> SensorTestEvent = null;

        public SmartLightConfigInfo LightConfigInfo
        {
            get { return _lightConfigInfo; }
        }
        public LightSensorConfigState ConfigState { get; set; }

        public AutoBrightExtendData BrightExtendData { get; set; }

        private List<BrightnessMapping> _brightnessMappingTable = new List<BrightnessMapping>();
        private List<UseablePeripheral> _useablePeripheral = new List<UseablePeripheral>();

        private Dictionary<int, UseablePeripheral> _internTable = new Dictionary<int, UseablePeripheral>();

        private bool _isSoftwareConfig;
        private delegate void SetLightProbeEventDel(object sender, EventArgs e);

        public frm_OpticalProbeConfig()
        {
            InitializeComponent();
            this.Load += frm_OpticalProbeConfig_Load;
            MonitorAllConfig.Instance().GetLightProbeEvent += frm_OpticalProbeConfig_GetLightProbeEvent;
            //MonitorAllConfig.Instance().BrightnessValueRefreshed += UC_BrightnessConfig_BrightnessValueRefreshed;
        }

        private void frm_OpticalProbeConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmClosed();
        }

        public frm_OpticalProbeConfig(bool isSoftwareConfig, SmartLightConfigInfo lightConfigInfo)
            : this()
        {
            _lightConfigInfo = lightConfigInfo;
            _isSoftwareConfig = isSoftwareConfig;

            if (_lightConfigInfo == null
                || _lightConfigInfo.DispaySoftWareConfig == null
                || _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting == null
                || _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo == null)
            {
                return;
            }
            if (_isSoftwareConfig)
            {
                enableCheckBox.Checked = _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.IsEnable;
                if (_lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue >= 0 ||
                    _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue <= 100)
                {
                    brightnessNumericUpDown.Value = _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue;
                }
            }
            else
            {
                enableCheckBox.Checked = _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.IsEnable;
                if (_lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue >= 0 ||
                    _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue <= 100)
                {
                    brightnessNumericUpDown.Value = _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue;
                }
            }
        }

        private void frm_OpticalProbeConfig_GetLightProbeEvent(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                SetLightProbeEventDel cs = new SetLightProbeEventDel(frm_OpticalProbeConfig_GetLightProbeEvent);
                this.Invoke(cs, new object[] { sender, e });
            }
            else
            {
                try
                {
                    RefreshOpticalProbeConfigView();
                    RefreshMappingConfigView();
                }
                catch (Exception ex)
                {
                    MonitorAllConfig.Instance().WriteLogToFile("ExistCatch:SenSor Read Rallback Failed:" + ex.ToString(), true);
                }
                finally
                {
                    CloseProcessForm();
                }
            }
        }

        private void RefreshMappingConfigView()
        {
            brightnessDataGridView.Rows.Clear();
            if (_isSoftwareConfig)
            {
                if (_lightConfigInfo == null || _lightConfigInfo.DispaySoftWareConfig == null || _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting == null)
                {
                    return;
                }

                if (_lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo != null)
                {
                    this.enableCheckBox.Checked = _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.IsEnable;
                    this.brightnessNumericUpDown.Value
                        = _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue < 0 ||
                        _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue > 100
                        ? 0 : _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue;
                }

                foreach (var item in _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList)
                {
                    object[] row = { item.EnvironmentBright, item.DisplayBright };
                    brightnessDataGridView.Rows.Add(row);
                }
            }
            else
            {
                if (_lightConfigInfo == null || _lightConfigInfo.DisplayHardcareConfig == null || _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting == null)
                {
                    return;
                }
                if (_lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo != null)
                {
                    this.enableCheckBox.Checked = _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.IsEnable;
                    this.brightnessNumericUpDown.Value
                        = _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue < 0 ||
                        _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue > 100
                        ? 0 : _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.OpticalFailureInfo.BrightnessValue;
                }
                foreach (var item in _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.AutoBrightMappingList)
                {
                    object[] row = { item.EnvironmentBright, item.DisplayBright };
                    brightnessDataGridView.Rows.Add(row);
                }
            }
        }

        private void RefreshOpticalProbeConfigView()
        {
            _internTable.Clear();
            opticalProbeDataGridView.Rows.Clear();
            _useablePeripheral = MonitorAllConfig.Instance().AllOpticalProbeInfo.UseablePeripheralList;
            List<PeripheralsLocation> opticalList = null;
            if (_isSoftwareConfig)
            {
                if (_lightConfigInfo == null
                    || _lightConfigInfo.DispaySoftWareConfig == null
                    || _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting == null
                    || _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.UseLightSensorList == null
                    || _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.UseLightSensorList.Count == 0)
                {
                    foreach (var itemConfig in MonitorAllConfig.Instance().AllOpticalProbeInfo.UseablePeripheralList)
                    {
                        string positionViewData = GetPositionViewData(itemConfig);
                        object[] row = { false, positionViewData, itemConfig.SensorValue.ToString() };
                        opticalProbeDataGridView.Rows.Add(row);
                        _internTable.Add(opticalProbeDataGridView.Rows.Count - 1, itemConfig);
                    }
                    return;
                }
                opticalList = _lightConfigInfo.DispaySoftWareConfig.AutoBrightSetting.UseLightSensorList;
            }
            else
            {
                if (_lightConfigInfo == null
                    || _lightConfigInfo.DisplayHardcareConfig == null
                    || _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting == null
                    || _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.UseLightSensorList == null
                    || _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.UseLightSensorList.Count == 0)
                {
                    foreach (var itemConfig in MonitorAllConfig.Instance().AllOpticalProbeInfo.UseablePeripheralList)
                    {
                        string positionViewData = GetPositionViewData(itemConfig);
                        object[] row = { false, positionViewData, itemConfig.SensorValue.ToString() };
                        opticalProbeDataGridView.Rows.Add(row);
                        _internTable.Add(opticalProbeDataGridView.Rows.Count - 1, itemConfig);
                    }
                    return;
                }
                opticalList = _lightConfigInfo.DisplayHardcareConfig.AutoBrightSetting.UseLightSensorList;
            }

            foreach (var itemConfig in MonitorAllConfig.Instance().AllOpticalProbeInfo.UseablePeripheralList)
            {
                var resultItem = opticalList.FirstOrDefault(o => o.IsEquals(itemConfig));
                if (resultItem == null)
                {
                    string positionViewData = GetPositionViewData(itemConfig);
                    object[] row = { false, positionViewData, itemConfig.SensorValue.ToString() };
                    opticalProbeDataGridView.Rows.Add(row);
                }
                else
                {
                    string positionViewData = GetPositionViewData(resultItem);
                    if (resultItem.SenderIndex != 0)
                    {
                        object[] row = { true, positionViewData, itemConfig.SensorValue.ToString(), CommonUI.GetCustomMessage(_languageTable, "NoSoftWareFail", "无软件失效") };
                        opticalProbeDataGridView.Rows.Add(row);
                        opticalProbeDataGridView.CurrentRow.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        object[] row = { true, positionViewData, itemConfig.SensorValue.ToString(), string.Empty };
                        opticalProbeDataGridView.Rows.Add(row);
                    }
                }
                _internTable.Add(opticalProbeDataGridView.Rows.Count - 1, itemConfig);
            }

        }

        private static string GetPositionViewData(UseablePeripheral opticalProbeItem)
        {
            string positionViewData = string.Empty;

            if (opticalProbeItem.SensorType == PeripheralsType.LightSensorOnSender)
            {
                positionViewData = opticalProbeItem.FirstSenderSN + "-" + (opticalProbeItem.SenderIndex + 1);
            }
            else if (opticalProbeItem.SensorType == PeripheralsType.LightSensorOnFuncCardInPort)
            {
                positionViewData = opticalProbeItem.FirstSenderSN + "-" + (opticalProbeItem.SenderIndex + 1) + "-"
                    + (opticalProbeItem.PortIndex + 1) + "-" + (opticalProbeItem.FuncCardIndex + 1) + "-" + (opticalProbeItem.SensorIndex + 1);
            }
            return positionViewData;
        }

        private static string GetPositionViewData(PeripheralsLocation opticalProbeItem)
        {
            string positionViewData = string.Empty;

            if (opticalProbeItem.SensorType == PeripheralsType.LightSensorOnSender)
            {
                positionViewData = opticalProbeItem.FirstSenderSN + "-" + (opticalProbeItem.SenderIndex + 1);
            }
            else if (opticalProbeItem.SensorType == PeripheralsType.LightSensorOnFuncCardInPort)
            {
                positionViewData = opticalProbeItem.FirstSenderSN + "-" + (opticalProbeItem.SenderIndex + 1) + "-"
                    + (opticalProbeItem.PortIndex + 1) + "-" + (opticalProbeItem.FuncCardIndex + 1) + "-" + (opticalProbeItem.SensorIndex + 1);
            }
            return positionViewData;
        }

        private void frm_OpticalProbeConfig_Load(object sender, EventArgs e)
        {
            ApplyUILanguageTable();
            MonitorAllConfig.Instance().RefreshAllOpticalProbeInfos();
            string strText = CommonUI.GetCustomMessage(_languageTable, "Getting_Opticalprobe_information", "正在获取光探头信息,请稍候...");
            ShowProcessForm(strText, true);
        }

        private void ApplyUILanguageTable()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.OpticalProbeConfigLangPath, this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.OpticalProbeConfigLangPath, "frm_OpticalProbeConfig", out _languageTable);

            if (_languageTable == null || _languageTable.Count == 0)
                return;

            foreach (DataGridViewColumn columnItem in brightnessDataGridView.Columns)
            {
                if (columnItem.CellType == typeof(DataGridViewButtonCell))
                {
                    string strText = CommonUI.GetCustomMessage(_languageTable, (columnItem.Name + "_Text").ToLower(), string.Empty);
                    (columnItem as DataGridViewButtonColumn).Text = strText;
                }
                string str = CommonUI.GetCustomMessage(_languageTable, columnItem.Name.ToLower(), string.Empty);
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                columnItem.HeaderText = str;
            }
            foreach (DataGridViewColumn columnItem in opticalProbeDataGridView.Columns)
            {
                if (columnItem.CellType == typeof(DataGridViewButtonCell))
                {
                    string strText = CommonUI.GetCustomMessage(_languageTable, (columnItem.Name + "_Text").ToLower(), string.Empty);
                    (columnItem as DataGridViewButtonColumn).Text = strText;
                }
                string str = CommonUI.GetCustomMessage(_languageTable, columnItem.Name.ToLower(), string.Empty);
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                columnItem.HeaderText = str;
            }

            if (_isTestStart)
            {
                crystalButton_BrightTest.Text = CommonUI.GetCustomMessage(_languageTable, "crystalButton_BrightTestStop", "停止测试");
            }
            else
            {
                crystalButton_BrightTest.Text = CommonUI.GetCustomMessage(_languageTable, "crystalButton_BrightTestStart", "光探头测试");
            }
        }

        private void brightnessDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //DataGridViewCell cell = brightnessDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //if (cell.OwningColumn.CellType == typeof(DataGridViewButtonCell))
            //    return;
            //e.Cancel = !IsBrightness(cell);
        }

        private void brightnessDataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            //DataGridViewRow row = brightnessDataGridView.Rows[e.RowIndex];
            //DataGridViewCell environmentBrightnessCell = row.Cells[0];
            //DataGridViewCell ledBrightnessCell = row.Cells[1];
            //e.Cancel = !(IsBrightness(environmentBrightnessCell) && IsBrightness(ledBrightnessCell));
        }


        private void brightnessDataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow row = brightnessDataGridView.Rows[e.RowIndex];
            //DataGridViewCell environmentBrightnessCell = row.Cells[0];
            //DataGridViewCell ledBrightnessCell = row.Cells[1];
            //environmentBrightnessCell.ErrorText = string.Empty;
            //ledBrightnessCell.ErrorText = string.Empty;
        }


        private void brightnessDataGridView_Validated(object sender, EventArgs e)
        {
            _brightnessMappingTable.Clear();
            foreach (DataGridViewRow row in brightnessDataGridView.Rows)
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                    return;
                var mappingItem = new BrightnessMapping();
                mappingItem.EnvironmentBrightness = int.Parse(row.Cells[0].Value.ToString());
                mappingItem.LedBrightness = int.Parse(row.Cells[1].Value.ToString());
                _brightnessMappingTable.Add(mappingItem);
            }
        }

        private void brightnessDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == 2 && !brightnessDataGridView.Rows[e.RowIndex].IsNewRow)
            {
                brightnessDataGridView.Rows.Remove(brightnessDataGridView.Rows[e.RowIndex]);
            }
        }


        private void opticalProbeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    var currentRow = opticalProbeDataGridView.Rows[e.RowIndex];
            //    var item = (DataGridViewCheckBoxCell)currentRow.Cells[e.ColumnIndex];
            //    if (item == null)
            //        return;
            //    bool isChecked = (bool)item.EditedFormattedValue;
            //    var currentPositionInfoCell = currentRow.Cells[1] as DataGridViewTextBoxCell;
            //    var currentRemarkInfoCell = currentRow.Cells[3] as DataGridViewTextBoxCell;
            //    if (currentPositionInfoCell == null)
            //        return;
            //    if (isChecked)
            //    {
            //        string positionInfo = currentPositionInfoCell.Value as string;
            //        string[] splitStr = positionInfo.Split(new char[] { '-' });
            //        if (splitStr.Length > 1 && Convert.ToInt32(splitStr[1]) > 1)
            //        {
            //            currentRow.DefaultCellStyle.ForeColor = Color.Red;
            //            currentRemarkInfoCell.Value = CommonUI.GetCustomMessage(_languageTable, "NoSoftWareFail", "无软件失效");
            //        }

            //        _useablePeripheral.Add(_internTable[e.RowIndex]);
            //    }
            //    else
            //    {
            //        currentRow.DefaultCellStyle.ForeColor = Color.Black;
            //        currentRemarkInfoCell.Value = string.Empty;
            //        _useablePeripheral.Remove(_internTable[e.RowIndex]);
            //    }

            //}
        }


        private void confirmButton_Click(object sender, EventArgs e)
        {
            ConfigState = LightSensorConfigState.OK_State;
            if (_useablePeripheral == null || _useablePeripheral.Count == 0)
            {
                ConfigState = LightSensorConfigState.NoSensor_State;
            }
            if (_brightnessMappingTable == null || _brightnessMappingTable.Count == 0)
            {
                ConfigState = LightSensorConfigState.NoMapping_State;
            }

            for (int i = 0; i < opticalProbeDataGridView.Rows.Count; i++)
            {
                var cell = opticalProbeDataGridView.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                string cellValue = cell.Value as string;
                if (!string.IsNullOrEmpty(cellValue))
                {
                    ConfigState = LightSensorConfigState.InvalidSensor_State;
                    break;
                }
            }

            brightnessDataGridView_Validated(sender, e);
            BrightExtendData = new AutoBrightExtendData();
            BrightExtendData.AutoBrightMappingList = new List<DisplayAutoBrightMapping>();
            BrightExtendData.UseLightSensorList = new List<PeripheralsLocation>();
            if (this.enableCheckBox.Checked)
            {
                BrightExtendData.OpticalFailureInfo = new OpticalProbeFailureInfo() { IsEnable = true, BrightnessValue = (int)this.brightnessNumericUpDown.Value };
            }
            else
            {
                BrightExtendData.OpticalFailureInfo = new OpticalProbeFailureInfo() { IsEnable = false, BrightnessValue = (int)this.brightnessNumericUpDown.Value };
            }
            foreach (var mappingItem in _brightnessMappingTable)
            {
                var displayBrightMapping = new DisplayAutoBrightMapping();
                displayBrightMapping.EnvironmentBright = mappingItem.EnvironmentBrightness;
                displayBrightMapping.DisplayBright = mappingItem.LedBrightness;
                BrightExtendData.AutoBrightMappingList.Add(displayBrightMapping);
            }
            foreach (var peripheralItem in _useablePeripheral)
            {
                var peripheral = new UseablePeripheral();
                peripheral = peripheralItem;
                BrightExtendData.UseLightSensorList.Add(peripheral);
            }
            BrightExtendData.AutoBrightMappingList.Sort(CompareBrightnessByValue);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void crystalButton_BrightTest_Click(object sender, EventArgs e)
        {
            if (_isTestStart)
            {
                crystalButton_BrightTest.Text = CommonUI.GetCustomMessage(_languageTable, "crystalButton_BrightTestStart", "光探头测试");
                if (SensorTestEvent != null)
                {
                    SensorTestEvent(false, null);
                }
                _isTestStart = false;
            }
            else
            {
                if (SensorTestEvent != null)
                {
                    if (_useablePeripheral == null || _useablePeripheral.Count == 0)
                    {
                        CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_languageTable, "nolightsensorconfig", "不存在光探头信息，无法测试！"));
                        return;
                    }
                    crystalButton_BrightTest.Text = CommonUI.GetCustomMessage(_languageTable, "crystalButton_BrightTestStop", "停止测试");

                    List<PeripheralsLocation> pers = new List<PeripheralsLocation>();
                    foreach (UseablePeripheral useable in _useablePeripheral)
                    {
                        pers.Add(useable);
                    }
                    SensorTestEvent(true, pers);
                }
                _isTestStart = true;
            }
        }

        private int CompareBrightnessByValue(DisplayAutoBrightMapping x, DisplayAutoBrightMapping y)
        {
            if (x.EnvironmentBright == null)
            {
                if (y.EnvironmentBright == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y.EnvironmentBright == null)
                {
                    return 1;
                }
                else
                {
                    int retval = x.EnvironmentBright.CompareTo(y.EnvironmentBright);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return x.EnvironmentBright.CompareTo(y.EnvironmentBright);
                    }
                }
            }
        }

        private bool IsBrightness(DataGridViewCell cell)
        {
            Int32 cellValueAsInt;
            if (cell.EditedFormattedValue.ToString().Length == 0)
            {
                brightnessDataGridView.Rows[cell.RowIndex].ErrorText = CommonUI.GetCustomMessage(_languageTable, "inputbrightvalue", "请输入亮度值");
                return false;
            }
            else if (!Int32.TryParse(cell.EditedFormattedValue.ToString(), out cellValueAsInt))
            {
                brightnessDataGridView.Rows[cell.RowIndex].ErrorText = CommonUI.GetCustomMessage(_languageTable, "brightvalueDigital", "亮度值必须为数字");
                return false;
            }
            else
            {
                foreach (DataGridViewRow row in brightnessDataGridView.Rows)
                {
                    if (row == null || row.Cells[cell.ColumnIndex] == null || row.Cells[cell.ColumnIndex].Value == null)
                        break;
                    if (row.Cells[cell.ColumnIndex].RowIndex == cell.RowIndex)
                    {
                        continue;
                    }
                    if (row.Cells[cell.ColumnIndex].Value.ToString().Equals(cell.EditedFormattedValue.ToString()))
                    {
                        brightnessDataGridView.Rows[cell.RowIndex].ErrorText = CommonUI.GetCustomMessage(_languageTable, "exitssamelightconfig", "存在相同亮度配置");
                        return false;
                    }
                }
            }

            brightnessDataGridView.Rows[cell.RowIndex].ErrorText = string.Empty;
            return true;
        }

        private void autoSectionButton_Click(object sender, EventArgs e)
        {
            frm_AutoSection autoSectionView = new frm_AutoSection();
            autoSectionView.Owner = this;
            autoSectionView.StartPosition = FormStartPosition.CenterParent;
            if (autoSectionView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (autoSectionView.AutoSectionParam != null)
                {
                    List<DisplayAutoBrightMapping> mappingList;
                    if (CustomTransform.FastSegment(autoSectionView.AutoSectionParam, out mappingList))
                    {
                        brightnessDataGridView.Rows.Clear();
                        foreach (var item in mappingList)
                        {
                            object[] row = { item.EnvironmentBright, item.DisplayBright };
                            brightnessDataGridView.Rows.Add(row);
                            var mappingItem = new BrightnessMapping();
                            mappingItem.EnvironmentBrightness = item.EnvironmentBright;
                            mappingItem.LedBrightness = item.DisplayBright;
                            _brightnessMappingTable.Add(mappingItem);
                        }
                    }
                }
            }
            brightnessDataGridView_Validated(sender, e);
        }

        private void AddMappingItemButton_Click(object sender, EventArgs e)
        {
            Frm_AddMappingItem addMappingItemView = new Frm_AddMappingItem(brightnessDataGridView);
            addMappingItemView.Owner = this;
            addMappingItemView.StartPosition = FormStartPosition.CenterParent;
            if (addMappingItemView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                object[] row = { addMappingItemView.EnvironmentItem, addMappingItemView.LedItem };
                brightnessDataGridView.Rows.Add(row);
                var mappingItem = new BrightnessMapping();
                mappingItem.EnvironmentBrightness = addMappingItemView.EnvironmentItem;
                mappingItem.LedBrightness = addMappingItemView.LedItem;
                _brightnessMappingTable.Add(mappingItem);
            }
        }
        private void IsOK()
        {

        }

        private void enableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null)
                return;
            if (checkBox.Checked)
            {
                this.brightnessNumericUpDown.Enabled = true;
            }
            else
            {
                this.brightnessNumericUpDown.Enabled = false;
            }
        }

        private void refreshOpticalProbeCrystalButton_Click(object sender, EventArgs e)
        {
            MonitorAllConfig.Instance().RefreshAllOpticalProbeInfos();
            string strText = CommonUI.GetCustomMessage(_languageTable, "Getting_Opticalprobe_information", "正在获取光探头信息,请稍候...");
            ShowProcessForm(strText, true);
        }

        delegate void SetBrightnessValueTextCallback(string value);

        void UC_BrightnessConfig_BrightnessValueRefreshed(object sender, BrightnessValueRefreshEventArgs e)
        {
            if (_lightConfigInfo == null || _lightConfigInfo.DispaySoftWareConfig == null || string.IsNullOrEmpty(_lightConfigInfo.DispaySoftWareConfig.DisplayUDID))
            {
                return;
            }
            if (e.SN == _lightConfigInfo.DispaySoftWareConfig.DisplayUDID)
            {
                //SetBrightnessValue(e.BrightnessValue);
            }
        }

        private void FrmClosed()
        {
            if (SensorTestEvent != null && _isTestStart == true)
            {
                SensorTestEvent(false, null);
            }
            this.Load -= frm_OpticalProbeConfig_Load;
            MonitorAllConfig.Instance().GetLightProbeEvent -= frm_OpticalProbeConfig_GetLightProbeEvent;
        }
        private void SetBrightnessValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            if (this.currentBrightnessTextBox.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.currentBrightnessTextBox.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.currentBrightnessTextBox.Disposing || this.currentBrightnessTextBox.IsDisposed)
                        return;
                }
                SetBrightnessValueTextCallback d = new SetBrightnessValueTextCallback(SetBrightnessValue);
                this.currentBrightnessTextBox.Invoke(d, new object[] { value });
            }
            else
            {
                this.currentBrightnessTextBox.Text = Math.Round((100d * Convert.ToDouble(value) / 255), 0).ToString();
            }
        }

        private void opticalProbeDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (opticalProbeDataGridView.IsCurrentCellDirty)
            {
                opticalProbeDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void opticalProbeDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && opticalProbeDataGridView.Rows.Count > 0)
            {
                var currentRow = opticalProbeDataGridView.Rows[e.RowIndex];
                var item = (DataGridViewCheckBoxCell)currentRow.Cells[e.ColumnIndex];
                if (item == null)
                    return;
                bool isChecked = (bool)item.EditedFormattedValue;
                var currentPositionInfoCell = currentRow.Cells[1] as DataGridViewTextBoxCell;
                var currentRemarkInfoCell = currentRow.Cells[3] as DataGridViewTextBoxCell;
                if (currentPositionInfoCell == null)
                    return;
                if (isChecked)
                {
                    string positionInfo = currentPositionInfoCell.Value as string;
                    string[] splitStr = positionInfo.Split(new char[] { '-' });
                    if (splitStr.Length > 1 && Convert.ToInt32(splitStr[1]) > 1)
                    {
                        currentRow.DefaultCellStyle.ForeColor = Color.Red;
                        currentRemarkInfoCell.Value = CommonUI.GetCustomMessage(_languageTable, "NoSoftWareFail", "无软件失效");
                    }
                    if (!_useablePeripheral.Contains(_internTable[e.RowIndex]))
                    {
                        _useablePeripheral.Add(_internTable[e.RowIndex]);
                    }
                }
                else
                {
                    currentRow.DefaultCellStyle.ForeColor = Color.Black;
                    currentRemarkInfoCell.Value = string.Empty;
                    _useablePeripheral.Remove(_internTable[e.RowIndex]);
                }

            }
        }
    }

    public class BrightnessMapping
    {
        public int EnvironmentBrightness { get; set; }
        public int LedBrightness { get; set; }
    }

    public enum LightSensorConfigState
    {
        OK_State,
        NoSensor_State,
        NoMapping_State,
        InvalidSensor_State
    }

    public static class PeripheralsLocationExtension
    {
        public static bool IsEquals(this PeripheralsLocation self, UseablePeripheral peripheral)
        {
            return ((((peripheral.FirstSenderSN.Split('-')[0] == self.FirstSenderSN.Split('-')[0])
                && (peripheral.FuncCardIndex == self.FuncCardIndex))
                && ((peripheral.PortIndex == self.PortIndex)
                && (peripheral.SenderIndex == self.SenderIndex)))
                && ((peripheral.SensorIndex == self.SensorIndex)
                && (peripheral.SensorType == self.SensorType)));
        }
    }
}
