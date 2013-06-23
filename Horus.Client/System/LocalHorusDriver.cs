/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Horus.Client.System
{
    internal class LocalHorusDriver
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
