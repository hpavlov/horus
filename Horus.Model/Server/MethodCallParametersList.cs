using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Horus.Model.Server
{
    public class MethodCallParametersList
    {
        [XmlArrayItem("Param")]
        public List<MethodParameter> Parameters = new List<MethodParameter>();
    }

    public class MethodParameter
    {
        public string Type;
        public string Value;        
    }
}
