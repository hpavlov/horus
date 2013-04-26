using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.Drivers;
using Horus.Model.Drivers;

namespace Horus.Client.System
{
    public abstract class HorusSession
    {
        public static HorusSession CreateLocalSession()
        {
            return new LocalHorusSession();    
        }

        public static HorusSession CreateRemoteSession(Uri serviceUri, string userName, string password)
        {
            var rv = new RemoteHorusSession(serviceUri);
            rv.Login(userName, password);

            return rv;
        }

        protected HorusDriverSummary[] availableDrivers = null;

        public abstract HorusDriverSummary[] EnumDrivers();
        public abstract HorusDriverSummary[] EnumSimulators();

        public abstract List<HorusDeviceSummary> EnumDevices();
        public abstract List<HorusDeviceSummary> EnumDevices<TSupportedInterface>();

        
        // NOTE: To deal with changing interface version at client side in .NET applcation we use abstract classes (such as HorusCamera)
        //       When a new driver interface becomes available and supported by the platform, the new methods will ne added to this HorusSession class
        public abstract HorusDriver CreateDriverInstance(HorusDriverSummary driverSummary);
        public abstract HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary);
        public abstract HorusVideo CreateVideoInstance(HorusDeviceSummary deviceSummary);

        private void EnsureAvailableDrivers()
        {
            if (availableDrivers == null)
            {
                availableDrivers = EnumDrivers();
                if (availableDrivers == null)
                    throw new InvalidOperationException("Cannot enumerate drivers right now or drivers cannot be enumrated.");
            }
        }

        public virtual HorusDriverSummary[] EnumDrivers<TSupportedInterface>()
        {
            EnsureAvailableDrivers();

            return availableDrivers
                .Where(x => x.SupportedInterfaces.Contains(typeof (TSupportedInterface).FullName))
                .ToArray();
        }



    }
}
