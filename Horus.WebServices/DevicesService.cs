using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horus.Model.Drivers;
using Horus.Model.Server;
using Horus.WebServices.Interfaces;

namespace Horus.WebServices
{
    public class DevicesService
    {
		public HorusLogicalDeviceSummaryList ListLogicalDevices(ISession session)
		{
			List<HorusDeviceSummary> locigalDevices = session.LocalHorusSession.EnumDevices();

			return new HorusLogicalDeviceSummaryList(locigalDevices);
		}
    }
}
