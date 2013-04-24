using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horus.Client.System;
using Horus.Model.Drivers;

namespace Horus.Config
{
    public partial class frmMain : Form
    {
        private LocalHorusDriver[] drivers;

        public frmMain()
        {
            InitializeComponent();
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
            //HorusSession session = HorusSession.CreateLocalSession();
            //HorusDriverSummary[] drivers = session.EnumDrivers();
            //session.CreateDriverInstance(drivers[0]);
        }

    }
}
