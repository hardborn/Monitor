using Nova.LCT.GigabitSystem.Common;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class LEDDisplayInfoCollection
    {
        public LEDDisplayInfoCollection()
        {
            LedSimples = new SerializableDictionary<string, List<SimpleLEDDisplayInfo>>();
            LedStandards = new SerializableDictionary<string, List<StandardLEDDisplayInfo>>();
            LedComplex = new SerializableDictionary<string, List<ComplexLEDDisplayInfo>>();
        }
        public SerializableDictionary<string, List<SimpleLEDDisplayInfo>> LedSimples { get; set; }
        public SerializableDictionary<string, List<StandardLEDDisplayInfo>> LedStandards { get; set; }
        public SerializableDictionary<string, List<ComplexLEDDisplayInfo>> LedComplex { get; set; }
    }


}
