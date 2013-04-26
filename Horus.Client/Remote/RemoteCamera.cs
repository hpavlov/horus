using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Model.Interfaces;

namespace Horus.Client.Remote
{
    internal class RemoteCamera : ICamera
    {
        private RemoteHorusSession remoteSession;
        private string instanceId;

        internal RemoteCamera(RemoteHorusSession remoteSession, string instanceId)
        {
            this.remoteSession = remoteSession;
            this.instanceId = instanceId;
        }

        public string Method1(int arg1)
        {
            return remoteSession.InterfaceRemoteFunctionCall<ICamera, string>(instanceId, x => x.Method1(arg1));
        }

        public int Property1
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Version
        {
            get { throw new NotImplementedException(); }
        }

        // TODO: The IHorusDriver should not be a base class of the device interfaces and these methods below should disappear
        //       The IHorusDriver should be an intenal interface only visible to the LocalHorusSession and configuration

        public Model.Drivers.HorusEnabledDeviceSummary[] GetAvailableDevices()
        {
            throw new NotImplementedException();
        }

        public void Initialize(IHorusContext horusContext)
        {
            throw new NotImplementedException();
        }

        public void LinkToDevice(string deviceName)
        {
            throw new NotImplementedException();
        }

        public void SetupDialog()
        {
            throw new NotImplementedException();
        }
    }
}
