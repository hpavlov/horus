/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;

namespace SampleCameraDrivers
{
    public class Camera2Driver : HorusDriverBase, ICameraV2
	{
        // NOTE: This should not be mistaken by the interface version
		protected override int GetDriverVersion()
		{
			return 3;
		}

        public override HorusEnabledDeviceSummary[] GetAvailableDevices()
        {
            // NOTE: In reality this should check the hardware channels for available devices and return them
            return new HorusEnabledDeviceSummary[]
            {
                new HorusEnabledDeviceSummary()
                    {
                        DeviceName = "DummyCamera2Device",
                        IsAvailable = true
                    }
            };
        }

        public override void Initialize(IHorusContext horusContext)
        {
            // NOTE: This dummy demonstration doesn't use this method
        }

        public override void LinkToDevice(string deviceName)
        {
            // NOTE: This dummy demonstration doesn't use this method
        }

        public override void SetupDialog()
        {
            // NOTE: This dummy demonstration doesn't use this method
        }

        public string Method1(int arg1)
        {
            return "Camera2Driver.Method1";
        }

        public int Property1 { get; set; }

        public string Method2(int arg1, string arg2)
        {
            return "Camera2Driver.Method2";
        }
	}
}
