/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Kayak.Framework;
using Horus.Model.Drivers;
using Horus.Model.Server;
using Horus.Server.Context;
using Horus.Server.Model;
using Horus.WebServices;

namespace Horus.Server.Services
{
    internal class HorusService : KayakService
    {
	    private DevicesService devicesService = new DevicesService();
		private DriversService driversService = new DriversService();

        [Path("/")]
		[Verb("GET")]
		public void Unknown()
		{
			Response.Write("UnmappedREequestUrl");
		}

		[Path("/devices/list")]
		[Verb("GET")]
		public void ListLogicalDevices(string sessionId)
		{
			Session session = ServerContext.Instance.GetSession(sessionId);

			HorusLogicalDeviceSummaryList rv = devicesService.ListLogicalDevices(session);

			XmlResponse(rv);
		}

		[Path("/drivers/{device}/create-instance")]
		[Verb("POST")]
		public void CreateDriverInstance(string device, string driverName, string interfaceName, string sessionId)
		{
			Session session = ServerContext.Instance.GetSession(sessionId);

			HorusDriverInstanceSummary rv = driversService.CreateDriverInstance(session, device, driverName, interfaceName);

			XmlResponse(rv);
		}

		[Path("/drivers/{instanceId}/call-method/{methodName}")]
		[Verb("POST")]
		public void DriverInstanceMethodCall(string instanceId, string methodName, string sessionId)
		{
			Session session = ServerContext.Instance.GetSession(sessionId);

			using (TextReader reader = new StreamReader(Request.InputStream))
			{
				string stringContent = reader.ReadToEnd();

				object returnValue = driversService.DriverInstanceMethodCall(session, instanceId, methodName, stringContent);

				if (returnValue != null)
					Response.Write(returnValue);
			}
		}

		[Path("/drivers/{instanceId}/property/{propertyName}")]
		[Verb("GET")]
		public void DriverInstancePropertyGet(string instanceId, string propertyName, string sessionId)
		{
			Session session = ServerContext.Instance.GetSession(sessionId);

			object returnValue = driversService.DriverInstancePropertyGet(session, instanceId, propertyName);

			if (returnValue != null)
				Response.Write(returnValue);
		}

		[Path("/drivers/{instanceId}/property/{propertyName}")]
		[Verb("POST")]
		public void DriverInstancePropertySet(string instanceId, string propertyName, string sessionId)
		{
			Session session = ServerContext.Instance.GetSession(sessionId);

			using (TextReader reader = new StreamReader(Request.InputStream))
			{
				string stringContent = reader.ReadToEnd();

				driversService.DriverInstancePropertySet(session, instanceId, propertyName, stringContent);
			}
		}

        protected void XmlResponse(object model)
        {
            if (model != null)
            {
                var ser = new XmlSerializer(model.GetType());
                var output = new StringBuilder();
                using (var wrt = new System.IO.StringWriter(output))
                {
                    ser.Serialize(wrt, model);
                }

                Response.Write(output.ToString());
                Response.Headers["Content-Type"] = "application/xml";
            }
        }
    }
}
