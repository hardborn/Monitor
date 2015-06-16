using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Nova.Monitoring.Common
{
    public class CommunicationSettingsFactory
    {
        public static NetTcpBinding DataServiceTcpBinding
        {
            get
            {
                var tcpBinding = new NetTcpBinding(SecurityMode.None);
                tcpBinding.MaxBufferPoolSize = 2147483647;
                tcpBinding.MaxReceivedMessageSize = 2147483647;
                tcpBinding.MaxBufferSize = 2147483647;
                tcpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
                tcpBinding.ReaderQuotas.MaxDepth = 2147483647;
                tcpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                tcpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                tcpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
                tcpBinding.SendTimeout = TimeSpan.MaxValue;
                tcpBinding.ReceiveTimeout = TimeSpan.MaxValue;
                tcpBinding.ReliableSession.InactivityTimeout = TimeSpan.MaxValue;
                return tcpBinding;
            }
        }

        public static NetTcpBinding CreateDataServiceTcpBinding(TimeSpan timeout)
        {
            var tcpBinding = new NetTcpBinding(SecurityMode.None);
            tcpBinding.TransactionProtocol = TransactionProtocol.OleTransactions;
            tcpBinding.TransferMode = TransferMode.Buffered;
            tcpBinding.MaxBufferPoolSize = 2147483647;
            tcpBinding.MaxReceivedMessageSize = 2147483647;
            tcpBinding.MaxBufferSize = 2147483647;
            tcpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            tcpBinding.ReaderQuotas.MaxDepth = 2147483647;
            tcpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            tcpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            tcpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            tcpBinding.SendTimeout = new TimeSpan(0, 0, 10);
            tcpBinding.ReceiveTimeout = TimeSpan.MaxValue;
            tcpBinding.ReliableSession.InactivityTimeout = TimeSpan.MaxValue;

            return tcpBinding;
        }

        public static NetNamedPipeBinding CreateDataServicePipeBinding(TimeSpan timeout)
        {
            var pipeBinding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            pipeBinding.TransactionProtocol = TransactionProtocol.OleTransactions;
            pipeBinding.TransferMode = TransferMode.Buffered;
            pipeBinding.MaxBufferPoolSize = 2147483647;
            pipeBinding.MaxReceivedMessageSize = 2147483647;
            pipeBinding.MaxBufferSize = 2147483647;
            pipeBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            pipeBinding.ReaderQuotas.MaxDepth = 2147483647;
            pipeBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            pipeBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            pipeBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            pipeBinding.SendTimeout = new TimeSpan(0, 00, 20);
            pipeBinding.OpenTimeout = new TimeSpan(0, 0, 30);
            pipeBinding.ReceiveTimeout = TimeSpan.MaxValue;
            pipeBinding.CloseTimeout = TimeSpan.MaxValue;
            // pipeBinding.ReliableSession.InactivityTimeout = TimeSpan.MaxValue;
            
            return pipeBinding;
        }
    }
}
