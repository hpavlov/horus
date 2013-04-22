using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Drivers
{
    public class HorusDriverSummary
    {
        public string DriverName { get; set; }
        public Type[] SupportedInterfaces { get; set; }
    }
}
