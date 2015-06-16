using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{

    public class StrategyTable
    {
        private List<Strategy> _strategies;

        public List<Strategy> StrategyList
        {
            get { return _strategies; }
            set { _strategies = value; }
        }
        public StrategyTable()
        {
        }
        private StrategyTable(StrategyTable strategyTable)
        {
            this.StrategyList = strategyTable.StrategyList == null ? null : strategyTable.StrategyList.Select(T => (Strategy)T.Clone()).ToList();
        }

        public object Clone()
        {
            StrategyTable strategyTable = new StrategyTable(this);
            return strategyTable;
        }

    }
    public class Strategy
    {
        private Guid _id;
        private string _sn;
        private List<Rule> _ruleTable;
        private StrategyType _type;

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public string SN
        {
            get { return _sn; }
            set
            {
                _sn = value;
            }
        }

        public StrategyType Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        public List<Rule> RuleTable
        {
            get { return _ruleTable; }
            set
            {
                _ruleTable = value;
            }
        }
        public Strategy()
        {
        }
        private Strategy(Strategy strategy)
        {
            this.Id = strategy.Id;
            this.SN = string.IsNullOrEmpty(strategy.SN) ? string.Empty : strategy.SN.Clone() as string;
            this.Type = strategy.Type;
            this.RuleTable = strategy.RuleTable == null ? null : strategy.RuleTable.Select(T => (Rule)T.Clone()).ToList();
        }

        public object Clone()
        {
            Strategy strategy = new Strategy(this);
            return strategy;
        }
    }

    public class ActionCommandElement : ICloneable
    {
        private List<ActionCommand> _actionCommandCollection = new List<ActionCommand>();
        public List<ActionCommand> ActionCommandCollection
        {
            get
            { return _actionCommandCollection; }
            set
            {
                _actionCommandCollection = value;
            }
        }
        public ActionCommandElement()
        {
        }
        private ActionCommandElement(ActionCommandElement element)
        {
            this.ActionCommandCollection = element.ActionCommandCollection == null ? null : element.ActionCommandCollection.Select(T => (ActionCommand)T.Clone()).ToList();
        }

        public object Clone()
        {
            ActionCommandElement element = new ActionCommandElement(this);
            return element;
        }

    }

    public class ActionCommand : ICloneable
    {
        private ActionTarget _actionTarget;
        private ActionType _actionType;


        public ActionTarget ActionTarget
        {
            get { return _actionTarget; }
            set
            {
                _actionTarget = value;
            }
        }

        public ActionType ActionType
        {
            get { return _actionType; }
            set
            {
                _actionType = value;
            }
        }
        public ActionCommand()
        {
        }

        private ActionCommand(ActionCommand actionCommand)
        {
            this.ActionTarget = actionCommand.ActionTarget == null ? null : actionCommand.ActionTarget.Clone() as ActionTarget;
            this.ActionType = actionCommand.ActionType;
            // this.ParameterAlarmConfigList = config.ParameterAlarmConfigList == null ? null : config.ParameterAlarmConfigList.Select(T => (ParameterAlarmConfig)T.Clone()).ToList();
        }

        public object Clone()
        {
            ActionCommand actionCommand = new ActionCommand(this);
            return actionCommand;
        }
    }

    public class ActionTarget : ICloneable
    {
        private ActionTargetType _targetType;
        private List<string> _deviceTarget;
        private StateObject _parameterTarget;

        public ActionTargetType TargetType
        {
            get { return _targetType; }
            set { _targetType = value; }
        }

        public List<string> DeviceTarget
        {
            get { return _deviceTarget; }
            set
            {
                _deviceTarget = value;
            }
        }

        public StateObject ParameterTarget
        {
            get { return _parameterTarget; }
            set
            {
                _parameterTarget = value;
            }
        }

        public ActionTarget()
        {
        }
        private ActionTarget(ActionTarget target)
        {
            this.TargetType = target.TargetType;
            this.DeviceTarget = target.DeviceTarget == null ? null : target.DeviceTarget.Select(T => (string)T.Clone()).ToList();
            this.ParameterTarget = target.ParameterTarget;
        }

        public object Clone()
        {
            ActionTarget target = new ActionTarget(this);
            return target;
        }
    }

    public class Rule : ICloneable
    {
        private ConditionElement _conditionTable = new ConditionElement();
        private ActionCommandElement _actionCommandTable= new ActionCommandElement();

        public ConditionElement RuleCondition
        {
            get { return _conditionTable; }
            set
            {
                _conditionTable = value;
            }
        }

        public ActionCommandElement RuleAction
        {
            get { return _actionCommandTable; }
            set
            {
                _actionCommandTable = value;
            }
        }

        public Rule()
        {
        }
        private Rule(Rule rule)
        {
            this.RuleCondition = rule.RuleCondition == null ? null : rule.RuleCondition.Clone() as ConditionElement;
            this.RuleAction = rule.RuleAction == null ? null : rule.RuleAction.Clone() as ActionCommandElement;
            // this.ParameterAlarmConfigList = config.ParameterAlarmConfigList == null ? null : config.ParameterAlarmConfigList.Select(T => (ParameterAlarmConfig)T.Clone()).ToList();
        }

        public object Clone()
        {
            Rule rule = new Rule(this);
            return rule;
        }
    }

    public enum ActionType
    {
        Set,
        Open,
        Close,
        Enable,
        Disable
    }

    public enum ActionTargetType
    {
        Parameter,
        Device,
        SmartFunction
    }

    public enum ExpressionType
    {
        Arithmetic,
        Relation,
        Logical
    }
}
