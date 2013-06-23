/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Interfaces
{
    public interface IVideoFrame
    {
        // Summary:
        //     Returns the actual exposure duration in seconds (i.e. shutter open time).
        //
        // Exceptions:
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if not supported
        //
        // Remarks:
        //     This may differ from the exposure time corresponding to the requested frame
        //     exposure due to shutter latency, camera timing precision, etc.
        double ExposureDuration { get; }

        //
        // Summary:
        //     Returns the actual exposure start time in the FITS-standard CCYY-MM-DDThh:mm:ss[.sss...]
        //     format, if supported.
        //
        // Exceptions:
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if not supported
        string ExposureStartTime { get; }

        //
        // Summary:
        //     Returns the frame number.
        //
        // Remarks:
        //     The frame number of the first exposed frame may not be zero and is dependent
        //     on the device and/or the driver. The frame number increases with each acquired
        //     frame not with each requested frame by the client.  Must return -1 if frame
        //     numbering is not supported
        long FrameNumber { get; }

        //
        // Summary:
        //     Returns a safearray of int of size ASCOM.DeviceInterface.IVideo.Width * ASCOM.DeviceInterface.IVideo.Height
        //     containing the pixel values from the video frame.
        //
        // Remarks:
        //     The application must inspect the Safearray parameters to determine the dimensions.
        //     The value will be only populated when the video frame has been obtained from
        //     the ASCOM.DeviceInterface.IVideo.LastVideoFrame property. When the video
        //     frame is obtained from the ASCOM.DeviceInterface.IVideo.LastVideoFrameImageArrayVariant
        //     property a NULL value must be returned. Do not throw an exception in this
        //     case.
        //     For color or multispectral cameras, will produce an array of ASCOM.DeviceInterface.IVideo.Width
        //     * ASCOM.DeviceInterface.IVideo.Height * NumPlanes. If the application cannot
        //     handle multispectral images, it should use just the first plane.
        //     The pixels in the array start from the top left part of the image and are
        //     listed by horizontal lines/rows. The second pixels in the array is the second
        //     pixels from the first horizontal row and the second last pixel in the array
        //     is the second last pixels from the last horizontal row.
        object ImageArray { get; }

        //
        // Summary:
        //     Returns additional information associated with the video frame.
        //
        // Remarks:
        //     Must be implemented This property must return an empty string if no additonal
        //     video frame information is supported. Please do not throw a ASCOM.PropertyNotImplementedException.
        string ImageInfo { get; }
    }
}
