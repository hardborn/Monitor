using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nova.Monitoring.Common
{

    public class ConditionElement
    {
        private List<Condition> _conditionCollection = new List<Condition>();
        public List<Condition> ConditionCollection
        {
            get { return _conditionCollection; }
            set
            {
                _conditionCollection = value;
            }
        }
        public ConditionElement()
        {
        }
        private ConditionElement(ConditionElement element)
        {
            this.ConditionCollection = element.ConditionCollection == null ? null : element.ConditionCollection.Select(T => (Condition)T.Clone()).ToList();
            // this.ParameterAlarmConfigList = config.ParameterAlarmConfigList == null ? null : config.ParameterAlarmConfigList.Select(T => (ParameterAlarmConfig)T.Clone()).ToList();
        }

        public object Clone()
        {
            ConditionElement element = new ConditionElement(this);
            return element;
        }
    }
    public class Condition
    {
        private Guid _id;
        private OperatorType _operator;
        private StateQuantityType _conditionParameter;
        private ConditionAlgorithm _algorithm;
        private string _leftExpression;
        private int _rightExpression;

        public Condition(OperatorType operatorType, int right, ConditionAlgorithm algorithm, StateQuantityType conditionParameter)
        {
            _id = Guid.NewGuid();
            _operator = operatorType;
            _algorithm = algorithm;
            //_leftExpression = left;
            _rightExpression = right;
            _conditionParameter = conditionParameter;
        }


        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public OperatorType Operator
        {
            get { return _operator; }
            set
            {
                _operator = value;
            }
        }

        public StateQuantityType ConditionParameter
        {
            get { return _conditionParameter; }
            set
            {
                _conditionParameter = value;
            }
        }

        public ConditionAlgorithm Algorithm
        {
            get { return _algorithm; }
            set
            { _algorithm = value; }
        }

        public int RightExpression
        {
            get { return _rightExpression; }
            set
            {
                _rightExpression = value;
            }
        }


        public Condition()
        {
        }
        private Condition(Condition condition)
        {
            this.Algorithm = condition.Algorithm;
            this.ConditionParameter = condition.ConditionParameter;
            this.Id = condition.Id;
            this.Operator = condition.Operator;
        }

        public object Clone()
        {
            Condition element = new Condition(this);
            return element;
        }
    }

    public enum OperatorType
    {
        GreaterThan,
        LessThan
    }

    public enum ConditionAlgorithm
    {
        MaxValueAlgorithm,
        AverageAlgorithm
    }
}
