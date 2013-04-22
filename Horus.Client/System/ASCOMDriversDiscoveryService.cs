using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Model.Drivers;

namespace Horus.ClientFrameWork.CS.System
{
    public class ASCOMDriversDiscoveryService
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
