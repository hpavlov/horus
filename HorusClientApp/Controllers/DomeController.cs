using System.Drawing;
using System.Windows.Forms;
using Horus.Client.Drivers;

namespace HorusClientApp.Controllers
    {
    public class DomeController
        {
        frmMain mainForm;
        bool running = false;
        HorusDome domeObject;

        public DomeController(frmMain mainForm) { this.mainForm = mainForm; }

        void ConnectToDevice()
            {
            try
                {
                mainForm.Cursor = Cursors.WaitCursor;
                domeObject.Connected = true;

                if (domeObject.IsConnected) {}
                }
            finally
                {
                mainForm.Cursor = Cursors.Default;
                }
            }

        public void DisconnectFromDevice()
            {
            if (domeObject != null)
                {
                domeObject.Connected = false;
                domeObject = null;
                }
            }

        static Font debugTextFont = new Font(FontFamily.GenericMonospace, 10);

        }
    }
