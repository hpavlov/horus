using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using Horus.Client.Drivers;
using Horus.Client.Remote;
using Horus.Model.Drivers;
using Horus.Model.Server;
using Horus.Model.Helpers;

namespace Horus.Client.System
{
    internal class RemoteHorusSession : HorusSession
    {
        private string sessionId;
        private Uri horusServiceUri;

        public RemoteHorusSession(Uri horusServiceUri)
        {
            this.horusServiceUri = horusServiceUri;
        }

        public void Login(string userName, string password)
        {
            // TODO: We need to think how to send the credential securely. May always need HTTPS for this, but may explore other options?

            string response = MakeHttpPostCall(horusServiceUri + "session/new?userId=" + userName);

            sessionId = response;
        }

        private string MakeHttpGetCall(string url)
        {
            return MakeHttpCall(url, "GET", null);
        }

        private string MakeHttpPostCall(string url)
        {
            return MakeHttpCall(url, "POST", null);
        }

        private string MakeHttpPostCall(string url, string requestBody)
        {
            return MakeHttpCall(url, "POST", requestBody);
        }

        private string MakeHttpCall(string url, string method, string requestBody)
        {
            var request = HttpWebRequest.Create(url);
            request.Method = method;

            if (requestBody != null)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(requestBody);

                request.ContentLength = bytes.Length;
                request.ContentType = "application/xml";
                using (var wrt = new BinaryWriter(request.GetRequestStream()))
                {
                    wrt.Write(bytes, 0, bytes.Length);
                }
                
            }

            using (WebResponse response = request.GetResponse())
            {
                using (Stream resp = response.GetResponseStream())
                using (TextReader rdr = new StreamReader(resp))
                {
                    return rdr.ReadToEnd();
                }
            }
        }

        internal string InterfaceRemoteFunctionCall<TInterface, TResult>(string instanceId, Expression<Func<TInterface, TResult>> func)
        {
            string interfaceName = (typeof(TInterface)).Name;

            // TODO: find the name of the property returned by the function | or name of the function being called using MVC style lambda expressions

            //var memberAccess = func.Body as MemberExpression;

            var methodCall = func.Body as MethodCallExpression;
            
            string url = horusServiceUri + 
                string.Format("drivers/{0}/call-method/{1}?sessionId={2}", instanceId, methodCall.Method.Name, sessionId);

            var methodParamValues = new MethodCallParametersList();
            ReadOnlyCollection<Expression> arguments = ((MethodCallExpression) func.Body).Arguments;

            ParameterInfo[] methodParams = methodCall.Method.GetParameters();
            for (int i = 0; i < methodParams.Count(); i++)
            {
                ParameterInfo prm = methodParams[i];
                UnaryExpression argAsObj = Expression.Convert(arguments[i], typeof (object));
                object argValue = Expression.Lambda<Func<object>>(argAsObj, null).Compile()();

                var methodParam = new MethodParameter()
                {
                    Type = prm.ParameterType.FullName,
                    Value = argValue != null ? argValue.ToString() : null
                };

                methodParamValues.Parameters.Add(methodParam);
            }

            string strResponse = MakeHttpPostCall(url, methodParamValues.AsSerialized());

            if (typeof(TResult) == typeof(string))
                return strResponse;

            return null;
        }

        public override HorusDriverSummary[] EnumDrivers()
        {
            throw new NotImplementedException();
        }

        public override HorusDriverSummary[] EnumSimulators()
        {
            throw new NotImplementedException();
        }

        public override HorusDriver CreateDriverInstance(HorusDriverSummary driverSummary)
        {
            throw new NotImplementedException();
        }

        public override HorusCamera CreateCameraInstance(HorusDeviceSummary deviceSummary)
        {
            string strResponse = MakeHttpPostCall(horusServiceUri +
                    string.Format("drivers/{0}/create-instance?driverName={1}&interfaceName=ICamera&sessionId={2}",
                        deviceSummary.DeviceName,
                        deviceSummary.DeviceDriver.DriverName,
                        sessionId));

            HorusDriverInstanceSummary summary = strResponse.AsDeserialized<HorusDriverInstanceSummary>();

            // TODO: The summary may contain error messages and other information 

            return new HorusCamera(new RemoteCamera(this, summary.InstanceId));
        }

        public override HorusVideo CreateVideoInstance(HorusDeviceSummary deviceSummary)
        {
            string strResponse = MakeHttpPostCall(horusServiceUri +
                    string.Format("drivers/{0}/create-instance?driverName={1}&interfaceName=IVideo&sessionId={2}", 
                        deviceSummary.DeviceName, 
                        deviceSummary.DeviceDriver.DriverName, 
                        sessionId));

            return new HorusVideo(null);
        }

        public override List<HorusDeviceSummary> EnumDevices()
        {
            string strResponse = MakeHttpGetCall(horusServiceUri + "devices/list?sessionId=" + sessionId);

            HorusLogicalDeviceSummaryList deviceList = strResponse.AsDeserialized<HorusLogicalDeviceSummaryList>();
            return new List<HorusDeviceSummary>(deviceList.LogicalDevices);
        }

        public override List<HorusDeviceSummary> EnumDevices<TSupportedInterface>()
        {
            var rv = new List<HorusDeviceSummary>();
            rv = EnumDevices();

            // NOTE: This rediculous linq check is because I was thinking I am having string comparison issues but it was something else. This need to be simplified
            return rv.Where(x => x.DeviceDriver
                .SupportedInterfaces
                .Exists(y => string.Equals(y, typeof (TSupportedInterface).FullName, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();
        }
    }
}
