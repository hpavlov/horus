/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Horus.Config.ConfigurationPersisters;
using Horus.Config.Model;
using Horus.Config.Properties;
using Horus.Model.Helpers;

namespace Horus.Config
{
    public class HorusConfigManager
    {
        private static string HORUS_HOME = "HORUS_HOME";

        public static HorusConfigManager Instance = new HorusConfigManager();

        private object syncRoot = new object();
        private HorusDeviceConfig horusDeviceConfig;
        private IConfigurationPersister configPersister;

        private HorusConfigManager()
        {
            AssertHorusHomeIsConfigured();

            configPersister = new FileSystemPersister(Environment.GetEnvironmentVariable(HORUS_HOME, EnvironmentVariableTarget.Machine));
        }

        public void LoadConfiguration()
        {
            lock (syncRoot)
            {
                try
                {
                    horusDeviceConfig = configPersister.ReadConfiguration().AsDeserialized<HorusDeviceConfig>();
                }
                catch (Exception)
                {
                    horusDeviceConfig = new HorusDeviceConfig();
                }
                
            }
        }

        public void SaveConfiguration()
        {
            lock (syncRoot)
            {
                SaveConfigurationNoLocking();
            }
        }
        public void SaveConfigurationNoLocking()  
        {
            configPersister.WriteConfiguration(horusDeviceConfig.AsSerialized());
        }

        private void AssertHorusHomeIsConfigured()
        {
            string horusHome = Environment.GetEnvironmentVariable(HORUS_HOME, EnvironmentVariableTarget.Machine);
            if (string.IsNullOrEmpty(horusHome) ||
                !Directory.Exists(horusHome))
            {
                throw new ApplicationException("HORUS_HOME hasn't been set up. Have you installed the Horus Server component?");
            }
        }

        private void EnsureDeviceConfig()
        {
            if (horusDeviceConfig == null)
                LoadConfiguration();
        }

        public bool IsLogicalDeviceConfigured(Type driverType, string deviceName)
        {
            lock (syncRoot)
            {
                DriverConfig driverConfig = GetDriverConfigNoLocking(driverType);
                if (driverConfig == null)
                    return false;

                return horusDeviceConfig.Devices.Exists(x =>
                    string.Equals(x.DeviceName, deviceName, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(x.DriverId, driverConfig.DriverId, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public DeviceConfig GetLogicalDeviceConfiguration(Type driverType, string deviceName)
        {
            lock (syncRoot)
            {
                DriverConfig driverConfig = EnsureDriverConfigNoLocking(driverType);

                return horusDeviceConfig.Devices.SingleOrDefault(x =>
                    string.Equals(x.DeviceName, deviceName, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(x.DriverId, driverConfig.DriverId, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public DeviceConfig RegisterLogicalDeviceConfiguration(Type driverType, string deviceName)
        {
            lock (syncRoot)
            {
                DriverConfig driverConfig = EnsureDriverConfigNoLocking(driverType);

                DeviceConfig rv = horusDeviceConfig.Devices.SingleOrDefault(x =>
                    string.Equals(x.DeviceName, deviceName, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(x.DriverId, driverConfig.DriverId, StringComparison.InvariantCultureIgnoreCase));

                if (rv == null)
                {
                    rv = new DeviceConfig()
                    {
                        DeviceName = deviceName,
                        DriverId = driverConfig.DriverId
                    };

                    horusDeviceConfig.Devices.Add(rv);
                }

                return rv;
            }
        }

        public string DriverAddinsPath
        {
            get
            {
                lock (syncRoot)
                {
                    // NOTE: HORUS_HOME must have been set by the installer
                    AssertHorusHomeIsConfigured();

                    string addinsPath = Path.GetFullPath(Environment.GetEnvironmentVariable(HORUS_HOME, EnvironmentVariableTarget.Machine) + @"\LogicalDevices");

                    if (!Directory.Exists(addinsPath))
                        Directory.CreateDirectory(addinsPath);

                    return addinsPath;
                }
            }
        }

        public TDriverConfig GetDeviceDriverData<TDriverConfig>(Type driver, string deviceName) where TDriverConfig : new()
        {
            lock (syncRoot)
            {
                EnsureDeviceConfig();
                bool hasPendingChanges = false;

                DriverConfig driverConfig = GetDriverConfigNoLocking(driver);

                if (driverConfig == null)
                {
                    driverConfig = new DriverConfig()
                    {
                        DriverId = Guid.NewGuid().ToString(),
                        DriverAssemblyName = driver.Assembly.FullName,
                        DriverTypeName = driver.FullName
                    };
                    horusDeviceConfig.Drivers.Add(driverConfig);
                    hasPendingChanges = true;
                }

                DeviceConfig deviceConfig = horusDeviceConfig.Devices
                        .SingleOrDefault(x =>
                            string.Compare(x.DeviceName, deviceName, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                            string.Compare(x.DriverId, driverConfig.DriverId, StringComparison.InvariantCultureIgnoreCase) == 0);

                if (deviceConfig == null)
                {
                    deviceConfig = new DeviceConfig()
                    {
                        DeviceName = deviceName,   
                        DriverId = driverConfig.DriverId,
                        DriverDeviceData = new TDriverConfig().AsSerializedNode()
                    };
                    horusDeviceConfig.Devices.Add(deviceConfig);
                    hasPendingChanges = true;                    
                }

                if (hasPendingChanges)
                {
                    SaveConfigurationNoLocking();
                }

                return deviceConfig.DriverDeviceData.OuterXml.AsDeserialized<TDriverConfig>();
            }
        }

        private DriverConfig GetDriverConfigNoLocking(Type driver)
        {
            return horusDeviceConfig
                    .Drivers
                    .SingleOrDefault(x =>
                        string.Compare(x.DriverAssemblyName, driver.Assembly.FullName, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                        string.Compare(x.DriverTypeName, driver.FullName, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        private DriverConfig EnsureDriverConfigNoLocking(Type driver)
        {
            DriverConfig driverConfig = horusDeviceConfig
                    .Drivers
                    .SingleOrDefault(x =>
                        string.Compare(x.DriverAssemblyName, driver.Assembly.FullName, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                        string.Compare(x.DriverTypeName, driver.FullName, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (driverConfig == null)
            {
                driverConfig = new DriverConfig()
                {
                    DriverId = Guid.NewGuid().ToString(),
                    DriverAssemblyName = driver.Assembly.FullName,
                    DriverTypeName = driver.FullName
                };
                horusDeviceConfig.Drivers.Add(driverConfig);
                SaveConfigurationNoLocking();
            }

            return driverConfig;
        }

        public void SetDeviceDriverData(object settings, Type driver, string deviceName)
        {
            lock (syncRoot)
            {
                EnsureDeviceConfig();

                DriverConfig driverConfig = EnsureDriverConfigNoLocking(driver);

                DeviceConfig deviceConfig = horusDeviceConfig.Devices
                        .SingleOrDefault(x =>
                            string.Compare(x.DeviceName, deviceName, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                            string.Compare(x.DriverId, driverConfig.DriverId, StringComparison.InvariantCultureIgnoreCase) == 0);

                if (deviceConfig == null)
                {
                    deviceConfig = new DeviceConfig()
                    {
                        DeviceName = deviceName,
                        DriverId = driverConfig.DriverId,
                        DriverDeviceData = settings.AsSerializedNode()
                    };
                    horusDeviceConfig.Devices.Add(deviceConfig);
                }
                else
                    deviceConfig.DriverDeviceData = settings.AsSerializedNode();

                SaveConfigurationNoLocking();
            }
        }
    }
}
