using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.Drivers;
using Horus.Config;
using Horus.Config.Model;
using Horus.Model.Drivers;

namespace Horus.Configurator.ViewModels
{
    public class DeviceModel
    {
        public HorusEnabledDeviceSummary Device;
        public HorusDriver Driver;
        public DeviceConfig DeviceConfig;

        public DeviceModel(HorusEnabledDeviceSummary device, HorusDriver driver)
        {
            this.Device = device;
            this.Driver = driver;
            DeviceConfig = HorusConfigManager.Instance.GetLogicalDeviceConfiguration(driver.DriverType, device.DeviceName);

            // TODO: This should be done when the Driver is initialized and not here!
            if (DeviceConfig != null)
                Driver.LinkToDevice(device.DeviceName);
        }

        public override string ToString()
        {
            var displayText = new StringBuilder();
            displayText.AppendFormat("{0} [{1}] [{2}]", 
                Device.DeviceName, 
                Device.IsAvailable ? "Available" : "Unavailable",
                DeviceConfig != null ? "Configured" : "Not Configured");

            return displayText.ToString();
        }
    }
}
