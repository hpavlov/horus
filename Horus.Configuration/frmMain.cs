using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horus.Client.Drivers;
using Horus.Client.System;
using Horus.Configurator.Controllers;
using Horus.Configurator.ViewModels;
using Horus.Model.Drivers;

namespace Horus.Config
{
    public partial class frmMain : Form
    {
        private LocalHorusDriver[] drivers;
        private DeviceController deviceController;

        public frmMain()
        {
            InitializeComponent();

            deviceController = new DeviceController(this);

            HorusConfigManager.Instance.LoadConfiguration();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadDriversList();
        }

        private void LoadDriversList()
        {
            lbDrivers.Items.Clear();

            drivers = NativeHorusDriversDriscoveryService.DriscoverAvailableDrivers();
            foreach(LocalHorusDriver driver in drivers)
            {
                lbDrivers.Items.Add(driver.Implementor);
            }
        }

        private void btnFindDevices_Click(object sender, EventArgs e)
        {
            deviceController.SearchAttachedDevices();
        }

        private void btnConfigureDevice_Click(object sender, EventArgs e)
        {
            var model = lbDevices.SelectedItem as DeviceModel;
            if (model != null)
            {
                deviceController.ConfigureDevice(model);                
            }
        }
    }
}
