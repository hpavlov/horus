using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;

namespace Horus.WebServices.Interfaces
{
    public interface ISession
    {
		HorusSession LocalHorusSession { get; }
		//string SessionId { get; }
		//string UserId { get; }
		//DateTime SessionExpiryTime { get; }
	    void AddSessionObject(string objectId, object instance);
	    object GetSessionObject(string objectId);
    }
}
