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
                        DeviceName = "DummyCameraDevice",
                        IsAvailable = true
                    }
            };
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
