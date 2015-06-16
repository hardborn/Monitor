using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public interface IDataDispatcher
    {
        void Initialize(string host, int port);
    }
}
