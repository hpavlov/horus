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
