/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Horus.Config.ConfigurationPersisters
{
    public class FileSystemPersister : IConfigurationPersister
    {
        private string configFileName;

        public FileSystemPersister(string horusHomePath)
        {
            configFileName = Path.GetFullPath(horusHomePath + @"\HorusSystem.config");
        }

        public string ReadConfiguration()
        {
            return File.ReadAllText(configFileName);
        }

        public void WriteConfiguration(string content)
        {
            File.WriteAllText(configFileName, content);
        }
    }
}
