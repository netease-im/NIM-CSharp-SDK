using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIMDemo.Beauty
{
	class smooth_info_s
	{
		public Int64[] matrix_;
		public Int32[] matrix_sqr_;
		public Int32[] skin_mask_;
		public int width_;
		public int height_;
	}
	class Smooth
	{
		private static smooth_info_s smooth_init(byte[] in_i420, Int32 width, Int32 height)
		{
			smooth_info_s info = new smooth_info_s();
			info.matrix_ = new Int64[width * height];
			info.matrix_sqr_ = new Int32[width * height];
			info.skin_mask_ = new Int32[width * height];
			info.width_ = width;
			info.height_ = height;
			smooth_init_skin_mask(info, in_i420);
			smooth_init_integral(info, in_i420);
			return info;
		}

		private static void smooth_init_skin_mask(smooth_info_s info, byte[] in_i420)
		{
            int wxh = info.width_ * info.height_;
         
			for (int i = 0; i < info.height_; i++)
			{
				int offset =i * info.width_;
				int offset2 = (i / 2) * (info.width_ / 2); 
                int add_count = 0;
				for (uint j = 0; j < info.width_; j++)
				{
					int src_y = Convert.ToInt32(in_i420[offset]);
					int src_u = Convert.ToInt32(in_i420[wxh + offset2-1]);
					int src_v =in_i420[wxh * 5 / 4 + offset2-1];
					if ((src_u > 77 && src_u < 127) && (src_v > 133 && src_v < 173))
					{
						info.skin_mask_[offset] = 255;
						//in_i420[offset] = 255 - (255 - src_y)*0.9;
					}
					else
					{
						info.skin_mask_[offset] = 0;
					}
					offset++;
					if (j % 2 == 0)
					{
						offset2++;
                        add_count++;
					}
				}
			}
		}

		private static void smooth_init_integral(smooth_info_s info, byte[] in_y)
		{
			int width = info.width_;
			int height = info.height_;
            Int32[] column_sum = new Int32[width];
            Int32[] column_sum_sqr = new Int32[width];

			column_sum[0] = in_y[0];
            column_sum_sqr[0] = in_y[0] * in_y[0];

			info.matrix_[0] = column_sum[0];
			info.matrix_sqr_[0] = column_sum_sqr[0];

			for (uint i = 1; i < width; i++)
			{

				column_sum[i] = in_y[i];
				column_sum_sqr[i] =in_y[i] * in_y[i];

				info.matrix_[i] = column_sum[i];
				info.matrix_[i] += info.matrix_[i - 1];
				info.matrix_sqr_[i] = column_sum_sqr[i];
				info.matrix_sqr_[i] += info.matrix_sqr_[i - 1];
			}
			for (uint i = 1; i < height; i++)
			{
				int offset = Convert.ToInt32(i * width);

				column_sum[0] += in_y[offset];
				column_sum_sqr[0] += in_y[offset] * in_y[offset];

				info.matrix_[offset] = column_sum[0];
				info.matrix_sqr_[offset] = column_sum_sqr[0];
				// other columns  
				for (int j = 1; j < width; j++)
				{
					column_sum[j] += in_y[offset + j];
					column_sum_sqr[j] += in_y[offset + j] * in_y[offset + j];

					info.matrix_[offset + j] = info.matrix_[offset + j - 1] + column_sum[j];
					info.matrix_sqr_[offset + j] = info.matrix_sqr_[offset + j - 1] + column_sum_sqr[j];
				}
			}
		}

		private static void smooth_processing(smooth_info_s info, byte[] in_y, float sigema, int radius, UInt16 alpha)
		{
			radius = Convert.ToInt32(radius > 0 ? radius : Math.Max(info.width_, info.height_) * 0.02);

			for (int i = 1; i < info.height_; i++)
			{
				for (int j = 1; j < info.width_; j++)
				{
					uint offset = Convert.ToUInt32(i * info.width_ + j);
					if (info.skin_mask_[offset] > 0)
					{
						int i_max = i + radius >= info.height_ - 1 ? info.height_ - 1 : i + radius;
						int j_max = j + radius >= info.width_ - 1 ? info.width_ - 1 : j + radius;
						int i_min = i - radius <= 1 ? 1 : i - radius;
						int j_min = j - radius <= 1 ? 1 : j - radius;

						int squar = (i_max - i_min + 1) * (j_max - j_min + 1);
						int i4 = i_max * info.width_ + j_max;
						int i3 = (i_min - 1) * info.width_ + (j_min - 1);
						int i2 = i_max * info.width_ + (j_min - 1);
						int i1 = (i_min - 1) * info.width_ + j_max;

						float m = (info.matrix_[i4]
							+ info.matrix_[i3]
							- info.matrix_[i2]
							- info.matrix_[i1]) / squar;

						float v = (info.matrix_sqr_[i4]
							+ info.matrix_sqr_[i3]
							- info.matrix_sqr_[i2]
							- info.matrix_sqr_[i1]) / squar - m * m;
						float k = v / (v + sigema);
						Int32 temp = Convert.ToInt32(m - k * m + k * in_y[offset]);
						temp = in_y[offset] * (255 - alpha) + temp * alpha;
						temp /= 255;
						in_y[offset] = Convert.ToByte(temp > 255 ? 255 : temp);
					}
				}
			}
		}


		public static void smooth_process(byte[] i420, Int32 width, Int32 height, int sigema_level, int radius, UInt16 alpha)
		{
			smooth_info_s info = smooth_init(i420, width, height);
			smooth_processing(info, i420, sigema_level * sigema_level * 5 + 10, radius, alpha);
			smooth_clear(info);
		}
		private static void smooth_clear(smooth_info_s info)
		{

		}
	}
}
