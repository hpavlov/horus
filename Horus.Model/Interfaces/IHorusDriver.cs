/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;

namespace Horus.Model.Interfaces
{
    /// <summary>
    /// This interface is called internally by the Horus System and is not exposed to end clients
    /// </summary>
	public interface IHorusDriver
	{
        /// <summary>
        /// The version of the driver that implements the given interface. This should not be mistaken with the interafce version
        /// </summary>
		int Version { get; }

        HorusEnabledDeviceSummary[] GetAvailableDevices();

	    void Initialize(IHorusContext horusContext);

	    void LinkToDevice(string deviceName);

	    void SetupDialog();
	}
}
