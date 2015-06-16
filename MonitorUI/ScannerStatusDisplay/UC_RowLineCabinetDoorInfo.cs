using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_RowLineCabinetDoorInfo : UserControl
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
        public UC_RowLineCabinetDoorInfo()
        {
            InitializeComponent();
        }

        public UC_RowLineCabinetDoorInfo(string totalCountStr, string faultCountStr)
        {
            InitializeComponent();
            UpdateLangusge(totalCountStr, faultCountStr);
        }
        public void UpdateLangusge(string totalCountStr, string faultCountStr)
        {
            label_SBTotalCount.Text = totalCountStr + ":";
            label_ErrorSBCount.Text = faultCountStr + ":";
        }
    }
}
