using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Config.Properties;

namespace Horus.Config.ConfigurationPersisters
{
    public class UserApplicationSettingsPersister : IConfigurationPersister
    {
        public UserApplicationSettingsPersister()
        { }

        public string ReadConfiguration()
        {
            return Settings.Default.HorusDeviceConfig;
        }

        public void WriteConfiguration(string content)
        {
            Settings.Default.HorusDeviceConfig = content;
        }
    }
}
