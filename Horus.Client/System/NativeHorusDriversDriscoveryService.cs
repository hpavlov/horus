using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Horus.Client.System;
using Horus.Model.Drivers;
using Horus.Model.Interfaces;
using SampleCameraDrivers;

namespace Horus.Client.System
{
    internal class NativeHorusDriversDriscoveryService
    {
        public static LocalHorusDriver[] DriscoverAvailableDrivers()
        {
            var rv = new List<LocalHorusDriver>();

            string driversPath = Path.GetFullPath(Environment.GetEnvironmentVariable("HORUS_HOME", EnvironmentVariableTarget.Machine) + @"\Drivers");

            string[] allDlls = Directory.GetFiles(driversPath, "*.dll", SearchOption.AllDirectories);

            Assembly asm;
            Type[] allTypes;

            foreach(string dllPath in allDlls)
            {
                // HACK: The code above needs to be fixed to work. Currently we load the assemblies in our own AppDomain. This is really bad!
                // TODO: In the final version we need to load drivers in their own AppDomains via the DriverAppDomainManager
                try
                {
                    asm = Assembly.LoadFrom(dllPath);

                    allTypes = asm.GetTypes();

                    LocalHorusDriver[] horusDrivers = allTypes
                        .Where(x => typeof(IHorusDriver).IsAssignableFrom(x))
                        .Select(x => new LocalHorusDriver(asm, x))
                        .ToArray();

                    rv.AddRange(horusDrivers);
                }
                catch (Exception ex)
                {
                }
                // END of HACK
            }

            // NOTE: This is just part of the dummy example with the 2 camera drivers
            asm = typeof (Camera1Driver).Assembly;

            allTypes = asm.GetTypes();

            rv.AddRange(allTypes
                .Where(x => typeof (IHorusDriver).IsAssignableFrom(x))
                .Select(x => new LocalHorusDriver(asm, x))
                .ToArray());

            return rv.ToArray();
        }
    }
}
