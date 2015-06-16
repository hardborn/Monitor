using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nova.Monitoring.Common
{
    [DataContract]
    [KnownType(typeof(int[]))]
    [KnownType(typeof(string[]))]
    [KnownType(typeof(byte[]))]
    [KnownType(typeof(double[]))]
    [KnownType(typeof(float[]))]
    public class DataPoint
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public object Value { get; set; }
    }
}
