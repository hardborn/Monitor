using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;

namespace Nova.Monitoring.Engine
{
    public class WebDataService : IWebDataService
    {
        public List<DataPoint> GetDataList(string[] identity)
        {
            return DataEngine.GetDataList(identity);
        }

        public void SendCommand(Command command)
        {
            DataEngine.AddCommand(command);
        }

        public void SendData(string identity, object data)
        {
            DataEngine.SendData(identity, data);
        }

        public DataPoint[] GetAllData()
        {
            return DataEngine.GetAllData();
        }

        public DataPoint GetData(string identity)
        {
            return DataEngine.GetData(identity);
        }

        public Stream ViewData(string mode)
        {
            if (!string.IsNullOrEmpty(mode) && mode.ToLower() == "wap")
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/vnd.wap.wml";
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<?xml version=\"1.0\" encoding=\"UTF-8\"?><wml><card title=\"Data\"><p><strong>DataEngine© current data:</strong></p><p><anchor>Refresh<go href=\"view?mode=wap&t={0}\"/></anchor></p><p><table columns=\"3\" title=\"Data:\">", DateTime.Now.Millisecond);
                DataPoint[] dps = GetAllData();

                foreach (DataPoint dp in dps)
                {
                    string row = "<tr><td><strong>{0}</strong></td><td>{2}</td><td>{1}</td></tr>";
                    sb.AppendFormat(row, dp.Key, dp.Value, "|");
                }
                sb.Append("</table></p></card></wml>");
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
                return ms;
            }
            else
                if (!string.IsNullOrEmpty(mode) && mode.ToLower() == "table")
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/html;charset=utf-8";

                    DataPoint[] dps = GetAllData();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table border=1><tr><th>Keys</th><th>Value</th></tr>");
                    foreach (DataPoint dp in dps)
                    {
                        if (dp.Value.GetType() == typeof(List<DataPoint>))
                        {
                            foreach (var dataPointItem in (dp.Value as List<DataPoint>))
                            {
                                string row = "<tr><td class=\"identity\">{0}</td><td class=\"value\">{1}</td></tr>";
                                sb.AppendFormat(row, "[" + dp.Key + "]\r\n" + dataPointItem.Key, dataPointItem.Value);
                            }
                        }
                        else if (dp.Value.GetType() == typeof(DataPoint))
                        {
                            var point = dp.Value as DataPoint;
                            if (point.Value.GetType() == typeof(List<DataPoint>))
                            {
                                foreach (var dataPointItem in (point.Value as List<DataPoint>))
                                {
                                    string row = "<tr><td class=\"identity\">{0}</td><td class=\"value\">{1}</td></tr>";
                                    sb.AppendFormat(row, "[" + point.Key + "]\r\n" + dataPointItem.Key, dataPointItem.Value);
                                }
                            }
                            else
                            {
                                string row = "<tr><td class=\"identity\">{0}</td><td class=\"value\">{1}</td></tr>";
                                sb.AppendFormat(row, point.Key, point.Value);
                            }
                        }
                        else
                        {
                            string row = "<tr><td class=\"identity\">{0}</td><td class=\"value\">{1}</td></tr>";
                            sb.AppendFormat(row, dp.Key, dp.Value);
                        }
                    }
                    sb.Append("</table>");

                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
                    return ms;
                }
                else
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/html;charset=utf-8";
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(Nova.Monitoring.Engine.Resource.View));
                    return ms;
                }
        }


        public DataPoint[] GetDataArray(string[] identity)
        {
            throw new NotImplementedException();
        }
    }
}
