using Newtonsoft.Json;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataDispatcher
{
    public class AllPhysicalDisplayInfoHandler:Handler
    {
        public AllPhysicalDisplayInfoHandler(ClientDispatcher client)
            : base(client)
        {

        }

        public override void Execute(string key,object data)
        {
            if (_client == null)
                return;

            LEDDisplayInfoCollection obj;
            using (var stringReader = new StringReader(data as string))
            {
                obj = JsonConvert.DeserializeObject<LEDDisplayInfoCollection>(data as string);
            }

            _client.NotifyPhysicalDisplayInfoUpdated(obj);
        }
    }
}
