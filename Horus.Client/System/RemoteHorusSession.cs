using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using Horus.Client.Drivers;
using Horus.Model.Drivers;

namespace Horus.ClientFrameWork.CS.System
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
            
        }

        private string MakeHttpCall(string url)
        {
            var request = HttpWebRequest.Create(url);

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

            string strResponse = MakeHttpCall(bld.ToString());

            if (typeof(TResult) == typeof(string))
                return strResponse;

            return null;
        }

        public override HorusDriverSummary[] EnumDrivers()
        {
            string strResponse = MakeHttpCall("/drivers/list");

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
    }
}
