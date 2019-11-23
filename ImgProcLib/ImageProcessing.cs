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

		public static byte[] GetBitmapPixels(BitmapSource bitmapImage)
		{
			if (bitmapImage == null)
				throw new ArgumentNullException();

			var stride = DetermineStride(bitmapImage);
			var pixels = new byte[bitmapImage.PixelHeight * stride];

			bitmapImage.CopyPixels(pixels, stride, 0);

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

		private static BitmapSource CreateImage(BitmapSource bitmapImage, byte[] pixels)
		{
			var stride = DetermineStride(bitmapImage);

			var bitmapSource = BitmapSource.Create(bitmapImage.PixelWidth, bitmapImage.PixelHeight, bitmapImage.DpiX, bitmapImage.DpiY, bitmapImage.Format, bitmapImage.Palette, pixels, stride);

			return bitmapSource;
		}

		private static int DetermineStride(BitmapSource bitmapImage) =>
			bitmapImage.PixelWidth * (bitmapImage.Format.BitsPerPixel / 8);
	}
}
