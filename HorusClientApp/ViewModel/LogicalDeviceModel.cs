using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;

namespace HorusClientApp.ViewModel
{
    public class LogicalDeviceModel
    {
        public HorusDeviceSummary DeviceSummary;

        public LogicalDeviceModel(HorusDeviceSummary device)
        {
            DeviceSummary = device;
        }

        public override string ToString()
        {
            return DeviceSummary.DeviceName;
        }
    }
}
