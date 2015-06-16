using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class ComboBoxData_VM : ViewModelBase
    {
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        private string _name = "";

        public object Data
        {
            get;
            set;
        }
    }
}
