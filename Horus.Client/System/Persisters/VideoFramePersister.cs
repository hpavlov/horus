/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.Remote.Envelops;
using Horus.Model.Helpers;
using Horus.Model.Interfaces;

namespace Horus.Client.System.Persisters
{
    public class VideoFramePersister : IModelPersister
    {
        public string ToHttpResponse(object instance)
        {
            // TODO: We may need a better/smaller/faster serialization
            VideoFrameEnvelop videoFrame = new VideoFrameEnvelop(instance as IVideoFrame);

            string respnse = videoFrame.AsSerialized();

            return respnse;
        }

        public object FromHttpResponse(string response)
        {
            // TODO: We may need a better/smaller/faster serialization

            return (IVideoFrame)response.AsDeserialized<VideoFrameEnvelop>();
        }
    }
}
