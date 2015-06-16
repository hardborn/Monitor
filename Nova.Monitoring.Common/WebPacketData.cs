using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nova.Monitoring.Common
{
    [DataContract]
    public class WebPacketData
    {
        /// <summary>
        /// 数据包标识
        /// </summary>
        [DataMember]
        public string Identifier { get; set; }

        /// <summary>
        /// 数据包类型
        /// </summary>
        [DataMember]
        public PacketDataType IdentifierClass { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [DataMember]
        public string Timestamp { get; set; }

        /// <summary>
        /// 数据包序列号（可选）
        /// </summary>
        [DataMember]
        public string SequenceNumber { get; set; }

        /// <summary>
        /// 数据点集合
        /// </summary>
        [DataMember]
        public string DataPointCollection { get; set; }

    }

    [DataContract]
    public enum PacketDataType
    {
        [EnumMember]
        MonitoringData = 1
    }
}
