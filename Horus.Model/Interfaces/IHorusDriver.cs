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
