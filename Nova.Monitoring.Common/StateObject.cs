using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class StateObject:ICloneable
    {
        /// <summary>
        /// 状态类型
        /// </summary>
        public StateQuantityType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 状态值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        public StateObject() { }

        private StateObject(StateObject stateObject)
        {
            this.Type = stateObject.Type;
            this.Value = stateObject.Value;
        }

        public object Clone()
        {
            var obj = new StateObject(this);
            return obj;
        }
    }
}
