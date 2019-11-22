using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImgProcLib
{
	public class ImageProcessing
	{
		private const double R = 0.2126;
		private const double G = 0.7152;
		private const double B = 0.0722;

		public BitmapImage ConvertBitmapImageToGreyscale(BitmapImage bitmapImage)
		{
			var pixels = GetBitmapPixels(bitmapImage);

			pixels = ConvertPixelsToGreyscale(pixels);

			return CreateImage(bitmapImage, pixels);
		}

		public static byte[] GetBitmapPixels(BitmapImage bitmapImage)
		{
			if (bitmapImage == null)
				throw new ArgumentNullException();

			var stride = GetStride(bitmapImage);
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

		private static BitmapImage CreateImage(BitmapSource bitmapImage, byte[] pixels)
		{
			var stride = GetStride(bitmapImage);

			var bitmapSource = BitmapSource.Create(bitmapImage.PixelWidth, bitmapImage.PixelHeight, bitmapImage.DpiX, bitmapImage.DpiY, bitmapImage.Format, bitmapImage.Palette, pixels, stride);

			var encoder = new JpegBitmapEncoder();

			encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
			var memoryStream = new MemoryStream();
			encoder.Save(memoryStream);

			memoryStream.Position = 0;
			var convertedBitmapImage = new BitmapImage();
			convertedBitmapImage.BeginInit();
			convertedBitmapImage.StreamSource = memoryStream;
			convertedBitmapImage.EndInit();

			return convertedBitmapImage;
		}

		private static int GetStride(BitmapSource bitmapImage) =>
			bitmapImage.PixelWidth * (bitmapImage.Format.BitsPerPixel / 8);
	}
}
