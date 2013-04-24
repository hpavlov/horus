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

        public string DriverAddinsPath
        {
            get
            {
                lock (syncRoot)
                {
                    // NOTE: HORUS_HOME must have been set by the installer
                    AssertHorusHomeIsConfigured();

                    string addinsPath = Path.GetFullPath(Environment.GetEnvironmentVariable(HORUS_HOME, EnvironmentVariableTarget.Machine) + @"\Drivers");

                    if (!Directory.Exists(addinsPath))
                        Directory.CreateDirectory(addinsPath);

                    return addinsPath;
                }
            }
        }

        public TDriverConfig GetDeviceDriverData<TDriverConfig>(Type driver, string deviceId) where TDriverConfig : new()
        {
            lock (syncRoot)
            {
                EnsureDeviceConfig();
                bool hasPendingChanges = false;

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
                    hasPendingChanges = true;
                }

                DeviceConfig deviceConfig = horusDeviceConfig.Devices
                        .SingleOrDefault(x =>
                            string.Compare(x.DeviceId, deviceId, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                            string.Compare(x.DriverId, driverConfig.DriverId, StringComparison.InvariantCultureIgnoreCase) == 0);

                if (deviceConfig == null)
                {
                    deviceConfig = new DeviceConfig()
                    {
                        DeviceId = Guid.NewGuid().ToString(),   
                        DriverId = driverConfig.DriverId,
                        DriverDeviceData = new TDriverConfig().AsSerializedNode()
                    };
                    horusDeviceConfig.Drivers.Add(driverConfig);
                    hasPendingChanges = true;                    
                }

                if (hasPendingChanges)
                {
                    SaveConfigurationNoLocking();
                }

                return deviceConfig.DriverDeviceData.OuterXml.AsDeserialized<TDriverConfig>();
            }
        }
    }
}
