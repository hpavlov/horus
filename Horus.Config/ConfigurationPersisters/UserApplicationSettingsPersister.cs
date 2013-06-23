/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

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
