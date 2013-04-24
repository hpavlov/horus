using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;

namespace SampleCameraDrivers
{
    public class Camera1Driver : HorusDriverBase, ICamera
	{
        // NOTE: This should not be mistaken by the interface version
		protected override int GetDriverVersion()
		{
			return 1;
		}

        public override HorusEnabledDeviceSummary[] GetAvailableDevices()
        {
            // NOTE: In reality this should check the hardware channels for available devices and return them
            return new HorusEnabledDeviceSummary[]
            {
                new HorusEnabledDeviceSummary()
                    {
                        DeviceName = "DummyCamera1Device",
                        IsAvailable = true
                    }
            };
        }

        public string Method1(int arg1)
        {
            return "Camera1Driver.Method1";
        }

        public int Property1 { get; set; }
	}
}
