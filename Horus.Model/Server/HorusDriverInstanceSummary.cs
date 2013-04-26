using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Server
{
    public class HorusDriverInstanceSummary
    {
        public HorusDriverInstanceSummary()
        {
            // Required fro XML serialization
        }

        public HorusDriverInstanceSummary(string instainceId)
        {
            InstanceId = instainceId;
        }

        public string InstanceId { get; set; }
    }
}
