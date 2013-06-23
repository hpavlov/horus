/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Interfaces;

namespace Horus.Model.Drivers
{
	public abstract class HorusDriverBase : IHorusDriver
	{
		protected abstract int GetDriverVersion();

		public int Version
		{
			get { return GetDriverVersion(); }
		}

        public abstract void Initialize(IHorusContext horusContext);

        public abstract void LinkToDevice(string deviceName);

        public abstract void SetupDialog();

	    public abstract HorusEnabledDeviceSummary[] GetAvailableDevices();
	}
}
