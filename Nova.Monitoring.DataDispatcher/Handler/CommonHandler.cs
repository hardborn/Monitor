using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataDispatcher
{
    public class CommonHandler:Handler
    {
        public CommonHandler(ClientDispatcher client)
            : base(client)
        {

        }

        public override void Execute(string key,object data)
        {
            if (_client == null)
                return;

            _client.NotifyDataReceived(key,data);
        }
    }
}
