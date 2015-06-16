using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nova.Monitoring.Common;

namespace Nova.Monitoring.Engine
{
    public class CommandManageQueue : DataManageQueue, IDisposable
    {
        protected override void CommandExecute(object cmd)
        {
            if (cmd is Command)
            {
                DataEngine.ExecuteCommand((Command)cmd);
            }
        }
        //protected override void CommandExecute(DataPoint dp)
        //{ }
        //protected override void CommandExecute(string key, object data)
        //{ }
    }
}
