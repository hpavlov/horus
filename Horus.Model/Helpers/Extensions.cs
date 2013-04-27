using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Horus.Model.Helpers
{
    public static class Extensions
    {
        public static T AsDeserialized<T>(this XmlNode node)
        {
            return node.OuterXml.AsDeserialized<T>();
        }

        public static T AsDeserialized<T>(this string xmlString)
        {
            var ser = new XmlSerializer(typeof(T));
            using (TextReader rdr = new StringReader(xmlString))
            {
                return (T)ser.Deserialize(rdr);
            }
        }

        public static XmlNode AsSerializedNode(this object obj)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(obj.AsSerialized());
            return xmlDoc.DocumentElement;
        }

        public static string AsSerialized(this object obj)
        {
            var ser = new XmlSerializer(obj.GetType());
            var outXmlStr = new StringBuilder();
            using (TextWriter writer = new StringWriter(outXmlStr))
            {
                ser.Serialize(writer, obj);
            }

            return outXmlStr.ToString();
        }

        public static string AsSerialized<TBaseClass>(this object obj)
        {
            var ser = new XmlSerializer(typeof(TBaseClass));
            var outXmlStr = new StringBuilder();
            using (TextWriter writer = new StringWriter(outXmlStr))
            {
                ser.Serialize(writer, obj);
            }

            return outXmlStr.ToString();
        }

    }
}
