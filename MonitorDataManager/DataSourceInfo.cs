using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nova.Monitoring.MonitorDataManager
{
    /// <summary>
    /// 数据源的采集对象
    /// </summary>
    [Serializable]
    public class DataSourceInfo
    {
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string Type { get; set; }
        [XmlAttribute()]
        public string Location { get; set; }
    }
}
