using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Horus.Client.Drivers;
using Horus.Client.System;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;

namespace Horus.ClientFrameWork.CS.System
{
    // NOTE: This implementation is pretty naive and requires serious thought and further work
    public class LocalHorusSession : HorusSession
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
            ObjectHandle newInstanceHandle = Activator.CreateInstance(localDriver.Assembly.GetName().FullName, localDriver.Implementor.FullName);
            return newInstanceHandle.Unwrap() as TInterface;
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
                return new HorusDriver(driverInterfaceInstance);
            }

            return null;
        }

        public override HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary)
        {
            EnsureLocalDrivers();

            throw new NotImplementedException();
        }

        public override HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary, string driverName)
        {
            if (!deviceSummary.AvailableDrivers.Exists(x => x.DriverName == driverName))
                throw new ArgumentException("The specified driver cannot connect to the specified device.");

            EnsureLocalDrivers();

            LocalHorusDriver localDriver = allLocalDrivers.SingleOrDefault(x => x.Implementor.FullName == driverName);

            if (localDriver != null)
            {
                IHorusDriver driverInterfaceInstance = CreateDriverInstance<IHorusDriver>(localDriver);
                return new HorusCamera(driverInterfaceInstance);
            }

            return null;
        }
    }
}
