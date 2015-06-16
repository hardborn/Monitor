using GalaSoft.MvvmLight;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_WHControlConfig_Smoke_VM : UC_WHControlConfig_VM_Base
    {
        #region 属性
        private int _greaterThan = 1;
        public int GreaterThan
        {
            get { return _greaterThan; }
            set
            {
                _greaterThan = value;
                RaisePropertyChanged("GreaterThan");
            }
        }

        #endregion
        #region 公共方法
        public UC_WHControlConfig_Smoke_VM(string sn, Guid id, int greaterThan, Dictionary<string, PowerCtrl_Type> powerCtrlDic)
            : base(sn, id, StrategyType.SmokeStrategy, powerCtrlDic)
        {
            _greaterThan = greaterThan;
        }
        public UC_WHControlConfig_Smoke_VM(string sn)
            : base(sn, StrategyType.SmokeStrategy)
        {
        }
        #endregion
        #region 私有函数
        //private void InitialPowerList(Dictionary<string, PowerCtrl_Type> powerCtrlDic)
        //{
        //    _powerCtrlDic = new Dictionary<string, PowerCtrl_Type>();
        //    if (MonitorAllConfig.Instance().PeripheralCfgDICDic != null &&
        //        MonitorAllConfig.Instance().PeripheralCfgDICDic.Count != 0)
        //    {
        //        foreach (var item in MonitorAllConfig.Instance().PeripheralCfgDICDic.Keys)
        //        {
        //            if (!powerCtrlDic.ContainsKey(item)) _powerCtrlDic.Add(item, PowerCtrl_Type.still);
        //            else _powerCtrlDic.Add(item, powerCtrlDic[item]);
        //        }
        //    }
        //}
        #endregion
        #region

        //private Dictionary<string, PowerCtrl_Type> _powerCtrlDic;
        //public Dictionary<string, PowerCtrl_Type> PowerCtrlDic
        //{
        //    get { return _powerCtrlDic; }
        //    set
        //    {
        //        _powerCtrlDic = value;
        //        RaisePropertyChanged("PowerCtrlList");
        //    }
        //}

        //private Smoke_VM _strategyInfo;
        //public Smoke_VM StrategyInfo
        //{
        //    get { return _strategyInfo; }
        //    set
        //    {
        //        _strategyInfo = value;
        //        RaisePropertyChanged("StrategyInfo");
        //    }
        //}
        //#region 公共方法
        /////// <summary>
        /////// 创建烟雾策略
        /////// </summary>
        /////// <param name="selectedSN"></param>
        ////public void Initialize(string selectedSN)
        ////{
        ////    _straVM = new Strategy_VM(null);
        ////    InitialPowerList();
        ////}

        ///// <summary>
        ///// 初始化烟雾策略
        ///// </summary>
        ///// <param name="strategyInfo"></param>
        //public void Initialize(Smoke_VM strategyInfo)
        //{
        //    _strategyInfo = strategyInfo;
        //    InitialPowerList();
        //}
        //#endregion
        //#region 私有函数
        ///// <summary>
        ///// 初始化电源管理列表
        ///// </summary>
        //private void InitialPowerList()
        //{
        //    _powerCtrlDic = new Dictionary<string, PowerCtrl_Type>();
        //    if (MonitorAllConfig.Instance().PeripheralCfgDICDic != null &&
        //        MonitorAllConfig.Instance().PeripheralCfgDICDic.Count != 0)
        //    {
        //        foreach (var item in MonitorAllConfig.Instance().PeripheralCfgDICDic.Keys)
        //        {
        //            if (!_strategyInfo.PowerCtrlDic.ContainsKey(item)) _powerCtrlDic.Add(item, PowerCtrl_Type.still);
        //            else _powerCtrlDic.Add(item, _strategyInfo.PowerCtrlDic[item]);
        //        }
        //    }
        //}
        //#endregion
        #endregion
    }
}
