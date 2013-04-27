using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Server.Context;
using Horus.Server.Model;
using Kayak.Framework;

namespace Horus.Server.Services
{
	class SessionService : HorusService
	{           
        [Path("/session/new")]
		[Verb("POST")]
        public void NewSession(string userId)
        {
            // NOTE: THIS IS VERY DANGEROUS SECURITY RISK !!! ONLY USED FOR THIS DUMMY DEMO
            Session session = ServerContext.Instance.GetSessionByUserId(userId);

            if (session == null)
            {
                HorusSession horusSession = HorusSession.CreateLocalSession();
                string sessionId = Guid.NewGuid().ToString();

                session = new Session()
                {
                    SessionId = sessionId,
                    LocalHorusSession = horusSession,
                    UserId = userId,
                    SessionExpiryTime = DateTime.UtcNow.AddHours(1)
                };

                ServerContext.Instance.AddSession(session, sessionId);                
            }

            Response.Write(session.SessionId);
		}
	}
}
