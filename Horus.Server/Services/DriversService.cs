using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.ClientFrameWork.CS.System;
using Horus.Model.Server;
using Horus.Server.Helpers;
using Kayak.Framework;

namespace Horus.Server.Services
{
    class DriversService : HorusService
    {
        [Path("/drivers/list")]
        [Verb("GET")]
        public void ListRegisteredDrivers(string sessionId)
        {
            var session = new LocalHorusSession();

            XmlResponse(new HorusDriverSummaryList());
        }


        [Path("/drivers/{driver}/{interfaceName}/{member}")]
        [Verb("GET")]
        public void DriverInterfaceMemberCall(string driver, string interfaceName, string member, string sessionId)
        {
            Response.Write(string.Format("HorusServiceLoopback: driver={0};interface={1};member={2};sessionId={3}", driver, interfaceName, member, sessionId));
        }
    }
}
