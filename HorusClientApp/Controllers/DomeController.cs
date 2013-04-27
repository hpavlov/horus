using System;
using System.Drawing;
using System.Windows.Forms;
using Horus.Client.Drivers;

namespace HorusClientApp.Controllers
    {
    public class DomeController
        {
        frmMain mainForm;
        bool running = false;
        HorusDome dome;

        public DomeController(frmMain mainForm) { this.mainForm = mainForm; }

        void ConnectToDevice()
            {
            try
                {
                mainForm.Cursor = Cursors.WaitCursor;
                dome.Connected = true;

                if (dome.IsConnected) {}
                }
            finally
                {
                mainForm.Cursor = Cursors.Default;
                }
            }

        public void DisconnectFromDevice()
            {
            if (dome != null)
                {
                dome.Connected = false;
                dome = null;
                }
            mainForm.SetDomeUiStateChoosing();
            }

        static Font debugTextFont = new Font(FontFamily.GenericMonospace, 10);

        public void ConnectToDevice(HorusDome domeInstance)
            {
            dome = domeInstance;
            try
                {
                dome.Connected = true;
                if (dome.Connected)
                    {
                    mainForm.SetDomeUiStateConnected();
                    }
                else
                    {
                    mainForm.SetDomeUiStateChooseOrConnect();
                    }
                }
            catch (Exception ex)
                {
                mainForm.SetDomeUiStateChoosing();
                }
            }
        }
    }