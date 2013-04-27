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
using Horus.Model.Drivers;
using Horus.Model.Interfaces;
using HorusClientApp.Controllers;
using HorusClientApp.ViewModel;

namespace HorusClientApp
{
	public partial class frmMain : Form
	{
	    private VideoController videoController;
	    private DomeController domeController;

		public frmMain()
		{
			InitializeComponent();

		    videoController = new VideoController(this);
		    domeController = new DomeController(this);
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                videoController.DisconnectFromCamera();
                components.Dispose();
            }
            base.Dispose(disposing);
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


	    private HorusSession localDomeSession;

	    private void btnAction_Click(object sender, EventArgs e)
        {
            if (localDomeSession == null)
            {
                localDomeSession = HorusSession.CreateLocalSession();
                List<HorusDeviceSummary> logicalDevices = localDomeSession.EnumDevices<IVideo>();

                cbLogicalVideoDevices.Items.Clear();
                foreach(HorusDeviceSummary device in logicalDevices)
                {
                    cbLogicalVideoDevices.Items.Add(new LogicalDeviceModel(device));
                }

                if (cbLogicalVideoDevices.Items.Count > 0)
                    cbLogicalVideoDevices.SelectedIndex = 0;

                btnAction.Text = "Connect";
            }
            else
            {
                var logicalDevice = cbLogicalVideoDevices.SelectedItem as LogicalDeviceModel;
                if (logicalDevice != null)
                {
                    HorusVideo video = localDomeSession.CreateVideoInstance(logicalDevice.DeviceSummary);
                    videoController.PlayVideo(video);
                }
            }
        }

        private void btnDomeAction_Click(object sender, EventArgs e)
        {
            if (localDomeSession == null)
            {
                localDomeSession = HorusSession.CreateLocalSession();
                List<HorusDeviceSummary> logicalDevices = localDomeSession.EnumDevices<IDome>();

                cbLogicalDomeDevices.Items.Clear();
                foreach (HorusDeviceSummary device in logicalDevices)
                {
                    cbLogicalDomeDevices.Items.Add(new LogicalDeviceModel(device));
                }

                if (cbLogicalDomeDevices.Items.Count > 0)
                {
                    cbLogicalDomeDevices.SelectedIndex = 0;
                btnDomeConnect.Enabled = true;
                btnDomeDisconnect.Enabled = false;
                }
        }

	}

	    void btnDomeConnect_Click(object sender, EventArgs e)
	        {
	        var logicalDevice = cbLogicalDomeDevices.SelectedItem as LogicalDeviceModel;
	        if (logicalDevice != null)
	            {
	            HorusDome dome = localDomeSession.CreateDomeInstance(logicalDevice.DeviceSummary);
	            domeController.ConnectToDevice(dome);
	            }
	        }

	    void SetDomeUiStateAllDisabled()
	        {
	        btnDomeAction.Enabled = false;
	        cbLogicalDomeDevices.Enabled = false;
	        btnDomeConnect.Enabled = false;
	        btnDomeDisconnect.Enabled = false;
	        }

	    internal void SetDomeUiStateChoosing()
	        {
            SetDomeUiStateAllDisabled();
	        btnDomeAction.Enabled = true;
	        cbLogicalDomeDevices.Enabled = true;
	        }

	    internal void SetDomeUiStateChooseOrConnect()
        {
            SetDomeUiStateAllDisabled();
            btnDomeAction.Enabled = true;
            cbLogicalDomeDevices.Enabled = true;
            btnDomeConnect.Enabled = true;
        }

	    internal void SetDomeUiStateConnected()
        {
            SetDomeUiStateAllDisabled();
            btnDomeDisconnect.Enabled = true;
        }

	}
}
