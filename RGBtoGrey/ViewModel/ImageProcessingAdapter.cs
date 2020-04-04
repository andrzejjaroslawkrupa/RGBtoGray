using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImgProcLib;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGrey.ViewModel
{
	public class ImageProcessingAdapter : IImageProcessingAdapter
	{
		public async Task<BitmapSource> ConvertImage(string path)
		{
			var uri = new Uri(path);
			var originalImage = new BitmapImage(uri);
			originalImage.Freeze();

			return await Task.Run(() => ImageProcessing.ConvertBitmapImageToGreyscale(originalImage));
		}
	}
}