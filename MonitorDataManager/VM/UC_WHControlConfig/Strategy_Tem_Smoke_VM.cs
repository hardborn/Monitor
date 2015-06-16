using GalaSoft.MvvmLight;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    //public class Strategy_Tem_Smoke_VM : ViewModelBase
    //{
    //    #region 属性
    //    private Guid _id;
    //    public Guid ID
    //    {
    //        get { return _id; }
    //        set
    //        {
    //            _id = value;
    //            RaisePropertyChanged("ID");
    //        }
    //    }
    //    private string _aliaName;
    //    public string AliaName
    //    {
    //        get { return _aliaName; }
    //        set
    //        {
    //            _aliaName = value;
    //            RaisePropertyChanged("AliaName");
    //        }
    //    }

    //    private string _sn;
    //    public string SN
    //    {
    //        get { return _sn; }
    //        set
    //        {
    //            _sn = value;
    //            RaisePropertyChanged("SN");
    //        }
    //    }

    //    private StrategyType _stratType = StrategyType.SmokeStrategy;
    //    public StrategyType StratlType
    //    {
    //        get { return _stratType; }
    //        set
    //        {
    //            _stratType = value;
    //            RaisePropertyChanged("StratlType");
    //        }
    //    }

    //    private ConditionAlgorithm _conditionAlgor = ConditionAlgorithm.AverageAlgorithm;
    //    public ConditionAlgorithm ConditionAlgor
    //    {
    //        get { return _conditionAlgor; }
    //        set
    //        {
    //            _conditionAlgor = value;
    //            RaisePropertyChanged("ConditionAlgor");
    //        }
    //    }

    //    private int _lessThan = 70;
    //    public int LessThan
    //    {
    //        get { return _lessThan; }
    //        set
    //        {
    //            _lessThan = value;
    //            RaisePropertyChanged("LessThan");
    //        }
    //    }
    //    private int _greaterThan = 50;
    //    public int GreaterThan
    //    {
    //        get { return _greaterThan; }
    //        set
    //        {
    //            _greaterThan = value;
    //            RaisePropertyChanged("GreaterThan");
    //        }
    //    }

    //    private bool _isControlBrightness = false;
    //    public bool IsControlBrightness
    //    {
    //        get { return _isControlBrightness; }
    //        set
    //        {
    //            _isControlBrightness = value;
    //            RaisePropertyChanged("IsControlBrightness");
    //        }
    //    }
    //    private int _brightness = 0;
    //    public int Brightness
    //    {
    //        get { return _brightness; }
    //        set
    //        {
    //            _brightness = value;
    //            RaisePropertyChanged("Brightness");
    //        }
    //    }

    //    private Dictionary<string, PowerCtrl_Type> _powerCtrlDic;
    //    public Dictionary<string, PowerCtrl_Type> PowerCtrlDic
    //    {
    //        get { return _powerCtrlDic; }
    //        set { _powerCtrlDic = value; }
    //    }
    //    #endregion
    //    #region 公共方法
    //    public Strategy_Tem_Smoke_VM(string aliaName, string sn, Guid id, StrategyType stratType, ConditionAlgorithm conditionAlgor, int lessThan, int greaterThan, bool isControlBrightness, int brightness, Dictionary<string, PowerCtrl_Type> powerCtrlDic)
    //    {
    //        _aliaName = aliaName;
    //        _sn = sn;
    //        _id = id;
    //        _stratType = stratType;
    //        _conditionAlgor = conditionAlgor;
    //        _lessThan = lessThan;
    //        _greaterThan = greaterThan;
    //        _isControlBrightness = isControlBrightness;
    //        if (_isControlBrightness) _brightness = brightness;
    //        _powerCtrlDic = powerCtrlDic;
    //    }
    //    public Strategy_Tem_Smoke_VM(string sn, StrategyType type)
    //    {
    //        _sn = sn;
    //        _aliaName = MonitorAllConfig.Instance().LedInfoList.FindAll(a => a.Sn == _sn)[0].AliaName;
    //        _stratType = type;
    //        _id = Guid.NewGuid();
    //        _powerCtrlDic = new Dictionary<string, PowerCtrl_Type>();
    //        if (type == StrategyType.SmokeStrategy)
    //        {
    //            _greaterThan = 1;
    //        }
    //    }
    //    #endregion
    //}
}
