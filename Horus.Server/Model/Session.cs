using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;

namespace Horus.Server.Model
{
    public class Session
    {
        public HorusSession LocalHorusSession;
        public string SessionId;
        public string UserId;
        public DateTime SessionExpiryTime;
    }
}
