using Nova.LCT.GigabitSystem.HWConfigAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class OpticalProbeConfig : ICloneable
    {
        //private string _ledSerialNumber = string.Empty;
        //private UseablePeripheral _deviceInfo = new UseablePeripheral();
        public string SN { get; set; }
        public List<UseablePeripheral> ConfigInfo { get; set; }

        public OpticalProbeConfig()
        {

        }
        private OpticalProbeConfig(OpticalProbeConfig config)
        {
            this.SN = string.IsNullOrEmpty(config.SN) ? string.Empty : config.SN;
            this.ConfigInfo = config.ConfigInfo == null ? null : config.ConfigInfo.Select(item => (UseablePeripheral)item.Clone()).ToList();
        }

        public object Clone()
        {
            OpticalProbeConfig device = new OpticalProbeConfig(this);
            return device;
        }
    }
}
