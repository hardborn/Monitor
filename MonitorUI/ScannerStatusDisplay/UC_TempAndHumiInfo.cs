using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_TempAndHumiInfo : UserControl
    {
        public UC_TempAndHumiInfo()
        {
            InitializeComponent();
        }
        public string MaxValueStr
        {
            get { return linkLabel_MaxValue.Text; }
            set { linkLabel_MaxValue.Text = value; }
        }
        public string MinValueStr
        {
            get { return linkLabel_MinValue.Text; }
            set { linkLabel_MinValue.Text = value; }
        }
        public string AverageValueStr
        {
            get { return label_AverageValue.Text; }
            set { label_AverageValue.Text = value; }
        }
        public string BeyondAverageCount
        {
            get { return label_BeyondAverageCount.Text; }
            set { label_BeyondAverageCount.Text = value; }
        }
        public string TempAlarmCnt
        {
            get { return label_TempAlarmCnt.Text; }
            set { label_TempAlarmCnt.Text = value; }
        }
        public ClickMaxMinValueEventHandler ClickMaxMinValueEvent = null;

        private string _minValueStr = "";
        private string _maxValueStr = "";
        private string _averageValueStr = "";
        private string _curUnit = "";

        public UC_TempAndHumiInfo(string minStr, string maxStr, string averageStr,
                                  string beyongAverageStr, string alarmCountStr,
                                  string unit, ClickMaxMinValueEventHandler clickMaxMinValueEvent)
        {
            InitializeComponent();
            _curUnit = unit;
            ClickMaxMinValueEvent = clickMaxMinValueEvent;
            UpdateLangusge(minStr, maxStr, averageStr, beyongAverageStr, alarmCountStr);
        }


        public void UpdateLangusge(string minStr, string maxStr, string averageStr, 
                                   string beyongAverageStr,string alarmCountStr)
        {
            _minValueStr = minStr;
            _maxValueStr = maxStr;
            _averageValueStr = averageStr;

            label_Min.Text = minStr + "(" + _curUnit + "):";
            label_Max.Text = maxStr + "(" + _curUnit + "):";
            label_Average.Text = averageStr + "(" + _curUnit + "):";
            label_BeyondAverage.Text = beyongAverageStr + ":";
            label_AlrmInfo.Text = alarmCountStr + ":";
        }
        public void UpdateUnit(string unit)
        {
            _curUnit = unit;
            label_Min.Text = _minValueStr + "(" + _curUnit + "):";
            label_Max.Text = _maxValueStr + "(" + _curUnit + "):";
            label_Average.Text = _averageValueStr + "(" + _curUnit + "):";
        }

        private void linkLabel_MinValue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ClickMaxMinValueEvent != null)
            {
                ClickMaxMinValueEvent.Invoke(null, new ClickMaxMinValueEventArgs(ClickLinkLabelType.MinValue));
            }
        }

        private void linkLabel_MaxValue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ClickMaxMinValueEvent != null)
            {
                ClickMaxMinValueEvent.Invoke(null, new ClickMaxMinValueEventArgs(ClickLinkLabelType.MaxValue));
            }
        }
    }
}
