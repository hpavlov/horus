using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Horus.Client.Drivers;
using Horus.Client.System;
using Horus.Client.System.Persisters;
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

            if (string.Equals(interfaceName, "ICamera", StringComparison.InvariantCultureIgnoreCase))
            {
                HorusDeviceSummary summary = session.LocalHorusSession.EnumDevices<ICamera>()
                            .FirstOrDefault(x =>
                                string.Equals(x.DeviceName, device, StringComparison.CurrentCultureIgnoreCase) &&
                                string.Equals(x.DeviceDriver.DriverName, driverName, StringComparison.CurrentCultureIgnoreCase));

                HorusCamera camera = session.LocalHorusSession.CreateCameraInstance(summary);
                string objectId = Guid.NewGuid().ToString();
                ServerContext.Instance.AddSessionObject(sessionId, objectId, camera);

                XmlResponse(new HorusDriverInstanceSummary(objectId));
            }
            else if (string.Equals(interfaceName, "IVideo", StringComparison.InvariantCultureIgnoreCase))
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

                object returnValue = DeviceInterfaceCaller.MakeDeviceMethodCall(deviceInstance, methodName, paramList);

                if (returnValue != null)
                {
                    Response.Write(returnValue);
                }
            }
        }

        [Path("/drivers/{instanceId}/property/{propertyName}")]
        [Verb("GET")]
        public void DriverInstancePropertyGet(string instanceId, string propertyName, string sessionId)
        {
            object deviceInstance = ServerContext.Instance.GetSessionObject(sessionId, instanceId);

            object returnValue = DeviceInterfaceCaller.DevicePropertyGet(deviceInstance, propertyName);

            if (returnValue != null)
            {
                // TODO: Need to come up with some sort of registration driven model persister
                //       This is probably a naive implementation

                IModelPersister modelPersister =  ModelPersister.Instance.GetCustomPersister(returnValue.GetType());
                if (modelPersister != null)
                {
                    string content = modelPersister.ToHttpResponse(returnValue);
                    Response.Write(content);
                }
                else
                    Response.Write(returnValue);
            }
        }

        [Path("/drivers/{instanceId}/property/{propertyName}")]
        [Verb("POST")]
        public void DriverInstancePropertySet(string instanceId, string propertyName, string sessionId)
        {
            object deviceInstance = ServerContext.Instance.GetSessionObject(sessionId, instanceId);
            using (TextReader reader = new StreamReader(Request.InputStream))
            {
                string stringContent = reader.ReadToEnd();

                // TODO: deserialize a MethodCallParametersList from the request body (NOTE: check content type for JSON or XML)
                MethodCallParametersList paramList = stringContent.AsDeserialized<MethodCallParametersList>();

                DeviceInterfaceCaller.DevicePropertySet(deviceInstance, propertyName, paramList);
            }
        }
    }
}
