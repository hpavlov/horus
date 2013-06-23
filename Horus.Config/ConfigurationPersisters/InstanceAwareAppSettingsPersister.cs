using System;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TiGra;

namespace Horus.Config.ConfigurationPersisters
{
    /// <summary>
    ///   Class InstanceAwareAppSettingsPersister - persists application settings as XML in a file on disk.
    ///   Objectives of this class:
    ///   <list type="table">
    ///     <item>
    ///       <term>Instance-aware</term>
    ///       <description>
    ///         We must be able to cope with multiple instances of a driver, so settings should be saved per driver
    ///         and per instance. There may be multiple instances of any given driver all with different settings.
    ///         The driver is responsible for providing its instance ID.
    ///       </description>
    ///     </item>
    ///     <item>
    ///       <term>Works with Application Settings</term>
    ///       <description>
    ///         Application settings provide important usability benefits to driver developers so its important not to preclude the use of those features.
    ///         In particular, the ability to data-bind settings to WinForms controls reduces the entire burden of handling settings to just
    ///         one or two lines of code. Therefore, this class has to be able to work within that infrastructure.
    ///       </description>
    ///     </item>
    ///   </list>
    /// </summary>
    public class InstanceAwareAppSettingsPersister
    {
        #region XML element names
        const string RootElementName = "horusDriverSettings";
        const string HorusDriverElementName = "horusDriver";
        const string DriverInstanceElementName = "driverInstance";
        const string DirverIdAttributeName = "driverId";
        const string InstanceIdAttributeName = "instanceId";
        const string SerializeAsAttributeName = "serializeAs";
        #endregion
        static XmlEscaper _escaper;

        /// <summary>
        ///   Loads the settings for the specified instance of a driver.
        ///   Settings are loaded from a file located in the same directory as teh driver and from
        ///   a file whose name is the same as the driver assembly suffixed with '.settings'
        /// </summary>
        /// <param name="settings">The driver's settings class derived from ApplicationSettingsBase.</param>
        /// <param name="instanceId">The instance id of the driver.</param>
        public static void LoadSettings(ApplicationSettingsBase settings, string instanceId)
        {
            Contract.Requires(settings != null);
            Contract.Requires(!string.IsNullOrEmpty(instanceId));
            Assembly driverAssembly = Assembly.GetCallingAssembly(); // This should be the driver assembly.
            string driverLocation = driverAssembly.Location;
            string driverDirectory = Path.GetDirectoryName(driverLocation);
            string driverFileNameWithoutExtension = Path.GetFileNameWithoutExtension(driverLocation);
            string settingsFileName = driverFileNameWithoutExtension + ".settings";
            Debug.Assert(driverDirectory != null, "myDirectory != null");
            string settingsFileFullPath = Path.Combine(driverDirectory, settingsFileName);
            LoadSettings(settings, settingsFileFullPath, driverFileNameWithoutExtension, instanceId);
        }

        /// <summary>
        ///   Loads the settings for the specified instance of the specified driver, from the specified file.
        /// </summary>
        /// <param name="settings">The settings class.</param>
        /// <param name="fileName">Name of the settings XML file.</param>
        /// <param name="driverId">The driver id.</param>
        /// <param name="instanceId">The instance id.</param>
        internal static void LoadSettings(
            ApplicationSettingsBase settings, string fileName, string driverId, string instanceId)
        {
            Contract.Requires(settings != null);
            Contract.Requires(!string.IsNullOrEmpty(instanceId));
            Diagnostics.Enter();
            XElement instanceXml = LoadInstanceSettings(fileName, driverId, instanceId);

            // We now have an XElement containing the driver settings as child nodes.
            // We need to iterate through the settings properties, extracting its value from the loaded XML.
            // If no value is found, use the default for the setting.
            foreach (SettingsPropertyValue propertyValue in settings.PropertyValues)
            {
                string name = propertyValue.Name;
                object defaultValue = propertyValue.Property.DefaultValue;
                string loadedValue = GetSettingValueOrDefault(name, defaultValue as string, instanceXml);
                Diagnostics.TraceVerbose("Deserialized setting name={0} value={1}", name, loadedValue);
                propertyValue.SerializedValue = loadedValue;
            }
            Diagnostics.Exit();
        }

        /// <summary>
        ///   Gets a setting from an XML collection and deserializes it into a string.
        ///   Performs string unescaping or conversion from a Base64 string as necessary.
        ///   If the setting cannot be found in the XML, or if it cannot be deserialized,
        ///   then its default value is returned.
        /// </summary>
        /// <param name="name">The setting name.</param>
        /// <param name="defaultValue">The setting's default value.</param>
        /// <param name="settingsXml">
        ///   <see cref="XElement" /> containing the settings collection.
        /// </param>
        /// <returns>A string containing the setting's decoded/deserialized value.</returns>
        static string GetSettingValueOrDefault(string name, string defaultValue, XElement settingsXml)
        {
            XElement settingNode = settingsXml.Element(name);
            if (settingNode == null)
                return defaultValue;

            string serializationString = settingNode.Attribute(SerializeAsAttributeName).Value;
            var serializationType = (SettingsSerializeAs)Enum.Parse(typeof(SettingsSerializeAs), serializationString);
            switch (serializationType)
            {
            case SettingsSerializeAs.String:
                // Need to unescape serialized strings
                XmlEscaper escaper = Escaper;
                string unescapedValue = escaper.Unescape(settingNode.Value);
                return unescapedValue;
            case SettingsSerializeAs.Binary:
                byte[] decodedString = Convert.FromBase64String(settingNode.Value);
                return Encoding.UTF8.GetString(decodedString);
            default:
                return defaultValue;
            }
        }

        /// <summary>
        ///   Loads the settings file and extracts the settings for this driver instance.
        /// </summary>
        /// <param name="settingsFile">Name of the settings file.</param>
        /// <param name="driverId">The driver id.</param>
        /// <param name="instanceId">The driver instance id.</param>
        /// <returns>
        ///   An <see cref="XElement" /> containing the settings for this instance.
        /// </returns>
        static XElement LoadInstanceSettings(string settingsFile, string driverId, string instanceId)
        {
            var fileInfo = new FileInfo(settingsFile);
            if (!fileInfo.Exists)
                return GetNewInstanceElement(instanceId);

            XElement settingsXml = XElement.Load(settingsFile);
            // ToDo: If this fails, we should return a new, empty element.
            // Select the node for our driver type. If none found, create a new one.
            XElement driverElement =
                settingsXml.Descendants()
                           .SingleOrDefault(
                               x =>
                               x.Name == HorusDriverElementName && x.Attribute(DirverIdAttributeName).Value == driverId);
            if (driverElement == null)
                return GetNewInstanceElement(instanceId);

            // Select the node containing the settings for our instance; if found, remove it.
            XElement instanceElement =
                driverElement.Descendants()
                             .SingleOrDefault(
                                 x =>
                                 x.Name == DriverInstanceElementName
                                 && x.Attribute(InstanceIdAttributeName).Value == instanceId);

            if (instanceElement == null)
                return GetNewInstanceElement(instanceId);

            return instanceElement;
        }

        /// <summary>
        ///   Creates and returns a new, empty driver instance XML element
        /// </summary>
        /// <returns>
        ///   An <see cref="XElement" /> containing a new, empty driver settings instance.
        /// </returns>
        static XElement GetNewInstanceElement(string instanceId)
        {
            return new XElement(
                DriverInstanceElementName, new XAttribute(InstanceIdAttributeName, instanceId ?? String.Empty));
        }

        /// <summary>
        ///   Saves the settings for the specified driver instance.
        ///   Settings are loaded from a file located in the same directory as the driver and from
        ///   a file whose name is the same as the driver assembly suffixed with '.settings'
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="instanceId">The instance id.</param>
        public static void SaveSettings(ApplicationSettingsBase settings, string instanceId)
        {
            Contract.Requires(settings != null);
            Contract.Requires(!string.IsNullOrEmpty(instanceId));
            Assembly driverAssembly = Assembly.GetCallingAssembly(); // This should be the driver assembly.
            string driverLocation = driverAssembly.Location;
            string driverDirectory = Path.GetDirectoryName(driverLocation);
            string driverFileNameWithoutExtension = Path.GetFileNameWithoutExtension(driverLocation);
            string settingsFileName = driverFileNameWithoutExtension + ".settings";
            Debug.Assert(driverDirectory != null, "myDirectory != null");
            string settingsFileFullPath = Path.Combine(driverDirectory, settingsFileName);
            SaveSettings(settings, settingsFileFullPath, driverFileNameWithoutExtension, instanceId);
        }

        /// <summary>
        ///   Saves the settings as XML to a file on disk.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="driverId"></param>
        /// <param name="instanceId">Identifies the logical device instance</param>
        internal static void SaveSettings(
            ApplicationSettingsBase settings, string fileName, string driverId, string instanceId)
        {
            Contract.Requires(settings != null);
            Contract.Requires(!string.IsNullOrEmpty(instanceId));
            Diagnostics.TraceInfo(
                "Persisting settings for DriverId={0} InstanceId={1} to {2}", driverId, instanceId, fileName);
            XElement instanceRoot = BuildInstanceXml(settings, instanceId);
            SaveInstanceSettings(fileName, driverId, instanceId, instanceRoot);
        }

        /// <summary>
        ///   Saves the driver instance settings, without disturbing settings for other instances.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="driverId">The driver id.</param>
        /// <param name="instanceId">The instance id.</param>
        /// <param name="instanceRoot">The instance root.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        static void SaveInstanceSettings(string fileName, string driverId, string instanceId, XElement instanceRoot)
        {
            XElement settingsXml;
            var fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
                settingsXml = XElement.Load(fileName); // Read settings for all instances
            else
            {
                Diagnostics.TraceVerbose("Saving settings as new file");
                settingsXml = new XElement(RootElementName);
            }

            // Select the node for our driver type. If none found, create a new one.
            XElement driverElement =
                settingsXml.Descendants()
                           .SingleOrDefault(
                               x =>
                               x.Name == HorusDriverElementName && x.Attribute(DirverIdAttributeName).Value == driverId);
            if (driverElement == null)
            {
                driverElement = new XElement(HorusDriverElementName, new XAttribute(DirverIdAttributeName, driverId));
                settingsXml.Add(driverElement);
            }

            // Select the node containing the settings for our instance; if found, remove it.
            XElement instanceElement =
                driverElement.Descendants()
                             .SingleOrDefault(
                                 x =>
                                 x.Name == DriverInstanceElementName
                                 && x.Attribute(InstanceIdAttributeName).Value == instanceId);
            if (instanceElement != null)
                instanceElement.ReplaceWith(instanceRoot);
            else
                driverElement.Add(instanceRoot); // Add the new settings
            settingsXml.Save(fileName);
        }

        static XElement BuildInstanceXml(ApplicationSettingsBase settings, string instanceId)
        {
            var instanceRoot = new XElement(
                DriverInstanceElementName, new XAttribute(InstanceIdAttributeName, instanceId));
            foreach (SettingsPropertyValue propertyValue in settings.PropertyValues)
            {
                XElement element = SerializeToXmlElement(propertyValue.Property, propertyValue);
                instanceRoot.Add(element);
            }
            Diagnostics.TraceInfo("Built driver instance settings:\n{0}", instanceRoot);
            return instanceRoot;
        }

        /// <summary>
        ///   Serializes to XML element.
        ///   Based on the code in <see cref="System.Configuration.LocalFileSettingsProvider" />.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="value">The value.</param>
        /// <returns>XmlNode.</returns>
        static XElement SerializeToXmlElement(SettingsProperty setting, SettingsPropertyValue value)
        {
            var serializedValue = value.SerializedValue as string;

            if (serializedValue == null && setting.SerializeAs == SettingsSerializeAs.Binary)
            {
                // SettingsPropertyValue returns a byte[] in the binary serialization case. We need to
                // encode this - we use base64 since SettingsPropertyValue understands it and we won't have
                // to special case while deserializing. 
                var buf = value.SerializedValue as byte[];
                if (buf != null)
                    serializedValue = Convert.ToBase64String(buf);
            }

            if (serializedValue == null)
                serializedValue = String.Empty;

            // We need to escape string serialized values 
            if (setting.SerializeAs == SettingsSerializeAs.String)
                serializedValue = Escaper.Escape(serializedValue);

            var valueXml = new XElement(
                setting.Name, serializedValue, new XAttribute(SerializeAsAttributeName, setting.SerializeAs));
            return valueXml;
        }

        //private SettingsPropertyValueCollection GetSettingValuesFromFile(string fileName, string instanceId, SettingsPropertyCollection properties)
        //{
        //    var settingsXml = XElement.Load(fileName);
        //    var instanceSettings = from s in settingsXml.Descendants()
        //                           where 
        //    SettingsPropertyValueCollection propertyValueCollection = new SettingsPropertyValueCollection();
        //    foreach (SettingsProperty property1 in properties)
        //    {
        //        string name = property1.Name;
        //        SettingsPropertyValue property2 = new SettingsPropertyValue(property1);
        //        if (dictionary.Contains((object)name))
        //        {
        //            StoredSetting storedSetting = (StoredSetting)dictionary[(object)name];
        //            string escapedString = storedSetting.Value.InnerXml;
        //            if (storedSetting.SerializeAs == SettingsSerializeAs.String)
        //                escapedString = this.Escaper.Unescape(escapedString);
        //            property2.SerializedValue = (object)escapedString;
        //            property2.IsDirty = true;
        //            propertyValueCollection.Add(property2);
        //        }
        //    }
        //    return propertyValueCollection;
        //}

        /// <summary>
        ///   Gets the XML escaper.
        /// </summary>
        /// <value>The escaper.</value>
        static XmlEscaper Escaper { get { return _escaper ?? (_escaper = new XmlEscaper()); } }

        /// <summary>
        ///   Nested class XmlEscaper, performs string escaping and unescaping.
        /// </summary>
        class XmlEscaper
        {
            readonly XmlDocument doc;
            readonly XmlElement temp;

            internal XmlEscaper()
            {
                doc = new XmlDocument();
                temp = doc.CreateElement("temp");
            }

            internal string Escape(string xmlString)
            {
                if (String.IsNullOrEmpty(xmlString))
                    return xmlString;
                return new XElement("dummy", xmlString).Value;
            }

            internal string Unescape(string escapedString)
            {
                if (String.IsNullOrEmpty(escapedString))
                    return escapedString;
                temp.InnerXml = escapedString;
                return temp.InnerText;
            }
        }
    }
}
