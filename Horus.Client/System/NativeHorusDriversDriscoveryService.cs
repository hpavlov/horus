using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Horus.Client.System;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;
using SampleCameraDrivers;

namespace Horus.ClientFrameWork.CS.System
{
    public class NativeHorusDriversDriscoveryService
    {
        public static LocalHorusDriver[] DriscoverAvailableDrivers()
        {
            // TODO: This could search a place where the native CLR assemblies will be installed
            //       or use any other technology to find them, will enumerate the drivers and return a list
            //       In this Dummy implementaion a fixed list is returned for demonstration purposes

            // NOTE: This is a hack (referencing SampleCameraDrivers.dll from Horus.Client and getting the assembly from a type)
            //       In the real app we will need to find the assemblies and load the types
            Assembly asm = typeof (Camera1Driver).Assembly;

            // This didn't work for some reason so I used the hack above
            //Assembly.ReflectionOnlyLoad("SampleCameraDrivers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            Type[] allTypes = asm.GetTypes();

            return allTypes
                .Where(x => typeof (IHorusDriver).IsAssignableFrom(x))
                .Select(x => new LocalHorusDriver(asm, x))
                .ToArray();
        }
    }
}
