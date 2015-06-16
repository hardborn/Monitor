using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_SmokeFanPowerInfo : UserControl
    {
        public string TotalCount
        {
            get { return label_TotalCountValue.Text; }
            set { label_TotalCountValue.Text = value; }
        }
        public string AlarmCount
        {
            get { return label_AlarmCnt.Text; }
            set { label_AlarmCnt.Text = value; }
        }

        public UC_SmokeFanPowerInfo()
        {
            InitializeComponent();
        }
        public UC_SmokeFanPowerInfo(string totalCountStr, string alarmCountStr)
        {
            InitializeComponent();
            UpdateLangusge(totalCountStr, alarmCountStr);
        }
        public void UpdateLangusge(string totalCountStr, string alarmCountStr)
        {
            label_SBTotalCount.Text = totalCountStr + ":";
            label_AlrmCount.Text = alarmCountStr + ":";
        }
    }
}
