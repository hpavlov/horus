using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horus.Model.Exceptions;
using Horus.Model.Interfaces;

namespace Horus.Client.Drivers
    {
    public class HorusDome : HorusDriver
        {
        IDome dome;

        internal HorusDome(object deviceInterface) : base(deviceInterface as IHorusDriver) { dome = deviceInterface as IDome; }

        public void Disconnect()
            {
            if (dome != null)
                {
                dome.Connected = false;
                dome = null;
                }
            }

        public bool IsConnected { get { return ShieldedCall(() => dome != null && dome.Connected, false); } }

        public bool Connected { get { return dome.Connected; } set { dome.Connected = value; } }

        public bool HasSupportedActions { get { return ShieldedCall(() => dome != null && dome.SupportedActions.Count > 0, false); } }

        public IList<string> SupportedActions
            {
            get
                {
                try
                    {
                    return dome.SupportedActions;
                    }
                catch (Exception ex)
                    {
                    return new List<string>();
                    }
                }
            }

        public void AbortSlew() { dome.AbortSlew(); }
        public double Altitude { get { return dome.Altitude; } }
        public bool AtHome { get { return dome.AtHome; } }
        public bool AtPark { get { return dome.AtPark; } }

        public string ExecuteAction(string actionName, string actionParameters) { return dome.Action(actionName, actionParameters); }

        public string DriverInfo
            {
            get
                {
                if (dome != null)
                    return ShieldedCall(() => dome.DriverInfo, string.Empty, false);
                return string.Empty;
                }
            }

        public string DriverVersion
            {
            get
                {
                if (dome != null)
                    return ShieldedCall(() => dome.DriverVersion, string.Empty, false);

                return string.Empty;
                }
            }

        public string DeviceName
            {
            get
                {
                if (dome != null)
                    return ShieldedCall(() => dome.Name, string.Empty);

                return string.Empty;
                }
            }

        public string DeviceDescription
            {
            get
                {
                if (dome != null)
                    return ShieldedCall(() => dome.Description, string.Empty, false);

                return string.Empty;
                }
            }

        public double Azimuth { get { return dome.Azimuth; } }
        public bool CanFindHome { get { return dome.CanFindHome; } }
        public bool CanPark { get { return dome.CanPark; } }
        public bool CanSetAltitude { get { return dome.CanSetAltitude; } }
        public bool CanSetAzimuth { get { return dome.CanSetAzimuth; } }
        public bool CanSetPark { get { return dome.CanSetPark; } }
        public bool CanSetShutter { get { return dome.CanSetShutter; } }
        public bool CanSlave { get { return dome.CanSlave; } }
        public bool CanSyncAzimuth { get { return dome.CanSyncAzimuth; } }
        public void CloseShutter() { dome.CloseShutter(); }
        public void FindHome() { dome.FindHome(); }
        public void OpenShutter() { dome.OpenShutter(); }
        public void Park() { dome.Park(); }
        public void SetPark() { dome.SetPark(); }
        public DomeShutterState ShutterStatus { get { return ShieldedCall(() => dome.ShutterStatus, DomeShutterState.shutterError); } }
        public bool Slaved { get { return dome.Slaved; } }
        public bool Slewing { get { return dome.Slewing; } }
        public void SlewToAltitude(double Altitude) { dome.SlewToAltitude(Altitude); }
        public void SlewToAzimuth(double Azimuth) { dome.SlewToAzimuth(Azimuth); }
        public void SyncToAzimuth(double Azimuth) { dome.SyncToAzimuth(Azimuth); }

        T ShieldedCall<T>(Func<T> method, T errorValue) { return ShieldedCall(method, errorValue, true); }

        T ShieldedCall<T>(Func<T> method, T errorValue, bool showError)
            {
            try
                {
                return method();
                }
            catch (HorusDriverException dex)
                {
                //if (showError)
                //    frmUnexpectedError.ShowErrorMessage(dex);

                return errorValue;
                }
            }
        }
    }
