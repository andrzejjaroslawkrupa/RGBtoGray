using System;
using System.Windows.Media.Imaging;
using ImgProcLib;

namespace RGBtoGray.ViewModel
{
	public class ImageProcessingAdapter : IImageProcessingAdapter
	{
		public BitmapImage ConvertImage(Uri imageUri)
		{
			var imageProcessing = new ImageProcessing();
			var originalImage = new BitmapImage(imageUri);
			return imageProcessing.ConvertBitmapImageToGrayscale(originalImage);
		}
	}
}