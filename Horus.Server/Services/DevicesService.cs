using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Model.Drivers;
using Horus.Model.Server;
using Horus.Server.Model;
using Kayak.Framework;
using Horus.Server.Context;

namespace Horus.Server.Services
{
    class DevicesService : HorusService
    {
        [Path("/devices/list")]
        [Verb("GET")]
        public void ListLogicalDevices(string sessionId)
        {

            Session session = ServerContext.Instance.GetSession(sessionId);
            List<HorusDeviceSummary> locigalDevices = session.LocalHorusSession.EnumDevices();

            XmlResponse(new HorusLogicalDeviceSummaryList(locigalDevices));
        }
    }
}
