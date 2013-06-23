/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Server.Context;
using Horus.WebServices.Interfaces;

namespace Horus.Server.Model
{
	public class Session : ISession
	{
		public HorusSession LocalHorusSession { get; internal set; }

		public string SessionId;

		public string UserId;

		public DateTime SessionExpiryTime;

		public void AddSessionObject(string objectId, object instance)
		{
			ServerContext.Instance.AddSessionObject(SessionId, objectId, instance);
		}

		public object GetSessionObject(string objectId)
		{
			return ServerContext.Instance.GetSessionObject(SessionId, objectId);
		}
	}
}
