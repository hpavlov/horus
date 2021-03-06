﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

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
        // ToDo: [TPL] Can we use a generic abstract method here rather than an abstract method for each type of driver?
        //         public abstract THorusDriver CreateDriverInstance<THorusDriver>(HorusDriverSummary driverSummary);

        public abstract HorusDriver CreateDriverInstance(HorusDriverSummary driverSummary);
        public abstract HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary);
        public abstract HorusVideo CreateVideoInstance(HorusDeviceSummary deviceSummary);
        public abstract HorusDome CreateDomeInstance(HorusDeviceSummary deviceSummary);

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
