using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    /// <summary>
    /// 单屏的注册对象信息
    /// </summary>
    public class DataOneRegistationInfo
    {
        private string _sn10 = string.Empty;
        public string Sn10
        {
            get
            {
                return _sn10;
            }
            set
            {
                _sn10 = value;
            }
        }
        private string _led_name = string.Empty;
        public string Led_name
        {
            get
            {
                return _led_name;
            }
            set
            {
                _led_name = value;
            }
        }
        private double _longitude = 0;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
            }
        }
        private double _latitude = 0;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
            }
        }
        public Image MapSelect
        {
            get;
            set;
        }

        private int _width = 0;
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        private int _height = 0;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        private bool _isRegister = false;
        public bool IsRegister
        {
            get
            {
                return _isRegister;
            }
            set
            {
                _isRegister = value;
            }
        }
        private string _card_Num = string.Empty;
        public string Card_Num
        {
            get
            {
                return _card_Num;
            }
            set
            {
                _card_Num = value;
            }
        }
        private string _sn = string.Empty;
        public string Sn
        {
            get
            {
                return _sn;
            }
            set
            {
                _sn = value;
                string[] str = _sn.Split('-');
                Sn10 = str[0] + "-" + (Convert.ToInt32(str[1], 16) + 1).ToString("00");
            }
        }

        public string Card_NumSave { get; set; }

        public string Mac { get; set; }

        public string UserId { get; set; }
    }
}