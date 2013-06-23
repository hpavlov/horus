/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Config;
using Horus.Model.Interfaces;

namespace Horus.Client.System
{
    public class HorusContext : IHorusContext
    {
        private HorusSession session;

        public HorusContext(HorusSession session)
        {
            this.session = session;

            HorusConfigManager.Instance.LoadConfiguration();
        }

        public TSettings ReadDriverSettings<TSettings>(Type driver, string deviceId) where TSettings: new()
        {
            return HorusConfigManager.Instance.GetDeviceDriverData<TSettings>(driver, deviceId);
        }

        public void WriteDriverSettings(object settings, Type driver, string deviceId)
        {
            HorusConfigManager.Instance.SetDeviceDriverData(settings, driver, deviceId);
        }
    }
}
