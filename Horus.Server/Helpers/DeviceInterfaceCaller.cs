﻿using System;
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
                paramList.Add(GetParameterValue(param));
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

        public static object DevicePropertyGet(object deviceInstace, string propertyName)
        {
            Type deviceType = deviceInstace.GetType();
            PropertyInfo property = deviceType.GetProperty(propertyName);

            try
            {
                return property.GetValue(deviceInstace, null);
            }
            catch (TargetInvocationException tex)
            {
                throw tex.InnerException;
            }
        }

        public static void DevicePropertySet(object deviceInstace, string propertyName, MethodCallParametersList parameters)
        {
            Type deviceType = deviceInstace.GetType();
            PropertyInfo property = deviceType.GetProperty(propertyName);

            try
            {
                object propertyValue = GetParameterValue(parameters.Parameters[0]);

                property.SetValue(deviceInstace, propertyValue, null);
            }
            catch (TargetInvocationException tex)
            {
                throw tex.InnerException;
            }
        }

        private static object GetParameterValue(MethodParameter param)
        {
            // TODO: This is a rather naive implementation

            if (param.Type == "System.String")
                return Convert.ToString(param.Value);
            else if (param.Type == "System.Int32")
                return Convert.ToInt32(param.Value);
            else if (param.Type == "System.Boolean")
                return Convert.ToBoolean(param.Value);
            else
                throw new NotSupportedException();
        }
    }
}
