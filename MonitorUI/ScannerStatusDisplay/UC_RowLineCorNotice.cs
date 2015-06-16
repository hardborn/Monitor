using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_RowLineCorNotice : UserControl
    {
        public UC_RowLineCorNotice()
        {
            InitializeComponent();
        }

        public UC_RowLineCorNotice(string normalstr, string invalidStr, string AlarmStr)
        {
            InitializeComponent();
            UpdateLanguage(normalstr, invalidStr, AlarmStr);
        }
        public void UpdateLanguage(string normalstr, string invalidStr, string AlarmStr)
        {
            label_OK.Text = normalstr;
            label_Unknown.Text = invalidStr;
            label_Error.Text = AlarmStr;
        }
    }
}
