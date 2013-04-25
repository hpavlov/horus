using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Client.System.AppDomains
{
    public class DriverAppDomainManager
    {
        // TODO: Drivers need to be created and run on separate app domains. They will have to communicate via MarshalByRefObjects 
        // see http://stackoverflow.com/questions/5275839/inter-appdomain-communication-problem?rq=1 
        
        // NOTE: Parent Domain can also pass data to the child domain using something like this:
        //static void RunInChildDomain()
        //{
        //    AppDomain childDomain = AppDomain.CreateDomain("friendlyName");
        //    string parameterValue = "notmii";
        //    childDomain.SetData("parameter", parameterValue);
        //    childDomain.DoCallBack(PrintName);
        //}

        //static void PrintName()
        //{
        //    string Name = Convert.ToString(AppDomain.CurrentDomain.GetData("parameter"));
        //    Console.WriteLine(Name);
        //}

        // NOTE: Research if there are any existing cross-domain code execution frameworks out there

        // NOTE: Loading assembiled could be done using code based on this: https://github.com/jduv/AppDomainToolkit/blob/master/README.md

        //// TODO: This needs to be reviews and everything else that should be setup needs to be set
        ////       We also need a really solid AppDomain wrapper that will handle exceptions, etc.
        //// http://stackoverflow.com/questions/658498/how-to-load-assembly-to-appdomain-with-all-references-recursively
        //// NOTE: This may be also worth looking at: https://github.com/jduv/AppDomainToolkit/blob/master/README.md

        //static Assembly appDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    // TODO: Search the same folder
        //    return null;
        //}

        //static void appDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        //{
        //    //
        //}

        //class Proxy : MarshalByRefObject
        //{
        //    public Assembly GetAssembly(string assemblyPath)
        //    {
        //        try
        //        {
        //            return Assembly.LoadFrom(assemblyPath);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new InvalidOperationException("Cannot looad " + assemblyPath, ex);
        //        }
        //    }
        //}

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
    }
}
