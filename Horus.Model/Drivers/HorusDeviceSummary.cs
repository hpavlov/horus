using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Drivers
{
    public class HorusDeviceSummary
    {
        public string DeviceName { get; set; }
        public bool IsAvailable { get; set; }
        public List<HorusDriverSummary> AvailableDrivers { get; set; }
    }
}
