using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIMDemo.Beauty
{
	class ColorBalance
	{
		public static void colorbalance_rgb_u8(byte[] rgb, UInt64 size, UInt32 nb_min, UInt32 nb_max, UInt32 step)
		{
			balance_u8(rgb, size, nb_min, nb_max, 0, step);
			balance_u8(rgb, size, nb_min, nb_max, 1, step);
			balance_u8(rgb, size, nb_min, nb_max, 2, step);
		}

        public static void colorbalance_yuv_u8(byte[] yuv, UInt64 size, UInt32 nb_min, UInt32 nb_max)
        {
            balance_u8(yuv, size, nb_min, nb_max, 0, 1); //对y值取平衡,对亮度进行调整
        }

		private static void balance_u8(byte[] data, UInt64 size, UInt32 nb_min, UInt32 nb_max, UInt32 offset, UInt32 step)
		{
			int min = 0, max = 0;

			/* sanity checks */
			if (data.Length <= 0)
			{
				System.Diagnostics.Debug.Assert(false);
			}
			if (nb_min + nb_max >= size)
			{
				nb_min = Convert.ToUInt32((size - 1) / 2);
				nb_max = Convert.ToUInt32((size - 1) / 2);

			}

			/* get the min/max */
			if (0 != nb_min || 0 != nb_max)
				quantiles_u8(data,(int) size, (int)nb_min, (int)nb_max, ref min, ref max, offset, step);
			else
				minmax_u8(data, size,ref min, ref max, offset, step);

			/* rescale 
			*/
			rescale_u8(data,(int) size, min, max, offset, step);

		}

		private static void quantiles_u8(byte[] data, int size, int nb_min, int nb_max, ref int min, ref int max, UInt32 offset, UInt32 step)
		{
			/*
		* the histogram must hold all possible "unsigned char" values,
		* including 0
		*/
			int h_size = 255 + 1;
			int[] histo = new int[256];
			int i;

			/* make a cumulative histogram
			* 构建直方图
			*/
			for (i = 0; i < size; i++)
				histo[data[i * step + offset]] += 1;
			for (i = 1; i < h_size; i++)
				histo[i] += histo[i - 1];

			/* get the new min/max */

			if (0 == min)
			{
				/* simple forward traversal of the cumulative histogram */
				/* search the first value > nb_min */
				i = 0;
				while (i < h_size && histo[i] <= nb_min)
					i++;
				/* the corresponding histogram value is the current cell position */
				min = i;
			}

			if (0 == max)
			{
				/* simple backward traversal of the cumulative histogram */
				/* search the first value <= size - nb_max */
				i = h_size - 1;
				/* i is unsigned, we check i<h_size instead of i>=0 */
				while (i < h_size && histo[i] > (size - nb_max))
					i--;
				/*
				* if we are not at the end of the histogram,
				* get to the next cell,
				* the last (backward) value > size - nb_max
				*/
				if (i < h_size - 1)
					i++;
				max =i;
			}
			return;
		}

		private static void minmax_u8(byte[] data, UInt64 size, ref int ptr_min, ref int ptr_max, UInt32 offset, UInt32 step)
		{
			int min, max;
			UInt32 i;
			UInt32 index = 0;
			/* compute min and max */
			min = data[0 + offset];
			max = data[0 + offset];
			for (i = 1; i < size; i++)
			{
				index = i * step + offset;

				if (data[index] < min)
					min = data[index];
				if (data[index] > max)
					max = data[index];
			}

			/* save min and max to the returned pointers if available */
			if (0 != ptr_min)
				ptr_min = min;
			if (0 != ptr_max)
				ptr_max = max;
			return;
		}

		private static void rescale_u8(byte[] data, int size,int min,int max, UInt32 offset, UInt32 step)
		{
			int i;

			if (max <= min)
				for (i = 0; i < size; i++)
					data[i * step + offset] =Convert.ToByte(255 / 2);
			else
			{
				/* build a normalization table */
				byte[] norm = new byte[256];
				for (i = 0; i < min; i++)
					norm[i] = 0;
				for (i = min; i < max; i++)
					/*
					* we can't store and reuse UCHAR_MAX / (max - min) because
					*     105 * 255 / 126.            -> 212.5, rounded to 213
					*     105 * (double) (255 / 126.) -> 212.4999, rounded to 212
					*/
					norm[i] = (byte)((i - min) * 255
					/ (double)(max - min) + .5);
				for (i = max; i < 255 + 1; i++)
					norm[i] =(byte)255;
				/* use the normalization table to transform the data */
				for (i = 0; i < size; i++)
					data[i * step + offset] = norm[(UInt32)data[i * step + offset]];
			}
		}
	}
}
