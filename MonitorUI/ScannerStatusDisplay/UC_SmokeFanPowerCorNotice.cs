using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_SmokeFanPowerCorNotice : UserControl
    {
        public UC_SmokeFanPowerCorNotice()
        {
            InitializeComponent();
        }
        public UC_SmokeFanPowerCorNotice(MonitorDisplayType curTupy)
        {
            InitializeComponent();
            switch (curTupy)
            {
                case MonitorDisplayType.Fan:
                    label_Normal.Image = global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.Fan_OK;
                    label_AlarmPic.Image = global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.Fan_alarm;
                    label_InvalidPic.Image = global::Nova.Monitoring.UI.ScannerStatusDisplay.Properties.Resources.Fan_invalid;
                    break;
                default:
                    label_Normal.Image = null;
                    label_AlarmPic.Image = null;
                    label_InvalidPic.Image = null;
                    break;
            }
        }
        public void UpdateLanguage(string normalStr, string alarmStr, string unknowStr)
        {
            label_OK.Text = normalStr;
            label_AlarmValue.Text = alarmStr;
            label_Unknown.Text = unknowStr;
        }

    }
}
