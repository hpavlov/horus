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
