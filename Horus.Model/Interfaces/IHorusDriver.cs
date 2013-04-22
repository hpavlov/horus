using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;

namespace Horus.Model.Interfaces
{
	public interface IHorusDriver
	{
        /// <summary>
        /// The version of the driver that implements the given interface. This should not be mistaken with the interafce version
        /// </summary>
		int Version { get; }

        HorusEnabledDeviceSummary[] GetAvailableDevices();
	}
}
