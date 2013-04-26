using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Horus.Model.Server;

namespace Horus.Server.Helpers
{
    public static class DeviceInterfaceCaller
    {
        public static object MakeDeviceMethodCall(object deviceInstace, string methodName, MethodCallParametersList parameters)
        {
            // NOTE: This could work by pure reflection including some safe checks, etc

            Type deviceType = deviceInstace.GetType();
            MethodInfo method = deviceType.GetMethod(methodName);

            var paramList = new List<object>();

            foreach (MethodParameter param in parameters.Parameters)
            {
                // TODO: This is a rather naive implementation

                if (param.Type == "string")
                    paramList.Add(Convert.ToString(param.Value));
                else if (param.Type == "int")
                    paramList.Add(Convert.ToInt32(param.Value));
                else
                    throw new NotSupportedException();
            }

            try
            {
                return method.Invoke(deviceInstace, paramList.ToArray());
            }
            catch (TargetInvocationException tex)
            {
                throw tex.InnerException;
            }
        }
    }
}
