﻿// -----------------------------------------------------------------------
//  <copyright file="" company="TiGra Astronomy">
//      Copyright © 2010 TiGra Networks, all rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
// 
// Based on code by Mike Nathan, found at http://www.geek-republic.com/2010/11/c-portable-settings-provider/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using TiGra;

namespace Horus.Config
{
    public class HorusSettingsProvider : SettingsProvider
    {
        // Define some static strings later used in our XML creation
        // XML Root node
        const string XMLROOT = "configuration";

        // Configuration declaration node
        const string CONFIGNODE = "configSections";

        // Configuration section group declaration node
        const string GROUPNODE = "sectionGroup";

        // User section node
        const string USERNODE = "userSettings";

        // Application Specific Node
        readonly string APPNODE;

        /// <summary>
        ///   The assembly for which settings are being managed.
        /// </summary>
        readonly Assembly settingsAssembly;

        readonly string settingsAssemblyName;
        XmlDocument xmlDoc;

        /// <summary>
        ///   Initializes a new instance of the <see cref="HorusSettingsProvider" /> class.
        /// </summary>
        public HorusSettingsProvider()
        {
            settingsAssembly = WhoCalledMe();
            settingsAssemblyName = settingsAssembly.GetName().Name;
            APPNODE = settingsAssemblyName + ".Properties.Settings";
            Diagnostics.TraceInfo("Managing settings for {0} in {1}", settingsAssemblyName, APPNODE);
        }

        // Override the ApplicationName property, returning the solution name.  No need to set anything, we just need to
        // retrieve information, though the set method still needs to be defined.
        /// <summary>
        ///   Gets or sets the name of the currently running application.
        /// </summary>
        /// <value>The name of the assembly for which settings are being managed.</value>
        /// <returns>
        ///   A <see cref="T:System.String" /> that contains the assembly's short name, for example, <c>My.Settings.Assembly</c>.
        /// </returns>
        public override string ApplicationName { get { return settingsAssemblyName; } set { return; } }

        /// <summary>
        ///   Gets the XML config.
        /// </summary>
        /// <value>The XML config.</value>
        XmlDocument XMLConfig
        {
            get
            {
                // Check if we already have accessed the XML config file. If the xmlDoc object is empty, we have not.
                if (xmlDoc == null)
                {
                    xmlDoc = new XmlDocument();

                    // If we have not loaded the config, try reading the file from disk.
                    try
                    {
                        xmlDoc.Load(Path.Combine(GetAppPath(), GetSettingsFilename()));
                    }

                        // If the file does not exist on disk, catch the exception then create the XML template for the file.
                    catch (Exception)
                    {
                        // XML Declaration
                        // <?xml version="1.0" encoding="utf-8"?>
                        XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        xmlDoc.AppendChild(dec);

                        // Create root node and append to the document
                        // <configuration>
                        XmlElement rootNode = xmlDoc.CreateElement(XMLROOT);
                        xmlDoc.AppendChild(rootNode);

                        // Create Configuration Sections node and add as the first node under the root
                        // <configSections>
                        XmlElement configNode = xmlDoc.CreateElement(CONFIGNODE);
                        xmlDoc.DocumentElement.PrependChild(configNode);

                        // Create the user settings section group declaration and append to the config node above
                        // <sectionGroup name="userSettings"...>
                        XmlElement groupNode = xmlDoc.CreateElement(GROUPNODE);
                        groupNode.SetAttribute("name", USERNODE);
                        groupNode.SetAttribute("type", "System.Configuration.UserSettingsGroup");
                        configNode.AppendChild(groupNode);

                        // Create the Application section declaration and append to the groupNode above
                        // <section name="AppName.Properties.Settings"...>
                        XmlElement newSection = xmlDoc.CreateElement("section");
                        newSection.SetAttribute("name", APPNODE);
                        newSection.SetAttribute("type", "System.Configuration.ClientSettingsSection");
                        groupNode.AppendChild(newSection);

                        // Create the userSettings node and append to the root node
                        // <userSettings>
                        XmlElement userNode = xmlDoc.CreateElement(USERNODE);
                        xmlDoc.DocumentElement.AppendChild(userNode);

                        // Create the Application settings node and append to the userNode above
                        // <AppName.Properties.Settings>
                        XmlElement appNode = xmlDoc.CreateElement(APPNODE);
                        userNode.AppendChild(appNode);
                    }
                }
                return xmlDoc;
            }
        }

        /// <summary>
        ///   Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(ApplicationName, config);
            Diagnostics.TraceVerbose("Provider initialized");
        }

        // Simply returns the name of the settings file, which is the solution name plus ".config"
        public virtual string GetSettingsFilename() { return settingsAssemblyName + ".config"; }

        // Gets current executable path in order to determine where to read and write the config file
        public virtual string GetAppPath() { return new FileInfo(settingsAssembly.Location).DirectoryName; }

        // Retrieve settings from the configuration file
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext sContext, SettingsPropertyCollection settingsColl)
        {
            // Create a collection of values to return
            var retValues = new SettingsPropertyValueCollection();

            // Create a temporary SettingsPropertyValue to reuse
            SettingsPropertyValue setVal;

            // Loop through the list of settings that the application has requested and add them
            // to our collection of return values.
            foreach (SettingsProperty sProp in settingsColl)
            {
                setVal = new SettingsPropertyValue(sProp);
                setVal.IsDirty = false;
                setVal.SerializedValue = GetSetting(sProp);
                retValues.Add(setVal);
            }
            return retValues;
        }

        // Save any of the applications settings that have changed (flagged as "dirty")
        public override void SetPropertyValues(SettingsContext sContext, SettingsPropertyValueCollection settingsColl)
        {
            // Set the values in XML
            foreach (SettingsPropertyValue spVal in settingsColl)
            {
                SetSetting(spVal);
            }

            // Write the XML file to disk
            try
            {
                XMLConfig.Save(Path.Combine(GetAppPath(), GetSettingsFilename()));
            }
            catch (Exception ex)
            {
                Diagnostics.TraceError(ex);

                // Uncomment the following line to enable a MessageBox for forms-based apps
                // MessageBox.Show(ex.Message, "Error writting configuration file to disk", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Uncomment the following line to enable a console message for console-based apps
                //Console.WriteLine("Error writing configuration file to disk: " + ex.Message);
            }
        }

        // Retrieve values from the configuration file, or if the setting does not exist in the file, 
        // retrieve the value from the application's default configuration
        object GetSetting(SettingsProperty setProp)
        {
            object retVal;
            try
            {
                // Search for the specific settings node we are looking for in the configuration file.
                // If it exists, return the InnerText or InnerXML of its first child node, depending on the setting type.

                // If the setting is serialized as a string, return the text stored in the config
                if (setProp.SerializeAs.ToString() == "String")
                {
                    return XMLConfig.SelectSingleNode("//setting[@name='" + setProp.Name + "']").FirstChild.InnerText;
                }

                    // If the setting is stored as XML, deserialize it and return the proper object.  This only supports
                // StringCollections at the moment - I will likely add other types as I use them in applications.
                else
                {
                    string settingType = setProp.PropertyType.ToString();
                    string xmlData = XMLConfig.SelectSingleNode("//setting[@name='" + setProp.Name + "']").FirstChild.InnerXml;
                    var xs = new XmlSerializer(typeof(string[]));
                    var data = (string[])xs.Deserialize(new XmlTextReader(xmlData, XmlNodeType.Element, null));

                    switch (settingType)
                    {
                        case "System.Collections.Specialized.StringCollection":
                            var sc = new StringCollection();
                            sc.AddRange(data);
                            return sc;
                        default:
                            return "";
                    }
                }
            }
            catch (Exception)
            {
                // Check to see if a default value is defined by the application.
                // If so, return that value, using the same rules for settings stored as Strings and XML as above
                if ((setProp.DefaultValue != null))
                {
                    if (setProp.SerializeAs.ToString() == "String")
                    {
                        retVal = setProp.DefaultValue.ToString();
                    }
                    else
                    {
                        string settingType = setProp.PropertyType.ToString();
                        string xmlData = setProp.DefaultValue.ToString();
                        var xs = new XmlSerializer(typeof(string[]));
                        var data = (string[])xs.Deserialize(new XmlTextReader(xmlData, XmlNodeType.Element, null));

                        switch (settingType)
                        {
                            case "System.Collections.Specialized.StringCollection":
                                var sc = new StringCollection();
                                sc.AddRange(data);
                                return sc;

                            default:
                                return "";
                        }
                    }
                }
                else
                {
                    retVal = "";
                }
            }
            return retVal;
        }

        void SetSetting(SettingsPropertyValue setProp)
        {
            // Define the XML path under which we want to write our settings if they do not already exist
            XmlNode SettingNode = null;

            try
            {
                // Search for the specific settings node we want to update.
                // If it exists, return its first child node, (the <value>data here</value> node)
                SettingNode = XMLConfig.SelectSingleNode("//setting[@name='" + setProp.Name + "']").FirstChild;
            }
            catch (Exception)
            {
                SettingNode = null;
            }

            // If we have a pointer to an actual XML node, update the value stored there
            if ((SettingNode != null))
            {
                if (setProp.Property.SerializeAs.ToString() == "String")
                {
                    SettingNode.InnerText = setProp.SerializedValue.ToString();
                }
                else
                {
                    // Write the object to the config serialized as Xml - we must remove the Xml declaration when writing
                    // the value, otherwise .Net's configuration system complains about the additional declaration.
                    SettingNode.InnerXml = setProp.SerializedValue.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-16""?>", "");
                }
            }
            else
            {
                // If the value did not already exist in this settings file, create a new entry for this setting

                // Search for the application settings node (<Appname.Properties.Settings>) and store it.
                XmlNode tmpNode = XMLConfig.SelectSingleNode("//" + APPNODE);

                // Create a new settings node and assign its name as well as how it will be serialized
                XmlElement newSetting = xmlDoc.CreateElement("setting");
                newSetting.SetAttribute("name", setProp.Name);

                if (setProp.Property.SerializeAs.ToString() == "String")
                {
                    newSetting.SetAttribute("serializeAs", "String");
                }
                else
                {
                    newSetting.SetAttribute("serializeAs", "Xml");
                }

                // Append this node to the application settings node (<Appname.Properties.Settings>)
                tmpNode.AppendChild(newSetting);

                // Create an element under our named settings node, and assign it the value we are trying to save
                XmlElement valueElement = xmlDoc.CreateElement("value");
                if (setProp.Property.SerializeAs.ToString() == "String")
                {
                    valueElement.InnerText = setProp.SerializedValue.ToString();
                }
                else
                {
                    // Write the object to the config serialized as Xml - we must remove the Xml declaration when writing
                    // the value, otherwise .Net's configuration system complains about the additional declaration
                    valueElement.InnerXml = setProp.SerializedValue.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-16""?>", "");
                }

                //Append this new element under the setting node we created above
                newSetting.AppendChild(valueElement);
            }
        }

        /// <summary>
        ///   Determines the name of the assembly that invoked this settings provider.
        ///   This is done by walking up the call stack until we find an assembly that is not
        ///   'mscorlib.dll' and does not begin with 'System.'
        /// </summary>
        /// <returns>
        ///   A <see cref="Assembly" /> referencing the executable or DLL for which settings are required.
        /// </returns>
        Assembly WhoCalledMe()
        {
            var stackTrace = new StackTrace(10);
            StackFrame[] stackFrames = stackTrace.GetFrames();

            var excludedPrefixes = new List<string> { "mscorlib.", "System." };
            foreach (StackFrame stackFrame in stackFrames)
            {
                MethodBase method = stackFrame.GetMethod();
                Type declaringType = method.DeclaringType;
                Assembly assembly = declaringType.Assembly;
                string name = assembly.GetName().Name;
                if (name.StartsWith("mscorlib") || name.StartsWith("System."))
                {
                    continue;
                }
                return assembly;
            }
            // If we don't find a match, return the entry assembly, which will normally be the process executable.
            return Assembly.GetEntryAssembly();
        }
    }
}