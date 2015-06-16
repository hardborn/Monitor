using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class GlobalErrorHandler : IErrorHandler
    {
        // Provide a fault. The Message fault parameter can be replaced, or set to
        // null to suppress reporting a fault.

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var newEx = new FaultException(
                string.Format("Exception caught at GlobalErrorHandler{0}Method: {1}{2}Message:{3}",
                             Environment.NewLine, error.TargetSite.Name, Environment.NewLine, error.Message));

            MessageFault msgFault = newEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }

        // HandleError. Log an error, then allow the error to be handled as usual.
        // Return true if the error is considered as already handled

        public bool HandleError(Exception error)
        {
            //string path = HostingEnvironment.ApplicationPhysicalPath;

            //using (TextWriter tw = File.AppendText(Path.Combine(path, @"Logs\\error.txt")))
            //{
            //    if (error != null)
            //    {
            //        tw.WriteLine("Exception:{0}{1}Method: {2}{3}Message:{4}",
            //            error.GetType().Name, Environment.NewLine, error.TargetSite.Name,
            //            Environment.NewLine, error.Message + Environment.NewLine);
            //    }
            //    tw.Close();
            //}

            return true;
        }
    }
}
