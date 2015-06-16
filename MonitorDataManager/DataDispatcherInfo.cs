using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nova.Monitoring.MonitorDataManager
{
    /// <summary>
    /// 数据分发器
    /// </summary>
    [Serializable]
    public class DataDispatcherInfo
    {
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string Type { get; set; }
        [XmlAttribute()]
        public string Location { get; set; }
    }
}
