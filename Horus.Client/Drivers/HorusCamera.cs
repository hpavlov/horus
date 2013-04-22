using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Interfaces;

namespace Horus.Client.Drivers
{
    public class HorusCamera : HorusDriver, ICamera
    {
        private ICamera cameraInterface;

        internal HorusCamera(object deviceInterface)
            : base(deviceInterface as IHorusDriver)
        {
            this.cameraInterface = deviceInterface as ICamera;
        }

        public string Method1(int arg1)
        {
            return cameraInterface.Method1(arg1);
        }

        public int Property1
        {
            get { return cameraInterface.Property1; }

            set { cameraInterface.Property1 = value; }
        }
    }
}
