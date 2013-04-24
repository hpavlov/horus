﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horus.Client.Drivers;
using Horus.Client.System;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;

namespace HorusClientApp
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

        private void btnLocalCamera1_Click(object sender, EventArgs e)
        {
            // NOTE: In a real client application the session and driver instantiation will be managed at a different scope.
            //       This is just a quick example and includes all objects that need to be created and all methods that need to be called 
            //       during the lifetime of the realworld app in order to make a method call on a driver interface

            var localSession = HorusSession.CreateLocalSession();
            HorusDeviceSummary deviceSummary = localSession.EnumDevices<ICamera>().First(x => x.DeviceName == "DummyCamera1Device");

            HorusCamera camera = localSession.CreateCameraInstance(deviceSummary);

            MessageBox.Show(camera.Method1(0));
        }

        private void btnLocalCamera2_Click(object sender, EventArgs e)
        {
            // NOTE: In a real client application the session and driver instantiation will be managed at a different scope.
            //       This is just a quick example and includes all objects that need to be created and all methods that need to be called 
            //       during the lifetime of the realworld app in order to make a method call on a driver interface

            var localSession = HorusSession.CreateLocalSession();
            HorusDeviceSummary deviceSummary = localSession.EnumDevices<ICamera>().First(x => x.DeviceName == "DummyCamera2Device");

            HorusCamera camera = localSession.CreateCameraInstance(deviceSummary);

            MessageBox.Show(camera.Method1(0));
        }

        private void btnRemoteCamera1_Click(object sender, EventArgs e)
        {
            // NOTE: In a real client application the session and driver instantiation will be managed at a different scope.
            //       This is just a quick example and includes all objects that need to be created and all methods that need to be called 
            //       during the lifetime of the realworld app in order to make a method call on a driver interface

            var remoteSession = HorusSession.CreateRemoteSession(new Uri(tbxEndpointV1.Text), tbxUser.Text, tbxPassword.Text);

            HorusDeviceSummary deviceSummary = remoteSession.EnumDevices<ICamera>().First(x => x.DeviceName == "DummyCamera1Device");

            HorusCamera camera = remoteSession.CreateCameraInstance(deviceSummary);

            MessageBox.Show(camera.Method1(0));
        }

	}
}
