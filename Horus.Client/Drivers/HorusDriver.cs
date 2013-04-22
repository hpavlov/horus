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
        protected IHorusDriver driverInstance;

		private HorusDriver()
		{ }

        internal HorusDriver(IHorusDriver driverInterfaceInstance)
		{
            this.driverInstance = driverInterfaceInstance;
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
    }
}
