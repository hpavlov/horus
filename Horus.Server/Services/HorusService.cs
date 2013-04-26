using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Horus.Server.Model;
using Kayak.Framework;

namespace Horus.Server.Services
{
    internal class HorusService : KayakService
    {
        [Path("/")]
		[Verb("GET")]
		public void Unknown()
		{
			Response.Write("UnmappedREequestUrl");
		}

        protected void XmlResponse(object model)
        {
            if (model != null)
            {
                var ser = new XmlSerializer(model.GetType());
                var output = new StringBuilder();
                using (var wrt = new System.IO.StringWriter(output))
                {
                    ser.Serialize(wrt, model);
                }

                Response.Write(output.ToString());
                Response.Headers["Content-Type"] = "application/xml";
            }                 
        }
    }
}
