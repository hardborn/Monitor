using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class LedInfo : INotifyPropertyChanged
    {
        private double _width;
        private double _height;
        private string _latitude;
        private string _longtitude;
        private List<PartInfo> _partsInfoList;
        private string _description;
        private string _name;
        private string _mac;
        private string _serialNumber;
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                _serialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }
        public string Mac
        {
            get
            {
                return _mac;
            }
            set
            {
                _mac = value;
                OnPropertyChanged("Mac");
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }


        public string Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        public string Longitude
        {
            get
            {
                return _longtitude;
            }
            set
            {
                _longtitude = value;
                OnPropertyChanged("Longitude");
            }
        }
        public List<PartInfo> PartsInfoList
        {
            get
            {
                return _partsInfoList;
            }
            set
            {
                _partsInfoList = value;
                OnPropertyChanged("PartsInfoList");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        #region INotifyPropertyChanged


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
