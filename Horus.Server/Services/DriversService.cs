using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Horus.Client.Drivers;
using Horus.Client.System;
using Horus.Model.Drivers;
using Horus.Model.Helpers;
using Horus.Model.Interfaces;
using Horus.Model.Server;
using Horus.Server.Context;
using Horus.Server.Helpers;
using Horus.Server.Model;
using Kayak.Framework;

namespace Horus.Server.Services
{
    class DriversService : HorusService
    {
        // TODO: We would want to use a unique {deviceid} here, that is asigned and saved for the logical device when it is created
        // TODO: Somewhere early in the pipe line, all calls should be intercepted and the HorusSession should be passed here as parameter
        //       Also think about where is a better place to save the list of instanciated devices - the HorusSession or the ServerContext?
        // TODO: How do we represent different installed version of the same driver on the server (i.e. (1) do we allow this, and (2) What will be the device name for them)

        [Path("/drivers/{device}/create-instance")]
        [Verb("POST")]
        public void CreateDriverInstance(string device, string driverName, string interfaceName, string sessionId)
        {
            Session session = ServerContext.Instance.GetSession(sessionId);
        
            if (string.Equals(interfaceName, "IVideo", StringComparison.InvariantCultureIgnoreCase))
            {
                HorusDeviceSummary summary = session.LocalHorusSession.EnumDevices<IVideo>()
                            .FirstOrDefault(x => 
                                string.Equals(x.DeviceName, device, StringComparison.CurrentCultureIgnoreCase) &&
                                string.Equals(x.DeviceDriver.DriverName, driverName, StringComparison.CurrentCultureIgnoreCase));

                HorusVideo video = session.LocalHorusSession.CreateVideoInstance(summary);
                string objectId = Guid.NewGuid().ToString();
                ServerContext.Instance.AddSessionObject(sessionId, objectId, video);

                XmlResponse(new HorusDriverInstanceSummary(objectId));
            }
        }


        [Path("/drivers/{instanceId}/call-method/{methodName}")]
        [Verb("POST")]
        public void DriverInstanceMethodCall(string instanceId, string methodName, string sessionId)
        {
            object deviceInstance = ServerContext.Instance.GetSessionObject(sessionId, instanceId);

            using (TextReader reader = new StreamReader(Request.InputStream))
            {
                string stringContent = reader.ReadToEnd();

                // TODO: deserialize a MethodCallParametersList from the request body (NOTE: check content type for JSON or XML)
                MethodCallParametersList paramList = stringContent.AsDeserialized<MethodCallParametersList>();

                DeviceInterfaceCaller.MakeDeviceMethodCall(deviceInstance, methodName, paramList);
            }
        }
    }
}
