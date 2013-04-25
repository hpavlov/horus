using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HorusClientApp.Koyash
{
    public enum LumaConversionMode
    {
        R = 0,
        G = 1,
        B = 2,
        GrayScale = 3
    }

    internal static class NativeHelpers
    {
        private const string KOYASH_VIDEO_UTILS_DLL_NAME = "Koyash.VideoUtilities.Native.dll";

        [DllImport(KOYASH_VIDEO_UTILS_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetBitmapPixels(
            int width,
            int height,
            int bpp,
            [In, MarshalAs(UnmanagedType.LPArray)] int[,] pixels,
            [In, Out] byte[] bitmapBytes);

        [DllImport(KOYASH_VIDEO_UTILS_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetColourBitmapPixels(
            int width,
            int height,
            int bpp,
            [In, MarshalAs(UnmanagedType.LPArray)] int[, ,] pixels,
            [In, Out] byte[] bitmapBytes);

        [DllImport(KOYASH_VIDEO_UTILS_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetMonochromePixelsFromBitmap(
            int width,
            int height,
            int bpp,
            [In] IntPtr hBitmap,
            [In, Out, MarshalAs(UnmanagedType.LPArray)] int[,] bitmapBytes,
            int mode);

        [DllImport(KOYASH_VIDEO_UTILS_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetColourPixelsFromBitmap(
            int width,
            int height,
            int bpp,
            [In] IntPtr hBitmap,
            [In, Out, MarshalAs(UnmanagedType.LPArray)] int[, ,] bitmapBytes);


        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);


        public static Bitmap PrepareBitmapForDisplay(int[,] imageArray, int width, int height)
        {
            return PrepareBitmapForDisplay(imageArray, width, height, false);
        }

        public static Bitmap PrepareBitmapForDisplay(object[,] imageArray, int width, int height)
        {
            return PrepareBitmapForDisplay(imageArray, width, height, true);
        }

        public static Bitmap PrepareColourBitmapForDisplay(int[, ,] imageArray, int width, int height)
        {
            return PrepareColourBitmapForDisplay(imageArray, width, height, false);
        }

        public static Bitmap PrepareColourBitmapForDisplay(object[, ,] imageArray, int width, int height)
        {
            return PrepareColourBitmapForDisplay(imageArray, width, height, true);
        }

        public static object GetMonochromePixelsFromBitmap(Bitmap bitmap, LumaConversionMode conversionMode)
        {
            int[,] bitmapBytes = new int[bitmap.Width, bitmap.Height];

            IntPtr hBitmap = bitmap.GetHbitmap();
            try
            {
                GetMonochromePixelsFromBitmap(bitmap.Width, bitmap.Height, 8, hBitmap, bitmapBytes, (int)conversionMode);
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return bitmapBytes;
        }

        public static object GetColourPixelsFromBitmap(Bitmap bitmap)
        {
            int[, ,] bitmapBytes = new int[bitmap.Width, bitmap.Height, 3];

            IntPtr hBitmap = bitmap.GetHbitmap();
            try
            {
                GetColourPixelsFromBitmap(bitmap.Width, bitmap.Height, 8, hBitmap, bitmapBytes);
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return bitmapBytes;
        }

        private static Bitmap PrepareBitmapForDisplay(object imageArray, int width, int height, bool useVariantPixels)
        {
            Bitmap displayBitmap = null;

            int[,] pixels = new int[height, width];

            if (useVariantPixels)
            {
                Array safeArr = (Array)imageArray;
                Array.Copy(safeArr, pixels, pixels.Length);
            }
            else
            {
                Array safeArr = (Array)imageArray;
                Array.Copy(safeArr, pixels, pixels.Length);
            }

            byte[] rawBitmapBytes = new byte[(width * height * 3) + 40 + 14 + 1];

            GetBitmapPixels(width, height, (int)8, pixels, rawBitmapBytes);

            using (MemoryStream memStr = new MemoryStream(rawBitmapBytes))
            {
                displayBitmap = (Bitmap)Image.FromStream(memStr);
            }

            return displayBitmap;
        }

        private static Bitmap PrepareColourBitmapForDisplay(object imageArray, int width, int height, bool useVariantPixels)
        {
            Bitmap displayBitmap = null;

            int[, ,] pixels = new int[height, width, 3];

            if (useVariantPixels)
            {
                Array safeArr = (Array)imageArray;
                Array.Copy(safeArr, pixels, pixels.Length);
            }
            else
            {
                Array safeArr = (Array)imageArray;
                Array.Copy(safeArr, pixels, pixels.Length);
            }

            byte[] rawBitmapBytes = new byte[(width * height * 3) + 40 + 14 + 1];

            GetColourBitmapPixels(width, height, (int)8, pixels, rawBitmapBytes);

            using (MemoryStream memStr = new MemoryStream(rawBitmapBytes))
            {
                displayBitmap = (Bitmap)Image.FromStream(memStr);
            }

            return displayBitmap;
        }
    }
}
