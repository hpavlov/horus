/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Interfaces
{
    public interface IVideo : IHorusDriver
    {
        // Summary:
        //     Reports the bit depth the camera can produce.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if data unavailable.
        int BitDepth { get; }

        //
        // Summary:
        //     Returns the current camera operational state
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must return an exception if the camera status is unavailable.
        //
        // Remarks:
        //     Returns one of the following status information: Value State Meaning 0 CameraIdle
        //     At idle state, camera is available for commands 1 CameraBusy The camera is
        //     waiting for operation to complete. The camera is not responding to commands
        //     right now 2 CameraRunning The camera is running and video frames are available
        //     for viewing and recording 3 CameraRecording The camera is running and recording
        //     a video 4 CameraError Camera error condition serious enough to prevent further
        //     operations (connection fail, etc.).
        VideoCameraState CameraState { get; }

        //
        // Summary:
        //     Returns the width of the video camera CCD chip in unbinned pixels.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if the value is not known
        int CameraXSize { get; }

        //
        // Summary:
        //     Returns the height of the video camera CCD chip in unbinned pixels.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if the value is not known
        int CameraYSize { get; }

        //
        // Summary:
        //     Returns True if the camera supports custom image configuration via the ASCOM.DeviceInterface.IVideo.ConfigureImage()
        //     method.
        //
        // Remarks:
        //     Must be implemented
        bool CanConfigureImage { get; }

        //
        // Summary:
        //     Set True to connect to the device. Set False to disconnect from the device.
        //      You can also read the property to check whether it is connected.
        //
        // Exceptions:
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implementedDo not use a NotConnectedException here, that exception
        //     is for use in other methods that require a connection in order to succeed.
        bool Connected { get; set; }

        //
        // Summary:
        //     Returns a description of the device, such as manufacturer and modelnumber.
        //     Any ASCII characters may be used.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when
        //     connected.
        //
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implemented
        string Description { get; }

        //
        // Summary:
        //     Descriptive and version information about this ASCOM driver.
        //
        // Exceptions:
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implemented This string may contain line endings and may be hundreds
        //     to thousands of characters long.  It is intended to display detailed information
        //     on the ASCOM driver, including version and copyright data.  See the ASCOM.DeviceInterface.IVideo.Description
        //     property for information on the device itself.  To get the driver version
        //     in a parseable string, use the ASCOM.DeviceInterface.IVideo.DriverVersion
        //     property.
        string DriverInfo { get; }

        //
        // Summary:
        //     A string containing only the major and minor version of the driver.
        //
        // Exceptions:
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implemented This must be in the form "n.n".  It should not to be
        //     confused with the ASCOM.DeviceInterface.IVideo.InterfaceVersion property,
        //     which is the version of this specification supported by the driver.
        string DriverVersion { get; }

        //
        // Summary:
        //     The maximum supported exposure (integration time) in seconds.
        //
        // Remarks:
        //     This value is for information purposes only. The exposure cannot be set directly
        //     in seconds, use ASCOM.DeviceInterface.IVideo.IntegrationRate method to change
        //     the exposure.
        double ExposureMax { get; }

        //
        // Summary:
        //     The minimum supported exposure (integration time) in seconds.
        //
        // Remarks:
        //     This value is for information purposes only. The exposure cannot be set directly
        //     in seconds, use ASCOM.DeviceInterface.IVideo.IntegrationRate method to change
        //     the exposure.
        double ExposureMin { get; }

        //
        // Summary:
        //     The frame reate at which the camera is running.
        //
        // Remarks:
        //     Analogue cameras run in one of the two fixes frame rates - 25fps for PAL
        //     video and 29.97fps for NTSC video. Digital cameras usually can run on variable
        //     framerate.
        VideoCameraFrameRate FrameRate { get; }

        //
        // Summary:
        //     Index into the ASCOM.DeviceInterface.IVideo.Gains array for the selected
        //     camera gain
        //
        // Returns:
        //     Index into the Gains array for the selected camera gain
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        //   ASCOM.InvalidValueException:
        //     Must throw an exception if not valid.
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if gain is not supported
        //
        // Remarks:
        //     ASCOM.DeviceInterface.IVideo.Gain can be used to adjust the gain setting
        //     of the camera, if supported. There are two typical usage scenarios: Discrete
        //     gain video cameras will return a 0-based array of strings - ASCOM.DeviceInterface.IVideo.Gains,
        //     which correspond to different disctere gain settings supported by the camera.
        //     ASCOM.DeviceInterface.IVideo.Gain must be set to an integer in this range.
        //     ASCOM.DeviceInterface.IVideo.GainMin and ASCOM.DeviceInterface.IVideo.GainMax
        //     must thrown an exception if this mode is used.  Adjustable gain video cameras
        //     - ASCOM.DeviceInterface.IVideo.GainMin and ASCOM.DeviceInterface.IVideo.GainMax
        //     return integers, which specify the valid range for ASCOM.DeviceInterface.IVideo.GainMin
        //     and ASCOM.DeviceInterface.IVideo.Gain.
        //     The driver must default ASCOM.DeviceInterface.IVideo.Gain to a valid value.
        short Gain { get; set; }

        //
        // Summary:
        //     Maximum value of ASCOM.DeviceInterface.IVideo.Gain
        //
        // Returns:
        //     The maximum gain value that this camera supports
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if gainmax is not supported
        //
        // Remarks:
        //     When specifying the gain setting with an integer value, ASCOM.DeviceInterface.IVideo.GainMax
        //     is used in conjunction with ASCOM.DeviceInterface.IVideo.GainMin to specify
        //     the range of valid settings.
        //     ASCOM.DeviceInterface.IVideo.GainMax shall be greater than ASCOM.DeviceInterface.IVideo.GainMin.
        //     If either is available, then both must be available.
        //     Please see ASCOM.DeviceInterface.IVideo.Gain for more information.
        //     It is recommended that this function be called only after a ASCOM.DeviceInterface.IVideo.Connected
        //     is established with the camera hardware, to ensure that the driver is aware
        //     of the capabilities of the specific camera model.
        short GainMax { get; }

        //
        // Summary:
        //     Minimum value of ASCOM.DeviceInterface.IVideo.Gain
        //
        // Returns:
        //     The minimum gain value that this camera supports
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if gainmin is not supported
        //
        // Remarks:
        //     When specifying the gain setting with an integer value, ASCOM.DeviceInterface.IVideo.GainMin
        //     is used in conjunction with ASCOM.DeviceInterface.IVideo.GainMax to specify
        //     the range of valid settings.
        //     ASCOM.DeviceInterface.IVideo.GainMax shall be greater than ASCOM.DeviceInterface.IVideo.GainMin.
        //     If either is available, then both must be available.
        //     Please see ASCOM.DeviceInterface.IVideo.Gain for more information.
        //     It is recommended that this function be called only after a ASCOM.DeviceInterface.IVideo.Connected
        //     is established with the camera hardware, to ensure that the driver is aware
        //     of the capabilities of the specific camera model.
        short GainMin { get; }

        //
        // Summary:
        //     Gains supported by the camera
        //
        // Returns:
        //     An ArrayList of gain names or values
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if gainmin is not supported
        //
        // Remarks:
        //     ASCOM.DeviceInterface.IVideo.Gains provides a 0-based array of available
        //     gain settings. This is often used to specify ISO settings for DSLR cameras.
        //     Typically the application software will display the available gain settings
        //     in a drop list. The application will then supply the selected index to the
        //     driver via the ASCOM.DeviceInterface.IVideo.Gain property.
        //     The ASCOM.DeviceInterface.IVideo.Gain setting may alternatively be specified
        //     using integer values; if this mode is used then ASCOM.DeviceInterface.IVideo.Gains
        //     is invalid and must throw an exception. Please see ASCOM.DeviceInterface.IVideo.GainMax
        //     and ASCOM.DeviceInterface.IVideo.GainMin for more information.
        //     It is recommended that this function be called only after a ASCOM.DeviceInterface.IVideo.Connected
        //     is established with the camera hardware, to ensure that the driver is aware
        //     of the capabilities of the specific camera model.
        ArrayList Gains { get; }

        //
        // Summary:
        //     Index into the ASCOM.DeviceInterface.IVideo.Gammas array for the selected
        //     camera gamma
        //
        // Returns:
        //     Index into the Gammas array for the selected camera gamma
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        //   ASCOM.InvalidValueException:
        //     Must throw an exception if not valid.
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if gamma is not supported
        //
        // Remarks:
        //     ASCOM.DeviceInterface.IVideo.Gamma can be used to adjust the gamma setting
        //     of the camera, if supported. A 0-based array of strings - ASCOM.DeviceInterface.IVideo.Gammas,
        //     which correspond to different disctere gamma settings supported by the camera
        //     will be returned. ASCOM.DeviceInterface.IVideo.Gamma must be set to an integer
        //     in this range.
        //     The driver must default ASCOM.DeviceInterface.IVideo.Gamma to a valid value.
        int Gamma { get; set; }

        //
        // Summary:
        //     Gamma values supported by the camera
        //
        // Returns:
        //     An ArrayList of gamma names or values
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if gainmin is not supported
        //
        // Remarks:
        //     ASCOM.DeviceInterface.IVideo.Gammas provides a 0-based array of available
        //     gamma settings.  Typically the application software will display the available
        //     gamma settings in a drop list. The application will then supply the selected
        //     index to the driver via the ASCOM.DeviceInterface.IVideo.Gamma property.
        //     It is recommended that this function be called only after a ASCOM.DeviceInterface.IVideo.Connected
        //     is established with the camera hardware, to ensure that the driver is aware
        //     of the capabilities of the specific camera model.
        ArrayList Gammas { get; }

        //
        // Summary:
        //     Returns the height of the video frame in pixels.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if the value is not known
        //
        // Remarks:
        //     For analogue video cameras working via a frame grabber the dimentions of
        //     the video frames may be different than the dimention of the CCD chip
        int Height { get; }

        //
        // Summary:
        //     Index into the ASCOM.DeviceInterface.IVideo.SupportedIntegrationRates array
        //     for the selected camera integration rate
        //
        // Returns:
        //     Index into the SupportedIntegrationRates array for the selected camera integration
        //     rate
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        //   ASCOM.InvalidValueException:
        //     Must throw an exception if not valid.
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if the camera supports only one integration rate
        //     (exposure) that cannot be changed.
        //
        // Remarks:
        //     ASCOM.DeviceInterface.IVideo.IntegrationRate can be used to adjust the integration
        //     rate (exposure) of the camera, if supported. A 0-based array of strings -
        //     ASCOM.DeviceInterface.IVideo.SupportedIntegrationRates, which correspond
        //     to different disctere integration rate settings supported by the camera will
        //     be returned. ASCOM.DeviceInterface.IVideo.IntegrationRate must be set to
        //     an integer in this range.
        //     The driver must default ASCOM.DeviceInterface.IVideo.IntegrationRate to a
        //     valid value when integration rate is supported by the camera.
        int IntegrationRate { get; set; }

        //
        // Summary:
        //     The interface version number that this device supports. Should return 2 for
        //     this interface version.
        //
        // Exceptions:
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implemented Clients can detect legacy V1 drivers by trying to read
        //     ths property.  If the driver raises an error, it is a V1 driver. V1 did not
        //     specify this property. A driver may also return a value of 1. In other words,
        //     a raised error or a return value of 1 indicates that the driver is a V1 driver.
        short InterfaceVersion { get; }

        //
        // Summary:
        //     Returns a ASCOM.DeviceInterface.IVideoFrame with its ASCOM.DeviceInterface.IVideoFrame.ImageArray
        //     property populated.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if data unavailable.
        //
        //   ASCOM.InvalidOperationException:
        //     If called before any video frame has been taken
        //
        // Remarks:
        //     The ASCOM.DeviceInterface.IVideoFrame.ImageArrayVariant property of the video
        //     frame will not be populated. Use the ASCOM.DeviceInterface.IVideo.LastVideoFrameImageArrayVariant
        //     property to obtain a video frame that has the ASCOM.DeviceInterface.IVideoFrame.ImageArrayVariant
        //     populated.
        IVideoFrame LastVideoFrame { get; }

        //
        // Summary:
        //     The short name of the driver, for display purposes.
        //
        // Exceptions:
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implemented
        string Name { get; }

        //
        // Summary:
        //     Returns the width of the CCD chip pixels in microns.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if data unavailable.
        double PixelSizeX { get; }

        //
        // Summary:
        //     Returns the height of the CCD chip pixels in microns.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if data unavailable.
        double PixelSizeY { get; }

        //
        // Summary:
        //     Sensor name
        //
        // Returns:
        //     The name of sensor used within the camera
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.IVideo.Connected in order to
        //     retrieve necessary information from the camera.)
        //
        // Remarks:
        //     Returns the name (datasheet part number) of the sensor, e.g. ICX285AL. The
        //     format is to be exactly as shown on manufacturer data sheet, subject to the
        //     following rules. All letter shall be uppercase. Spaces shall not be included.
        //     Any extra suffixes that define region codes, package types, temperature range,
        //     coatings, grading, color/monochrome, etc. shall not be included. For color
        //     sensors, if a suffix differentiates different Bayer matrix encodings, it
        //     shall be included.
        //     Examples:
        //     ICX285AL-F shall be reported as ICX285 KAF-8300-AXC-CD-AA shall be reported
        //     as KAF-8300
        //     Note:
        //     The most common usage of this property is to select approximate color balance
        //     parameters to be applied to the Bayer matrix of one-shot color sensors. Application
        //     authors should assume that an appropriate IR cutoff filter is in place for
        //     color sensors.
        //     It is recommended that this function be called only after a ASCOM.DeviceInterface.IVideo.Connected
        //     is established with the camera hardware, to ensure that the driver is aware
        //     of the capabilities of the specific camera model.
        //     This is only available for the Camera Interface Version 2
        string SensorName { get; }

        //
        // Summary:
        //     Type of colour information returned by the the camera sensor
        //
        // Returns:
        //     The ASCOM.DeviceInterface.SensorType enum value of the camera sensor
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the information is not available. (Some drivers
        //     may require an active ASCOM.DeviceInterface.ICameraV2.Connected in order
        //     to retrieve necessary information from the camera.)
        //
        // Remarks:
        //     This is only available for the Camera Interface Version 2
        //     ASCOM.DeviceInterface.ICameraV2.SensorType returns a value indicating whether
        //     the sensor is monochrome, or what Bayer matrix it encodes. The following
        //     values are defined:
        //     Value Enumeration Meaning 0 Monochrome Camera produces monochrome array with
        //     no Bayer encoding 1 Colour Camera produces color image directly, requiring
        //     not Bayer decoding 2 RGGB Camera produces RGGB encoded Bayer array images
        //     3 CMYG Camera produces CMYG encoded Bayer array images 4 CMYG2 Camera produces
        //     CMYG2 encoded Bayer array images 5 LRGB Camera produces Kodak TRUESENSE Bayer
        //     LRGB array images
        //     Please note that additional values may be defined in future updates of the
        //     standard, as new Bayer matrices may be created by sensor manufacturers in
        //     the future. If this occurs, then a new enumeration value shall be defined.
        //     The pre-existing enumeration values shall not change.
        //     ASCOM.DeviceInterface.ICameraV2.SensorType can possibly change between exposures,
        //     for example if ASCOM.DeviceInterface.ICameraV2.ReadoutMode is changed, and
        //     should always be checked after each exposure.
        //     In the following definitions, R = red, G = green, B = blue, C = cyan, M =
        //     magenta, Y = yellow. The Bayer matrix is defined with X increasing from left
        //     to right, and Y increasing from top to bottom. The pattern repeats every
        //     N x M pixels for the entire pixel array, where N is the height of the Bayer
        //     matrix, and M is the width.
        //     RGGB indicates the following matrix:
        //     X = 0 X = 1 Y = 0 R G Y = 1 G B
        //     CMYG indicates the following matrix:
        //     X = 0 X = 1 Y = 0 Y C Y = 1 G M
        //     CMYG2 indicates the following matrix:
        //     X = 0 X = 1 Y = 0 C Y Y = 1 M G Y = 2 C Y Y = 3 G M
        //     LRGB indicates the following matrix (Kodak TRUESENSE):
        //     X = 0 X = 1 X = 2 X = 3 Y = 0 L R L G Y = 1 R L G L Y = 2 L G L B Y = 3 G
        //     L B L
        //     The alignment of the array may be modified by ASCOM.DeviceInterface.ICameraV2.BayerOffsetX
        //     and ASCOM.DeviceInterface.ICameraV2.BayerOffsetY. The offset is measured
        //     from the 0,0 position in the sensor array to the upper left corner of the
        //     Bayer matrix table. Please note that the Bayer offset values are not affected
        //     by subframe settings.
        //     For example, if a CMYG2 sensor has a Bayer matrix offset as shown below,
        //     ASCOM.DeviceInterface.ICameraV2.BayerOffsetX is 0 and ASCOM.DeviceInterface.ICameraV2.BayerOffsetY
        //     is 1:
        //     X = 0 X = 1 Y = 0 G M Y = 1 C Y Y = 2 M G Y = 3 C Y
        //     It is recommended that this function be called only after a ASCOM.DeviceInterface.ICameraV2.Connected
        //     is established with the camera hardware, to ensure that the driver is aware
        //     of the capabilities of the specific camera model.
        SensorType SensorType { get; }

        //
        // Summary:
        //     Returns the list of action names supported by this driver.
        //
        // Exceptions:
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implemented This method must return an empty arraylist if no actions
        //     are supported. Please do not throw a ASCOM.PropertyNotImplementedException.
        //     This is an aid to client authors and testers who would otherwise have to
        //     repeatedly poll the driver to determine its capabilities. Returned action
        //     names may be in mixed case to enhance presentation but will be recognised
        //     case insensitively in the ASCOM.DeviceInterface.IVideo.Action(System.String,System.String)
        //     method.
        //     An array list collection has been selected as the vehicle for action names
        //     in order to make it easier for clients to determine whether a particular
        //     action is supported. This is easily done through the Contains method. Since
        //     the collection is also ennumerable it is easy to use constructs such as For
        //     Each ... to operate on members without having to be concerned about hom many
        //     members are in the collection.
        //     Collections have been used in the Telescope specification for a number of
        //     years and are known to be compatible with COM. Within .NET the ArrayList
        //     is the correct implementation to use as the .NET Generic methods are not
        //     compatible with COM.
        ArrayList SupportedActions { get; }

        //
        // Summary:
        //     Returns the list of integration rates supported by the video camera.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if data unavailable.
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw exception if camera supports only one integration rate (exposure)
        //     that cannot be changed.
        //
        // Remarks:
        //     Digital and integrating analogue video cameras allow the effective exposure
        //     of a frame to be changed. If the camera supports setting the exposure directly
        //     i.e. 2.153 sec then the driver must only return a range of useful supported
        //     exposures. For many video cameras the supported exposures (integration rates)
        //     increase by a factor of 2 from a base exposure e.g. 1, 2, 4, 8, 16 sec or
        //     0.04, 0.08, 0.16, 0.32, 0.64, 1.28, 2.56, 5.12, 10.24 sec.  If the camers
        //     supports only one exposure that cannot be changed (such as all non integrating
        //     PAL or NTSC video cameras) then this property must throw ASCOM.PropertyNotImplementedException.
        ArrayList SupportedIntegrationRates { get; }

        //
        // Summary:
        //     The name of the video capture device when such a device is used. For analogue
        //     video this is usually the video capture card or dongle attached to the computer.
        string VideoCaptureDeviceName { get; }

        //
        // Summary:
        //     Returns the video codec used to record the video file, e.g. XVID, DVSD, YUY2,
        //     HFYU etc. For AVI files this is usually the FourCC identifier of the codec.
        //     If no codec is used an empty string must be returned.
        string VideoCodec { get; }

        //
        // Summary:
        //     Returns the file format of the recorded video file, e.g. AVI, MPEG, ADV etc.
        string VideoFileFormat { get; }

        //
        // Summary:
        //     The size of the video frame buffer.
        //
        // Remarks:
        //     Must be implemented When retrieving video frames using the ASCOM.DeviceInterface.IVideo.LastVideoFrame
        //     and ASCOM.DeviceInterface.IVideo.LastVideoFrameImageArrayVariant properties
        //     the driver may use a buffer to queue the frames waiting to be read by the
        //     client. This property returns the size of the buffer in frames or if no buffering
        //     is supported then the value of less than 2 should be returned. The size of
        //     the buffer can be controlled by the end user from the driver setup dialog.
        int VideoFramesBufferSize { get; }

        //
        // Summary:
        //     Returns the width of the video frame in pixels.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if the value is not known
        //
        // Remarks:
        //     For analogue video cameras working via a frame grabber the dimentions of
        //     the video frames may be different than the dimention of the CCD chip
        int Width { get; }

        // Summary:
        //     Invokes the specified device-specific action.
        //
        // Parameters:
        //   ActionName:
        //     A well known name agreed by interested parties that represents the action
        //     to be carried out.
        //
        //   ActionParameters:
        //     List of required parameters or an System.String if none are required.
        //
        // Returns:
        //     A string response. The meaning of returned strings is set by the driver author.
        //
        // Exceptions:
        //   ASCOM.MethodNotImplementedException:
        //     Throws this exception if no actions are suported.
        //
        //   ASCOM.ActionNotImplementedException:
        //     It is intended that the SupportedActions method will inform clients of driver
        //     capabilities, but the driver must still throw an ASCOM.ActionNotImplemented
        //     exception if it is asked to perform an action that it does not support.
        //
        //   ASCOM.NotConnectedException:
        //     If the driver is not connected.
        //
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Can throw a not implemented exception This method is intended for use in
        //     all current and future device types and to avoid name clashes, management
        //     of action names is important from day 1. A two-part naming convention will
        //     be adopted - DeviceType:UniqueActionName where: DeviceType is the same value
        //     as would be used by ASCOM.Utilities.Chooser.DeviceType e.g. Telescope, Camera,
        //     Switch etc.  UniqueActionName is a single word, or multiple words joined
        //     by underscore characters, that sensibly describes the action to be performed.
        //     It is recommended that UniqueActionNames should be a maximum of 16 characters
        //     for legibility.  Should the same function and UniqueActionName be supported
        //     by more than one type of device, the reserved DeviceType of “General” will
        //     be used. Action names will be case insensitive, so FilterWheel:SelectWheel,
        //     filterwheel:selectwheel and FILTERWHEEL:SELECTWHEEL will all refer to the
        //     same action.
        //     The names of all supported actions must bre returned in the ASCOM.DeviceInterface.IVideo.SupportedActions
        //     property.
        string Action(string ActionName, string ActionParameters);

        //
        // Summary:
        //     Displays an image configuration dialog that allows configuration of specialized
        //     image settings such as White or Colour Balance for example.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw an exception if the camera is not connected.
        //
        //   ASCOM.PropertyNotImplementedException:
        //     Must throw an exception if ConfigureImage is not supported.
        //
        // Remarks:
        //     This dialog is not intended to be used in unattended mode but can give great
        //     control over the image quality for some drivers and devices. The dialog may
        //     also allow chaning settings such as Gamma and Gain that can be also controlled
        //     directly via the ASCOM.DeviceInterface.IVideo interface. If a client software
        //     displays the current Gamma and Gain it should update the values after this
        //     method has been called as those values for Gamma and Gain may have changed.
        //     To support automated and unattended control over the specializded image settings
        //     available on this dialog the driver must also alow their control via ASCOM.DeviceInterface.IVideo.SupportedActions
        void ConfigureImage();

        //
        // Summary:
        //     Dispose the late-bound interface, if needed. Will release it via COM if it
        //     is a COM object, else if native .NET will just dereference it for GC.
        void Dispose();

        //
        // Summary:
        //     Launches a configuration dialog box for the driver. The call will not return
        //     until the user clicks OK or cancel manually.
        //
        // Exceptions:
        //   ASCOM.DriverException:
        //     Must throw an exception if the call was not successful
        //
        // Remarks:
        //     Must be implemented
        void SetupDialog();

        //
        // Summary:
        //     Starts recording a new video file.
        //
        // Parameters:
        //   PreferredFileName:
        //     The file name requested by the client. Some systems may not allow the file
        //     name to be controlled directly and they should ignore this parameter.
        //
        // Returns:
        //     The actual file name that is being recorded.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if not connected.
        //
        //   ASCOM.InvalidOperationException:
        //     Must throw exception if the current camera state doesn't allow to begin recording
        //     a file.
        //
        //   ASCOM.DriverException:
        //     Must throw exception if there is any other problem as result of which the
        //     recording cannot begin.
        string StartRecordingVideoFile(string PreferredFileName);

        //
        // Summary:
        //     Stops the recording of a video file.
        //
        // Exceptions:
        //   ASCOM.NotConnectedException:
        //     Must throw exception if not connected.
        //
        //   ASCOM.InvalidOperationException:
        //     Must throw exception if the current camera state doesn't allow to stop recording
        //     the file or no file is currently being recorded.
        //
        //   ASCOM.DriverException:
        //     Must throw exception if there is any other problem as result of which the
        //     recording cannot stop.
        void StopRecordingVideoFile();
    }
}
