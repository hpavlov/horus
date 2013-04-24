using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Config.ConfigurationPersisters
{
    public interface IConfigurationPersister
    {
        string ReadConfiguration();
        void WriteConfiguration(string content);
    }
}
