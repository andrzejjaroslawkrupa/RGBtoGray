using System;
using System.Windows.Media.Imaging;

namespace ImgProcLib
{
	public class ImageProcessing
	{
		public BitmapImage ConvertToGrayscale(BitmapImage originalImage)
		{
			return originalImage;
		}

		public byte[] GetBitmapPixels(BitmapImage bitmapImage)
		{
			if (bitmapImage == null)
				throw new ArgumentNullException();

			int stride = bitmapImage.PixelWidth * (bitmapImage.Format.BitsPerPixel / 8);
			byte[] pixels = new byte[bitmapImage.PixelHeight * stride];

			bitmapImage.CopyPixels(pixels, stride, 0);

			return pixels;
		}
	}
}
