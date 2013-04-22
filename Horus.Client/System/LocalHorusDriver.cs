using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Horus.Client.System
{
    public class LocalHorusDriver
    {
        public Type Implementor { get; private set; }
        public Assembly Assembly { get; private set; }

        internal LocalHorusDriver(Assembly assembly, Type implementor)
        {
            Implementor = implementor;
            Assembly = assembly;
        }
    }
}
