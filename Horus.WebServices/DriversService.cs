using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horus.Client.Drivers;
using Horus.Client.System.Persisters;
using Horus.Model.Drivers;
using Horus.Model.Helpers;
using Horus.Model.Interfaces;
using Horus.Model.Server;
using Horus.WebServices.Interfaces;
using Horus.ebServices.Helpers;

namespace Horus.WebServices
{
	public class DriversService
	{
		public HorusDriverInstanceSummary CreateDriverInstance(ISession session, string device, string driverName, string interfaceName)
		{
			if (string.Equals(interfaceName, "ICamera", StringComparison.InvariantCultureIgnoreCase))
			{
				HorusDeviceSummary summary = session.LocalHorusSession.EnumDevices<ICamera>()
							.FirstOrDefault(x =>
								string.Equals(x.DeviceName, device, StringComparison.CurrentCultureIgnoreCase) &&
								string.Equals(x.DeviceDriver.DriverName, driverName, StringComparison.CurrentCultureIgnoreCase));

				HorusCamera camera = session.LocalHorusSession.CreateCameraInstance(summary);
				string objectId = Guid.NewGuid().ToString();

				session.AddSessionObject(objectId, camera);

				return new HorusDriverInstanceSummary(objectId);
			}
			else if (string.Equals(interfaceName, "IVideo", StringComparison.InvariantCultureIgnoreCase))
			{
				HorusDeviceSummary summary = session.LocalHorusSession.EnumDevices<IVideo>()
							.FirstOrDefault(x =>
								string.Equals(x.DeviceName, device, StringComparison.CurrentCultureIgnoreCase) &&
								string.Equals(x.DeviceDriver.DriverName, driverName, StringComparison.CurrentCultureIgnoreCase));

				HorusVideo video = session.LocalHorusSession.CreateVideoInstance(summary);
				string objectId = Guid.NewGuid().ToString();
				session.AddSessionObject(objectId, video);

				return new HorusDriverInstanceSummary(objectId);
			}

			return null;
		}

		public object DriverInstanceMethodCall(ISession session, string instanceId, string methodName, string contentBody)
		{
			object deviceInstance = session.GetSessionObject(instanceId);

			// TODO: deserialize a MethodCallParametersList from the request body (NOTE: check content type for JSON or XML)
			MethodCallParametersList paramList = contentBody.AsDeserialized<MethodCallParametersList>();

			object returnValue = DeviceInterfaceCaller.MakeDeviceMethodCall(deviceInstance, methodName, paramList);

			return returnValue;
		}

		public object DriverInstancePropertyGet(ISession session, string instanceId, string propertyName)
		{
			object deviceInstance = session.GetSessionObject(instanceId);

			object returnValue = DeviceInterfaceCaller.DevicePropertyGet(deviceInstance, propertyName);

			if (returnValue != null)
			{
				// TODO: Need to come up with some sort of registration driven model persister
				//       This is probably a naive implementation

				IModelPersister modelPersister = ModelPersister.Instance.GetCustomPersister(returnValue.GetType());
				if (modelPersister != null)
				{
					string content = modelPersister.ToHttpResponse(returnValue);
					return content;
				}
				else
					return returnValue;
			}

			return null;
		}

		public void DriverInstancePropertySet(ISession session, string instanceId, string propertyName, string requestBody)
		{
			object deviceInstance = session.GetSessionObject(instanceId);

			// TODO: deserialize a MethodCallParametersList from the request body (NOTE: check content type for JSON or XML)
			MethodCallParametersList paramList = requestBody.AsDeserialized<MethodCallParametersList>();

			DeviceInterfaceCaller.DevicePropertySet(deviceInstance, propertyName, paramList);
		}
	}
}
