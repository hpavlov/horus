using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using Horus.Client.Drivers;
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
            return MakeHttpCall(url, "GET");
        }

        private string MakeHttpPostCall(string url)
        {
            return MakeHttpCall(url, "POST");
        }

        private string MakeHttpCall(string url, string method)
        {
            var request = HttpWebRequest.Create(url);
            request.Method = method;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream resp = response.GetResponseStream())
                using (TextReader rdr = new StreamReader(resp))
                {
                    return rdr.ReadToEnd();
                }
            }
        }

        internal string InterfaceRemoteFunctionCall<TInterface, TResult>(string driverName, Expression<Func<TInterface, TResult>> func)
        {
            string interfaceName = (typeof(TInterface)).Name;

            // TODO: find the name of the property returned by the function | or name of the function being called using MVC style lambda expressions

            var memberAccess = func.Body as MemberExpression;
            var methodCall = func.Body as MethodCallExpression;


            var bld = new UriBuilder(horusServiceUri);
            bld.Path = string.Format("/drivers/{0}/{1}/{2}", driverName, interfaceName, methodCall.Method.Name);

            bld.Query = ""; //TODO: Add function call parameters here

            string strResponse = MakeHttpPostCall(bld.ToString());

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
            throw new NotImplementedException();
        }

        public override HorusVideo CreateVideoInstance(HorusDeviceSummary deviceSummary)
        {
            throw new NotImplementedException();
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

            ICamera interface not returned ???
            return rv.Where(x => x.DeviceDriver.SupportedInterfaces.Exists(y => string.Equals(y, typeof (TSupportedInterface).FullName, StringComparison.InvariantCultureIgnoreCase))).ToList();
        }
    }
}
