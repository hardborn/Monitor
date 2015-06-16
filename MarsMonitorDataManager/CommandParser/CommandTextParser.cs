using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json.Converters;

namespace Nova.Monitoring.ColudSupport
{
    public class CommandTextParser
    {
        public static object objLock = new object();

        public static string SerialCmdTextParamTo(TransferParams param)
        {
            return JsonConvert.SerializeObject(param);
        }

        public static TransferParams DeserialCmdTextToParam(string cmdTxt)
        {
            TransferParams param = (TransferParams)JsonConvert.DeserializeObject(cmdTxt, typeof(TransferParams));
            return param;
        }
        //public static T GetDeserialization<T, K>(K data) where T : class
        //{
        //    lock (objLock)
        //    {
        //        if (string.IsNullOrEmpty(data as string))
        //        {
        //            return null;
        //        }
        //        StringReader stringReader = new StringReader(data as string);
        //        System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(
        //            typeof(T));
        //        T obj = deserializer.Deserialize(stringReader) as T;
        //        stringReader.Close();
        //        return obj;
        //    }
        //}

        //public static string GetSerialization<T>(T obj)
        //{
        //    lock (objLock)
        //    {
        //        XmlSerializer deserializer = new XmlSerializer(typeof(T));
        //        StringWriter sw = new StringWriter();
        //        deserializer.Serialize(sw, obj);
        //        sw.Close();
        //        return sw.ToString();
        //    }
        //}

        public static string GetJsonSerialization<T>(T param)
        {
            return JsonConvert.SerializeObject(param);
        }

        public static string GetJsonSerialization<T>(T param, bool isTimeFormat)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();//这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "HH':'mm':'ss";
            return JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented, timeConverter);
        }

        public static T GetDeJsonSerialization<T>(string cmdTxt)
        {
            try
            {
                return (T)JsonConvert.DeserializeObject(cmdTxt, typeof(T));
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine("转换成Json出错：" + ex.ToString());
                return default(T);
            }
        }
    }
}