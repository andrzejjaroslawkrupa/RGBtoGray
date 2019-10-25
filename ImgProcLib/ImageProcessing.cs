using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImgProcLib
{
	public class ImageProcessing
	{
		private const double R = 0.2126;
		private const double G = 0.7152;
		private const double B = 0.0722;

		public BitmapImage ConvertBitmapImageToGrayscale(BitmapImage bitmapImage)
		{
			var pixels = GetBitmapPixels(bitmapImage);

			pixels = ConvertPixelsToGrayscale(pixels);

			return CreateImageFile(bitmapImage, pixels);
		}

		public byte[] GetBitmapPixels(BitmapImage bitmapImage)
		{
			if (bitmapImage == null)
				throw new ArgumentNullException();

			int stride = DetermineStride(bitmapImage);
			byte[] pixels = new byte[bitmapImage.PixelHeight * stride];

			bitmapImage.CopyPixels(pixels, stride, 0);

			return pixels;
		}

		private byte[] ConvertPixelsToGrayscale(byte[] pixels)
		{
			for (int y = 0; y < pixels.Length; y += 4)
			{
				pixels[y] = (byte)(R * pixels[y] + G * pixels[y + 1] + B * pixels[y + 2]);
				pixels[y + 1] = pixels[y];
				pixels[y + 2] = pixels[y];
			}

			return pixels;
		}

		private BitmapImage CreateImageFile(BitmapImage bitmapImage, byte[] pixels)
		{
			var stride = DetermineStride(bitmapImage);

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

		private int DetermineStride(BitmapImage bitmapImage)
		{
			return bitmapImage.PixelWidth * (bitmapImage.Format.BitsPerPixel / 8);
		}
	}
}
