using System;
using System.Windows.Media.Imaging;

namespace ImgProcLib
{
	public static class ImageProcessing
	{
		private const double R = 0.2126;
		private const double G = 0.7152;
		private const double B = 0.0722;

		public static BitmapSource ConvertBitmapImageToGreyscale(BitmapSource bitmapSource)
		{
			var pixels = GetBitmapPixels(bitmapSource);

			pixels = ConvertPixelsToGreyscale(pixels);

			return CreateImage(bitmapSource, pixels);
		}

		public static byte[] GetBitmapPixels(BitmapSource bitmapSource)
		{
			if (bitmapSource == null)
				throw new ArgumentNullException();

			var stride = CalculateStride(bitmapSource);
			var pixels = new byte[bitmapSource.PixelHeight * stride];

			bitmapSource.CopyPixels(pixels, stride, 0);

			return pixels;
		}

		private static byte[] ConvertPixelsToGreyscale(byte[] pixels)
		{
			for (var i = 0; i < pixels.Length; i += 4)
			{
				pixels[i] = (byte)(R * pixels[i] + G * pixels[i + 1] + B * pixels[i + 2]);
				pixels[i + 1] = pixels[i];
				pixels[i + 2] = pixels[i];
			}

			return pixels;
		}

		private static BitmapSource CreateImage(BitmapSource inputBitmapSource, byte[] pixels)
		{
			var stride = CalculateStride(inputBitmapSource);

			var outputBitmapSource = BitmapSource.Create(inputBitmapSource.PixelWidth, inputBitmapSource.PixelHeight, inputBitmapSource.DpiX, inputBitmapSource.DpiY, inputBitmapSource.Format, inputBitmapSource.Palette, pixels, stride);

			return outputBitmapSource;
		}

		private static int CalculateStride(BitmapSource bitmapSource) =>
			bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
	}
}
