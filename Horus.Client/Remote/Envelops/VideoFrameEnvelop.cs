using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using Horus.Model.Interfaces;

namespace Horus.Client.Remote.Envelops
{
    public class VideoFrameEnvelop : IVideoFrame
    {
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        static extern void MoveMemory(IntPtr dest, IntPtr src, int size);

        public VideoFrameEnvelop()
        { }

        public VideoFrameEnvelop(IVideoFrame videoFrame)
        {
            try
            {
                ExposureDuration = videoFrame.ExposureDuration;
            }
            catch(NotSupportedException)
            { }

            try
            {
                ExposureStartTime = videoFrame.ExposureStartTime;
            }
            catch (NotSupportedException)
            { }

            try
            {
                FrameNumber = videoFrame.FrameNumber;
            }
            catch (NotSupportedException)
            { }

            try
            {
                // TODO: This is an extreamly slow and naive implementation
                if (videoFrame.ImageArray is int[,])
                {
                    ImageArrayDimentions = 2;

                    int[,] pixels = (int[,])videoFrame.ImageArray;

                    ImageArrayLength2 = pixels.GetLength(0);
                    ImageArrayLength1 = pixels.GetLength(1);


                    ImageArrayPacked = new byte[ImageArrayLength1 * ImageArrayLength2 * sizeof(int)];

                    int idx = 0;
                    for (int y = 0; y < ImageArrayLength2; y++)
                    {
                        for (int x = 0; x < ImageArrayLength1; x++)
                        {
                            int intVal = pixels[y, x];
                            ImageArrayPacked[idx] = (byte)(intVal & 0xFF);
                            ImageArrayPacked[idx + 1] = (byte)((intVal >> 8) & 0xFF);
                            ImageArrayPacked[idx + 2] = (byte)((intVal >> 16) & 0xFF);
                            ImageArrayPacked[idx + 3] = (byte)((intVal >> 24) & 0xFF);
                            idx += 4;
                        }
                    }
                }

                ImageArray = videoFrame.ImageArray;
                
            }
            catch (NotSupportedException)
            { }

            try
            {
                ImageInfo = videoFrame.ImageInfo;
            }
            catch (NotSupportedException)
            { }
        }

        public double ExposureDuration { get; set; }

        public string ExposureStartTime { get; set; }

        public long FrameNumber { get; set; }

        private object imageArray = null;

        [XmlIgnore]
        public object ImageArray
        {
            get
            {
                if (imageArray == null && ImageArrayPacked != null)
                {
                    UnpackImageArray();
                }

                return imageArray;
            }
            set { imageArray = value; }
        }

        public int ImageArrayDimentions { get; set; }
        public int ImageArrayLength1 { get; set; }
        public int ImageArrayLength2 { get; set; }
        public int ImageArrayLength3 { get; set; }

        public byte[] ImageArrayPacked { get; set; }

        public string ImageInfo { get; set; }

        private void UnpackImageArray()
        {
            if (ImageArrayDimentions == 2)
            {
                int[,] pixels = new int[ImageArrayLength2, ImageArrayLength1];

                int idx = 0;
                for (int y = 0; y < ImageArrayLength2; y++)
                {
                    for (int x = 0; x < ImageArrayLength1; x++)
                    {


                        int intVal =
                            (ImageArrayPacked[idx + 3] << 24) +
                            (ImageArrayPacked[idx + 2] << 16) +
                            (ImageArrayPacked[idx + 1] << 8) +
                            ImageArrayPacked[idx];

                        pixels[y, x] = intVal;

                        idx += 4;
                    }
                }

                imageArray = pixels;
            }
        }
    }
}
