using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo.Helper
{
    class ScreenCaptureHelper
    {
        /// <summary>
        /// bitmap转换成Byte[]
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        private static byte[] Bitmap2Bytes(Bitmap bm)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
           // DemoTrace.WriteLine("时间戳 start：" + ts.ToString());
            int data_size = bm.Width * bm.Height * 3;
            Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);
            System.Drawing.Imaging.BitmapData bmpData =
         bm.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
         bm.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
    
            int bytes = Math.Abs(bmpData.Stride) * bm.Height;
            byte[] src_value = new byte[bytes];
            byte[] rgbValues = new byte[data_size];
            System.Runtime.InteropServices.Marshal.Copy(ptr, src_value, 0, bytes);
            int width_stride = bm.Width * 3;
            for (int i = 0; i<bm.Height; i++)
            {
                Array.Copy(src_value, i * bmpData.Stride, rgbValues, (bm.Height - i - 1) * width_stride, width_stride);
            }

            bm.UnlockBits(bmpData);
            TimeSpan ts_end = new TimeSpan(DateTime.Now.Ticks);
            //DemoTrace.WriteLine("时间戳 end：" + ts_end.ToString());
            return rgbValues;
        }
        public static byte[] GetScreenCapture(out int width,out int height)
        {
            width = Screen.PrimaryScreen.Bounds.Width;
            height = Screen.PrimaryScreen.Bounds.Height;
            Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics imgGraphics = Graphics.FromImage(image);
            //设置截屏区域
            imgGraphics.CopyFromScreen(0, 0, 0, 0, new Size(Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width), Screen.PrimaryScreen.Bounds.Height));
            byte[] dataBB = Bitmap2Bytes(image);
            imgGraphics.Dispose();
            return dataBB;
        }
    }
}
