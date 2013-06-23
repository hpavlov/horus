/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;

namespace Horus.Client.Drivers
{
	public class HorusDriver : IHorusDriver 
	{
        // TODO: This should not be used as a base class to the classes created by the Client framework and exposed to end clients
        //       because the IHorusDriver interface is not intended to be used by end clients, but only by the Horus System
        //       So refactor this and make sure the end clients cannot get an IHorusDriver interface to the devices they control

        protected IHorusDriver driverInstance;

		private HorusDriver()
		{ }

        internal HorusDriver(IHorusDriver driverInterfaceInstance)
		{
            this.driverInstance = driverInterfaceInstance;
		}

        public Type DriverType
        {
            get { return driverInstance.GetType(); }
        }

		public int Version
		{
			get
			{
                return driverInstance.Version;
			}
		}

        public HorusEnabledDeviceSummary[] GetAvailableDevices()
        {
            return driverInstance.GetAvailableDevices();
        }


        public void Initialize(IHorusContext horusContext)
        {
            driverInstance.Initialize(horusContext);
        }

        public void LinkToDevice(string deviceName)
        {
            driverInstance.LinkToDevice(deviceName);
        }

        public void SetupDialog()
        {
            driverInstance.SetupDialog();
        }
    }
}
