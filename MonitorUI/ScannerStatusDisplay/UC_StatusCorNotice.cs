using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_StatusCorNotice : UserControl
    {
        public UC_StatusCorNotice()
        {
            InitializeComponent();
        }

        public UC_StatusCorNotice(string normalstr, string invalidStr, string faultStr, string AlarmStr)
        {
            InitializeComponent();
            UpdateLanguage(normalstr, invalidStr, faultStr, AlarmStr);
        }
        public void UpdateLanguage(string normalstr, string invalidStr, string faultStr, string AlarmStr)
        {
            label_OK.Text = normalstr;
            label_Unknown.Text = invalidStr;
            label_Error.Text = faultStr;
            label_VoltAlarm.Text = AlarmStr;
        }
    }
}
