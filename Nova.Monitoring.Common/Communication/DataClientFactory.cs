using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Nova.Monitoring.Common
{
    public class DataClientFactory
    {
        public static IDataService CreateDataServiceTcpProxy(IDataClient dataReceiver)
        {
            return CreateDataServiceTcpProxy(dataReceiver, "127.0.0.1", 8888);
        }

        public static IDataService CreateDataServiceTcpProxy(IDataClient dataReceiver, string hostname)
        {
            return CreateDataServiceTcpProxy(dataReceiver, hostname, 8888);
        }
        public static IDataService CreateDataServiceTcpProxy(IDataClient dataReceiver, string hostname, int port)
        {
            //var factory =
            //    new DuplexChannelFactory<IDataService>(new InstanceContext(dataReceiver), CommunicationSettingsFactory.CreateDataServicePipeBinding(TimeSpan.MaxValue));

            //var builder = new UriBuilder();
            //builder.Scheme = Uri.UriSchemeNetPipe;
            //builder.Host = hostname;

            var factory = new DuplexChannelFactory<IDataService>(new InstanceContext(dataReceiver), CommunicationSettingsFactory.CreateDataServiceTcpBinding(TimeSpan.MaxValue));
            var builder = new UriBuilder();
            builder.Scheme = Uri.UriSchemeNetTcp;
            builder.Host = hostname;
            builder.Path = "/Data";
            builder.Port = port;


            var address = new EndpointAddress(builder.Uri);

            return factory.CreateChannel(address);
        }

        public static IDataService CreateDataServicePipeProxy(IDataClient dataReceiver, string hostname)
        {
            var factory =
                new DuplexChannelFactory<IDataService>(new InstanceContext(dataReceiver), CommunicationSettingsFactory.CreateDataServicePipeBinding(TimeSpan.MaxValue));

            var builder = new UriBuilder();
            builder.Scheme = Uri.UriSchemeNetPipe;
            builder.Host = hostname;
            builder.Path = "/IDataService";
            var address = new EndpointAddress(builder.Uri);
            return factory.CreateChannel(address);
        }
    }
}
