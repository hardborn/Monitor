using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class BrightnessLog
    {
        public string Id { get; set; }
        public string Timestamp { get; set; }
        public float BrightnessValue { get; set; }
        public DimmingMode OperationType { get; set; }
        public BrightnessLogResult Result { get; set; }
    }

    public enum DimmingMode
    {
        FixedBrightness,
        AutoBrightness,
        ManualBrightness,
        SmartBrightness,
        RuleBrightness,
    }

    public enum BrightnessLogResult
    {
        Success,
        Failure
    }
}
