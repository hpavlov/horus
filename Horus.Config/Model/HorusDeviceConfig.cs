/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Horus.Config.Model
{
    public class HorusDeviceConfig
    {
        [XmlArrayItem("Driver")]
        public List<DriverConfig> Drivers = new List<DriverConfig>();

        [XmlArrayItem("Device")]
        public List<DeviceConfig> Devices = new List<DeviceConfig>();
    }

    public class DriverConfig
    {
        public string DriverId {get; set;}
        public string DriverAssemblyName { get; set; }
        public string DriverTypeName { get; set; }
    }

    public class DeviceConfig
    {
        public string DeviceName { get; set; }
        public string DriverId { get; set; }

        public XmlNode DriverDeviceData;
    }
}
