using System;
using System.Windows.Media.Imaging;
using ImgProcLib;

namespace RGBtoGrey.ViewModel
{
	public class ImageProcessingAdapter : IImageProcessingAdapter
	{
		public BitmapImage ConvertImage(string path)
		{
			var uri = new Uri(path);
			var originalImage = new BitmapImage(uri);

			return ImageProcessing.ConvertBitmapImageToGreyscale(originalImage);
		}
	}
}