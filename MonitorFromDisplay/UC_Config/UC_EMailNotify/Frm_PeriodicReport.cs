using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;
using Nova.Windows.Forms;
using Nova.Resource.Language;
using Nova.Monitoring.Common;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class Frm_PeriodicReport : Frm_CommonBase
    {
        public Frm_PeriodicReport()
        {
            InitializeComponent();
            dateTimePicker_Day.Enabled = false;
            label_Date.Enabled = false;
            EnableLable(false);

        }
        private DateTime _sendTime = DateTime.Now;
        public DateTime SendTimer
        {
            get { return _sendTime; }
            set 
            {
                _sendTime = value;
            }
        }

        public EMailSendModel SendModel
        {
            get { return _sendModel; }
            set { _sendModel = value; }
        }

        private EMailSendModel _sendModel = EMailSendModel.None;

        public DayOfWeek SendMailWeek
        {
            get { return _sendMailWeek; }
            set { _sendMailWeek = value; }
        }
        private DayOfWeek _sendMailWeek = DayOfWeek.Monday;

        private Hashtable _hashtable = null;
        public bool UpdateLanguage(Hashtable hashtable)
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            _hashtable = hashtable;

            return true;
        }


        private void crystalButton_Ok_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (checkBox_EveryDay.Checked)
            {
                _sendTime = dateTimePicker_hour.Value;
                _sendModel=EMailSendModel.Everyday;
                _sendMailWeek = DayOfWeek.Monday;
            }
            else if (checkBox_Weekly.Checked)
            {
                DayOfWeek weekType = SelectedWeek();
                //if (weekType == WeekType.None)
                //{
                //    msg = CommonUI.GetCustomMessage(_hashtable, "NotificationModel", "请选择星期几!");

                //    CustomMessageBox.ShowCustomMessageBox(this, msg, "", MessageBoxButtons.OK, MessageBoxIconType.Error);
                //    return;
                //}
                _sendTime = dateTimePicker_hour.Value;
                _sendModel=EMailSendModel.Weekly;
                _sendMailWeek = weekType;
            }
            else if(checkBox_Monthly.Checked)
            {
                int hour = DateTime.Now.Hour;
                DateTime time= dateTimePicker_Day.Value.Date;
                DateTime dateTime = time.AddHours(dateTimePicker_hour.Value.Hour).AddMinutes(dateTimePicker_hour.Value.Minute);

                _sendTime = dateTime;
                _sendModel=EMailSendModel.Mouthly;
                _sendMailWeek = DayOfWeek.Monday;
            }
            else
            {
                //if (!CustomTransform.GetLanguageString("NotificationModel", Frm_MonitorStatusDisplay.LangHashTable, out msg))
                //{
                //    msg = "请选择发件模式！";
                //}
                ShowCustomMessageBox(CommonUI.GetCustomMessage(_hashtable, "NotificationModel", "请选择发件模式!"),
                    "", MessageBoxButtons.OK, MessageBoxIconType.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void crystalButton_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkBox_EveryDay_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_EveryDay.Checked)
            {
                checkBox_Weekly.Checked = false;
                checkBox_Monthly.Checked = false;
                dateTimePicker_Day.Enabled = false;
                label_Date.Enabled = false;

                EnableCheckBox(checkBox_Weekly.Checked);
            }
            EnableLable(checkBox_EveryDay.Checked);
        }

        private void checkBox_Weekly_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Weekly.Checked)
            {
                checkBox_EveryDay.Checked = false;
                checkBox_Monthly.Checked = false;
                dateTimePicker_Day.Enabled = false;
                label_hour.Enabled = true;

            }
            EnableCheckBox(checkBox_Weekly.Checked);
            EnableLable(checkBox_Weekly.Checked);
        }

        private void checkBox_Monthly_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Monthly.Checked)
            {
                checkBox_Weekly.Checked = false;
                checkBox_EveryDay.Checked = false;
                dateTimePicker_Day.Enabled = true;

                label_Date.Enabled = true;
            }
            else
            {
                dateTimePicker_Day.Enabled = false;

                label_Date.Enabled = false;
            }
            EnableCheckBox(checkBox_Weekly.Checked);
            EnableLable(checkBox_Monthly.Checked);

        }

        public bool UpdateLanguage(string langFileName, string proxyLangName)
        {
            MultiLanguageUtils.UpdateLanguage(langFileName, this);

            return true;
        }

        private void Frm_PeriodicReport_Load(object sender, EventArgs e)
        {
            InitializeData();
#if CreateSource
            FormLanguageFile.ExportFormLanguage(this, "D:\\资源\\" + this.Name + ".xml");
#endif
            //UpdateLanguage(Frm_MonitorStatusDisplay.LanFileName, null);
            //Frm_MonitorStatusDisplay.AdjusterFontObj.Attach(this);

        }

        #region 私有方法
        private void EnableLable(bool enable)
        {
            label_hour.Enabled = enable;
            dateTimePicker_hour.Enabled = enable;
        }
        private DayOfWeek SelectedWeek()
        {
            if (checkBox_Monday.Checked)
            {
                checkBox_Tuesday.Checked = false;
                checkBox_Wednesday.Checked = false;
                checkBox_Thursday.Checked = false;
                checkBox_Friday.Checked = false;
                checkBox_Saturday.Checked = false;
                checkBox_Sunday.Checked = false;
                return DayOfWeek.Monday;
            }
            else if (checkBox_Tuesday.Checked)
            {
                checkBox_Monday.Checked = false;
                checkBox_Wednesday.Checked = false;
                checkBox_Thursday.Checked = false;
                checkBox_Friday.Checked = false;
                checkBox_Saturday.Checked = false;
                checkBox_Sunday.Checked = false;
                return DayOfWeek.Tuesday;

            }
            else if (checkBox_Wednesday.Checked)
            {
                checkBox_Monday.Checked = false;
                checkBox_Tuesday.Checked = false;
                checkBox_Thursday.Checked = false;
                checkBox_Friday.Checked = false;
                checkBox_Saturday.Checked = false;
                checkBox_Sunday.Checked = false;
                return DayOfWeek.Wednesday;

            }
            else if (checkBox_Thursday.Checked)
            {
                checkBox_Monday.Checked = false;
                checkBox_Tuesday.Checked = false;
                checkBox_Wednesday.Checked = false;
                checkBox_Friday.Checked = false;
                checkBox_Saturday.Checked = false;
                checkBox_Sunday.Checked = false;
                return DayOfWeek.Thursday;

            }
            else if (checkBox_Friday.Checked)
            {
                checkBox_Monday.Checked = false;
                checkBox_Tuesday.Checked = false;
                checkBox_Wednesday.Checked = false;
                checkBox_Thursday.Checked = false;
                checkBox_Saturday.Checked = false;
                checkBox_Sunday.Checked = false;
                return DayOfWeek.Friday;

            }
            else if (checkBox_Saturday.Checked)
            {
                checkBox_Monday.Checked = false;
                checkBox_Tuesday.Checked = false;
                checkBox_Wednesday.Checked = false;
                checkBox_Thursday.Checked = false;
                checkBox_Sunday.Checked = false;
                return DayOfWeek.Saturday;

            }
            else if (checkBox_Sunday.Checked)
            {
                checkBox_Monday.Checked = false;
                checkBox_Tuesday.Checked = false;
                checkBox_Wednesday.Checked = false;
                checkBox_Thursday.Checked = false;
                checkBox_Saturday.Checked = false;
                return DayOfWeek.Sunday;

            }
            return DayOfWeek.Monday;
        }
        private void EnableCheckBox(bool enable)
        {
            checkBox_Monday.Enabled = enable;
            checkBox_Tuesday.Enabled = enable;
            checkBox_Thursday.Enabled = enable;
            checkBox_Wednesday.Enabled = enable;
            checkBox_Friday.Enabled = enable;
            checkBox_Saturday.Enabled = enable;
            checkBox_Sunday.Enabled = enable;
        }
        private void InitializeData()
        {
            if (_sendModel == EMailSendModel.Everyday)
            {
                dateTimePicker_Day.Enabled = false;
                dateTimePicker_hour.Value = _sendTime;
                checkBox_EveryDay.Checked = true;
            }
            else if (_sendModel == EMailSendModel.Weekly)
            {
                SetCheckBox();
                dateTimePicker_hour.Value = _sendTime;
                checkBox_Weekly.Checked = true;
            }
            else if (_sendModel == EMailSendModel.Mouthly)
            {
                SetTextBox(_sendTime);
                checkBox_Monthly.Checked = true;
            }
            else if (_sendModel == EMailSendModel.None)
            {
                EnableCheckBox(false);
                dateTimePicker_hour.Value = DateTime.Now;
                dateTimePicker_Day.Value = DateTime.Now;
            }
        }
        private void SetTextBox(DateTime time)
        {
            dateTimePicker_Day.Value = time;
            dateTimePicker_hour.Value = time;
        }
        private void SetCheckBox()
        {
            if (_sendMailWeek == DayOfWeek.Monday)
            {
                checkBox_Monday.Checked = true;
            }
            else if (_sendMailWeek == DayOfWeek.Tuesday)
            {
                checkBox_Tuesday.Checked = true;
            }
            else if (_sendMailWeek == DayOfWeek.Wednesday)
            {
                checkBox_Wednesday.Checked = true;
            }
            else if (_sendMailWeek == DayOfWeek.Thursday)
            {
                checkBox_Thursday.Checked = true;
            }
            else if (_sendMailWeek == DayOfWeek.Friday)
            {
                checkBox_Friday.Checked = true;
            }
            else if (_sendMailWeek == DayOfWeek.Saturday)
            {
                checkBox_Saturday.Checked = true;
            }
            else if (_sendMailWeek == DayOfWeek.Sunday)
            {
                checkBox_Sunday.Checked = true;
            }
        }

        #endregion
    }
}