using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Interfaces
{
    public enum SensorType
    {
        // Summary:
        //     Camera produces monochrome array with no Bayer encoding
        Monochrome = 0,
        //
        // Summary:
        //     Camera produces color image directly, requiring not Bayer decoding
        Color = 1,
        //
        // Summary:
        //     Camera produces RGGB encoded Bayer array images
        RGGB = 2,
        //
        // Summary:
        //     Camera produces CMYG encoded Bayer array images
        CMYG = 3,
        //
        // Summary:
        //     Camera produces CMYG2 encoded Bayer array images
        CMYG2 = 4,
        //
        // Summary:
        //     Camera produces Kodak TRUESENSE Bayer LRGB array images
        LRGB = 5,
    }

    public enum VideoCameraFrameRate
    {
        // Summary:
        //     This is a video camera that supports variable frame rates
        Variable = 0,
        //
        // Summary:
        //     This is a digital camera or system that supports variable frame rates
        Digital = 0,
        //
        // Summary:
        //     25 frames per second (fps) corresponding to a PAL (colour) or CCIR (black
        //     and white) video standard
        PAL = 1,
        //
        // Summary:
        //     29.97 frames per second (fps) corresponding to an NTSC (colour) or EIAb>
        //     (black and white) video standard
        NTSC = 2,
    }

    public enum VideoCameraState
    {
        // Summary:
        //     Camera status idle. The video camera expecting commands
        videoCameraIdle = 0,
        //
        // Summary:
        //     Camera status running. The video is receiving signal and video frames are
        //     available for viewing or recording
        videoCameraRunning = 1,
        //
        // Summary:
        //     Camera status recording. The video camera is recording video to the file
        //     system. Video frames are available for viewing
        videoCameraRecording = 2,
        //
        // Summary:
        //     Camera status error. The video camera is in a state of an error and cannot
        //     continue its operation. Usually a restart will be required to resolve the
        //     error condition
        videoCameraError = 3,
    }
}
