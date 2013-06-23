/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Client.System.Persisters;
using Horus.Model.Interfaces;

namespace Horus.Client.Remote
{
    internal class RemoteVideo : IVideo
    {
        private RemoteHorusSession remoteSession;
        private string instanceId;
        private VideoFramePersister videoFramePersister;

        internal RemoteVideo(RemoteHorusSession remoteSession, string instanceId)
        {
            this.remoteSession = remoteSession;
            this.instanceId = instanceId;
            this.videoFramePersister = new VideoFramePersister();
        }

        public int BitDepth
        {
            get { throw new NotImplementedException(); }
        }

        public VideoCameraState CameraState
        {
            get { throw new NotImplementedException(); }
        }

        public int CameraXSize
        {
            get { throw new NotImplementedException(); }
        }

        public int CameraYSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanConfigureImage
        {
            get { throw new NotImplementedException(); }
        }

        public bool Connected
        {
            get
            {
                return remoteSession.InterfaceRemoteProperyGet<IVideo, bool>(instanceId, x => x.Connected);
            }
            set
            {
                remoteSession.InterfaceRemoteProperySet<IVideo, bool>(instanceId, x => x.Connected, value);
            }
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public string DriverInfo
        {
            get { throw new NotImplementedException(); }
        }

        public string DriverVersion
        {
            get { throw new NotImplementedException(); }
        }

        public double ExposureMax
        {
            get { throw new NotImplementedException(); }
        }

        public double ExposureMin
        {
            get { throw new NotImplementedException(); }
        }

        public VideoCameraFrameRate FrameRate
        {
            get { throw new NotImplementedException(); }
        }

        public short Gain
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

        public short GainMax
        {
            get { throw new NotImplementedException(); }
        }

        public short GainMin
        {
            get { throw new NotImplementedException(); }
        }

        public global::System.Collections.ArrayList Gains
        {
            get { throw new NotImplementedException(); }
        }

        public int Gamma
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

        public global::System.Collections.ArrayList Gammas
        {
            get { throw new NotImplementedException(); }
        }

        public int Height
        {
            get { return remoteSession.InterfaceRemoteProperyGet<IVideo, int>(instanceId, x => x.Height); }
        }

        public int IntegrationRate
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

        public short InterfaceVersion
        {
            get { throw new NotImplementedException(); }
        }

        public IVideoFrame LastVideoFrame
        {
            get
            {
                return remoteSession.InterfaceRemoteProperyGet<IVideo, IVideoFrame>(instanceId, x => x.LastVideoFrame);                
            }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public double PixelSizeX
        {
            get { throw new NotImplementedException(); }
        }

        public double PixelSizeY
        {
            get { throw new NotImplementedException(); }
        }

        public string SensorName
        {
            get { throw new NotImplementedException(); }
        }

        public SensorType SensorType
        {
            get { return remoteSession.InterfaceRemoteProperyGet<IVideo, SensorType>(instanceId, x => x.SensorType); }
        }

        public global::System.Collections.ArrayList SupportedActions
        {
            get { throw new NotImplementedException(); }
        }

        public global::System.Collections.ArrayList SupportedIntegrationRates
        {
            get { throw new NotImplementedException(); }
        }

        public string VideoCaptureDeviceName
        {
            get { throw new NotImplementedException(); }
        }

        public string VideoCodec
        {
            get { throw new NotImplementedException(); }
        }

        public string VideoFileFormat
        {
            get { throw new NotImplementedException(); }
        }

        public int VideoFramesBufferSize
        {
            get { throw new NotImplementedException(); }
        }

        public int Width
        {
            get { return remoteSession.InterfaceRemoteProperyGet<IVideo, int>(instanceId, x => x.Width); }
        }

        public string Action(string ActionName, string ActionParameters)
        {
            throw new NotImplementedException();
        }

        public void ConfigureImage()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetupDialog()
        {
            throw new NotImplementedException();
        }

        public string StartRecordingVideoFile(string PreferredFileName)
        {
            throw new NotImplementedException();
        }

        public void StopRecordingVideoFile()
        {
            throw new NotImplementedException();
        }

        public int Version
        {
            get { throw new NotImplementedException(); }
        }

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
    }
}
