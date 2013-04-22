using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Server.Model;
using Kayak.Framework;

namespace Horus.Server.Services
{
	class SessionService : HorusService
	{
	    private static List<Session> allSessions = new List<Session>();
	    private static object syncRoot = new object();
            
        [Path("/session/new")]
		[Verb("POST")]
		public void NewSession(string userId)
		{
			Response.Write("UserID:" + userId);
		}



	}
}
