/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Model.Drivers;

namespace Horus.Model.Server
{
    public class HorusLogicalDeviceSummaryList
    {
        public HorusDeviceSummary[] LogicalDevices;

        public HorusLogicalDeviceSummaryList()
        {
            // Required fro XML serialization
        }

        public HorusLogicalDeviceSummaryList(List<HorusDeviceSummary> logicalDevices)
        {
            LogicalDevices = new List<HorusDeviceSummary>(logicalDevices).ToArray();
        }
    }
}
