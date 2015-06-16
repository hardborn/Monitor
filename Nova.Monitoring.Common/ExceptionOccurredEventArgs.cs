using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class ExceptionOccurredEventArgs : EventArgs
    {
        public string DataSourceName { get; set; }
        public Exception Exception { get; set; }
        public ExceptionOccurredEventArgs(string dataSourceName, Exception exception)
        {
            Exception = exception;
        }
    }
}
