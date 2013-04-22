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

	    public abstract HorusEnabledDeviceSummary[] GetAvailableDevices();
	}
}
