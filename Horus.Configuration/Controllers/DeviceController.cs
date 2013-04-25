using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.Drivers;
using Horus.Client.System;
using Horus.Config;
using Horus.Configurator.ViewModels;
using Horus.Model.Drivers;

namespace Horus.Configurator.Controllers
{
    public class DeviceController
    {
        private frmMain mainForm;
        private List<DeviceModel> identifiedDevices = new List<DeviceModel>();

        public DeviceController(frmMain mainForm)
        {
            this.mainForm = mainForm;
        }

        public void SearchAttachedDevices()
        {
            identifiedDevices.Clear();

            HorusSession session = HorusSession.CreateLocalSession();
            HorusDriverSummary[] summaries = session.EnumDrivers();
            foreach (HorusDriverSummary driverSummary in summaries)
            {
                HorusDriver driver = session.CreateDriverInstance(driverSummary);
                HorusEnabledDeviceSummary[] devices = driver.GetAvailableDevices();

                foreach (HorusEnabledDeviceSummary device in devices)
                {
                    identifiedDevices.Add(new DeviceModel(device, driver));                    
                }
            }

            RefreshDeviceList();
        }

        private void RefreshDeviceList()
        {
            mainForm.lbDevices.Items.Clear();
            identifiedDevices.ForEach(x => mainForm.lbDevices.Items.Add(x));
        }

        public void ConfigureDevice(DeviceModel model)
        {
            if (model.DeviceConfig == null)
            {
                model.Driver.LinkToDevice(model.Device.DeviceName);
                model.DeviceConfig = HorusConfigManager.Instance.RegisterLogicalDeviceConfiguration(model.Driver.DriverType, model.Device.DeviceName);                
            }

            model.Driver.SetupDialog();

            RefreshDeviceList();
        }
    }
}
