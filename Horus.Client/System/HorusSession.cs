﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.Drivers;
using Horus.Model.Drivers;

namespace Horus.ClientFrameWork.CS.System
{
    public abstract class HorusSession
    {
        protected HorusDriverSummary[] availableDrivers = null;

        public abstract HorusDriverSummary[] EnumDrivers();
        public abstract HorusDriverSummary[] EnumSimulators();
        
        // NOTE: To deal with changing interface version at client side in .NET applcation we use abstract classes (such as HorusCamera)
        //       When a new driver interface becomes available and supported by the platform, the new methods will ne added to this HorusSession class
        public abstract HorusDriver CreateDriverInstance(HorusDriverSummary driverSummary);
        public abstract HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary);
        public abstract HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary, string driverName);

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
                    HorusDeviceSummary deviceSummary = rv.SingleOrDefault(x => x.DeviceName == device.DeviceName);
                    if (deviceSummary == null)
                    {
                        deviceSummary = new HorusDeviceSummary()
                        {
                            DeviceName = device.DeviceName, 
                            IsAvailable = device.IsAvailable,
                            AvailableDrivers = new List<HorusDriverSummary>()
                        };
                        rv.Add(deviceSummary);
                    }
                    deviceSummary.AvailableDrivers.Add(driver);
                }
            }

            return rv;
        }
    }
}