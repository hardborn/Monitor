using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_StatusInfo : UserControl
    {
        public string TotalCount
        {
            get { return label_TotalCountValue.Text; }
            set { label_TotalCountValue.Text = value; }
        }
        public string FaultCount
        {
            get { return label_ErrorCountValue.Text; }
            set { label_ErrorCountValue.Text = value; }
        }
        public string AlarmCount
        {
            get { return label_AlarmCnt.Text; }
            set { label_AlarmCnt.Text = value; }
        }
        public UC_StatusInfo()
        {
            InitializeComponent();
        }

        public UC_StatusInfo(string totalCountStr, string faultCountStr, string alarmCountStr)
        {
            InitializeComponent();
            UpdateLangusge(totalCountStr, faultCountStr, alarmCountStr);
        }
        public void UpdateLangusge(string totalCountStr, string faultCountStr, string alarmCountStr)
        {
            label_SBTotalCount.Text = totalCountStr + ":";
            label_ErrorSBCount.Text = faultCountStr + ":";
            label_AlrmCount.Text = alarmCountStr + ":";
        }
    }
}
