﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Horus.Model.Exceptions;

//-----------------------------------------------------------------------
// <summary>Defines the IDome Interface</summary>
//-----------------------------------------------------------------------
namespace Horus.Model.Interfaces
    {
    /// <summary>
    /// Defines the IDome Interface
    /// </summary>
    public interface IDome : IHorusDriver
        {
        //CCDA0D85-474A-4775-8105-1D513ADC3896

        #region "Common Methods"
        //IAscomDriver Methods

        /// <summary>
        /// Set True to connect to the device. Set False to disconnect from the device.
        /// You can also read the property to check whether it is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Must be implemented</b></p>Do not use a NotConnectedException here, that exception is for use in other methods that require a connection in order to succeed.</remarks>

        bool Connected { get; set; }
        /// <summary>
        /// Returns a description of the device, such as manufacturer and modelnumber. Any ASCII characters may be used. 
        /// </summary>
        /// <value>The description.</value>
        /// <exception cref="HorusDriverNotConnectedException">If the device is not connected and this information is only available when connected.</exception>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Must be implemented</b></p> </remarks>

        string Description { get; }
        /// <summary>
        /// Descriptive and version information about this ASCOM driver.
        /// </summary>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks>
        /// <p style="color:red"><b>Must be implemented</b></p> This string may contain line endings and may be hundreds to thousands of characters long.
        /// It is intended to display detailed information on the ASCOM driver, including version and copyright data.
        /// See the <see cref="Description" /> property for information on the device itself.
        /// To get the driver version in a parseable string, use the <see cref="DriverVersion" /> property.
        /// </remarks>

        string DriverInfo { get; }
        /// <summary>
        /// A string containing only the major and minor version of the driver.
        /// </summary>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Must be implemented</b></p> This must be in the form "n.n".
        /// It should not to be confused with the <see cref="InterfaceVersion" /> property, which is the version of this specification supported by the 
        /// driver.
        /// </remarks>

        string DriverVersion { get; }
        /// <summary>
        /// The interface version number that this device supports. Should return 2 for this interface version.
        /// </summary>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Must be implemented</b></p> Clients can detect legacy V1 drivers by trying to read ths property.
        /// If the driver raises an error, it is a V1 driver. V1 did not specify this property. A driver may also return a value of 1. 
        /// In other words, a raised error or a return value of 1 indicates that the driver is a V1 driver.
        /// </remarks>

        short InterfaceVersion { get; }
        /// <summary>
        /// The short name of the driver, for display purposes
        /// </summary>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Must be implemented</b></p> </remarks>

        string Name { get; }
        /// <summary>
        /// Launches a configuration dialog box for the driver.  The call will not return
        /// until the user clicks OK or cancel manually.
        /// </summary>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Must be implemented</b></p> </remarks>

        void SetupDialog();
        //DeviceControl Methods

        /// <summary>
        /// Invokes the specified device-specific action.
        /// </summary>
        /// <param name="ActionName">
        /// A well known name agreed by interested parties that represents the action to be carried out. 
        /// </param>
        /// <param name="ActionParameters">List of required parameters or an <see cref="String.Empty">Empty String</see> if none are required.
        /// </param>
        /// <returns>A string response. The meaning of returned strings is set by the driver author.</returns>
        /// <exception cref="ASCOM.MethodNotImplementedException">Throws this exception if no actions are suported.</exception>
        /// <exception cref="ASCOM.ActionNotImplementedException">It is intended that the SupportedActions method will inform clients 
        /// of driver capabilities, but the driver must still throw an ASCOM.ActionNotImplemented exception if it is asked to 
        /// perform an action that it does not support.</exception>
        /// <exception cref="HorusDriverNotConnectedException">If the driver is not connected.</exception>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <example>Suppose filter wheels start to appear with automatic wheel changers; new actions could 
        /// be “FilterWheel:QueryWheels” and “FilterWheel:SelectWheel”. The former returning a 
        /// formatted list of wheel names and the second taking a wheel name and making the change, returning appropriate 
        /// values to indicate success or failure.
        /// </example>
        /// <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> 
        /// This method is intended for use in all current and future device types and to avoid name clashes, management of action names 
        /// is important from day 1. A two-part naming convention will be adopted - <b>DeviceType:UniqueActionName</b> where:
        /// <list type="bullet">
        /// <item><description>DeviceType is the same value as would be used by <see cref="ASCOM.Utilities.Chooser.DeviceType"/> e.g. Telescope, Camera, Switch etc.</description></item>
        /// <item><description>UniqueActionName is a single word, or multiple words joined by underscore characters, that sensibly describes the action to be performed.</description></item>
        /// </list>
        /// <para>
        /// It is recommended that UniqueActionNames should be a maximum of 16 characters for legibility.
        /// Should the same function and UniqueActionName be supported by more than one type of device, the reserved DeviceType of 
        /// “General” will be used. Action names will be case insensitive, so FilterWheel:SelectWheel, filterwheel:selectwheel 
        /// and FILTERWHEEL:SELECTWHEEL will all refer to the same action.</para>
        /// <para>The names of all supported actions must bre returned in the <see cref="SupportedActions"/> property.</para>
        /// </remarks>
        string Action(string ActionName, string ActionParameters);

        /// <summary>
        /// Returns the list of action names supported by this driver.
        /// </summary>
        /// <value>An ArrayList of strings (SafeArray collection) containing the names of supported actions.</value>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Must be implemented</b></p> This method must return an empty arraylist if no actions are supported. Please do not throw a 
        /// <see cref="ASCOM.PropertyNotImplementedException" />.
        /// <para>This is an aid to client authors and testers who would otherwise have to repeatedly poll the driver to determine its capabilities. 
        /// Returned action names may be in mixed case to enhance presentation but  will be recognised case insensitively in 
        /// the <see cref="Action">Action</see> method.</para>
        ///<para>An array list collection has been selected as the vehicle for  action names in order to make it easier for clients to
        /// determine whether a particular action is supported. This is easily done through the Contains method. Since the
        /// collection is also ennumerable it is easy to use constructs such as For Each ... to operate on members without having to be concerned 
        /// about hom many members are in the collection. </para>
        /// <para>Collections have been used in the Telescope specification for a number of years and are known to be compatible with COM. Within .NET
        /// the ArrayList is the correct implementation to use as the .NET Generic methods are not compatible with COM.</para>
        /// </remarks>

        IList<string> SupportedActions { get; }
        /// <summary>
        /// Transmits an arbitrary string to the device and does not wait for a response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
        /// <exception cref="HorusDriverNotConnectedException">If the driver is not connected.</exception>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> </remarks>

        void CommandBlind(string Command, bool Raw = false);
        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a boolean response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the interpreted boolean response received from the device.
        /// </returns>
        /// <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
        /// <exception cref="HorusDriverNotConnectedException">If the driver is not connected.</exception>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> </remarks>
        bool CommandBool(string Command, bool Raw = false);

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a string response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the string response received from the device.
        /// </returns>
        /// <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
        /// <exception cref="HorusDriverNotConnectedException">If the driver is not connected.</exception>
        /// <exception cref="HorusDriverException">Must throw an exception if the call was not successful</exception>
        /// <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> </remarks>
        string CommandString(string Command, bool Raw = false);

        /// <summary>
        /// Dispose the late-bound interface, if needed. Will release it via COM
        /// if it is a COM object, else if native .NET will just dereference it
        /// for GC.
        /// </summary>

        void Dispose();
        #endregion

        #region "Device Methods"
        /// <summary>
        /// Immediately cancel current dome operation.
        /// </summary>
        /// <remarks>
        /// Calling this method will immediately disable hardware slewing (<see cref="Slaved" /> will become False).
        /// Raises an error if a communications failure occurs, or if the command is known to have failed. 
        /// </remarks>

        void AbortSlew();
        /// <summary>
        /// The dome altitude (degrees, horizon zero and increasing positive to 90 zenith).
        /// </summary>
        /// <remarks>
        /// Raises an error only if no altitude control. If actual dome altitude can not be read,
        /// then reports back the last slew position. 
        /// </remarks>

        double Altitude { get; }
        /// <summary>
        ///   Indicates whether the dome is in the home position.
        ///   Raises an error if not supported. 
        /// <para>
        ///   This is normally used following a <see cref="FindHome" /> operation. The value is reset with any azimuth
        ///   slew operation that moves the dome away from the home position.
        /// </para>
        /// <para>
        ///   <see cref="AtHome" /> may also become true durng normal slew operations, if the dome passes through the home position
        ///   and the dome controller hardware is capable of detecting that; or at the end of a slew operation if the dome
        ///   comes to rest at the home position.
        /// </para>
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The home position is normally defined by a hardware sensor positioned around the dome circumference
        ///     and represents a fixed, known azimuth reference.
        ///   </para>
        ///   <para>
        ///     For some devices, the home position may represent a small range of azimuth values, rather than a discrete
        ///     value, since dome inertia, the resolution of the home position sensor and/or the azimuth encoder may be
        ///     insufficient to return the exact same azimuth value on each occasion. Some dome controllers, on the other
        ///     hand, will always force the azimuth reading to a fixed value whenever the home position sensor is active.
        ///     Because of these potential differences in behaviour, applications should not rely on the reported azimuth
        ///     position being identical each time <see cref="AtHome" /> is set <c>true</c>.
        ///   </para>
        /// </remarks>
        /// [ASCOM-135] TPL - Updated documentation

        bool AtHome { get; }
        /// <summary>
        /// True if the dome is in the programmed park position.
        /// </summary>
        /// <remarks>
        /// Set only following a <see cref="Park" /> operation and reset with any slew operation.
        /// Raises an error if not supported. 
        /// </remarks>

        bool AtPark { get; }
        /// <summary>
        /// The dome azimuth (degrees, North zero and increasing clockwise, i.e., 90 East, 180 South, 270 West)
        /// </summary>
        /// <remarks>Raises an error only if no azimuth control. If actual dome azimuth can not be read, then reports back last slew position</remarks>

        double Azimuth { get; }
        /// <summary>
        /// True if driver can do a search for home position.
        /// </summary>

        bool CanFindHome { get; }
        /// <summary>
        /// True if driver is capable of setting dome altitude.
        /// </summary>

        bool CanPark { get; }
        /// <summary>
        /// True if driver is capable of setting dome altitude.
        /// </summary>

        bool CanSetAltitude { get; }
        /// <summary>
        /// True if driver is capable of setting dome azimuth.
        /// </summary>

        bool CanSetAzimuth { get; }
        /// <summary>
        /// True if driver can set the dome park position.
        /// </summary>

        bool CanSetPark { get; }
        /// <summary>
        /// True if driver is capable of automatically operating shutter.
        /// </summary>

        bool CanSetShutter { get; }
        /// <summary>
        /// True if the dome hardware supports slaving to a telescope.
        /// </summary>
        /// <remarks>See the notes for the <see cref="Slaved" /> property.</remarks>

        bool CanSlave { get; }
        /// <summary>
        /// True if driver is capable of synchronizing the dome azimuth position using the <see cref="SyncToAzimuth" /> method.
        /// </summary>

        bool CanSyncAzimuth { get; }
        /// <summary>
        /// Close shutter or otherwise shield telescope from the sky.
        /// </summary>

        void CloseShutter();
        /// <summary>
        /// Start operation to search for the dome home position.
        /// </summary>
        /// <remarks>
        /// After Home position is established initializes <see cref="Azimuth" /> to the default value and sets the <see cref="AtHome" /> flag. 
        /// Exception if not supported or communications failure. Raises an error if <see cref="Slaved" /> is True.
        /// </remarks>

        void FindHome();
        /// <summary>
        /// Open shutter or otherwise expose telescope to the sky.
        /// </summary>
        /// <remarks>
        /// Raises an error if not supported or if a communications failure occurs. 
        /// </remarks>

        void OpenShutter();
        /// <summary>
        /// Rotate dome in azimuth to park position.
        /// </summary>
        /// <remarks>
        /// After assuming programmed park position, sets <see cref="AtPark" /> flag. Raises an error if <see cref="Slaved" /> is True,
        /// or if not supported, or if a communications failure has occurred. 
        /// </remarks>

        void Park();
        /// <summary>
        /// Set the current azimuth, altitude position of dome to be the park position.
        /// </summary>
        /// <remarks>
        /// Raises an error if not supported or if a communications failure occurs. 
        /// </remarks>

        void SetPark();
        /// <summary>
        /// Status of the dome shutter or roll-off roof.
        /// </summary>
        /// <remarks>
        /// Raises an error only if no shutter control.
        /// If actual shutter status can not be read, 
        /// then reports back the last shutter state. 
        /// </remarks>

        DomeShutterState ShutterStatus { get; }
        /// <summary>
        /// True if the dome is slaved to the telescope in its hardware, else False.
        /// </summary>
        /// <remarks>
        /// Set this property to True to enable dome-telescope hardware slaving,
        /// if supported (see <see cref="CanSlave" />). Raises an exception on any attempt to set 
        /// this property if hardware slaving is not supported).
        /// Always returns False if hardware slaving is not supported. 
        /// </remarks>

        bool Slaved { get; set; }
        /// <summary>
        /// True if any part of the dome is currently moving, False if all dome components are steady.
        /// </summary>
        /// <remarks>
        /// Raises an error if <see cref="Slaved" /> is True, if not supported, if a communications failure occurs,
        /// or if the dome can not reach indicated azimuth. 
        /// </remarks>

        bool Slewing { get; }
        /// <summary>
        /// Slew the dome to the given altitude position.
        /// </summary>
        /// <remarks>
        /// Raises an error if <see cref="Slaved" /> is True, if not supported, if a communications failure occurs,
        /// or if the dome can not reach indicated altitude. 
        /// </remarks>
        /// <param name="Altitude">Target dome altitude (degrees, horizon zero and increasing positive to 90 zenith)</param>

        void SlewToAltitude(double Altitude);
        /// <summary>
        /// Slew the dome to the given azimuth position.
        /// </summary>
        /// <remarks>
        /// Raises an error if <see cref="Slaved" /> is True, if not supported, if a communications failure occurs,
        /// or if the dome can not reach indicated azimuth. 
        /// </remarks>
        /// <param name="Azimuth">Target azimuth (degrees, North zero and increasing clockwise. i.e., 90 East, 180 South, 270 West)</param>

        void SlewToAzimuth(double Azimuth);
        /// <summary>
        /// Synchronize the current position of the dome to the given azimuth.
        /// </summary>
        /// <remarks>
        /// Raises an error if not supported or if a communications failure occurs. 
        /// </remarks>
        /// <param name="Azimuth">Target azimuth (degrees, North zero and increasing clockwise. i.e., 90 East, 180 South, 270 West)</param>
        #endregion
        void SyncToAzimuth(double Azimuth);

        }
    }

