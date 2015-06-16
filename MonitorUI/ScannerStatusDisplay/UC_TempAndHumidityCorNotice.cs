using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class UC_TempAndHumidityCorNotice : UserControl
    {
        public UC_TempAndHumidityCorNotice()
        {
            InitializeComponent();
        }
        private float _minValue = 0.0f;
        private float _maxValue = 0.0f;
        private float _curThreshold = 0.0f;
        private string _curUnit = "";
        public float CurThreshold
        {
            get { return _curThreshold; }
            set
            {
                _curThreshold = value;
                label_Threshold.Text = _curThreshold.ToString() + _curUnit;
            }
        }
        public UC_TempAndHumidityCorNotice(float minValue, float threshold, float maxValue, string unit)
        {
            InitializeComponent();

            _minValue = minValue;
            _maxValue = maxValue;
            _curThreshold = threshold;
            _curUnit = unit;

            label_MaxValue.Text = maxValue.ToString() + unit;
            label_Threshold.Text = threshold.ToString() + unit;
            label_MinValue.Text = minValue.ToString() + unit;
        }

        public void UpdateLanguage(string invalidStr)
        {
            label_Unknow.Text = invalidStr;
        }

        public void UpdateUnit(float minValue, float threshold, float maxValue, string unit)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _curThreshold = threshold;
            _curUnit = unit;

            label_MaxValue.Text = _maxValue.ToString() + unit;
            label_Threshold.Text = _curThreshold.ToString() + unit;
            label_MinValue.Text = _minValue.ToString() + unit;
        }
        public void UpdateUnit(string unit)
        {
            _curUnit = unit;

            label_MaxValue.Text = _maxValue.ToString() + unit;
            label_Threshold.Text = _curThreshold.ToString() + unit;
            label_MinValue.Text = _minValue.ToString() + unit;
        }
    }
}
