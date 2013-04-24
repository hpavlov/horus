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
    class Proxy : MarshalByRefObject
    {
        public Assembly GetAssembly(string assemblyPath)
        {
            try
            {
                return Assembly.LoadFrom(assemblyPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Cannot looad " + assemblyPath, ex);
            }
        }
    }

    internal class NativeHorusDriversDriscoveryService
    {
        static Assembly appDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // TODO: Search the same folder
            return null;
        }

        static void appDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            //
        }

        public static LocalHorusDriver[] DriscoverAvailableDrivers()
        {
            var rv = new List<LocalHorusDriver>();

            string driversPath = Path.GetFullPath(Environment.GetEnvironmentVariable("HORUS_HOME", EnvironmentVariableTarget.Machine) + @"\Drivers");

            string[] allDlls = Directory.GetFiles(driversPath, "*.dll", SearchOption.AllDirectories);

            Assembly asm;
            Type[] allTypes;

            foreach(string dllPath in allDlls)
            {
                //// TODO: This needs to be reviews and everything else that should be setup needs to be set
                ////       We also need a really solid AppDomain wrapper that will handle exceptions, etc.
                //// http://stackoverflow.com/questions/658498/how-to-load-assembly-to-appdomain-with-all-references-recursively
                //// NOTE: This may be also worth looking at: https://github.com/jduv/AppDomainToolkit/blob/master/README.md
                //var setup = new AppDomainSetup()
                //{
                //    ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                //    PrivateBinPath = Path.GetDirectoryName(dllPath),
                //    ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
                //};

                //AppDomain appDomain = AppDomain.CreateDomain("Horus-Driver-Check", AppDomain.CurrentDomain.Evidence, setup);
                //try
                //{
                //    appDomain.AssemblyLoad += new AssemblyLoadEventHandler(appDomain_AssemblyLoad);
                //    appDomain.AssemblyResolve += new ResolveEventHandler(appDomain_AssemblyResolve);

                //    Type type = typeof(Proxy);
                //    var value = (Proxy)appDomain.CreateInstanceAndUnwrap(
                //        type.Assembly.FullName,
                //        type.FullName);

                //    asm = value.GetAssembly(dllPath);

                //    allTypes = asm.GetTypes();

                //    LocalHorusDriver[] horusDrivers = allTypes
                //        .Where(x => typeof(IHorusDriver).IsAssignableFrom(x))
                //        .Select(x => new LocalHorusDriver(asm, x))
                //        .ToArray();

                //    rv.AddRange(horusDrivers);
                //}
                //catch(Exception ex)
                //{ }
                //finally
                //{
                //    AppDomain.Unload(appDomain);
                //}

                // HACK: The code above needs to be fixed to work. Currently we load the assemblies in our own AppDomain. This is really bad!
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
