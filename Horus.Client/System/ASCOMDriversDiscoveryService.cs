/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Model.Drivers;

namespace Horus.Client.System
{
    internal class ASCOMDriversDiscoveryService
    {
        public static LocalHorusDriver[] DriscoverAvailableDrivers()
        {
            // TODO: As ASCOM drivers will need a Horus wrapper to work, the available Horus drivers that 
            //       connect to ASCOM enabled devices are actually the available ASCOM devices for which we have 
            //       a Horus wrapper implemented. This method will find them.

            // NOTE: How do we filter out the Simulators out?

            return new LocalHorusDriver[] { };
        }
    }
}
