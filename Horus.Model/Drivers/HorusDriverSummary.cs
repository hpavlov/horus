using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Horus.Model.Drivers
{
    public class HorusDriverSummary
    {
        public string DriverName { get; set; }

        [XmlArrayItem("FullName")]
        public List<string> SupportedInterfaces = new List<string>();
    }
}
