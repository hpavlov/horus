using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Horus.Client.Drivers;
using Horus.Client.System;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;

namespace Horus.Client.System
{
    // NOTE: This implementation is pretty naive and requires serious thought and further work
    internal class LocalHorusSession : HorusSession
    {
        private List<LocalHorusDriver> allLocalDrivers = null;

        private void EnsureLocalDrivers()
        {
            if (allLocalDrivers == null)
            {
                allLocalDrivers = new List<LocalHorusDriver>();
                allLocalDrivers.AddRange(NativeHorusDriversDriscoveryService.DriscoverAvailableDrivers());
                allLocalDrivers.AddRange(ASCOMDriversDiscoveryService.DriscoverAvailableDrivers());
            }            
        }

        private TInterface CreateDriverInstance<TInterface>(LocalHorusDriver localDriver) where TInterface : class
        {
            // HACK: Drivers should be loaded into separate AppDomains
            if (AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.FullName == localDriver.Assembly.GetName().FullName) == null)
            {
                AppDomain.CurrentDomain.Load(localDriver.Assembly.GetName());
            }

            object newInstance = Activator.CreateInstance(localDriver.Implementor);
            return newInstance as TInterface;
        }

        public override HorusDriverSummary[] EnumDrivers()
        {
            EnsureLocalDrivers();

            var rv = new List<HorusDriverSummary>();

            foreach (LocalHorusDriver localDriver in allLocalDrivers)
            {
                // NOTE: This is a rather simple implementation
                Type[] implementedHorusInterfaces = localDriver.Implementor.FindInterfaces((type, criteria) => typeof (IHorusDriver).IsAssignableFrom(type), null);
                rv.Add(new HorusDriverSummary()
                        {
                            DriverName = localDriver.Implementor.FullName,
                            SupportedInterfaces = implementedHorusInterfaces
                        }
               );
            }

            return rv.ToArray();
        }

        public override HorusDriverSummary[] EnumSimulators()
        {
            throw new NotImplementedException();
        }

        public override HorusDriver CreateDriverInstance(HorusDriverSummary driverSummary)
        {
            EnsureLocalDrivers();

            LocalHorusDriver localDriver =  allLocalDrivers.SingleOrDefault(x => x.Implementor.FullName == driverSummary.DriverName);

            if (localDriver != null)
            {
                IHorusDriver driverInterfaceInstance = CreateDriverInstance<IHorusDriver>(localDriver);
                driverInterfaceInstance.Initialize(new HorusContext(this));
                return new HorusDriver(driverInterfaceInstance);
            }

            return null;
        }

        public override HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary)
        {
            EnsureLocalDrivers();

            LocalHorusDriver localDriver = allLocalDrivers.SingleOrDefault(x => x.Implementor.FullName == deviceSummary.DeviceDriver.DriverName);

            if (localDriver != null)
            {
                IHorusDriver driverInterfaceInstance = CreateDriverInstance<IHorusDriver>(localDriver);
                driverInterfaceInstance.Initialize(new HorusContext(this));
                driverInterfaceInstance.LinkToDevice(deviceSummary.DeviceName);
                return new HorusCamera(driverInterfaceInstance);
            }

            return null;
        }

        public override HorusVideo CreateVideoInstance(HorusDeviceSummary deviceSummary)
        {
            EnsureLocalDrivers();

            LocalHorusDriver localDriver = allLocalDrivers.SingleOrDefault(x => x.Implementor.FullName == deviceSummary.DeviceDriver.DriverName);

            if (localDriver != null)
            {
                IHorusDriver driverInterfaceInstance = CreateDriverInstance<IHorusDriver>(localDriver);
                driverInterfaceInstance.Initialize(new HorusContext(this));
                driverInterfaceInstance.LinkToDevice(deviceSummary.DeviceName);                
                return new HorusVideo(driverInterfaceInstance);
            }

            return null;            
        }
    }
}
