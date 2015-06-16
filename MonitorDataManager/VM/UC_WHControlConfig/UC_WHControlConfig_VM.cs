
using GalaSoft.MvvmLight;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_WHControlConfig_VM : ViewModelBase
    {
        #region 属性
        private bool _isEnableCtrl = true;
        public bool IsEnableCtrl
        {
            get { return _isEnableCtrl; }
            set
            {
                _isEnableCtrl = value;
                RaisePropertyChanged("IsEnableCtrl");
            }
        }
        private string _selectedScreenSN;
        public string SelectedScreenSN
        {
            get { return _selectedScreenSN; }
            set
            {
                _selectedScreenSN = value;
                RaisePropertyChanged("SelectedScreenSN");
            }
        }
        private List<UC_WHControlConfig_Smoke_VM> _smokeStrategyList = new List<UC_WHControlConfig_Smoke_VM>();

        public List<UC_WHControlConfig_Smoke_VM> SmokeStrategyList
        {
            get { return _smokeStrategyList; }
            set
            {
                _smokeStrategyList = value;
                RaisePropertyChanged("SmokeStrategyList");
            }
        }
        private List<UC_WHControlConfig_Tem_VM> _temStrategyList = new List<UC_WHControlConfig_Tem_VM>();
        public List<UC_WHControlConfig_Tem_VM> TemStrategyList
        {
            get { return _temStrategyList; }
            set
            {
                _temStrategyList = value;
                RaisePropertyChanged("TemStrategyList");
            }
        }
        private List<UC_WHControlConfig_VM_Base> _vm_BaseList = new List<UC_WHControlConfig_VM_Base>();
        public List<UC_WHControlConfig_VM_Base> VM_BaseList
        {
            get { return _vm_BaseList; }
            set
            {
                _vm_BaseList = value;
                RaisePropertyChanged("Vm_BaseList");
            }
        }
        private UC_WHControlConfig_VM_Base _selectedStrategy;
        public UC_WHControlConfig_VM_Base SelectedStrategy
        {
            get { return _selectedStrategy; }
            set
            {
                _selectedStrategy = value;
                RaisePropertyChanged("SelectedStrategy");
            }
        }
        #endregion
        #region 公共方法
        public UC_WHControlConfig_VM()
        {
            _selectedScreenSN = string.Empty;
        }
        public void Initialize(string sn)
        {
            try
            {
                _selectedScreenSN = sn;
                //_mode = MonitorDataManager.Mode.add;
                if (MonitorAllConfig.Instance().LedInfoList.FindAll(a => a.Sn == _selectedScreenSN).Count == 0)
                {
                    _isEnableCtrl = false;
                    return;
                }
                else _isEnableCtrl = true;
                _temStrategyList.Clear();
                _smokeStrategyList.Clear();
                _vm_BaseList.Clear();
                if (MonitorAllConfig.Instance().StrategyConfigDic.Keys.Contains(_selectedScreenSN))
                {
                    //已配置过策略
                    UC_WHControlConfig_Smoke_VM smoke_VM;
                    UC_WHControlConfig_Tem_VM tem_VM;
                    List<Strategy> straList = MonitorAllConfig.Instance().StrategyConfigDic[_selectedScreenSN];
                    Dictionary<string, PowerCtrl_Type> powerCtrlDic;
                    foreach (var item in straList)
                    {
                        if (item.Type == StrategyType.LightStrategy) continue;
                        foreach (var rule in item.RuleTable)
                        {
                            //if (!_strategyDic.Keys.Contains(item.Type))
                            //    _strategyDic.Add(item.Type, new List<Strategy_Tem_Smoke_VM>());
                            if (rule.RuleAction.ActionCommandCollection.FindIndex(a => a.ActionTarget.TargetType == ActionTargetType.SmartFunction) >= 0) continue;
                            List<ActionCommand> aCmdList = rule.RuleAction.ActionCommandCollection.FindAll(a => (a.ActionType != ActionType.Set && a.ActionTarget.TargetType != ActionTargetType.SmartFunction));
                            powerCtrlDic = new Dictionary<string, PowerCtrl_Type>();

                            if (aCmdList.Count != 0)
                            {
                                PowerCtrl_Type pType = PowerCtrl_Type.still;
                                foreach (var cmd in aCmdList)
                                {
                                    if (cmd.ActionType == ActionType.Open)
                                        pType = PowerCtrl_Type.open;
                                    else if (cmd.ActionType == ActionType.Close)
                                        pType = PowerCtrl_Type.close;
                                    if (cmd.ActionTarget.DeviceTarget != null)
                                    {
                                        foreach (var target in cmd.ActionTarget.DeviceTarget)
                                        {
                                            if (!powerCtrlDic.ContainsKey(target))
                                            {
                                                powerCtrlDic.Add(target, pType);
                                            }
                                        }
                                    }
                                }
                            }
                            var action = rule.RuleAction.ActionCommandCollection.Find(a => a.ActionType == ActionType.Set && item.Type == StrategyType.TemperatureStrategy);
                            bool isBrightness = false;
                            int brightnessValue = 0;
                            if (action != null)
                            {
                                isBrightness = true;
                                int.TryParse(action.ActionTarget.ParameterTarget.Value.ToString(), out brightnessValue);
                            }
                            if (item.Type == StrategyType.SmokeStrategy)
                            {
                                smoke_VM = new UC_WHControlConfig_Smoke_VM(item.SN, Guid.NewGuid(), rule.RuleCondition.ConditionCollection.Find(a => a.Operator == OperatorType.GreaterThan).RightExpression, powerCtrlDic);
                                _smokeStrategyList.Add(smoke_VM);
                                _vm_BaseList.Add(smoke_VM);
                            }
                            else if (item.Type == StrategyType.TemperatureStrategy)
                            {
                                var lessRule = rule.RuleCondition.ConditionCollection.Find(a => a.Operator == OperatorType.LessThan);
                                var greaterRule = rule.RuleCondition.ConditionCollection.Find(a => a.Operator == OperatorType.GreaterThan);
                                int lessRightEx = 0, greaterRightEx = 0;
                                if (lessRule != null) lessRightEx = lessRule.RightExpression;
                                if (greaterRule != null) greaterRightEx = greaterRule.RightExpression;
                                tem_VM = new UC_WHControlConfig_Tem_VM(item.SN, Guid.NewGuid(), rule.RuleCondition.ConditionCollection[0].Algorithm, lessRightEx, greaterRightEx, isBrightness, brightnessValue, powerCtrlDic);
                                _temStrategyList.Add(tem_VM);
                                _vm_BaseList.Add(tem_VM);
                            }
                        }
                    }
                    //if (_temStrategyList.Count != 0 || _smokeStrategyList.Count != 0) _mode = MonitorDataManager.Mode.modify;
                }
            }
            catch (Exception ex)
            {
                MonitorAllConfig.Instance().WriteLogToFile("ExistCatch：策略主页面初始化异常：" + ex.ToString(), true);
            }
        }
        public UC_WHControlConfig_VM_Base CreateStrategy(StrategyType type)
        {
            UC_WHControlConfig_VM_Base strategy = null;
            if (type == StrategyType.SmokeStrategy)
            {
                strategy = new UC_WHControlConfig_Smoke_VM(_selectedScreenSN);
            }
            else if (type == StrategyType.TemperatureStrategy)
            {
                strategy = new UC_WHControlConfig_Tem_VM(_selectedScreenSN);
            }
            else if (type == StrategyType.LightStrategy)
            {
                //处理亮度控制初始化
                return null;
            }
            return strategy;
        }
        public bool DeleteStartegy(UC_WHControlConfig_VM_Base strategyInfo)
        {
            if (strategyInfo.StratlType == StrategyType.SmokeStrategy)
            {
                var vm = (UC_WHControlConfig_Smoke_VM)strategyInfo;
                if (vm == null) return false;
                var smokeVM = _smokeStrategyList.Find(a => a.ID == vm.ID);
                if (smokeVM == null) return false;
                else
                {
                    if (_smokeStrategyList.Remove(smokeVM))
                        return _vm_BaseList.Remove(smokeVM);
                    else return false;
                }
            }
            else if (strategyInfo.StratlType == StrategyType.TemperatureStrategy)
            {
                var vm = (UC_WHControlConfig_Tem_VM)strategyInfo;
                if (vm == null) return false;
                var temVM = _temStrategyList.Find(a => a.ID == vm.ID);
                if (temVM == null) return false;
                else
                {
                    if (_temStrategyList.Remove(temVM))
                        return _vm_BaseList.Remove(temVM);
                    else return false;
                }
            }
            else if (strategyInfo.StratlType == StrategyType.LightStrategy)
            {
                //处理亮度控制
            }
            return true;
        }
        public void AddStartegy(UC_WHControlConfig_VM_Base strategyInfo)
        {
            if (strategyInfo.StratlType == StrategyType.SmokeStrategy)
            {
                var vm = (UC_WHControlConfig_Smoke_VM)strategyInfo;
                _smokeStrategyList.Add(vm);
                _vm_BaseList.Add(vm);
            }
            else if (strategyInfo.StratlType == StrategyType.TemperatureStrategy)
            {
                var vm = (UC_WHControlConfig_Tem_VM)strategyInfo;
                _temStrategyList.Add(vm);
                _vm_BaseList.Add(vm);
            }
        }
        public void ModifyStartegy(UC_WHControlConfig_VM_Base strategyInfo)
        {
            if (strategyInfo.StratlType == StrategyType.SmokeStrategy)
            {
                var vm = (UC_WHControlConfig_Smoke_VM)strategyInfo;
                if (vm == null) return;
                var smokeVM = _smokeStrategyList.Find(a => a.ID == vm.ID);
                if (smokeVM != null)
                {
                    int index = _smokeStrategyList.FindIndex(a => a.ID == smokeVM.ID);
                    _smokeStrategyList.RemoveAt(index);
                    _smokeStrategyList.Insert(index, vm);
                    index = _vm_BaseList.FindIndex(a => a.ID == smokeVM.ID);
                    _vm_BaseList.RemoveAt(index);
                    _vm_BaseList.Insert(index, vm);
                }
            }
            else if (strategyInfo.StratlType == StrategyType.TemperatureStrategy)
            {
                var vm = (UC_WHControlConfig_Tem_VM)strategyInfo;
                if (vm == null) return;
                var temVM = _temStrategyList.Find(a => a.ID == vm.ID);
                if (temVM != null)
                {
                    int index = _temStrategyList.FindIndex(a => a.ID == temVM.ID);
                    _temStrategyList.RemoveAt(index);
                    _temStrategyList.Insert(index, vm);
                    index = _vm_BaseList.FindIndex(a => a.ID == temVM.ID);
                    _vm_BaseList.RemoveAt(index);
                    _vm_BaseList.Insert(index, vm);
                }
            }
        }
        public bool Save()
        {
            StrategyTable sTable = new StrategyTable();
            sTable.StrategyList = new List<Strategy>();
            List<Strategy> smokeList = GetSmokeStrategy(_smokeStrategyList);
            if (smokeList.Count != 0) sTable.StrategyList.AddRange(smokeList);
            List<Strategy> temList = GetTemperatureStrategy(_temStrategyList);
            if (temList.Count != 0) sTable.StrategyList.AddRange(temList);
            return MonitorAllConfig.Instance().SaveStrategyCofig(sTable, _selectedScreenSN);
        }
        #endregion
        #region 私有函数
        private ControlConfigSaveRes IsTemOk(UC_WHControlConfig_Tem_VM tem_VM)
        {
            if (tem_VM == null) return ControlConfigSaveRes.tem_objIsNull;
            bool isAllInvalid = true;
            foreach (var item in tem_VM.PowerCtrlDic.Values)
            {
                if (item != PowerCtrl_Type.still) isAllInvalid = false;
            }
            if (isAllInvalid && !tem_VM.IsControlBrightness) return ControlConfigSaveRes.tem_CtrlCfgIsInvalid;
            if (tem_VM.GreaterThan + 5 > tem_VM.LessThan) return ControlConfigSaveRes.tem_ConditionError;
            List<UC_WHControlConfig_Tem_VM> tem = _temStrategyList.FindAll(a => (a.LessThan + 5 > tem_VM.GreaterThan &&
              a.GreaterThan - 5 < tem_VM.GreaterThan) ||
              (a.LessThan + 5 > tem_VM.LessThan &&
              a.GreaterThan - 5 < tem_VM.LessThan) ||
              (a.GreaterThan - 5 > tem_VM.GreaterThan &&
              a.LessThan + 5 < tem_VM.LessThan));
            if (tem.Count != 0) return ControlConfigSaveRes.tem_CtrlCfgIsExist;
            return ControlConfigSaveRes.ok;
        }
        private ControlConfigSaveRes IsSmokeOk(UC_WHControlConfig_Smoke_VM smoke_VM)
        {
            if (smoke_VM == null) return ControlConfigSaveRes.smoke_objIsNull;
            bool isAllInvalid = true;
            foreach (var item in smoke_VM.PowerCtrlDic)
            {
                if (item.Value == PowerCtrl_Type.still) continue;
                else isAllInvalid = false;
                for (int i = 0; i < _smokeStrategyList.Count; i++)
                {
                    if (IsSmokeConflict(item.Key, _smokeStrategyList[i].PowerCtrlDic)) return ControlConfigSaveRes.smoke_CtrlCfgIsExist;
                }
            }
            if (isAllInvalid) return ControlConfigSaveRes.smoke_CtrlCfgIsInvalid;
            return ControlConfigSaveRes.ok;
        }
        private bool IsSmokeConflict(string key, Dictionary<string, PowerCtrl_Type> smokeDic)
        {
            List<UC_WHControlConfig_Smoke_VM> smokeList = new List<UC_WHControlConfig_Smoke_VM>();
            foreach (var item in smokeDic)
            {
                if (item.Key == key && item.Value != PowerCtrl_Type.still) return true;
            }
            return false;
        }
        private List<Strategy> GetTemperatureStrategy(List<UC_WHControlConfig_Tem_VM> temStrategyList)
        {
            Strategy stra;
            List<Strategy> straList = new List<Strategy>();
            List<UC_WHControlConfig_Tem_VM> temList;
            Rule ru;
            Condition conGreater, conLess;
            ActionCommand actCmd;
            int smartBright_Threshold = 100;
            ConditionAlgorithm condiAlgor = ConditionAlgorithm.MaxValueAlgorithm;
            foreach (var item in MonitorAllConfig.Instance().LedInfoList)
            {
                if (item.Sn != _selectedScreenSN) continue;
                temList = temStrategyList.FindAll(a => a.SN == item.Sn);
                stra = new Strategy();
                stra.Type = StrategyType.TemperatureStrategy;
                stra.SN = item.Sn;
                if (temStrategyList.Count == 0) stra.Id = Guid.NewGuid();
                else stra.Id = temStrategyList[0].ID;
                stra.RuleTable = new List<Rule>();
                if (temList.Count != 0)
                {
                    for (int i = 0; i < temList.Count; i++)
                    {
                        ru = new Rule();
                        //初始化ConditionElement
                        ru.RuleCondition = new ConditionElement();
                        ru.RuleCondition.ConditionCollection = new List<Condition>();
                        conGreater = new Condition(OperatorType.GreaterThan, temList[i].GreaterThan, temList[i].ConditionAlgor, StateQuantityType.Temperature);
                        ru.RuleCondition.ConditionCollection.Add(conGreater);
                        conLess = new Condition(OperatorType.LessThan, temList[i].LessThan, temList[i].ConditionAlgor, StateQuantityType.Temperature);
                        ru.RuleCondition.ConditionCollection.Add(conLess);
                        //初始化ActionCommandElement
                        ru.RuleAction = new ActionCommandElement();
                        ru.RuleAction.ActionCommandCollection = new List<ActionCommand>();
                        condiAlgor = temList[i].ConditionAlgor;
                        #region 初始化亮度
                        if (temList[i].IsEnableBrightCtrl)
                        {
                            //调整亮度
                            if (temList[i].IsControlBrightness)
                            {
                                if (smartBright_Threshold > temList[i].GreaterThan) smartBright_Threshold = temList[i].GreaterThan;
                                actCmd = new ActionCommand();
                                actCmd.ActionType = ActionType.Set;
                                actCmd.ActionTarget = new ActionTarget();
                                actCmd.ActionTarget.TargetType = ActionTargetType.Parameter;
                                actCmd.ActionTarget.ParameterTarget = new StateObject();
                                actCmd.ActionTarget.ParameterTarget.Type = StateQuantityType.Brightness;
                                actCmd.ActionTarget.ParameterTarget.Value = temList[i].Brightness;
                                actCmd.ActionTarget.DeviceTarget = new List<string>();
                                ru.RuleAction.ActionCommandCollection.Add(actCmd);
                            }
                            else
                            {
                                //开启智能亮度
                            }
                        }
                        #endregion
                        #region 电源列表
                        if (temList[i].PowerCtrlDic.Count != 0)
                        {
                            //初始化打开电源列表
                            List<string> powerOpenList = new List<string>();
                            foreach (var status in temList[i].PowerCtrlDic)
                            {
                                if (status.Value == PowerCtrl_Type.open) powerOpenList.Add(status.Key);
                            }
                            if (powerOpenList.Count() != 0)
                            {
                                actCmd = new ActionCommand();
                                actCmd.ActionType = ActionType.Open;
                                actCmd.ActionTarget = new ActionTarget();
                                actCmd.ActionTarget.TargetType = ActionTargetType.Device;
                                actCmd.ActionTarget.DeviceTarget = powerOpenList;
                                ru.RuleAction.ActionCommandCollection.Add(actCmd);
                            }
                            //初始化关闭电源列表
                            List<string> powerCloseList = new List<string>();
                            foreach (var status in temList[i].PowerCtrlDic)
                            {
                                if (status.Value == PowerCtrl_Type.close) powerCloseList.Add(status.Key);
                            }
                            if (powerCloseList.Count() != 0)
                            {
                                actCmd = new ActionCommand();
                                actCmd.ActionType = ActionType.Close;
                                actCmd.ActionTarget = new ActionTarget();
                                actCmd.ActionTarget.TargetType = ActionTargetType.Device;
                                actCmd.ActionTarget.DeviceTarget = powerCloseList;
                                ru.RuleAction.ActionCommandCollection.Add(actCmd);
                            }
                        }
                        #endregion
                        stra.RuleTable.Add(ru);
                    }
                }
                #region 处理智能亮度
                if (smartBright_Threshold - 5 > 0)
                {
                    Rule ruBright = new Rule();
                    //初始化ConditionElement
                    ruBright.RuleCondition = new ConditionElement();
                    ruBright.RuleCondition.ConditionCollection = new List<Condition>();
                    conLess = new Condition(OperatorType.LessThan, smartBright_Threshold - 5, ConditionAlgorithm.MaxValueAlgorithm, StateQuantityType.Temperature);
                    ruBright.RuleCondition.ConditionCollection.Add(conLess);
                    //初始化ActionCommandElement
                    ruBright.RuleAction = new ActionCommandElement();
                    ruBright.RuleAction.ActionCommandCollection = new List<ActionCommand>();
                    ActionCommand actBright = new ActionCommand();
                    actBright.ActionType = ActionType.Enable;
                    actBright.ActionTarget = new ActionTarget();
                    actBright.ActionTarget.TargetType = ActionTargetType.SmartFunction;
                    ruBright.RuleAction.ActionCommandCollection.Add(actBright);
                    stra.RuleTable.Add(ruBright);
                }
                #endregion
                straList.Add(stra);
            }
            return straList;
        }
        private List<Strategy> GetSmokeStrategy(List<UC_WHControlConfig_Smoke_VM> smokeStrategyList)
        {
            Strategy stra;
            List<Strategy> straList = new List<Strategy>();
            if (smokeStrategyList == null || smokeStrategyList.Count == 0) return straList;
            //Dictionary<string, List<UC_WHControlConfig_Tem_VM>> temDic = new Dictionary<string, List<UC_WHControlConfig_Tem_VM>>();
            List<UC_WHControlConfig_Smoke_VM> smokeList;
            Rule ru;
            Condition conGreater;
            ActionCommand actCmd;
            foreach (var item in MonitorAllConfig.Instance().LedInfoList)
            {
                smokeList = smokeStrategyList.FindAll(a => a.SN == item.Sn);
                if (smokeList.Count != 0)
                {
                    stra = new Strategy();
                    stra.Type = StrategyType.SmokeStrategy;
                    stra.SN = item.Sn;
                    stra.Id = smokeStrategyList[0].ID;
                    stra.RuleTable = new List<Rule>();
                    for (int i = 0; i < smokeList.Count; i++)
                    {
                        ru = new Rule();
                        //初始化ConditionElement
                        ru.RuleCondition = new ConditionElement();
                        ru.RuleCondition.ConditionCollection = new List<Condition>();
                        conGreater = new Condition(OperatorType.GreaterThan, smokeList[i].GreaterThan, ConditionAlgorithm.MaxValueAlgorithm, StateQuantityType.Smoke);
                        ru.RuleCondition.ConditionCollection.Add(conGreater);
                        //初始化ActionCommandElement
                        ru.RuleAction = new ActionCommandElement();
                        ru.RuleAction.ActionCommandCollection = new List<ActionCommand>();
                        if (smokeList[i].PowerCtrlDic.Count != 0)
                        {
                            List<string> powerCloseList = new List<string>();
                            //初始化关闭电源列表
                            foreach (var status in smokeList[i].PowerCtrlDic)
                            {
                                if (status.Value == PowerCtrl_Type.close)
                                    powerCloseList.Add(status.Key);
                            }
                            if (powerCloseList.Count() != 0)
                            {
                                actCmd = new ActionCommand();
                                actCmd.ActionType = ActionType.Close;
                                actCmd.ActionTarget = new ActionTarget();
                                actCmd.ActionTarget.TargetType = ActionTargetType.Device;
                                actCmd.ActionTarget.DeviceTarget = powerCloseList;
                                ru.RuleAction.ActionCommandCollection.Add(actCmd);
                            }
                        }
                        stra.RuleTable.Add(ru);
                    }
                    straList.Add(stra);
                }
            }
            return straList;
        }
        #endregion
    }
}
