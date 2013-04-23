using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;

namespace Horus.Model.Interfaces
{
    public enum DriverConnectionPoint
    {
        Custom = 0,
        ListOfCustomConnectionPoints,
        SerialPort,
        ParallelPort,
        FireWite
    }

	public interface IHorusDriver
	{
        /// <summary>
        /// The version of the driver that implements the given interface. This should not be mistaken with the interafce version
        /// </summary>
		int Version { get; }

        DriverConnectionPoint ConnectionPointType { get; }

	    HorusEnabledDeviceSummary[] GetAvailableDevices();
	}
}
