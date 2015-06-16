using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Xml.Serialization;
using Nova.Monitoring.MonitorDataManager;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using Nova.Windows.Forms;
using System.Diagnostics;
using Nova.NovaCare.TileMap.Core;
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_CareServerConfig : UC_ConfigBase
    {
        private UC_CareServerConfig_VM _vm = null;
        public UC_CareServerConfig()
        {
            InitializeComponent();
            _vm = new UC_CareServerConfig_VM();
            uCCareServerConfigVMBindingSource.DataSource = _vm;
            dataGridViewBaseEx_Care.DataSource = bindingSource1;
            dataGridViewBaseEx_Care.AllowUserToResizeRows = false;
            dataGridViewBaseEx_Care.AllowUserToResizeColumns = false; 
            dataGridViewBaseEx_Care.Columns[0].HeaderText = "屏Id";
            dataGridViewBaseEx_Care.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(211, 211, 211);
            dataGridViewBaseEx_Care.Columns[0].Width = 120;
            dataGridViewBaseEx_Care.Columns[0].ReadOnly = true;
            dataGridViewBaseEx_Care.Columns[1].HeaderText = "屏别名";
            (dataGridViewBaseEx_Care.Columns[1] as DataGridViewTextBoxColumn).MaxInputLength = 20;
            dataGridViewBaseEx_Care.Columns[1].Width = 70;
            dataGridViewBaseEx_Care.Columns[2].HeaderText = "经度";
            dataGridViewBaseEx_Care.Columns[2].Width = 65;
            dataGridViewBaseEx_Care.Columns[3].HeaderText = "纬度";
            dataGridViewBaseEx_Care.Columns[3].Width = 65;
            dataGridViewBaseEx_Care.Columns[4].HeaderText = "地图";
            dataGridViewBaseEx_Care.Columns[4].Width = 40;
            (dataGridViewBaseEx_Care.Columns[4] as DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Zoom;
            (dataGridViewBaseEx_Care.Columns[4] as DataGridViewImageColumn).Image = Properties.Resources.mapMarker;
            dataGridViewBaseEx_Care.Columns[5].HeaderText = "宽度";
            dataGridViewBaseEx_Care.Columns[5].DefaultCellStyle.BackColor=Color.FromArgb(211,211,211);
            dataGridViewBaseEx_Care.Columns[5].Width = 40;
            dataGridViewBaseEx_Care.Columns[5].ReadOnly = true;
            dataGridViewBaseEx_Care.Columns[6].HeaderText = "高度";
            dataGridViewBaseEx_Care.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(211, 211, 211);
            dataGridViewBaseEx_Care.Columns[6].Width = 40;
            dataGridViewBaseEx_Care.Columns[6].ReadOnly = true;
            dataGridViewBaseEx_Care.Columns[7].HeaderText = "注册状态";
            dataGridViewBaseEx_Care.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(211, 211, 211);
            dataGridViewBaseEx_Care.Columns[7].Width = 65;
            dataGridViewBaseEx_Care.Columns[7].ReadOnly = true;
            dataGridViewBaseEx_Care.Columns[8].HeaderText = "屏信息";
            dataGridViewBaseEx_Care.Columns[8].DefaultCellStyle.BackColor = Color.FromArgb(211, 211, 211);
            dataGridViewBaseEx_Care.Columns[8].Width = 100;
            dataGridViewBaseEx_Care.Columns[8].ReadOnly = true;
            dataGridViewBaseEx_Care.Columns[9].Visible = false;
            dataGridViewBaseEx_Care.Columns[10].Visible = false;
            dataGridViewBaseEx_Care.Columns[11].Visible = false;
            dataGridViewBaseEx_Care.Columns[12].Visible = false;
            UpdateLanguage();
            crystalButton_RefreshUser_Click(null, null);
        }
        
        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable = null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_CareServerConfig_String", out _hashTable);
            HsLangTable = _hashTable;
            dataGridViewBaseEx_Care.Columns[0].HeaderText = CommonUI.GetCustomMessage(_hashTable,"ScreenSN","屏ID");
            dataGridViewBaseEx_Care.Columns[1].HeaderText = CommonUI.GetCustomMessage(_hashTable,"SnAlia","屏别名");
            dataGridViewBaseEx_Care.Columns[2].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Longitude", "经度");
            dataGridViewBaseEx_Care.Columns[3].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Latitude", "纬度");
            dataGridViewBaseEx_Care.Columns[4].HeaderText = CommonUI.GetCustomMessage(_hashTable, "MapName", "地图");
            dataGridViewBaseEx_Care.Columns[5].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Width", "宽度");
            dataGridViewBaseEx_Care.Columns[6].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Height", "高度");
            dataGridViewBaseEx_Care.Columns[7].HeaderText = CommonUI.GetCustomMessage(_hashTable, "CareStatus", "注册状态");
            dataGridViewBaseEx_Care.Columns[8].HeaderText = CommonUI.GetCustomMessage(_hashTable, "ScreenMsg", "屏信息");
        }

        private delegate void LedRegistationInfoHandle(bool e);
        void UC_CareServerConfig_LedRegistationInfoEvent(bool e)
        {
            if (!this.InvokeRequired)
            {
                if (e)
                {
                    _vm.CmdInitialize.Execute(null);
                    bindingSource1.ResetBindings(false);
                    CloseProcessForm();
                }
            }
            else
            {
                LedRegistationInfoHandle led = new LedRegistationInfoHandle(UC_CareServerConfig_LedRegistationInfoEvent);
                this.Invoke(led,new object[]{e});
            }
        }

        protected override void Register()
        {
            Messenger.Default.Register<string>(this, MsgToken.MSG_NOTIFYDIALOG_OK, OnCareServerSaveFinish);
            MonitorAllConfig.Instance().LedRegistationInfoEvent += UC_CareServerConfig_LedRegistationInfoEvent;
        }

        protected override void UnRegister()
        {
            Messenger.Default.Unregister<string>(this, MsgToken.MSG_NOTIFYDIALOG_OK, OnCareServerSaveFinish);
            MonitorAllConfig.Instance().LedRegistationInfoEvent -= UC_CareServerConfig_LedRegistationInfoEvent;
        }

        private void txt_CareRegisterUser_TextChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_CareRegisterUser.Text) || isExistError)
            {
                crystalButton_OK.Enabled = false;
            }
            else
            {
                crystalButton_OK.Enabled = true;
            }
        }

        private void OnCareServerSaveFinish(string result)
        {
            string tmpType = string.Empty;
            switch (result)
            {
                case "ScreenAlreadyExists":
                    tmpType = CommonUI.GetCustomMessage(HsLangTable, "ScreenAlreadyExists", "屏体已注册!");
                    break;
                case "AccountNotExist":
                    tmpType = CommonUI.GetCustomMessage(HsLangTable, "AccountNotExist", "账户不存在!");
                    break;
                case "ScreenRegisteredSuccessfully":
                    tmpType = CommonUI.GetCustomMessage(HsLangTable, "ScreenRegisteredSuccessfully", "注册成功!");
                    break;
                case "SnEmpty":
                    tmpType = CommonUI.GetCustomMessage(HsLangTable, "SnEmpty", "不允许存在屏体别名为空!");
                    break;
                case "ScreenReregister":
                    if (ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "ScreenReregister", "免则声明：注册用户变更，是否继续?"), "", MessageBoxButtons.OKCancel, MessageBoxIconType.Question)
                        == DialogResult.OK)
                    {
                        Action action = new Action(() =>
                        {
                            _vm.CmdSaveTo.Execute(true);
                        });
                        this.BeginInvoke(action, null);
                    }
                    else
                    {
                        CloseProcessForm();
                    }
                    return;
                default:
                    tmpType = CommonUI.GetCustomMessage(HsLangTable, "CareRegFailed", "注册失败!");
                    break;
            }
            bindingSource1.ResetBindings(false);
            ShowCustomMessageBox(tmpType, "", MessageBoxButtons.OK, MessageBoxIconType.Alert);
            CloseProcessForm();
        }

        private void crystalButton_RefreshUser_Click(object sender, EventArgs e)
        {
            _vm.CmdGetRegist.Execute(null);
            ShowProcessForm(CommonUI.GetCustomMessage(HsLangTable, "CareReadData", "注册数据正在获取中，请稍候再试..."),true);
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            Action action = new Action(() =>
            {
                _vm.CmdSaveTo.Execute(false);
            });
            this.BeginInvoke(action, null);
            ShowSending("SaveCareServerConfig", "正在进行注册,请稍候...", true);
        }

        private void dataGridViewBaseEx_Care_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewBaseEx_Care.Rows.Count || e.ColumnIndex < 0)
            {
                return;
            }
            if(e.ColumnIndex==4)
            {
                Nova.Monitoring.Control.TileMapView map = new Nova.Monitoring.Control.TileMapView();
                bool? isResult = map.ShowDialog();
                if (isResult != null && isResult == true)
                {
                    PointLatLng point = map.CurrentPosition;
                    dataGridViewBaseEx_Care[2, e.RowIndex].Value = Math.Round(point.Lng, 5).ToString();
                    dataGridViewBaseEx_Care[3, e.RowIndex].Value = Math.Round(point.Lat, 5).ToString();
                }
            }
        }

        private void dataGridViewBaseEx_Care_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewBaseEx_Care.Rows.Count || e.ColumnIndex < 0)
            {
                return;
            }
            if(e.ColumnIndex==2 || e.ColumnIndex==3)
            {
                object txtBox = dataGridViewBaseEx_Care[e.ColumnIndex,e.RowIndex].Value;
                if (txtBox == null)
                {
                    dataGridViewBaseEx_Care[e.ColumnIndex, e.RowIndex].Value = 0;
                    return;
                }
                double txtValue = 0;
                if (double.TryParse(txtBox.ToString(), out txtValue))
                {
                    if (txtValue < -180)
                    {
                        txtValue = -180;
                        dataGridViewBaseEx_Care[e.ColumnIndex, e.RowIndex].Value = txtValue.ToString();
                    }
                    else if (txtValue > 180)
                    {
                        txtValue = 180;
                        dataGridViewBaseEx_Care[e.ColumnIndex, e.RowIndex].Value = txtValue.ToString();
                    }

                    if (txtValue.ToString().IndexOf('-') >= 0 && txtValue.ToString().Length>10)
                    {
                        txtValue = Math.Round(txtValue, 5);
                        dataGridViewBaseEx_Care[e.ColumnIndex, e.RowIndex].Value = txtValue.ToString();
                    }
                    else if (txtValue.ToString().IndexOf('-') < 0 && txtValue.ToString().Length > 9)
                    {
                        txtValue = Math.Round(txtValue, 5);
                        dataGridViewBaseEx_Care[e.ColumnIndex, e.RowIndex].Value = txtValue.ToString();
                    }
                }
                else if (!string.IsNullOrEmpty(txtBox.ToString()))
                {
                    if (txtBox.ToString().LastIndexOf('-') != 0)
                    {
                        dataGridViewBaseEx_Care[e.ColumnIndex, e.RowIndex].Value = txtBox.ToString().Substring(0, txtBox.ToString().Length - 1);
                    }
                }
            }
        }

        private void dataGridViewBaseEx_Care_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
        }

        bool isExistError = false;
        private void dataGridViewBaseEx_Care_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1 || e.RowIndex < 0)
            {
                return;
            }
            isExistError = false;
            string strName = string.Empty;
            if (dataGridViewBaseEx_Care[1, e.RowIndex].Value == null ||
                string.IsNullOrEmpty(dataGridViewBaseEx_Care[1, e.RowIndex].Value.ToString()))
            {
                dataGridViewBaseEx_Care[1, e.RowIndex].ToolTipText = "不允许名字为空";
                dataGridViewBaseEx_Care[1, e.RowIndex].Style.BackColor = Color.Yellow;
                crystalButton_OK.Enabled = false;
                return;
            }

            Dictionary<int, string> values = new Dictionary<int, string>();
            int dwCount = dataGridViewBaseEx_Care.Rows.Count;
            for (int i = 0; i < dwCount; i++)
            {
                if (dataGridViewBaseEx_Care[1, i].Value == null)
                {
                    dataGridViewBaseEx_Care[1, i].Style.BackColor = Color.Yellow;
                    dataGridViewBaseEx_Care[1, i].ToolTipText
                    = CommonUI.GetCustomMessage(HsLangTable, "PowerControlTipEmpty", "不允许名字为空");
                    isExistError = true;
                }
                else
                {
                    values.Add(i, dataGridViewBaseEx_Care[1, i].Value.ToString());
                }
            }

            foreach (KeyValuePair<int, string> keyvalue in values)
            {
                if (values.Values.ToList().FindAll(a => a == keyvalue.Value).Count > 1)
                {
                    dataGridViewBaseEx_Care[1, keyvalue.Key].Style.BackColor = Color.Yellow;
                    dataGridViewBaseEx_Care[1, keyvalue.Key].ToolTipText
                        = CommonUI.GetCustomMessage(HsLangTable, "PowerControlTipDuplicate", "不允许名字重复");
                    isExistError = true;
                }
                else if (System.Text.Encoding.Default.GetBytes(keyvalue.Value).Length > 21)
                {
                    dataGridViewBaseEx_Care[1, keyvalue.Key].Style.BackColor = Color.Yellow;
                    dataGridViewBaseEx_Care[1, keyvalue.Key].ToolTipText
                        = CommonUI.GetCustomMessage(HsLangTable, "PowerControlTipLength", "不允许名字字节长度超过20");
                    isExistError = true;
                }
                else
                {
                    dataGridViewBaseEx_Care[1, keyvalue.Key].Style.BackColor = Color.White;
                    dataGridViewBaseEx_Care[1, keyvalue.Key].ToolTipText = "";
                }
            }

            if (isExistError == true)
            {
                crystalButton_OK.Enabled = false;
            }
            else
            {
                txt_CareRegisterUser_TextChanged(null, null);
            }
        }

        private void dataGridViewBaseEx_Care_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == 8 && e.Value!=null && !string.IsNullOrEmpty(e.Value.ToString()))
            {
                string str = e.Value.ToString();
                str = str.Replace("SendCard", CommonUI.GetCustomMessage(HsLangTable, "SendCard", "SendCard"))
                    .Replace("ReceiverCard", CommonUI.GetCustomMessage(HsLangTable, "ReceiverCard", "ReceiverCard"))
                    .Replace("MonitoringCard", CommonUI.GetCustomMessage(HsLangTable, "MonitoringCard", "MonitoringCard"));
                e.Value = str;
            }
        }
    }
}
