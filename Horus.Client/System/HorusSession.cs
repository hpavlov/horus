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
                .Where(x => x.SupportedInterfaces.Contains(typeof (TSupportedInterface)))
                .ToArray();
        }

        public virtual List<HorusDeviceSummary> EnumDevices<TSupportedInterface>()
        {
            var rv = new List<HorusDeviceSummary>();

            HorusDriverSummary[] drivers = EnumDrivers<TSupportedInterface>();

            foreach(HorusDriverSummary driver in drivers)
            {
                HorusDriver instance = CreateDriverInstance(driver);

                HorusEnabledDeviceSummary[] deviceSummaries =  instance.GetAvailableDevices();
                foreach(HorusEnabledDeviceSummary device in deviceSummaries)
                {
                    // TODO: There will be two different questions thay will need to be answered here
                    // (1) The Horus System enumerating all availabel devices and all drivers that can control them and
                    // (2) The client asking for a device to control (which should be tied to the configuration of a specific device-driver pair configured by the Admin)
                    //
                    // This dummy implementation assumes only one driver will be available for each device
                    HorusDeviceSummary deviceSummary = rv.SingleOrDefault(x => x.DeviceName == device.DeviceName);
                    if (deviceSummary == null)
                    {
                        deviceSummary = new HorusDeviceSummary()
                        {
                            DeviceName = device.DeviceName, 
                            IsAvailable = device.IsAvailable,
                            DeviceDriver = driver
                        };
                        rv.Add(deviceSummary);
                    }
                }
            }

            return rv;
        }
    }
}
