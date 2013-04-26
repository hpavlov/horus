using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;

namespace Horus.Model.Server
{
    public class HorusLogicalDeviceSummaryList
    {
        public HorusDeviceSummary[] LogicalDevices;

        public HorusLogicalDeviceSummaryList()
        { }

        public HorusLogicalDeviceSummaryList(List<HorusDeviceSummary> logicalDevices)
        {
            LogicalDevices = new List<HorusDeviceSummary>(logicalDevices).ToArray();
        }
    }
}
