using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class ComponentBase : IDevice, IMonitoringConfig,ICloneable
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DeviceType Type
        {
            get;
            set;
        }

        public List<MonitoringParameter> Parameters
        {
            get;
            set;
        }

        public bool MonitoringEnable
        {
            get;
            set;
        }

        public string PositionInfo
        {
            get;
            set;
        }

         public ComponentBase()
        {
        }
         private ComponentBase(ComponentBase component)
        {
           this.Id = component.Id.Clone() as string;
            this.MonitoringEnable = component.MonitoringEnable;
            this.Name = component.Name.Clone() as string;
            this.Parameters = component.Parameters.Select(t =>(MonitoringParameter) t.Clone()).ToList() as List<MonitoringParameter>;
            this.PositionInfo = component.PositionInfo as string;
            this.Type = component.Type;
                
        }


        public object Clone()
        {
            var component = new ComponentBase(this);
            return component;
        }
    }
}
