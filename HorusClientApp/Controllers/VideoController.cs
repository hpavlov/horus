using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Horus.Client.Drivers;
using Horus.Model.Interfaces;
using HorusClientApp.Koyash;

namespace HorusClientApp.Controllers
{
    public class VideoController
    {
        private frmMain mainForm;
        private bool running = false;
        private HorusVideo videoObject;
        private long lastDisplayedVideoFrameNumber = -1;

        private int imageWidth;
        private int imageHeight;

        private CameraImage cameraImageHelper;

        public VideoController(frmMain mainForm)
        {
            this.mainForm = mainForm;

            cameraImageHelper = new CameraImage();

            running = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(DisplayVideoFrames));
        }

        public void PlayVideo(HorusVideo video)
        {
            videoObject = video;

            ConnectToCamera();
        }

        private void ConnectToCamera()
        {
            try
            {
                mainForm.Cursor = Cursors.WaitCursor;
                videoObject.Connected = true;

                if (videoObject.IsConnected)
                {
                    imageWidth = videoObject.Width;
                    imageHeight = videoObject.Height;
                    mainForm.picboxVideo.Image = new Bitmap(imageWidth, imageHeight);

                    ResizeVideoFrameTo(imageWidth, imageHeight);
                }
            }
            finally
            {
                mainForm.Cursor = Cursors.Default;
            }


            mainForm.picboxVideo.Width = videoObject.Width;
            mainForm.picboxVideo.Height = videoObject.Height;
        }

        public void DisconnectFromCamera()
        {
            if (videoObject != null)
            {
                videoObject.Connected = false;
                videoObject = null;
            }
        }

        private static Font debugTextFont = new Font(FontFamily.GenericMonospace, 10);

        private delegate void PaintVideoFrameDelegate(IVideoFrame frame, Bitmap bmp);

        private int renderedFrameCounter = 0;
        private long startTicks = 0;
        private long endTicks = 0;

        private double renderFps = double.NaN;
        private long currentFrameNo = 0;

        private void PaintVideoFrame(IVideoFrame frame, Bitmap bmp)
        {
            bool isEmptyFrame = frame == null;
            if (!isEmptyFrame)
                isEmptyFrame = frame.ImageArray == null;

            if (isEmptyFrame)
            {
                using (Graphics g = Graphics.FromImage(mainForm.picboxVideo.Image))
                {
                    if (bmp == null)
                        g.Clear(Color.Green);
                    else
                        g.DrawImage(bmp, 0, 0);

                    g.Save();
                }

                mainForm.picboxVideo.Invalidate();
                return;
            }

            currentFrameNo = frame.FrameNumber;

            renderedFrameCounter++;

            if (renderedFrameCounter == 20)
            {
                renderedFrameCounter = 0;
                endTicks = DateTime.Now.Ticks;
                if (startTicks != 0)
                {
                    renderFps = 20.0 / new TimeSpan(endTicks - startTicks).TotalSeconds;
                }
                startTicks = DateTime.Now.Ticks;
            }

            using (Graphics g = Graphics.FromImage(mainForm.picboxVideo.Image))
            {
                g.DrawImage(bmp, 0, 0);

                g.Save();
            }

            mainForm.picboxVideo.Invalidate();
            bmp.Dispose();
        }

        private void DisplayVideoFrames(object state)
        {
            while (running)
            {
                if (videoObject != null &&
                    videoObject.Connected)
                {
                    try
                    {
                        IVideoFrame frame = videoObject.LastVideoFrame;

                        if (frame != null &&
                            (frame.FrameNumber == -1 || frame.FrameNumber != lastDisplayedVideoFrameNumber))
                        {
                            lastDisplayedVideoFrameNumber = frame.FrameNumber;

                            Bitmap bmp = null;

                            cameraImageHelper.SetImageArray(frame.ImageArray, imageWidth, imageHeight, videoObject.SensorType);

                            bmp = cameraImageHelper.GetDisplayBitmap();
                            

                            mainForm.Invoke(new PaintVideoFrameDelegate(PaintVideoFrame), new object[] { frame, bmp });
                        }
                    }
                    catch (ObjectDisposedException) { }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);

                        Bitmap errorBmp = new Bitmap(mainForm.picboxVideo.Width, mainForm.picboxVideo.Height);
                        using (Graphics g = Graphics.FromImage(errorBmp))
                        {
                            g.Clear(Color.Tomato);
                            g.DrawString(ex.Message, debugTextFont, Brushes.Black, 10, 10);
                            g.Save();
                        }
                        try
                        {
                            mainForm.Invoke(new PaintVideoFrameDelegate(PaintVideoFrame), new object[] { null, errorBmp });
                        }
                        catch (InvalidOperationException)
                        {
                            // InvalidOperationException could be thrown when closing down the app i.e. when the form has been already disposed
                        }
                    }

                }

                Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        private void ResizeVideoFrameTo(int imageWidth, int imageHeight)
        {
            mainForm.Width = Math.Max(800, (imageWidth - mainForm.picboxVideo.Width) + mainForm.Width);
            mainForm.Height = Math.Max(600, (imageHeight - mainForm.picboxVideo.Height) + mainForm.Height);
        }
    }
}
