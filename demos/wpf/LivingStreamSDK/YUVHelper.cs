using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIMDemo.LivingStreamSDK
{
	class YUVHelper
	{
		private static int A = 3;
		private static int R = 2;
		private static int G = 1;
		private static int B = 0;


        //旋转180
        public static void i420Revert(ref byte[] yuv_src,int width,int height)
        {
            int yuv_size=width*height*3/2; 
            byte[] new_yuv = new byte[yuv_size];
            if(yuv_src.Length!=yuv_size)
            {
                return ;
            }  

            int y_size = width * height ;
            int u_index = width * height;
            int v_index = width * height * 5 / 4;
            for (int i = 0; i < height;i++)
            {
               //y
               Array.Copy(yuv_src, i*width,new_yuv,(height-i-1)*width, width);
               
               //u v
              if(i%2==0)
              {
                  Array.Copy(yuv_src,(u_index+(i/2)*width/2), new_yuv,u_index+(height/2- i/2 - 1) * width/2, width/2);
                  Array.Copy(yuv_src, (v_index + (i / 2) * width / 2), new_yuv, v_index + (height / 2 - i / 2 - 1) * width/2, width / 2);
              }
            }



            yuv_src = new_yuv;
            return;
        }

		public static byte[] ARGBToI420(byte[] src_argb,int width, int height)
		{
            byte[] i420 = new byte[width * height * 3 / 2];
			int index_src = 0;
			int index_y = 0;
			int index_uv = 0;
            int dst_stride_y = width;
            int dst_stride_uv = width / 2;
			if (src_argb == null || width <= 0 || height <= 0)
			{
				return null;
			}

            //2行处理数据
			for (int y = 0; y < height; y += 2)
			{
				ARGBToUVRow(src_argb, i420, width,height,index_src,index_uv);
				ARGBToYRow(src_argb, i420, width, index_src, index_y);
                ARGBToYRow(src_argb, i420, width, index_src + width, index_y + dst_stride_y);
				index_src += width * 2;
				index_y += dst_stride_y * 2;
                index_uv += dst_stride_uv;
			}

            //若像素高度为奇数，处理最后剩余的一行
			if (Convert.ToBoolean(height & 1))
			{
				ARGBToUVRow(src_argb,i420, width,height,index_src,index_uv);
				ARGBToYRow(src_argb, i420, width, index_src, index_y);
			}

            return i420;
		}


        //倒序转换
        public static byte[] I420ToARGBRevert(byte[] src, int width, int height)
        {
            int numOfPixel = width * height; 
            int positionOfU = numOfPixel;
            int positionOfV = numOfPixel / 4 +numOfPixel;
        
            byte[] rgb = new byte[numOfPixel * 4];
            int index = 0;
            for (int i = height-1; i >=0; i--)
            {
                int startY = i * width;
                int step = (i / 2) * (width / 2);
                int startU = positionOfU + step;
                int startV = positionOfV + step;
                for (int j = 0; j < width; j++)
                {
                    int Y = startY + j;
                    int V = startV + j / 2;
                    int U = startU + j / 2;
                   // int index = Y * 4;
                    RGB tmp = yuvTorgb(src[Y], src[U], src[V]);
                    rgb[index + A] = 255; 
                    rgb[index + R] =tmp.r;
                    rgb[index + G] = tmp.g; 
                    rgb[index + B] = tmp.b;
                    index += 4;
                }
            }
            return rgb;
        }

        //顺序转换
        public static byte[] I420ToARGB(byte[] src, int width, int height)
        {
            int numOfPixel = width * height;
            int positionOfU = numOfPixel;
            int positionOfV = numOfPixel / 4 + numOfPixel;

            byte[] rgb = new byte[numOfPixel * 4];
            for (int i =0; i<height; i++)
            {
                int startY = i * width;
                int step = (i / 2) * (width / 2);
                int startU = positionOfU + step;
                int startV = positionOfV + step;
                for (int j = 0; j < width; j++)
                {
                    int Y = startY + j;
                    int V = startV + j / 2;
                    int U = startU + j / 2;
                   int index = Y * 4;
                    RGB tmp = yuvTorgb(src[Y], src[U], src[V]);
                    rgb[index + A] = 255;
                    rgb[index + R] = tmp.r;
                    rgb[index + G] = tmp.g;
                    rgb[index + B] = tmp.b;
                    index += 4;
                }
            }
            return rgb;
        }

		private  class RGB
		{
			public byte r;
			public byte g;
			public byte b;
		}


		private static RGB yuvTorgb(byte Y, byte U, byte V)
		{
			RGB rgb = new RGB();
			int r = (int)((Y & 0xff) + 1.4075 * ((V & 0xff) - 128));
			int g = (int)((Y & 0xff) - 0.3455 * ((U & 0xff) - 128) - 0.7169 * ((V & 0xff) - 128));
			int b = (int)((Y & 0xff) + 1.779 * ((U & 0xff) - 128));
			rgb.r = Convert.ToByte((r < 0 ? 0 : r > 255 ? 255 : r));
			rgb.g = Convert.ToByte((g < 0 ? 0 : g > 255 ? 255 : g));
			rgb.b = Convert.ToByte((b < 0 ? 0 : b > 255 ? 255 : b));
			return rgb;
		}



		//Y = (0.257 * R) + (0.504 * G) + (0.098 * B) + 16
        //Cb = U = -(0.148 * R) - (0.291 * G) + (0.439 * B) + 128
		//Cr = V = (0.439 * R) - (0.368 * G) - (0.071 * B) + 128

        private static void ARGBToUVRow(byte[] src_argb, byte[] i420, int width, int height, int index_src, int index_uv)
        {
            int y_size = width * height;
            int u_size = y_size / 4;
            int index_u = y_size + index_uv;
            int index_v = y_size + u_size + index_uv;


            for (int index = 0; index < width; index += 2)
            {
                int index_r = index_src * 4 + index * 4 + R;
                int index_g = index_src * 4 + index * 4 + G;
                int index_b = index_src * 4 + index * 4 + B;

                i420[index_u] = Convert.ToByte(-src_argb[index_r] * 0.148 - 0.291 * src_argb[index_g] + src_argb[index_b] * 0.439 + 128);
                i420[index_v] = Convert.ToByte(src_argb[index_r] * 0.439 - 0.368 * src_argb[index_g] - src_argb[index_b] * 0.071 + 128);

                index_u++;
                index_v++;
            }
        }

		private static void ARGBToYRow(byte[] src_argb, byte[] i420, int width, int index_src, int index_y)
		{
            for (int index = 0; index < width; index++)
            {
                int index_r = index_src * 4 + index * 4 + R;
                int index_g = index_src * 4 + index * 4 + G;
                int index_b = index_src * 4 + index * 4 + B;

                i420[index_y + index] = Convert.ToByte(src_argb[index_r] * 0.257 + 0.504 * src_argb[index_g] + src_argb[index_r] * 0.098 + 16);

            }
		}
	}
}
